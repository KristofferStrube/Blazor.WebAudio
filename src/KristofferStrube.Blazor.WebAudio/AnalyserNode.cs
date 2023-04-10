using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class AnalyserNode : AudioNode
{
    public new static Task<AnalyserNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AnalyserNode(jSRuntime, jSReference));
    }

    public static async Task<AnalyserNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, AnalyserOptions? options = null)
    {
        var helper = await jSRuntime.GetHelperAsync();
        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructAnalyzerNode", context.JSReference, options);
        return new AnalyserNode(jSRuntime, jSInstance);
    }

    protected AnalyserNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task GetByteTimeDomainDataAsync(Uint8Array array)
    {
        await JSReference.InvokeVoidAsync("getByteTimeDomainData", array.JSReference);
    }

    public async Task<ulong> GetFrequencyBinCountAsync()
    {
        var helper = await helperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "frequencyBinCount");
    }
}
