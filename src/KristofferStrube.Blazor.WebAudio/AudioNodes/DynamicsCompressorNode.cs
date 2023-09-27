using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="DynamicsCompressorNode"/> is an <see cref="AudioNode"/> processor implementing a dynamics compression effect.<br />
/// Dynamics compression is very commonly used in musical production and game audio.
/// It lowers the volume of the loudest parts of the signal and raises the volume of the softest parts.
/// Overall, a louder, richer, and fuller sound can be achieved.
/// It is especially important in games and musical applications where large numbers of individual sounds are played simultaneous to control the overall signal level and help avoid clipping (distorting) the audio output to the speakers.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#DynamicsCompressorNode">See the API definition here</see>.</remarks>
public class DynamicsCompressorNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="DynamicsCompressorNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="DynamicsCompressorNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="DynamicsCompressorNode"/>.</returns>
    public static new Task<DynamicsCompressorNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new DynamicsCompressorNode(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="DynamicsCompressorNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="DynamicsCompressorNode"/>.</param>
    protected DynamicsCompressorNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }
}
