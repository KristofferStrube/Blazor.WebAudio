using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;


/// <summary>
/// This specifies options for constructing a <see cref="PannerNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#PannerOptions">See the API definition here</see>.</remarks>
public class PannerOptions : AudioNodeOptions
{
    /// <summary>
    /// <see cref="ChannelCountMode"/> determines how channels will be counted when up-mixing and down-mixing connections to any inputs to the node.
    /// </summary>
    /// <remarks>
    /// The default value is <see cref="ChannelCountMode.ClampedMax"/>.
    /// </remarks>
    [JsonPropertyName("channelCountMode")]
    public override ChannelCountMode ChannelCountMode { get; set; } = ChannelCountMode.ClampedMax;

    /// <summary>
    /// The panning model to use for the node.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="PanningModelType.EqualPower"/>.
    /// </remarks>
    [JsonPropertyName("panningModel")]
    public PanningModelType PanningModel { get; set; } = PanningModelType.EqualPower;

    /// <summary>
    /// The distance model to use for the node.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="DistanceModelType.Inverse"/>.
    /// </remarks>
    [JsonPropertyName("distanceModel")]
    public DistanceModelType DistanceModel { get; set; } = DistanceModelType.Inverse;

    /// <summary>
    /// The initial x-coordinate value for the <see cref="PannerNode.GetPositionXAsync"/> AudioParam.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>0</c>.
    /// </remarks>
    [JsonPropertyName("positionX")]
    public float PositionX { get; set; } = 0;

    /// <summary>
    /// The initial y-coordinate value for the <see cref="PannerNode.GetPositionYAsync"/> AudioParam.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>0</c>.
    /// </remarks>
    [JsonPropertyName("positionY")]
    public float PositionY { get; set; } = 0;

    /// <summary>
    /// The initial z-coordinate value for the <see cref="PannerNode.GetPositionZAsync"/> AudioParam.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>0</c>.
    /// </remarks>
    [JsonPropertyName("positionZ")]
    public float PositionZ { get; set; } = 0;

    /// <summary>
    /// The initial x-component value for the <see cref="PannerNode.GetOrientationXAsync"/> AudioParam.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>1</c>.
    /// </remarks>
    [JsonPropertyName("orientationX")]
    public float OrientationX { get; set; } = 1;

    /// <summary>
    /// The initial y-component value for the <see cref="PannerNode.GetOrientationYAsync"/> AudioParam.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>0</c>.
    /// </remarks>
    [JsonPropertyName("orientationY")]
    public float OrientationY { get; set; } = 0;

    /// <summary>
    /// The initial z-component value for the <see cref="PannerNode.GetOrientationZAsync"/> AudioParam.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>0</c>.
    /// </remarks>
    [JsonPropertyName("orientationZ")]
    public float OrientationZ { get; set; } = 0;

    /// <summary>
    /// The initial value for <see cref="PannerNode.GetRefDistanceAsync"/> of the node.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>1</c>.
    /// </remarks>
    [JsonPropertyName("refDistance")]
    public double RefDistance { get; set; } = 1;

    /// <summary>
    /// The initial value for <see cref="PannerNode.GetMaxDistanceAsync"/> of the node.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>10000</c>.
    /// </remarks>
    [JsonPropertyName("maxDistance")]
    public double MaxDistance { get; set; } = 10000;

    /// <summary>
    /// The initial value for <see cref="PannerNode.GetRolloffFactorAsync"/> of the node.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>1</c>.
    /// </remarks>
    [JsonPropertyName("rolloffFactor")]
    public double RolloffFactor { get; set; } = 1;

    /// <summary>
    /// The initial value for <see cref="PannerNode.GetConeInnerAngleAsync"/> of the node.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>360</c>.
    /// </remarks>
    [JsonPropertyName("coneInnerAngle")]
    public double ConeInnerAngle { get; set; } = 360;

    /// <summary>
    /// The initial value for <see cref="PannerNode.GetConeOuterAngleAsync"/> of the node.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>360</c>.
    /// </remarks>
    [JsonPropertyName("coneOuterAngle")]
    public double ConeOuterAngle { get; set; } = 360;

    /// <summary>
    /// The initial value for <see cref="PannerNode.GetConeOuterGainAsync"/> of the node.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>0</c>.
    /// </remarks>
    [JsonPropertyName("coneOuterGain")]
    public double ConeOuterGain { get; set; } = 0;
}
