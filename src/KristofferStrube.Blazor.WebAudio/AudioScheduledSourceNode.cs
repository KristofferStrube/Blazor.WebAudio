using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class AudioScheduledSourceNode : AudioNode
{
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
