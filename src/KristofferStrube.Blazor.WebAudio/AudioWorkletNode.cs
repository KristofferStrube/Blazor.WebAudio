using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents an audio graph whose <see cref="AudioDestinationNode"/> is routed to a real-time output device that produces a signal directed at the user.
/// In most use cases, only a single <see cref="AudioContext"/> is used per document.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#audioworkletnode">See the API definition here</see>.</remarks>
public class AudioWorkletNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioWorkletNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioWorkletNode"/>.</param>
    protected AudioWorkletNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
