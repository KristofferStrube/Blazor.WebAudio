using AngleSharp.Dom;
using KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor.NodeEditors;
using System.Globalization;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor;

public class Gain : Node
{
    public Gain(IElement element, SVGEditor.SVGEditor svg) : base(element, svg) { }

    private AudioNode? audioNode;
    public override Func<AudioContext, Task<AudioNode>> AudioNode => async (context) =>
    {
        if (audioNode is null)
        {
            GainOptions options = new();
            if (GainValue is { } g)
            {
                options.Gain = g;
            }
            GainNode oscillator = await GainNode.CreateAsync(context.JSRuntime, context, options);
            audioNode = oscillator;
        }
        return audioNode;
    };

    public float? GainValue
    {
        get => Element.GetAttribute("data-gain") is { } value ? float.Parse(value, CultureInfo.InvariantCulture) : null;
        set
        {
            if (value is null)
            {
                _ = Element.RemoveAttribute("data-gain");
            }
            else
            {
                Element.SetAttribute("data-gain", value.Value.AsString());
            }
            Changed?.Invoke(this);
        }
    }

    public override Type Presenter => typeof(GainEditor);

    public static new void AddNew(SVGEditor.SVGEditor SVG)
    {
        IElement element = SVG.Document.CreateElement("RECT");
        element.SetAttribute("data-elementtype", "gain");

        Gain node = new(element, SVG)
        {
            Changed = SVG.UpdateInput,
            Stroke = "#EE534F",
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
