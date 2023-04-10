using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Converters;

public class ChannelCountModeConverter : JsonConverter<ChannelCountMode>
{
    public override ChannelCountMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
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
