using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebAudio.Options;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents a processing node which applies a linear convolution effect given an impulse response.<br />
/// The input of this node is either mono (<c>1</c> channel) or stereo (<c>2</c> channels) and cannot be increased.
/// Connections from nodes with more channels will be down-mixed appropriately.<br />
/// There are channelCount constraints and channelCountMode constraints for this node.
/// These constraints ensure that the input to the node is either mono or stereo.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#ConvolverNode">See the API definition here</see>.</remarks>
public class ConvolverNode : AudioNode
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of an <see cref="ConvolverNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ConvolverNode"/>.</param>
    /// <returns>A wrapper instance for an <see cref="ConvolverNode"/>.</returns>
    public static new Task<ConvolverNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new ConvolverNode(jSRuntime, jSReference));
    }

    /// <summary>
    /// Creates a <see cref="ConvolverNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="ConvolverNode"/> will be associated with.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="ConvolverNode"/>.</param>
    /// <returns>A new instance of a <see cref="ConvolverNode"/>.</returns>
    public static async Task<ConvolverNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, ConvolverOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructConvolverNode", context, options);
        return new ConvolverNode(jSRuntime, jSInstance);
    }

    private ConvolverNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Gets the buffer that is used for convolution.
    /// </summary>
    public async Task<AudioBuffer?> GetBufferAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "buffer");
        return jSInstance is null ? null : await AudioBuffer.CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Sets the buffer that will be used for convolution.
    /// At the time when this attribute is set, the buffer and the state of the normalize attribute will be used to configure the <see cref="ConvolverNode"/> with this impulse response having the given normalization.
    /// The initial value of this attribute is <see langword="null"/>.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="NotSupportedErrorException"/> if <see cref="AudioBuffer.GetNumberOfChannelsAsync"/> is not <c>1</c>, <c>2</c>, <c>4</c>, or if the <see cref="AudioBuffer.GetSampleRateAsync"/> is not equaivalent to the associated <see cref="BaseAudioContext.GetSampleRateAsync"/>.
    /// </remarks>
    public async Task SetBufferAsync(AudioBuffer? value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "buffer", value?.JSReference);
    }

    /// <summary>
    /// Gets the flag for whether the impulse response from the buffer will be scaled by an equal-power normalization when the buffer atttribute is set.<br />
    /// </summary>
    /// <remarks>
    /// Its default value is <see langword="true"/> in order to achieve a more uniform output level from the convolver when loaded with diverse impulse responses.
    /// If it is set to <see langword="false"/>, then the convolution will be rendered with no pre-processing/scaling of the impulse response.
    /// Changes to this value do not take effect until the next time the buffer attribute is set.<br />
    /// If the it is <see langword="false"/> when the buffer attribute is set then the <see cref="ConvolverNode"/> will perform a linear convolution given the exact impulse response contained within the buffer.<br />
    /// Otherwise, if the it is <see langword="true"/> when the buffer attribute is set then the <see cref="ConvolverNode"/> will first perform a scaled RMS-power analysis of the audio data contained within buffer to calculate a normalizationScale.
    /// During processing, the <see cref="ConvolverNode"/> will then take this calculated normalizationScale value and multiply it by the result of the linear convolution resulting from processing the input with the impulse response (represented by the buffer) to produce the final output.
    /// </remarks>
    public async Task<bool> GetNormalizeAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "normalize");
    }

    /// <summary>
    /// Sets the flag for whether the impulse response from the buffer will be scaled by an equal-power normalization when the buffer atttribute is set.<br />
    /// </summary>
    /// <remarks>
    /// Its default value is <see langword="true"/> in order to achieve a more uniform output level from the convolver when loaded with diverse impulse responses.
    /// If it is set to <see langword="false"/>, then the convolution will be rendered with no pre-processing/scaling of the impulse response.
    /// Changes to this value do not take effect until the next time the buffer attribute is set.<br />
    /// If the it is <see langword="false"/> when the buffer attribute is set then the <see cref="ConvolverNode"/> will perform a linear convolution given the exact impulse response contained within the buffer.<br />
    /// Otherwise, if the it is <see langword="true"/> when the buffer attribute is set then the <see cref="ConvolverNode"/> will first perform a scaled RMS-power analysis of the audio data contained within buffer to calculate a normalizationScale.
    /// During processing, the <see cref="ConvolverNode"/> will then take this calculated normalizationScale value and multiply it by the result of the linear convolution resulting from processing the input with the impulse response (represented by the buffer) to produce the final output.
    /// </remarks>
    public async Task SetNormalizeAsync(bool value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "normalize", value);
    }
}
