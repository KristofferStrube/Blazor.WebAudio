using AngleSharp.Dom;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor;

public class AudioDestination : Node
{
    public AudioDestination(IElement element, SVGEditor.SVGEditor svg) : base(element, svg) { }

    private AudioNode? audioNode;
    public override Func<AudioContext, Task<AudioNode>> AudioNode => async (context) =>
    {
        audioNode ??= await context.GetDestinationAsync();
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
            StrokeWidth = "2"
        };

        (node.X, node.Y) = SVG.LocalDetransform(SVG.LastRightClick);

        SVG.ClearSelectedShapes();
        SVG.SelectShape(node);
        SVG.AddElement(node);
    }
}
