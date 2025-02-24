using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebAudio.Options;
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

    /// <summary>
    /// Creates an <see cref="MediaElementAudioSourceNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="AudioContext"/> this new <see cref="MediaElementAudioSourceNode"/> will be associated with.</param>
    /// <param name="options">Initial parameter value for this <see cref="MediaElementAudioSourceNode"/>.</param>
    /// <returns>A new instance of a <see cref="MediaElementAudioSourceNode"/>.</returns>
    public static async Task<MediaElementAudioSourceNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, MediaElementAudioSourceOptions options)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructMediaElementAudioSourceNode", context, options);
        return new MediaElementAudioSourceNode(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected MediaElementAudioSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }
}
