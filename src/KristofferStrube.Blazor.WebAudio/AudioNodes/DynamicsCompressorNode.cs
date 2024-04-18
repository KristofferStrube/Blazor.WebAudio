using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="DynamicsCompressorNode"/> is an <see cref="AudioNode"/> processor implementing a dynamics compression effect.<br />
/// Dynamics compression is very commonly used in musical production and game audio.
/// It lowers the volume of the loudest parts of the signal and raises the volume of the softest parts.
/// Overall, a louder, richer, and fuller sound can be achieved.
/// It is especially important in games and musical applications where large numbers of individual sounds are played simultaneous to control the overall signal level and help avoid clipping (distorting) the audio output to the speakers.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#DynamicsCompressorNode">See the API definition here</see>.</remarks>
public class DynamicsCompressorNode : AudioNode, IJSCreatable<DynamicsCompressorNode>
{
    /// <inheritdoc/>
    public static new async Task<DynamicsCompressorNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<DynamicsCompressorNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new DynamicsCompressorNode(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Creates an <see cref="DynamicsCompressorNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="DynamicsCompressorNode"/> will be associated with.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="DynamicsCompressorNode"/>.</param>
    /// <returns>A new instance of an <see cref="DynamicsCompressorNode"/>.</returns>
    public static async Task<DynamicsCompressorNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, DynamicsCompressorOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructDynamicsCompressorNode", context.JSReference, options);
        return new DynamicsCompressorNode(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected DynamicsCompressorNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

    /// <summary>
    /// The decibel value above which the compression will start taking effect.
    /// Default is <c>-24</c> and it must be between <c>-100</c> and <c>0</c>.
    /// </summary>
    public async Task<AudioParam> GetThresholdAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "threshold");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// A decibel value representing the range above the threshold where the curve smoothly transitions to the "ratio" portion.
    /// Default is <c>30</c> and it must be between <c>0</c> and <c>40</c>.
    /// </summary>
    public async Task<AudioParam> GetKneeAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "knee");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// The amount of dB change in input for a 1 dB change in output.
    /// Default is <c>12</c> and it must be between <c>1</c> and <c>20</c>.
    /// </summary>
    public async Task<AudioParam> GetRatioAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "ratio");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// A read-only decibel value for metering purposes, representing the current amount of gain reduction that the compressor is applying to the signal.
    /// If fed no signal the value will be 0 (no gain reduction).
    /// </summary>
    public async Task<float> GetReductionAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<float>("getAttribute", JSReference, "reduction");
    }

    /// <summary>
    /// The amount of time (in seconds) to reduce the gain by 10dB.
    /// Default is <c>0.003</c> and it must be between <c>0</c> and <c>1</c>.
    /// </summary>
    public async Task<AudioParam> GetAttackAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "attack");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// The amount of time (in seconds) to increase the gain by 10dB.
    /// Default is <c>0.25</c> and it must be between <c>0</c> and <c>1</c>.
    /// </summary>
    public async Task<AudioParam> GetReleaseAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "release");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }
}
