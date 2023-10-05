﻿using KristofferStrube.Blazor.WebAudio.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="OscillatorNode"/> represents an audio source generating a periodic waveform. It can be set to a few commonly used waveforms. Additionally, it can be set to an arbitrary periodic waveform through the use of a <see cref="PeriodicWave"/> object.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#OscillatorNode">See the API definition here</see>.</remarks>
public class OscillatorNode : AudioScheduledSourceNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of an <see cref="OscillatorNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="OscillatorNode"/>.</param>
    /// <returns>A wrapper instance for an <see cref="OscillatorNode"/>.</returns>
    public static new Task<OscillatorNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new OscillatorNode(jSRuntime, jSReference));
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
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructOcillatorNode", context.JSReference, options);
        return new OscillatorNode(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of an <see cref="OscillatorNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="OscillatorNode"/>.</param>
    protected OscillatorNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    /// <summary>
    /// The frequency (in Hertz) of the periodic waveform.
    /// Its default value is <c>440</c>.
    /// This parameter is a-rate. It forms a compound parameter with detune to form the computedOscFrequency.
    /// Its nominal range is [-Nyquist frequency, Nyquist frequency].
    /// </summary>
    public async Task<AudioParam> GetFrequencyAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "frequency");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance);
    }
}
