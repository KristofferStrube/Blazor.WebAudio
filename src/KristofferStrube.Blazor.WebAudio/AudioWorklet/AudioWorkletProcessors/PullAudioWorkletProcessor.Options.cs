namespace KristofferStrube.Blazor.WebAudio;

public partial class PullAudioWorkletProcessor
{
    public class Options
    {
        public int LowTide { get; set; } = 100;
        public int HighTide { get; set; } = 500;
        public int BufferRequestSize { get; set; } = 100;
        public required Func<double> Produce { get; set; }
    }
}
