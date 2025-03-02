using FluentAssertions;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public abstract class AudioNodeWithAudioNodeOptions<TAudioNode, TAudioNodeOptions> : AudioNodeTest<TAudioNode> where TAudioNode : AudioNode where TAudioNodeOptions : AudioNodeOptions, new()
{
    public abstract Task<TAudioNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, TAudioNodeOptions? options);

    public virtual TAudioNodeOptions? CreateDefaultOptions() => null;

    public override async Task<TAudioNode> GetDefaultInstanceAsync()
    {
        return await CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext(), CreateDefaultOptions() ?? null);
    }

    [Test]
    public async Task CreateAsync_WithEmptyOptions_Succeeds()
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            return await CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext(), CreateDefaultOptions() ?? new TAudioNodeOptions());
        };

        // Act
        await OnAfterRerenderAsync();

        // Assert
        _ = EvaluationContext.Exception.Should().BeNull();
        _ = EvaluationContext.Result.Should().BeOfType<TAudioNode>();
    }
}
