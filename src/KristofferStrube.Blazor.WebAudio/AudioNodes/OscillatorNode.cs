using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="OscillatorNode"/> represents an audio source generating a periodic waveform. It can be set to a few commonly used waveforms. Additionally, it can be set to an arbitrary periodic waveform through the use of a <see cref="PeriodicWave"/> object.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#OscillatorNode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class OscillatorNode : AudioScheduledSourceNode, IJSCreatable<OscillatorNode>
{
    /// <inheritdoc/>
    public static new async Task<OscillatorNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<OscillatorNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new OscillatorNode(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Creates an <see cref="OscillatorNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="OscillatorNode"/> will be associated with.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="OscillatorNode"/>.</param>
    /// <returns>A new instance of an <see cref="OscillatorNode"/>.</returns>
    public static async Task<OscillatorNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, OscillatorOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructOcillatorNode", context, options);
        return new OscillatorNode(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected OscillatorNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    /// <summary>
    /// The shape of the periodic waveform.
    /// The default value is <see cref="OscillatorType.Sine"/>.
    /// </summary>
    public async Task<OscillatorType> GetTypeAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<OscillatorType>("getAttribute", JSReference, "type");
    }

    /// <summary>
    /// The shape of the periodic waveform.
    /// The <see cref="SetPeriodicWaveAsync"/> method can be used to set a custom waveform, which results in this attribute being set to <see cref="OscillatorType.Custom"/>.
    /// The default value is <see cref="OscillatorType.Sine"/>.
    /// When this attribute is set, the phase of the oscillator will be conserved.
    /// </summary>
    public async Task SetTypeAsync(OscillatorType value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "type", value);
    }

    /// <summary>
    /// The frequency (in Hertz) of the periodic waveform.
    /// Its default value is <c>440</c>.
    /// This parameter is <see cref="AutomationRate.ARate"/>.
    /// It forms a compound parameter with detune to form the computedOscFrequency.
    /// Its nominal range is [-Nyquist frequency, Nyquist frequency].
    /// </summary>
    public async Task<AudioParam> GetFrequencyAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "frequency");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// A detuning value (in cents) which will offset the frequency by the given amount.
    /// Its default value is <c>0</c>.
    /// This parameter is <see cref="AutomationRate.ARate"/>.
    /// It forms a compound parameter with frequency to form the computedOscFrequency.
    /// The nominal range listed below allows this parameter to detune the frequency over the entire possible range of frequencies.
    /// </summary>
    public async Task<AudioParam> GetDetuneAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "detune");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Sets an arbitrary custom periodic waveform given a <see cref="PeriodicWave"/>.
    /// </summary>
    /// <param name="periodicWave">Custom waveform to be used by the oscillator</param>
    /// <returns></returns>
    public async Task SetPeriodicWaveAsync(PeriodicWave periodicWave)
    {
        await JSReference.InvokeVoidAsync("setPeriodicWave", periodicWave.JSReference);
    }
}
