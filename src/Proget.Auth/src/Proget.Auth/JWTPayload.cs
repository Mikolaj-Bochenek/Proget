namespace Proget.Auth;

public class JWTPayload
{
    public string? Subject { get; set; }
    public string? Role { get; set; }
    public long Expires { get; set; }
    public IEnumerable<Claim>? Claims { get; set; }
}
