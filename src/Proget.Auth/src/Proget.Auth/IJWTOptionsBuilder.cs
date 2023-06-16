namespace Proget.Auth;

public interface IJWTOptionsBuilder : IProgetOptionsBuilder<JWTOptions>
{
    IJWTOptionsBuilder WithAudienceValidation(bool validateAudience);
    IJWTOptionsBuilder WithIssuerValidation(bool validateIssuer);
    IJWTOptionsBuilder WithLifetimeValidation(bool validateLifetime);
    IJWTOptionsBuilder WithIssuerSigningKeyValidation(bool validateIssuerSigningKey);
    IJWTOptionsBuilder WithAudience(string audience);
    IJWTOptionsBuilder WithIssuer(string issuer);
    IJWTOptionsBuilder WithIssuerSigningKey(string issuerSigningKey);
    IJWTOptionsBuilder WithExpiryMinutes(int expiryMinutes);
    IJWTOptionsBuilder WithAllowAnonymous(params string[] endpoints);
}
