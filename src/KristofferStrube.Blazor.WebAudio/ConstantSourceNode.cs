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

    private ConstantSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task<AudioParam> GetOffsetAsync() => default!;
}
