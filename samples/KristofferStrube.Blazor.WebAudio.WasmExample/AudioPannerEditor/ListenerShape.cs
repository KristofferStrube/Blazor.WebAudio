using AngleSharp.Dom;
using KristofferStrube.Blazor.SVGEditor;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioPannerEditor;

public class ListenerShape : Circle
{
    public ListenerShape(IElement element, SVGEditor.SVGEditor svg) : base(element, svg)
    {
    }

    public override Type Presenter => typeof(ListenerShapeEditor);

    public static ListenerShape AddNew(SVGEditor.SVGEditor SVG, double x, double y)
    {
        IElement element = SVG.Document.CreateElement("CIRCLE");

        ListenerShape circle = new(element, SVG)
        {
            Changed = SVG.UpdateInput,
            Fill = "lightgrey",
            Cx = x,
            Cy = y,
            R = 1
        };

        SVG.AddElement(circle);

        return circle;
    }
}
