using BlazorServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace IntegrationTests.Infrastructure;

public class AudioContextEvaluationContext(IJSRuntime jSRuntime) : EvaluationContext, IEvaluationContext<AudioContextEvaluationContext>
{
    public IJSRuntime JSRuntime => jSRuntime;

    public static AudioContextEvaluationContext Create(IServiceProvider provider)
    {
        IJSRuntime jSRuntime = provider.GetRequiredService<IJSRuntime>();

        return new AudioContextEvaluationContext(jSRuntime);
    }

    public async Task<AudioContext> GetAudioContext()
    {
        AudioContext audioContext = await AudioContext.CreateAsync(jSRuntime);
        return audioContext;
    }
}
