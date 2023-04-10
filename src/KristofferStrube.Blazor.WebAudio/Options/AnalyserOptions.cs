using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

public class AnalyserOptions : AudioNodeOptions
{
    [JsonPropertyName("fftSize")]
    public ulong FftSize { get; set; } = 2048;

    [JsonPropertyName("maxDecibels")]
    public double MaxDecibels { get; set; } = -30;

    [JsonPropertyName("minDecibels")]
    public double MinDecibels { get; set; } = -100;

    [JsonPropertyName("SmoothingTimeConstant")]
    public double SmoothingTimeConstant { get; set; } = 0.8;
}
