using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class ChannelSplitterNodeTest : AudioNodeWithAudioNodeOptions<ChannelSplitterNode, ChannelSplitterOptions>
{
    public override async Task<ChannelSplitterNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, ChannelSplitterOptions? options)
        => await ChannelSplitterNode.CreateAsync(JSRuntime, AudioContext, options);

    public override Dictionary<ChannelCountMode, Type> UnsupportedChannelCountModes => new()
    {
        [ChannelCountMode.Max] = typeof(InvalidStateErrorException),
        [ChannelCountMode.ClampedMax] = typeof(InvalidStateErrorException),
    };

    public override Dictionary<ChannelInterpretation, Type> UnsupportedChannelInterpretations => new()
    {
        [ChannelInterpretation.Speakers] = typeof(InvalidStateErrorException),
    };
}
