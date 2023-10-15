using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies options to use in constructing a <see cref="GainNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#GainOptions">See the API definition here</see>.</remarks>
public class GainOptions : AudioNodeOptions
{
    /// <summary>
    /// The initial gain value for <see cref="AudioParam.GetValueAsync"/>
    /// </summary>
    [JsonPropertyName("gain")]
    public float Gain { get; set; } = 1;
}
