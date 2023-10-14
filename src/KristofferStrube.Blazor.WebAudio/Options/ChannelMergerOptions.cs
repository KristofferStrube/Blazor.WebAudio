using KristofferStrube.Blazor.WebIDL.Exceptions;

namespace KristofferStrube.Blazor.WebAudio.Options;


/// <summary>
/// This specifies the options to use in constructing a <see cref="ChannelMergerNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#ChannelMergerOptions">See the API definition here</see>.</remarks>
public class ChannelMergerOptions : AudioNodeOptions
{
    /// <summary>
    /// The number inputs for the <see cref="ChannelMergerNode"/>.
    /// </summary>
    /// <remarks>
    /// It throws an <see cref="IndexSizeErrorException"/> if it is less than <c>1</c> or larger than the supported number of channels when used for constructing a <see cref="ChannelMergerNode"/>.
    /// </remarks>
    public ulong NumberOfInputs { get; set; } = 6;
}
