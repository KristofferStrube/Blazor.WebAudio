namespace IntegrationTests.AudioNodeTests;

public class MediaStreamAudioSourceNodeTest : AudioNodeTest<MediaStreamAudioSourceNode>
{
    public override async Task<MediaStreamAudioSourceNode> GetDefaultInstanceAsync()
    {
        return await MediaStreamAudioSourceNode.CreateAsync(JSRuntime, await GetAudioContextAsync(), new MediaStreamAudioSourceOptions()
        {
            MediaStream = await EvaluationContext.GetMediaStream()
        });
    }
}
