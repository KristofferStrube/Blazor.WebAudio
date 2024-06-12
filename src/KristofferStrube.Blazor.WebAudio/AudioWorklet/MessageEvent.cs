using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// Messages in server-sent events, cross-document messaging, channel messaging, broadcast channels, and WebSockets use the <see cref="MessageEvent"/> interface for their message events.
/// </summary>
/// <remarks><see href="https://html.spec.whatwg.org/#messageevent">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class MessageEvent : Event, IJSCreatable<MessageEvent>
{
    /// <summary>
    /// A lazily evaluated task that gives access to helper methods for the Web Audio API.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> webAudioHelperTask;

    /// <inheritdoc/>
    public static new async Task<MessageEvent> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<MessageEvent> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new MessageEvent(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected MessageEvent(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        webAudioHelperTask = new(jSRuntime.GetHelperAsync);
    }

    /// <summary>
    /// Gets the message being sent as an <see cref="IJSObjectReference"/>.
    /// </summary>
    /// <returns></returns>
    public async Task<IJSObjectReference> GetDataAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "data");
    }

    /// <summary>
    /// Gets the message being sent as any type you want.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<T> GetDataAsync<T>()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<T>("getAttribute", JSReference, "data");
    }

    /// <summary>
    /// Gets the message being sent as a <see cref="ValueReference"/> that can be used to check the concrete underlying type.
    /// </summary>
    /// <returns></returns>
    public ValueReference Data => new(JSRuntime, JSReference, "data");

    /// <summary>
    /// Gets the origin of the message, for server-sent events and cross-document messaging.
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetOriginAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "origin");
    }
}