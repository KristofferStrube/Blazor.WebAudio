using KristofferStrube.Blazor.WebIDL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.Shared;

public partial class TimeDomainPlot : ComponentBase, IDisposable
{
    private bool running;
    private byte[] timeDomainMeasurements = Array.Empty<byte>();

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    [Parameter, EditorRequired]
    public AnalyserNode? Analyser { get; set; }

    [Parameter]
    public int Height { get; set; } = 200;

    [Parameter]
    public string Color { get; set; } = "red";

    protected override async Task OnAfterRenderAsync(bool _)
    {
        if (running || Analyser is null)
        {
            return;
        }

        running = true;

        int bufferLength = (int)await Analyser.GetFrequencyBinCountAsync();
        Uint8Array timeDomainDataArray = await Uint8Array.CreateAsync(JSRuntime, bufferLength);

        while (running)
        {
            await Analyser.GetByteTimeDomainDataAsync(timeDomainDataArray);
            try
            {
                timeDomainMeasurements = await timeDomainDataArray.GetAsArrayAsync();
            }
            catch
            {
                // If others try to retrieve a byte array at the same time Blazor will fail, so we simply just do nothing if it fails.
            }

            await Task.Delay(20);
            StateHasChanged();
        }
    }

    public void Dispose()
    {
        running = false;
        GC.SuppressFinalize(this);
    }
}