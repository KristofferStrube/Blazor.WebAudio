using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class ConstantSourceNodeTest : AudioNodeWithAudioNodeOptions<ConstantSourceNode, ConstantSourceOptions>
{
    public override async Task<ConstantSourceNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, ConstantSourceOptions? options)
        => await ConstantSourceNode.CreateAsync(jSRuntime, context, options);
}
