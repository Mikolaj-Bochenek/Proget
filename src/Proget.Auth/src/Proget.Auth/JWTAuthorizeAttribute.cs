namespace Proget.Auth;

public class JWTAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();
    private readonly List<string> _requiredClaims;
    private readonly JWTClaimTypes _type;

    public JWTAuthorizeAttribute(JWTClaimTypes type, params string[] requiredClaims) : base()
    {
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        _type = type;
        _requiredClaims = requiredClaims.ToList();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        _claims = context.HttpContext.User.Claims;
        
        var claims = GetClaims(Enum.GetName(_type));

        var isAuthorize = !_requiredClaims.Except(claims).Any();

        if (!isAuthorize)
        {
            var exception = new UnauthorizedException();

            context.Result = new UnauthorizedActionResult(exception);
        }  
    }

    private IEnumerable<string> GetClaims(string? key)
    {
        var claims = _claims.Where(c => c.Type.Equals(key)).ToList();
        
        foreach (var claim in claims)
            yield return claim.Value;
    }
}