using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents a processing node which positions / spatializes an incoming audio stream in three-dimensional space.
/// The spatialization is in relation to the <see cref="BaseAudioContext"/>'s <see cref="AudioListener"/> (listener attribute).
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#PannerNode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class PannerNode : AudioNode, IJSCreatable<PannerNode>
{
    /// <inheritdoc/>
    public static new async Task<PannerNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<PannerNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new PannerNode(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected PannerNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }
}
