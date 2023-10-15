using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies the options to be used when constructing a <see cref="BiquadFilterNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#BiquadFilterOptions">See the API definition here</see>.</remarks>
public class BiquadFilterOptions : AudioNodeOptions
{
    /// <summary>
    /// The desired initial type of the filter.
    /// </summary>
    [JsonPropertyName("type")]
    public BiquadFilterType Type { get; set; } = BiquadFilterType.Lowpass;

    /// <summary>
    /// The desired initial value for Q.
    /// </summary>
    [JsonPropertyName("Q")]
    public float Q { get; set; } = 1;

    /// <summary>
    /// The desired initial value for detune.
    /// </summary>
    [JsonPropertyName("detune")]
    public float Detune { get; set; } = 0;

    /// <summary>
    /// The desired initial value for frequency.
    /// </summary>
    [JsonPropertyName("frequency")]
    public float Frequency { get; set; } = 350;

    /// <summary>
    /// The desired initial value for gain.
    /// </summary>
    [JsonPropertyName("gain")]
    public float Gain { get; set; } = 0;
}
