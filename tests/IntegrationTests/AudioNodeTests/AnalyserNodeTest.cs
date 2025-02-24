namespace IntegrationTests.AudioNodeTests;

public class AnalyserNodeTest : AudioNodeTest<AnalyserNode>
{
    public override async Task<AnalyserNode> GetDefaultInstanceAsync()
    {
        return await AnalyserNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
