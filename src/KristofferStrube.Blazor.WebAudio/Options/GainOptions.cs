using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

public class GainOptions
{
    [JsonPropertyName("gain")]
    public float Gain { get; set; } = 1;
}
