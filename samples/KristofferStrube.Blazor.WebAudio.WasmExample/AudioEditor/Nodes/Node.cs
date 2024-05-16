using AngleSharp.Dom;
using KristofferStrube.Blazor.SVGEditor;
using Microsoft.AspNetCore.Components.Web;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor;

public abstract class Node : Rect, ITaskQueueable
{
    public Dictionary<string, Func<AudioContext, Task<AudioParam>>> AudioParams { get; set; } = new();
    public virtual Dictionary<string, int> AudioParamPositions { get; set; } = new();

    public int Offset(string? identifier = null) => identifier is not null && AudioParamPositions.TryGetValue(identifier, out int offset) ? offset : 20;

    public Node(IElement element, SVGEditor.SVGEditor svg) : base(element, svg)
    {
        string? id = element.GetAttribute("id");
        if (id is null || svg.Elements.Any(e => e.Id == id))
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    public Queue<Func<AudioContext, Task>> QueuedTasks { get; } = new();

    public override string Fill
    {
        get
        {
            int[] parts = Stroke[1..].Chunk(2).Select(part => int.Parse(part, System.Globalization.NumberStyles.HexNumber)).ToArray();
            return "#" + string.Join("", parts.Select(part => Math.Min(255, part + 50).ToString("X2")));
        }
    }

    public new float Width
    {
        get => 250;
        set => base.Width = 250;
    }

    public new float Height
    {
        get => 100;
        set => base.Height = 100;
    }

    public abstract Func<AudioContext, Task<AudioNode>> AudioNode { get; }

    public HashSet<Connector> IngoingConnectors { get; } = new();
    public HashSet<Connector> OutgoingConnectors { get; } = new();

    public string? CurrentActiveAudioParamIdentifier { get; set; }

    public override void HandlePointerMove(PointerEventArgs eventArgs)
    {
        base.HandlePointerMove(eventArgs);
        if (SVG.EditMode is EditMode.Move)
        {
            foreach (Connector connector in IngoingConnectors)
            {
                connector.UpdateLine();
            }
            foreach (Connector connector in OutgoingConnectors)
            {
                connector.UpdateLine();
            }
        }
    }

    public override void BeforeBeingRemoved()
    {
        foreach (Connector connector in IngoingConnectors)
        {
            SVG.RemoveElement(connector);
        }
        foreach (Connector connector in OutgoingConnectors)
        {
            SVG.RemoveElement(connector);
        }
    }
}