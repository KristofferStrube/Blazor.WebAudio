using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies options for constructing a <see cref="DelayNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#DelayOptions">See the API definition here</see>.</remarks>
public class DelayOptions : AudioNodeOptions
{
    /// <summary>
    /// The maximum delay time for the node. Time is in seconds and must be greater than <c>0</c> and less than <c>3</c> minutes (<c>180</c> seconds).
    /// </summary>
    /// <remarks>
    /// The default value is <c>1</c>.
    /// </remarks>
    [JsonPropertyName("maxDelayTime")]
    public double MaxDelayTime { get; set; } = 1;

    /// <summary>
    /// The initial delay time for the node.
    /// </summary>
    /// <remarks>
    /// The default value is <c>0</c>.
    /// </remarks>
    [JsonPropertyName("delayTime")]
    public double DelayTime { get; set; } = 0;
}
