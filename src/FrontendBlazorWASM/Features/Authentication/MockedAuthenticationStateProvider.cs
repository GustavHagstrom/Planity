using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Planity.FrontendBlazorWASM.Features.Authentication;

public class MockedAuthenticationStateProvider : AuthenticationStateProvider
{
    private bool _isLoggedIn = true;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    private ClaimsPrincipal _user = new ClaimsPrincipal(new ClaimsIdentity(
        new[]
        {
            new Claim(ClaimTypes.Name, "Test User"),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("UserId", "UserID123"),
            new Claim("OrganizationId", "OrganizationID123"),
        }, "mock"));
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_isLoggedIn)
        {
            return Task.FromResult(new AuthenticationState(_user));
        }
        // If not logged in, return an anonymous user
        return Task.FromResult(new AuthenticationState(_anonymous));
    }
    public void SignIn()
    {
        _isLoggedIn = true;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_user)));
    }
    public void SignOut()
    {
        _isLoggedIn = false;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}
