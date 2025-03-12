using BlazorServer;
using KristofferStrube.Blazor.MediaCaptureStreams;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace IntegrationTests.Infrastructure;

public class AudioContextEvaluationContext(IJSRuntime jSRuntime, IMediaDevicesService mediaDevicesService) : EvaluationContext
{
    public IJSRuntime JSRuntime => jSRuntime;

    public IMediaDevicesService MediaDevicesService => mediaDevicesService;

    public static AudioContextEvaluationContext Create(IServiceProvider provider)
    {
        IMediaDevicesService mediaDevicesService = provider.GetRequiredService<IMediaDevicesService>();
        IJSRuntime jSRuntime = provider.GetRequiredService<IJSRuntime>();

        return new AudioContextEvaluationContext(jSRuntime, mediaDevicesService);
    }

    public async Task<AudioContext> GetAudioContext()
    {
        AudioContext audioContext = await AudioContext.CreateAsync(jSRuntime);
        return audioContext;
    }

    public async Task<MediaStream> GetMediaStream()
    {
        await using MediaDevices mediaDevices = await mediaDevicesService.GetMediaDevicesAsync();
        MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new() { Audio = true });
        return mediaStream;
    }

    public async Task<IJSObjectReference> GetAudioElementAyns()
    {
        return await JSRuntime.InvokeAsync<IJSObjectReference>("getAudioElement");
    }
}
