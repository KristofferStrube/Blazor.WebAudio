
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies the options to use in constructing a <see cref="DynamicsCompressorNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#DynamicsCompressorOptions">See the API definition here</see>.</remarks>
public class DynamicsCompressorOptions : AudioNodeOptions
{
    /// <inheritdoc path="/summary"/>
    /// <remarks>
    /// The default value is <see cref="ChannelCountMode.ClampedMax"/>.
    /// </remarks>
    [JsonPropertyName("channelCountMode")]
    public override ChannelCountMode ChannelCountMode { get; set; } = ChannelCountMode.ClampedMax;

    /// <summary>
    /// The initial value for the <see cref="DynamicsCompressorNode.GetAttackAsync"/> AudioParam.
    /// </summary>
    [JsonPropertyName("attack")]
    public float Attack { get; set; } = 0.003f;

    /// <summary>
    /// The initial value for the <see cref="DynamicsCompressorNode.GetKneeAsync"/> AudioParam.
    /// </summary>
    [JsonPropertyName("knee")]
    public float Knee { get; set; } = 30f;

    /// <summary>
    /// The initial value for the <see cref="DynamicsCompressorNode.GetRatioAsync"/> AudioParam.
    /// </summary>
    [JsonPropertyName("ratio")]
    public float Ratio { get; set; } = 12f;

    /// <summary>
    /// The initial value for the <see cref="DynamicsCompressorNode.GetReleaseAsync"/> AudioParam.
    /// </summary>
    [JsonPropertyName("release")]
    public float Release { get; set; } = 0.25f;

    /// <summary>
    /// The initial value for the <see cref="DynamicsCompressorNode.GetThresholdAsync"/> AudioParam.
    /// </summary>
    [JsonPropertyName("threshold")]
    public float Threshold { get; set; } = -24f;
}
