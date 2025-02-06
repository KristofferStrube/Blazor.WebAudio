using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents a processing node which positions an incoming audio stream in a stereo image using a low-cost panning algorithm.
/// This panning effect is common in positioning audio components in a stereo stream.<br />
/// The input of this node is stereo (2 channels) and cannot be increased.
/// Connections from nodes with fewer or more channels will be up-mixed or down-mixed appropriately.
/// The output of this node is hard-coded to stereo (2 channels) and cannot be configured.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#StereoPannerNode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class StereoPannerNode : AudioNode, IJSCreatable<StereoPannerNode>
{
    /// <inheritdoc/>
    public static new async Task<StereoPannerNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<StereoPannerNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new StereoPannerNode(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Creates a <see cref="StereoPannerNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="StereoPannerNode"/> will be associated with.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="StereoPannerNode"/>.</param>
    /// <returns>A new instance of a <see cref="StereoPannerNode"/>.</returns>
    public static async Task<StereoPannerNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, StereoPannerOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructStereoPannerNode", context, options);
        return new StereoPannerNode(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected StereoPannerNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    /// <summary>
    /// The position of the input in the output’s stereo image. <c>-1</c> represents full left, <c>+1</c> represents full right.
    /// </summary>
    public async Task<AudioParam> GetPanAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "pan");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }
}
