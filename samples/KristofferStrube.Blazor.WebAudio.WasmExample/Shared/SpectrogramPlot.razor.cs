using KristofferStrube.Blazor.WebIDL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.Shared;

public partial class SpectrogramPlot : IDisposable
{
    private bool running;
    private readonly List<byte[]> data = [];
    private readonly List<(double start, double length)> intervals = [];

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



    protected override void OnParametersSet()
    {

    }

    protected override async Task OnAfterRenderAsync(bool _)
    {
        if (running || Analyser is null) return;
        running = true;

        int bufferLength = (int)await Analyser.GetFrequencyBinCountAsync();
        Uint8Array frequencyDataArray = await Uint8Array.CreateAsync(JSRuntime, Math.Min(bufferLength, UpperFrequency - LowerFrequency));

        DateTimeOffset start = DateTimeOffset.UtcNow;
        DateTimeOffset lastTime = DateTimeOffset.UtcNow;
        while (running)
        {
            await Analyser.GetByteFrequencyDataAsync(frequencyDataArray);
            data.Add((await frequencyDataArray.GetByteArrayAsync())[LowerFrequency..UpperFrequency]);

            DateTimeOffset currentTime = DateTimeOffset.UtcNow;
            var intervalLength = (currentTime - lastTime).TotalMilliseconds;
            intervals.Add(((lastTime - start).TotalMilliseconds, intervalLength));

            if ((currentTime - start).TotalMilliseconds > TimeInSeconds * 1000)
            {
                start = DateTimeOffset.UtcNow;
                data.Clear();
                intervals.Clear();
            }
            lastTime = currentTime;

            StateHasChanged();
            await Task.Delay(Math.Max(1, 100 - (int)intervalLength));

        }
    }

    public void Dispose()
    {
        running = false;
    }
}