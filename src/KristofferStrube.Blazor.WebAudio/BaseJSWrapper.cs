using KristofferStrube.Blazor.WebAudio.Converters;
using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

[JsonConverter(typeof(IJSWrapperConverter<BaseJSWrapper>))]
public abstract class BaseJSWrapper : IJSWrapper, IAsyncDisposable
{
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;

    public IJSRuntime JSRuntime { get; }
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
