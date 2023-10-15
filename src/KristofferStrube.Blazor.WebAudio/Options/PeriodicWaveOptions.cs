using KristofferStrube.Blazor.WebIDL.Exceptions;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="PeriodicWaveOptions"/> is used to specify how the waveform is constructed.<br />
/// If only one of real or imag is specified.
/// The other is treated as if it were an array of all zeroes of the same length.
/// If neither is given, a <see cref="PeriodicWave"/> is created that will be equivalent to an <see cref="OscillatorNode"/> with <see cref="OscillatorType.Sine"/>.
/// If both are given, the sequences must have the same length; otherwise an error of type <see cref="NotSupportedErrorException"/> will be thrown.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#PeriodicWaveOptions">See the API definition here</see>.</remarks>
public class PeriodicWaveOptions : PeriodicWaveConstraints
{
    /// <summary>
    /// The imag parameter represents an array of sine terms.
    /// The first element (index <c>0</c>) does not exist in the Fourier series.
    /// The second element (index <c>1</c>) represents the fundamental frequency.
    /// The third represents the first overtone and so on.
    /// </summary>
    [JsonPropertyName("imag")]
    public required float[] Imag { get; set; }

    /// <summary>
    /// The real parameter represents an array of cosine terms.
    /// The first element (index <c>0</c>) is the DC-offset of the periodic waveform.
    /// The second element (index <c>1</c>) represents the fundmental frequency.
    /// The third represents the first overtone and so on.
    /// </summary>
    [JsonPropertyName("real")]
    public required float[] Real { get; set; }
}
