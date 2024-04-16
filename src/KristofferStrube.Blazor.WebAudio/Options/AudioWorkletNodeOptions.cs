using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This specifies the options to be used when constructing an <see cref="AudioWorkletNode"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#dictdef-audioworkletnodeoptions">See the API definition here</see>.</remarks>
public class AudioWorkletNodeOptions : AudioNodeOptions
{
    /// <summary>
    /// This is used to initialize the value of <see cref="AudioNode.GetNumberOfInputsAsync"/>.
    /// </summary>
    [JsonPropertyName("numberOfInputs")]
    public ulong NumberOfInputs { get; set; } = 1;

    /// <summary>
    /// This is used to initialize the value of <see cref="AudioNode.GetNumberOfOutputsAsync"/>.
    /// </summary>
    [JsonPropertyName("numberOfOutputs")]
    public ulong NumberOfOutputs { get; set; } = 1;

    /// <summary>
    /// This array is used to configure the number of channels in each output.
    /// </summary>
    [JsonPropertyName("outputChannelCount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ulong[]? OutputChannelCount { get; set; }

    /// <summary>
    /// This is a list of user-defined key-value pairs that are used to set the initial value of an <see cref="AudioParam"/> with the matched name in the <see cref="AudioWorkletNode"/>.
    /// </summary>
    [JsonPropertyName("parameterData")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, double>? ParameterData { get; set; }

    /// <summary>
    /// This holds any user-defined data that may be used to initialize custom properties in an <see cref="AudioWorkletProcessor"/> instance that is associated with the <see cref="AudioWorkletNode"/>.
    /// </summary>
    [JsonPropertyName("processorOptions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? ProcessorOptions { get; set; }
}