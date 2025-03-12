namespace IntegrationTests.AudioNodeTests;

public class ConstantSourceNodeTest : AudioNodeTest<ConstantSourceNode>
{
    public override async Task<ConstantSourceNode> GetDefaultInstanceAsync()
    {
        return await ConstantSourceNode.CreateAsync(EvaluationContext.JSRuntime, await GetAudioContextAsync());
    }
}
