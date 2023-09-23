using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// Holds the current time with regards to the related <see cref="BaseAudioContext"/> (<see cref="ContextTime"/>)
/// and the current time with regards to the Performance interface.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioTimestamp">See the API definition here</see>.</remarks>
public class AudioTimestamp
{
    /// <summary>
    /// Represents a point in the time coordinate system of <see cref="BaseAudioContext"/>’s currentTime.
    /// </summary>
    [JsonPropertyName("contextTime")]
    public double ContextTime { get; set; }

    /// <summary>
    /// Represents a point in the time coordinate system of a Performance interface implementation (described in the <see href="https://www.w3.org/TR/hr-time-3/">High Resolution Time</see> standard).
    /// </summary>
    [JsonPropertyName("performanceTime")]
    public double PerformanceTime { get; set; }
}
