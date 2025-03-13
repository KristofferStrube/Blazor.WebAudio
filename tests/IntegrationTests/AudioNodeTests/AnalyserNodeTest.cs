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

        await using OscillatorNode oscillator = await OscillatorNode.CreateAsync(JSRuntime, context);
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, context);
        await oscillator.ConnectAsync(node);
        await oscillator.StartAsync();

        int bufferLength = (int)await node.GetFrequencyBinCountAsync();
        await using Float32Array buffer = await Float32Array.CreateAsync(JSRuntime, bufferLength);

        // Act
        await node.GetFloatFrequencyDataAsync(buffer);

        // Assert
        float lastElement = await buffer.AtAsync(bufferLength - 1);
        _ = lastElement.Should().NotBe(0);
    }

    [Test]
    public async Task GetByteFrequencyDataAsync_ShouldPopulateBuffer()
    {
        // Arrange
        await using AudioContext context = await GetAudioContextAsync();

        await using OscillatorNode oscillator = await OscillatorNode.CreateAsync(JSRuntime, context);
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, context);
        await oscillator.ConnectAsync(node);
        await oscillator.StartAsync();

        int bufferLength = (int)await node.GetFrequencyBinCountAsync();
        await using Uint8Array buffer = await Uint8Array.CreateAsync(JSRuntime, bufferLength);

        // Act
        await node.GetByteFrequencyDataAsync(buffer);

        // Assert
        byte tenthElement = await buffer.AtAsync(10);
        _ = tenthElement.Should().NotBe(0);
    }

    [Test]
    public async Task GetFloatTimeDomainDataAsync_ShouldPopulateBuffer()
    {
        // Arrange
        await using AudioContext context = await GetAudioContextAsync();

        await using OscillatorNode oscillator = await OscillatorNode.CreateAsync(JSRuntime, context);
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, context);
        await oscillator.ConnectAsync(node);
        await oscillator.StartAsync();

        int bufferLength = (int)await node.GetFftSizeAsync();
        await using Float32Array buffer = await Float32Array.CreateAsync(JSRuntime, bufferLength);

        // Act
        await node.GetFloatTimeDomainDataAsync(buffer);

        // Assert
        float tenthElement = await buffer.AtAsync(bufferLength - 1);
        _ = tenthElement.Should().NotBe(0);
    }
}
