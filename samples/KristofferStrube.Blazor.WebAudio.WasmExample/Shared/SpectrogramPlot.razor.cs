using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;
using KristofferStrube.Blazor.CSSView;
using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.Window;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.Shared;

public partial class SpectrogramPlot : IDisposable
{
    private bool running;
    private byte[,]? data;
    private Canvas canvas = default!;

    private string canvasStyle => $"height:{Height}px;width:100%;";

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    [Parameter, EditorRequired]
    public AnalyserNode? Analyser { get; set; }

    [Parameter]
    public int Height { get; set; } = 200;

    [Parameter]
    public int Width { get; set; } = 200;

    [Parameter]
    public int TimeInSeconds { get; set; } = 20;

    [Parameter]
    public int LowerFrequency { get; set; } = 0;

    [Parameter]
    public int UpperFrequency { get; set; } = 100;

    protected override async Task OnAfterRenderAsync(bool _)
    {
        if (running || Analyser is null) return;
        running = true;

        data = new byte[UpperFrequency - LowerFrequency, 100];

        int bufferLength = (int)await Analyser.GetFrequencyBinCountAsync();
        Uint8Array frequencyDataArray = await Uint8Array.CreateAsync(JSRuntime, Math.Min(bufferLength, UpperFrequency - LowerFrequency));

        while (running)
        {
            for (int i = 0; i < TimeInSeconds * 10; i++)
            {
                if (!running) break;

                await Analyser.GetByteFrequencyDataAsync(frequencyDataArray);

                byte[] reading = await frequencyDataArray.GetAsArrayAsync();

                await using (Context2D context = await canvas.GetContext2DAsync())
                {
                    await context.ClearRectAsync(0, 0, Width / (double)TimeInSeconds / 10, Height);

                    for (int j = 0; j < reading.Length; j++)
                    {
                        string color = $"#F{(255 - reading[j]) / 16:X}{(255 - reading[j]) / 16:X}";
                        await context.FillAndStrokeStyles.FillStyleAsync(color);
                        await context.FillRectAsync(i / (double)TimeInSeconds / 10 * Width, j / (double)reading.Length * Height, Width / (double)TimeInSeconds / 10, 1);
                    }
                }
                await Task.Delay(1);
            }
        }
    }

    public void Dispose()
    {
        running = false;
        GC.SuppressFinalize(this);
    }
}