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
        _ = await audioNodeSlim.WaitAsync(200);
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
        _ = audioNodeSlim.Release();
        return audioNode;
    };

    public new float Height
    {
        get => 280;
        set => base.Height = 280;
    }

    public BiquadFilterType? Type
    {
        get => Element.GetAttribute("data-type") is { } value ? Deserialize<BiquadFilterType>($"\"{value}\"") : null;
        set
        {
            if (value is null)
            {
                _ = Element.RemoveAttribute("data-type");
            }
            else
            {
                Element.SetAttribute("data-type", Serialize(value.Value)[1..^1]);
            }
            Changed?.Invoke(this);
        }
    }

    public float? Q
    {
        get => Element.GetAttribute("data-Q") is { } value ? float.Parse(value, CultureInfo.InvariantCulture) : null;
        set
        {
            if (value is null)
            {
                _ = Element.RemoveAttribute("data-Q");
            }
            else
            {
                Element.SetAttribute("data-v", value.Value.AsString());
            }
            Changed?.Invoke(this);
        }
    }

    public float? Detune
    {
        get => Element.GetAttribute("data-detune") is { } value ? float.Parse(value, CultureInfo.InvariantCulture) : null;
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

    public float? Gain
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
