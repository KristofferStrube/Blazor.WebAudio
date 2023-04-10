using KristofferStrube.Blazor.WebAudio.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

[JsonConverter(typeof(ChannelCountModeConverter))]
public enum ChannelCountMode
{
    Max,
    ClampedMax,
    Explicit
}