using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// Base class for wrapping objects in the Blazor.WebAudio library.
/// </summary>
[IJSWrapperConverter]
public abstract class BaseJSWrapper : IJSWrapper, IAsyncDisposable
{
    /// <summary>
    /// A lazily evaluated task that gives access to helper methods.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;

    /// <inheritdoc/>
    public IJSRuntime JSRuntime { get; }

    /// <inheritdoc/>
    public IJSObjectReference JSReference { get; }

    /// <inheritdoc/>
    public bool DisposesJSReference { get; }

    /// <inheritdoc cref="IJSCreatable{T}.CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    internal BaseJSWrapper(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        helperTask = new(jSRuntime.GetHelperAsync);
        JSReference = jSReference;
        JSRuntime = jSRuntime;
        DisposesJSReference = options.DisposesJSReference;
    }

    /// <summary>
    /// Disposes the underlying js object reference.
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        if (helperTask.IsValueCreated)
        {
            IJSObjectReference module = await helperTask.Value;
            await module.DisposeAsync();
        }
        await IJSWrapper.DisposeJSReference(this);
        GC.SuppressFinalize(this);
    }
}
