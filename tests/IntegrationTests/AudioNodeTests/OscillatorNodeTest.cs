using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class OscillatorNodeTest : AudioNodeWithAudioNodeOptions<OscillatorNode, OscillatorOptions>
{
    public override async Task<OscillatorNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, OscillatorOptions? options)
        => await OscillatorNode.CreateAsync(JSRuntime, AudioContext, options);
}
