using AngleSharp.Dom;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor;

public class AudioDestination : Node
{
    public AudioDestination(IElement element, SVGEditor.SVGEditor svg) : base(element, svg) { }

    private AudioNode? audioNode;
    public override Func<AudioContext, Task<AudioNode>> AudioNode => async (context) =>
    {
        _ = await audioNodeSlim.WaitAsync(200);
        audioNode ??= await context.GetDestinationAsync();
        _ = audioNodeSlim.Release();
        return audioNode;
    };

    public override Type Presenter => typeof(NodeEditors.AudioDestinationEditor);

    public static new void AddNew(SVGEditor.SVGEditor SVG)
    {
        IElement element = SVG.Document.CreateElement("RECT");
        element.SetAttribute("data-elementtype", "audio-destination");

        AudioDestination node = new(element, SVG)
        {
            Changed = SVG.UpdateInput,
            Stroke = "#EC407A",
            StrokeWidth = "2",
            Height = 100,
            Width = 250,
        };

        (node.X, node.Y) = SVG.LocalDetransform(SVG.LastRightClick);

        SVG.ClearSelectedShapes();
        SVG.SelectShape(node);
        SVG.AddElement(node);
    }
}
