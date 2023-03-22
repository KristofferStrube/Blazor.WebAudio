using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebAudio.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class AudioNode : EventTarget
{
    protected readonly Lazy<Task<IJSObjectReference>> webAudioHelperTask;

    public static Task<AudioNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AudioNode(jSRuntime, jSReference));
    }

    protected AudioNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        webAudioHelperTask = new(jSRuntime.GetHelperAsync);
    }

    public async Task<AudioNode> ConnectAsync(AudioNode destinationNode, ulong output = 0, ulong input = 0)
    {
        var jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("connect", destinationNode.JSReference, output, input);
        return await CreateAsync(JSRuntime, jSInstance);
    }

    public async Task DisconnectAsync()
    {
        await JSReference.InvokeVoidAsync("disconnect");
    }
}
