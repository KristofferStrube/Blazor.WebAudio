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
[IJSWrapperConverter]
public class AnalyserNode : AudioNode, IJSCreatable<AnalyserNode>
{
    /// <inheritdoc/>
    public static new async Task<AnalyserNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<AnalyserNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new AnalyserNode(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Creates an <see cref="AnalyserNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="AnalyserNode"/> will be associated with.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="AnalyserNode"/>.</param>
    /// <returns>A new instance of an <see cref="AnalyserNode"/>.</returns>
    public static async Task<AnalyserNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, AnalyserOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructAnalyzerNode", context, options);
        return new AnalyserNode(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected AnalyserNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

    /// <summary>
    /// Gets a reference to the bytes held by the <see cref="Float32Array"/> passed as an argument.
    /// Copies the current frequency data into those bytes.
    /// </summary>
    /// <remarks>
    /// If the array has fewer elements than <see cref="GetFrequencyBinCountAsync"/>, the excess elements will be dropped.
    /// If the array has more elements than the <see cref="GetFrequencyBinCountAsync"/>, the excess elements will be ignored.
    /// The most recent <see cref="GetFftSizeAsync"/> frames are used in computing the frequency data.<br />
    /// If another call to <see cref="GetByteFrequencyDataAsync"/> or <see cref="GetFloatFrequencyDataAsync"/> occurs within the same render quantum as a previous call, the current frequency data is not updated with the same data.
    /// Instead, the previously computed data is returned.
    /// </remarks>
    /// <param name="array">This parameter is where the frequency-domain analysis data will be copied.</param>
    public async Task GetFloatFrequencyDataAsync(Float32Array array)
    {
        await JSReference.InvokeVoidAsync("getFloatFrequencyData", array);
    }

    /// <summary>
    /// Gets a reference to the bytes held by the <see cref="Uint8Array"/> passed as an argument.
    /// Copies the current frequency data into those bytes.
    /// </summary>
    /// <remarks>
    /// If the array has fewer elements than <see cref="GetFrequencyBinCountAsync"/>, the excess elements will be dropped.
    /// If the array has more elements than the <see cref="GetFrequencyBinCountAsync"/>, the excess elements will be ignored.
    /// The most recent <see cref="GetFftSizeAsync"/> frames are used in computing the frequency data.<br />
    /// If another call to <see cref="GetByteFrequencyDataAsync"/> or <see cref="GetFloatFrequencyDataAsync"/> occurs within the same render quantum as a previous call, the current frequency data is not updated with the same data.
    /// Instead, the previously computed data is returned.
    /// </remarks>
    /// <param name="array">This parameter is where the frequency-domain analysis data will be copied.</param>
    public async Task GetByteFrequencyDataAsync(Uint8Array array)
    {
        await JSReference.InvokeVoidAsync("getByteFrequencyData", array);
    }

    /// <summary>
    /// Gets a reference to the bytes held by the <see cref="Float32Array"/> passed as an argument.
    /// Copies the current time-domain data (waveform data) into those bytes.
    /// </summary>
    /// <remarks>
    /// If the array has fewer elements than the value from <see cref="GetFftSizeAsync"/>, the excess elements will be dropped.
    /// If the array has more elements than what <see cref="GetFftSizeAsync"/> returns, the excess elements will be ignored.
    /// The most recent frames are used in computing the byte data defined by the value from <see cref="GetFftSizeAsync"/>.
    /// </remarks>
    /// <param name="array">This parameter is where the time-domain sample data will be copied.</param>
    public async Task GetFloatTimeDomainDataAsync(Float32Array array)
    {
        await JSReference.InvokeVoidAsync("getFloatTimeDomainData", array);
    }

    /// <summary>
    /// Gets a reference to the bytes held by the <see cref="Uint8Array"/> passed as an argument.
    /// Copies the current time-domain data (waveform data) into those bytes.
    /// </summary>
    /// <remarks>
    /// If the array has fewer elements than the value from <see cref="GetFftSizeAsync"/>, the excess elements will be dropped.
    /// If the array has more elements than what <see cref="GetFftSizeAsync"/> returns, the excess elements will be ignored.
    /// The most recent frames are used in computing the byte data defined by the value from <see cref="GetFftSizeAsync"/>.
    /// </remarks>
    /// <param name="array">This parameter is where the time-domain sample data will be copied.</param>
    public async Task GetByteTimeDomainDataAsync(Uint8Array array)
    {
        await JSReference.InvokeVoidAsync("getByteTimeDomainData", array);
    }

    /// <summary>
    /// Gets the size of the FFT used for frequency-domain analysis (in sample-frames).
    /// </summary>
    /// <returns>Sample frames size.</returns>
    public async Task<ulong> GetFftSizeAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "fftSize");
    }

    /// <summary>
    /// Sets the size of the FFT used for frequency-domain analysis (in sample-frames).
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="IndexSizeErrorException"/> if <paramref name="value"/> is not in the range <c>32</c> to <c>32768</c>
    /// The default value is <c>2048</c>.
    /// Note that large FFT sizes can be costly to compute.
    /// </remarks>
    /// <param name="value">The new value</param>
    /// <exception cref="IndexSizeErrorException"></exception>
    public async Task SetFftSizeAsync(ulong value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "fftSize", value);
    }

    /// <summary>
    /// Gets half the FFT size.
    /// </summary>
    public async Task<ulong> GetFrequencyBinCountAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "frequencyBinCount");
    }

    /// <summary>
    /// Gets the maximum power value in the scaling range for the FFT analysis data for conversion to unsigned byte values. The default value is <c>-30</c>.
    /// </summary>
    public async Task<double> GetMaxDecibelsAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "maxDecibels");
    }

    /// <summary>
    /// Sets the maximum power value in the scaling range for the FFT analysis data for conversion to unsigned byte values. The default value is <c>-30</c>.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="IndexSizeErrorException"/> if <paramref name="value"/> is less than or equal to <see cref="GetMinDecibelsAsync"/>.
    /// </remarks>
    /// <param name="value">The new value.</param>
    /// <exception cref="IndexSizeErrorException"></exception>
    public async Task SetMaxDecibelsAsync(double value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "maxDecibels", value);
    }

    /// <summary>
    /// Gets the minimum power value in the scaling range for the FFT analysis data for conversion to unsigned byte values. The default value is <c>-100</c>.
    /// </summary>
    public async Task<double> GetMinDecibelsAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "minDecibels");
    }

    /// <summary>
    /// Sets the minimum power value in the scaling range for the FFT analysis data for conversion to unsigned byte values. The default value is <c>-100</c>.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="IndexSizeErrorException"/> if <paramref name="value"/> is more than or equal to <see cref="GetMaxDecibelsAsync"/>.
    /// </remarks>
    /// <param name="value">The new value.</param>
    /// <exception cref="IndexSizeErrorException"></exception>
    public async Task SetMinDecibelsAsync(double value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "minDecibels", value);
    }

    /// <summary>
    /// Gets a value from <c>0</c>0 to <c>1</c> where <c>0</c> represents no time averaging with the last analysis frame. The default value is <c>0.8</c>.
    /// </summary>
    public async Task<double> GetSmoothingTimeConstantAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "smoothingTimeConstant");
    }

    /// <summary>
    /// Sets a value from <c>0</c>0 to <c>1</c> where <c>0</c> represents no time averaging with the last analysis frame. The default value is <c>0.8</c>.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="IndexSizeErrorException"/> if <paramref name="value"/> is less than <c>0</c> og more than <c>1</c>.
    /// </remarks>
    /// <param name="value">The new value.</param>
    /// <exception cref="IndexSizeErrorException"></exception>
    public async Task SetSmoothingTimeConstantAsync(double value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "smoothingTimeConstant", value);
    }
}
