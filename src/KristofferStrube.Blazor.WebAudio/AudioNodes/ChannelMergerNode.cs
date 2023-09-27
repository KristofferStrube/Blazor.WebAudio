using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The <see cref="ChannelMergerNode"/> is for use in more advanced applications and would often be used in conjunction with <see cref="ChannelSplitterNode"/>.<br />
/// This interface represents an <see cref="AudioNode"/> for combining channels from multiple audio streams into a single audio stream.
/// It has a variable number of inputs (defaulting to 6), but not all of them need be connected.
/// There is a single output whose audio stream has a number of channels equal to the number of inputs when any of the inputs is actively processing.
/// If none of the inputs are actively processing, then output is a single channel of silence.<br />
/// To merge multiple inputs into one stream, each input gets downmixed into one channel (mono) based on the specified mixing rule.
/// An unconnected input still counts as one silent channel in the output.
/// Changing input streams does not affect the order of output channels.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#ChannelMergerNode">See the API definition here</see>.</remarks>
public class ChannelMergerNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of an <see cref="ChannelMergerNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ChannelMergerNode"/>.</param>
    /// <returns>A wrapper instance for an <see cref="ChannelMergerNode"/>.</returns>
    public static new Task<ChannelMergerNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new ChannelMergerNode(jSRuntime, jSReference));
    }

    private ChannelMergerNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }
}
