using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebAudio.Events;
using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="OfflineAudioContext"/> is a particular type of <see cref="BaseAudioContext"/> for rendering/mixing-down (potentially) faster than real-time.
/// It does not render to the audio hardware, but instead renders as quickly as possible, fulfilling the returned promise with the rendered result as an <see cref="AudioBuffer"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#offlineaudiocontext">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class OfflineAudioContext : BaseAudioContext, IJSCreatable<OfflineAudioContext>
{
    /// <summary>
    /// An error handling reference to the underlying js object.
    /// </summary>
    protected IJSObjectReference ErrorHandlingJSReference { get; set; }

    /// <inheritdoc/>
    public static new async Task<OfflineAudioContext> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<OfflineAudioContext> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new OfflineAudioContext(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Creates an <see cref="OfflineAudioContext"/> using the standard constructor.
    /// </summary>
    /// <remarks>
    /// Throws an <see cref="InvalidStateErrorException"/> if the page hasn't loaded yet.<br />
    /// It throws an <see cref="NotSupportedErrorException"/> if any of the arguments are negative, zero, or outside their nominal range.
    /// </remarks>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="contextOptions">The initial parameters needed to construct this context.</param>
    /// <exception cref="InvalidStateErrorException"></exception>
    /// <exception cref="NotSupportedErrorException"></exception>
    public static async Task<OfflineAudioContext> CreateAsync(IJSRuntime jSRuntime, OfflineAudioContextOptions contextOptions)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        ErrorHandlingJSObjectReference errorHandlingHelper = new(jSRuntime, helper);
        IJSObjectReference jSInstance = await errorHandlingHelper.InvokeAsync<IJSObjectReference>("constructOfflineAudioContext", contextOptions);
        return new OfflineAudioContext(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Creates an <see cref="OfflineAudioContext"/> using the standard constructor.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="InvalidStateErrorException"/> if the page hasn't loaded yet.<br />
    /// It throws an <see cref="NotSupportedErrorException"/> if any of the arguments are negative, zero, or outside their nominal range.
    /// </remarks>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="numberOfChannels">Determines how many channels the buffer will have. Browsers will support at least 32 channels.</param>
    /// <param name="length">Determines the size of the buffer in sample-frames.</param>
    /// <param name="sampleRate">Describes the sample-rate of the linear PCM audio data in the buffer in sample-frames per second. The range is at least from <c>8000</c> to <c>96000</c> but browsers can support broader ranges.</param>
    /// <exception cref="InvalidStateErrorException"></exception>
    /// <exception cref="NotSupportedErrorException"></exception>
    public static async Task<OfflineAudioContext> CreateAsync(IJSRuntime jSRuntime, ulong numberOfChannels, ulong length, float sampleRate)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        ErrorHandlingJSObjectReference errorHandlingHelper = new(jSRuntime, helper);
        IJSObjectReference jSInstance = await errorHandlingHelper.InvokeAsync<IJSObjectReference>("constructOfflineAudioContextWithThreeParameters", numberOfChannels, length, sampleRate);
        return new OfflineAudioContext(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected OfflineAudioContext(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        ErrorHandlingJSReference = new ErrorHandlingJSObjectReference(jSRuntime, jSReference);
    }

    /// <summary>
    /// Given the current connections and scheduled changes, starts rendering audio.
    /// </summary>
    /// <remarks>
    /// Although the primary method of getting the rendered audio data is via its promise return value, the instance will also dispatch an event with the rendered audio to the listeners created via <see cref="AddOnCompleteEventListener"/> for legacy reasons.<br />
    /// It throws an <see cref="InvalidStateErrorException"/> if the page hasn't loaded yet.<br />
    /// It throws an <see cref="InvalidStateErrorException"/> if rendering has already been started.
    /// </remarks>
    /// <exception cref="InvalidStateErrorException"></exception>
    /// <returns>The rendered audio.</returns>
    public async Task<AudioBuffer> StartRenderingAsync()
    {
        IJSObjectReference jSInstance = await ErrorHandlingJSReference.InvokeAsync<IJSObjectReference>("startRendering");
        return await AudioBuffer.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Resumes the progression of the <see cref="OfflineAudioContext"/>'s <see cref="BaseAudioContext.GetCurrentTimeAsync"/> when it has been suspended.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="InvalidStateErrorException"/> if the page hasn't loaded yet.<br  />
    /// It throws an <see cref="InvalidStateErrorException"/> if the <see cref="OfflineAudioContext"/> has been closed.<br />
    /// It throws an <see cref="InvalidStateErrorException"/> if rendering hasn't been started.
    /// </remarks>
    /// <exception cref="InvalidStateErrorException"></exception>
    public async Task ResumeAsync()
    {
        await ErrorHandlingJSReference.InvokeVoidAsync("resume");
    }

    /// <summary>
    /// Schedules a suspension of the time progression in the audio context at the specified time.
    /// This is generally useful when manipulating the audio graph synchronously on <see cref="OfflineAudioContext"/>.
    /// </summary>
    /// <remarks>
    /// Note that the maximum precision of suspension is the size of the render quantum and the specified suspension time will be rounded up to the nearest render quantum boundary.
    /// For this reason, it is not allowed to schedule multiple suspends at the same quantized frame.
    /// Also, scheduling should be done while the context is not running to ensure precise suspension.<br />
    /// It throws an <see cref="InvalidStateErrorException"/> if <paramref name="suspendTime"/> is negative, is less than or equals to the curren time, is greater than or equal to the total render duration, or is scheduled by another suspend for the same time.
    /// </remarks>
    /// <param name="suspendTime">Schedules a suspension of the rendering at the specified time, which is quantized and rounded up to the render quantum size.</param>
    /// <returns></returns>
    public async Task SuspendAsync(double suspendTime)
    {
        await ErrorHandlingJSReference.InvokeVoidAsync("suspend", suspendTime);
    }

    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for the completion event.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task<EventListener<OfflineAudioCompletionEvent>> AddOnCompleteEventListener(Func<OfflineAudioCompletionEvent, Task> callback, AddEventListenerOptions? options = null)
    {
        EventListener<OfflineAudioCompletionEvent> eventListener = await EventListener<OfflineAudioCompletionEvent>.CreateAsync(JSRuntime, callback);
        await AddEventListenerAsync("complete", eventListener, options);
        return eventListener;
    }

    /// <summary>
    /// The size of the buffer in sample-frames. This is the same as the value of the length parameter for the constructor.
    /// </summary>
    /// <returns></returns>
    public async Task<ulong> GetLengthAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "length");
    }

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnCompleteEventListener"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task RemoveOnCompleteEventListener(EventListener<OfflineAudioCompletionEvent> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("complete", callback, options);
    }
}
