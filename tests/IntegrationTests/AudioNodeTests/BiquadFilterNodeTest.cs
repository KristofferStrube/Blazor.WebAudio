namespace IntegrationTests.AudioNodeTests;

public class BiquadFilterNodeTest : AudioNodeTest<BiquadFilterNode>
{
    public override async Task<BiquadFilterNode> GetDefaultInstanceAsync()
    {
        return await BiquadFilterNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
