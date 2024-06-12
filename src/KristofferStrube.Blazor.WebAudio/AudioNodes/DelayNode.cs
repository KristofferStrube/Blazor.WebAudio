using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// A delay-line is a fundamental building block in audio applications.
/// This interface is an <see cref="AudioNode"/> with a single input and single output.
/// The number of channels of the output always equals the number of channels of the input.<br />
/// It delays the incoming audio signal by a certain amount.
/// Specifically, at each time <c>t</c>, input signal <c>input(t)</c>, delay time <c>delayTime(t)</c> and output signal <c>output(t)</c>, the output will be <c>output(t) = input(t - delayTime(t))</c>.
/// The default delayTime is <c>0</c> seconds (no delay).
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#DelayNode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class DelayNode : AudioNode, IJSCreatable<DelayNode>
{
    /// <inheritdoc/>
    public static new async Task<DelayNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<DelayNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new DelayNode(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Creates an <see cref="DelayNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="DelayNode"/> will be associated with.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="DelayNode"/>.</param>
    /// <returns>A new instance of an <see cref="DelayNode"/>.</returns>
    public static async Task<DelayNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, DelayOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructDelayNode", context.JSReference, options);
        return new DelayNode(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected DelayNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

    /// <summary>
    /// An <see cref="AudioParam"/> object representing the amount of delay (in seconds) to apply.
    /// Its default value is <c>0</c> (no delay).
    /// The minimum value is <c>0</c> and the maximum value is determined by the maxDelayTime argument parsed to <see cref="BaseAudioContext.CreateDelayAsync(double)"/> or the <see cref="DelayOptions.MaxDelayTime"/> if constructed using the <see cref="CreateAsync(IJSRuntime, BaseAudioContext, DelayOptions?)"/> method.
    /// </summary>
    /// <remarks>
    /// If <see cref="DelayNode"/> is part of a cycle, then the value is clamped to a minimum of one render quantum.
    /// </remarks>
    public async Task<AudioParam> GetDelayTimeAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "delayTime");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }
}
