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
}
