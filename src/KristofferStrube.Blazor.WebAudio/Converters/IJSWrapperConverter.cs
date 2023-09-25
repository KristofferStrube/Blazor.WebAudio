using KristofferStrube.Blazor.WebIDL;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Converters;

public class IJSWrapperConverter<TWrapper> : JsonConverter<TWrapper> where TWrapper : IJSWrapper
{
    public override TWrapper Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, TWrapper value, JsonSerializerOptions options)
    {
        writer.WriteRawValue(JsonSerializer.Serialize(value.JSReference, options));
    }
}
