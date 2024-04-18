using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The <see cref="AudioWorklet"/> object allows developers to supply scripts (such as JavaScript or WebAssembly code) to process audio on the rendering thread, supporting custom <see cref="AudioNode"/>s.
/// This processing mechanism ensures synchronous execution of the script code with other built-in <see cref="AudioNode"/>s in the audio graph.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#audioworklet">See the API definition here</see>.</remarks>
public class AudioWorklet : Worklet, IJSCreatable<AudioWorklet>
{
    /// <inheritdoc/>
    public static new async Task<AudioWorklet> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<AudioWorklet> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new AudioWorklet(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected AudioWorklet(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }


}
