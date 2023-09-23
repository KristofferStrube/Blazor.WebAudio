namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// An enum for specifying the strategy that the <see cref="AudioContext"/> should use for balancing audio output latency and power consumption.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#enumdef-audiocontextlatencycategory">See the API definition here</see>.</remarks>
public enum AudioContextLatencyCategory
{
    /// <summary>
    /// Provide the lowest audio output latency possible without glitching. This is the default.
    /// </summary>
    Interactive,
    /// <summary>
    /// Balance audio output latency and power consumption.
    /// </summary>
    Balanced,
    /// <summary>
    /// Prioritize sustained playback without interruption over audio output latency. Lowest power consumption.
    /// </summary>
    Playback,
}

internal static class AudioContextLatencyCategoryExtensions
{
    public static string AsString(this AudioContextLatencyCategory type)
    {
        return type switch
        {
            AudioContextLatencyCategory.Balanced => "balanced",
            AudioContextLatencyCategory.Interactive => "interactive",
            AudioContextLatencyCategory.Playback => "playback",
            _ => throw new ArgumentException($"Value '{type}' was not a valid {nameof(AudioContextLatencyCategory)}.")
        };
    }
}
