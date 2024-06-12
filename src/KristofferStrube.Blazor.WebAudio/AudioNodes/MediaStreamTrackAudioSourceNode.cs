using KristofferStrube.Blazor.MediaCaptureStreams;
using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents an audio source from a <see cref="MediaStreamTrack"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#MediaStreamTrackAudioSourceNode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class MediaStreamTrackAudioSourceNode : AudioNode, IJSCreatable<MediaStreamTrackAudioSourceNode>
{
    /// <inheritdoc/>
    public static new async Task<MediaStreamTrackAudioSourceNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<MediaStreamTrackAudioSourceNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new MediaStreamTrackAudioSourceNode(jSRuntime, jSReference, options));
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
        return new MediaStreamTrackAudioSourceNode(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected MediaStreamTrackAudioSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }
}
