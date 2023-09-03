using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="IIRFilterNode"/> is an <see cref="AudioNode"/> processor implementing a general IIR Filter.<br />
/// In general, it is best to use <see cref="BiquadFilterNode"/>'s to implement higher-order filters for the following reasons:
/// <list type="bullet">
///     <item>Generally less sensitive to numeric issues.</item>
///     <item>Filter parameters can be automated.</item>
///     <item>Can be used to create all even-ordered IIR filters.</item>
/// </list>
/// However, odd-ordered filters cannot be created, so if such filters are needed or automation is not needed, then IIR filters may be appropriate.<br />
/// Once created, the coefficients of the IIR filter cannot be changed.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#IIRFilterNode">See the API definition here</see>.</remarks>
public class IIRFilterNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="IIRFilterNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="IIRFilterNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="IIRFilterNode"/>.</returns>
    public static new Task<IIRFilterNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new IIRFilterNode(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="IIRFilterNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="IIRFilterNode"/>.</param>
    protected IIRFilterNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }
}
