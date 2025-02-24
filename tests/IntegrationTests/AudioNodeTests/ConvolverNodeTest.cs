namespace IntegrationTests.AudioNodeTests;

public class ConvolverNodeTest : AudioNodeTest<ConvolverNode>
{
    public override async Task<ConvolverNode> GetDefaultInstanceAsync()
    {
        return await ConvolverNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
