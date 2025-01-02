using KristofferStrube.Blazor.WebAudio.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The <see cref="DistanceModelType"/> enum determines which algorithm will be used to reduce the volume of an audio source as it moves away from the listener. The default is "inverse".
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#enumdef-distancemodeltype">See the API definition here</see>.</remarks>
[JsonConverter(typeof(DistanceModelTypeConverter))]
public enum DistanceModelType
{
    /// <summary>
    /// The volume decreases proportionally to the distance between the listener and the sound source, creating a uniform and predictable attenuation effect.
    /// </summary>
    Linear,

    /// <summary>
    /// The volume decreases sharply as the distance increases initially but tapers off at greater distances, closely resembling real-world acoustic behavior.
    /// </summary>
    Inverse,

    /// <summary>
    /// The volume decreases exponentially as the distance increases, resulting in a rapid attenuation of sound over shorter distances.
    /// </summary>
    Exponential,
}