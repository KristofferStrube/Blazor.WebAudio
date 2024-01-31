using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Converters;

internal class OscillatorTypeConverter : JsonConverter<OscillatorType>
{
    public override OscillatorType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "sine" => OscillatorType.Sine,
            "square" => OscillatorType.Square,
            "sawtooth" => OscillatorType.Sawtooth,
            "triangle" => OscillatorType.Triangle,
            "custom" => OscillatorType.Custom,
            var value => throw new ArgumentException($"Value '{value}' was not a valid {nameof(OscillatorType)}."),
        };
    }

    public override void Write(Utf8JsonWriter writer, OscillatorType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            OscillatorType.Sine => "sine",
            OscillatorType.Square => "square",
            OscillatorType.Sawtooth => "sawtooth",
            OscillatorType.Triangle => "triangle",
            OscillatorType.Custom => "custom",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(OscillatorType)}.")
        });
    }
}
