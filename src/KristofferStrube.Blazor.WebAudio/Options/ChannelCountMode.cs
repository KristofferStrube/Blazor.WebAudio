using KristofferStrube.Blazor.WebAudio.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// An enum for specifying how channels will be counted when up-mixing and down-mixing connections to any inputs to the node.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#enumdef-channelcountmode">See the API definition here</see>.</remarks>
[JsonConverter(typeof(ChannelCountModeConverter))]
public enum ChannelCountMode
{
    /// <summary>
    /// The computed number of channels is the maximum of the number of channels of all connections to an input. In this mode <see cref="AudioNode.GetChannelCountAsync"/> is ignored.
    /// </summary>
    Max,

    /// <summary>
    /// The computed number of channels is determined as for <see cref="Max"/> and then clamped to a maximum value of <see cref="AudioNode.GetChannelCountAsync"/>.
    /// </summary>
    ClampedMax,

    /// <summary>
    /// The computed number of channels is the exact value as specified by <see cref="AudioNode.GetChannelCountAsync"/>.
    /// </summary>
    Explicit
}