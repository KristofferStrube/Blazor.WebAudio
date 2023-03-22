using KristofferStrube.Blazor.WebAudio.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class OscillatorNode : AudioScheduledSourceNode
{
    public static async Task<OscillatorNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, OscillatorOptions? options = null)
    {
        var helper = await jSRuntime.GetHelperAsync();
        var jSInstance = options is null
            ? await helper.InvokeAsync<IJSObjectReference>("constructOcillatorNote", context.JSReference)
            : await helper.InvokeAsync<IJSObjectReference>("constructOcillatorNote", context.JSReference,
            new
            {
                type = options!.Type.AsString(),
                frequency = options!.Frequency,
                detune = options!.Detune,
                // Missing periodicWave
            });
        return new OscillatorNode(jSRuntime, jSInstance);
    }

    protected OscillatorNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
