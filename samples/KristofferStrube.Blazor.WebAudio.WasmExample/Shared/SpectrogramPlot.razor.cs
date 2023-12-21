using KristofferStrube.Blazor.WebIDL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using CommunityToolkit.HighPerformance;
using System;

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

        DateTimeOffset start = DateTimeOffset.UtcNow;
        DateTimeOffset lastTime = DateTimeOffset.UtcNow;
        while (running)
        {
            await Analyser.GetByteFrequencyDataAsync(frequencyDataArray);
            byte[] reading = await frequencyDataArray.GetByteArrayAsync();

            DateTimeOffset currentTime = DateTimeOffset.UtcNow;
            int intervalStart = (int)((lastTime - start).TotalMilliseconds / TimeInSeconds / 10);
            int intervalEnd = (int)((currentTime - start).TotalMilliseconds / TimeInSeconds / 10);
            lastTime = currentTime;

            for (int i = intervalStart; i < intervalEnd; i++)
            {
                if (i > 99)
                {
                    start = DateTimeOffset.UtcNow;
                    lastTime = start;
                    break;
                }
                for (int j = 0; j < reading.Length; j++)
                {
                    data[j, i] = reading[j];
                }
                //Memory2D<byte> readingAs2dMemory = reading.AsMemory2D(UpperFrequency - LowerFrequency, 1);
                //Memory2D<byte> viewInData = buffer.Slice(0, i, UpperFrequency - LowerFrequency, 1);
                //readingAs2dMemory.CopyTo(viewInData);
            }

            StateHasChanged();
            await Task.Delay(10);

        }
    }

    public void Dispose()
    {
        running = false;
    }
}