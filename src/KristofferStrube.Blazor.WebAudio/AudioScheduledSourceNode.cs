using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The interface represents the common features of source nodes such as <see cref="AudioBufferSourceNode"/>, <see cref="ConstantSourceNode"/>, and <see cref="OscillatorNode"/>.
/// Before a source is started (by calling <see cref="StartAsync(double)"/>, the source node will output silence (0).
/// After a source has been stopped (by calling <see cref="StopAsync(double)"/>), the source will then output silence (0).
/// <see cref="AudioScheduledSourceNode"/> cannot be instantiated directly, but is instead extended by the concrete interfaces for the source nodes.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioScheduledSourceNode">See the API definition here</see>.</remarks>
public class AudioScheduledSourceNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioScheduledSourceNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioScheduledSourceNode"/>.</param>
    protected AudioScheduledSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    /// <summary>
    /// Schedules a sound to playback at an exact time.
    /// </summary>
    /// <param name="when">The when parameter describes at what time (in seconds) the sound should start playing. It is in the same time coordinate system as the AudioContext's currentTime attribute.</param>
    public async Task StartAsync(double when = 0)
    {
        await JSReference.InvokeVoidAsync("start", when);
    }

    /// <summary>
    /// Schedules a sound to stop playback at an exact time.
    /// </summary>
    /// <param name="when">The when parameter describes at what time (in seconds) the source should stop playing. It is in the same time coordinate system as the AudioContext's currentTime attribute.</param>
    public async Task StopAsync(double when = 0)
    {
        await JSReference.InvokeVoidAsync("stop", when);
    }
}
