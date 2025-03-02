using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class ChannelMergerNodeTest : AudioNodeWithAudioNodeOptions<ChannelMergerNode, ChannelMergerOptions>
{
    public override async Task<ChannelMergerNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, ChannelMergerOptions? options)
        => await ChannelMergerNode.CreateAsync(jSRuntime, context, options);
}