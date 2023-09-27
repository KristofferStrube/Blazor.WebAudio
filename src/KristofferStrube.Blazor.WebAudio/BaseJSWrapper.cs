using KristofferStrube.Blazor.WebAudio.Converters;
using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// Base class for wrapping objects in the Blazor.WebAudio library.
/// </summary>
[JsonConverter(typeof(IJSWrapperConverter<BaseJSWrapper>))]
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

    /// <summary>
    /// Constructs a wrapper instance for an equivalent JS instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing JS instance that should be wrapped.</param>
    internal BaseJSWrapper(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        helperTask = new(jSRuntime.GetHelperAsync);
        JSReference = jSReference;
        JSRuntime = jSRuntime;
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
        GC.SuppressFinalize(this);
    }
}
