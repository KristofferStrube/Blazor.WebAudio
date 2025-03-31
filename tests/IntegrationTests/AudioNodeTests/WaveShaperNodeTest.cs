using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class WaveShaperNodeTest : AudioNodeWithAudioNodeOptions<WaveShaperNode, WaveShaperOptions>
{
    public override async Task<WaveShaperNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, WaveShaperOptions? options)
        => await WaveShaperNode.CreateAsync(JSRuntime, AudioContext, options);
}
