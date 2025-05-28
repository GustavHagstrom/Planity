using System.Collections;

namespace Planity.FrontendBlazorWASM;

public static class Routes
{
    public const string Dashboard = "/";
    public const string Settings = "/settings";
    public const string ProjectsOverview = "/projects";
    public const string ProjecstNew = "projects/new";
    public static string ProjectDetails(string id) => $"/projects/{id}";
    public const string Unauthorized = "/unauthorized";

    // Resource routes
    public const string Resources = "/resources";
    public static string ResourceDetails(string id) => $"/resources/{id}";
    public const string ResourceNew = "/resources/new";

    public static string[] AllRoutes =>
    [
        Dashboard,
        Settings,
        ProjectsOverview,
        ProjecstNew,
        ProjectDetails("SampleId"),
        Unauthorized,
        Resources,
        ResourceDetails("SampleId"),
        ResourceNew
    ];
}
