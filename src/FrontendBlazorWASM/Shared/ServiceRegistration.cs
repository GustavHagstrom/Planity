using Microsoft.AspNetCore.Components.Authorization;
using Planity.FrontendBlazorWASM.Shared.Services;

namespace Planity.FrontendBlazorWASM.Features.Shared;

public static class ServiceRegistration
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationStateProvider, MockedAuthenticationStateProvider>();
        services.AddScoped<IAuthService, MockedAuthService>();
        services.AddScoped<IProjectService, MockProjectService>();
        services.AddScoped<IResourceService, MockResourceService>();
        services.AddScoped<IDateCalculator, DateCalculator>();
        services.AddScoped<IRenderCalculator, RenderCalculator>();
        return services;
    }
}
