using FluentAssertions;
using IntegrationTests.Infrastructure;

namespace IntegrationTests.AudioNodeTests;

public abstract class AudioNodeTest<TAudioNode> : AudioContextBlazorTest where TAudioNode : AudioNode
{
    public abstract Task<TAudioNode> GetDefaultInstanceAsync();

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

}
