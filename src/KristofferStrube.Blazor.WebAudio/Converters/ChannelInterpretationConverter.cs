using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Converters;

public class ChannelInterpretationConverter : JsonConverter<ChannelInterpretation>
{
    public override ChannelInterpretation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ChannelInterpretation value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            ChannelInterpretation.Speakers => "speakers",
            ChannelInterpretation.Discrete => "discrete",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(ChannelInterpretation)}.")
        });
    }
}
