namespace KristofferStrube.Blazor.WebAudio;

public enum OscillatorType
{
    Sine,
    Square,
    Sawtooth,
    Triangle,
    Custom
}

public static class OscillatorTypeExtensions
{
    public static string AsString(this OscillatorType type)
    {
        return type switch
        {
            OscillatorType.Sine => "sine",
            OscillatorType.Square => "square",
            OscillatorType.Sawtooth => "sawtooth",
            OscillatorType.Triangle => "triangle",
            OscillatorType.Custom => "custom",
            _ => throw new ArgumentException($"Value '{type}' was not a valid OscillatorType.")
        };
    }
}
