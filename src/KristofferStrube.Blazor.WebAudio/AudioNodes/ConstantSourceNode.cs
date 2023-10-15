using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebAudio.Options;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents a constant audio source whose output is nominally a constant value.
/// It is useful as a constant source node in general and can be used as if it were a constructible <see cref="AudioParam"/> by automating its <see cref="GetOffsetAsync"/> or connecting another node to it.<br />
/// The single output of this node consists of one channel (mono).
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#ConstantSourceNode">See the API definition here</see>.</remarks>
public class ConstantSourceNode : AudioScheduledSourceNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ConstantSourceNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ConstantSourceNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="ConstantSourceNode"/>.</returns>
    public static new Task<ConstantSourceNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new ConstantSourceNode(jSRuntime, jSReference));
    }

    /// <summary>
    /// Creates a <see cref="ConstantSourceNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="ConstantSourceNode"/> will be associated with.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="ConstantSourceNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="ConstantSourceNode"/>.</returns>
    public static async Task<ConstantSourceNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, ConstantSourceOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructConstantSourceNode", context, options);
        return new ConstantSourceNode(jSRuntime, jSInstance);
    }

    private ConstantSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// The constant value of the source.
    /// </summary>
    /// <remarks>
    /// Default value: <c>1</c><br />
    /// Min value: <see cref="float.MinValue"/><br />
    /// Max value: <see cref="float.MaxValue"/><br />
    /// Automation rate: <see cref="AutomationRate.ARate"/>
    /// </remarks>
    /// <returns>An new <see cref="AudioParam"/>.</returns>
    public async Task<AudioParam> GetOffsetAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "offset");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance);
    }
}
