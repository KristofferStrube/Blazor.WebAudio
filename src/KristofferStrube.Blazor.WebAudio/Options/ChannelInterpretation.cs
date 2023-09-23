using KristofferStrube.Blazor.WebAudio.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// An enum for specifying how individual channels will be treated when up-mixing and down-mixing connections to any inputs to the node.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#enumdef-channelinterpretation">See the API definition here</see>.</remarks>
[JsonConverter(typeof(ChannelInterpretationConverter))]
public enum ChannelInterpretation
{
    /// <summary>
    /// Use up-mix equations or down-mix equations.
    /// In cases where the number of channels do not match any of these basic speaker layouts, revert to <see cref="Discrete"/>.
    /// </summary>
    Speakers,
    /// <summary>
    /// Up-mix by filling channels until they run out then zero out remaining channels.
    /// Down-mix by filling as many channels as possible, then dropping remaining channels.
    /// </summary>
    Discrete,
}