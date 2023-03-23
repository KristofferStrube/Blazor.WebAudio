using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Options;

public class GainOptions
{
    [JsonPropertyName("gain")]
    public float Gain { get; set; } = 1;
}
