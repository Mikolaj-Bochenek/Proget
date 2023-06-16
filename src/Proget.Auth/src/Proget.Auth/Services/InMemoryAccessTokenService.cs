namespace Proget.Auth.Services;

internal sealed class InMemoryAccessTokenService : IAccessTokenService
{
    private readonly IMemoryCache _cache;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJWTHandler _jwtHandler;
    private readonly TimeSpan _expires;

    public InMemoryAccessTokenService(IMemoryCache cache, IHttpContextAccessor httpContextAccessor,
        IJWTHandler jwtHandler, JWTOptions jwtOptions)
    {
        _cache = cache;
        _httpContextAccessor = httpContextAccessor;
        _jwtHandler = jwtHandler;
        _expires = TimeSpan.FromMinutes(jwtOptions.ExpiryMinutes);
    }

    public Task<bool> IsCurrentTokenActive()
        => GetCurrentToken().IsNullOrEmpty() ?
            Task.FromResult(false) : IsActiveAsync(GetCurrentToken());

    public Task DeactivateCurrentTokenAsync()
        => DeactivateAsync(GetCurrentToken());

    public void AddContextClaims()
    {
        var token = GetCurrentToken();

        if (!token.IsNullOrEmpty())
        {
            var tokenPayload = _jwtHandler.GetTokenPayload(token);

            if (tokenPayload?.Claims is not null)
            {
                if (_httpContextAccessor.HttpContext is not null)
                {
                    var identity = new ClaimsIdentity(tokenPayload.Claims);

                    _httpContextAccessor.HttpContext.User = new ClaimsPrincipal(identity);
                }      
            }
        }
    }

    private Task<bool> IsActiveAsync(string token)
        => Task.FromResult(string.IsNullOrWhiteSpace(_cache.Get<string>(GetKey(token))));

    private Task DeactivateAsync(string token)
    {
        _cache.Set(GetKey(token), "revoked", new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _expires
        });

        return Task.CompletedTask;
    }

    private string GetCurrentToken()
    {
        var authorizationHeader = _httpContextAccessor?.HttpContext?.Request.Headers["authorization"].ToString();

        return string.IsNullOrEmpty(authorizationHeader)
            ? string.Empty
            : authorizationHeader.Replace("Bearer ", string.Empty);
    }

    private static string GetKey(string token) => $"blacklisted-tokens:{token}";
}
