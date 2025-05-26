namespace Planity.FrontendBlazorWASM.Shared.Services;

public interface IAuthService
{
    Task LoginAsync(bool redirectoToDashboard);
    Task LogoutAsync();
    Task<bool> IsUserAuthenticatedAsync();
    Task<string> GetUserIdAsync();
    Task<string> GetOrganizationIdAsync();
    Task<string> GetUserNameAsync();
}
