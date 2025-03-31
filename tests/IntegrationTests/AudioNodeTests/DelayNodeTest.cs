using FluentAssertions;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class DelayNodeTest : AudioNodeWithAudioNodeOptions<DelayNode, DelayOptions>
{
    public override async Task<DelayNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, DelayOptions? options)
        => await DelayNode.CreateAsync(JSRuntime, AudioContext, options);

    [Test]
    [TestCase(0)]
    [TestCase(10)]
    public async Task GetDelayTimeAsync_ShouldRetrieveDelayTimeParameter(float delayTime)
    {
        // Arrange
        await using DelayNode node = await DelayNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            DelayTime = delayTime,
            MaxDelayTime = delayTime + 1,
        });

        // Act
        await using AudioParam delayTimeParameter = await node.GetDelayTimeAsync();

        // Assert
        float readDelayTime = await delayTimeParameter.GetValueAsync();
        _ = readDelayTime.Should().Be(delayTime);

        await delayTimeParameter.SetValueAsync(delayTime + 1);
        readDelayTime = await delayTimeParameter.GetValueAsync();
        _ = readDelayTime.Should().Be(delayTime + 1);
    }
}
