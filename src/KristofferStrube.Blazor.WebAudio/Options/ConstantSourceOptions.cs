using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies options for constructing a <see cref="ConstantSourceNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#ConstantSourceOptions">See the API definition here</see>.</remarks>
public class ConstantSourceOptions : AudioNodeOptions
{
    /// <summary>
    /// The initial value for the <see cref="ConstantSourceNode.GetOffsetAsync"/> <see cref="AudioParam"/> of this node.
    /// </summary>
    [JsonPropertyName("offset")]
    public float Offset { get; set; } = 1;
}
