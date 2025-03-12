using FluentAssertions;
using KristofferStrube.Blazor.WebIDL.Exceptions;

namespace IntegrationTests.AudioNodeTests;

public abstract class AudioNodeTest<TAudioNode> : BlazorTest where TAudioNode : AudioNode
{
    public abstract Task<TAudioNode> GetDefaultInstanceAsync();

    public virtual Dictionary<ChannelCountMode, Type> UnsupportedChannelCountModes => [];

    public virtual Dictionary<ChannelInterpretation, Type> UnsupportedChannelInterpretations => [];

    [Test]
    public async Task CreateAsync_WithNoOptions_Succeeds()
    {
        // Act
        await using TAudioNode node = await GetDefaultInstanceAsync();

        // Assert
        _ = node.Should().BeOfType<TAudioNode>();
    }

    [TestCase(ChannelCountMode.Max)]
    [TestCase(ChannelCountMode.Explicit)]
    [TestCase(ChannelCountMode.ClampedMax)]
    [Test]
    public async Task SettingChannelCountMode_SetsChannelCountMode_ExceptForUnsupportedValues(ChannelCountMode mode)
    {
        // Act
        Func<Task<ChannelCountMode>> action = async () =>
        {
            await using TAudioNode node = await GetDefaultInstanceAsync();
            await node.SetChannelCountModeAsync(mode);
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
    public async Task SettingChannelInterpretation_SetsInterpretation_ExceptForUnsupportedValues(ChannelInterpretation interpretation)
    {
        // Act
        Func<Task<ChannelInterpretation>> action = async () =>
        {
            await using TAudioNode node = await GetDefaultInstanceAsync();
            await node.SetChannelInterpretationAsync(interpretation);
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
