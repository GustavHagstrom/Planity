using Planity.FrontendBlazorWASM;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Globalization;
using Planity.FrontendBlazorWASM.Features.Dashboard;
using Planity.FrontendBlazorWASM.Features.Milestones;
using Planity.FrontendBlazorWASM.Features.Project;
using Planity.FrontendBlazorWASM.Features.Resources;
using Planity.FrontendBlazorWASM.Features.Settings;
using Planity.FrontendBlazorWASM.Features.Tasks;

// Set default culture (e.g., Swedish)
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("sv-SE");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("sv-SE");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//builder.Services.AddMsalAuthentication(options =>
//{
//    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
//});

builder.Services.AddAuthorizationCore(options =>
{
    //options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    //options.AddPolicy("User", policy => policy.RequireRole("User"));
});
builder.Services.AddMudServices();
builder.Services.AddSharedServices();
builder.ConfigureFeatures(config =>
{
    config.AddFeature<DashboardFeature>();
    config.AddFeature<MilestoneFeature>();
    config.AddFeature<ProjectFeature>();
    config.AddFeature<ResourceFeature>();
    config.AddFeature<TaskFeature>();
    config.AddFeature<SettingsFeature>();
});

var host = builder.Build();



await host.RunAsync();