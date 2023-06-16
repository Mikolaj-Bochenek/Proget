namespace Proget.Auth.Handlers;

internal sealed class JWTHandler : IJWTHandler
{
    private readonly JWTOptions _options;
    private readonly string? _issuer;
    private readonly SigningCredentials _signingCredentials;
    private static readonly Dictionary<JWTClaimTypes, IEnumerable<string>> EmptyClaims = new();
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    private readonly TokenValidationParameters _tokenValidationParameters;

    private static readonly ISet<string> DefaultClaims = new HashSet<string>
    {
        JwtRegisteredClaimNames.Sub,
        JwtRegisteredClaimNames.UniqueName,
        JwtRegisteredClaimNames.Jti,
        JwtRegisteredClaimNames.Iat,
        JwtRegisteredClaimNames.Exp,
        JwtRegisteredClaimNames.Aud,
        JwtRegisteredClaimNames.Nbf,
        JwtRegisteredClaimNames.Iss,
        ClaimTypes.Role
    };

    public JWTHandler(JWTOptions options, TokenValidationParameters tokenValidationParameters)
    {
        var issuerSigningKey = tokenValidationParameters.IssuerSigningKey;
        if (issuerSigningKey is null)
            throw new InvalidOperationException("Issuer signing key not set.");

        _options = options;
        _issuer = options.Issuer;
        _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
        _tokenValidationParameters = tokenValidationParameters;
    }

    public JsonWebToken GenerateJWT(string userId, string? audience = null, IDictionary<JWTClaimTypes, IEnumerable<string>>? claims = null)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID claim (subject) cannot be empty.", nameof(userId));

        var now = DateTime.UtcNow;

        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.UniqueName, userId),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString()),
        };

        if (!string.IsNullOrWhiteSpace(audience))
        {
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
        }

        if (claims?.Any() is true)
        {
            var customClaims = new List<Claim>();

            foreach (var (key, values) in claims)
            {
                var claim = Enum.GetName(key);
                if (!string.IsNullOrWhiteSpace(claim))
                    customClaims.AddRange(values.Select(value => new Claim(claim, value)));
            }

            jwtClaims.AddRange(customClaims);
        }

        var expires = now.AddMinutes(_options.ExpiryMinutes);

        var jwt = new JwtSecurityToken(
            issuer: _issuer,
            claims: jwtClaims,
            notBefore: now,
            expires: expires,
            signingCredentials: _signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JsonWebToken(
            AccessToken: token,
            RefreshToken: string.Empty,
            Expires: expires.ToTimestamp(),
            UserId: userId,
            Claims: claims ?? EmptyClaims
        );
    }

    public JWTPayload? GetTokenPayload(string accessToken)
    {
        _jwtSecurityTokenHandler.ValidateToken(accessToken, _tokenValidationParameters,
            out var validatedSecurityToken);

        if (!(validatedSecurityToken is JwtSecurityToken jwt))
        {
            return null;
        }

        return new JWTPayload
        {
            Subject = jwt.Subject,
            Role = jwt.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
            Expires = jwt.ValidTo.ToTimestamp(),
            Claims = jwt.Claims   
        };
    }
}
