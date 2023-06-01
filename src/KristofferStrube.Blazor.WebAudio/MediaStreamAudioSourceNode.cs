using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class MediaStreamAudioSourceNode : AudioNode
{
    public static new Task<MediaStreamAudioSourceNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new MediaStreamAudioSourceNode(jSRuntime, jSReference));
    }

    protected MediaStreamAudioSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }
}
