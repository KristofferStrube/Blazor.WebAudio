using KristofferStrube.Blazor.WebAudio.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class AnalyzerNode : AudioNode
{
    public new static Task<AnalyzerNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AnalyzerNode(jSRuntime, jSReference));
    }

    public static async Task<AnalyzerNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, AnalyserOptions? options = null)
    {
        var helper = await jSRuntime.GetHelperAsync();
        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructAnalyzerNode", context.JSReference, options);
        return new AnalyzerNode(jSRuntime, jSInstance);
    }

    protected AnalyzerNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    // We need to extend Blazor.WebIDL to support Uint8Array as a reference type.
    //public async Task GetByteTimeDomainDataAsync(Uint8Array array)
}
