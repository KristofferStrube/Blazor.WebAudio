using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents a node which is able to provide real-time frequency and time-domain analysis information.
/// The audio stream will be passed un-processed from input to output.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AnalyserNode">See the API definition here</see>.</remarks>
public class AnalyserNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of an <see cref="AnalyserNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AnalyserNode"/>.</param>
    /// <returns>A wrapper instance for an <see cref="AnalyserNode"/>.</returns>
    public static new Task<AnalyserNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AnalyserNode(jSRuntime, jSReference));
    }

    /// <summary>
    /// Creates an <see cref="AnalyserNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="AnalyserNode"/> will be associated with.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="AnalyserNode"/>.</param>
    /// <returns>A new instance of a <see cref="AnalyserNode"/>.</returns>
    public static async Task<AnalyserNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, AnalyserOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructAnalyzerNode", context.JSReference, options);
        return new AnalyserNode(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of an <see cref="AnalyserNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AnalyserNode"/>.</param>
    protected AnalyserNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Get a reference to the bytes held by the <see cref="Uint8Array"/> passed as an argument.
    /// Copies the current time-domain data (waveform data) into those bytes.
    /// </summary>
    /// <remarks>
    /// If the array has fewer elements than the value of fftSize, the excess elements will be dropped.
    /// If the array has more elements than what <see cref="GetFftSizeAsync"/> returns, the excess elements will be ignored.
    /// The most recent frames are used in computing the byte data defined by the value from <see cref="GetFftSizeAsync"/>.
    /// </remarks>
    /// <param name="array">This parameter is where the time-domain sample data will be copied.</param>
    public async Task GetByteFrequencyDataAsync(Uint8Array array)
    {
        await JSReference.InvokeVoidAsync("getByteFrequencyData", array.JSReference);
    }

    /// <summary>
    /// Get a reference to the bytes held by the <see cref="Uint8Array"/> passed as an argument.
    /// Copies the current time-domain data (waveform data) into those bytes.
    /// </summary>
    /// <remarks>
    /// If the array has fewer elements than the value of fftSize, the excess elements will be dropped.
    /// If the array has more elements than what <see cref="GetFftSizeAsync"/> returns, the excess elements will be ignored.
    /// The most recent frames are used in computing the byte data defined by the value from <see cref="GetFftSizeAsync"/>.
    /// </remarks>
    /// <param name="array">This parameter is where the time-domain sample data will be copied.</param>
    public async Task GetByteTimeDomainDataAsync(Uint8Array array)
    {
        await JSReference.InvokeVoidAsync("getByteTimeDomainData", array.JSReference);
    }

    /// <summary>
    /// Gets the size of the FFT used for frequency-domain analysis (in sample-frames).
    /// </summary>
    /// <returns>Sample frames size.</returns>
    public async Task<ulong> GetFftSizeAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "fftSize");
    }

    /// <summary>
    /// Sets the size of the FFT used for frequency-domain analysis (in sample-frames).
    /// </summary>
    /// <remarks>
    /// This must be a power of two in the range 32 to 32768, otherwise an <see cref="RangeErrorException"/> exception must be thrown.
    /// The default value is <c>2048</c>.
    /// Note that large FFT sizes can be costly to compute.
    /// </remarks>
    /// <param name="value">The new value</param>
    public async Task SetFftSizeAsync(ulong value)
    {
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "fftSize", value);
    }

    /// <summary>
    /// Gets half the FFT size.
    /// </summary>
    /// <returns>Sample frames size.</returns>
    public async Task<ulong> GetFrequencyBinCountAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "frequencyBinCount");
    }
}
