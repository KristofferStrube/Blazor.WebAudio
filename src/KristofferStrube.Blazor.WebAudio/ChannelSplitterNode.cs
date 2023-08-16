using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The <see cref="ChannelSplitterNode"/> is for use in more advanced applications and would often be used in conjunction with <see cref="ChannelMergerNode"/>.<br />
/// This interface represents an <see cref="AudioNode"/> for accessing the individual channels of an audio stream in the routing graph.
/// It has a single input, and a number of <c>"active"</c> outputs which equals the number of channels in the input audio stream.
/// For example, if a stereo input is connected to an <see cref="ChannelSplitterNode"/> then the number of active outputs will be two (one from the left channel and one from the right).
/// There are always a total number of N outputs (determined by the numberOfOutputs parameter to the <see cref="AudioContext"/> method <see cref="BaseAudioContext.CreateChannelSplitterAsync(ulong)"/>),
/// The default number is 6 if this value is not provided.
/// Any outputs which are not <c>"active"</c> will output silence and would typically not be connected to anything.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#ChannelSplitterNode">See the API definition here</see>.</remarks>
public class ChannelSplitterNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of an <see cref="ChannelSplitterNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ChannelSplitterNode"/>.</param>
    /// <returns>A wrapper instance for an <see cref="ChannelSplitterNode"/>.</returns>
    public static new Task<ChannelSplitterNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new ChannelSplitterNode(jSRuntime, jSReference));
    }

    private ChannelSplitterNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }
}
