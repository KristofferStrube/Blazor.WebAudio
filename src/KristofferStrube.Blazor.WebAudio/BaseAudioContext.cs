using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
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
    /// Factory method for an <see cref="AudioBuffer"/>.
    /// </summary>
    /// <param name="numberOfChannels">Determines how many channels the buffer will have. An implementation will support at least 32 channels.</param>
    /// <param name="length">Determines the size of the buffer in sample-frames. This must be at least 1.</param>
    /// <param name="sampleRate">Describes the sample-rate of the linear PCM audio data in the buffer in sample-frames per second. An implementation will support sample rates in at least the range <c>8000</c> to <c>96000</c>.</param>
    /// <returns>An <see cref="AudioBuffer"/>.</returns>
    public async Task<AudioBuffer> CreateBufferAsync(ulong numberOfChannels, ulong length, float sampleRate)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createBuffer", numberOfChannels, length, sampleRate);
        return await AudioBuffer.CreateAsync(JSRuntime, jSInstance);
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
    /// It will throw an <see cref="IndexSizeErrorException"/> if <paramref name="numberOfInputs"/> is less than 1 or is greater than the number of supported channels.
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
    /// It will throw an <see cref="IndexSizeErrorException"/> if <paramref name="numberOfOutputs"/> is less than 1 or is greater than the number of supported channels.
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
    /// Factory method for a <see cref="DelayNode"/>. The initial default delay time will be <c>0</c> seconds.
    /// </summary>
    /// <param name="maxDelayTime">
    /// Specifies the maximum delay time in seconds allowed for the delay line.
    /// If specified, this value must be greater than zero and less than three minutes or a <see cref="NotSupportedErrorException"/> exception will be thrown.
    /// If not specified, then <c>1</c> will be used.
    /// </param>
    /// <exception cref="NotSupportedErrorException"></exception>
    /// <returns>An <see cref="DelayNode"/></returns>
    public async Task<DelayNode> CreateDelayAsync(double maxDelayTime = 1)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createDelay", maxDelayTime);
        return await DelayNode.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Factory method for a <see cref="DynamicsCompressorNode"/>.
    /// </summary>
    /// <returns>An <see cref="DynamicsCompressorNode"/></returns>
    public async Task<DynamicsCompressorNode> CreateDynamicsCompressorAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createDynamicsCompressor");
        return await DynamicsCompressorNode.CreateAsync(JSRuntime, jSInstance);
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
    /// Factory method for an <see cref="IIRFilterNode"/>.
    /// </summary>
    /// <param name="feedforward">
    /// An array of the feedforward (numerator) coefficients for the transfer function of the IIR filter. The maximum length of this array is <c>20</c>.<br/>
    /// If all of the values are zero, an <see cref="InvalidStateErrorException"/> will be thrown. A <see cref="NotSupportedErrorException"/> will be thrown if the array length is <c>0</c> or greater than <c>20</c>.
    /// </param>
    /// <param name="feedback">
    /// An array of the feedback (denominator) coefficients for the transfer function of the IIR filter. The maximum length of this array is <c>20</c>.<br />
    /// If the first element of the array is <c>0</c>, an <see cref="InvalidStateErrorException"/> will be thrown. A <see cref="NotSupportedErrorException"/> will be thrown if the array length is <c>0</c> or greater than <c>20</c>.
    /// </param>
    /// <exception cref="InvalidStateErrorException"></exception>
    /// <exception cref="NotSupportedErrorException"></exception>
    /// <returns>An <see cref="IIRFilterNode"/></returns>
    public async Task<IIRFilterNode> CreateIIRFilterAsync(double[] feedforward, double[] feedback)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createIIRFilter", feedforward, feedback);
        return await IIRFilterNode.CreateAsync(JSRuntime, jSInstance);
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

    /// <summary>
    /// Factory method for a <see cref="PannerNode"/>.
    /// </summary>
    /// <returns>A <see cref="PannerNode"/></returns>
    public async Task<PannerNode> CreatePannerAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createPanner");
        return await PannerNode.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Factory method for a <see cref="PeriodicWave"/>.
    /// </summary>
    /// <param name="real">A sequence of cosine parameters. See its <see cref="PeriodicWaveOptions.Real"/> constructor argument for a more detailed description.</param>
    /// <param name="imag">A sequence of sine parameters. See its <see cref="PeriodicWaveOptions.Imag"/> constructor argument for a more detailed description.</param>
    /// <param name="constraints">If not given, the waveform is normalized. Otherwise, the waveform is normalized according the value given by <paramref name="constraints"/>.</param>
    /// <returns>A <see cref="PeriodicWave"/></returns>
    public async Task<PeriodicWave> CreatePeriodicWaveAsync(float[] real, float[] imag, PeriodicWaveConstraints constraints = default!)
    {
        constraints ??= new();
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createPeriodicWave", real, imag, constraints);
        return await PeriodicWave.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Factory method for a <see cref="StereoPannerNode"/>.
    /// </summary>
    /// <returns>A <see cref="StereoPannerNode"/></returns>
    public async Task<StereoPannerNode> CreateStereoPannerAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createStereoPanner");
        return await StereoPannerNode.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Factory method for a <see cref="WaveShaperNode"/>.
    /// </summary>
    /// <returns>A <see cref="WaveShaperNode"/></returns>
    public async Task<WaveShaperNode> CreateWaveShaperAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createWaveShaper");
        return await WaveShaperNode.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Asynchronously decodes the audio file data contained in the <paramref name="audioData"/>.
    /// The <paramref name="audioData"/> can, for example, be loaded from a <see cref="HttpClient"/> by calling <see cref="HttpClient.GetByteArrayAsync(string?)"/>.
    /// Audio file data can be in any of the formats supported by the audio element.
    /// The buffer passed to <see cref="DecodeAudioDataAsync(byte[], Func{AudioBuffer, Task}?, Func{DOMException, Task}?)"/> has its content-type determined by sniffing, as described in <see href="https://mimesniff.spec.whatwg.org/">MIME Sniffing Standard</see>.<br />
    /// Although the primary method of interfacing with this function is via its <see cref="Task"/> return value, the callback parameters are provided for legacy reasons.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="InvalidStateErrorException"/> if the document was not fully loaded when this method is invoked.<br />
    /// It throws an <see cref="DataCloneErrorException"/> if the array is actually <see langword="null"/>.<br />
    /// It throws an <see cref="EncodingErrorException"/> if <see href="https://mimesniff.spec.whatwg.org/#matching-an-audio-or-video-type-pattern">MIME Sniffing §6.2 Matching an audio or video type pattern</see> doesn't find a MIME type or if it failed to decode it into a linear PCM (pulse code modulation).<br />
    /// </remarks>
    /// <param name="audioData">An  <see cref="T:byte[]" /> containing compressed audio data.</param>
    /// <param name="successCallback">A callback function which will be invoked when the decoding is finished. The single argument to this callback is an <see cref="AudioBuffer"/> representing the decoded PCM audio data.</param>
    /// <param name="errorCallback">A callback function which will be invoked if there is an error decoding the audio file.</param>
    /// <returns>A new <see cref="AudioBuffer"/>.</returns>
    public async Task<AudioBuffer> DecodeAudioDataAsync(
        byte[] audioData,
        Func<AudioBuffer, Task>? successCallback = null,
        Func<DOMException, Task>? errorCallback = null)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference arrayBuffer = await helper.InvokeAsync<IJSObjectReference>("toArrayBuffer", audioData);

        return await DecodeAudioDataAsync(arrayBuffer, successCallback, errorCallback);
    }

    /// <summary>
    /// Asynchronously decodes the audio file data contained in the <paramref name="audioData"/>.
    /// The <paramref name="audioData"/> can, for example, be loaded from a <see cref="HttpClient"/> by calling <see cref="HttpClient.GetByteArrayAsync(string?)"/>.
    /// Audio file data can be in any of the formats supported by the audio element.
    /// The buffer passed to <see cref="DecodeAudioDataAsync(IJSObjectReference, Func{AudioBuffer, Task}?, Func{DOMException, Task}?)"/> has its content-type determined by sniffing, as described in <see href="https://mimesniff.spec.whatwg.org/">MIME Sniffing Standard</see>.<br />
    /// Although the primary method of interfacing with this function is via its <see cref="Task"/> return value, the callback parameters are provided for legacy reasons.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="InvalidStateErrorException"/> if the document was not fully loaded when this method is invoked.<br />
    /// It throws an <see cref="DataCloneErrorException"/> if the array is detached.<br />
    /// It throws an <see cref="EncodingErrorException"/> if <see href="https://mimesniff.spec.whatwg.org/#matching-an-audio-or-video-type-pattern">MIME Sniffing §6.2 Matching an audio or video type pattern</see> doesn't find a MIME type or if it failed to decode it into a linear PCM (pulse code modulation).<br />
    /// </remarks>
    /// <param name="audioData">An <see cref="IJSObjectReference"/> to an existing AudioBuffer containing compressed audio data.</param>
    /// <param name="successCallback">A callback function which will be invoked when the decoding is finished. The single argument to this callback is an <see cref="AudioBuffer"/> representing the decoded PCM audio data.</param>
    /// <param name="errorCallback">A callback function which will be invoked if there is an error decoding the audio file.</param>
    /// <returns>A new <see cref="AudioBuffer"/>.</returns>
    public async Task<AudioBuffer> DecodeAudioDataAsync(
        IJSObjectReference audioData,
        Func<AudioBuffer, Task>? successCallback = null,
        Func<DOMException, Task>? errorCallback = null)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        if (ErrorHandlingJSInterop.ErrorHandlingJSInteropHasBeenSetup)
        {
            helper = new ErrorHandlingJSObjectReference(JSRuntime, helper);
        }

        DotNetObjectReference<DecodeSuccessCallback>? successCallbackObjRef = successCallback is null ? null : DotNetObjectReference.Create(new DecodeSuccessCallback(JSRuntime, successCallback));
        DotNetObjectReference<DecodeErrorCallback>? errorCallbackObjRef = errorCallback is null ? null : DotNetObjectReference.Create(new DecodeErrorCallback(JSRuntime, errorCallback));
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("decodeAudioData", JSReference, audioData, successCallbackObjRef, errorCallbackObjRef);

        return await AudioBuffer.CreateAsync(JSRuntime, jSInstance);
    }

    private record DecodeSuccessCallback(IJSRuntime JSRuntime, Func<AudioBuffer, Task> SuccessCallback)
    {
        [JSInvokable]
        public async Task Invoke(IJSObjectReference jsDecodedData)
        {
            await SuccessCallback(await AudioBuffer.CreateAsync(JSRuntime, jsDecodedData));
        }
    };

    private record DecodeErrorCallback(IJSRuntime JSRuntime, Func<DOMException, Task> ErrorCallback)
    {
        [JSInvokable]
        public async Task Invoke(JSError jSError)
        {
            if (ErrorMappers.Default[jSError.Name](jSError) is DOMException dOMException)
            {
                await ErrorCallback(dOMException);
            }
        }
    };
}
