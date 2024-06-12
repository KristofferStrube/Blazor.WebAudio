using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents an audio source from an audio or video element.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#MediaElementAudioSourceNode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class MediaElementAudioSourceNode : AudioNode, IJSCreatable<MediaElementAudioSourceNode>
{
    /// <inheritdoc/>
    public static new Task<MediaElementAudioSourceNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<MediaElementAudioSourceNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new MediaElementAudioSourceNode(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected MediaElementAudioSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }
}
