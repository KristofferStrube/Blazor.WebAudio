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
    Max,
    ClampedMax,
    Explicit
}