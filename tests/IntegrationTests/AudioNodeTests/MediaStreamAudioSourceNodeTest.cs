namespace IntegrationTests.AudioNodeTests;

public class MediaStreamAudioSourceNodeTest : AudioNodeTest<MediaStreamAudioSourceNode>
{
    public override async Task<MediaStreamAudioSourceNode> GetDefaultInstanceAsync()
    {
        return await MediaStreamAudioSourceNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext(), new MediaStreamAudioSourceOptions()
        {
            MediaStream = await EvaluationContext.GetMediaStream()
        });
    }
}
