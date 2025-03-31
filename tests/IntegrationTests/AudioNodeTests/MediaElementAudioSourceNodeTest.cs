using KristofferStrube.Blazor.WebAudio.Options;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class MediaElementAudioSourceNodeTest : AudioNodeTest<MediaElementAudioSourceNode>
{
    public override async Task<MediaElementAudioSourceNode> GetDefaultInstanceAsync()
    {
        IJSObjectReference element = await EvaluationContext.GetAudioElementAyns();
        return await MediaElementAudioSourceNode.CreateAsync(JSRuntime, AudioContext, new MediaElementAudioSourceOptions()
        {
            MediaElement = element
        });
    }
}
