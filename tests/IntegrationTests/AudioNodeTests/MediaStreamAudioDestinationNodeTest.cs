using KristofferStrube.Blazor.WebAudio.Options;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class MediaStreamAudioDestinationNodeTest : AudioNodeWithAudioNodeOptions<MediaStreamAudioDestinationNode, MediaStreamAudioDestinationOptions>
{
    public override async Task<MediaStreamAudioDestinationNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, MediaStreamAudioDestinationOptions? options)
        => await MediaStreamAudioDestinationNode.CreateAsync(JSRuntime, AudioContext, options);
}
