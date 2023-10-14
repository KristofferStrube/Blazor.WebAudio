using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebAudio.Options;
using KristofferStrube.Blazor.WebIDL.Exceptions;
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

    /// <summary>
    /// Creates an <see cref="ChannelMergerNode"/> using the standard constructor.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="IndexSizeErrorException"/> if <see cref="ChannelMergerOptions.NumberOfInputs"/> is less than <c>1</c> or larger than the supported number of channels.
    /// </remarks>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="ChannelMergerNode"/> will be associated with.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="ChannelMergerNode"/>.</param>
    /// <exception cref="IndexSizeErrorException"></exception>
    /// <returns>A new instance of an <see cref="ChannelMergerNode"/>.</returns>
    public static async Task<ChannelMergerNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, ChannelMergerOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructChannelMergerNode", context, options);
        return new ChannelMergerNode(jSRuntime, jSInstance);
    }

    private ChannelMergerNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }
}
