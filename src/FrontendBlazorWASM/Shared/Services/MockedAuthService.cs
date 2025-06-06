﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class MockedAuthService(AuthenticationStateProvider authStateProvider, NavigationManager navigationManager) : IAuthService
{
    public async Task<string> GetOrganizationIdAsync()
    {
        var state = await authStateProvider.GetAuthenticationStateAsync();
        return state.User.FindFirst("OrganizationId")?.Value ?? string.Empty;
    }
    public async Task<string> GetUserIdAsync()
    {
        var state = await authStateProvider.GetAuthenticationStateAsync();
        return state.User.FindFirst("UserId")?.Value ?? string.Empty;
    }
    public async Task<string> GetUserNameAsync()
    {
        var state = await authStateProvider.GetAuthenticationStateAsync();
        return state.User.Identity?.Name ?? string.Empty;
    }
    public async Task<bool> IsUserAuthenticatedAsync()
    {
        var state = await authStateProvider.GetAuthenticationStateAsync();
        return state.User.Identity?.IsAuthenticated ?? false;
    }
    public Task LoginAsync(bool redirectoToDashboard)
    {
        if (authStateProvider is MockedAuthenticationStateProvider mockedAuthStateProvider)
        {
            mockedAuthStateProvider.SignIn();
            
        }
        if (redirectoToDashboard)
        {
            navigationManager.NavigateTo(Routes.Dashboard);
        }
        return Task.CompletedTask;
    }
    public Task LogoutAsync()
    {
        if (authStateProvider is MockedAuthenticationStateProvider mockedAuthStateProvider)
        {
            mockedAuthStateProvider.SignOut();
        }
        return Task.CompletedTask;
    }
}
