using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents an audio source from an in-memory audio asset in an <see cref="AudioBuffer"/>.
/// It is useful for playing audio assets which require a high degree of scheduling flexibility and accuracy.
/// If sample-accurate playback of network- or disk-backed assets is required, an implementer should use <see cref="AudioWorkletNode"/> to implement playback.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioBufferSourceNode">See the API definition here</see>.</remarks>
public class AudioBufferSourceNode : AudioScheduledSourceNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioBufferSourceNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioBufferSourceNode"/>.</param>
    protected AudioBufferSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
