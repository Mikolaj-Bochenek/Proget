namespace Proget.Auth;

public interface IAccessTokenService
{
    Task<bool> IsCurrentTokenActive();
    Task DeactivateCurrentTokenAsync();
    void AddContextClaims();
}
