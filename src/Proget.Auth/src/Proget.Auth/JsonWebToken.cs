namespace Proget.Auth;

public record JsonWebToken(string AccessToken, string RefreshToken, long Expires, string UserId,
    IDictionary<JWTClaimTypes, IEnumerable<string>>? Claims);
// {
//     // public JsonWebToken() : this()
//     // {
//     //     AccessToken = accessToken;
//     //     RefreshToken = refreshToken;
//     //     Expires = expires;
//     //     UserId = userId;
//     //     Claims = claims;
//     // }
//     // public string AccessToken { get; init; }
//     // public string RefreshToken { get; init; }
//     // public long Expires { get; init; }
//     // public string UserId { get; init; } = string.Empty;
//     // public IDictionary<JWTClaimTypes, IEnumerable<string>>? Claims { get; init; }
// }
