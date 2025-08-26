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
            var resource = resources.FirstOrDefault(r => r.Id == task.ResourceId);
            if (resource is null || resource.WorkCalendar is null) return null;
            var workCalendar = resource.WorkCalendar;
            var remainingHours = task.WorkHours;
            var currentDate = task.Start!.Value;
            while (remainingHours > 0)
            {
                var dayHours = GetWorkHours(workCalendar, currentDate);
                var dayCapacity = dayHours * resource.Workers * resource.Efficiency;
                remainingHours -= dayCapacity;
                // Om arbetet är klart, returnera aktuellt datum
                if (remainingHours <= 0)
                    return currentDate;
                currentDate = currentDate.AddDays(1);
            }
            return currentDate;
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

        // För Resource och andra typer kan du lägga till logik här
        return null;
    }

    public bool IsWorkDay(WorkCalendar calendar, DateTime date)
    {
        // Arbetsdag om arbetstimmar > 0 och inte helgdag
        return calendar.WorkHoursByDayOfWeek.TryGetValue(date.DayOfWeek, out var h) && h > 0 && !calendar.Holidays.Contains(date.Date);
    }

    public double GetWorkHours(WorkCalendar calendar, DateTime date)
    {
        var baseHours = calendar.WorkHoursByDayOfWeek.TryGetValue(date.DayOfWeek, out var h) ? h : 0;
        var overtime = calendar.OvertimeHours.TryGetValue(date.Date, out var ot) ? ot : 0;
        return IsWorkDay(calendar, date) ? baseHours + overtime : overtime;
    }
}
