using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents a memory-resident audio asset.
/// It can contain one or more channels with each channel appearing to be 32-bit floating-point linear PCM values with a nominal range of <c>[−1,1]</c> but the values are not limited to this range.
/// Typically, it would be expected that the length of the PCM data would be fairly short (usually somewhat less than a minute).
/// For longer sounds, such as music soundtracks, streaming should be used with the audio element and <see cref="MediaElementAudioSourceNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioBuffer">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class AudioBuffer : BaseJSWrapper, IJSCreatable<AudioBuffer>
{
    /// <inheritdoc/>
    public static async Task<AudioBuffer> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<AudioBuffer> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new AudioBuffer(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Creates an <see cref="AudioBuffer"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="options">An <see cref="AudioBufferOptions"/> that determine the properties for this <see cref="AudioBuffer"/>.</param>
    /// <returns>A new instance of an <see cref="AudioBuffer"/>.</returns>
    public static async Task<AudioBuffer> CreateAsync(IJSRuntime jSRuntime, AudioBufferOptions options)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructAudioBuffer", options);
        return new AudioBuffer(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    public AudioBuffer(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    /// <summary>
    /// The sample-rate for the PCM audio data in samples per second.
    /// </summary>
    public async Task<float> GetSampleRateAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<float>("getAttribute", JSReference, "sampleRate");
    }

    /// <summary>
    /// Length of the PCM audio data in sample-frames.
    /// </summary>
    public async Task<ulong> GetLengthAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "length");
    }

    /// <summary>
    /// Duration of the PCM audio data in seconds.
    /// </summary>
    public async Task<double> GetDurationAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "duration");
    }

    /// <summary>
    /// The number of discrete audio channels.
    /// </summary>
    public async Task<ulong> GetNumberOfChannelsAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "numberOfChannels");
    }

    /// <summary>
    /// Either gets a reference to or gets a copy of the bytes stored in the <see cref="AudioBuffer"/>.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="UnknownErrorException"/> if the <see cref="Float32Array"/> can't be created.<br />
    /// It throws an <see cref="IndexSizeErrorException"/> if <paramref name="channel"/> is greater than or equal to the number of channels.<br />
    /// <br />
    /// The methods <see cref="CopyToChannelAsync"/> and <see cref="CopyFromChannelAsync"/> can be used to fill part of an array by passing in a <see cref="Float32Array"/> that’s a view onto the larger array.
    /// When reading data from an <see cref="AudioBuffer"/>'s channels, and the data can be processed in chunks, <see cref="CopyFromChannelAsync"/> should be preferred to calling <see cref="GetChannelDataAsync"/>
    /// and accessing the resulting array, because it may avoid unnecessary memory allocation and copying.
    /// </remarks>
    /// <exception cref="UnknownErrorException"></exception>
    /// <exception cref="IndexSizeErrorException"></exception>
    /// <returns>A new <see cref="Float32Array"/>.</returns>
    public async Task<Float32Array> GetChannelDataAsync(ulong channel)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("getChannelData", channel);
        return await Float32Array.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Copies the samples from the specified channel of the <see cref="AudioBuffer"/> to the <paramref name="destination"/> array.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="IndexSizeErrorException"/> if <paramref name="channelNumber"/> is greater than or equal to the number of channels.<br />
    /// </remarks>
    /// <param name="destination">The array the channel data will be copied to.</param>
    /// <param name="channelNumber">The index of the channel to copy the data from.</param>
    /// <param name="bufferOffset">An optional offset, defaulting to 0. Data from the <see cref="AudioBuffer"/> starting at this offset is copied to the <paramref name="destination"/>.</param>
    /// <exception cref="IndexSizeErrorException"></exception>
    public async Task CopyFromChannelAsync(Float32Array destination, ulong channelNumber, ulong bufferOffset = 0)
    {
        await JSReference.InvokeVoidAsync("copyFromChannel", destination.JSReference, channelNumber, bufferOffset);
    }

    /// <summary>
    /// Copies the samples to the specified channel of the <see cref="AudioBuffer"/> from the <paramref name="source"/> array.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="IndexSizeErrorException"/> if <paramref name="channelNumber"/> is greater than or equal to the number of channels.<br />
    /// </remarks>
    /// <param name="source">The array the channel data will be copied from.</param>
    /// <param name="channelNumber">The index of the channel to copy the data to.</param>
    /// <param name="bufferOffset">An optional offset, defaulting to 0. Data from the source is copied to the <see cref="AudioBuffer"/> starting at this offset.</param>
    /// <exception cref="IndexSizeErrorException"></exception>
    public async Task CopyToChannelAsync(Float32Array source, ulong channelNumber, ulong bufferOffset = 0)
    {
        await JSReference.InvokeVoidAsync("copyToChannel", source.JSReference, channelNumber, bufferOffset);
    }
}
