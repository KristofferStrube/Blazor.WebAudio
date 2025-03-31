using FluentAssertions;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class ChannelMergerNodeTest : AudioNodeWithAudioNodeOptions<ChannelMergerNode, ChannelMergerOptions>
{
    public override async Task<ChannelMergerNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, ChannelMergerOptions? options)
        => await ChannelMergerNode.CreateAsync(JSRuntime, AudioContext, options);

    public override Dictionary<ChannelCountMode, Type> UnsupportedChannelCountModes => new()
    {
        [ChannelCountMode.Max] = typeof(InvalidStateErrorException),
        [ChannelCountMode.ClampedMax] = typeof(InvalidStateErrorException),
    };

    [Test]
    public async Task CreateAsync_WithNoOptions_DefaultsTo6Inputs()
    {
        // Arrange
        await using ChannelMergerNode node = await ChannelMergerNode.CreateAsync(JSRuntime, AudioContext);

        // Act
        ulong numberOfInputs = await node.GetNumberOfInputsAsync();

        // Assert
        _ = numberOfInputs.Should().Be(6);
    }

    [Test]
    public async Task CreateAsync_WithEmptyOptions_DefaultsTo6Inputs()
    {
        // Arrange
        await using ChannelMergerNode node = await ChannelMergerNode.CreateAsync(JSRuntime, AudioContext, new());

        // Act
        ulong numberOfInputs = await node.GetNumberOfInputsAsync();

        // Assert
        _ = numberOfInputs.Should().Be(6);
    }
}