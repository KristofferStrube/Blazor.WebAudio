using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Converters;

internal class RequestCredentialsConverter : JsonConverter<RequestCredentials>
{
    public override RequestCredentials Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, RequestCredentials value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            RequestCredentials.Omit => "omit",
            RequestCredentials.SameOrigin => "same-origin",
            RequestCredentials.Include => "include",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(RequestCredentials)}.")
        });
    }
}
