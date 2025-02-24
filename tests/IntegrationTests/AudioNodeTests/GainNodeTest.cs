namespace IntegrationTests.AudioNodeTests;

public class GainNodeTest : AudioNodeTest<GainNode>
{
    public override async Task<GainNode> GetDefaultInstanceAsync()
    {
        return await GainNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
