using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class ConvolverNodeTest : AudioNodeWithAudioNodeOptions<ConvolverNode, ConvolverOptions>
{
    public override async Task<ConvolverNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, ConvolverOptions? options)
        => await ConvolverNode.CreateAsync(jSRuntime, context, options);
}
