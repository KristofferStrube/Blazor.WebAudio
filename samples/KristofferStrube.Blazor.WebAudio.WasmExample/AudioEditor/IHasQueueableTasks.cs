namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor;

public interface ITaskQueueable
{
    public Queue<Func<AudioContext, Task>> QueuedTasks { get; }
}
