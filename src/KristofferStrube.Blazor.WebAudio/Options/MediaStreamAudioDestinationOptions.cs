namespace KristofferStrube.Blazor.WebAudio.Options;

/// <summary>
/// This specifies the options to use in constructing a <see cref="MediaStreamAudioDestinationNode"/>.
/// </summary>
/// <remarks>
/// This type doesn't exist in the specs, but the <see cref="MediaStreamAudioDestinationNode"/> has a non-standard value for the <see cref="AudioNodeOptions.ChannelCountMode"/> attribute which is why this type is added in this wrapper.<br />
/// <see href="https://www.w3.org/TR/webaudio/#MediaStreamAudioSourceOptions">See the API definition here</see>.
/// </remarks>
public class MediaStreamAudioDestinationOptions : AudioNodeOptions
{
    /// <inheritdoc path="/summary"/>
    /// <remarks>
    /// The default value is <see cref="ChannelCountMode.Explicit"/>.
    /// </remarks>
    public override ChannelCountMode ChannelCountMode { get; set; } = ChannelCountMode.Explicit;
}
