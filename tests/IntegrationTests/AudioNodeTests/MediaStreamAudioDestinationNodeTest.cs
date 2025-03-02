using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class MediaStreamAudioDestinationNodeTest : AudioNodeWithAudioNodeOptions<MediaStreamAudioDestinationNode, AudioNodeOptions>
{
    public override async Task<MediaStreamAudioDestinationNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, AudioNodeOptions? options)
        => await MediaStreamAudioDestinationNode.CreateAsync(jSRuntime, context, options);
}
