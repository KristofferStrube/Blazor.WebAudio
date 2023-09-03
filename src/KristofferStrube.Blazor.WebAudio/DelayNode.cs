using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// A delay-line is a fundamental building block in audio applications.
/// This interface is an <see cref="AudioNode"/> with a single input and single output.
/// The number of channels of the output always equals the number of channels of the input.<br />
/// It delays the incoming audio signal by a certain amount.
/// Specifically, at each time <c>t</c>, input signal <c>input(t)</c>, delay time <c>delayTime(t)</c> and output signal <c>output(t)</c>, the output will be <c>output(t) = input(t - delayTime(t))</c>.
/// The default delayTime is <c>0</c> seconds (no delay).
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#DelayNode">See the API definition here</see>.</remarks>
public class DelayNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="DelayNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="DelayNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="DelayNode"/>.</returns>
    public static new Task<DelayNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new DelayNode(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="DelayNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="DelayNode"/>.</param>
    protected DelayNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }
}
