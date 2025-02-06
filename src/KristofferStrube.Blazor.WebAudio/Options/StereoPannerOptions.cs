using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies options for constructing a <see cref="StereoPannerNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#StereoPannerOptions">See the API definition here</see>.</remarks>
public class StereoPannerOptions : AudioNodeOptions
{
    /// <summary>
    /// The initial value for <see cref="StereoPannerNode.GetPanAsync"/>
    /// </summary>
    [JsonPropertyName("pan")]
    public float Pan { get; set; } = 0;
}
