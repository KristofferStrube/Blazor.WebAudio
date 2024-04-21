using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// Each <see cref="MessagePort"/> object can be entangled with another (a symmetric relationship).
/// Each <see cref="MessagePort"/> object also has a task source called the port message queue, initially empty.
/// A port message queue can be enabled or disabled, and is initially disabled.
/// Once enabled, a port can never be disabled again (though messages in the queue can get moved to another queue or removed altogether, which has much the same effect).
/// </summary>
/// <remarks><see href="https://html.spec.whatwg.org/multipage/web-messaging.html#messageport">See the API definition here</see>.</remarks>
public class MessagePort : EventTarget, IJSCreatable<MessagePort>
{
    /// <inheritdoc/>
    public static new async Task<MessagePort> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<MessagePort> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new MessagePort(jSRuntime, jSReference, options));
    }

    /// <inheritdoc/>
    protected internal MessagePort(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    public async Task PostMessageAsync(object message, ITransferable[]? transfer = null)
    {
        await JSReference.InvokeVoidAsync("postMessage", message, transfer?.Select(e => e.JSReference).ToArray());
    }

    public async Task StartAsync()
    {
        await JSReference.InvokeVoidAsync("start");
    }

    public async Task CloseAsync()
    {
        await JSReference.InvokeVoidAsync("close");
    }

    /// <inheritdoc/>
    public async Task AddOnMessageEventListenerAsync(EventListener<MessageEvent> callback, AddEventListenerOptions? options = null)
    {
        await AddEventListenerAsync("message", callback, options);
    }

    /// <inheritdoc/>
    public async Task RemoveOnMessageEventListenerAsync(EventListener<MessageEvent> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("message", callback, options);
    }
}
