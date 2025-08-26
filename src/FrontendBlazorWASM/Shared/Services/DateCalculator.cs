using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Models;
using System.Resources;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class DateCalculator(IResourceService resourceService) : IDateCalculator
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
            double workerEff = resource.Workers * resource.Efficiency;
            bool isFirstDay = true;
            while (remainingHours > 0)
            {
                var periods = workCalendar.WorkPeriodsByDayOfWeek.TryGetValue(currentDate.DayOfWeek, out var p) ? p : new List<(TimeSpan, TimeSpan)>();
                bool isHoliday = workCalendar.Holidays.Contains(currentDate.Date);
                bool hasOvertime = workCalendar.OvertimeHours.TryGetValue(currentDate.Date, out var overtimeHours) && overtimeHours > 0;
                int passCount = periods.Count;
                bool hasOrdinaryWork = passCount > 0 && !isHoliday;
                bool hasAnyWork = hasOrdinaryWork || hasOvertime;
                if (!hasAnyWork)
                {
                    currentDate = currentDate.Date.AddDays(1);
                    isFirstDay = false;
                    continue;
                }
                // Iterera �ver ordinarie perioder
                foreach (var (periodStart, periodEnd) in periods)
                {
                    var actualStart = isFirstDay && currentDate.TimeOfDay > periodStart ? currentDate.TimeOfDay : periodStart;
                    if (actualStart >= periodEnd)
                        continue;
                    var availableHours = (periodEnd - actualStart).TotalHours * workerEff;
                    if (availableHours <= 0)
                        continue;
                    if (remainingHours <= availableHours)
                    {
                        var hoursUsed = remainingHours / workerEff;
                        return currentDate.Date.Add(actualStart).Add(TimeSpan.FromHours(hoursUsed));
                    }
                    remainingHours -= availableHours;
                    if (remainingHours <= 0) return currentDate.Date.Add(periodEnd);
                }
                // Hantera �vertid
                if (hasOvertime)
                {
                    TimeSpan overtimeStart = passCount > 0 ? periods[^1].Item2 : TimeSpan.FromHours(0);
                    var availableOvertime = overtimeHours * workerEff;
                    if (remainingHours <= availableOvertime)
                    {
                        var hoursUsed = remainingHours / workerEff;
                        return currentDate.Date.Add(overtimeStart).Add(TimeSpan.FromHours(hoursUsed));
                    }
                    remainingHours -= availableOvertime;
                    if (remainingHours <= 0) return currentDate.Date.Add(overtimeStart).Add(TimeSpan.FromHours(overtimeHours));
                }
                currentDate = currentDate.Date.AddDays(1);
                isFirstDay = false;
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

        // F�r Resource och andra typer kan du l�gga till logik h�r
        return null;
    }

    // Hj�lpmetod f�r att r�kna ut om det �r arbetsdag
    public bool IsWorkDay(WorkCalendar calendar, DateTime date)
    {
        return calendar.WorkPeriodsByDayOfWeek.TryGetValue(date.DayOfWeek, out var periods) && periods.Count > 0 && !calendar.Holidays.Contains(date.Date);
    }
    public async Task<DateTime?> CalculateStartAsync(IGanttItem item)
    {
        var resources = await resourceService.GetOrganizationResources();
        return CalculateStart(item, resources);
    }
    public DateTime? CalculateStart(IGanttItem  item, List<Resource> resources)
    {
        if (item.Start is null) return null;
        if (item is Milestone milestone) return milestone.Start;
        if (item is Resource) return null;
        if (item is Project project)
        {
            var start = project.Tasks.Select(t => CalculateStart(t, resources))
                .Concat(project.Milestones.Select(m => CalculateStart(m, resources)))
                .Where(d => d.HasValue)
                .DefaultIfEmpty(null)
                .Min();
            return start;
        }
        if (item is ProjectTask task)
        {
            var resource = resources.FirstOrDefault(r => r.Id == task.ResourceId);
            if (resource is null || resource.WorkCalendar is null) return task.Start;
            var calendar = resource.WorkCalendar;
            DateTime searchDate = task.Start!.Value;
            while (true)
            {
                if (calendar.WorkPeriodsByDayOfWeek.TryGetValue(searchDate.DayOfWeek, out var periods) && periods.Count > 0 && !calendar.Holidays.Contains(searchDate.Date))
                {
                    periods = periods.OrderBy(p => p.Start).ToList();
                    var firstPeriodStart = periods.First().Start;
                    if (searchDate.TimeOfDay < firstPeriodStart)
                    {
                        return searchDate.Date.Add(firstPeriodStart);
                    }
                    else if (searchDate.TimeOfDay >= periods.Last().End)
                    {
                        searchDate = searchDate.Date.AddDays(1);
                    }
                    else
                    {
                        // Om tiden �r inom en period, anv�nd tiden som den �r
                        return searchDate;
                    }
                }
                else
                {
                    // Ingen arbetstid denna dag, g� till n�sta dag
                    searchDate = searchDate.Date.AddDays(1);
                }
            }
        }
        return null;
        //if (item.Start is null) return null;
        //var resource = resources.FirstOrDefault(r => r.Id == item.ResourceId);
        //if (resource is null || resource.WorkCalendar is null) return item.Start;
        //var calendar = resource.WorkCalendar;
        //DateTime searchDate = item.Start.Value;
        //while (true)
        //{
        //    if (calendar.WorkPeriodsByDayOfWeek.TryGetValue(searchDate.DayOfWeek, out var periods) && periods.Count > 0 && !calendar.Holidays.Contains(searchDate.Date))
        //    {
        //        var firstPeriodStart = periods[0].Start;
        //        if (searchDate.TimeOfDay < firstPeriodStart)
        //        {
        //            return searchDate.Date.Add(firstPeriodStart);
        //        }
        //        else if (searchDate.TimeOfDay >= periods.Last().End)
        //        {
        //            searchDate = searchDate.Date.AddDays(1);
        //        }
        //        else
        //        {
        //            // Om tiden �r inom en period, anv�nd tiden som den �r
        //            return searchDate;
        //        }
        //    }
        //    else
        //    {
        //        // Ingen arbetstid denna dag, g� till n�sta dag
        //        searchDate = searchDate.Date.AddDays(1);
        //    }
        //}
    }
}
