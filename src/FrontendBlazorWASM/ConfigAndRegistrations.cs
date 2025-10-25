using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Services;
namespace Planity.FrontendBlazorWASM;

public class FeatureBuilder
{
    private readonly WebAssemblyHostBuilder _builder;
    private readonly IServiceProvider _sp;

    public FeatureBuilder(WebAssemblyHostBuilder builder, IServiceProvider sp)
    {
        _builder = builder;
        _sp = sp;
    }
    public RenderCollection ComponentTypeCollection { get; set; } = new();
    public void AddFeature<T>() where T : IFeature
    {
        var feature = ActivatorUtilities.CreateInstance<T>(_sp);
        feature.RegisterServices(_builder.Services);
        feature.RegisterRenderObjects(ComponentTypeCollection);
    }
    
}
//public record MenueLink(string Title, string Url, string Icon);
public record MenuLink(string Title, string Route, string? Icon = null, NavLinkMatch? Match = null);
public class RenderCollection
{
    private HashSet<Type> _appBarNavigationComponents = new();
    private HashSet<MenuLink> _createNewLinks = new();

    public void AddAppBarNavigationComponent<T>() where T : ComponentBase => _appBarNavigationComponents.Add(typeof(T));
    public IEnumerable<Type> GetAppBarNavigationComponents() => _appBarNavigationComponents;
    public void AddCreateNewLink(MenuLink link) => _createNewLinks.Add(link);
    public IEnumerable<MenuLink> GetCreateNewLinks() => _createNewLinks;
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
        var sp = builder.Services.BuildServiceProvider();
        var featureBuilder = new FeatureBuilder(builder, sp);
        config(featureBuilder);
        builder.Services.AddSingleton(featureBuilder.ComponentTypeCollection);

    }
}
