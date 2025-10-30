using System.Collections;

namespace Planity.FrontendBlazorWASM;

public static class Routes
{
    public const string Dashboard = "/";
    public const string Settings = "/settings";
    public const string ProjectsOverview = "/projects";
    public const string ProjectsNew = "/projects/new";
    public const string ProjectGantt = "/projects/gantt";
    public static string ProjectDetails(string id) => $"/projects/{id}";
    public const string Unauthorized = "/unauthorized";

    // Resource routes
    public const string ResourcesOverview = "/resources";
    public const string ResourceGantt = "/resources/resource-gantt";
    public static string ResourceDetails(string id) => $"/resources/{id}";
    public const string ResourceNew = "/resources/new";


    // Task routes
    public const string TasksOverview = "/tasks";
    public const string TaskNew = "/tasks/new";
    public static string TaskDetails(string id) => $"/tasks/{id}";

    // Milestone routes
    public const string MilestonesOverview = "/milestones";
    public const string MilestoneNew = "/milestones/new";
    public static string MilestoneDetails(string id) => $"/milestones/{id}";

    public static string[] AllRoutes =>
    [
        Dashboard,
        Settings,
        ProjectsOverview,
        ProjectsNew,
        ProjectDetails("SampleId"),
        Unauthorized,
        ResourcesOverview,
        ResourceDetails("SampleId"),
        ResourceNew,
        TasksOverview,
        TaskNew,
        TaskDetails("SampleId"),
        MilestonesOverview,
        MilestoneNew,
        MilestoneDetails("SampleId")
    ];
}
