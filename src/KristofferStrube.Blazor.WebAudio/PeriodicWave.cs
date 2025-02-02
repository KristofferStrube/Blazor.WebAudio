using KristofferStrube.Blazor.WebAudio.Extensions;
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

    /// <summary>
    /// Creates a <see cref="PeriodicWave"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="PeriodicWave"/> will be associated with.</param>
    /// <param name="options">Initial parameter value for this <see cref="PeriodicWave"/>.</param>
    /// <returns>A new instance of a <see cref="PeriodicWave"/>.</returns>
    public static async Task<PeriodicWave> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, PeriodicWaveOptions options)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructPeriodicWave", context, options);
        return new PeriodicWave(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected PeriodicWave(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }
}
