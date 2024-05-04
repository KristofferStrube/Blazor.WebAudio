using KristofferStrube.Blazor.DOM;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class PullAudioWorkletProcessor : AudioWorkletProcessor, IAsyncDisposable
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
    public static async Task<PullAudioWorkletProcessor> CreateAsync(BaseAudioContext audioContext, int lowTide = 100, int highTide = 500, int bufferRequestSize = 100)
    {
        await using AudioWorklet audioWorklet = await audioContext.GetAudioWorkletAsync();
        await audioWorklet.AddModuleAsync("./_content/KristofferStrube.Blazor.WebAudio/KristofferStrube.Blazor.WebAudio.PullAudioProcessor.js");

        AudioWorkletNodeOptions options = new()
        {
            ParameterData = new()
            {
                ["lowTide"] = lowTide,
                ["highTide"] = highTide,
                ["bufferRequestSize"] = bufferRequestSize,
            }
        };

        AudioWorkletNode audioWorkletNode = await AudioWorkletNode.CreateAsync(audioContext.JSRuntime, audioContext, "kristoffer-strube-webaudio-pull-audio-processor", options);

        MessagePort messagePortAsynchronous = await audioWorkletNode.GetPortAsync();
        MessagePortInProcess messagePort = await MessagePortInProcess.CreateAsync(audioContext.JSRuntime, (IJSInProcessObjectReference)messagePortAsynchronous.JSReference);
        await messagePort.StartAsync();
        EventListener<MessageEvent> messageEventListener = await EventListener<MessageEvent>.CreateAsync(audioContext.JSRuntime, async (e) =>
        {
            int dataNeededToReachLowTide = await e.GetDataAsync<int>();
            Console.WriteLine(dataNeededToReachLowTide);
            messagePort.PostMessage(
                Enumerable
                .Range(0, dataNeededToReachLowTide)
                    .Select(
                        _ => Enumerable.Range(0, 128).Select(_ => Random.Shared.NextDouble() * 2 - 1).ToArray()
                    ).ToArray()
            );
        });
        await messagePort.AddOnMessageEventListenerAsync(messageEventListener);

        await messagePort.PostMessageAsync(
            Enumerable
                .Range(0, 100)
                .Select(
                    _ => Enumerable.Range(0, 128).Select(_ => Random.Shared.NextDouble() * 2 - 1).ToArray()
                ).ToArray()
        );

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
