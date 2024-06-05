﻿using AngleSharp.Dom;
using KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor.NodeEditors;
namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor;

public class Analyser : Node
{
    public Analyser(IElement element, SVGEditor.SVGEditor svg) : base(element, svg) { }

    private AudioNode? audioNode;
    public override Func<AudioContext, Task<AudioNode>> AudioNode => async (context) =>
    {
        _ = await audioNodeSlim.WaitAsync(200);
        if (audioNode is null)
        {
            AnalyserNode analyser = await AnalyserNode.CreateAsync(context.JSRuntime, context);

            audioNode = analyser;
        }
        _ = audioNodeSlim.Release();
        return audioNode;
    };

    public new float Height
    {
        get => 130;
        set => base.Height = 130;
    }

    public string Type
    {
        get => Element.GetAttribute("data-type");
        set
        {
            if (value is null)
            {
                _ = Element.RemoveAttribute("data-type");
            }
            else
            {
                Element.SetAttribute("data-type", value);
            }
            Changed?.Invoke(this);
        }
    }

    public bool Running { get; set; }

    public override Type Presenter => typeof(AnalyserEditor);

    public static new void AddNew(SVGEditor.SVGEditor SVG)
    {
        IElement element = SVG.Document.CreateElement("RECT");
        element.SetAttribute("data-elementtype", "analyser");

        Analyser node = new(element, SVG)
        {
            Changed = SVG.UpdateInput,
            Stroke = "#9CCC66",
            StrokeWidth = "2",
            Height = 130,
            Width = 250,
            Type = "TimeDomain",
        };

        (node.X, node.Y) = SVG.LocalDetransform(SVG.LastRightClick);

        SVG.ClearSelectedShapes();
        SVG.SelectShape(node);
        SVG.AddElement(node);
    }
}
