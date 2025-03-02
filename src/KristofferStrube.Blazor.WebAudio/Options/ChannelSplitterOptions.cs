using KristofferStrube.Blazor.WebIDL.Exceptions;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies the options to use in constructing a <see cref="ChannelSplitterNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#ChannelSplitterOptions">See the API definition here</see>.</remarks>
public class ChannelSplitterOptions : AudioNodeOptions
{
    /// <summary>
    /// <inheritdoc path="/summary"/>
    /// </summary>
    /// <remarks>
    /// The default value is <see cref="ChannelCountMode.Explicit"/>.
    /// </remarks>
    [JsonPropertyName("channelCountMode")]
    public override ChannelCountMode ChannelCountMode { get; set; } = ChannelCountMode.Explicit;

    /// <inheritdoc path="/summary"/>
    /// <remarks>
    /// The default value is <see cref="ChannelInterpretation.Discrete"/>.
    /// </remarks>
    [JsonPropertyName("channelInterpretation")]
    public override ChannelInterpretation ChannelInterpretation { get; set; } = ChannelInterpretation.Discrete;

    /// <summary>
    /// The number inputs for the <see cref="ChannelSplitterNode"/>.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="IndexSizeErrorException"/> if it is less than <c>1</c> or larger than the supported number of channels when used for constructing a <see cref="ChannelSplitterNode"/>.
    /// </remarks>
    [JsonPropertyName("numberOfInputs")]
    public ulong NumberOfInputs { get; set; } = 6;
}
