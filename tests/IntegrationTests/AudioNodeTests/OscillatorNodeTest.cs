namespace IntegrationTests.AudioNodeTests;

public class OscillatorNodeTest : AudioNodeTest<OscillatorNode>
{
    public override async Task<OscillatorNode> GetDefaultInstanceAsync()
    {
        return await OscillatorNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
