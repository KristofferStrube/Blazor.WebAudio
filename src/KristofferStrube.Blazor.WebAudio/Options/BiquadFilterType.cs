using KristofferStrube.Blazor.WebAudio.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The common filter types used to configure a <see cref="BiquadFilterNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#enumdef-biquadfiltertype">See the API definition here</see>.</remarks>
[JsonConverter(typeof(BiquadFilterTypeConverter))]
public enum BiquadFilterType
{
    /// <summary>
    /// A lowpass filter allows frequencies below the cutoff frequency to pass through and attenuates frequencies above the cutoff.
    /// It implements a standard second-order resonant lowpass filter with 12dB/octave rolloff.
    /// </summary>
    /// <remarks>
    /// Frequency: The cutoff frequency.<br />
    /// Q: Controls how peaked the response will be at the cutoff frequency. A large value makes the response more peaked.<br />
    /// Gain: Not used in this filter type.<br />
    /// </remarks>
    Lowpass,
    /// <summary>
    /// A highpass filter is the opposite of a lowpass filter. Frequencies above the cutoff frequency are passed through, but frequencies below the cutoff are attenuated.
    /// It implements a standard second-order resonant highpass filter with 12dB/octave rolloff.
    /// </summary>
    /// <remarks>
    /// Frequency: The cutoff frequency below which the frequencies are attenuated.<br />
    /// Q: Controls how peaked the response will be at the cutoff frequency. A large value makes the response more peaked.<br />
    /// Gain: Not used in this filter type.<br />
    /// </remarks>
    Highpass,
    /// <summary>
    /// A bandpass filter allows a range of frequencies to pass through and attenuates the frequencies below and above this frequency range.
    /// It implements a second-order bandpass filter.
    /// </summary>
    /// <remarks>
    /// Frequency: The center of the frequency band.<br />
    /// Q: Controls the width of the band. The width becomes narrower as the Q value increases.<br />
    /// Gain: Not used in this filter type.<br />
    /// </remarks>
    Bandpass,
    /// <summary>
    /// The lowshelf filter allows all frequencies through, but adds a boost (or attenuation) to the lower frequencies.
    /// It implements a second-order lowshelf filter.
    /// </summary>
    /// <remarks>
    /// Frequency: The upper limit of the frequences where the boost (or attenuation) is applied.<br />
    /// Q: Not used in this filter type.<br />
    /// Gain: The boost, in dB, to be applied. If the value is negative, the frequencies are attenuated.<br />
    /// </remarks>
    Lowshelf,
    /// <summary>
    /// The highshelf filter is the opposite of the lowshelf filter and allows all frequencies through, but adds a boost to the higher frequencies.
    /// It implements a second-order highshelf filter
    /// </summary>
    /// <remarks>
    /// Frequency: The lower limit of the frequences where the boost (or attenuation) is applied.<br />
    /// Q: Not used in this filter type.<br />
    /// Gain: The boost, in dB, to be applied. If the value is negative, the frequencies are attenuated.<br />
    /// </remarks>
    Highshelf,
    /// <summary>
    /// The peaking filter allows all frequencies through, but adds a boost (or attenuation) to a range of frequencies.
    /// </summary>
    /// <remarks>
    /// Frequency: The center frequency of where the boost is applied.<br />
    /// Q: Controls the width of the band of frequencies that are boosted. A large value implies a narrow width.<br />
    /// Gain: The boost, in dB, to be applied. If the value is negative, the frequencies are attenuated.<br />
    /// </remarks>
    Peaking,
    /// <summary>
    /// The notch filter (also known as a band-stop or band-rejection filter) is the opposite of a bandpass filter.
    /// It allows all frequencies through, except for a set of frequencies.
    /// </summary>
    /// <remarks>
    /// Frequency: The center frequency of where the notch is applied.<br />
    /// Q: Controls the width of the band of frequencies that are attenuated. A large value implies a narrow width.<br />
    /// Gain: Not used in this filter type.<br />
    /// </remarks>
    Notch,
    /// <summary>
    /// An allpass filter allows all frequencies through, but changes the phase relationship between the various frequencies.
    /// It implements a second-order allpass filter
    /// </summary>
    /// <remarks>
    /// Frequency: The frequency where the center of the phase transition occurs. Viewed another way, this is the frequency with maximal group delay.<br />
    /// Q: Controls how sharp the phase transition is at the center frequency. A larger value implies a sharper transition and a larger group delay.<br />
    /// Gain: Not used in this filter type.<br />
    /// </remarks>
    Allpass,
}
