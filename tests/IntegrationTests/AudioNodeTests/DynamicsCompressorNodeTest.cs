using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class DynamicsCompressorNodeTest : AudioNodeWithAudioNodeOptions<DynamicsCompressorNode, DynamicsCompressorOptions>
{
    public override async Task<DynamicsCompressorNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, DynamicsCompressorOptions? options)
        => await DynamicsCompressorNode.CreateAsync(jSRuntime, context, options);
}
