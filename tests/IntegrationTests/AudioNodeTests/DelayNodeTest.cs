using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class DelayNodeTest : AudioNodeWithAudioNodeOptions<DelayNode, DelayOptions>
{
    public override async Task<DelayNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, DelayOptions? options)
        => await DelayNode.CreateAsync(JSRuntime, AudioContext, options);
}
