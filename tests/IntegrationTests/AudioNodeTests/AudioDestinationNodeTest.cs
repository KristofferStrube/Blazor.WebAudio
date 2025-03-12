namespace IntegrationTests.AudioNodeTests;

public class AudioDestinationNodeTest : AudioNodeTest<AudioDestinationNode>
{
    public override async Task<AudioDestinationNode> GetDefaultInstanceAsync()
    {
        AudioContext context = await GetAudioContextAsync();
        return await context.GetDestinationAsync();
    }
}
