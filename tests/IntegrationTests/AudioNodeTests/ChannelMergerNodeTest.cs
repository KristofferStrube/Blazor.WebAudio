namespace IntegrationTests.AudioNodeTests;

public class ChannelMergerNodeTest : AudioNodeTest<ChannelMergerNode>
{
    public override async Task<ChannelMergerNode> GetDefaultInstanceAsync()
    {
        return await ChannelMergerNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
