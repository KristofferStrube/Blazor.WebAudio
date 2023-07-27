using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents a memory-resident audio asset.
/// It can contain one or more channels with each channel appearing to be 32-bit floating-point linear PCM values with a nominal range of <c>[−1,1]</c> but the values are not limited to this range.
/// Typically, it would be expected that the length of the PCM data would be fairly short (usually somewhat less than a minute).
/// For longer sounds, such as music soundtracks, streaming should be used with the audio element and <see cref="MediaElementAudioSourceNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioBuffer">See the API definition here</see>.</remarks>
public class AudioBuffer : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of an <see cref="AudioBuffer"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioBuffer"/>.</param>
    /// <returns>A wrapper instance for an <see cref="AudioBuffer"/>.</returns>
    public static Task<AudioBuffer> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AudioBuffer(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioBuffer"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioBuffer"/>.</param>
    public AudioBuffer(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    /// <summary>
    /// Length of the PCM audio data in sample-frames.
    /// </summary>
    public async Task<ulong> GetLength()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "length");
    }

    /// <summary>
    /// Duration of the PCM audio data in seconds.
    /// </summary>
    /// <returns>The duration in seconds.</returns>
    public async Task<double> GetDuration()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "duration");
    }
}
