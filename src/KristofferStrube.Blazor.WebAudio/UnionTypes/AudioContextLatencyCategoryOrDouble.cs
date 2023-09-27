using KristofferStrube.Blazor.WebAudio.UnionTypes;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// A value that is either a <see cref="AudioContextLatencyCategory"/> or a <see cref="double"/>.
/// </summary>
public class AudioContextLatencyCategoryOrDouble : UnionType
{
    /// <summary>
    /// Creates an <see cref="AudioContextLatencyCategoryOrDouble"/> from an <see cref="AudioContextLatencyCategory"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="AudioContextLatencyCategory"/>.</param>
    public AudioContextLatencyCategoryOrDouble(AudioContextLatencyCategory value) : base(value) { }

    /// <summary>
    /// Creates an <see cref="AudioContextLatencyCategoryOrDouble"/> from a <see cref="double"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="double"/>.</param>
    public AudioContextLatencyCategoryOrDouble(double value) : base(value) { }

    internal AudioContextLatencyCategoryOrDouble(object value) : base(value) { }

    /// <summary>
    /// Creates an <see cref="AudioContextLatencyCategoryOrDouble"/> from an <see cref="AudioContextLatencyCategory"/>.
    /// </summary>
    /// <param name="value">An <see cref="AudioContextLatencyCategory"/>.</param>
    public static implicit operator AudioContextLatencyCategoryOrDouble(AudioContextLatencyCategory value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates an <see cref="AudioContextLatencyCategoryOrDouble"/> from a <see cref="double"/>.
    /// </summary>
    /// <param name="value">A <see cref="double"/>.</param>
    public static implicit operator AudioContextLatencyCategoryOrDouble(double value)
    {
        return new(value);
    }
}
