﻿using System.Globalization;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.AudioEditor;

internal static class FloatExtensions
{
    internal static string AsString(this float f)
    {
        return Math.Round(f, 4).ToString(CultureInfo.InvariantCulture);
    }
}