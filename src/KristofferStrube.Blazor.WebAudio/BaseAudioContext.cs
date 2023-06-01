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
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSIntance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "destination");
        return await AudioDestinationNode.CreateAsync(JSRuntime, jSIntance);
    }

    public async Task<float> GetSampleRateAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<float>("getAttribute", JSReference, "sampleRate");
    }

    public async Task<double> GetCurrentTimeAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "currentTime");
    }

    public async Task<AudioListener> GetListenerAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSIntance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "listener");
        return await AudioListener.CreateAsync(JSRuntime, jSIntance);
    }

    public async Task<AudioContextState> GetStateAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        string state = await helper.InvokeAsync<string>("getAttribute", JSReference, "state");
        return AudioContextStateExtensions.Parse(state);
    }

    // TODO: We need to implement a wrapper for Worklet before we can add implement a wrapper for AudioWorklet.

    public async Task<EventListener<Event>> AddOnStateChangeEventListener(Func<Event, Task> eventHandler, AddEventListenerOptions? options = null)
    {
        EventListener<Event> eventListener = await EventListener<Event>.CreateAsync(JSRuntime, eventHandler);
        await AddEventListenerAsync("statechange", eventListener, options);
        return eventListener;
    }

    public async Task RemoveOnStateChangeEventListener(EventListener<Event> eventListener, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("statechange", eventListener, options);
    }

    public async Task<AnalyserNode> CreateAnalyserAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createAnalyser");
        return await AnalyserNode.CreateAsync(JSRuntime, jSInstance);
    }

    public async Task<GainNode> CreateGainAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createGain");
        return await GainNode.CreateAsync(JSRuntime, jSInstance);
    }
}
