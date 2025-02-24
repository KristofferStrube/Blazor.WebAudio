using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The interface represents the common features of source nodes such as <see cref="AudioBufferSourceNode"/>, <see cref="ConstantSourceNode"/>, and <see cref="OscillatorNode"/>.
/// Before a source is started (by calling <see cref="StartAsync(double)"/>, the source node will output silence (0).
/// After a source has been stopped (by calling <see cref="StopAsync(double)"/>), the source will then output silence (0).
/// <see cref="AudioScheduledSourceNode"/> cannot be instantiated directly, but is instead extended by the concrete interfaces for the source nodes.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioScheduledSourceNode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public abstract class AudioScheduledSourceNode : AudioNode
{
    /// <inheritdoc cref="IJSCreatable{AudioScheduledSourceNode}.CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected AudioScheduledSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for when the <see cref="AudioScheduledSourceNode"/> ends.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task AddOnEndedEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null)
    {
        await AddEventListenerAsync("ended", callback, options);
    }

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnEndedEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task RemoveOnEndedEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("ended", callback, options);
    }

    /// <summary>
    /// Schedules a sound to playback at an exact time.
    /// </summary>
    /// <param name="when">The when parameter describes at what time (in seconds) the sound should start playing. It is in the same time coordinate system as the AudioContext's currentTime attribute.</param>
    public async Task StartAsync(double when = 0)
    {
        await JSReference.InvokeVoidAsync("start", when);
    }

    /// <summary>
    /// Schedules a sound to stop playback at an exact time.
    /// </summary>
    /// <param name="when">The when parameter describes at what time (in seconds) the source should stop playing. It is in the same time coordinate system as the AudioContext's currentTime attribute.</param>
    public async Task StopAsync(double when = 0)
    {
        await JSReference.InvokeVoidAsync("stop", when);
    }
}
