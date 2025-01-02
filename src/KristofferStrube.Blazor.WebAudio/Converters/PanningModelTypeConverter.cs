using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Converters;

internal class PanningModelTypeConverter : JsonConverter<PanningModelType>
{
    public override PanningModelType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "equalpower" => PanningModelType.EqualPower,
            "HRTF" => PanningModelType.HRTF,
            var value => throw new ArgumentException($"Value '{value}' was not a valid {nameof(PanningModelType)}.")
        };
    }

    public override void Write(Utf8JsonWriter writer, PanningModelType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            PanningModelType.EqualPower => "equalpower",
            PanningModelType.HRTF => "HRTF",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(PanningModelType)}.")
        });
    }
}
