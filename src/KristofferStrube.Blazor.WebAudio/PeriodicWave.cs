using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="PeriodicWave"/> represents an arbitrary periodic waveform to be used with an <see cref="OscillatorNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#periodicwave">See the API definition here</see>.</remarks>
public class PeriodicWave : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="PeriodicWave"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="PeriodicWave"/>.</param>
    /// <returns>A wrapper instance for a <see cref="PeriodicWave"/>.</returns>
    public static Task<PeriodicWave> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new PeriodicWave(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="PeriodicWave"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="PeriodicWave"/>.</param>
    public PeriodicWave(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
