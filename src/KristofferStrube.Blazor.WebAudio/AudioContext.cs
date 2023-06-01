using KristofferStrube.Blazor.MediaCaptureStreams;
using KristofferStrube.Blazor.WebAudio.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class AudioContext : BaseAudioContext
{
    // Todo: Add AudioContextOptions parameter
    public static async Task<AudioContext> CreateAsync(IJSRuntime jSRuntime, object? contextOptions = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructAudioContext", contextOptions);
        return new AudioContext(jSRuntime, jSInstance);
    }

    protected AudioContext(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task ResumeAsync()
    {
        await JSReference.InvokeVoidAsync("resume");
    }

    public async Task SuspendAsync()
    {
        await JSReference.InvokeVoidAsync("suspend");
    }

    public async Task CloseAsync()
    {
        await JSReference.InvokeVoidAsync("close");
    }

    public async Task<MediaStreamAudioSourceNode> CreateMediaStreamSourceAsync(MediaStream mediaStream)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createMediaStreamSource", mediaStream.JSReference);
        return await MediaStreamAudioSourceNode.CreateAsync(JSRuntime, jSInstance);
    }
}
