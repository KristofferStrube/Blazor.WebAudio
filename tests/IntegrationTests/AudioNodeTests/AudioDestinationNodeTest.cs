using FluentAssertions;

namespace IntegrationTests.AudioNodeTests;

public class AudioDestinationNodeTest : AudioNodeTest<AudioDestinationNode>
{
    public override async Task<AudioDestinationNode> GetDefaultInstanceAsync()
    {
        return await AudioContext.GetDestinationAsync();
    }

    [Test]
    public async Task GetMaxChannelCountAsync_RetrievesMaxChannelCount()
    {
        // Arrange
        await using AudioDestinationNode destination = await AudioContext.GetDestinationAsync();

        // Act
        ulong maxChannelCount = await destination.GetMaxChannelCountAsync();

        // Assert
        _ = maxChannelCount.Should().BeGreaterThan(0);
    }
}
