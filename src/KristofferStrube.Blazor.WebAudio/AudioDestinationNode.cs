using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class AudioDestinationNode : AudioNode
{
    public static Task<AudioDestinationNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AudioDestinationNode(jSRuntime, jSReference));
    }

    protected AudioDestinationNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
