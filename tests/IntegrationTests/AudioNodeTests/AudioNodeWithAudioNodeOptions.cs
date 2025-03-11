using FluentAssertions;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public abstract class AudioNodeWithAudioNodeOptions<TAudioNode, TAudioNodeOptions> : AudioNodeTest<TAudioNode> where TAudioNode : AudioNode where TAudioNodeOptions : AudioNodeOptions, new()
{
    public abstract Task<TAudioNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, TAudioNodeOptions? options);

    public override async Task<TAudioNode> GetDefaultInstanceAsync()
    {
        return await CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext(), null);
    }

    [Test]
    public async Task CreateAsync_WithEmptyOptions_Succeeds()
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            return await CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext(), new TAudioNodeOptions());
        };

        // Act
        await OnAfterRerenderAsync();

        // Assert
        _ = EvaluationContext.Exception.Should().BeNull();
        _ = EvaluationContext.Result.Should().BeOfType<TAudioNode>();
    }

    [Test]
    public async Task CreateAsync_WithEmptyOptions_HasSameChannelCountModeAsWhenNoOptionsAreUsed()
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            await using TAudioNode emptyOptionsNode = await CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext(), new TAudioNodeOptions());
            await using TAudioNode noOptionsNode = await CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext(), null);

            ChannelCountMode emptyOptionsCountMode = await emptyOptionsNode.GetChannelCountModeAsync();
            ChannelCountMode noOptionsCountMode = await noOptionsNode.GetChannelCountModeAsync();

            return (emptyOptionsCountMode, noOptionsCountMode);
        };

        // Act
        await OnAfterRerenderAsync();

        // Assert
        _ = EvaluationContext.Exception.Should().BeNull();
        (ChannelCountMode emptyOptionsCountMode, ChannelCountMode noOptionsCountMode) = EvaluationContext.Result.Should().BeOfType<(ChannelCountMode, ChannelCountMode)>().Subject;
        _ = emptyOptionsCountMode.Should().Be(noOptionsCountMode);
    }

    [Test]
    public async Task CreateAsync_WithEmptyOptions_HasSameChannelInterpretationAsWhenNoOptionsAreUsed()
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            await using TAudioNode emptyOptionsNode = await CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext(), new TAudioNodeOptions());
            await using TAudioNode noOptionsNode = await CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext(), null);

            ChannelInterpretation emptyOptionsChannelInterpretation = await emptyOptionsNode.GetChannelInterpretationAsync();
            ChannelInterpretation noOptionsChannelInterpretation = await noOptionsNode.GetChannelInterpretationAsync();

            return (emptyOptionsChannelInterpretation, noOptionsChannelInterpretation);
        };

        // Act
        await OnAfterRerenderAsync();

        // Assert
        _ = EvaluationContext.Exception.Should().BeNull();
        (ChannelInterpretation emptyOptionsChannelInterpretation, ChannelInterpretation noOptionsChannelInterpretation) = EvaluationContext.Result.Should().BeOfType<(ChannelInterpretation, ChannelInterpretation)>().Subject;
        _ = emptyOptionsChannelInterpretation.Should().Be(noOptionsChannelInterpretation);
    }

    [TestCase(ChannelCountMode.Max)]
    [TestCase(ChannelCountMode.Explicit)]
    [TestCase(ChannelCountMode.ClampedMax)]
    [Test]
    public async Task CreateAsync_WithDifferentChannelCountModes_SetsChannelCountMode_ExceptForUnsupportedValues(ChannelCountMode mode)
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            TAudioNodeOptions options = new();
            options.ChannelCountMode = mode;

            await using TAudioNode node = await CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext(), options);

            return await node.GetChannelCountModeAsync();
        };

        // Act
        await OnAfterRerenderAsync();

        // Assert
        if (UnsupportedChannelCountModes.TryGetValue(mode, out Type? exceptionType))
        {
            _ = EvaluationContext.Result.Should().Be(null);
            _ = EvaluationContext.Exception.Should().BeOfType(exceptionType);
        }
        else
        {
            _ = EvaluationContext.Exception.Should().BeNull();
            _ = EvaluationContext.Result.Should().Be(mode);
        }
    }

    [TestCase(ChannelInterpretation.Discrete)]
    [TestCase(ChannelInterpretation.Speakers)]
    [Test]
    public async Task CreateAsync_WithDifferentChannelInterpretations_SetsChannelInterpretation_ExceptForUnsupportedValues(ChannelInterpretation interpretation)
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            TAudioNodeOptions options = new();
            options.ChannelInterpretation = interpretation;

            await using TAudioNode node = await CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext(), options);

            return await node.GetChannelInterpretationAsync();
        };

        // Act
        await OnAfterRerenderAsync();

        // Assert
        if (UnsupportedChannelInterpretations.TryGetValue(interpretation, out Type? exceptionType))
        {
            _ = EvaluationContext.Result.Should().Be(null);
            _ = EvaluationContext.Exception.Should().BeOfType(exceptionType);
        }
        else
        {
            _ = EvaluationContext.Exception.Should().BeNull();
            _ = EvaluationContext.Result.Should().Be(interpretation);
        }
    }
}
