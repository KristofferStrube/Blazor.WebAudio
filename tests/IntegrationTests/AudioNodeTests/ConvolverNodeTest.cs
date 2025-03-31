using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class ConvolverNodeTest : AudioNodeWithAudioNodeOptions<ConvolverNode, ConvolverOptions>
{
    public override async Task<ConvolverNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, ConvolverOptions? options)
        => await ConvolverNode.CreateAsync(JSRuntime, AudioContext, options);

    public override Dictionary<ChannelCountMode, Type> UnsupportedChannelCountModes => new()
    {
        [ChannelCountMode.Max] = typeof(NotSupportedErrorException)
    };
}
