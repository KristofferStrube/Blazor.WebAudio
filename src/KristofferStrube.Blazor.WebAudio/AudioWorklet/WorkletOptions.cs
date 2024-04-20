using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The options used to add a module via <see cref="Worklet.AddModuleAsync(string, KristofferStrube.Blazor.WebAudio.WorkletOptions?)"/>
/// </summary>
/// <remarks><see href="https://html.spec.whatwg.org/multipage/worklets.html#workletoptions">See the API definition here</see>.</remarks>
public class WorkletOptions
{
    /// <summary>
    /// Can be set to a credentials mode to modify the script-fetching process.
    /// It defaults to <see cref="RequestCredentials.SameOrigin"/>.
    /// </summary>
    [JsonPropertyName("credentials")]
    public RequestCredentials Credentials { get; set; } = RequestCredentials.SameOrigin;
}
