using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies the options that can be used in constructing all <see cref="AudioNode"/>s.
/// All members are optional. However, the specific values used for each node depends on the actual node.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioNodeOptions">See the API definition here</see>.</remarks>
public class AudioNodeOptions
{
    /// <summary>
    /// <see cref="ChannelCount"/> is the number of channels used when up-mixing and down-mixing connections to any inputs to the node.
    /// The default value is <c>2</c> except for specific nodes where its value is specially determined.
    /// This attribute has no effect for nodes with no inputs.
    /// </summary>
    [JsonPropertyName("channelCount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ulong? ChannelCount { get; set; }

    /// <summary>
    /// <see cref="ChannelCountMode"/> determines how channels will be counted when up-mixing and down-mixing connections to any inputs to the node.
    /// </summary>
    /// <remarks>
    /// The default value is <see cref="ChannelCountMode.Max"/>. This attribute has no effect for nodes with no inputs.
    /// </remarks>
    [JsonPropertyName("channelCountMode")]
    public virtual ChannelCountMode ChannelCountMode { get; set; } = ChannelCountMode.Max;

    /// <summary>
    /// <see cref="ChannelInterpretation"/> determines how individual channels will be treated when up-mixing and down-mixing connections to any inputs to the node.
    /// </summary>
    /// <remarks>
    /// The default value is <see cref="ChannelInterpretation.Speakers"/>. This attribute has no effect for nodes with no inputs.
    /// </remarks>
    [JsonPropertyName("channelInterpretation")]
    public virtual ChannelInterpretation ChannelInterpretation { get; set; } = ChannelInterpretation.Speakers;
}
