namespace IntegrationTests.AudioNodeTests;

public class WaveShaperNodeTest : AudioNodeTest<WaveShaperNode>
{
    public override async Task<WaveShaperNode> GetDefaultInstanceAsync()
    {
        return await WaveShaperNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
