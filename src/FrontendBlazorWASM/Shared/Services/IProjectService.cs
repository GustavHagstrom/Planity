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

    // Tasks
    Task<ProjectTask> AddTaskToProjectAsync(string projectId, ProjectTask task);
    Task<ProjectTask> UpdateTaskAsync(string projectId, ProjectTask task);
    Task DeleteTaskAsync(string projectId, string taskId);
    Task AssignResourceToTaskAsync(string projectId, string taskId, string resourceId);
    Task<ProjectTask?> GetTaskByIdAsync(string taskId);
    Task<List<ProjectTask>> GetAllTasksAsync();

    // Milestones
    Task<Milesonte> AddMilestoneToProjectAsync(string projectId, Milesonte milestone);
    Task<Milesonte> UpdateMilestoneAsync(string projectId, Milesonte milestone);
    Task DeleteMilestoneAsync(string projectId, string milestoneId);
    Task<Milesonte?> GetMilestoneByIdAsync(string milestoneId);
    Task<List<Milesonte>> GetAllMilestonesAsync();
}
