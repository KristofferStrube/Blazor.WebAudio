using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

public class AudioNodeOptions
{
    [JsonPropertyName("channelCount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ulong? ChannelCount { get; set; }

    [JsonPropertyName("channelCountMode")]
    public ChannelCountMode ChannelCountMode { get; set; } = ChannelCountMode.Max;

    [JsonPropertyName("channelInterpretation")]
    public ChannelInterpretation ChannelInterpretation { get; set; } = ChannelInterpretation.Speakers;
}
