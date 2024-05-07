using KristofferStrube.Blazor.DOM;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

//
public partial class PullAudioWorkletProcessor : AudioWorkletProcessor, IAsyncDisposable
{
    private readonly MessagePortInProcess messagePort;
    private readonly EventListener<MessageEvent> messageEventListener;

    /// <inheritdoc/>
    public override AudioWorkletNode Node { get; }

    private PullAudioWorkletProcessor(AudioWorkletNode node, MessagePortInProcess messagePort, EventListener<MessageEvent> messageEventListener)
    {
        Node = node;
        this.messagePort = messagePort;
        this.messageEventListener = messageEventListener;
    }

    /// <inheritdoc/>
    public static async Task<PullAudioWorkletProcessor> CreateAsync(BaseAudioContext audioContext, Options options)
    {
        await using AudioWorklet audioWorklet = await audioContext.GetAudioWorkletAsync();
        await audioWorklet.AddModuleAsync("./_content/KristofferStrube.Blazor.WebAudio/KristofferStrube.Blazor.WebAudio.PullAudioProcessor.js");

        ulong channels = (ulong)(options.ProduceStereo is not null ? 2 : 1);

        double[] ProduceArray(int chunks)
        {
            double[] result;
            if (options.ProduceStereo is not null)
            {
                result = new double[chunks * 128 * 2];
                for (int i = 0; i < chunks; i++)
                {
                    for (int j = 0; j < 128; j++)
                    {
                        (double left, double right) = options.ProduceStereo();
                        result[(i * 128 * 2) + j] = left;
                        result[(i * 128 * 2) + 128 + j] = right;
                    }
                }
            }
            else if (options.ProduceMono is not null)
            {
                result = new double[chunks * 128];
                for (int i = 0; i < chunks; i++)
                {
                    for (int j = 0; j < 128; j++)
                    {
                        double monoSound = options.ProduceMono();
                        result[(i * 128) + j] = monoSound;
                    }
                }
            }
            else
            {
                result = Enumerable.Range(0, 128 * chunks).Select(_ => 0.0).ToArray();
            }
            return result;
        }

        AudioWorkletNodeOptions nodeOptions = new()
        {
            ParameterData = new()
            {
                ["lowTide"] = options.LowTide,
                ["highTide"] = options.HighTide,
                ["bufferRequestSize"] = options.BufferRequestSize,
            },
            OutputChannelCount = new ulong[] { channels }
        };

        AudioWorkletNode audioWorkletNode = await AudioWorkletNode.CreateAsync(audioContext.JSRuntime, audioContext, "kristoffer-strube-webaudio-pull-audio-processor", nodeOptions);

        MessagePort messagePortAsynchronous = await audioWorkletNode.GetPortAsync();
        MessagePortInProcess messagePort = await MessagePortInProcess.CreateAsync(audioContext.JSRuntime, (IJSInProcessObjectReference)messagePortAsynchronous.JSReference);
        await messagePort.StartAsync();
        EventListener<MessageEvent> messageEventListener = await EventListener<MessageEvent>.CreateAsync(audioContext.JSRuntime, async (e) =>
        {
            messagePort.PostMessage(ProduceArray(options.BufferRequestSize));
        });
        await messagePort.AddOnMessageEventListenerAsync(messageEventListener);

        return new PullAudioWorkletProcessor(audioWorkletNode, messagePort, messageEventListener);
    }

    public async ValueTask DisposeAsync()
    {
        if (messageEventListener is not null && messagePort is not null)
        {
            await messagePort.RemoveOnMessageEventListenerAsync(messageEventListener);
            await messageEventListener.DisposeAsync();
        }

        if (messagePort is not null)
        {
            await messagePort.CloseAsync();
            await messagePort.DisposeAsync();
        }

        if (Node is not null)
        {
            await Node.DisconnectAsync();
            await Node.DisposeAsync();
        }
    }
}
