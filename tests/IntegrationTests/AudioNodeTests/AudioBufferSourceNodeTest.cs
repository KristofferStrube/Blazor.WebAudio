namespace IntegrationTests.AudioNodeTests;

public class AudioBufferSourceNodeTest : AudioNodeTest<AudioBufferSourceNode>
{
    public override async Task<AudioBufferSourceNode> GetDefaultInstanceAsync()
    {
        return await AudioBufferSourceNode.CreateAsync(JSRuntime, await GetAudioContextAsync());
    }
}
