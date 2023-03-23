using KristofferStrube.Blazor.WebAudio.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class AudioContext : BaseAudioContext
{
    // Todo: Add AudioContextOptions parameter
    public static async Task<AudioContext> CreateAsync(IJSRuntime jSRuntime, object? contextOptions = null)
    {
        var helper = await jSRuntime.GetHelperAsync();
        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructAudioContext", contextOptions);
        return new AudioContext(jSRuntime, jSInstance);
    }

    protected AudioContext(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task<double> GetCurrentTimeAsync()
    {
        var helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "currentTime");
    }
}
