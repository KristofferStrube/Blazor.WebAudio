using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebAudio.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class BaseAudioContext : EventTarget
{
    protected readonly Lazy<Task<IJSObjectReference>> webAudioHelperTask;
    protected BaseAudioContext(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        webAudioHelperTask = new(jSRuntime.GetHelperAsync);
    }

    public async Task<AudioDestinationNode> GetDestinationAsync()
    {
        var helper = await helperTask.Value;
        var jSIntance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "destination");
        return await AudioDestinationNode.CreateAsync(JSRuntime, jSIntance);
    }
}
