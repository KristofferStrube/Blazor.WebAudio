using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The <see cref="Worklet"/> class provides the capability to add module scripts into its associated WorkletGlobalScopes.
/// The user agent can then create classes registered on the WorkletGlobalScopes and invoke their methods.
/// </summary>
/// <remarks><see href="https://html.spec.whatwg.org/multipage/worklets.html#worklet">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class Worklet : BaseJSWrapper, IJSCreatable<Worklet>
{
    /// <summary>
    /// An error handling reference to the underlying js object.
    /// </summary>
    protected IJSObjectReference ErrorHandlingJSReference { get; set; }

    /// <inheritdoc/>
    public static async Task<Worklet> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<Worklet> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new Worklet(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected Worklet(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        ErrorHandlingJSReference = new ErrorHandlingJSObjectReference(jSRuntime, jSReference);
    }

    /// <summary>
    /// Loads and executes the module script given by <paramref name="moduleURL"/> into all of worklet's global scopes.
    /// It can also create additional global scopes as part of this process, depending on the worklet type.
    /// The returned promise will fulfill once the script has been successfully loaded and run in all global scopes.
    /// </summary>
    /// <remarks>
    /// Throws an <see cref="AbortErrorException"/> if it fails to fetch the script.<br />
    /// Throws the exception thrown within the module itself if it fails.
    /// </remarks>
    /// <param name="moduleURL"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task AddModuleAsync(string moduleURL, WorkletOptions? options = null)
    {
        await ErrorHandlingJSReference.InvokeVoidAsync("addModule", moduleURL, options);
    }
}
