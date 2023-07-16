using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class ConstantSourceNode : AudioScheduledSourceNode
{
    protected ConstantSourceNode(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }
}
