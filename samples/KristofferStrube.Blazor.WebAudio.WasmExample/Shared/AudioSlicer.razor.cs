using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;
using KristofferStrube.Blazor.CSSView;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.Shared;

public partial class AudioSlicer : ComponentBase, IDisposable
{
    private Canvas canvas = default!;
    private ElementReference wrapper;
    private readonly List<float> amplitudes = [];

    private string canvasStyle => $"height:{Height}px;width:100%;user-select:none;touch-action:none;";

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    [Parameter, EditorRequired]
    public required AudioBuffer AudioBuffer { get; set; }

    [Parameter]
    public int Height { get; set; } = 200;

    [Parameter]
    public string Color { get; set; } = "#000";

    [Parameter]
    public string MarkColor { get; set; } = "#F00";

    [Parameter]
    public bool PlaySoundWhenEndingMark { get; set; } = true;

    [Parameter]
    public double MarkedOffset { get; set; }

    [Parameter]
    public EventCallback<double> MarkedOffsetChanged { get; set; }

    [Parameter]
    public double MarkedDuration { get; set; }

    [Parameter]
    public EventCallback<double> MarkedDurationChanged { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;

        ulong numberOfChannels = await AudioBuffer.GetNumberOfChannelsAsync();
        ulong length = await AudioBuffer.GetLengthAsync();
        float sampleRate = await AudioBuffer.GetSampleRateAsync();
        ulong samples = (ulong)(sampleRate * 0.001f);

        var data = new Float32Array[numberOfChannels];
        for (ulong i = 0; i < numberOfChannels; i++)
        {
            data[i] = await AudioBuffer.GetChannelDataAsync(i);
        }

        for (ulong i = 0; i < length; i += samples)
        {
            // We intentionally only take 1 value from each sample slice as we only need this for a visualization
            float amplitude = 0;
            for (ulong k = 0; k < numberOfChannels; k++)
            {
                amplitude += await data[k].AtAsync((long)i);
            }
            amplitudes.Add(amplitude / samples / numberOfChannels);
        }

        for (ulong i = 0; i < numberOfChannels; i++)
        {
            await data[i].DisposeAsync();
        }

        await InvokeAsync(StateHasChanged);

        await RenderCanvas();
    }

    private async Task RenderCanvas()
    {
        double left = start < end ? start : end;
        double right = end < start ? start : end;

        await using Context2D context = await canvas.GetContext2DAsync();

        await context.FillAndStrokeStyles.FillStyleAsync($"#fff");
        await context.FillRectAsync(0, 0, amplitudes.Count, Height);

        float maxAmplitude = amplitudes.Max();

        for (int i = 0; i < amplitudes.Count; i++)
        {
            double percentage = i / (double)amplitudes.Count;
            string color = start != 0 && end != 1 && left < percentage && percentage < right ? MarkColor : Color;
            float amplitude = amplitudes[i];
            await context.FillAndStrokeStyles.FillStyleAsync(color);
            await context.FillRectAsync(i, (Height / 2.0) - (amplitude / maxAmplitude / 2 * Height), 1, amplitude / maxAmplitude * Height);
        }
    }

    private double start = 0;
    private double end = 1;
    private bool down = false;

    private async Task PointerDown(PointerEventArgs eventArgs)
    {
        await using Element wrapperElement = await Element.CreateAsync(JSRuntime, wrapper);
        var clientRect = await wrapperElement.GetBoundingClientRectAsync();
        var x = eventArgs.OffsetX;
        start = x / clientRect.Width;
        end = x / clientRect.Width;
        down = true;
        await RenderCanvas();
    }

    private async Task PointerMove(PointerEventArgs eventArgs)
    {
        if (!down)
            return;
        await UpdateEnd(eventArgs.OffsetX);
        await RenderCanvas();
    }

    private async Task PointerStop(PointerEventArgs eventArgs)
    {
        if (!down)
            return;
        down = false;
        await UpdateEnd(eventArgs.OffsetX);
        await RenderCanvas();
        await PlayMarkedSound();
    }

    private async Task PlayMarkedSound()
    {
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);

        AudioBufferSourceOptions options = new()
        {
            Buffer = AudioBuffer,
            Loop = false
        };
        await using AudioBufferSourceNode source = await AudioBufferSourceNode.CreateAsync(JSRuntime, context, options);

        await using AudioDestinationNode destination = await context.GetDestinationAsync();
        await source.ConnectAsync(destination);

        double bufferDuration = await AudioBuffer.GetDurationAsync();

        double left = start < end ? start : end;
        double right = end < start ? start : end;

        double offset = left * bufferDuration;
        double duration = (right - left) * bufferDuration;

        await source.StartAsync(0, offset, duration);

        await MarkedOffsetChanged.InvokeAsync(offset);
        await MarkedDurationChanged.InvokeAsync(duration);
    }

    private async Task UpdateEnd(double x)
    {
        await using Element wrapperElement = await Element.CreateAsync(JSRuntime, wrapper);
        var clientRect = await wrapperElement.GetBoundingClientRectAsync();
        end = x / clientRect.Width;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}