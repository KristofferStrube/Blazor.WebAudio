using KristofferStrube.Blazor.MediaCaptureStreams;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies the options to use in constructing a <see cref="MediaStreamTrackAudioSourceNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#MediaStreamTrackAudioSourceOptions">See the API definition here</see>.</remarks>
public class MediaStreamTrackAudioSourceOptions
{
    /// <summary>
    /// The media stream track that will act as a source.
    /// </summary>
    [JsonPropertyName("mediaStreamTrack")]
    public required MediaStreamTrack MediaStreamTrack { get; set; }
}