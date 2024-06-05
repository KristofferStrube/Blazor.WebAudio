using AngleSharp.Dom;
using KristofferStrube.Blazor.MediaCaptureStreams;
using KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor.NodeEditors;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor;

public class MediaStreamAudioSource : Node
{
    public MediaStreamAudioSource(IElement element, SVGEditor.SVGEditor svg) : base(element, svg) { }

    private AudioNode? audioNode;
    public override Func<AudioContext, Task<AudioNode>> AudioNode => async (context) =>
    {
        _ = await audioNodeSlim.WaitAsync(200);
        if (audioNode is null)
        {
            await SetMediaStreamAudioSourceNode(context);
        }
        return audioNode!;
        _ = audioNodeSlim.Release();
    };

    public async Task SetMediaStreamAudioSourceNode(AudioContext context)
    {

        MediaDevicesService mediaDevicesService = new(context.JSRuntime);
        MediaDevices mediaDevices = await mediaDevicesService.GetMediaDevicesAsync();

        MediaTrackConstraints mediaTrackConstraints = new()
        {
            AutoGainControl = false,
            DeviceId = SelectedAudioSource is null ? null : new ConstrainDomString(SelectedAudioSource)
        };
        MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new MediaStreamConstraints() { Audio = mediaTrackConstraints });

        MediaDeviceInfo[] deviceInfos = await mediaDevices.EnumerateDevicesAsync();
        AudioOptions.Clear();
        foreach (MediaDeviceInfo device in deviceInfos)
        {
            if (await device.GetKindAsync() is MediaDeviceKind.AudioInput)
            {
                AudioOptions.Add((await device.GetLabelAsync(), await device.GetDeviceIdAsync()));
            }
        }

        AudioNode? oldAudioNode = audioNode;
        audioNode = await context.CreateMediaStreamSourceAsync(mediaStream);

        await StopAsync(oldAudioNode);
        if (oldAudioNode is not null)
        {
            foreach (Connector connector in OutgoingConnectors)
            {
                connector.From = this;
            }
        }
    }

    public string? SelectedAudioSource { get; set; }
    public List<(string label, string id)> AudioOptions { get; set; } = new();

    public override Type Presenter => typeof(MediaStreamAudioSourceEditor);

    public static new void AddNew(SVGEditor.SVGEditor SVG)
    {
        IElement element = SVG.Document.CreateElement("RECT");
        element.SetAttribute("data-elementtype", "media-stream-audio-source");

        MediaStreamAudioSource node = new(element, SVG)
        {
            Changed = SVG.UpdateInput,
            Stroke = "#28B6F6",
            StrokeWidth = "2",
            Height = 100,
            Width = 250,
        };

        (node.X, node.Y) = SVG.LocalDetransform(SVG.LastRightClick);

        SVG.ClearSelectedShapes();
        SVG.SelectShape(node);
        SVG.AddElement(node);
    }

    public override async void BeforeBeingRemoved()
    {
        await StopAsync(audioNode);
    }

    public async Task StopAsync(AudioNode? audioNodeToStop)
    {
        if (audioNodeToStop is not null)
        {
            await audioNodeToStop!.DisconnectAsync();
            MediaStream oldMediaStream = await ((MediaStreamAudioSourceNode)audioNodeToStop).GetMediaStreamAsync();
            MediaStreamTrack[] oldAudioTracks = await oldMediaStream.GetAudioTracksAsync();
            await oldAudioTracks.First().StopAsync();
        }
    }
}
