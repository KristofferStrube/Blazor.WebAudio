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

    public async Task<float> GetSampleRateAsync()
    {
        var helper = await helperTask.Value;
        return await helper.InvokeAsync<float>("getAttribute", JSReference, "sampleRate");
    }

    public async Task<double> GetCurrentTimeAsync()
    {
        var helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "currentTime");
    }

    public async Task<AudioListener> GetListenerAsync()
    {
        var helper = await helperTask.Value;
        var jSIntance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "listener");
        return await AudioListener.CreateAsync(JSRuntime, jSIntance);
    }

    public async Task<AudioContextState> GetStateAsync()
    {
        var helper = await helperTask.Value;
        var state = await helper.InvokeAsync<string>("getAttribute", JSReference, "state");
        return AudioContextStateExtensions.Parse(state);
    }

    // TODO: We need to implement a wrapper for Worklet before we can add implement a wrapper for AudioWorklet.

    public async Task<EventListener<Event>> AddOnStateChangeEventListener(Func<Event, Task> eventHandler, AddEventListenerOptions? options = null)
    {
        var eventListener = await EventListener<Event>.CreateAsync(JSRuntime, eventHandler);
        await AddEventListenerAsync("statechange", eventListener, options);
        return eventListener;
    }

    public async Task RemoveOnStateChangeEventListener(EventListener<Event> eventListener, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("statechange", eventListener, options);
    }
}
