using Planity.FrontendBlazorWASM.Shared.Models;
using Planity.FrontendBlazorWASM.Shared.Mock;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class MockProjectService : IProjectService
{
    public Task<List<Project>> GetProjectsAsync() => Task.FromResult(MockedDataStore.Projects.ToList());

    public Task<Project?> GetProjectByIdAsync(string projectId) =>
        Task.FromResult(MockedDataStore.Projects.FirstOrDefault(p => p.Id == projectId));

    public Task<Project> CreateProjectAsync(Project project)
    {
        project.Id = Guid.NewGuid().ToString();
        MockedDataStore.Projects.Add(project);
        return Task.FromResult(project);
    }

    public Task<Project> UpdateProjectAsync(Project project)
    {
        var existing = MockedDataStore.Projects.FirstOrDefault(p => p.Id == project.Id);
        if (existing != null)
        {
            existing.Name = project.Name;
            existing.Description = project.Description;
            existing.Tasks = project.Tasks;
            existing.Milestones = project.Milestones;
        }
        return Task.FromResult(project);
    }

    public Task DeleteProjectAsync(string projectId)
    {
        var project = MockedDataStore.Projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
            MockedDataStore.Projects.Remove(project);
        return Task.CompletedTask;
    }

    public Task<ProjectTask> AddTaskToProjectAsync(string projectId, ProjectTask task)
    {
        var project = MockedDataStore.Projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            task.Id = Guid.NewGuid().ToString();
            project.Tasks.Add(task);
        }
        return Task.FromResult(task);
    }

    public Task<ProjectTask> UpdateTaskAsync(string projectId, ProjectTask task)
    {
        var project = MockedDataStore.Projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            var existing = project.Tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existing != null)
            {
                existing.Name = task.Name;
                existing.Description = task.Description;
                existing.Start = task.Start;
                existing.ResourceId = task.ResourceId;
            }
        }
        return Task.FromResult(task);
    }

    public Task DeleteTaskAsync(string projectId, string taskId)
    {
        var project = MockedDataStore.Projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            var task = project.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
                project.Tasks.Remove(task);
        }
        return Task.CompletedTask;
    }

    public Task AssignResourceToTaskAsync(string projectId, string taskId, string resourceId)
    {
        var project = MockedDataStore.Projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            var task = project.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
                task.ResourceId = resourceId;
        }
        return Task.CompletedTask;
    }

    public Task<Milestone> AddMilestoneToProjectAsync(string projectId, Milestone milestone)
    {
        var project = MockedDataStore.Projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            milestone.Id = Guid.NewGuid().ToString();
            project.Milestones.Add(milestone);
        }
        return Task.FromResult(milestone);
    }

    public Task<Milestone> UpdateMilestoneAsync(string projectId, Milestone milestone)
    {
        var project = MockedDataStore.Projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            var existing = project.Milestones.FirstOrDefault(m => m.Id == milestone.Id);
            if (existing != null)
            {
                existing.Name = milestone.Name;
                existing.Description = milestone.Description;
                existing.Start = milestone.Start;
            }
        }
        return Task.FromResult(milestone);
    }

    public Task DeleteMilestoneAsync(string projectId, string milestoneId)
    {
        var project = MockedDataStore.Projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            var milestone = project.Milestones.FirstOrDefault(m => m.Id == milestoneId);
            if (milestone != null)
                project.Milestones.Remove(milestone);
        }
        return Task.CompletedTask;
    }

    public Task<ProjectTask?> GetTaskByIdAsync(string taskId)
    {
        var task = MockedDataStore.Projects.SelectMany(p => p.Tasks).FirstOrDefault(t => t.Id == taskId);
        return Task.FromResult(task);
    }

    public Task<List<ProjectTask>> GetAllTasksAsync()
    {
        var allTasks = MockedDataStore.Projects.SelectMany(p => p.Tasks).ToList();
        return Task.FromResult(allTasks);
    }

    public Task<Milestone?> GetMilestoneByIdAsync(string milestoneId)
    {
        var milestone = MockedDataStore.Projects.SelectMany(p => p.Milestones).FirstOrDefault(m => m.Id == milestoneId);
        return Task.FromResult(milestone);
    }

    public Task<List<Milestone>> GetAllMilestonesAsync()
    {
        var allMilestones = MockedDataStore.Projects.SelectMany(p => p.Milestones).ToList();
        return Task.FromResult(allMilestones);
    }

    public Task<List<Project>> GetProjectsAsync(List<string> projectIds)
    {
        var projects = MockedDataStore.Projects.Where(p => projectIds.Contains(p.Id)).ToList();
        return Task.FromResult(projects);
    }
}
