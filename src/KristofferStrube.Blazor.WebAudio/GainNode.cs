using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebAudio.Options;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class GainNode : AudioNode
{
    public static new Task<GainNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new GainNode(jSRuntime, jSReference));
    }

    public static async Task<GainNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, GainOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructGainNode", context.JSReference, options);
        return new GainNode(jSRuntime, jSInstance);
    }

    protected GainNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<AudioParam> GetGainAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "gain");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance);
    }
}
