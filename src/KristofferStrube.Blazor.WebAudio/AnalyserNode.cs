using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class AnalyserNode : AudioNode
{
    public static new Task<AnalyserNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AnalyserNode(jSRuntime, jSReference));
    }

    public static async Task<AnalyserNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, AnalyserOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructAnalyzerNode", context.JSReference, options);
        return new AnalyserNode(jSRuntime, jSInstance);
    }

    protected AnalyserNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task GetByteFrequencyDataAsync(Uint8Array array)
    {
        await JSReference.InvokeVoidAsync("getByteFrequencyData", array.JSReference);
    }

    public async Task GetByteTimeDomainDataAsync(Uint8Array array)
    {
        await JSReference.InvokeVoidAsync("getByteTimeDomainData", array.JSReference);
    }

    public async Task<ulong> GetFrequencyBinCountAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "frequencyBinCount");
    }
}
