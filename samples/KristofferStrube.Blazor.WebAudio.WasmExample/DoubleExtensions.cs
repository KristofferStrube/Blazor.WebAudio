using System.Globalization;

namespace KristofferStrube.Blazor.WebAudio.WasmExample;

public static class DoubleExtensions
{
    public static string AsString(this double value)
    {
        return value.ToString(CultureInfo.InvariantCulture);
    }
}
