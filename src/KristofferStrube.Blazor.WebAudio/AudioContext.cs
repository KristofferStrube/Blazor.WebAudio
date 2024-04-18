using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.MediaCaptureStreams;
using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents an audio graph whose <see cref="AudioDestinationNode"/> is routed to a real-time output device that produces a signal directed at the user.
/// In most use cases, only a single <see cref="AudioContext"/> is used per document.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioContext">See the API definition here</see>.</remarks>
public class AudioContext : BaseAudioContext, IJSCreatable<AudioContext>
{
    /// <inheritdoc/>
    public static new async Task<AudioContext> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<AudioContext> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new AudioContext(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Creates an <see cref="AudioContext"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="contextOptions">User-specified options controlling how the <see cref="AudioContext"/> should be constructed.</param>
    /// <returns>A new instance of an <see cref="AudioContext"/>.</returns>
    public static async Task<AudioContext> CreateAsync(IJSRuntime jSRuntime, AudioContextOptions? contextOptions = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructAudioContext", contextOptions);
        return new AudioContext(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected AudioContext(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    /// <summary>
    /// This represents the number of seconds of processing latency incurred by the <see cref="AudioContext"/> passing the audio from the <see cref="AudioDestinationNode"/> to the audio subsystem.
    /// It does not include any additional latency that might be caused by any other processing between the output of the <see cref="AudioDestinationNode"/> and the audio hardware and specifically does not include any latency incurred the audio graph itself.
    /// </summary>
    /// <remarks>
    /// For example, if the audio context is running at <c>44.1 kHz</c> and the <see cref="AudioDestinationNode"/> implements double buffering internally and can process and output audio each render quantum, then the processing latency is <c>(2*128)/44100=5.805ms</c>, approximately.
    /// </remarks>
    public async Task<double> GetBaseLatencyAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "baseLatency");
    }

    /// <summary>
    /// The estimation in seconds of audio output latency, i.e., the interval between the time the UA requests the host system to play a buffer and the time at which the first sample in the buffer is actually processed by the audio output device.
    /// For devices such as speakers or headphones that produce an acoustic signal, this latter time refers to the time when a sample’s sound is produced.
    /// </summary>
    /// <remarks>
    /// The value of <see cref="GetOutputLatencyAsync"/> depends on the platform and the connected hardware audio output device.
    /// The value of <see cref="GetOutputLatencyAsync"/> does not change for the context’s lifetime as long as the connected audio output device remains the same.
    /// If the audio output device is changed the outputLatency attribute value will be updated accordingly.
    /// </remarks>
    public async Task<double> GetOutputLatencyAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "outputLatency");
    }

    /// <summary>
    /// Returns a new <see cref="AudioTimestamp"/> instance containing two related audio stream position values for the context: <see cref="AudioTimestamp.ContextTime"/> contains the time of the sample frame which is currently being rendered by the audio output device (i.e., output audio stream position), in the same units and origin as context’s currentTime; the <see cref="AudioTimestamp.PerformanceTime"/> contains the time estimating the moment when the sample frame corresponding to the stored <see cref="AudioTimestamp.ContextTime"/> value was rendered by the audio output device, in the same units and origin as performance.now() (described in the <see href="https://www.w3.org/TR/hr-time-3/">High Resolution Time</see> standard)
    /// </summary>
    /// <remarks>
    /// If the context’s rendering graph has not yet processed a block of audio, then it returns an <see cref="AudioTimestamp"/> instance with both members containing zero.<br />
    /// After the context’s rendering graph has started processing of blocks of audio, its <see cref="BaseAudioContext.GetCurrentTimeAsync"/> always exceeds the <see cref="AudioTimestamp.ContextTime"/> obtained from this method.
    /// </remarks>
    /// <returns></returns>
    public async Task<AudioTimestamp> GetOutputTimestampAsync()
    {
        return await JSReference.InvokeAsync<AudioTimestamp>("getOutputTimestamp");
    }

    /// <summary>
    /// Resumes the progression of the <see cref="AudioContext"/>'s currentTime when it has been suspended.
    /// </summary>
    public async Task ResumeAsync()
    {
        await JSReference.InvokeVoidAsync("resume");
    }

    /// <summary>
    /// Suspends the progression of <see cref="AudioContext"/>'s currentTime, allows any current context processing blocks that are already processed to be played to the destination, and then allows the system to release its claim on audio hardware. 
    /// </summary>
    /// <remarks>
    /// This is generally useful when the application knows it will not need the <see cref="AudioContext"/> for some time, and wishes to temporarily release system resource associated with the <see cref="AudioContext"/>.
    /// </remarks>
    public async Task SuspendAsync()
    {
        await JSReference.InvokeVoidAsync("suspend");
    }

    /// <summary>
    /// Closes the AudioContext, releasing the system resources being used.
    /// </summary>
    /// <remarks>
    /// This will not automatically release all <see cref="AudioContext"/>-created objects, but will suspend the progression of the <see cref="AudioContext"/>'s currentTime, and stop processing audio data.
    /// </remarks>
    public async Task CloseAsync()
    {
        await JSReference.InvokeVoidAsync("close");
    }

    /// <summary>
    /// Creates a <see cref="MediaElementAudioSourceNode"/> given an HTMLMediaElement.
    /// As a consequence of calling this method, audio playback from the HTMLMediaElement will be re-routed into the processing graph of the <see cref="AudioContext"/>.
    /// </summary>
    /// <param name="mediaElement">The media element that will be re-routed.</param>
    /// <returns>A new <see cref="MediaElementAudioSourceNode"/>.</returns>
    public async Task<MediaElementAudioSourceNode> CreateMediaElementSourceAsync(EventTarget mediaElement)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createMediaElementSource", mediaElement.JSReference);
        return await MediaElementAudioSourceNode.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Creates a <see cref="MediaStreamAudioSourceNode"/>.
    /// </summary>
    /// <param name="mediaStream">The media stream that will act as source.</param>
    /// <returns>A new <see cref="MediaStreamAudioSourceNode"/>.</returns>
    public async Task<MediaStreamAudioSourceNode> CreateMediaStreamSourceAsync(MediaStream mediaStream)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createMediaStreamSource", mediaStream.JSReference);
        return await MediaStreamAudioSourceNode.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Creates a <see cref="MediaStreamTrackAudioSourceNode"/>.
    /// </summary>
    /// <remarks>
    /// It will throw an <see cref="InvalidStateErrorException"/> if the kind of the <paramref name="mediaStreamTrack"/> is not <see cref="MediaStreamTrackKind.Audio"/>.
    /// </remarks>
    /// <param name="mediaStreamTrack">The <see cref="MediaStreamTrack"/> that will act as source.</param>
    /// <exception cref="InvalidStateErrorException"></exception>
    /// <returns>A new <see cref="MediaStreamTrackAudioSourceNode"/>.</returns>
    public async Task<MediaStreamTrackAudioSourceNode> CreateMediaStreamTrackSourceAsync(MediaStreamTrack mediaStreamTrack)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createMediaStreamTrackSource", mediaStreamTrack.JSReference);
        return await MediaStreamTrackAudioSourceNode.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Creates a <see cref="MediaStreamAudioDestinationNode"/>.
    /// </summary>
    /// <returns>A new <see cref="MediaStreamAudioDestinationNode"/>.</returns>
    public async Task<MediaStreamAudioDestinationNode> CreateMediaStreamDestinationAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createMediaStreamDestination");
        return await MediaStreamAudioDestinationNode.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }
}
