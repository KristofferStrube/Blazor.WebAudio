namespace IntegrationTests.AudioNodeTests;

public class AudioBufferSourceNodeTest : AudioNodeTest<AudioBufferSourceNode>
{
    public override async Task<AudioBufferSourceNode> GetDefaultInstanceAsync()
    {
        return await AudioBufferSourceNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
