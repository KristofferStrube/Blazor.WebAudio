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
public class StereoPannerNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="StereoPannerNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="StereoPannerNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="StereoPannerNode"/>.</returns>
    public static new Task<StereoPannerNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new StereoPannerNode(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="StereoPannerNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="StereoPannerNode"/>.</param>
    protected StereoPannerNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
