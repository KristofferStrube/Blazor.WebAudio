using KristofferStrube.Blazor.MediaCaptureStreams;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface is an audio destination representing a <see cref="MediaStream"/> with a single <see cref="MediaStreamTrack"/> whose kind is <see cref="MediaStreamTrackKind.Audio"/>. This <see cref="MediaStream"/> is created when the node is created and is accessible via <see cref="GetStreamAsync"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#MediaStreamTrackAudioSourceNode">See the API definition here</see>.</remarks>
public class MediaStreamAudioDestinationNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaStreamAudioDestinationNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaStreamAudioDestinationNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="MediaStreamAudioDestinationNode"/>.</returns>
    public static new Task<MediaStreamAudioDestinationNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new MediaStreamAudioDestinationNode(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaStreamAudioDestinationNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaStreamAudioDestinationNode"/>.</param>
    protected MediaStreamAudioDestinationNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// A <see cref="MediaStream"/> containing a single <see cref="MediaStreamTrack"/> with the same number of channels as the node itself, and whose kind is <see cref="MediaStreamTrackKind.Audio"/>.
    /// </summary>
    /// <returns></returns>
    public async Task<MediaStream> GetStreamAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "stream");
        return await MediaStream.CreateAsync(JSRuntime, jSInstance);
    }
}
