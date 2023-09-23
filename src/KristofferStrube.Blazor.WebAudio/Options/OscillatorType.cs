namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// Changing the gain of an audio signal is a fundamental operation in audio applications. This interface is an <see cref="AudioNode"/> with a single input and single output.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#enumdef-oscillatortype">See the API definition here</see>.</remarks>
public enum OscillatorType
{
    /// <summary>
    /// A sine wave
    /// </summary>
    Sine,
    /// <summary>
    /// A square wave of duty period 0.5
    /// </summary>
    Square,
    /// <summary>
    ///	A sawtooth wave
    /// </summary>
    Sawtooth,
    /// <summary>
    /// A triangle wave
    /// </summary>
    Triangle,
    /// <summary>
    /// A custom periodic wave
    /// </summary>
    Custom
}

internal static class OscillatorTypeExtensions
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
            _ => throw new ArgumentException($"Value '{type}' was not a valid {nameof(OscillatorType)}.")
        };
    }
}
