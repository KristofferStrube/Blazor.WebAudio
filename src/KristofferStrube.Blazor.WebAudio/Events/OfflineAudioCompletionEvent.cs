using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio.Events;

/// <summary>
/// This is an <see cref="Event"/> object which is dispatched to <see cref="OfflineAudioContext"/> for legacy reasons.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#OfflineAudioCompletionEvent">See the API definition here</see>.</remarks>
public class OfflineAudioCompletionEvent : Event, IJSCreatable<OfflineAudioCompletionEvent>
{
    /// <summary>
    /// A lazily evaluated task that gives access to helper methods for the Web Audio API.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> webAudioHelperTask;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="OfflineAudioCompletionEvent"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="OfflineAudioCompletionEvent"/>.</param>
    /// <returns>A wrapper instance for a <see cref="OfflineAudioCompletionEvent"/>.</returns>
    public static new Task<OfflineAudioCompletionEvent> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new OfflineAudioCompletionEvent(jSRuntime, jSReference));
    }


    /// <summary>
    /// Creates an <see cref="OfflineAudioCompletionEvent"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="type"><inheritdoc cref="Event.CreateAsync(IJSRuntime, string, EventInit?)" path="/param[@name='type']"/></param>
    /// <param name="eventInitDict"><inheritdoc cref="Event.CreateAsync(IJSRuntime, string, EventInit?)" path="/param[@name='eventInitDict']"/></param>
    /// <returns></returns>
    public static async Task<OfflineAudioCompletionEvent> CreateAsync(IJSRuntime jSRuntime, string type, OfflineAudioCompletionEventInit eventInitDict)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructOfflineAudioCompletionEvent", type, eventInitDict);
        return new OfflineAudioCompletionEvent(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="OfflineAudioCompletionEvent"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="OfflineAudioCompletionEvent"/>.</param>
    protected OfflineAudioCompletionEvent(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        webAudioHelperTask = new(jSRuntime.GetHelperAsync);
    }

    /// <summary>
    /// An <see cref="AudioBuffer"/> containing the rendered audio data.
    /// </summary>
    public async Task<AudioBuffer> GetRenderedBufferAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "renderedBuffer");
        return await AudioBuffer.CreateAsync(JSRuntime, jSInstance);
    }
}
