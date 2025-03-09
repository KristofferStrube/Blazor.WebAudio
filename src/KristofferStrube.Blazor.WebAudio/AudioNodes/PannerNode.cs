using KristofferStrube.Blazor.WebAudio.Extensions;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// This interface represents a processing node which positions / spatializes an incoming audio stream in three-dimensional space.
/// The spatialization is in relation to the <see cref="BaseAudioContext"/>'s <see cref="AudioListener"/> (listener attribute).
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webaudio/#PannerNode">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class PannerNode : AudioNode, IJSCreatable<PannerNode>
{
    /// <inheritdoc/>
    public static new async Task<PannerNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<PannerNode> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new PannerNode(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Creates an <see cref="PannerNode"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="context">The <see cref="BaseAudioContext"/> this new <see cref="PannerNode"/> will be associated with.</param>
    /// <param name="options">Optional initial parameter value for this <see cref="PannerNode"/>.</param>
    /// <returns>A new instance of an <see cref="PannerNode"/>.</returns>
    public static async Task<PannerNode> CreateAsync(IJSRuntime jSRuntime, BaseAudioContext context, PannerOptions? options = null)
    {
        await using ErrorHandlingJSObjectReference errorHandlingHelper = await jSRuntime.GetErrorHandlingHelperAsync();
        IJSObjectReference jSInstance = await errorHandlingHelper.InvokeAsync<IJSObjectReference>("constructPannerNode", context, options);
        return new PannerNode(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected PannerNode(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    /// <summary>
    /// Specifies the panning model used by this <see cref="PannerNode"/>. Defaults to <see cref="PanningModelType.EqualPower"/>.
    /// </summary>
    public async Task<PanningModelType> GetPanningModelAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<PanningModelType>("getAttribute", JSReference, "panningModel");
    }

    /// <summary>
    /// The x-coordinate position of the audio source in a 3D Cartesian system.
    /// </summary>
    public async Task<AudioParam> GetPositionXAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "positionX");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// The y-coordinate position of the audio source in a 3D Cartesian system.
    /// </summary>
    public async Task<AudioParam> GetPositionYAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "positionY");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// The z-coordinate position of the audio source in a 3D Cartesian system.
    /// </summary>
    public async Task<AudioParam> GetPositionZAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "positionZ");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Describes the x-component of the vector of the direction the audio source is pointing in 3D Cartesian coordinate space.
    /// </summary>
    public async Task<AudioParam> GetOrientationXAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "orientationX");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Describes the y-component of the vector of the direction the audio source is pointing in 3D Cartesian coordinate space.
    /// </summary>
    public async Task<AudioParam> GetOrientationYAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "orientationY");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Describes the z-component of the vector of the direction the audio source is pointing in 3D Cartesian coordinate space.
    /// </summary>
    public async Task<AudioParam> GetOrientationZAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "orientationZ");
        return await AudioParam.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Specifies the distance model used by this <see cref="PannerNode"/>. Defaults to <see cref="DistanceModelType.Inverse"/>.
    /// </summary>
    public async Task<DistanceModelType> GetDistanceModelAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<DistanceModelType>("getAttribute", JSReference, "distanceModel");
    }

    /// <summary>
    /// Gets the reference distance for reducing volume as source moves further from the listener.
    /// For distances less than this, the volume is not reduced.
    /// The default value is <c>1</c>.
    /// </summary>
    public async Task<double> GetRefDistanceAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "refDistance");
    }

    /// <summary>
    /// Sets the reference distance for reducing volume as source moves further from the listener.
    /// For distances less than this, the volume is not reduced.
    /// </summary>
    /// <remarks>
    /// It will throw a <see cref="RangeErrorException"/> if this is set to a negative value.
    /// </remarks>
    /// <exception cref="RangeErrorException"/>
    public async Task SetRefDistanceAsync(double value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        ErrorHandlingJSObjectReference errorHandlingHelper = new(JSRuntime, helper);
        await errorHandlingHelper.InvokeVoidAsync("setAttribute", JSReference, "refDistance", value);
    }

    /// <summary>
    /// Gets the maximum distance between source and listener, after which the volume will not be reduced any further.
    /// The default value is <c>10000</c>.
    /// </summary>
    public async Task<double> GetMaxDistanceAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "maxDistance");
    }

    /// <summary>
    /// Sets the maximum distance between source and listener, after which the volume will not be reduced any further.
    /// </summary>
    /// <remarks>
    /// It will throw a <see cref="RangeErrorException"/> if this is set to a negative value.
    /// </remarks>
    /// <exception cref="RangeErrorException"/>
    public async Task SetMaxDistanceAsync(double value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        ErrorHandlingJSObjectReference errorHandlingHelper = new(JSRuntime, helper);
        await errorHandlingHelper.InvokeVoidAsync("setAttribute", JSReference, "maxDistance", value);
    }

    /// <summary>
    /// Describes how quickly the volume is reduced as source moves away from listener.
    /// The default value is <c>1</c>.
    /// The nominal range for the rolloffFactor specifies the minimum and maximum values the rolloffFactor can have.
    /// Values outside the range are clamped to lie within this range. The nominal range depends on <see cref="GetDistanceModelAsync"/> as follows:
    /// <list type="table">
    /// <item>
    /// <term><see cref="DistanceModelType.Linear"/></term>
    /// <description>The nominal range is <c>[0,1]</c></description>
    /// </item>
    /// <item>
    /// <term><see cref="DistanceModelType.Inverse"/></term>
    /// <description>The nominal range is <c>[0,∞)</c></description>
    /// </item>
    /// <item>
    /// <term><see cref="DistanceModelType.Exponential"/></term>
    /// <description>The nominal range is <c>[0,∞)</c></description>
    /// </item>
    /// </list>
    /// Note that the clamping happens as part of the processing of the distance computation.
    /// The attribute reflects the value that was set and is not modified.
    /// </summary>
    public async Task<double> GetRolloffFactorAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "rolloffFactor");
    }

    /// <summary>
    /// Describes how quickly the volume is reduced as source moves away from listener.
    /// The nominal range for the rolloffFactor specifies the minimum and maximum values the rolloffFactor can have.
    /// Values outside the range are clamped to lie within this range. The nominal range depends on <see cref="GetDistanceModelAsync"/> as follows:
    /// <list type="table">
    /// <item>
    /// <term><see cref="DistanceModelType.Linear"/></term>
    /// <description>The nominal range is <c>[0,1]</c></description>
    /// </item>
    /// <item>
    /// <term><see cref="DistanceModelType.Inverse"/></term>
    /// <description>The nominal range is <c>[0,∞)</c></description>
    /// </item>
    /// <item>
    /// <term><see cref="DistanceModelType.Exponential"/></term>
    /// <description>The nominal range is <c>[0,∞)</c></description>
    /// </item>
    /// </list>
    /// Note that the clamping happens as part of the processing of the distance computation.
    /// The attribute reflects the value that was set and is not modified.
    /// </summary>
    /// <remarks>
    /// It will throw a <see cref="RangeErrorException"/> if this is set to a negative value.
    /// </remarks>
    /// <exception cref="RangeErrorException"/>
    public async Task SetRolloffFactorAsync(double value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        ErrorHandlingJSObjectReference errorHandlingHelper = new(JSRuntime, helper);
        await errorHandlingHelper.InvokeVoidAsync("setAttribute", JSReference, "rolloffFactor", value);
    }

    /// <summary>
    /// Get the parameter for directional audio sources that is an angle, in degrees, inside of which there will be no volume reduction.
    /// The default value is <c>360</c>.
    /// The behavior is undefined if the angle is outside the interval <c>[0, 360]</c>.
    /// </summary>
    public async Task<double> GetConeInnerAngleAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "coneInnerAngle");
    }

    /// <summary>
    /// Sets the parameter for directional audio sources that is an angle, in degrees, inside of which there will be no volume reduction.
    /// The behavior is undefined if the angle is outside the interval <c>[0, 360]</c>.
    /// </summary>
    public async Task SetConeInnerAngleAsync(double value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        ErrorHandlingJSObjectReference errorHandlingHelper = new(JSRuntime, helper);
        await errorHandlingHelper.InvokeVoidAsync("setAttribute", JSReference, "coneInnerAngle", value);
    }

    /// <summary>
    /// Gets the parameter for directional audio sources that is an angle, in degrees, outside of which the volume will be reduced to a constant value of <see cref="GetConeOuterGainAsync"/>.
    /// The default value is <c>360</c>.
    /// The behavior is undefined if the angle is outside the interval <c>[0, 360]</c>.
    /// </summary>
    public async Task<double> GetConeOuterAngleAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "coneOuterAngle");
    }

    /// <summary>
    /// Sets the parameter for directional audio sources that is an angle, in degrees, outside of which the volume will be reduced to a constant value of <see cref="GetConeOuterGainAsync"/>.
    /// The default value is <c>360</c>.
    /// The behavior is undefined if the angle is outside the interval <c>[0, 360]</c>.
    /// </summary>
    public async Task SetConeOuterAngleAsync(double value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        ErrorHandlingJSObjectReference errorHandlingHelper = new(JSRuntime, helper);
        await errorHandlingHelper.InvokeVoidAsync("setAttribute", JSReference, "coneOuterAngle", value);
    }

    /// <summary>
    /// A parameter for directional audio sources that is the gain outside of <see cref="GetConeOuterAngleAsync"/>.
    /// The default value is <c>0</c>.
    /// It is a linear value (not dB) in the range <c>[0, 1]</c>.
    /// </summary>
    public async Task<double> GetConeOuterGainAsync()
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "coneOuterGain");
    }

    /// <summary>
    /// A parameter for directional audio sources that is the gain outside of the <see cref="GetConeOuterAngleAsync"/>.
    /// It is a linear value (not dB) in the range <c>[0, 1]</c>.
    /// </summary>
    /// <remarks>
    /// It will throw a <see cref="InvalidStateErrorException"/> if this is set to a value outside the range <c>[0, 1]</c>
    /// </remarks>
    public async Task SetConeOuterGainAsync(double value)
    {
        IJSObjectReference helper = await webAudioHelperTask.Value;
        ErrorHandlingJSObjectReference errorHandlingHelper = new(JSRuntime, helper);
        await errorHandlingHelper.InvokeVoidAsync("setAttribute", JSReference, "coneOuterGain", value);
    }
}
