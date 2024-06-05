using AngleSharp.Dom;
using KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor.NodeEditors;
using System.Globalization;
using static System.Text.Json.JsonSerializer;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor;

public class Oscillator : Node
{
    public Oscillator(IElement element, SVGEditor.SVGEditor svg) : base(element, svg)
    {
        AudioParams = new()
        {
            ["frequency"] = async (audioContext) => await ((OscillatorNode)await AudioNode(audioContext)).GetFrequencyAsync()
        };
    }

    public override Dictionary<string, int> AudioParamPositions { get; set; } = new()
    {
        ["frequency"] = 40
    };

    private AudioNode? audioNode;

    public override Func<AudioContext, Task<AudioNode>> AudioNode => async (context) =>
    {
        _ = await audioNodeSlim.WaitAsync(200);
        if (audioNode is null)
        {
            OscillatorOptions options = new();
            if (Frequency is { } f)
            {
                options.Frequency = f;
            }
            if (Type is { } t)
            {
                options.Type = t;
            }
            OscillatorNode oscillator = await OscillatorNode.CreateAsync(context.JSRuntime, context, options);
            await oscillator.StartAsync();
            audioNode = oscillator;
        }
        _ = audioNodeSlim.Release();
        return audioNode;
    };

    public new float Height
    {
        get => 130;
        set => base.Height = 130;
    }

    public OscillatorType? Type
    {
        get => Element.GetAttribute("data-type") is { } value ? Deserialize<OscillatorType>($"\"{value}\"") : null;
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
            Height = 150,
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
