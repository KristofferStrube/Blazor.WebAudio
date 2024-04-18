using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// The options used to add a module via <see cref="Worklet.AddModuleAsync(string, KristofferStrube.Blazor.WebAudio.WorkletOptions?)"/>
/// </summary>
public class WorkletOptions
{
    /// <summary>
    /// Can be set to a credentials mode to modify the script-fetching process.
    /// It defaults to <see cref="RequestCredentials.SameOrigin"/>.
    /// </summary>
    [JsonPropertyName("credentials")]
    public RequestCredentials Credentials { get; set; } = RequestCredentials.SameOrigin;
}
