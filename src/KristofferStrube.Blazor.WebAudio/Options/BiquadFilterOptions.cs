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
    /// <remarks>
    /// The default value is <see cref="BiquadFilterType.Lowpass"/>.
    /// </remarks>
    [JsonPropertyName("type")]
    public BiquadFilterType Type { get; set; } = BiquadFilterType.Lowpass;

    /// <summary>
    /// The desired initial value for Q.
    /// </summary>
    /// <remarks>
    /// The default value is <c>1</c>.
    /// </remarks>
    [JsonPropertyName("Q")]
    public float Q { get; set; } = 1;

    /// <summary>
    /// The desired initial value for detune.
    /// </summary>
    /// <remarks>
    /// The default value is <c>0</c>.
    /// </remarks>
    [JsonPropertyName("detune")]
    public float Detune { get; set; } = 0;

    /// <summary>
    /// The desired initial value for frequency.
    /// </summary>
    /// <remarks>
    /// The default value is <c>350</c>.
    /// </remarks>
    [JsonPropertyName("frequency")]
    public float Frequency { get; set; } = 350;

    /// <summary>
    /// The desired initial value for gain.
    /// </summary>
    /// <remarks>
    /// The default value is <c>0</c>.
    /// </remarks>
    [JsonPropertyName("gain")]
    public float Gain { get; set; } = 0;
}
