namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This special execution context is designed to enable the generation, processing, and analysis of audio data directly using a script in the audio rendering thread.
/// The user-supplied script code is evaluated in this scope to define one or more <see cref="AudioWorkletProcessor"/> subclasses, which in turn are used to instantiate <see cref="AudioWorkletProcessor"/>s, in a 1:1 association with <see cref="AudioWorkletNode"/>s in the main scope.<br />
/// Exactly one <see cref="AudioWorkletGlobalScope"/> exists for each <see cref="AudioContext"/> that contains one or more <see cref="AudioWorkletNode"/>s.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioWorkletGlobalScope">See the API definition here</see>.</remarks>
public class AudioWorkletGlobalScope
{
}
