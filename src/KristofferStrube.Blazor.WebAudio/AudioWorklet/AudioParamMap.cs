using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// A collection of <see cref="AudioParam"/> objects with associated names.
/// This maplike object is populated from a list of <see cref="AudioParamDescriptor"/>s in the <see cref="AudioWorkletProcessor"/> class constructor at the instantiation.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#audioparammap">See the API definition here</see>.</remarks>
public class AudioParamMap : BaseJSWrapper, IJSCreatable<AudioParamMap>
{
    /// <inheritdoc/>
    public static async Task<AudioParamMap> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<AudioParamMap> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new AudioParamMap(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected AudioParamMap(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }
}
