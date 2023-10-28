using AngleSharp.Dom;
using KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor.NodeEditors;
using System.Globalization;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor;

public class Oscillator : Node
{
    public Oscillator(IElement element, SVGEditor.SVGEditor svg) : base(element, svg) { }

    private AudioNode? audioNode;
    public override Func<AudioContext, Task<AudioNode>> AudioNode => async (context) =>
    {
        if (audioNode is null)
        {
            OscillatorOptions options = new();
            if (Frequency is { } f)
            {
                options.Frequency = f;
            }
            OscillatorNode oscillator = await OscillatorNode.CreateAsync(context.JSRuntime, context, options);
            await oscillator.StartAsync();
            audioNode = oscillator;
        }
        return audioNode;
    };

    public float? Frequency
    {
        get => Element.GetAttribute("data-frequency") is { } value ? float.Parse(value, CultureInfo.InvariantCulture) : null;
        set
        {
            if (value is null)
            {
                _ = Element.RemoveAttribute("data-frequency");
            }
            else
            {
                Element.SetAttribute("data-frequency", value.Value.AsString());
            }
            Changed?.Invoke(this);
        }
    }

    public override Type Presenter => typeof(OscillatorEditor);

    public static new void AddNew(SVGEditor.SVGEditor SVG)
    {
        IElement element = SVG.Document.CreateElement("RECT");
        element.SetAttribute("data-elementtype", "oscillator");

        Oscillator node = new(element, SVG)
        {
            Changed = SVG.UpdateInput,
            Stroke = "#28B6F6",
            StrokeWidth = "2",
            Height = 100,
            Width = 250,
        };

        (node.X, node.Y) = SVG.LocalDetransform(SVG.LastRightClick);

        SVG.ClearSelectedShapes();
        SVG.SelectShape(node);
        SVG.AddElement(node);
    }

    public override async void BeforeBeingRemoved()
    {
        if (audioNode is OscillatorNode { } oscillator)
        {
            await oscillator.StopAsync();
        }
    }
}
