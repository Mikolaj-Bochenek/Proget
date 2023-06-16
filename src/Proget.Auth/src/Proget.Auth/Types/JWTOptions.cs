namespace Proget.Auth.Types;

public class JWTOptions
{
    /// <summary>
    /// Gets or sets a boolean to control if the audience will be validated during token validation.
    /// Validation of the audience, mitigates forwarding attacks. For example, a site that receives a token,
    /// could not replay it to another side. A forwarded token would contain the audience of the original site.
    /// This boolean only applies to default audience validation. If AudienceValidator is set, it will be called
    /// regardless of whether this property is true or false. The default is true.
    /// </summary>
    /// <value>ValidateAudience</value>
    public bool ValidateAudience { get; set; } = true;

    /// <summary>
    /// Gets or sets a boolean to control if the issuer will be validated during token validation.
    /// Validation of the issuer mitigates forwarding attacks that can occur when an IdentityProvider
    /// represents multiple tenants and signs tokens with the same keys. It is possible that a token
    /// issued for the same audience could be from a different tenant. For example an application could
    /// accept users from contoso.onmicrosoft.com but not fabrikam.onmicrosoft.com, both valid tenants.
    /// An application that accepts tokens from fabrikam could forward them to the application that accepts
    /// tokens for contoso. This boolean only applies to default issuer validation. If IssuerValidator is set,
    /// it will be called regardless of whether this property is true or false. The default is true.
    /// </summary>
    /// <value>ValidateIssuer</value>
    public bool ValidateIssuer { get; set; } = true;

    /// <summary>
    /// Gets or sets a boolean to control if the lifetime will be validated during token validation.
    /// This boolean only applies to default lifetime validation. If LifetimeValidator is set,
    /// it will be called regardless of whether this property is true or false. The default is true.
    /// </summary>
    /// <value>ValidateLifetime</value>
    public bool ValidateLifetime { get; set; } = true;

    /// <summary>
    /// Gets or sets a boolean that controls if validation of the SecurityKey that signed the securityToken is called.
    /// It is possible for tokens to contain the public key needed to check the signature. For example, X509Data
    /// can be hydrated into an X509Certificate, which can be used to validate the signature. In these cases it is
    /// important to validate the SigningKey that was used to validate the signature. This boolean only applies to default
    /// signing key validation. If IssuerSigningKeyValidator is set, it will be called regardless of whether this property
    /// is true or false. The default is false.
    /// </summary>
    /// <value>ValidateIssuerSigningKey</value>
    public bool ValidateIssuerSigningKey { get; set; } = true;

    /// <summary>
    /// Gets or sets a string that represents a valid audience that will be used to check against the token's audience.
    /// The default is null.
    /// </summary>
    /// <value>Audience</value>
    public string? Audience { get; set; }

    /// <summary>
    /// Gets or sets a String that represents a valid issuer that will be used to check against the token's issuer.
    /// The default is null.
    /// </summary>
    /// <value>Issuer</value>
    public string? Issuer { get; set; }

    /// <summary>
    /// Gets or sets the SecurityKey that is to be used for signature validation.
    /// </summary>
    /// <value>IssuerSigningKey</value>
    public string? IssuerSigningKey { get; set; }

    /// <summary>
    /// Gets or sets a integer to control the token lifetime.
    /// If set, this integer will be called to validate the lifetime of the token
    /// This integer only applies to default lifetime validation.
    /// </summary>
    /// <value>ExpiryMinutes</value>
    public int ExpiryMinutes { get; set; }

    /// <summary>
    /// Gets or sets the list of API endpoints that would be not authorized via token.
    /// </summary>
    /// <value>AllowAnonymousEndpoints</value>
    public IEnumerable<string>? AllowAnonymousEndpoints { get; set; }
}