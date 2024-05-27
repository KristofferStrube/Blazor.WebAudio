using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Converters;

internal class ChannelCountModeConverter : JsonConverter<ChannelCountMode>
{
    public override ChannelCountMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "max" => ChannelCountMode.Max,
            "clamped-max" => ChannelCountMode.ClampedMax,
            "explicit" => ChannelCountMode.Explicit,
            var value => throw new ArgumentException($"Value '{value}' was not a valid {nameof(ChannelCountMode)}.")
        };
    }

    public override void Write(Utf8JsonWriter writer, ChannelCountMode value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            ChannelCountMode.Max => "max",
            ChannelCountMode.ClampedMax => "clamped-max",
            ChannelCountMode.Explicit => "explicit",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(ChannelCountMode)}.")
        });
    }
}
