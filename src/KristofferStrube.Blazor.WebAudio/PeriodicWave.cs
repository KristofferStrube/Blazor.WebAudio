using KristofferStrube.Blazor.WebIDL;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="IPeriodicWave"/> represents an arbitrary periodic waveform to be used with an <see cref="OscillatorNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#periodicwave">See the API definition here</see>.</remarks>
public interface IPeriodicWave : IJSWrapper
{
}
