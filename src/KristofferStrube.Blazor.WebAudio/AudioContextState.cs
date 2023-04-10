namespace KristofferStrube.Blazor.WebAudio;

public enum AudioContextState
{
    Suspended,
    Running,
    Closed
}

public static class AudioContextStateExtensions
{
    public static AudioContextState Parse(string state) => state switch
    {
        "suspended" => AudioContextState.Suspended,
        "running" => AudioContextState.Running,
        "closed" => AudioContextState.Closed,
        _ => throw new ArgumentException($"'{state}' was not a valid {nameof(AudioContextState)}")
    };
}
