using Microsoft.AspNetCore.Components.Authorization;
using Planity.FrontendBlazorWASM.Features.Shared.Authentication;

namespace Planity.FrontendBlazorWASM.Features.Shared;

public static class ServiceRegistration
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        
        services.AddScoped<AuthenticationStateProvider, MockedAuthenticationStateProvider>();
        services.AddScoped<IAuthService, MockedAuthService>();
        return services;
    }
}
