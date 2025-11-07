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
    //public const string TaskNew = "/tasks/new";
    public static string TaskNew(string? projectId = null, string? resourceId = null, string? returnUrl = null)
    {
        var url = "/tasks/new";
        var queryParams = new List<string>();
        if (!string.IsNullOrWhiteSpace(projectId))
            queryParams.Add($"projectId={projectId}");
        if (!string.IsNullOrWhiteSpace(resourceId))
            queryParams.Add($"resourceId={resourceId}");
        if (!string.IsNullOrWhiteSpace(returnUrl))
            queryParams.Add($"returnUrl={Uri.EscapeDataString(returnUrl)}");
        if (queryParams.Count > 0)
            url += "?" + string.Join("&", queryParams);
        return url;
    }
    public static string TaskDetails(string id, string? returnUrl = null)
    {
        var url = $"/tasks/{id}";
        if (!string.IsNullOrWhiteSpace(returnUrl))
        {
            url += $"?returnUrl={Uri.EscapeDataString(returnUrl)}";
        }
        return url;
    }

    // Milestone routes
    public const string MilestonesOverview = "/milestones";
    public static string MilestoneNew(string? projectId = null, string? returnUrl = null)
    {
        var url = "/milestones/new";
        var queryParams = new List<string>();
        if (!string.IsNullOrWhiteSpace(projectId))
            queryParams.Add($"projectId={projectId}");
        if (!string.IsNullOrWhiteSpace(returnUrl))
            queryParams.Add($"returnUrl={Uri.EscapeDataString(returnUrl)}");
        if (queryParams.Count > 0)
            url += "?" + string.Join("&", queryParams);
        return url;
    }
    public static string MilestoneDetails(string id, string? returnUrl = null)
    {
        var url = $"/milestones/{id}";
        if (!string.IsNullOrWhiteSpace(returnUrl))
        {
            url += $"?returnUrl={Uri.EscapeDataString(returnUrl)}";
        }
        return url;
    }
}
