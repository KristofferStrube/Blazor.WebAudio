using KristofferStrube.Blazor.MediaCaptureStreams;
using KristofferStrube.Blazor.WebAudio.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents an audio graph whose <see cref="AudioDestinationNode"/> is routed to a real-time output device that produces a signal directed at the user.
/// In most use cases, only a single <see cref="AudioContext"/> is used per document.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioContext">See the API definition here</see>.</remarks>
public class AudioContext : BaseAudioContext
{
    /// <summary>
    /// Creates an <see cref="AudioContext"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="contextOptions">User-specified options controlling how the <see cref="AudioContext"/> should be constructed.</param>
    /// <returns>A new instance of a <see cref="AudioContext"/>.</returns>
    public static async Task<AudioContext> CreateAsync(IJSRuntime jSRuntime, AudioContext? contextOptions = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructAudioContext", contextOptions);
        return new AudioContext(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AudioContext"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AudioContext"/>.</param>
    protected AudioContext(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    /// <summary>
    /// Resumes the progression of the <see cref="AudioContext"/>'s currentTime when it has been suspended.
    /// </summary>
    public async Task ResumeAsync()
    {
        await JSReference.InvokeVoidAsync("resume");
    }

    /// <summary>
    /// Suspends the progression of <see cref="AudioContext"/>'s currentTime, allows any current context processing blocks that are already processed to be played to the destination, and then allows the system to release its claim on audio hardware. 
    /// </summary>
    /// <remarks>
    /// This is generally useful when the application knows it will not need the <see cref="AudioContext"/> for some time, and wishes to temporarily release system resource associated with the <see cref="AudioContext"/>.
    /// </remarks>
    public async Task SuspendAsync()
    {
        await JSReference.InvokeVoidAsync("suspend");
    }

    /// <summary>
    /// Closes the AudioContext, releasing the system resources being used.
    /// </summary>
    /// <remarks>
    /// This will not automatically release all <see cref="AudioContext"/>-created objects, but will suspend the progression of the <see cref="AudioContext"/>'s currentTime, and stop processing audio data.
    /// </remarks>
    public async Task CloseAsync()
    {
        await JSReference.InvokeVoidAsync("close");
    }

    /// <summary>
    /// Creates a <see cref="MediaStreamAudioSourceNode"/>.
    /// </summary>
    /// <param name="mediaStream">The media stream that will act as source.</param>
    /// <returns>A new <see cref="MediaStreamAudioSourceNode"/>.</returns>
    public async Task<MediaStreamAudioSourceNode> CreateMediaStreamSourceAsync(MediaStream mediaStream)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("createMediaStreamSource", mediaStream.JSReference);
        return await MediaStreamAudioSourceNode.CreateAsync(JSRuntime, jSInstance);
    }
}
