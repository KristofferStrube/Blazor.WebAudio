using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.Shared;

public partial class AmplitudePlot : ComponentBase, IDisposable
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
        await using Uint8Array timeDomainData = await Uint8Array.CreateAsync(JSRuntime, bufferLength);

        while (running)
        {
            for (int i = 0; i < Width; i++)
            {
                if (!running) break;

                await Analyser.GetByteFrequencyDataAsync(timeDomainData);

                byte[] reading = await timeDomainData.GetAsArrayAsync();

                await using (Context2D context = await canvas.GetContext2DAsync())
                {
                    await context.FillAndStrokeStyles.FillStyleAsync($"#fff");
                    await context.FillRectAsync(i, 0, 1, Height);

                    double height = reading.Sum(r => r) / (reading.Length * 255.0);

                    await context.FillAndStrokeStyles.FillStyleAsync($"#000");
                    await context.FillRectAsync(i, (Height / 2.0) - (height / 2 * Height), 1, height * Height);
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