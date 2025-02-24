namespace IntegrationTests.AudioNodeTests;

public class DynamicsCompressorNodeTest : AudioNodeTest<DynamicsCompressorNode>
{
    public override async Task<DynamicsCompressorNode> GetDefaultInstanceAsync()
    {
        return await DynamicsCompressorNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
