using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class PannerNodeTest : AudioNodeWithAudioNodeOptions<PannerNode, PannerOptions>
{
    public override async Task<PannerNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, PannerOptions? options)
        => await PannerNode.CreateAsync(JSRuntime, AudioContext, options);

    public override Dictionary<ChannelCountMode, Type> UnsupportedChannelCountModes => new()
    {
        [ChannelCountMode.Max] = typeof(NotSupportedErrorException)
    };
}
