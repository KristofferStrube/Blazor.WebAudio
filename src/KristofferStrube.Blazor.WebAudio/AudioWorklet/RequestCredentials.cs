using KristofferStrube.Blazor.WebAudio.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAudio;

/// <summary>
/// Controls the flow of credentials during a fetch.
/// </summary>
/// <remarks><see href="https://fetch.spec.whatwg.org/#requestcredentials">See the API definition here</see>.</remarks>
[JsonConverter(typeof(RequestCredentialsConverter))]
public enum RequestCredentials
{
    /// <summary>
    /// Excludes credentials from this request, and causes any credentials sent back in the response to be ignored.
    /// </summary>
    Omit,

    /// <summary>
    /// Include credentials with requests made to same-origin URLs, and use any credentials sent back in responses from same-origin URLs.
    /// </summary>
    SameOrigin,

    /// <summary>
    /// Always includes credentials with this request, and always use any credentials sent back in the response.
    /// </summary>
    Include
}
