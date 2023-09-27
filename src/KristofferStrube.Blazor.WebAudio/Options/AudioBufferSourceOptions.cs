using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies options for constructing an <see cref="AudioBufferSourceNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioBufferSourceOptions">See the API definition here</see>.</remarks>
public class AudioBufferSourceOptions
{
    /// <summary>
    /// The audio asset to be played. This is equivalent to calling <see cref="AudioBufferSourceNode.SetBufferAsync(AudioBuffer?)"/>.
    /// </summary>
    [JsonPropertyName("buffer")]
    public AudioBuffer? Buffer { get; set; }

    /// <summary>
    /// The initial value for <see cref="AudioBufferSourceNode.GetDetuneAsync"/>.
    /// </summary>
    [JsonPropertyName("detune")]
    public float Detune { get; set; } = 0f;

    /// <summary>
    /// The initial value for <see cref="AudioBufferSourceNode.GetLoopAsync"/>
    /// </summary>
    [JsonPropertyName("loop")]
    public bool Loop { get; set; } = false;

    /// <summary>
    /// The initial value for <see cref="AudioBufferSourceNode.GetLoopEndAsync"/>
    /// </summary>
    [JsonPropertyName("loopEnd")]
    public double LoopEnd { get; set; } = 0;

    /// <summary>
    /// The initial value for <see cref="AudioBufferSourceNode.GetLoopStartAsync"/>
    /// </summary>
    [JsonPropertyName("loopStart")]
    public double LoopStart { get; set; } = 0;

    /// <summary>
    /// The initial value for <see cref="AudioBufferSourceNode.GetPlaybackRateAsync"/>
    /// </summary>
    [JsonPropertyName("playbackRate")]
    public float PlaybackRate { get; set; } = 1;
}
