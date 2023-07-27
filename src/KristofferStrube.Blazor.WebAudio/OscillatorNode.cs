using KristofferStrube.Blazor.WebAudio.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="OscillatorNode"/> represents an audio source generating a periodic waveform. It can be set to a few commonly used waveforms. Additionally, it can be set to an arbitrary periodic waveform through the use of a <see cref="IPeriodicWave"/> object.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#OscillatorNode">See the API definition here</see>.</remarks>
public class OscillatorNode : AudioScheduledSourceNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="OscillatorNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="OscillatorNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="OscillatorNode"/>.</returns>
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
    /// <returns>A new instance of a <see cref="OscillatorNode"/>.</returns>
    public static async Task<OscillatorNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, OscillatorOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = options is null
            ? await helper.InvokeAsync<IJSObjectReference>("constructOcillatorNode", context.JSReference)
            : await helper.InvokeAsync<IJSObjectReference>("constructOcillatorNode", context.JSReference,
            new
            {
                type = options!.Type.AsString(),
                frequency = options!.Frequency,
                detune = options!.Detune,
                // Missing periodicWave
            });
        return new OscillatorNode(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="OscillatorNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="OscillatorNode"/>.</param>
    protected OscillatorNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
