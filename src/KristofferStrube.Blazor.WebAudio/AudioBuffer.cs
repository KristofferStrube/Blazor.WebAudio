using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class AudioBuffer : BaseJSWrapper
{
    public AudioBuffer(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
