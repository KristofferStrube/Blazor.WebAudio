﻿using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// Changing the gain of an audio signal is a fundamental operation in audio applications. This interface is an <see cref="AudioNode"/> with a single input and single output.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#GainNode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class GainNode : AudioNode, IJSCreatable<GainNode>
{
    /// <inheritdoc/>
    public static new async Task<GainNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<GainNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new GainNode(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Creates an <see cref="GainNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="GainNode"/> will be associated with.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="GainNode"/>.</param>
    /// <returns>A new instance of a <see cref="GainNode"/>.</returns>
    public static async Task<GainNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, GainOptions? options = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructGainNode", context, options);
        return new GainNode(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected GainNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

    /// <summary>
    /// Represents the amount of gain to apply.
    /// </summary>
    public async Task<AudioParam> GetGainAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "gain");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }
}
