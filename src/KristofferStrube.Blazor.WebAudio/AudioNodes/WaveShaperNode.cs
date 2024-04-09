using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="WaveShaperNode"/> is an <see cref="AudioNode"/> processor implementing non-linear distortion effects.<br />
/// Non-linear waveshaping distortion is commonly used for both subtle non-linear warming, or more obvious distortion effects.
/// Arbitrary non-linear shaping curves may be specified.
/// The number of channels of the output always equals the number of channels of the input.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#WaveShaperNode">See the API definition here</see>.</remarks>
public class WaveShaperNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="WaveShaperNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="WaveShaperNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="WaveShaperNode"/>.</returns>
    public static new Task<WaveShaperNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new WaveShaperNode(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="WaveShaperNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="WaveShaperNode"/>.</param>
    protected WaveShaperNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
