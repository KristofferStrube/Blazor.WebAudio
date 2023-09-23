using KristofferStrube.Blazor.WebIDL.Exceptions;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The <see cref="AudioContextOptions"/> is used to specify user-specified options for an <see cref="AudioContext"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioContextOptions">See the API definition here</see>.</remarks>
public class AudioContextOptions
{
    /// <summary>
    /// Identify the type of playback, which affects tradeoffs between audio output latency and power consumption.
    /// </summary>
    /// <remarks>
    /// The preferred value of the <see cref="LatencyHint"/> is a value from <see cref="AudioContextLatencyCategory"/>.
    /// However, a <see cref="double"/> can also be specified for the number of seconds of latency for finer control to balance latency and power consumption.
    /// It is at the browser’s discretion to interpret the number appropriately.
    /// The actual latency used is given by <see cref="AudioContext.GetBaseLatencyAsync"/>.
    /// </remarks>
    [JsonPropertyName("latencyHint")]
    public AudioContextLatencyCategoryOrDouble LatencyHint { get; set; } = AudioContextLatencyCategory.Interactive;

    /// <summary>
    /// Sets the <see cref="BaseAudioContext.GetSampleRateAsync"/> to this value for the <see cref="AudioContext"/> that will be created.
    /// The supported values are the same as the sample rates for an <see cref="AudioBuffer"/> (at least <c>8000</c> to <c>96000</c>).
    /// </summary>
    /// <remarks>
    /// A <see cref="NotSupportedErrorException"/> exception will be thrown if the specified sample rate is not supported.<br />
    /// If sampleRate is not specified, the preferred sample rate of the output device for this <see cref="AudioContext"/> is used.
    /// </remarks>
    /// <exception cref="NotSupportedErrorException"></exception>
    [JsonPropertyName("sampleRate")]
    public float SampleRate { get; set; }
}
