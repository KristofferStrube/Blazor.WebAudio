using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This is an <see cref="AudioNode"/> representing the final audio destination and is what the user will ultimately hear.
/// It can often be considered as an audio output device which is connected to speakers.
/// All rendered audio to be heard will be routed to this node, a "terminal" node in the <see cref="AudioContext"/>'s routing graph.
/// There is only a single <see cref="AudioDestinationNode"/> per <see cref="AudioContext"/>, provided through the <see cref="BaseAudioContext.GetDestinationAsync"/> method.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioDestinationNode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class AudioDestinationNode : AudioNode, IJSCreatable<AudioDestinationNode>
{
    /// <inheritdoc/>
    public static new async Task<AudioDestinationNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<AudioDestinationNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new AudioDestinationNode(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected AudioDestinationNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    /// <summary>
    /// The maximum number of channels that the channelCount attribute can be set to.
    /// An <see cref="AudioDestinationNode"/> representing the audio hardware end-point (the normal case) can potentially output more than <c>2</c> channels of audio if the audio hardware is multi-channel.
    /// maxChannelCount is the maximum number of channels that this hardware is capable of supporting.
    /// </summary>
    /// <returns></returns>
    public async Task<ulong> GetMaxChannelCountAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "maxChannelCount");
    }
}
