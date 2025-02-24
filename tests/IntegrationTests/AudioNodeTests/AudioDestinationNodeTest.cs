namespace IntegrationTests.AudioNodeTests;

public class AudioDestinationNodeTest : AudioNodeTest<AudioDestinationNode>
{
    public override async Task<AudioDestinationNode> GetDefaultInstanceAsync()
    {
        AudioContext context = await EvaluationContext.GetAudioContext();
        return await context.GetDestinationAsync();
    }
}
