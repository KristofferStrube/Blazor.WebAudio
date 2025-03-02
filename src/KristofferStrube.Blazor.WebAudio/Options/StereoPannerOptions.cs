using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies options for constructing a <see cref="StereoPannerNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#StereoPannerOptions">See the API definition here</see>.</remarks>
public class StereoPannerOptions : AudioNodeOptions
{
    /// <inheritdoc path="/summary"/>
    /// <remarks>
    /// The default value is <see cref="ChannelCountMode.ClampedMax"/>.
    /// </remarks>
    public override ChannelCountMode ChannelCountMode { get; set; } = ChannelCountMode.ClampedMax;

    /// <summary>
    /// The initial value for <see cref="StereoPannerNode.GetPanAsync"/>
    /// </summary>
    [JsonPropertyName("pan")]
    public float Pan { get; set; } = 0;
}
