using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class BiquadFilterNodeTest : AudioNodeWithAudioNodeOptions<BiquadFilterNode, BiquadFilterOptions>
{
    public override async Task<BiquadFilterNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, BiquadFilterOptions? options)
        => await BiquadFilterNode.CreateAsync(jSRuntime, context, options);
}
