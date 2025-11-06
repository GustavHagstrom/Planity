using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public interface IProjectService
{
    // Projekt
    Task<Project> CreateAsync(Project project);
    Task<Project?> GetAsync(string projectId);
    Task<List<Project>> GetAllAsync();
    Task<List<Project>> GetAllAsync(List<string> projectIds);
    Task<Project> UpdateAsync(Project project);
    Task DeleteAsync(string projectId);
    

    // Tasks
    //Task<ProjectTask> UpdateTaskAsync(string projectId, ProjectTask task);
    //Task DeleteTaskAsync(string projectId, string taskId);
    //Task AssignResourceToTaskAsync(string projectId, string taskId, string resourceId);
    //Task<ProjectTask?> GetTaskByIdAsync(string taskId);
    //Task<List<ProjectTask>> GetAllTasksAsync();
    //Task CreateTaskAsync(ProjectTask task);

    // Milestones
    //Task<Milestone> AddMilestoneToProjectAsync(string projectId, Milestone milestone);
    //Task<Milestone> UpdateMilestoneAsync(string projectId, Milestone milestone);
    //Task DeleteMilestoneAsync(string projectId, string milestoneId);
    //Task<Milestone?> GetMilestoneByIdAsync(string milestoneId);
    //Task<List<Milestone>> GetAllMilestonesAsync();
}
