using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents a processing node which positions an incoming audio stream in a stereo image using a low-cost panning algorithm.
/// This panning effect is common in positioning audio components in a stereo stream.<br />
/// The input of this node is stereo (2 channels) and cannot be increased.
/// Connections from nodes with fewer or more channels will be up-mixed or down-mixed appropriately.
/// The output of this node is hard-coded to stereo (2 channels) and cannot be configured.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#StereoPannerNode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class StereoPannerNode : AudioNode, IJSCreatable<StereoPannerNode>
{
    /// <inheritdoc/>
    public static new async Task<StereoPannerNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<StereoPannerNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new StereoPannerNode(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected StereoPannerNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }
}
