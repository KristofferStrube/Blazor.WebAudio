using KristofferStrube.Blazor.DOM;
using System.Numerics;

namespace KristofferStrube.Blazor.WebAudio;

//
public partial class PullAudioWorkletProcessor : AudioWorkletProcessor, IAsyncDisposable
{
    private readonly MessagePort messagePort;
    private readonly EventListener<MessageEvent> messageEventListener;

    /// <inheritdoc/>
    public override AudioWorkletNode Node { get; }

    private PullAudioWorkletProcessor(AudioWorkletNode node, MessagePort messagePort, EventListener<MessageEvent> messageEventListener)
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

        TNumber[] ProduceArray<TNumber>(int chunks) where TNumber : INumber<TNumber>
        {
            TNumber[] result;
            if (options.ProduceStereo is not null)
            {
                result = new TNumber[chunks * 128 * 2];
                for (int i = 0; i < chunks; i++)
                {
                    for (int j = 0; j < 128; j++)
                    {
                        (double left, double right) = options.ProduceStereo();
                        if (options.Resolution is Resolution.Byte)
                        {
                            result[(i * 128 * 2) + j] = TNumber.CreateTruncating((left + 1) / 2 * 255);
                            result[(i * 128 * 2) + 128 + j] = TNumber.CreateTruncating((right + 1) / 2 * 255);
                        }
                        else
                        {
                            result[(i * 128 * 2) + j] = TNumber.CreateTruncating(left);
                            result[(i * 128 * 2) + 128 + j] = TNumber.CreateTruncating(right);
                        }
                    }
                }
            }
            else if (options.ProduceMono is not null)
            {
                result = new TNumber[chunks * 128];
                for (int i = 0; i < chunks; i++)
                {
                    for (int j = 0; j < 128; j++)
                    {
                        double monoSound = options.ProduceMono();
                        if (options.Resolution is Resolution.Byte)
                        {
                            result[(i * 128) + j] = TNumber.CreateTruncating((monoSound + 1) / 2 * 255);
                        }
                        else
                        {
                            result[(i * 128) + j] = TNumber.CreateTruncating(monoSound);
                        }
                    }
                }
            }
            else
            {
                result = Enumerable.Range(0, 128 * chunks).Select(_ => TNumber.Zero).ToArray();
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
                ["resolution"] = options.Resolution is Resolution.Byte ? 255 : 1,
            },
            OutputChannelCount = new ulong[] { channels }
        };

        AudioWorkletNode audioWorkletNode = await AudioWorkletNode.CreateAsync(audioContext.JSRuntime, audioContext, "kristoffer-strube-webaudio-pull-audio-processor", nodeOptions);

        MessagePort messagePort = await audioWorkletNode.GetPortAsync();
        await messagePort.StartAsync();
        EventListener<MessageEvent> messageEventListener = await EventListener<MessageEvent>.CreateAsync(audioContext.JSRuntime, async (e) =>
        {
            if (options.Resolution is Resolution.Byte)
            {
                await messagePort.PostMessageAsync(ProduceArray<byte>(options.BufferRequestSize));
            }
            else
            {
                await messagePort.PostMessageAsync(ProduceArray<double>(options.BufferRequestSize));
            }
        });
        await messagePort.AddOnMessageEventListenerAsync(messageEventListener);

        return new PullAudioWorkletProcessor(audioWorkletNode, messagePort, messageEventListener);
    }

    /// <summary>
    /// Removes the event listener that listens for events from the audio worklet processor, closes the message port, and disposes these and the <see cref="Node"/>.
    /// </summary>
    /// <returns></returns>
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
