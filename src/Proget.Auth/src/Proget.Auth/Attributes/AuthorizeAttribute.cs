namespace Proget.Auth.Attributes;

[Obsolete("Use JWTClaimTypes instead")]
public enum AuthorizeType
{
    Permissions,
    Roles
}

[Obsolete("Use JWTAuthorizeAttribute instead")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class AuthorizeAttribute : TypeFilterAttribute
{
    public AuthorizeAttribute(AuthorizeType type, string value)
    : base(typeof(AuthorizeActionFilter))
    {
        Arguments = new object[] { type, value };
    }
}

[Obsolete("Use JWTAuthorizeAttribute instead")]
public class AuthorizeActionFilter : IAuthorizationFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AuthorizeType _type;
    private readonly string _value;

    public AuthorizeActionFilter(IHttpContextAccessor httpContextAccessor, AuthorizeType type, string value)
    {
        _httpContextAccessor = httpContextAccessor;
        _type = type;
        _value = value;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var claims = _httpContextAccessor?.HttpContext?.User.Claims;
        
        var isAuthorize = _type switch
        {
            AuthorizeType.Permissions => claims?.Any(c => c.Type == Enum.GetName(_type) && c.Value == _value) ?? false,
            AuthorizeType.Roles => claims?.Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)
                .Any(c => c.Type == Enum.GetName(_type) && c.Value == _value) ?? false,
            _ => false
        };

        if (!isAuthorize)
        {
            var exception = new UnauthorizedException();

            context.Result = new UnauthorizedActionResult(exception);
        }
    }
}
