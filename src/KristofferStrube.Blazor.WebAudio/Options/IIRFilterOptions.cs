using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

public class IIRFilterOptions : AudioNodeOptions
{
    [JsonPropertyName("feedforward")]
    public required double[] Feedforward { get; set; }

    [JsonPropertyName("feedback")]
    public required double[] Feedback { get; set; }
}
