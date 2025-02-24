using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Converters;

internal class OverSampleTypeConverter : JsonConverter<OverSampleType>
{
    public override OverSampleType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "none" => OverSampleType.None,
            "2x" => OverSampleType.TwoX,
            "4x" => OverSampleType.FourX,
            var value => throw new ArgumentException($"Value '{value}' was not a valid {nameof(OverSampleType)}.")
        };
    }

    public override void Write(Utf8JsonWriter writer, OverSampleType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            OverSampleType.None => "none",
            OverSampleType.TwoX => "2x",
            OverSampleType.FourX => "4x",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(OverSampleType)}.")
        });
    }
}
