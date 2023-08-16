using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents a processing node which applies a linear convolution effect given an impulse response.<br />
/// The input of this node is either mono (1 channel) or stereo (2 channels) and cannot be increased.
/// Connections from nodes with more channels will be down-mixed appropriately.<br />
/// There are channelCount constraints and channelCountMode constraints for this node.
/// These constraints ensure that the input to the node is either mono or stereo.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#ConvolverNode">See the API definition here</see>.</remarks>
public class ConvolverNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of an <see cref="ConvolverNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ConvolverNode"/>.</param>
    /// <returns>A wrapper instance for an <see cref="ConvolverNode"/>.</returns>
    public static new Task<ConvolverNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new ConvolverNode(jSRuntime, jSReference));
    }

    private ConvolverNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }
}
