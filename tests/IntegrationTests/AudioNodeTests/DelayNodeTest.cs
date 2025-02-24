namespace IntegrationTests.AudioNodeTests;

public class DelayNodeTest : AudioNodeTest<DelayNode>
{
    public override async Task<DelayNode> GetDefaultInstanceAsync()
    {
        return await DelayNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
