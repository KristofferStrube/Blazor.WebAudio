using FluentAssertions;

namespace IntegrationTests.AudioNodeTests;

public class AudioBufferSourceNodeTest : AudioNodeTest<AudioBufferSourceNode>
{
    public override async Task<AudioBufferSourceNode> GetDefaultInstanceAsync()
    {
        return await AudioBufferSourceNode.CreateAsync(JSRuntime, await GetAudioContextAsync());
    }

    [Test]
    public async Task GetBufferAsync_ShouldReturnNull_WhenItHasNoBuffer()
    {
        // Arrange
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);

        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, context);

        // Act
        AudioBuffer? buffer = await node.GetBufferAsync();

        // Assert
        _ = buffer.Should().BeNull();
    }

    [Test]
    public async Task GetBufferAsync_ShouldReturnBuffer_WhenItHasBuffer()
    {
        // Arrange
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);
        await using AudioBuffer buffer = await AudioBuffer.CreateAsync(JSRuntime, new AudioBufferOptions() { Length = 1, SampleRate = 8000 });

        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, context, new()
        {
            Buffer = buffer
        });

        // Act
        AudioBuffer? readBuffer = await node.GetBufferAsync();

        // Assert
        _ = readBuffer.Should().NotBeNull();
    }
}
