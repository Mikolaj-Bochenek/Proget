namespace Proget.Auth;

public interface IJWTHandler
{
    JsonWebToken GenerateJWT(string userId, string? audience = null, IDictionary<JWTClaimTypes, IEnumerable<string>>? claims = null);
    JWTPayload? GetTokenPayload(string accessToken);
}
