using Planity.FrontendBlazorWASM.Shared.Models;
using Planity.FrontendBlazorWASM; // Importera enums

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class MockProjectService : IProjectService
{
    private readonly List<Project> _projects;

    public MockProjectService()
    {
        _projects = new List<Project>
        {
            new Project
            {
                Id = "P1",
                Name = "Bygg nytt garage",
                Description = "Bygga ett nytt dubbelgarage.",
                OrganizationId = "Org1",
                Status = ProjectStatus.InProgress,
                Tasks = new List<ProjectTask>
                {
                    new ProjectTask { Id = "T1", Name = "Grävning", Status = TaskStatus.NotStarted, Start = DateTime.Today, End = DateTime.Today.AddDays(2), AssignedResourceId = "2", AssignedResourceName = "Bertil Berg" },
                    new ProjectTask { Id = "T2", Name = "Gjutning platta", Status = TaskStatus.NotStarted, Start = DateTime.Today.AddDays(3), End = DateTime.Today.AddDays(5), AssignedResourceId = "3", AssignedResourceName = "Cecilia Carlsson" }
                },
                Milestones = new List<Milesonte>
                {
                    new Milesonte { Id = "M1", Name = "Platta klar", Date = DateTime.Today.AddDays(5), IsCompleted = false, ProjectId = Guid.Empty }
                }
            },
            new Project
            {
                Id = "P2",
                Name = "Renovera kök",
                Description = "Totalrenovering av kök.",
                OrganizationId = "Org1",
                Status = ProjectStatus.NotStarted,
                Tasks = new List<ProjectTask>(),
                Milestones = new List<Milesonte>()
            }
        };
    }

    public Task<List<Project>> GetProjectsAsync() => Task.FromResult(_projects.ToList());

    public Task<Project?> GetProjectByIdAsync(string projectId) =>
        Task.FromResult(_projects.FirstOrDefault(p => p.Id == projectId));

    public Task<Project> CreateProjectAsync(Project project)
    {
        project.Id = Guid.NewGuid().ToString();
        _projects.Add(project);
        return Task.FromResult(project);
    }

    public Task<Project> UpdateProjectAsync(Project project)
    {
        var existing = _projects.FirstOrDefault(p => p.Id == project.Id);
        if (existing != null)
        {
            existing.Name = project.Name;
            existing.Description = project.Description;
            existing.Status = project.Status;
            existing.Tasks = project.Tasks;
            existing.Milestones = project.Milestones;
        }
        return Task.FromResult(project);
    }

    public Task DeleteProjectAsync(string projectId)
    {
        var project = _projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
            _projects.Remove(project);
        return Task.CompletedTask;
    }

    public Task<ProjectTask> AddTaskToProjectAsync(string projectId, ProjectTask task)
    {
        var project = _projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            task.Id = Guid.NewGuid().ToString();
            project.Tasks.Add(task);
        }
        return Task.FromResult(task);
    }

    public Task<ProjectTask> UpdateTaskAsync(string projectId, ProjectTask task)
    {
        var project = _projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            var existing = project.Tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existing != null)
            {
                existing.Name = task.Name;
                existing.Description = task.Description;
                existing.Status = task.Status;
                existing.Start = task.Start;
                existing.End = task.End;
                existing.AssignedResourceId = task.AssignedResourceId;
                existing.AssignedResourceName = task.AssignedResourceName;
            }
        }
        return Task.FromResult(task);
    }

    public Task DeleteTaskAsync(string projectId, string taskId)
    {
        var project = _projects.FirstOrDefault(p => p.Id == projectId);
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
        var project = _projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            var task = project.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
                task.AssignedResourceId = resourceId;
        }
        return Task.CompletedTask;
    }

    public Task<Milesonte> AddMilestoneToProjectAsync(string projectId, Milesonte milestone)
    {
        var project = _projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            milestone.Id = Guid.NewGuid().ToString();
            project.Milestones.Add(milestone);
        }
        return Task.FromResult(milestone);
    }

    public Task<Milesonte> UpdateMilestoneAsync(string projectId, Milesonte milestone)
    {
        var project = _projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            var existing = project.Milestones.FirstOrDefault(m => m.Id == milestone.Id);
            if (existing != null)
            {
                existing.Name = milestone.Name;
                existing.Description = milestone.Description;
                existing.Date = milestone.Date;
                existing.IsCompleted = milestone.IsCompleted;
            }
        }
        return Task.FromResult(milestone);
    }

    public Task DeleteMilestoneAsync(string projectId, string milestoneId)
    {
        var project = _projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            var milestone = project.Milestones.FirstOrDefault(m => m.Id == milestoneId);
            if (milestone != null)
                project.Milestones.Remove(milestone);
        }
        return Task.CompletedTask;
    }
}
