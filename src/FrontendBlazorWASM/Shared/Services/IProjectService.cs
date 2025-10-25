using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public interface IProjectService
{
    // Projekt
    Task<List<Project>> GetProjectsAsync();
    Task<Project?> GetProjectByIdAsync(string projectId);
    Task<Project> CreateProjectAsync(Project project);
    Task<Project> UpdateProjectAsync(Project project);
    Task DeleteProjectAsync(string projectId);
    Task<List<Project>> GetProjectsAsync(List<string> projectIds);

    // Tasks
    Task<ProjectTask> AddTaskToProjectAsync(string projectId, ProjectTask task);
    Task<ProjectTask> UpdateTaskAsync(string projectId, ProjectTask task);
    Task DeleteTaskAsync(string projectId, string taskId);
    Task AssignResourceToTaskAsync(string projectId, string taskId, string resourceId);
    Task<ProjectTask?> GetTaskByIdAsync(string taskId);
    Task<List<ProjectTask>> GetAllTasksAsync();

    // Milestones
    Task<Milestone> AddMilestoneToProjectAsync(string projectId, Milestone milestone);
    Task<Milestone> UpdateMilestoneAsync(string projectId, Milestone milestone);
    Task DeleteMilestoneAsync(string projectId, string milestoneId);
    Task<Milestone?> GetMilestoneByIdAsync(string milestoneId);
    Task<List<Milestone>> GetAllMilestonesAsync();
}
