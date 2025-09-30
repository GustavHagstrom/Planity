using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Constants;
using Planity.FrontendBlazorWASM.Shared.Services;
namespace Planity.FrontendBlazorWASM;

public class FeatureBuilder(WebAssemblyHostBuilder builder)
{
    private ComponentRegistrationBuilder _componentRegistrationBuilder = new(builder);
    public void AddFeature<T>() where T : IFeature, new()
    {
        var feature = new T();
        feature.RegisterServices(builder.Services);
        // Configuration logic for the feature can be added here
    }
}
public class ComponentRegistrationBuilder(WebAssemblyHostBuilder builder)
{
    public void AddAppbarNavigation<T>() where T : ComponentBase
    {
        builder.Services.AddKeyedTransient(typeof(T), ComponentKeyConstants.AppbarNavKey);
    }
}

public static class ConfigAndRegistrations
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationStateProvider, MockedAuthenticationStateProvider>();
        services.AddScoped<IAuthService, MockedAuthService>();
        services.AddScoped<IProjectService, MockProjectService>();
        services.AddScoped<IResourceService, MockResourceService>();
        services.AddScoped<IDateCalculator, DateCalculator>();
        services.AddScoped<IRenderCalculator, RenderCalculator>();
        services.AddScoped<RenderFragmentService>();
        return services;
    }
    public static void ConfigureFeatures(this WebAssemblyHostBuilder builder, Action<FeatureBuilder> config)
    {
        var featureBuilder = new FeatureBuilder(builder);
        config(featureBuilder);
    }
}
