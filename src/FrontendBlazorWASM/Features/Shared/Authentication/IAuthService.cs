namespace Planity.FrontendBlazorWASM.Features.Shared.Authentication;

public interface IAuthService
{
    Task LoginAsync();
    Task LogoutAsync();
    Task<bool> IsUserAuthenticatedAsync();
    Task<string> GetUserIdAsync();
    Task<string> GetOrganizationIdAsync();
    Task<string> GetUserNameAsync();
}
