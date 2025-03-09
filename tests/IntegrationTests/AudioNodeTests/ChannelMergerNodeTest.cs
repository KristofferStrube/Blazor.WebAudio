using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class ChannelMergerNodeTest : AudioNodeWithAudioNodeOptions<ChannelMergerNode, ChannelMergerOptions>
{
    public override async Task<ChannelMergerNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, ChannelMergerOptions? options)
        => await ChannelMergerNode.CreateAsync(jSRuntime, context, options);

    public override Dictionary<ChannelCountMode, Type> UnsupportedChannelCountModes => new()
    {
        [ChannelCountMode.Max] = typeof(InvalidStateErrorException),
        [ChannelCountMode.ClampedMax] = typeof(InvalidStateErrorException),
    };
}