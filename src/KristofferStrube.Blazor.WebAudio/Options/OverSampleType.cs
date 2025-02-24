using KristofferStrube.Blazor.WebAudio.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The <see cref="OverSampleType"/> enum determines which the type of oversampling to use when shaping a curve.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#enumdef-oversampletype">See the API definition here</see>.</remarks>
[JsonConverter(typeof(OverSampleTypeConverter))]
public enum OverSampleType
{
    /// <summary>
    /// Don’t oversample
    /// </summary>
    None,

    /// <summary>
    /// Oversample two times
    /// </summary>
    TwoX,

    /// <summary>
    /// Oversample four times
    /// </summary>
    FourX,
}