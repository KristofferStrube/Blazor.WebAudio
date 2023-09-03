using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="PeriodicWaveConstraints"/> is used to specify how the waveform is normalized.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#PeriodicWaveConstraints">See the API definition here</see>.</remarks>
public class PeriodicWaveConstraints
{
    /// <summary>
    /// Controls whether the periodic wave is normalized or not. If <see langword="true"/>, the waveform is not normalized; otherwise, the waveform is normalized.
    /// </summary>
    [JsonPropertyName("disableNormalization")]
    public bool DisableNormalization { get; set; } = false;
}
