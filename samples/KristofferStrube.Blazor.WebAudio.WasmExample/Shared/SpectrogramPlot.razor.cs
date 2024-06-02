using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.Shared;

public partial class SpectrogramPlot : ComponentBase, IDisposable
{
    private bool running;
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

    protected override async Task OnAfterRenderAsync(bool _)
    {
        if (running || Analyser is null) return;
        running = true;

        int bufferLength = (int)await Analyser.GetFrequencyBinCountAsync();
        await using Uint8Array frequencyDataArray = await Uint8Array.CreateAsync(JSRuntime, bufferLength);

        while (running)
        {
            for (int i = 0; i < Width; i++)
            {
                if (!running) break;

                await Analyser.GetByteFrequencyDataAsync(frequencyDataArray);

                byte[] reading = await frequencyDataArray.GetAsArrayAsync();

                await using (Context2D context = await canvas.GetContext2DAsync())
                {
                    await context.FillAndStrokeStyles.FillStyleAsync($"#fff");
                    await context.FillRectAsync(i, 0, 1, Height);

                    for (int j = 0; j < reading.Length; j++)
                    {
                        string color = $"#F{(255 - reading[j]) / 16:X}{(255 - reading[j]) / 16:X}";
                        await context.FillAndStrokeStyles.FillStyleAsync(color);
                        await context.FillRectAsync(i, j / (double)reading.Length * Height, 1, 1);
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