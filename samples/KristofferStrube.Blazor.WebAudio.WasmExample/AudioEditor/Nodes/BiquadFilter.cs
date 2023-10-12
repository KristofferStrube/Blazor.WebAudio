using AngleSharp.Dom;
using KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor.NodeEditors;
using System.Globalization;
using static System.Text.Json.JsonSerializer;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor;

public class BiquadFilter : Node
{
    public BiquadFilter(IElement element, SVGEditor.SVGEditor svg) : base(element, svg) { }

    private AudioNode? audioNode;
    public override Func<AudioContext, Task<AudioNode>> AudioNode => async (context) =>
    {
        if (audioNode is null)
        {
            BiquadFilterOptions options = new();
            options.Type = Type ?? options.Type;
            options.Q = Q ?? options.Q;
            options.Detune = Detune ?? options.Detune;
            options.Frequency = Frequency ?? options.Frequency;
            options.Gain = Gain ?? options.Gain;

            BiquadFilterNode oscillator = await BiquadFilterNode.CreateAsync(context.JSRuntime, context, options);
            audioNode = oscillator;
        }
        return audioNode;
    };


    public new float Height
    {
        get => 280;
        set
        {
            base.Height = 280;
        }
    }

    public BiquadFilterType? Type
    {
        get
        {
            return Element.GetAttribute("data-type") is { } value ? Deserialize<BiquadFilterType>(value) : null;
        }
        set
        {
            if (value is null)
            {
                _ = Element.RemoveAttribute("data-type");
            }
            else
            {
                Element.SetAttribute("data-type", Serialize(value.Value));
            }
            Changed?.Invoke(this);
        }
    }

    public float? Q
    {
        get
        {
            return Element.GetAttribute("data-q") is { } value ? float.Parse(value, CultureInfo.InvariantCulture) : null;
        }
        set
        {
            if (value is null)
            {
                _ = Element.RemoveAttribute("data-q");
            }
            else
            {
                Element.SetAttribute("data-q", value.Value.AsString());
            }
            Changed?.Invoke(this);
        }
    }

    public float? Detune
    {
        get
        {
            return Element.GetAttribute("data-detune") is { } value ? float.Parse(value, CultureInfo.InvariantCulture) : null;
        }
        set
        {
            if (value is null)
            {
                _ = Element.RemoveAttribute("data-detune");
            }
            else
            {
                Element.SetAttribute("data-detune", value.Value.AsString());
            }
            Changed?.Invoke(this);
        }
    }

    public float? Frequency
    {
        get
        {
            return Element.GetAttribute("data-frequnecy") is { } value ? float.Parse(value, CultureInfo.InvariantCulture) : null;
        }
        set
        {
            if (value is null)
            {
                _ = Element.RemoveAttribute("data-frequnecy");
            }
            else
            {
                Element.SetAttribute("data-frequnecy", value.Value.AsString());
            }
            Changed?.Invoke(this);
        }
    }

    public float? Gain
    {
        get
        {
            return Element.GetAttribute("data-gain") is { } value ? float.Parse(value, CultureInfo.InvariantCulture) : null;
        }
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

    public override Type Presenter => typeof(BiquadFilterEditor);

    public static new void AddNew(SVGEditor.SVGEditor SVG)
    {
        IElement element = SVG.Document.CreateElement("RECT");
        element.SetAttribute("data-elementtype", "biquad-filter");

        BiquadFilter node = new(element, SVG)
        {
            Changed = SVG.UpdateInput,
            Stroke = "#FFEE58",
            StrokeWidth = "2",
            Height = 280,
            Width = 250,
        };

        (node.X, node.Y) = SVG.LocalDetransform(SVG.LastRightClick);

        SVG.ClearSelectedShapes();
        SVG.SelectShape(node);
        SVG.AddElement(node);
    }
}
