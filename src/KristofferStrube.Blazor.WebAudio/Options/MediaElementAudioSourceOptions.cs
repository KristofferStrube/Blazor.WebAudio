using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Options;

/// <summary>
/// This specifies the options to use in constructing a <see cref="MediaElementAudioSourceNode"/>.
/// </summary>
/// <remarks>
/// <see href="https://www.w3.org/TR/webaudio/#MediaElementAudioSourceOptions">See the API definition here</see>.
/// </remarks>
public class MediaElementAudioSourceOptions
{
    /// <summary>
    /// The media element that will be re-routed.
    /// </summary>
    [JsonPropertyName("mediaElement")]
    public required IJSObjectReference MediaElement { get; set; }
}
