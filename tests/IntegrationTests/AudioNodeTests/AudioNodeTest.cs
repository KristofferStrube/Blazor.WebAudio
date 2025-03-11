using FluentAssertions;
using IntegrationTests.Infrastructure;

namespace IntegrationTests.AudioNodeTests;

public abstract class AudioNodeTest<TAudioNode> : AudioContextBlazorTest where TAudioNode : AudioNode
{
    public abstract Task<TAudioNode> GetDefaultInstanceAsync();

    public virtual Dictionary<ChannelCountMode, Type> UnsupportedChannelCountModes => [];

    public virtual Dictionary<ChannelInterpretation, Type> UnsupportedChannelInterpretations => [];

    [Test]
    public async Task CreateAsync_WithNoOptions_Succeeds()
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            return await GetDefaultInstanceAsync();
        };

        // Act
        await OnAfterRerenderAsync();

        // Assert
        _ = EvaluationContext.Exception.Should().BeNull();
        _ = EvaluationContext.Result.Should().BeOfType<TAudioNode>();
    }

    [TestCase(ChannelCountMode.Max)]
    [TestCase(ChannelCountMode.Explicit)]
    [TestCase(ChannelCountMode.ClampedMax)]
    [Test]
    public async Task SettingChannelCountMode_SetsChannelCountMode_ExceptForUnsupportedValues(ChannelCountMode mode)
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            await using TAudioNode node = await GetDefaultInstanceAsync();

            await node.SetChannelCountModeAsync(mode);

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
    public async Task SettingChannelInterpretation_SetsInterpretation_ExceptForUnsupportedValues(ChannelInterpretation interpretation)
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            await using TAudioNode node = await GetDefaultInstanceAsync();

            await node.SetChannelInterpretationAsync(interpretation);

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
