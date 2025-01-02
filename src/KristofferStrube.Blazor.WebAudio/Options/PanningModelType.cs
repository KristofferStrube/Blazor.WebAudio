using KristofferStrube.Blazor.WebAudio.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The <see cref="PanningModelType"/> enum determines which spatialization algorithm will be used to position the audio in 3D space. The default is <see cref="EqualPower"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#enumdef-panningmodeltype">See the API definition here</see>.</remarks>
[JsonConverter(typeof(PanningModelTypeConverter))]
public enum PanningModelType
{
    /// <summary>
    /// A simple and efficient spatialization algorithm using equal-power panning.
    /// </summary>
    /// <remarks>
    /// When this panning model is used, all the <see cref="AudioParam"/>s used to compute the output of this node are <see cref="AutomationRate.ARate"/>.
    /// </remarks>
    EqualPower,

    /// <summary>
    /// A higher quality spatialization algorithm using a convolution with measured impulse responses from human subjects. This panning method renders stereo output.
    /// </summary>
    /// <remarks>
    /// When this panning model is used, all the <see cref="AudioParam"/>s used to compute the output of this node are <see cref="AutomationRate.KRate"/>.
    /// </remarks>
    HRTF,
}