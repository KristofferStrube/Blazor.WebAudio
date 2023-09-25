using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Options;

/// <summary>
/// This specifies the options to use in constructing an <see cref="OfflineAudioContext"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#OfflineAudioContextOptions">See the API definition here</see>.</remarks>
public class OfflineAudioContextOptions
{
    /// <summary>
    /// The number of channels for this <see cref="OfflineAudioContext"/>.
    /// </summary>
    [JsonPropertyName("numberOfChannelse")]
    public ulong NumberOfChannelse { get; set; } = 1;

    /// <summary>
    /// The length of the rendered <see cref="AudioBuffer"/> in sample-frames.
    /// </summary>
    [JsonPropertyName("length")]
    public required ulong Length { get; set; }

    /// <summary>
    /// The sample rate for this <see cref="OfflineAudioContext"/>.
    /// </summary>
    [JsonPropertyName("sampleRate")]
    public required float SampleRate { get; set; }
}
