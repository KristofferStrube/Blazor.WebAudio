using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies the options to use in constructing an <see cref="AudioBuffer"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioBufferOptions">See the API definition here</see>.</remarks>
public class AudioBufferOptions
{
    /// <summary>
    /// The number of channels for the buffer. Browsers will support at least 32 channels.
    /// </summary>
    [JsonPropertyName("numberOfChannels")]
    public ulong NumberOfChannels { get; set; } = 1;

    /// <summary>
    /// The length in sample frames of the buffer. Must be at least <c>1</c>.
    /// </summary>
    [JsonPropertyName("length")]
    public required ulong Length { get; set; }

    /// <summary>
    /// The sample rate in Hz for the buffer.
    /// The range is at least from <c>8000</c> to <c>96000</c> but browsers can support broader ranges.
    /// </summary>
    [JsonPropertyName("sampleRate")]
    public required float SampleRate { get; set; }
}