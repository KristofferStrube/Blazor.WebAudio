using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies options for constructing a <see cref="WaveShaperNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#WaveShaperOptions">See the API definition here</see>.</remarks>
public class WaveShaperOptions : AudioNodeOptions
{
    /// <summary>
    /// The initial value for <see cref="WaveShaperNode.GetCurveAsync"/>
    /// </summary>
    [JsonPropertyName("curve")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float[]? Curve { get; set; }

    /// <summary>
    /// The initial value for <see cref="WaveShaperNode.GetOversampleAsync"/>
    /// </summary>
    [JsonPropertyName("oversample")]
    public OverSampleType Oversample { get; set; } = OverSampleType.None;
}
