using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents the position and orientation of the person listening to the audio scene. All <see cref="PannerNode"/> objects spatialize in relation to the <see cref="BaseAudioContext"/>'s listener.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioListener">See the API definition here</see>.</remarks>
public class AudioListener : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioListener"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioListener"/>.</param>
    /// <returns>A wrapper instance for a <see cref="AudioListener"/>.</returns>
    public static Task<AudioListener> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AudioListener(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioListener"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioListener"/>.</param>
    protected AudioListener(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
