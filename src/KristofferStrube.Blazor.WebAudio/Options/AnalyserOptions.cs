using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies the options to be used when constructing an <see cref="AnalyserNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AnalyserOptions">See the API definition here</see>.</remarks>
public class AnalyserOptions : AudioNodeOptions
{
    /// <summary>
    /// The desired initial size of the FFT for frequency-domain analysis.
    /// </summary>
    [JsonPropertyName("fftSize")]
    public ulong FftSize { get; set; } = 2048;

    /// <summary>
    /// The desired initial maximum power in dB for FFT analysis.
    /// </summary>
    [JsonPropertyName("maxDecibels")]
    public double MaxDecibels { get; set; } = -30;

    /// <summary>
    /// The desired initial minimum power in dB for FFT analysis.
    /// </summary>
    [JsonPropertyName("minDecibels")]
    public double MinDecibels { get; set; } = -100;

    /// <summary>
    /// The desired initial smoothing constant for the FFT analysis.
    /// </summary>
    [JsonPropertyName("SmoothingTimeConstant")]
    public double SmoothingTimeConstant { get; set; } = 0.8;
}
