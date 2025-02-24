using BlazorServer;
using KristofferStrube.Blazor.MediaCaptureStreams;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace IntegrationTests.Infrastructure;

public class AudioMediaStreamEvaluationContext(IJSRuntime jSRuntime, IMediaDevicesService mediaDevicesService) : EvaluationContext, IEvaluationContext<AudioMediaStreamEvaluationContext>
{
    public IJSRuntime JSRuntime => jSRuntime;

    public IMediaDevicesService MediaDevicesService => mediaDevicesService;

    public static AudioMediaStreamEvaluationContext Create(IServiceProvider provider)
    {
        IMediaDevicesService mediaDevicesService = provider.GetRequiredService<IMediaDevicesService>();
        IJSRuntime jSRuntime = provider.GetRequiredService<IJSRuntime>();

        return new AudioMediaStreamEvaluationContext(jSRuntime, mediaDevicesService);
    }

    public async Task<MediaStream> GetMediaStream()
    {
        await using MediaDevices mediaDevices = await mediaDevicesService.GetMediaDevicesAsync();
        MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new() { Audio = true });
        return mediaStream;
    }
}
