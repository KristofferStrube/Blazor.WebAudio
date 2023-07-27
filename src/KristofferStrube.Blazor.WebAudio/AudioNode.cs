using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// <see cref="AudioNode"/>s are the building blocks of an <see cref="AudioContext"/>.
/// This interface represents audio sources, the audio destination, and intermediate processing modules.
/// These modules can be connected together to form processing graphs for rendering audio to the audio hardware.
/// Each node can have inputs and/or outputs. A source node has no inputs and a single output.
/// Most processing nodes such as filters will have one input and one output.
/// Each type of <see cref="AudioNode"/> differs in the details of how it processes or synthesizes audio.s
/// But, in general, an <see cref="AudioNode"/> will process its inputs (if it has any), and generate audio for its outputs (if it has any).
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioNode">See the API definition here</see>.</remarks>
public class AudioNode : EventTarget
{
    /// <summary>
    /// A lazily evaluated task that gives access to helper methods for the Web Audio API.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> webAudioHelperTask;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioNode"/>.</param>
    /// <returns>A wrapper instance for a <see cref="AudioNode"/>.</returns>
    public static new Task<AudioNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AudioNode(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioNode"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioNode"/>.</param>
    protected AudioNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        webAudioHelperTask = new(jSRuntime.GetHelperAsync);
    }

    /// <summary>
    /// Connects the <see cref="AudioNode"/> to a destination <see cref="AudioNode"/>. There can only be one connection between a given output of one specific node and a given input of another specific node. Multiple connections with the same termini are ignored.
    /// </summary>
    /// <remarks>
    /// It is possible to connect an <see cref="AudioNode"/> output to more than one <see cref="AudioParam"/> with multiple calls to <see cref="ConnectAsync(AudioNode, ulong, ulong)"/>. Thus, "fan-out" is supported.<br />
    /// It is possible to connect more than one <see cref="AudioNode"/> output to a single <see cref="AudioParam"/> with multiple calls to <see cref="ConnectAsync(AudioNode, ulong, ulong)"/>. Thus, "fan-in" is supported.<br />
    /// If the <paramref name="destinationNode"/> parameter is an <see cref="AudioNode"/> that has been created using another <see cref="AudioContext"/>, an <see cref="NotSupportedErrorException"/> will be thrown. That is, <see cref="AudioNode"/>s cannot be shared between <see cref="AudioContext"/>s.<br />
    /// If the <paramref name="output"/> parameter is out-of-bounds, an <see cref="RangeErrorException"/> exception will be thrown.<br />
    /// If the <paramref name="input"/> parameter is out-of-bounds, an <see cref="RangeErrorException"/> exception will be thrown.<br />
    /// </remarks>
    /// <param name="destinationNode">The destination parameter is the <see cref="AudioNode"/> to connect to.</param>
    /// <param name="output">The output parameter is an index describing which output of the <see cref="AudioNode"/> from which to connect.</param>
    /// <param name="input">The input parameter is an index describing which input of the destination <see cref="AudioNode"/> to connect to.</param>
    /// <exception cref="NotSupportedErrorException" />
    /// <exception cref="RangeErrorException" />
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
    /// If the <paramref name="output"/> parameter is out-of-bounds, an <see cref="RangeErrorException"/> exception will be thrown.<br />
    /// </remarks>
    /// <param name="destinationParam">The destination parameter is the <see cref="AudioParam"/> to connect to.</param>
    /// <param name="output">The output parameter is an index describing which output of the <see cref="AudioNode"/> from which to connect.</param>
    /// <exception cref="NotSupportedErrorException" />
    /// <exception cref="RangeErrorException" />
    /// <returns>This method returns destination AudioNode object.</returns>
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
}
