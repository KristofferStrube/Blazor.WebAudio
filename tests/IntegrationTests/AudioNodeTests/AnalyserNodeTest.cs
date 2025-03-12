using FluentAssertions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class AnalyserNodeTest : AudioNodeWithAudioNodeOptions<AnalyserNode, AnalyserOptions>
{
    public override Task<AnalyserNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, AnalyserOptions? options)
        => AnalyserNode.CreateAsync(jSRuntime, context, options);

    [Test]
    public async Task GetFloatFrequencyDataAsync_ShouldPopulateBuffer()
    {
        // Arrange
        await using AudioContext context = await GetAudioContextAsync();

        await using OscillatorNode oscillator = await OscillatorNode.CreateAsync(EvaluationContext.JSRuntime, context);
        await using AnalyserNode node = await AnalyserNode.CreateAsync(EvaluationContext.JSRuntime, context);
        await oscillator.ConnectAsync(node);
        await oscillator.StartAsync();

        int bufferLength = (int)await node.GetFrequencyBinCountAsync();
        await using Float32Array array = await Float32Array.CreateAsync(EvaluationContext.JSRuntime, bufferLength);

        // Act
        await node.GetFloatFrequencyDataAsync(array);

        // Assert
        float lastElement = await array.AtAsync(bufferLength - 1);
        _ = lastElement.Should().NotBe(0);
    }
}
