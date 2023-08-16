using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents an audio source from an in-memory audio asset in an <see cref="AudioBuffer"/>.
/// It is useful for playing audio assets which require a high degree of scheduling flexibility and accuracy.
/// If sample-accurate playback of network- or disk-backed assets is required, an implementer should use <see cref="AudioWorkletNode"/> to implement playback.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioBufferSourceNode">See the API definition here</see>.</remarks>
public class AudioBufferSourceNode : AudioScheduledSourceNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioBufferSourceNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioBufferSourceNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="AudioBufferSourceNode"/>.</returns>
    public static new Task<AudioBufferSourceNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AudioBufferSourceNode(jSRuntime, jSReference));
    }

    //public static async Task<AudioBufferSourceNode> CreateAsync(BaseAudioContext context, AudioBufferSourceOptions? options = null)

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioBufferSourceNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioBufferSourceNode"/>.</param>
    protected AudioBufferSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    /// <summary>
    /// Represents the audio asset to be played.
    /// </summary>
    /// <returns>A <see cref="AudioBuffer"/></returns>
    public async Task<AudioBuffer?> GetBufferAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "buffer");
        return jSInstance is null ? null : await AudioBuffer.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Sets the audio asset to be played.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="InvalidStateErrorException"/> if <paramref name="value"/> is not <see langword="null"/> and the buffer is already set.
    /// </remarks>
    /// <exception cref="InvalidStateErrorException"></exception>
    /// <param name="value">The new <see cref="AudioBuffer"/></param>
    public async Task SetBufferAsync(AudioBuffer? value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "buffer", value?.JSReference);
    }

    public async Task<AudioParam> GetPlaybackRate()
    {
        return default!;
    }

    /// <summary>
    /// Indicates if the region of audio data designated by <see cref="GetLoopStartAsync"/> and <see cref="GetLoopEndAsync"/> should be played continuously in a loop.
    /// The default value is <see langword="false"/>.
    /// </summary>
    public async Task<bool> GetLoopAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "loop");
    }

    /// <summary>
    /// Sets whether the region of audio data designated by <see cref="GetLoopStartAsync"/> and <see cref="GetLoopEndAsync"/> should be played continuously in a loop.
    /// The default value is <see langword="false"/>.
    /// </summary>
    public async Task SetLoopAsync(bool value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "loop", value);
    }

    /// <summary>
    /// An optional playhead position where looping should begin if <see cref="GetLoopAsync"/> is <see langword="true"/>.
    /// Its default value is <c>0</c>.
    /// If loopStart is less than <c>0</c>, looping will begin at <c>0</c>.
    /// If loopStart is greater than the duration of the buffer, looping will begin at the end of the buffer.
    /// </summary>
    public async Task<double> GetLoopStartAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "loopStart");
    }

    /// <summary>
    /// An optional playhead position where looping should begin if <see cref="GetLoopAsync"/> is <see langword="true"/>.
    /// Its default value is <c>0</c>, and it may usefully be set to any value between <c>0</c> and the duration of the buffer.
    /// If loopStart is less than <c>0</c>, looping will begin at <c>0</c>.
    /// If loopStart is greater than the duration of the buffer, looping will begin at the end of the buffer.
    /// </summary>
    /// <param name="value">The new loopStart value.</param>
    /// <returns></returns>
    public async Task SetLoopStartAsync(double value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "loopStart", value);
    }

    /// <summary>
    /// An optional playhead position where looping should end if <see cref="GetLoopAsync"/> is <see langword="true"/>.
    /// Its value is exclusive of the content of the loop.
    /// Its default value is <c>0</c>, and it may usefully be set to any value between <c>0</c> and the duration of the buffer.
    /// If loopEnd is less than or equal to <c>0</c>, or if loopEnd is greater than the duration of the buffer, looping will end at the end of the buffer.
    /// </summary>
    public async Task<double> GetLoopEndAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "loopEnd");
    }

    /// <summary>
    /// An optional playhead position where looping should end if <see cref="GetLoopAsync"/> is <see langword="true"/>.
    /// Its value is exclusive of the content of the loop.
    /// Its default value is <c>0</c>, and it may usefully be set to any value between <c>0</c> and the duration of the buffer.
    /// If loopEnd is less than or equal to <c>0</c>, or if loopEnd is greater than the duration of the buffer, looping will end at the end of the buffer.
    /// </summary>
    /// <param name="value">The new loopEnd value.</param>
    /// <returns></returns>
    public async Task SetLoopEndAsync(double value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "loopEnd", value);
    }

    /// <summary>
    /// Schedules a sound to playback at an exact time.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="InvalidStateErrorException"/> if it has already been started.<br />
    /// It throws an <see cref="RangeErrorException"/> if <paramref name="offset"/> is negative.<br />
    /// It throws an <see cref="RangeErrorException"/> if <paramref name="duration"/> is negative.
    /// </remarks>
    /// <param name="when">The when parameter describes at what time (in seconds) the sound should start playing. It is in the same time coordinate system as the AudioContext's currentTime attribute.</param>
    /// <param name="offset">The offset parameter supplies a playhead position where playback will begin. If <c>0</c> is passed in for this value, then playback will start from the beginning of the buffer.</param>
    /// <param name="duration">The duration parameter describes the duration of sound to be played, expressed as seconds of total buffer content to be output, including any whole or partial loop iterations. The units of duration are independent of the effects of <see cref="GetPlaybackRate"/>. For example, a duration of <c>5</c> seconds with a playback rate of <c>0.5</c> will output <c>5</c> seconds of buffer content at half speed, producing <c>10</c> seconds of audible output.</param>
    /// <exception cref="InvalidStateErrorException"></exception>
    public async Task StartAsync(double when = 0, double? offset = null, double? duration = null)
    {
        if (offset is null && duration is null)
        {
            await JSReference.InvokeVoidAsync("start", when);
        }
        else if (duration is null)
        {
            await JSReference.InvokeVoidAsync("start", when, offset);
        }
        else
        {
            await JSReference.InvokeVoidAsync("start", when, offset, duration);
        }
    }
}
