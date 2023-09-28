using KristofferStrube.Blazor.WebAudio.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The automation rate of an <see cref="AudioParam"/> can be selected calling <see cref="AudioParam.SetAutomationRateAsync"/> with one of the following values.
/// However, some <see cref="AudioParam"/>s have constraints on whether the automation rate can be changed.
/// </summary>
[JsonConverter(typeof(AutomationRateConverter))]
public enum AutomationRate
{
    /// <summary>
    /// This <see cref="AudioParam"/> is set for a-rate processing.
    /// </summary>
    /// <remarks>
    /// a-rate parameters will be sampled for each sample-frame of the block.
    /// </remarks>
    ARate,
    /// <summary>
    /// This <see cref="AudioParam"/> is set for k-rate processing.
    /// </summary>
    /// <remarks>
    ///  k-rate parameter will be sampled at the time of the very first sample-frame, and that value will be used for the entire block.
    /// </remarks>
    KRate
}
