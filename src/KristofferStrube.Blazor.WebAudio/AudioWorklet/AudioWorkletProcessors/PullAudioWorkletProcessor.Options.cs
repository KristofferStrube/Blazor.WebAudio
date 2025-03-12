namespace KristofferStrube.Blazor.WebAudio;

public partial class PullAudioWorkletProcessor
{
    /// <summary>
    /// Options for creating <see cref="PullAudioWorkletProcessor"/>.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Low tide for when the processor should request more chunks.
        /// </summary>
        public int LowTide { get; set; } = 10;

        /// <summary>
        /// High tide for when the processor should begin discarding chunks.
        /// </summary>
        public int HighTide { get; set; } = 50;

        /// <summary>
        /// Size of each individual chunk.
        /// </summary>
        public int BufferRequestSize { get; set; } = 10;

        /// <summary>
        /// Resolution used for transfering chunks.
        /// </summary>
        public Resolution Resolution { get; set; } = Resolution.Double;

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

    /// <summary>
    /// The resolution options for how the quality of the sound of the <see cref="PullAudioWorkletProcessor"/>.
    /// </summary>
    public enum Resolution
    {
        /// <summary>
        /// This is the full resolution from the producing methods.
        /// </summary>
        Double,

        /// <summary>
        /// This down-grades the produced sound to 255 discrete values to make the datatransfer more compact.
        /// </summary>
        Byte,
    }
}
