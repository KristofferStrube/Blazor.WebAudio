﻿using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies options for constructing a <see cref="ConvolverNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#ConvolverOptions">See the API definition here</see>.</remarks>
public class ConvolverOptions : AudioNodeOptions
{
    /// <summary>
    /// The desired buffer for the <see cref="ConvolverNode"/>. This buffer will be normalized according to the value of <see cref="DisableNormalization"/>.
    /// </summary>
    [JsonPropertyName("buffer")]
    public AudioBuffer? Buffer { get; set; }

    /// <summary>
    /// The opposite of the desired initial value for <see cref="ConvolverNode.GetNormalizeAsync"/>.
    /// </summary>
    /// <remarks>
    /// The default value is <see langword="false"/>.
    /// </remarks>
    [JsonPropertyName("disableNormalization")]
    public bool DisableNormalization { get; set; } = false;

    /// <inheritdoc path="/summary"/>
    /// <remarks>
    /// The default value is <see cref="ChannelCountMode.ClampedMax"/>.
    /// </remarks>
    public override ChannelCountMode ChannelCountMode { get; set; } = ChannelCountMode.ClampedMax;
}
