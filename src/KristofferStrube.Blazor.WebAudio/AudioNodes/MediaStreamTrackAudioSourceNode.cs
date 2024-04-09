using KristofferStrube.Blazor.MediaCaptureStreams;
using KristofferStrube.Blazor.WebAudio.Extensions;
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
    /// Creates a <see cref="MediaStreamTrackAudioSourceNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="MediaStreamTrackAudioSourceNode"/> will be associated with.</param>
    /// <param name="options">Initial parameter value for this <see cref="MediaStreamTrackAudioSourceNode"/>.</param>
    /// <returns>A new instance of a <see cref="MediaStreamTrackAudioSourceNode"/>.</returns>
    public static async Task<MediaStreamTrackAudioSourceNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, MediaStreamTrackAudioSourceOptions options)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructMediaStreamTrackAudioSourceNode", context, options);
        return new MediaStreamTrackAudioSourceNode(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaStreamTrackAudioSourceNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaStreamTrackAudioSourceNode"/>.</param>
    protected MediaStreamTrackAudioSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }
}
