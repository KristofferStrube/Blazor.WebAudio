namespace IntegrationTests.AudioNodeTests;

public class IIRFilterNodeTest : AudioNodeTest<IIRFilterNode>
{
    public override async Task<IIRFilterNode> GetDefaultInstanceAsync()
    {
        return await IIRFilterNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext(), new IIRFilterOptions()
        {
            Feedforward = [1],
            Feedback = [1],
        });
    }
}
