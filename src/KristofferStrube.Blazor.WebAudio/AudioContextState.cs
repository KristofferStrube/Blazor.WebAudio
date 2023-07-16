namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The state of a <see cref="BaseAudioContext"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#enumdef-audiocontextstate">See the API definition here</see>.</remarks>
public enum AudioContextState
{
    /// <summary>
    /// This context is currently suspended (context time is not proceeding, audio hardware may be powered down/released).
    /// </summary>
    Suspended,
    /// <summary>
    /// Audio is being processed.
    /// </summary>
    Running,
    /// <summary>
    /// 	This context has been released, and can no longer be used to process audio. All system audio resources have been released.
    /// </summary>
    Closed
}

internal static class AudioContextStateExtensions
{
    public static AudioContextState Parse(string state)
    {
        return state switch
        {
            "suspended" => AudioContextState.Suspended,
            "running" => AudioContextState.Running,
            "closed" => AudioContextState.Closed,
            _ => throw new ArgumentException($"'{state}' was not a valid {nameof(AudioContextState)}")
        };
    }
}
