namespace Proget.Auth.Builders;

internal sealed class JWTOptionsBuilder : IJWTOptionsBuilder
{
    private readonly JWTOptions _options = new();

    public IJWTOptionsBuilder WithAudienceValidation(bool validateAudience)
    {
        _options.ValidateAudience = validateAudience;
        return this;
    }

    public IJWTOptionsBuilder WithIssuerValidation(bool validateIssuer)
    {
        _options.ValidateIssuer = validateIssuer;
        return this;
    }

    public IJWTOptionsBuilder WithLifetimeValidation(bool validateLifetime)
    {
        _options.ValidateLifetime = validateLifetime;
        return this;
    }

    public IJWTOptionsBuilder WithIssuerSigningKeyValidation(bool validateIssuerSigningKey)
    {
        _options.ValidateIssuerSigningKey = validateIssuerSigningKey;
        return this;
    }

    public IJWTOptionsBuilder WithAudience(string audience)
    {
        _options.Audience = audience;
        return this;
    }

    public IJWTOptionsBuilder WithIssuer(string issuer)
    {
        _options.Issuer = issuer;
        return this;
    }

    public IJWTOptionsBuilder WithIssuerSigningKey(string issuerSigningKey)
    {
        _options.IssuerSigningKey = issuerSigningKey;
        return this;
    }

    public IJWTOptionsBuilder WithExpiryMinutes(int expiryMinutes)
    {
        _options.ExpiryMinutes = expiryMinutes;
        return this;
    }

    public IJWTOptionsBuilder WithAllowAnonymous(params string[] endpoints)
    {
        _options.AllowAnonymousEndpoints = endpoints;
        return this;
    }

    public JWTOptions Build() => _options;
}
