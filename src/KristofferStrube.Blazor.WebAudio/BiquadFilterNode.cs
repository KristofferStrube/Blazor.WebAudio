using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="BiquadFilterNode"/> is an <see cref="AudioNode"/> processor implementing very common low-order filters.<br />
/// Low-order filters are the building blocks of basic tone controls (bass, mid, treble), graphic equalizers, and more advanced filters.
/// Multiple BiquadFilterNode filters can be combined to form more complex filters.
/// The filter parameters such as <see cref="GetFrequencyAsync"/> can be changed over time for filter sweeps, etc.
/// Each BiquadFilterNode can be configured as one of a number of common filter types.
/// The default filter type is <c>"lowpass"</c>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#BiquadFilterNode">See the API definition here</see>.</remarks>
public class BiquadFilterNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of an <see cref="BiquadFilterNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="BiquadFilterNode"/>.</param>
    /// <returns>A wrapper instance for an <see cref="BiquadFilterNode"/>.</returns>
    public static new Task<BiquadFilterNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new BiquadFilterNode(jSRuntime, jSReference));
    }

    private BiquadFilterNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    /// <summary>
    /// A detune value, in cents, for the frequency.
    /// It forms a compound parameter with <see cref="GetFrequencyAsync"/> to form the computedFrequency.
    /// </summary>
    /// <returns>An <see cref="AudioParam"/></returns>
    public Task<AudioParam> GetDetuneAsync()
    {
        return Task.FromResult<AudioParam>(null!);
    }

    /// <summary>
    /// The frequency at which the <see cref="BiquadFilterNode"/> will operate, in Hz.
    /// It forms a compound parameter with <see cref="GetDetuneAsync"/> to form the computedFrequency.
    /// </summary>
    /// <returns>An <see cref="AudioParam"/></returns>
    public Task<AudioParam> GetFrequencyAsync()
    {
        return Task.FromResult<AudioParam>(null!);
    }
}
