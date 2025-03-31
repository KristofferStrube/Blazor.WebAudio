using FluentAssertions;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;
using System;

namespace IntegrationTests.AudioNodeTests;

public class ConvolverNodeTest : AudioNodeWithAudioNodeOptions<ConvolverNode, ConvolverOptions>
{
    public override async Task<ConvolverNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, ConvolverOptions? options)
        => await ConvolverNode.CreateAsync(JSRuntime, AudioContext, options);

    public override Dictionary<ChannelCountMode, Type> UnsupportedChannelCountModes => new()
    {
        [ChannelCountMode.Max] = typeof(NotSupportedErrorException)
    };

    [Test]
    public async Task GetBufferAsync_ShouldReturnNull_WhenItHasNoBuffer()
    {
        // Arrange
        await using ConvolverNode node = await ConvolverNode.CreateAsync(JSRuntime, AudioContext);

        // Act
        AudioBuffer? buffer = await node.GetBufferAsync();

        // Assert
        _ = buffer.Should().BeNull();
    }

    [Test]
    public async Task GetBufferAsync_ShouldRetrieveBuffer_WhenItHasBuffer()
    {
        // Arrange
        await using AudioBuffer buffer = await AudioBuffer.CreateAsync(JSRuntime, new AudioBufferOptions()
        {
            Length = 1,
            SampleRate = await AudioContext.GetSampleRateAsync()
        });

        await using ConvolverNode node = await ConvolverNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Buffer = buffer
        });

        // Act
        AudioBuffer? readBuffer = await node.GetBufferAsync();

        // Assert
        _ = readBuffer.Should().NotBeNull();
    }

    [Test]
    public async Task SetBufferAsync_ShouldUpdateTheBuffer()
    {
        // Arrange
        await using ConvolverNode node = await ConvolverNode.CreateAsync(JSRuntime, AudioContext);

        await using AudioBuffer buffer = await AudioBuffer.CreateAsync(JSRuntime, new AudioBufferOptions()
        {
            Length = 1,
            SampleRate = await AudioContext.GetSampleRateAsync()
        });

        // Act
        await node.SetBufferAsync(buffer);

        // Assert
        AudioBuffer? readBuffer = await node.GetBufferAsync();
        _ = readBuffer.Should().NotBeNull();
    }

    [Test]
    public async Task SetBufferAsync_WithNullArgument_ShouldClearBuffer()
    {
        // Arrange
        await using AudioBuffer buffer = await AudioBuffer.CreateAsync(JSRuntime, new AudioBufferOptions()
        {
            Length = 1,
            SampleRate = await AudioContext.GetSampleRateAsync()
        });

        await using ConvolverNode node = await ConvolverNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Buffer = buffer
        });

        // Act
        await node.SetBufferAsync(null);

        // Assert
        AudioBuffer? readBuffer = await node.GetBufferAsync();
        _ = readBuffer.Should().BeNull();
    }

    [Test]
    [TestCase(false)]
    [TestCase(true)]
    public async Task GetNormalizeAsync_ShouldRetrieveNormalize(bool normalize)
    {
        // Arrange
        await using ConvolverNode node = await ConvolverNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            DisableNormalization = !normalize
        });

        // Act
        bool readNormalize = await node.GetNormalizeAsync();

        // Assert
        _ = readNormalize.Should().Be(normalize);
    }

    [Test]
    [TestCase(false)]
    [TestCase(true)]
    public async Task SetNormalizeAsync_ShouldRetrieveNormalize(bool normalize)
    {
        // Arrange
        await using ConvolverNode node = await ConvolverNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            DisableNormalization = normalize // this is the inverse as the options parameter is whether normalization should be disabled.
        });

        // Act
        await node.SetNormalizeAsync(normalize);

        // Assert
        bool readNormalize = await node.GetNormalizeAsync();
        _ = readNormalize.Should().Be(normalize);
    }
}
