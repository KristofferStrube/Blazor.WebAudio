using KristofferStrube.Blazor.WebAudio.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

[JsonConverter(typeof(ChannelInterpretationConverter))]
public enum ChannelInterpretation
{
    Speakers,
    Discrete,
}