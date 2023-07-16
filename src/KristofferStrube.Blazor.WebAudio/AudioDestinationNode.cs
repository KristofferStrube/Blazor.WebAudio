using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This is an <see cref="AudioNode"/> representing the final audio destination and is what the user will ultimately hear.
/// It can often be considered as an audio output device which is connected to speakers.
/// All rendered audio to be heard will be routed to this node, a "terminal" node in the <see cref="AudioContext"/>'s routing graph.
/// There is only a single <see cref="AudioDestinationNode"/> per <see cref="AudioContext"/>, provided through the <see cref="BaseAudioContext.GetDestinationAsync"/> method.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioDestinationNode">See the API definition here</see>.</remarks>
public class AudioDestinationNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioDestinationNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioDestinationNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="AudioDestinationNode"/>.</returns>
    public static new Task<AudioDestinationNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AudioDestinationNode(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioDestinationNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioDestinationNode"/>.</param>
    protected AudioDestinationNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
