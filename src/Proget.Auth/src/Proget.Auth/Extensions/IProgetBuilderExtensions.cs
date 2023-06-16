namespace Proget.Auth.Extensions;

public static class IProgetBuilderExtensions
{
    private const string Section = "jwt";

    public static IProgetBuilder AddJWT(this IProgetBuilder builder,
        Func<IJWTOptionsBuilder, IJWTOptionsBuilder>? optionsBuilder = null, string section = Section)
    {
        if (string.IsNullOrWhiteSpace(section))
            section = Section;

        var options = builder.ConfigureOptions<JWTOptions, JWTOptionsBuilder, IJWTOptionsBuilder>(section ?? Section, optionsBuilder);

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddSingleton<IJWTHandler, JWTHandler>();
        builder.Services.AddSingleton<IAccessTokenService, InMemoryAccessTokenService>();
        builder.Services.AddTransient<AccessTokenValidatorMiddleware>();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = options.ValidateAudience,
            ValidateIssuer = options.ValidateIssuer,
            ValidateLifetime = options.ValidateLifetime,
            ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
            ValidIssuer = options.Issuer,
            ValidAudience = options.Audience,
            ClockSkew = TimeSpan.Zero
        };

        if (!string.IsNullOrWhiteSpace(options.IssuerSigningKey))
        {
            var rawKey = Encoding.UTF8.GetBytes(options.IssuerSigningKey);
            tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(rawKey);
        }

        builder.Services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Audience = options.Audience;
                o.TokenValidationParameters = tokenValidationParameters;
            });

        builder.Services.AddSingleton(tokenValidationParameters);

        return builder;
    }
}
