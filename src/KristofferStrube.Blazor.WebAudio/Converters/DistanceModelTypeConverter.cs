using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Converters;

internal class DistanceModelTypeConverter : JsonConverter<DistanceModelType>
{
    public override DistanceModelType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "linear" => DistanceModelType.Linear,
            "inverse" => DistanceModelType.Inverse,
            "exponential" => DistanceModelType.Exponential,
            var value => throw new ArgumentException($"Value '{value}' was not a valid {nameof(DistanceModelType)}.")
        };
    }

    public override void Write(Utf8JsonWriter writer, DistanceModelType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            DistanceModelType.Linear => "linear",
            DistanceModelType.Inverse => "inverse",
            DistanceModelType.Exponential => "exponential",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(DistanceModelType)}.")
        });
    }
}
