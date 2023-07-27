namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies options for constructing a <see cref="AudioBufferSourceNode"/>.
/// All members are optional; if not specified, the normal default is used in constructing the node.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioBufferSourceOptions">See the API definition here</see>.</remarks>
public class AudioBufferSourceOptions
{
    /// <summary>
    /// The audio asset to be played. This is equivalent to assigning buffer to the buffer attribute of the AudioBufferSourceNode.
    /// </summary>
    public AudioBuffer? Buffer { get; set; }

    /// <summary>
    /// The initial value for the detune AudioParam.
    /// </summary>
    public float Detune { get; set; } = 0f;

    /// <summary>
    /// 
    /// </summary>
    public bool Loop { get; set; } = false;

    public double LoopEnd { get; set; } = 0;

    public double LoopStart { get; set; } = 0;

    public float PlayBackRate { get; set; } = 1;
}
