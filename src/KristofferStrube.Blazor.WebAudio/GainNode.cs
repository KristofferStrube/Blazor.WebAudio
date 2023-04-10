using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebAudio.Options;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class GainNode : AudioNode
{
    public new static Task<GainNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new GainNode(jSRuntime, jSReference));
    }

    public static async Task<GainNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, GainOptions? options = null)
    {
        var helper = await jSRuntime.GetHelperAsync();
        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructGainNode", context.JSReference, options);
        return new GainNode(jSRuntime, jSInstance);
    }

    protected GainNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<AudioParam> GetGainAsync()
    {
        var helper = await webAudioHelperTask.Value;
        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "gain");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance);
    }
}
