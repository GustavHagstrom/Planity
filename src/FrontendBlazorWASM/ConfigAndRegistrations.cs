using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Services;
namespace Planity.FrontendBlazorWASM;

public class FeatureBuilder(WebAssemblyHostBuilder builder)
{
    public ComponentTypeCollection ComponentTypeCollection { get; set; } = new();
    public void AddFeature<T>() where T : IFeature, new()
    {
        var feature = new T();
        feature.RegisterServices(builder.Services);
        feature.RegisterInjecableComponents(ComponentTypeCollection);
        // Configuration logic for the feature can be added here
    }
    
}
public class ComponentTypeCollection
{
    private HashSet<Type> _appBarNavigationComponents = new();
    public void AddAppBarNavigationComponent<T>() where T : ComponentBase => _appBarNavigationComponents.Add(typeof(T));
    public IEnumerable<Type> GetAppBarNavigationComponents() => _appBarNavigationComponents;
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
        builder.Services.AddSingleton(featureBuilder.ComponentTypeCollection);

    }
}
