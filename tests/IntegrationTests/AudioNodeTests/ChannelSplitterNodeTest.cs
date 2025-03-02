using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class ChannelSplitterNodeTest : AudioNodeWithAudioNodeOptions<ChannelSplitterNode, ChannelSplitterOptions>
{
    public override async Task<ChannelSplitterNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, ChannelSplitterOptions? options)
        => await ChannelSplitterNode.CreateAsync(jSRuntime, context, options);
}
