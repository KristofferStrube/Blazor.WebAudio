namespace KristofferStrube.Blazor.WebAudio;

public class OscillatorOptions
{
    public OscillatorType Type { get; set; } = OscillatorType.Sine;
    public float Frequency { get; set; } = 440;
    public float Detune { get; set; } = 0;

    // Missing PeriodicWave for now.
}
