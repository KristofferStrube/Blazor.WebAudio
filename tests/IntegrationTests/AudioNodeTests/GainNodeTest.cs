using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class GainNodeTest : AudioNodeWithAudioNodeOptions<GainNode, GainOptions>
{
    public override async Task<GainNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, GainOptions? options)
        => await GainNode.CreateAsync(jSRuntime, context, options);
}
