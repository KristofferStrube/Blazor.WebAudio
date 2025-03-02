using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class AnalyserNodeTest : AudioNodeWithAudioNodeOptions<AnalyserNode, AnalyserOptions>
{
    public override Task<AnalyserNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, AnalyserOptions? options)
        => AnalyserNode.CreateAsync(jSRuntime, context, options);
}
