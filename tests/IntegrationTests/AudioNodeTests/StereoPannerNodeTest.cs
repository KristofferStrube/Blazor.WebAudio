using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class StereoPannerNodeTest : AudioNodeWithAudioNodeOptions<StereoPannerNode, StereoPannerOptions>
{
    public override async Task<StereoPannerNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, StereoPannerOptions? options)
        => await StereoPannerNode.CreateAsync(jSRuntime, context, options);

    public override Dictionary<ChannelCountMode, Type> UnsupportedChannelCountModes => new()
    {
        [ChannelCountMode.Max] = typeof(NotSupportedErrorException)
    };
}
