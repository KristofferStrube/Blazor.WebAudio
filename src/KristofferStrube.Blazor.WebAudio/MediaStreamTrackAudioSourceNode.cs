using KristofferStrube.Blazor.MediaCaptureStreams;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents an audio source from a <see cref="MediaStreamTrack"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#MediaStreamTrackAudioSourceNode">See the API definition here</see>.</remarks>
public class MediaStreamTrackAudioSourceNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaStreamTrackAudioSourceNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaStreamTrackAudioSourceNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="MediaStreamTrackAudioSourceNode"/>.</returns>
    public static new Task<MediaStreamTrackAudioSourceNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new MediaStreamTrackAudioSourceNode(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaStreamTrackAudioSourceNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaStreamTrackAudioSourceNode"/>.</param>
    protected MediaStreamTrackAudioSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }
}
