using KristofferStrube.Blazor.MediaCaptureStreams;
using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface is an audio destination representing a <see cref="MediaStream"/> with a single <see cref="MediaStreamTrack"/> whose kind is <see cref="MediaStreamTrackKind.Audio"/>. This <see cref="MediaStream"/> is created when the node is created and is accessible via <see cref="GetStreamAsync"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#MediaStreamAudioDestinationNode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class MediaStreamAudioDestinationNode : AudioNode, IJSCreatable<MediaStreamAudioDestinationNode>
{
    /// <inheritdoc/>
    public static new async Task<MediaStreamAudioDestinationNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<MediaStreamAudioDestinationNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new MediaStreamAudioDestinationNode(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Creates a <see cref="MediaStreamAudioDestinationNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="AudioContext"/> this new <see cref="MediaStreamAudioDestinationNode"/> will be associated with.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="MediaStreamAudioDestinationNode"/>.</param>
    /// <returns>A new instance of a <see cref="MediaStreamAudioDestinationNode"/>.</returns>
    public static async Task<MediaStreamAudioDestinationNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, AudioNodeOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructMediaStreamAudioDestinationNode", context, options);
        return new MediaStreamAudioDestinationNode(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected MediaStreamAudioDestinationNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

    /// <summary>
    /// A <see cref="MediaStream"/> containing a single <see cref="MediaStreamTrack"/> with the same number of channels as the node itself, and whose kind is <see cref="MediaStreamTrackKind.Audio"/>.
    /// </summary>
    public async Task<MediaStream> GetStreamAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "stream");
        return await MediaStream.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }
}
