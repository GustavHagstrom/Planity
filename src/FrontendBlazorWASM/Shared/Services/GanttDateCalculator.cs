using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Models;
using System.Resources;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class GanttDateCalculator(IResourceService resourceService) : IGanttDateCalculator
{
    public async Task<DateTime?> CalculateEndAsync(IGanttItem item)
    {
        var resources = await resourceService.GetOrganizationResources();
        return CalculateEnd(item, resources);
    }
    public DateTime? CalculateEnd(IGanttItem item, List<Resource> resources)
    {
        if (item.Start is null) return null;

        if (item is ProjectTask task)
        {
            var resource = resources.FirstOrDefault(r => r.Id == task.AssignedResourceId);
            if (resource is null) return null; // Ingen resurs tilldelad, kan inte ber�kna slutdatum
            var weeks = task.WorkHours / resource.Capacity;
            return task.Start!.Value.AddDays(weeks * 7);
        }
        else if (item is Milestone milestone)
        {
            return milestone.Start; // Milestones have no duration
        }
        else if (item is Project project)
        {
            var end = project.Tasks.Select(t => CalculateEnd(t, resources))
                .Concat(project.Milestones.Select(m => CalculateEnd(m, resources)))
                .Where(d => d.HasValue)
                .DefaultIfEmpty(null)
                .Max();
            return end;
        }

        // F�r Resource och andra typer kan du l�gga till logik h�r
        return null;
    }
}
