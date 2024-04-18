using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents the position and orientation of the person listening to the audio scene.
/// All <see cref="PannerNode"/> objects spatialize in relation to the <see cref="BaseAudioContext"/>'s listener.<br />
/// The positionX, positionY, and positionZ <see cref="AudioParam"/> represent the location of the listener in 3D Cartesian coordinate space.
/// <see cref="PannerNode"/> objects use this position relative to individual audio sources for spatialization.<br />
/// The forwardX, forwardY, and forwardZ parameters represent a direction vector in 3D space.
/// Both a forward vector and an up vector are used to determine the orientation of the listener.
/// In simple human terms, the forward vector represents which direction the person’s nose is pointing.<br />
/// The up vector represents the direction the top of a person’s head is pointing.
/// These two vectors are expected to be linearly independent.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#AudioListener">See the API definition here</see>.</remarks>
public class AudioListener : BaseJSWrapper, IJSCreatable<AudioListener>
{
    /// <inheritdoc/>
    public static async Task<AudioListener> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<AudioListener> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new AudioListener(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected AudioListener(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    /// <summary>
    /// Sets the x coordinate position of the audio listener in a 3D Cartesian coordinate space.
    /// </summary>
    /// <returns></returns>
    public async Task<AudioParam> GetPositionXAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("positionX");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Sets the y coordinate position of the audio listener in a 3D Cartesian coordinate space.
    /// </summary>
    /// <returns></returns>
    public async Task<AudioParam> GetPositionYAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("positionY");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Sets the z coordinate position of the audio listener in a 3D Cartesian coordinate space.
    /// </summary>
    /// <returns></returns>
    public async Task<AudioParam> GetPositionZAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("positionZ");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Sets the x coordinate component of the forward direction the listener is pointing in 3D Cartesian coordinate space.
    /// </summary>
    /// <returns></returns>
    public async Task<AudioParam> GetForwardXAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("forwardX");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Sets the y coordinate component of the forward direction the listener is pointing in 3D Cartesian coordinate space.
    /// </summary>
    /// <returns></returns>
    public async Task<AudioParam> GetForwardYAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("forwardY");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Sets the z coordinate component of the forward direction the listener is pointing in 3D Cartesian coordinate space.
    /// </summary>
    /// <returns></returns>
    public async Task<AudioParam> GetForwardZAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("forwardZ");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Sets the x coordinate component of the up direction the listener is pointing in 3D Cartesian coordinate space.
    /// </summary>
    /// <returns></returns>
    public async Task<AudioParam> GetUpXAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("upX");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Sets the y coordinate component of the up direction the listener is pointing in 3D Cartesian coordinate space.
    /// </summary>
    /// <returns></returns>
    public async Task<AudioParam> GetUpYAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("upY");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Sets the z coordinate component of the up direction the listener is pointing in 3D Cartesian coordinate space.
    /// </summary>
    /// <returns></returns>
    public async Task<AudioParam> GetUpZAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("upZ");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }
}
