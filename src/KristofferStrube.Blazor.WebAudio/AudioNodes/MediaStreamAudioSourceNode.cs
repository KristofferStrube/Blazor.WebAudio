using KristofferStrube.Blazor.MediaCaptureStreams;
using KristofferStrube.Blazor.WebAudio.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents an audio source from a <see cref="MediaStream"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#MediaStreamAudioSourceNode">See the API definition here</see>.</remarks>
public class MediaStreamAudioSourceNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaStreamAudioSourceNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaStreamAudioSourceNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="MediaStreamAudioSourceNode"/>.</returns>
    public static new Task<MediaStreamAudioSourceNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new MediaStreamAudioSourceNode(jSRuntime, jSReference));
    }

    /// <summary>
    /// Creates a <see cref="MediaStreamAudioSourceNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="MediaStreamAudioSourceNode"/> will be associated with.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="MediaStreamAudioSourceNode"/>.</param>
    /// <returns>A new instance of a <see cref="MediaStreamAudioSourceNode"/>.</returns>
    public static async Task<MediaStreamAudioSourceNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, MediaStreamAudioSourceOptions options)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructMediaStreamAudioSourceNode", context, options);
        return new MediaStreamAudioSourceNode(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaStreamAudioSourceNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaStreamAudioSourceNode"/>.</param>
    protected MediaStreamAudioSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Gets the <see cref="MediaStream"/> used when constructing this <see cref="MediaStreamAudioSourceNode"/>
    /// </summary>
    /// <returns></returns>
    public async Task<MediaStream> GetMediaStreamAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "mediaStream");
        return await MediaStream.CreateAsync(JSRuntime, jSInstance);
    }
}
