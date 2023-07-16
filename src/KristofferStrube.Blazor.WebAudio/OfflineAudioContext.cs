using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="OfflineAudioContext"/> is a particular type of <see cref="BaseAudioContext"/> for rendering/mixing-down (potentially) faster than real-time.
/// It does not render to the audio hardware, but instead renders as quickly as possible, fulfilling the returned promise with the rendered result as an <see cref="AudioBuffer"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#offlineaudiocontext">See the API definition here</see>.</remarks>
public class OfflineAudioContext : BaseAudioContext
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="OfflineAudioContext"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="OfflineAudioContext"/>.</param>
    protected OfflineAudioContext(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
