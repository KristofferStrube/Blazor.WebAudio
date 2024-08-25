using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents a user-defined <see cref="AudioNode"/> which lives on the control thread.<br />
/// The node is different from other processing nodes as it is retained in memory and perform audio processing in the absence of any connected inputs.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#audioworkletnode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class AudioWorkletNode : AudioNode, IJSCreatable<AudioWorkletNode>
{
    /// <inheritdoc/>
    public static new async Task<AudioWorkletNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<AudioWorkletNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new AudioWorkletNode(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Creates an <see cref="AudioWorkletNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="AudioWorkletNode"/> will be associated with.</param>
    /// <param name="name">A string that is a key for the <see cref="BaseAudioContext"/>’s node name to parameter descriptor map.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="AudioWorkletNode"/>.</param>
    /// <returns>A new instance of an <see cref="AudioWorkletNode"/>.</returns>
    public static async Task<AudioWorkletNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, string name, AudioWorkletNodeOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructAudioWorkletNode", context, name, options);
        return new AudioWorkletNode(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected AudioWorkletNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    /// <summary>
    /// This is a collection of <see cref="AudioParam"/> objects with associated names.
    /// This maplike object is populated from a list of <see cref="AudioParamDescriptor"/>s in the <see cref="AudioWorkletProcessor"/> class constructor at the instantiation.
    /// </summary>
    public async Task<AudioParamMap> GetParametersAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "parameter");
        return await AudioParamMap.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Every <see cref="AudioWorkletNode"/> has an associated port which is the <see cref="MessagePort"/>.
    /// It is connected to the port on the corresponding <see cref="AudioWorkletProcessor"/> object allowing bidirectional communication between the <see cref="AudioWorkletNode"/> and its <see cref="AudioWorkletProcessor"/>.
    /// </summary>
    /// <remarks>
    /// Authors that register a event listener on the "message" event of this port should call close on either end of the MessageChannel (either in the <see cref="AudioWorkletProcessor"/> or the <see cref="AudioWorkletNode"/> side) to allow for resources to be collected.
    /// </remarks>
    public async Task<MessagePort> GetPortAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "port");
        return await MessagePort.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }
}
