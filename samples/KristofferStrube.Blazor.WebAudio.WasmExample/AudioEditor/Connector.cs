using AngleSharp.Dom;
using KristofferStrube.Blazor.SVGEditor;
using KristofferStrube.Blazor.SVGEditor.Extensions;
using Microsoft.AspNetCore.Components.Web;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor;

public class Connector : Line, ITaskQueueable
{
    public Connector(IElement element, SVGEditor.SVGEditor svg) : base(element, svg)
    {
        UpdateLine();
    }

    public override Type Presenter => typeof(ConnectorEditor);

    public Queue<Func<AudioContext, Task>> QueuedTasks { get; } = new();

    public (Node node, ulong port)? From
    {
        get
        {
            var fromNode = (Node?)SVG.Elements.FirstOrDefault(e => e is Node && e.Id == Element.GetAttribute("data-from-node"));
            ulong fromPort = (ulong)Element.GetAttributeOrZero("data-from-port");
            _ = fromNode?.OutgoingConnectors.Add((this, fromPort));
            if (fromNode is null) return null;
            if (To is { } to)
            {
                QueuedTasks.Enqueue(async context => await (await fromNode.AudioNode(context)).ConnectAsync(await to.node.AudioNode(context), fromPort, to.port));
            }
            return (fromNode, fromPort);
        }
        set
        {
            if (From is { } from)
            {
                _ = from.node.OutgoingConnectors.Remove((this, from.port));
                QueuedTasks.Enqueue(async context => await (await from.node.AudioNode(context)).DisconnectAsync(from.port));
            }
            if (value is null)
            {
                _ = Element.RemoveAttribute("data-from-node");
                _ = Element.RemoveAttribute("data-from-port");
            }
            else
            {
                Element.SetAttribute("data-from-node", value.Value.node.Id);
                Element.SetAttribute("data-from-port", value.Value.port.ToString());
                _ = value.Value.node.OutgoingConnectors.Add((this, value.Value.port));
                if (To is { } to)
                {
                    QueuedTasks.Enqueue(async context => await (await value.Value.node.AudioNode(context)).ConnectAsync(await to.node.AudioNode(context), value.Value.port, to.port));
                }
            }
            Changed?.Invoke(this);
        }
    }


    public (Node node, ulong port)? To
    {
        get
        {
            var toNode = (Node?)SVG.Elements.FirstOrDefault(e => e is Node && e.Id == Element.GetAttribute("data-to-node"));
            ulong toPort = (ulong)Element.GetAttributeOrZero("data-to-port");
            _ = toNode?.OutgoingConnectors.Add((this, toPort));
            return toNode is null ? null : (toNode, toPort);
        }
        set
        {
            if (To is { } to)
            {
                _ = to.node.IngoingConnectors.Remove((this, to.port));
                if (From is { } from)
                {
                    QueuedTasks.Enqueue(async context => await (await from.node.AudioNode(context)).DisconnectAsync(from.port));
                }
            }
            if (value is null)
            {
                _ = Element.RemoveAttribute("data-to-node");
                _ = Element.RemoveAttribute("data-to-port");
            }
            else
            {
                Element.SetAttribute("data-to-node", value.Value.node.Id);
                Element.SetAttribute("data-to-port", value.Value.port.ToString());
                _ = value.Value.node.IngoingConnectors.Add((this, value.Value.port));
                if (From is { } from)
                {
                    QueuedTasks.Enqueue(async context => await (await from.node.AudioNode(context)).ConnectAsync(await value.Value.node.AudioNode(context), from.port, value.Value.port));
                }
            }
            Changed?.Invoke(this);
        }
    }

    public override void HandlePointerMove(PointerEventArgs eventArgs)
    {
        if (SVG.EditMode is EditMode.Add)
        {
            (X2, Y2) = SVG.LocalDetransform((eventArgs.OffsetX, eventArgs.OffsetY));
            double fromX = From!.Value.node.X + From!.Value.node.Width;
            double fromY = From!.Value.node.Y + (From!.Value.port * 20) + 20;
            SetStart((fromX, fromY), (X2, Y2));
        }
    }

    public override void HandlePointerUp(PointerEventArgs eventArgs)
    {
        if (SVG.EditMode is EditMode.Add
            && SVG.SelectedShapes.FirstOrDefault(s => s is Node node && node != From?.node) is Node { } to)
        {
            if (to.IngoingConnectors.Any(c => c.connector.To?.node == From?.node || c.connector.From?.node == From?.node))
            {
                Complete();
            }
            else
            {
                To = (to, 0);
                SVG.EditMode = EditMode.None;
                UpdateLine();
            }
        }
    }

    public override void Complete()
    {
        if (To is null)
        {
            SVG.RemoveElement(this);
            Changed?.Invoke(this);
        }
    }

    public static void AddNew(SVGEditor.SVGEditor SVG, Node fromNode, ulong fromPort)
    {
        IElement element = SVG.Document.CreateElement("LINE");
        element.SetAttribute("data-elementtype", "connector");

        Connector edge = new(element, SVG)
        {
            Changed = SVG.UpdateInput,
            Stroke = "black",
            StrokeWidth = "5",
            From = (fromNode, fromPort)
        };
        SVG.EditMode = EditMode.Add;

        SVG.ClearSelectedShapes();
        SVG.SelectShape(edge);
        SVG.AddElement(edge);
    }

    public void SetStart((double x, double y) from, (double x, double y) towards)
    {
        double differenceX = towards.x - from.x;
        double differenceY = towards.y - from.y;
        double distance = Math.Sqrt((differenceX * differenceX) + (differenceY * differenceY));

        if (distance > 0)
        {
            X1 = from.x + (differenceX / distance * 10);
            Y1 = from.y + (differenceY / distance * 10);
        }
    }

    public void UpdateLine()
    {
        if (From is null || To is null)
        {
            (X1, Y1) = (X2, Y2);
            return;
        }

        double fromX = From!.Value.node.X + From!.Value.node.Width;
        double fromY = From!.Value.node.Y + (From!.Value.port * 20) + 20;
        double toX = To!.Value.node.X;
        double toY = To!.Value.node.Y + (To!.Value.port * 20) + 20;
        double differenceX = toX - fromX;
        double differenceY = toY - fromY;
        double distance = Math.Sqrt((differenceX * differenceX) + (differenceY * differenceY));

        if (distance < 20)
        {
            (X1, Y1) = (X2, Y2);
        }
        else
        {
            SetStart((fromX, fromY), (toX, toY));
            X2 = toX - (differenceX / distance * 10);
            Y2 = toY - (differenceY / distance * 10);
        }
    }

    public override void BeforeBeingRemoved()
    {
        if (From is { } from)
        {
            _ = from.node.OutgoingConnectors.Remove((this, from.port));
            from.node.QueuedTasks.Enqueue(async context => await (await from.node.AudioNode(context)).DisconnectAsync(from.port));
        }
        if (To is { } to)
        {
            _ = to.node.IngoingConnectors.Remove((this, to.port));
        }
    }
}