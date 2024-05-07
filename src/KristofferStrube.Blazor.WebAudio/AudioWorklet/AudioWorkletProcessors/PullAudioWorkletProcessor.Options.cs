namespace KristofferStrube.Blazor.WebAudio;

public partial class PullAudioWorkletProcessor
{
    public class Options
    {
        public int LowTide { get; set; } = 100;
        public int HighTide { get; set; } = 500;
        public int BufferRequestSize { get; set; } = 100;

        /// <summary>
        /// A functions that will be used to pull data to play in the audio processor as mono audio
        /// </summary>
        /// <remarks>
        /// If <see cref="ProduceStereo"/> is supplied then this will be ignored.
        /// </remarks>
        public Func<double>? ProduceMono { get; set; }

        /// <summary>
        /// A functions that will be used to pull data to play in the audio processor as stereo audio.
        /// </summary>
        public Func<(double left, double right)>? ProduceStereo { get; set; }
    }
}
