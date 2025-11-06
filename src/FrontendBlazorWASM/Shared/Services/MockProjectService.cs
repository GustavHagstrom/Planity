using Planity.FrontendBlazorWASM.Shared.Models;
using Planity.FrontendBlazorWASM.Shared.Mock;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class MockProjectService : IProjectService
{
    public Task<List<Project>> GetAllAsync()
    {
        foreach (var project in MockedDataStore.Projects)
        {
            var tasks = MockedDataStore.Tasks.Where(t => t.ProjectId == project.Id).ToList();
            var milestones = MockedDataStore.Milestones.Where(m => m.ProjectId == project.Id).ToList();
            project.Tasks = tasks;
            project.Milestones = milestones;
        }
        return Task.FromResult(MockedDataStore.Projects);
    }

    public Task<Project?> GetAsync(string projectId)
    {
        var project = MockedDataStore.Projects.FirstOrDefault(p => p.Id == projectId);
        var tasks = MockedDataStore.Tasks.Where(t => t.ProjectId == projectId).ToList();
        var milestones = MockedDataStore.Milestones.Where(m => m.ProjectId == projectId).ToList();
        if (project != null)
        {
            project.Tasks = tasks;
            project.Milestones = milestones;
        }
        return Task.FromResult(project);
    }

    public Task<Project> CreateAsync(Project project)
    {
        MockedDataStore.Projects.Add(project);
        RemoveExistingTasks(project);
        RemoveExistingMilestones(project);
        MockedDataStore.Tasks.AddRange(project.Tasks);
        MockedDataStore.Milestones.AddRange(project.Milestones);
        return Task.FromResult(project);
    }
    
    public Task<Project> UpdateAsync(Project project)
    {

        var existing = MockedDataStore.Projects.FirstOrDefault(p => p.Id == project.Id);
        if (existing != null)
            MockedDataStore.Projects.Remove(existing!);
        MockedDataStore.Projects.Add(project);
        RemoveExistingTasks(project);
        RemoveExistingMilestones(project);
        MockedDataStore.Tasks.AddRange(project.Tasks);
        MockedDataStore.Milestones.AddRange(project.Milestones);
        return Task.FromResult(project);
    }

    public Task DeleteAsync(string projectId)
    {
        var project = MockedDataStore.Projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            MockedDataStore.Projects.Remove(project);
            RemoveExistingTasks(project);
            RemoveExistingMilestones(project);
        }
        return Task.CompletedTask;
    }
    public async Task<List<Project>> GetAllAsync(List<string> projectIds)
    {

        var projects = new List<Project>();
        foreach (var projectId in projectIds)
        {
            var project = await GetAsync(projectId);
            if (project != null)
            {
                projects.Add(project);
            }
        }
        return projects;
    }
    private void RemoveExistingTasks(Project project)
    {
        var existingTasks = MockedDataStore.Tasks.Where(t => t.ProjectId == project.Id).ToList();
        foreach (var task in existingTasks)
        {
            MockedDataStore.Tasks.Remove(task);
        }
    }
    private void RemoveExistingMilestones(Project project)
    {
        var existingMilestones = MockedDataStore.Milestones.Where(m => m.ProjectId == project.Id).ToList();
        foreach (var milestone in existingMilestones)
        {
            MockedDataStore.Milestones.Remove(milestone);
        }
    }
}
