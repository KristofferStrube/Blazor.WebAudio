using KristofferStrube.Blazor.WebIDL.Exceptions;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies the options to be used when constructing an <see cref="OscillatorNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#OscillatorOptions">See the API definition here</see>.</remarks>
public class OscillatorOptions : AudioNodeOptions
{
    /// <summary>
    /// The type of oscillator to be constructed.
    /// If periodicWave is specified, then any valid value for type is ignored; it is treated as if it were set to "custom".
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="InvalidStateErrorException"/> if this is set to <see cref="OscillatorType.Custom"/> without a <see cref="PeriodicWave"/> being provided.
    /// </remarks>
    public OscillatorType Type { get; set; } = OscillatorType.Sine;

    /// <summary>
    /// The initial frequency for the <see cref="OscillatorNode"/>.
    /// </summary>
    public float Frequency { get; set; } = 440;

    /// <summary>
    /// The initial detune value for the <see cref="OscillatorNode"/>.
    /// </summary>
    public float Detune { get; set; } = 0;

    /// <summary>
    /// The PeriodicWave for the <see cref="OscillatorNode"/>.
    /// If this is specified, then any valid value for <see cref="Type"/> is ignored; it is treated as if <see cref="OscillatorType.Custom"/> were specified.
    /// </summary>
    public PeriodicWave? PeriodicWave { get; set; }
}
