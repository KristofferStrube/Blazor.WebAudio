using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="PeriodicWave"/> represents an arbitrary periodic waveform to be used with an <see cref="OscillatorNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#periodicwave">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class PeriodicWave : BaseJSWrapper, IJSCreatable<PeriodicWave>
{
    /// <inheritdoc/>
    public static async Task<PeriodicWave> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<PeriodicWave> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new PeriodicWave(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    public PeriodicWave(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }
}
