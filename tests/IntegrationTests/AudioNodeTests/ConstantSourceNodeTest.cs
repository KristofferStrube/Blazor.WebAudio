using FluentAssertions;

namespace IntegrationTests.AudioNodeTests;

public class ConstantSourceNodeTest : AudioNodeTest<ConstantSourceNode>
{
    public override async Task<ConstantSourceNode> GetDefaultInstanceAsync()
    {
        return await ConstantSourceNode.CreateAsync(JSRuntime, AudioContext);
    }

    [Test]
    [TestCase(1)]
    [TestCase(-0.5f)]
    public async Task GetOffsetAsync_ShouldRetrieveOffsetParameter(float offset)
    {
        // Arrange
        await using ConstantSourceNode node = await ConstantSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Offset = offset
        });

        // Act
        await using AudioParam offsetParameter = await node.GetOffsetAsync();

        // Assert
        float readOffset = await offsetParameter.GetValueAsync();
        _ = readOffset.Should().Be(offset);

        await offsetParameter.SetValueAsync(offset + 1);
        readOffset = await offsetParameter.GetValueAsync();
        _ = readOffset.Should().Be(offset + 1);
    }
}
