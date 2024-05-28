using AngleSharp.Dom;
using KristofferStrube.Blazor.SVGEditor;
using Microsoft.AspNetCore.Components.Web;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.ADSREditor;

public class ADSRLine : Polyline
{
    private const int minAttack = 2;
    private const int minDecay = 2;
    private const int minRelease = 2;
    private const int min = 0;
    private const int max = 100;

    public override Type Presenter => typeof(ADSRLineEditor);

    public ADSRLine(IElement element, SVGEditor.SVGEditor svg) : base(element, svg)
    {
    }

    public override void HandlePointerMove(PointerEventArgs eventArgs)
    {
        (double x, double y) = SVG.LocalDetransform((eventArgs.OffsetX, eventArgs.OffsetY));

        y = Math.Clamp(y, min, max);

        switch (SVG.CurrentAnchor)
        {
            case 1:
                if (x < minAttack)
                {
                    Points[1] = (minAttack, min);
                    UpdatePoints();
                    break;
                }
                Points[1] = (x, min);
                if (Points[2].x < x + minDecay)
                {
                    Points[2] = (x + minDecay, Points[2].y);
                }
                if (Points[3].x < x + minDecay + 20)
                {
                    Points[3] = (x + minDecay + 20, Points[3].y);
                }
                if (Points[4].x < x + minDecay + 20 + minRelease)
                {
                    Points[4] = (x + minDecay + 20 + minRelease, max);
                }
                UpdatePoints();
                break;
            case 2:
                if (x - minDecay < minAttack)
                {
                    Points[1] = (minAttack, min);
                    Points[2] = (minAttack + minDecay, y);
                    Points[3] = (minAttack + minDecay + 20, y);
                    UpdatePoints();
                    break;
                }
                Points[2] = (x, y);
                Points[3] = (x + 20, y);
                if (Points[4].x < x + 20 + minRelease)
                {
                    Points[4] = (x + 20 + minRelease, max);
                }
                if (Points[1].x > x - minDecay)
                {
                    Points[1] = (x - minDecay, min);
                }
                UpdatePoints();
                break;
            case 3:
                if (x - 20 - minDecay < minAttack)
                {
                    Points[1] = (minAttack, min);
                    Points[2] = (minAttack + minDecay, y);
                    Points[3] = (minAttack + minDecay + 20, y);
                    UpdatePoints();
                    break;
                }
                Points[2] = (x - 20, y);
                Points[3] = (x, y);
                if (Points[4].x < x + minRelease)
                {
                    Points[4] = (x + minRelease, max);
                }
                if (Points[1].x > x - 20 - minDecay)
                {
                    Points[1] = (x - 20 - minDecay, min);
                }
                UpdatePoints();
                break;
            case 4:
                if (x - 20 - minRelease - minDecay < minAttack)
                {
                    Points[1] = (minAttack, min);
                    Points[2] = (minAttack + minDecay, Points[2].y);
                    Points[3] = (minAttack + minDecay + 20, Points[3].y);
                    Points[4] = (minAttack + minDecay + 20 + minRelease, max);
                    UpdatePoints();
                    break;
                }
                Points[4] = (x, max);
                if (Points[3].x > x - minRelease)
                {
                    Points[3] = (x - minRelease, Points[3].y);
                }
                if (Points[2].x > x - 20 - minRelease)
                {
                    Points[2] = (x - 20 - minRelease, Points[2].y);
                }
                if (Points[1].x > x - 20 - minRelease - minDecay)
                {
                    Points[1] = (x - 20 - minRelease - minDecay, min);
                }
                UpdatePoints();
                break;
            default:
                break;
        }
    }

    private void UpdatePoints()
    {
        Element.SetAttribute("points", PointsToString(Points));
        Changed?.Invoke(this);
    }
}
