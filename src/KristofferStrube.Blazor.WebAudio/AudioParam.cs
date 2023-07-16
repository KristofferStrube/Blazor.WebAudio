using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="AudioParam"/> controls an individual aspect of an <see cref="AudioNode"/>'s functionality, such as volume.
/// The parameter can be set immediately to a particular value using the <see cref="SetValueAsync"/> method.
/// Or, value changes can be scheduled to happen at very precise times (in the coordinate system of <see cref="AudioContext"/>'s currentTime attribute), for envelopes, volume fades, LFOs, filter sweeps, grain windows, etc.
/// In this way, arbitrary timeline-based automation curves can be set on any AudioParam.
/// Additionally, audio signals from the outputs of <see cref="AudioNode"/>s can be connected to an <see cref="AudioParam"/>, summing with the intrinsic parameter value.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioParam">See the API definition here</see>.</remarks>
public class AudioParam : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioParam"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioParam"/>.</param>
    /// <returns>A wrapper instance for a <see cref="AudioParam"/>.</returns>
    public static Task<AudioParam> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AudioParam(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioParam"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioParam"/>.</param>
    public AudioParam(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    /// <summary>
    /// Gets the parameter’s floating-point value. This attribute is initialized to same value as the one you get from <see cref="GetDefaultValueAsync"/>.
    /// </summary>
    /// <returns>A float.</returns>
    public async Task<float> GetValueAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<float>("getAttribute", JSReference, "value");
    }

    /// <summary>
    /// Setting this attribute has the effect of assigning the requested value, and calling the <see cref="SetValueAtTimeAsync"/> method with the current <see cref="AudioContext"/>'s currentTime. Any exceptions that would be thrown by <see cref="SetValueAtTimeAsync"/> will also be thrown by setting this attribute.
    /// </summary>
    /// <param name="value">The new value.</param>
    public async Task SetValueAsync(float value)
    {
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "value", value);
    }

    /// <summary>
    /// Initial value for <see cref="GetValueAsync"/>.
    /// </summary>
    /// <returns>A float</returns>
    public async Task<float> GetDefaultValueAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<float>("getAttribute", JSReference, "defaultValue");
    }

    /// <summary>
    /// The nominal minimum value that the parameter can take. Together with <see cref="GetMaxValueAsync"/>, this forms the nominal range for this parameter.
    /// </summary>
    /// <returns>A float</returns>
    public async Task<float> GetMinValueAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<float>("getAttribute", JSReference, "minValue");
    }

    /// <summary>
    /// The nominal maximum value that the parameter can take. Together with <see cref="GetMinValueAsync"/>, this forms the nominal range for this parameter.
    /// </summary>
    /// <returns>A float</returns>
    public async Task<float> GetMaxValueAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<float>("getAttribute", JSReference, "maxValue");
    }

    /// <summary>
    /// Schedules a parameter value change at the given time.
    /// </summary>
    /// <remarks>
    /// A <see cref="RangeErrorException"/> exception will be thrown if <paramref name="startTime"/> is negative or is not a finite number. If <paramref name="startTime"/> is less than currentTime, it is clamped to currentTime.
    /// </remarks>
    /// <param name="value">The value the parameter will change to at the given time.</param>
    /// <param name="startTime">The time in the same time coordinate system as the <see cref="BaseAudioContext"/>'s currentTime at which the parameter changes to the given value.</param>
    /// <returns>The same <see cref="AudioParam"/>.</returns>
    public async Task<AudioParam> SetValueAtTimeAsync(float value, double startTime)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("setValueAtTime", value, startTime);
        return await CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Schedules a linear continuous change in parameter value from the previous scheduled parameter value to the given value.
    /// </summary>
    /// <param name="value">The value the parameter will linearly ramp to at the given time.</param>
    /// <param name="endTime">The time in the same time coordinate system as the <see cref="BaseAudioContext"/>'s currentTime attribute at which the automation ends. </param>
    /// <returns>The same <see cref="AudioParam"/>.</returns>
    public async Task<AudioParam> LinearRampToValueAtTimeAsync(float value, double endTime)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("linearRampToValueAtTime", value, endTime);
        return await CreateAsync(JSRuntime, jSInstance);
    }
}
