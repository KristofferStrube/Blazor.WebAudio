using KristofferStrube.Blazor.DOM;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Events;

/// <summary>
/// Initialisation options for an <see cref="OfflineAudioCompletionEvent"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#OfflineAudioCompletionEventInit">See the API definition here</see>.</remarks>
public class OfflineAudioCompletionEventInit : EventInit
{
    /// <summary>
    /// Value to be assigned to the <see cref="OfflineAudioCompletionEvent.GetRenderedBufferAsync"/> of the event.
    /// </summary>
    [JsonPropertyName("renderedBuffer")]
    public required AudioBuffer RenderedBuffer { get; set; }
}
