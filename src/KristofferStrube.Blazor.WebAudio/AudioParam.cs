using KristofferStrube.Blazor.WebIDL;
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
[IJSWrapperConverter]
public class AudioParam : BaseJSWrapper, IJSCreatable<AudioParam>
{
    /// <inheritdoc/>
    public static async Task<AudioParam> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<AudioParam> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new AudioParam(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected AudioParam(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
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
    /// The automation rate for the <see cref="AudioParam"/>.
    /// The default value depends on the actual <see cref="AudioParam"/>; see the description of each individual <see cref="AudioParam"/> for the default value.
    /// </summary>
    /// <returns></returns>
    public async Task<AutomationRate> GetAutomationRateAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<AutomationRate>("getAttribute", JSReference, "automationRate");
    }

    /// <summary>
    /// The automation rate for the <see cref="AudioParam"/>.
    /// The default value depends on the actual <see cref="AudioParam"/>; see the description of each individual <see cref="AudioParam"/> for the default value.
    /// </summary>
    /// <returns></returns>
    public async Task SetAutomationRateAsync(AutomationRate value)
    {
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "automationRate", value);
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

    /// <summary>
    /// Schedules an exponential continuous change in parameter value from the previous scheduled parameter value to the given value.
    /// Parameters representing filter frequencies and playback rate are best changed exponentially because of the way humans perceive sound.
    /// </summary>
    /// <param name="value">The value the parameter will linearly ramp to at the given time.</param>
    /// <param name="endTime">The time in the same time coordinate system as the <see cref="BaseAudioContext"/>'s currentTime attribute at which the automation ends. </param>
    /// <returns>The same <see cref="AudioParam"/>.</returns>
    public async Task<AudioParam> ExponentialRampToValueAtTimeAsync(float value, double endTime)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("exponentialRampToValueAtTime", value, endTime);
        return await CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Start exponentially approaching the target value at the given time with a rate having the given time constant.
    /// Among other uses, this is useful for implementing the "decay" and "release" portions of an ADSR envelope.
    /// Please note that the parameter value does not immediately change to the target value at the given time, but instead gradually changes to the target value.
    /// </summary>
    /// <remarks>
    /// It throws a <see cref="RangeErrorException"/> if <paramref name="startTime"/> is negative or not a finite number.<br />
    /// It throws a <see cref="RangeErrorException"/> if <paramref name="timeConstant"/> is negative.
    /// </remarks>
    /// <param name="target">The value the parameter will start changing to at the given time.</param>
    /// <param name="startTime">The time at which the exponential approach will begin, in the same time coordinate system as the <see cref="AudioContext"/>'s currentTime attribute. If it is less than currentTime, it is clamped to currentTime.</param>
    /// <param name="timeConstant">The time-constant value of first-order filter (exponential) approach to the target value. The larger this value is, the slower the transition will be. If it is zero, the output value jumps immediately to the final value.</param>
    /// <exception cref="RangeErrorException"></exception>
    /// <returns>The same <see cref="AudioParam"/>.</returns>
    public async Task<AudioParam> SetTargetAtTimeAsync(float target, double startTime, float timeConstant)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("setTargetAtTime", target, startTime, timeConstant);
        return await CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Sets an array of arbitrary parameter values starting at the given time for the given duration.
    /// The number of values will be scaled to fit into the desired duration.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="InvalidStateErrorException"/> if the length of <paramref name="values"/> is less than 2.<br />
    /// It throws a <see cref="RangeErrorException"/> if <paramref name="startTime"/> is negative or not a finite number.<br />
    /// It throws a <see cref="RangeErrorException"/> if <paramref name="duration"/> is not strictly positive or if it not a finite number.
    /// </remarks>
    /// <param name="values">A sequence of float values representing a parameter value curve. These values will apply starting at the given time and lasting for the given duration. When this method is called, an internal copy of the curve is created for automation purposes. Subsequent modifications of the contents of the passed-in array therefore have no effect on the <see cref="AudioParam"/>.</param>
    /// <param name="startTime">The start time in the same time coordinate system as the <see cref="AudioContext"/>'s currentTime attribute at which the value curve will be applied. If it is less than currentTime, it is clamped to currentTime.</param>
    /// <param name="duration">The amount of time in seconds (after the startTime parameter) where values will be calculated according to the values parameter.</param>
    /// <exception cref="InvalidStateErrorException"></exception>
    /// <exception cref="RangeErrorException"></exception>
    /// <returns>The same <see cref="AudioParam"/>.</returns>
    public async Task<AudioParam> SetValueCurveAtTimeAsync(float[] values, double startTime, double duration)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("setValueCurveAtTime", values, startTime, duration);
        return await CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Cancels all scheduled parameter changes with times greater than or equal to cancelTime.
    /// Cancelling a scheduled parameter change means removing the scheduled event from the event list.
    /// Any active automations whose automation event time is less than cancelTime are also cancelled, and such cancellations may cause discontinuities because the original value (from before such automation) is restored immediately.
    /// Any hold values scheduled by <see cref="CancelAndHoldAtTimeAsync"/> are also removed if the hold time occurs after cancelTime.
    /// </summary>
    /// <remarks>
    /// It throws a <see cref="RangeErrorException"/> if <paramref name="cancelTime"/> is negative or not a finite number.<br />
    /// </remarks>
    /// <param name="cancelTime">The time after which any previously scheduled parameter changes will be cancelled. It is a time in the same time coordinate system as the <see cref="AudioContext"/>'s currentTime attribute. If it is less than currentTime, it is clamped to currentTime.</param>
    /// <exception cref="RangeErrorException"></exception>
    /// <returns>The same <see cref="AudioParam"/>.</returns>
    public async Task<AudioParam> CancelScheduledValuesAsync(double cancelTime)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("cancelScheduledValues", cancelTime);
        return await CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// This is similar to <see cref="CancelScheduledValuesAsync"/> in that it cancels all scheduled parameter changes with times greater than or equal to cancelTime.
    /// However, in addition, the automation value that would have happened at cancelTime is then proprogated for all future time until other automation events are introduced.
    /// </summary>
    /// <remarks>
    /// It throws a <see cref="RangeErrorException"/> if <paramref name="cancelTime"/> is negative or not a finite number.<br />
    /// </remarks>
    /// <param name="cancelTime">The time after which any previously scheduled parameter changes will be cancelled. It is a time in the same time coordinate system as the <see cref="AudioContext"/>'s currentTime attribute. If it is less than currentTime, it is clamped to currentTime.</param>
    /// <exception cref="RangeErrorException"></exception>
    /// <returns>The same <see cref="AudioParam"/>.</returns>
    public async Task<AudioParam> CancelAndHoldAtTimeAsync(double cancelTime)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("cancelAndHoldAtTime", cancelTime);
        return await CreateAsync(JSRuntime, jSInstance);
    }
}
