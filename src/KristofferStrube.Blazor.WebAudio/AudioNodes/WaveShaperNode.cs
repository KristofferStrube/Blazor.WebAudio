using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="WaveShaperNode"/> is an <see cref="AudioNode"/> processor implementing non-linear distortion effects.<br />
/// Non-linear waveshaping distortion is commonly used for both subtle non-linear warming, or more obvious distortion effects.
/// Arbitrary non-linear shaping curves may be specified.
/// The number of channels of the output always equals the number of channels of the input.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#WaveShaperNode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class WaveShaperNode : AudioNode, IJSCreatable<WaveShaperNode>
{
    /// <inheritdoc/>
    public static new async Task<WaveShaperNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<WaveShaperNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new WaveShaperNode(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected WaveShaperNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }
}
