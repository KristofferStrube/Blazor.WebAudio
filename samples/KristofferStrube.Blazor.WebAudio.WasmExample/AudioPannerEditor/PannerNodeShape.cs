using AngleSharp.Dom;
using KristofferStrube.Blazor.SVGEditor;
using Microsoft.AspNetCore.Components.Web;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioPannerEditor;

public class PannerNodeShape : Rect
{
    public PannerNodeShape(IElement element, SVGEditor.SVGEditor svg) : base(element, svg)
    {
    }

    public override Type Presenter => typeof(PannerNodeShapeEditor);

    public double Rotation { get; set; }

    public override string StateRepresentation => base.StateRepresentation + Rotation;

    public override void HandlePointerMove(PointerEventArgs eventArgs)
    {
        if (SVG.CurrentAnchor is not 4 || SVG.EditMode is not EditMode.MoveAnchor)
        {
            base.HandlePointerMove(eventArgs);
            return;
        }

        (double x, double y) = SVG.LocalDetransform((eventArgs.OffsetX, eventArgs.OffsetY));

        (double x, double y) rotationVector = 
            (
                x - (X + (Width / 2)),
                y - (Y + (Height / 2))
            );

        Rotation = (-Math.Atan(rotationVector.x / rotationVector.y) * 180 / Math.PI) + (rotationVector.y < 0 ? 180 : 0);
    }

    public static PannerNodeShape AddNew(SVGEditor.SVGEditor SVG, double x, double y, double rotation, string color)
    {
        IElement element = SVG.Document.CreateElement("RECT");

        PannerNodeShape pannerNode = new(element, SVG)
        {
            Changed = SVG.UpdateInput,
            StrokeWidth = "0",
            Fill = color,
            Rotation = rotation
        };
        (pannerNode.X, pannerNode.Y) = (x, y);
        (pannerNode.Width, pannerNode.Height) = (2, 2);

        SVG.AddElement(pannerNode);

        return pannerNode;
    }
}
