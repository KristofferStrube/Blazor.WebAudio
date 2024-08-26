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
    public int Width { get; set; } = 180;

    [Parameter]
    public string Color { get; set; } = "#000";

    protected override async Task OnAfterRenderAsync(bool _)
    {
        if (running || Analyser is null)
        {
            return;
        }

        running = true;

        int bufferLength = (int)await Analyser.GetFftSizeAsync();
        await using Uint8Array timeDomainData = await Uint8Array.CreateAsync(JSRuntime, bufferLength);

        while (running)
        {
            for (int i = 0; i < Width; i++)
            {
                if (!running)
                {
                    break;
                }

                await Analyser.GetByteTimeDomainDataAsync(timeDomainData);

                byte[] reading = await timeDomainData.GetAsArrayAsync();

                double amplitude = reading.Average(r => Math.Abs(r - 128)) / 128.0;

                await using (Context2D context = await canvas.GetContext2DAsync())
                {
                    if (i == 0)
                    {
                        await context.FillAndStrokeStyles.FillStyleAsync($"#fff");
                        await context.FillRectAsync(0, 0, Width * 10, Height * 10);
                    }

                    await context.FillAndStrokeStyles.FillStyleAsync($"#fff");
                    await context.FillRectAsync(i * 10, 0, 10, Height * 10);

                    await context.FillAndStrokeStyles.FillStyleAsync(Color);
                    await context.FillRectAsync(i * 10, (Height * 10 / 2.0) - (amplitude * Height * 10), 10, amplitude * 2 * Height * 10);
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