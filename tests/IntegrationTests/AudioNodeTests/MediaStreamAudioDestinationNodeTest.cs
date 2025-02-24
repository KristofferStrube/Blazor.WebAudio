namespace IntegrationTests.AudioNodeTests;

public class MediaStreamAudioDestinationNodeTest : AudioNodeTest<MediaStreamAudioDestinationNode>
{
    public override async Task<MediaStreamAudioDestinationNode> GetDefaultInstanceAsync()
    {
        return await MediaStreamAudioDestinationNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
