using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio.Converters;

internal class BiquadFilterTypeConverter : JsonConverter<BiquadFilterType>
{
    public override BiquadFilterType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "highpass" => BiquadFilterType.Highpass,
            "lowpass" => BiquadFilterType.Lowpass,
            "bandpass" => BiquadFilterType.Bandpass,
            "lowshelf" => BiquadFilterType.Lowshelf,
            "highshelf" => BiquadFilterType.Highshelf,
            "peaking" => BiquadFilterType.Peaking,
            "notch" => BiquadFilterType.Notch,
            "allpass" => BiquadFilterType.Allpass,
            var value => throw new ArgumentException($"Value '{value}' was not a valid {nameof(BiquadFilterType)}."),
        };
    }

    public override void Write(Utf8JsonWriter writer, BiquadFilterType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            BiquadFilterType.Highpass => "highpass",
            BiquadFilterType.Lowpass => "lowpass",
            BiquadFilterType.Bandpass => "bandpass",
            BiquadFilterType.Lowshelf => "lowshelf",
            BiquadFilterType.Highshelf => "highshelf",
            BiquadFilterType.Peaking => "peaking",
            BiquadFilterType.Notch => "notch",
            BiquadFilterType.Allpass => "allpass",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(BiquadFilterType)}.")
        });
    }
}
