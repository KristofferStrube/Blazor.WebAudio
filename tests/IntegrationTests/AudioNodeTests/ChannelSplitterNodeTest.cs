namespace IntegrationTests.AudioNodeTests;

public class ChannelSplitterNodeTest : AudioNodeTest<ChannelSplitterNode>
{
    public override async Task<ChannelSplitterNode> GetDefaultInstanceAsync()
    {
        return await ChannelSplitterNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
