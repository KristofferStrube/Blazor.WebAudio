using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="AudioNode"/>s are the building blocks of an <see cref="AudioContext"/>.
/// This interface represents audio sources, the audio destination, and intermediate processing modules.
/// These modules can be connected together to form processing graphs for rendering audio to the audio hardware.
/// Each node can have inputs and/or outputs. A source node has no inputs and a single output.
/// Most processing nodes such as filters will have one input and one output.
/// Each type of <see cref="AudioNode"/> differs in the details of how it processes or synthesizes audio.
/// But, in general, an <see cref="AudioNode"/> will process its inputs (if it has any), and generate audio for its outputs (if it has any).
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioNode">See the API definition here</see>.</remarks>
public class AudioNode : EventTarget, IJSCreatable<AudioNode>
{
    /// <summary>
    /// A lazily evaluated task that gives access to helper methods for the Web Audio API.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> webAudioHelperTask;

    /// <inheritdoc/>
    public static new async Task<AudioNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<AudioNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new AudioNode(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected AudioNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        webAudioHelperTask = new(jSRuntime.GetHelperAsync);
    }

    /// <summary>
    /// Connects the <see cref="AudioNode"/> to a destination <see cref="AudioNode"/>. There can only be one connection between a given output of one specific node and a given input of another specific node. Multiple connections with the same termini are ignored.
    /// </summary>
    /// <remarks>
    /// It is possible to connect an <see cref="AudioNode"/> output to more than one <see cref="AudioParam"/> with multiple calls to <see cref="ConnectAsync(AudioNode, ulong, ulong)"/>. Thus, "fan-out" is supported.<br />
    /// It is possible to connect more than one <see cref="AudioNode"/> output to a single <see cref="AudioParam"/> with multiple calls to <see cref="ConnectAsync(AudioNode, ulong, ulong)"/>. Thus, "fan-in" is supported.<br />
    /// It throws an <see cref="InvalidAccessErrorException"/> if <paramref name="destinationNode"/> is an <see cref="AudioNode"/> that has been created using another <see cref="AudioContext"/>.<br/>
    /// It throws an <see cref="IndexSizeErrorException"/> if the <paramref name="output"/> is out of bounds.<br />
    /// It throws an <see cref="IndexSizeErrorException"/> if the <paramref name="input"/> is out of bounds.
    /// </remarks>
    /// <param name="destinationNode">The destination parameter is the <see cref="AudioNode"/> to connect to.</param>
    /// <param name="output">The output parameter is an index describing which output of the <see cref="AudioNode"/> from which to connect.</param>
    /// <param name="input">The input parameter is an index describing which input of the destination <see cref="AudioNode"/> to connect to.</param>
    /// <exception cref="InvalidAccessErrorException" />
    /// <exception cref="IndexSizeErrorException" />
    /// <returns>This method returns destination AudioNode object.</returns>
    public async Task<AudioNode> ConnectAsync(AudioNode destinationNode, ulong output = 0, ulong input = 0)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("connect", destinationNode.JSReference, output, input);
        return await CreateAsync(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Connects the <see cref="AudioNode"/> to an <see cref="AudioParam"/>, controlling the parameter value with an a-rate signal.
    /// </summary>
    /// <remarks>
    /// It is possible to connect an <see cref="AudioNode"/> output to more than one <see cref="AudioParam"/> with multiple calls to <see cref="ConnectAsync(AudioNode, ulong, ulong)"/>. Thus, "fan-out" is supported.<br />
    /// It is possible to connect more than one <see cref="AudioNode"/> output to a single <see cref="AudioParam"/> with multiple calls to <see cref="ConnectAsync(AudioNode, ulong, ulong)"/>. Thus, "fan-in" is supported.<br />
    /// It throws an <see cref="InvalidAccessErrorException"/> if <paramref name="destinationParam"/> belongs to an <see cref="AudioNode"/> that belongs to a <see cref="BaseAudioContext"/> that is different from the <see cref="BaseAudioContext"/> that has created the <see cref="AudioNode"/> on which this method was called.<br />
    /// It throws an <see cref="IndexSizeErrorException"/> if the <paramref name="output"/> is out of bounds.
    /// </remarks>
    /// <param name="destinationParam">The destination parameter is the <see cref="AudioParam"/> to connect to.</param>
    /// <param name="output">The output parameter is an index describing which output of the <see cref="AudioNode"/> from which to connect.</param>
    /// <exception cref="InvalidAccessErrorException" />
    /// <exception cref="IndexSizeErrorException" />
    public async Task ConnectAsync(AudioParam destinationParam, ulong output = 0)
    {
        await JSReference.InvokeVoidAsync("connect", destinationParam.JSReference, output);
    }

    /// <summary>
    /// Disconnects all outgoing connections from the <see cref="AudioNode"/>.
    /// </summary>
    public async Task DisconnectAsync()
    {
        await JSReference.InvokeVoidAsync("disconnect");
    }

    /// <summary>
    /// Disconnects a single output of the <see cref="AudioNode"/> from any other <see cref="AudioNode"/> or <see cref="AudioParam"/> objects to which it is connected.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="IndexSizeErrorException"/> if the <paramref name="output"/> is out of bounds.
    /// </remarks>
    /// <param name="output">This parameter is an index describing which output of the <see cref="AudioNode"/> to disconnect. It disconnects all outgoing connections from the given output.</param>
    /// <exception cref="IndexSizeErrorException"></exception>
    public async Task DisconnectAsync(ulong output)
    {
        await JSReference.InvokeVoidAsync("disconnect", output);
    }

    /// <summary>
    /// Disconnects all outputs of the <see cref="AudioNode"/> that go to a specific destination <see cref="AudioNode"/>.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="InvalidAccessErrorException"/> if there is connection to the <paramref name="destinationNode"/>.
    /// </remarks>
    /// <param name="destinationNode">This parameter is the <see cref="AudioNode"/> to disconnect. It disconnects all outgoing connections to the given destinationNode.</param>
    /// <exception cref="InvalidAccessErrorException"></exception>
    public async Task DisconnectAsync(AudioNode destinationNode)
    {
        await JSReference.InvokeVoidAsync("disconnect", destinationNode.JSReference);
    }

    /// <summary>
    /// Disconnects a specific output of the <see cref="AudioNode"/> from any and all inputs of some destination <see cref="AudioNode"/>.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="InvalidAccessErrorException"/> if there is connection from the given <paramref name="output"/> to the <paramref name="destinationNode"/>.<br />
    /// It throws an <see cref="IndexSizeErrorException"/> if the <paramref name="output"/> is out of bounds.
    /// </remarks>
    /// <param name="destinationNode">This parameter is the <see cref="AudioNode"/> to disconnect.</param>
    /// <param name="output">This parameter is an index describing which output of the <see cref="AudioNode"/> from which to disconnect.</param>
    /// <exception cref="InvalidAccessErrorException"></exception>
    /// <exception cref="IndexSizeErrorException"></exception>
    public async Task DisconnectAsync(AudioNode destinationNode, ulong output)
    {
        await JSReference.InvokeVoidAsync("disconnect", destinationNode.JSReference, output);
    }

    /// <summary>
    /// Disconnects a specific output of the <see cref="AudioNode"/> from a specific input of some destination <see cref="AudioNode"/>.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="InvalidAccessErrorException"/> if there is no connection to the <paramref name="destinationNode"/> from the given <paramref name="output"/> to the given <paramref name="input"/>.<br />
    /// It throws an <see cref="IndexSizeErrorException"/> if the <paramref name="output"/> is out of bounds.<br />
    /// It throws an <see cref="IndexSizeErrorException"/> if the <paramref name="input"/> is out of bounds.
    /// </remarks>
    /// <param name="destinationNode">This parameter is the <see cref="AudioNode"/> to disconnect.</param>
    /// <param name="output">This parameter is an index describing which output of the <see cref="AudioNode"/> from which to disconnect.</param>
    /// <param name="input">This parameter is an index describing which input of the destination <see cref="AudioNode"/> to disconnect.</param>
    /// <exception cref="InvalidAccessErrorException"></exception>
    /// <exception cref="IndexSizeErrorException"></exception>
    public async Task DisconnectAsync(AudioNode destinationNode, ulong output, ulong input)
    {
        await JSReference.InvokeVoidAsync("disconnect", destinationNode.JSReference, output, input);
    }

    /// <summary>
    /// Disconnects all outputs of the <see cref="AudioNode"/> that go to a specific destination <see cref="AudioParam"/>.
    /// The contribution of this <see cref="AudioNode"/> to the computed parameter value goes to <c>0</c> when this operation takes effect.
    /// The intrinsic parameter value is not affected by this operation.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="InvalidAccessErrorException"/> if there is no connection to the <paramref name="destinationParam"/>.
    /// </remarks>
    /// <param name="destinationParam">This parameter is the <see cref="AudioParam"/> to disconnect.</param>
    /// <exception cref="InvalidAccessErrorException"></exception>
    public async Task DisconnectAsync(AudioParam destinationParam)
    {
        await JSReference.InvokeVoidAsync("disconnect", destinationParam.JSReference);
    }

    /// <summary>
    /// Disconnects a specific output of the <see cref="AudioNode"/> from a specific destination <see cref="AudioParam"/>.
    /// The contribution of this <see cref="AudioNode"/> to the computed parameter value goes to <c>0</c> when this operation takes effect.
    /// The intrinsic parameter value is not affected by this operation.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="InvalidAccessErrorException"/> if there is no connection to the <paramref name="destinationParam"/>.<br />
    /// It throws an <see cref="IndexSizeErrorException"/> if the <paramref name="output"/> is out of bounds.
    /// </remarks>
    /// <param name="destinationParam">This parameter is the <see cref="AudioParam"/> to disconnect.</param>
    /// <param name="output">This parameter is an index describing which output of the <see cref="AudioNode"/> from which to disconnect.</param>
    /// <exception cref="InvalidAccessErrorException"></exception>
    /// <exception cref="IndexSizeErrorException"></exception>
    public async Task DisconnectAsync(AudioParam destinationParam, ulong output)
    {
        await JSReference.InvokeVoidAsync("disconnect", destinationParam.JSReference, output);
    }

    /// <summary>
    /// Returns the <see cref="BaseAudioContext"/> which owns this <see cref="AudioNode"/>.
    /// </summary>
    public async Task<BaseAudioContext> GetContextAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "context");
        return await BaseAudioContext.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Returns the number of inputs feeding into the <see cref="AudioNode"/>.
    /// For source nodes, this will be <c>0</c>.
    /// </summary>
    /// <remarks>
    /// This attribute is predetermined for many <see cref="AudioNode"/> types, but some <see cref="AudioNode"/>s, like the <see cref="ChannelMergerNode"/> and the <see cref="AudioWorkletNode"/>, have variable number of inputs.
    /// </remarks>
    public async Task<ulong> GetNumberOfInputsAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "numberOfInputs");
    }

    /// <summary>
    /// Returns the number of outputs coming out of the <see cref="AudioNode"/>.
    /// </summary>
    /// <remarks>
    /// This attribute is predetermined for some <see cref="AudioNode"/> types, but can be variable, like for the <see cref="ChannelSplitterNode"/> and the <see cref="AudioWorkletNode"/>.
    /// </remarks>
    public async Task<ulong> GetNumberOfOutputsAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "numberOfOutputs");
    }

    /// <summary>
    /// Returns the number of channels used when up-mixing and down-mixing connections to any inputs to the node.
    /// The default value is <c>2</c> except for specific nodes where its value is specially determined.
    /// This attribute has no effect for nodes with no inputs.
    /// </summary>
    public async Task<ulong> GetChannelCountAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "channelCount");
    }

    /// <summary>
    /// Sets the number of channels used when up-mixing and down-mixing connections to any inputs to the node.
    /// The default value is <c>2</c> except for specific nodes where its value is specially determined.
    /// This attribute has no effect for nodes with no inputs.
    /// </summary>
    /// <remarks>
    /// It throws a <see cref="NotSupportedErrorException"/> if this <paramref name="value"/> is set to zero or to a value greater than the implementation’s maximum number of channels.
    /// </remarks>
    /// <exception cref="NotSupportedErrorException"></exception>
    public async Task SetChannelCountAsync(ulong value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "channelCount", value);
    }
}
