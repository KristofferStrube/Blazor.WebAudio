using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAudio;

public class AudioParam : BaseJSWrapper
{
    public static Task<AudioParam> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new AudioParam(jSRuntime, jSReference));
    }

    public AudioParam(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task<float> GetValueAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<float>("getAttribute", JSReference, "value");
    }

    public async Task SetValueAsync(float value)
    {
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "value", value);
    }

    /// <summary>
    /// Changes the gain <paramref name="value"/> linearly starting at the previous event and ending at <paramref name="endTime"/>.
    /// </summary>
    /// <param name="value">The gain value to change to.</param>
    /// <param name="endTime">The endtime in relation to the current <see cref="AudioContext"/>'s <see cref="AudioContext.GetCurrentTimeAsync"/>.</param>
    /// <returns></returns>
    public async Task<AudioParam> LinearRampToValueAtTimeAsync(float value, double endTime)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("linearRampToValueAtTime", value, endTime);
        return await CreateAsync(JSRuntime, jSInstance);
    }
}
