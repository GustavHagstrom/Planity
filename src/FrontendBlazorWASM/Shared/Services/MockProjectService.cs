using Planity.FrontendBlazorWASM.Shared.Models;

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
                Status = ProjectStatus.InProgress,
                Tasks = new List<ProjectTask>
                {
                    new ProjectTask { Id = "T1", Name = "Grävning", Status = ProjectTaskStatus.NotStarted, Start = DateTime.Today, AssignedResourceId = "1" },
                    new ProjectTask { Id = "T2", Name = "Gjutning platta", Status = ProjectTaskStatus.NotStarted, Start = DateTime.Today.AddDays(3), AssignedResourceId = "2"}
                },
                Milestones = new List<Milestone>
                {
                    new Milestone { Id = "M1", Name = "Platta klar", Date = DateTime.Today.AddDays(5), IsCompleted = false, ProjectId = Guid.Empty }
                }
            },
            new Project
            {
                Id = "P2",
                Name = "Renovera kök",
                Description = "Totalrenovering av kök.",
                Status = ProjectStatus.NotStarted,
                Tasks = new List<ProjectTask>
                {
                    new ProjectTask { Id = "T3", Name = "Spika", Status = ProjectTaskStatus.NotStarted, Start = DateTime.Today.AddDays(8.5), AssignedResourceId = "2" },
                    new ProjectTask { Id = "T4", Name = "Gjutning platta", Status = ProjectTaskStatus.NotStarted, Start = DateTime.Today.AddDays(9), AssignedResourceId = "3" }
                },
                Milestones = new List<Milestone>()
            },
            new Project
            {
                Id = "P3",
                Name = "Måla om huset",
                Description = "Fasadmålning och fönsterrenovering.",
                Status = ProjectStatus.Completed,
                Tasks = new List<ProjectTask>
                {
                    new ProjectTask { Id = "T5", Name = "Skrapa färg", Status = ProjectTaskStatus.Completed, Start = DateTime.Today.AddDays(-30), AssignedResourceId = "3" },
                    new ProjectTask { Id = "T6", Name = "Grundmåla", Status = ProjectTaskStatus.Completed, Start = DateTime.Today.AddDays(-27), AssignedResourceId = "1" },
                    new ProjectTask { Id = "T7", Name = "Slutmåla", Status = ProjectTaskStatus.Completed, Start = DateTime.Today.AddDays(-24), AssignedResourceId = "2" }
                },
                Milestones = new List<Milestone>
                {
                    new Milestone { Id = "M2", Name = "Halva huset klart", Date = DateTime.Today.AddDays(-23), IsCompleted = true, ProjectId = Guid.Empty },
                    new Milestone { Id = "M3", Name = "Färdigt!", Date = DateTime.Today.AddDays(-20), IsCompleted = true, ProjectId = Guid.Empty }
                }
            },
            new Project
            {
                Id = "P4",
                Name = "Trädgårdsanläggning",
                Description = "Anlägga ny trädgård med uteplats och gångar.",
                Status = ProjectStatus.InProgress,
                Tasks = new List<ProjectTask>
                {
                    new ProjectTask { Id = "T8", Name = "Markberedning", Status = ProjectTaskStatus.InProgress, Start = DateTime.Today.AddDays(-2), AssignedResourceId = "1"},
                    new ProjectTask { Id = "T9", Name = "Lägga plattor", Status = ProjectTaskStatus.NotStarted, Start = DateTime.Today.AddDays(3), AssignedResourceId = "2"}
                },
                Milestones = new List<Milestone>
                {
                    new Milestone { Id = "M4", Name = "Uteplats klar", Date = DateTime.Today.AddDays(7), IsCompleted = false, ProjectId = Guid.Empty }
                }
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
                existing.AssignedResourceId = task.AssignedResourceId;
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

    public Task<Milestone> AddMilestoneToProjectAsync(string projectId, Milestone milestone)
    {
        var project = _projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            milestone.Id = Guid.NewGuid().ToString();
            project.Milestones.Add(milestone);
        }
        return Task.FromResult(milestone);
    }

    public Task<Milestone> UpdateMilestoneAsync(string projectId, Milestone milestone)
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

    public Task<ProjectTask?> GetTaskByIdAsync(string taskId)
    {
        var task = _projects.SelectMany(p => p.Tasks).FirstOrDefault(t => t.Id == taskId);
        return Task.FromResult(task);
    }

    public Task<List<ProjectTask>> GetAllTasksAsync()
    {
        var allTasks = _projects.SelectMany(p => p.Tasks).ToList();
        return Task.FromResult(allTasks);
    }

    public Task<Milestone?> GetMilestoneByIdAsync(string milestoneId)
    {
        var milestone = _projects.SelectMany(p => p.Milestones).FirstOrDefault(m => m.Id == milestoneId);
        return Task.FromResult(milestone);
    }

    public Task<List<Milestone>> GetAllMilestonesAsync()
    {
        var allMilestones = _projects.SelectMany(p => p.Milestones).ToList();
        return Task.FromResult(allMilestones);
    }
}
