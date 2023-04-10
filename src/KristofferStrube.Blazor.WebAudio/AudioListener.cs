using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class AudioListener : BaseJSWrapper
{
    public static Task<AudioListener> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AudioListener(jSRuntime, jSReference));
    }

    protected AudioListener(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
