using System.Globalization;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor;

internal static class DoubleExtensions
{
    internal static string AsString(this double d)
    {
        return Math.Round(d, 9).ToString(CultureInfo.InvariantCulture);
    }
}