using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents a set of <see cref="AudioNode"/> objects and their connections. It allows for arbitrary routing of signals to an <see cref="AudioDestinationNode"/>. Nodes are created from the context and are then connected together.<br />
/// BaseAudioContext is not instantiated directly, but is instead extended by the concrete interfaces <see cref="AudioContext"/> (for real-time rendering) and <see cref="OfflineAudioContext"/> (for offline rendering).
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#BaseAudioContext">See the API definition here</see>.</remarks>
public class BaseAudioContext : EventTarget
{
    /// <summary>
    /// A lazily evaluated task that gives access to helper methods for the Web Audio API.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> webAudioHelperTask;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="BaseAudioContext"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="BaseAudioContext"/>.</param>
    protected BaseAudioContext(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        webAudioHelperTask = new(jSRuntime.GetHelperAsync);
    }

    /// <summary>
    /// An <see cref="AudioDestinationNode"/> with a single input representing the final destination for all audio.
    /// </summary>
    /// <remarks>
    /// Usually this will represent the actual audio hardware.
    /// All <see cref="AudioNode"/>s actively rendering audio will directly or indirectly connect to destination.
    /// </remarks>
    /// <returns>An <see cref="AudioDestinationNode"/>.</returns>
    public async Task<AudioDestinationNode> GetDestinationAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSIntance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "destination");
        return await AudioDestinationNode.CreateAsync(JSRuntime, jSIntance);
    }

    /// <summary>
    /// The sample rate (in sample-frames per second) at which the <see cref="BaseAudioContext"/> handles audio.
    /// </summary>
    /// <remarks>
    /// It is assumed that all <see cref="AudioNode"/>s in the context run at this rate.
    /// In making this assumption, sample-rate converters or "varispeed" processors are not supported in real-time processing.
    /// The Nyquist frequency is half this sample-rate value.
    /// </remarks>
    /// <returns>The sample rate in frames/second.</returns>
    public async Task<float> GetSampleRateAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<float>("getAttribute", JSReference, "sampleRate");
    }

    /// <summary>
    /// This is the time in seconds of the sample frame immediately following the last sample-frame in the block of audio most recently processed by the context’s rendering graph.
    /// </summary>
    /// <remarks>
    /// If the context’s rendering graph has not yet processed a block of audio, then currentTime has a value of zero.
    /// Read more about the attribut from <see href="https://www.w3.org/TR/webaudio/#dom-baseaudiocontext-currenttime">the API specs</see>.
    /// </remarks>
    /// <returns>time in seconds since the first sample frame was processed by this <see cref="BaseAudioContext"/>.</returns>
    public async Task<double> GetCurrentTimeAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "currentTime");
    }

    /// <summary>
    /// An <see cref="AudioListener"/> which is used for 3D spatialization.
    /// </summary>
    /// <returns>An <see cref="AudioListener"/>.</returns>
    public async Task<AudioListener> GetListenerAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSIntance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "listener");
        return await AudioListener.CreateAsync(JSRuntime, jSIntance);
    }

    /// <summary>
    /// Describes the current state of the <see cref="BaseAudioContext"/>.
    /// </summary>
    /// <returns>The state represented as an <see cref="AudioContextState"/> enum value.</returns>
    public async Task<AudioContextState> GetStateAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        string state = await helper.InvokeAsync<string>("getAttribute", JSReference, "state");
        return AudioContextStateExtensions.Parse(state);
    }

    // TODO: We need to implement a wrapper for Worklet before we can add implement a wrapper for AudioWorklet.

    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for when the state of the <see cref="AudioContext"/> has changed (i.e. when the corresponding promise would have resolved).
    /// An event of type <see cref="Event"/> will be dispatched to the event handler, which can query the <see cref="AudioContext"/>’s state directly.
    /// A newly-created <see cref="AudioContext"/> will always begin in the <see cref="AudioContextState.Suspended"/> state, and a state change event will be fired whenever the state changes to a different state.
    /// This event is fired before the oncomplete event is fired.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task<EventListener<Event>> AddOnStateChangeEventListener(Func<Event, Task> callback, AddEventListenerOptions? options = null)
    {
        EventListener<Event> eventListener = await EventListener<Event>.CreateAsync(JSRuntime, callback);
        await AddEventListenerAsync("statechange", eventListener, options);
        return eventListener;
    }

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnStateChangeEventListener"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task RemoveOnStateChangeEventListener(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("statechange", callback, options);
    }

    /// <summary>
    /// Factory method for an <see cref="AnalyserNode"/>.
    /// </summary>
    /// <returns>An <see cref="AnalyserNode"/>.</returns>
    public async Task<AnalyserNode> CreateAnalyserAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createAnalyser");
        return await AnalyserNode.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Factory method for a <see cref="BiquadFilterNode"/> representing a second order filter which can be configured as one of several common filter types.
    /// </summary>
    /// <returns>A <see cref="BiquadFilterNode"/>.</returns>
    public async Task<BiquadFilterNode> CreateBiquadFilterAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createBiquadFilter");
        return await BiquadFilterNode.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Factory method for an <see cref="AudioBufferSourceNode"/>.
    /// </summary>
    /// <returns>An <see cref="AudioBufferSourceNode"/>.</returns>
    public async Task<AudioBufferSourceNode> CreateBufferSourceAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createBufferSource");
        return await AudioBufferSourceNode.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Factory method for a <see cref="ChannelMergerNode"/> representing a channel merger.
    /// </summary>
    /// <remarks>
    /// It will throw a <see cref="IndexSizeErrorException"/> if <paramref name="numberOfInputs"/> is less than 1 or is greater than the number of supported channels.
    /// </remarks>
    /// <exception cref="IndexSizeErrorException"></exception>
    /// <returns>A <see cref="ChannelMergerNode"/>.</returns>
    public async Task<ChannelMergerNode> CreateChannelMergerAsync(ulong numberOfInputs = 6)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createChannelMerger", numberOfInputs);
        return await ChannelMergerNode.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Factory method for a <see cref="ChannelSplitterNode"/> representing a channel splitter.
    /// </summary>
    /// <remarks>
    /// It will throw a <see cref="IndexSizeErrorException"/> if <paramref name="numberOfOutputs"/> is less than 1 or is greater than the number of supported channels.
    /// </remarks>
    /// <exception cref="IndexSizeErrorException"></exception>
    /// <returns>A <see cref="ChannelSplitterNode"/>.</returns>
    public async Task<ChannelSplitterNode> CreateChannelSplitterAsync(ulong numberOfOutputs = 6)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createChannelSplitter", numberOfOutputs);
        return await ChannelSplitterNode.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Factory method for a <see cref="ConstantSourceNode"/>.
    /// </summary>
    /// <returns>An <see cref="ConstantSourceNode"/></returns>
    public async Task<ConstantSourceNode> CreateConstantSourceAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createConstantSource");
        return await ConstantSourceNode.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Factory method for a <see cref="ConvolverNode"/>.
    /// </summary>
    /// <returns>An <see cref="ConvolverNode"/></returns>
    public async Task<ConvolverNode> CreateConvolverAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createConvolver");
        return await ConvolverNode.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Factory method for a <see cref="GainNode"/>.
    /// </summary>
    /// <returns>A <see cref="GainNode"/></returns>
    public async Task<GainNode> CreateGainAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createGain");
        return await GainNode.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Factory method for an <see cref="OscillatorNode"/>.
    /// </summary>
    /// <returns>An <see cref="OscillatorNode"/></returns>
    public async Task<OscillatorNode> CreateOscillatorAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createOscillator");
        return await OscillatorNode.CreateAsync(JSRuntime, jSInstance);
    }

    public async Task<AudioBuffer> DecodeAudioDataAsync(
        byte[] audioData
        //Func<AudioBuffer, Task>? successCallback = null,
        //Func<DOMException, Task>? errorCallbac = null)
        )
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference arrayBuffer = await helper.InvokeAsync<IJSObjectReference>("toArrayBuffer", audioData);
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("decodeAudioData", arrayBuffer);
        return await AudioBuffer.CreateAsync(JSRuntime, jSInstance);
    }
}
