using FluentAssertions;
using FluentAssertions.Specialized;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public abstract class AudioNodeWithAudioNodeOptions<TAudioNode, TAudioNodeOptions> : AudioNodeTest<TAudioNode> where TAudioNode : AudioNode where TAudioNodeOptions : AudioNodeOptions, new()
{
    public abstract Task<TAudioNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, TAudioNodeOptions? options);

    public override async Task<TAudioNode> GetDefaultInstanceAsync()
    {
        return await CreateAsync(EvaluationContext.JSRuntime, await GetAudioContextAsync(), null);
    }

    [Test]
    public async Task CreateAsync_WithEmptyOptions_Succeeds()
    {
        // Act
        await using TAudioNode node = await CreateAsync(EvaluationContext.JSRuntime, await GetAudioContextAsync(), new TAudioNodeOptions());

        // Assert
        _ = node.Should().BeOfType<TAudioNode>();
    }

    [Test]
    public async Task CreateAsync_WithEmptyOptions_HasSameChannelCountModeAsWhenNoOptionsAreUsed()
    {
        // Arrange
        await using TAudioNode emptyOptionsNode = await CreateAsync(EvaluationContext.JSRuntime, await GetAudioContextAsync(), new TAudioNodeOptions());
        await using TAudioNode noOptionsNode = await CreateAsync(EvaluationContext.JSRuntime, await GetAudioContextAsync(), null);

        // Act
        ChannelCountMode emptyOptionsCountMode = await emptyOptionsNode.GetChannelCountModeAsync();
        ChannelCountMode noOptionsCountMode = await noOptionsNode.GetChannelCountModeAsync();

        // Assert
        _ = emptyOptionsCountMode.Should().Be(noOptionsCountMode);
    }

    [Test]
    public async Task CreateAsync_WithEmptyOptions_HasSameChannelInterpretationAsWhenNoOptionsAreUsed()
    {
        // Arrange
        await using TAudioNode emptyOptionsNode = await CreateAsync(EvaluationContext.JSRuntime, await GetAudioContextAsync(), new TAudioNodeOptions());
        await using TAudioNode noOptionsNode = await CreateAsync(EvaluationContext.JSRuntime, await GetAudioContextAsync(), null);

        // Act
        ChannelInterpretation emptyOptionsChannelInterpretation = await emptyOptionsNode.GetChannelInterpretationAsync();
        ChannelInterpretation noOptionsChannelInterpretation = await noOptionsNode.GetChannelInterpretationAsync();

        // Assert
        _ = emptyOptionsChannelInterpretation.Should().Be(noOptionsChannelInterpretation);
    }

    [TestCase(ChannelCountMode.Max)]
    [TestCase(ChannelCountMode.Explicit)]
    [TestCase(ChannelCountMode.ClampedMax)]
    [Test]
    public async Task CreateAsync_WithDifferentChannelCountModes_SetsChannelCountMode_ExceptForUnsupportedValues(ChannelCountMode mode)
    {
        // Arrange
        TAudioNodeOptions options = new();
        options.ChannelCountMode = mode;

        // Act
        Func<Task<ChannelCountMode>> action = async () =>
        {
            await using TAudioNode node = await CreateAsync(EvaluationContext.JSRuntime, await GetAudioContextAsync(), options);
            return await node.GetChannelCountModeAsync();
        };

        // Assert
        if (UnsupportedChannelCountModes.TryGetValue(mode, out Type? exceptionType))
        {
            _ = (await action.Should().ThrowAsync<WebIDLException>()).And.Should().BeOfType(exceptionType);
        }
        else
        {
            ChannelCountMode result = await action();
            _ = result.Should().Be(mode);
        }
    }

    [TestCase(ChannelInterpretation.Discrete)]
    [TestCase(ChannelInterpretation.Speakers)]
    [Test]
    public async Task CreateAsync_WithDifferentChannelInterpretations_SetsChannelInterpretation_ExceptForUnsupportedValues(ChannelInterpretation interpretation)
    {
        // Arrange
        TAudioNodeOptions options = new();
        options.ChannelInterpretation = interpretation;

        // Act
        Func<Task<ChannelInterpretation>> action = async () =>
        {
            await using TAudioNode node = await CreateAsync(EvaluationContext.JSRuntime, await GetAudioContextAsync(), options);
            return await node.GetChannelInterpretationAsync();
        };

        // Assert
        if (UnsupportedChannelInterpretations.TryGetValue(interpretation, out Type? exceptionType))
        {
            _ = (await action.Should().ThrowAsync<WebIDLException>()).And.Should().BeOfType(exceptionType);

        }
        else
        {
            ChannelInterpretation result = await action();
            _ = result.Should().Be(interpretation);
        }
    }
}
