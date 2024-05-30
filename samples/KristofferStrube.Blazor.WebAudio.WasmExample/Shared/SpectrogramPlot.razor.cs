using KristofferStrube.Blazor.WebIDL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.Shared;

public partial class SpectrogramPlot : IDisposable
{
    private bool running;
    private byte[,]? data;

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    [Parameter, EditorRequired]
    public AnalyserNode? Analyser { get; set; }

    [Parameter]
    public int Height { get; set; } = 200;

    [Parameter]
    public int TimeInSeconds { get; set; } = 4;

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
            for (int i = 0; i < 100; i++)
            {
                if (!running) break;

                await Analyser.GetByteFrequencyDataAsync(frequencyDataArray);

                await Task.Delay(1);

                if (i != 0) continue;

                byte[] reading = await frequencyDataArray.GetAsArrayAsync();

                for (int j = 0; j < reading.Length; j++)
                {
                    data[j, i] = reading[j];
                }

                StateHasChanged();
            }
        }
    }

    public void Dispose()
    {
        running = false;
        GC.SuppressFinalize(this);
    }
}