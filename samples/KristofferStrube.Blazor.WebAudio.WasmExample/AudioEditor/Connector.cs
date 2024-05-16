using AngleSharp.Dom;
using KristofferStrube.Blazor.SVGEditor;
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

    public bool IsHovered { get; set; } = false;

    public override string StateRepresentation => base.StateRepresentation + IsHovered.ToString();

    private Node? from;
    public Node? From
    {
        get
        {
            // If not previous loaded, then load from attributes and connect node-node or node-audioparam pairs that it connects.
            if (from is null)
            {
                var fromNode = (Node?)SVG.Elements.FirstOrDefault(e => e is Node && e.Id == Element.GetAttribute("data-from-node"));
                _ = fromNode?.OutgoingConnectors.Add(this);
                if (fromNode is null)
                {
                    return null;
                }

                QueueConnect(fromNode, To);

                from = fromNode;
            }
            return from;
        }
        set
        {
            // We don't support updating the from-node to some new value.

            // If the new value if null then remove its attribute
            if (value is null)
            {
                _ = Element.RemoveAttribute("data-from-node");
                from = null;
            }
            else // If it is actually a new value
            {
                Element.SetAttribute("data-from-node", value.Id);
                from = value;
                if (value is { } newNode )
                {
                    _ = value.OutgoingConnectors.Add(this);

                    QueueConnect(newNode, To);
                }
            }
            Changed?.Invoke(this);
        }
    }

    private (Node node, string? audioParamIdentifier)? to;
    public (Node node, string? audioParamIdentifier)? To
    {
        get
        {
            // If not previous loaded, then load from attributes.
            if (to is null)
            {
                var toNode = (Node?)SVG.Elements.FirstOrDefault(e => e is Node && e.Id == Element.GetAttribute("data-to-node"));

                string? audioParamIdentifier = Element.GetAttribute("data-to-audioparam");

                _ = toNode?.IngoingConnectors.Add(this);

                to = toNode is null ? null : (toNode, audioParamIdentifier);
            }
            return to;
        }
        set
        {
            // We don't support updating the to-node to some new value.

            if (value is { } newValue) // If it is actually a new value
            {
                Element.SetAttribute("data-to-node", newValue.node.Id);

                if (newValue.audioParamIdentifier is not null)
                {
                    Element.SetAttribute("data-to-audioparam", newValue.audioParamIdentifier);
                }

                to = value;
                _ = newValue.node.IngoingConnectors.Add(this);
                if (From is not null)
                {
                    QueueConnect(From, newValue);
                }
            }
            else // If the new value if null then remove its attribute 
            {
                _ = Element.RemoveAttribute("data-to-node");
                to = null;
            }
            Changed?.Invoke(this);
        }
    }

    private void QueueConnect(Node fromNode, (Node node, string? audioParamIdentifier)? to)
    {
        if (to is { node: { } toNode })
        {
            if (to is { audioParamIdentifier: { } paramIdentifier })
            {
                QueuedTasks.Enqueue(async context => await (await fromNode.AudioNode(context)).ConnectAsync(await toNode.AudioParams[paramIdentifier](context)));
            }
            else
            {
                QueuedTasks.Enqueue(async context => await (await fromNode.AudioNode(context)).ConnectAsync(await toNode.AudioNode(context)));
            }
        }
    }

    public override void HandlePointerMove(PointerEventArgs eventArgs)
    {
        if (SVG.EditMode is EditMode.Add)
        {
            (X2, Y2) = SVG.LocalDetransform((eventArgs.OffsetX, eventArgs.OffsetY));
            double fromX = From!.X + From!.Width;
            double fromY = From!.Y + 1 * 20;
            SetStart((fromX, fromY), (X2, Y2));
        }
    }

    public override void HandlePointerUp(PointerEventArgs eventArgs)
    {
        if (SVG.EditMode is EditMode.Add
            && SVG.SelectedShapes.FirstOrDefault(s => s is Node node && node != From) is Node { } to)
        {
            if (to.IngoingConnectors.Any(c => c.To?.node == From || c.From == From))
            {
                Complete();
            }
            else
            {
                To = (to, to.CurrentActiveAudioParamIdentifier);
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

    public static void AddNew(SVGEditor.SVGEditor SVG, Node fromNode)
    {
        IElement element = SVG.Document.CreateElement("LINE");
        element.SetAttribute("data-elementtype", "connector");

        Connector edge = new(element, SVG)
        {
            Changed = SVG.UpdateInput,
            Stroke = "black",
            StrokeWidth = "5",
            From = fromNode
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
            X1 = from.x + 10;
            Y1 = from.y;
        }
    }

    public void UpdateLine()
    {
        if (From is null || To is null)
        {
            (X1, Y1) = (X2, Y2);
            return;
        }

        double fromX = From!.X + From!.Width;
        double fromY = From!.Y + 20;
        double toX = To!.Value.node.X;
        double toY = To!.Value.node.Y + (To?.node.Offset(To?.audioParamIdentifier) ?? 20);
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
            X2 = toX - 10;
            Y2 = toY;
        }
    }

    public override void BeforeBeingRemoved()
    {
        if (From is { } from)
        {
            _ = from.OutgoingConnectors.Remove(this);
            from.QueuedTasks.Enqueue(async context => await (await from.AudioNode(context)).DisconnectAsync());
        }
        if (To is { } to)
        {
            _ = to.node.IngoingConnectors.Remove(this);
        }
    }
}