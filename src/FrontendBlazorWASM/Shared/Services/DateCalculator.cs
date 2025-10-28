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
    public DateTime? CalculateEnd(IGanttItem item, IReadOnlyList<Resource> resources)
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
                // Filtrera arbetspass för aktuell veckodag
                var dayPeriods = workCalendar.WorkPeriods
                    .Where(p => p.Day == currentDate.DayOfWeek)
                    .OrderBy(p => p.Start)
                    .ToList();

                bool isHoliday = workCalendar.Holidays.Any(x => x.Date == DateOnly.FromDateTime(currentDate.Date));
                bool hasOvertime = workCalendar.Overtime.Any(x => x.Date == DateOnly.FromDateTime(currentDate.Date) && x.TotalHours > 0);

                bool hasOrdinaryWork = dayPeriods.Count > 0 && !isHoliday;
                bool hasAnyWork = hasOrdinaryWork || hasOvertime;
                if (!hasAnyWork)
                {
                    currentDate = currentDate.Date.AddDays(1);
                    isFirstDay = false;
                    continue;
                }

                // Iterera över ordinarie perioder för dagen
                if (hasOrdinaryWork)
                {
                    foreach (var period in dayPeriods)
                    {
                        var actualStart = isFirstDay && currentDate.TimeOfDay > period.Start ? currentDate.TimeOfDay : period.Start;
                        if (actualStart >= period.End)
                            continue;

                        // Rå längd i timmar för arbetbara delen av passet
                        var rawHours = (period.End - actualStart).TotalHours;

                        // Justera för rast endast om passet överlappar antagen lunchrast (12:00–12:00+BreakDuration)
                        double lunchOverlapHours = 0;
                        if (period.BreakDuration > 0)
                        {
                            var lunchStart = TimeSpan.FromHours(12);
                            var lunchEnd = lunchStart + TimeSpan.FromHours(period.BreakDuration);

                            // Beräkna överlapp mellan [actualStart, period.End] och [lunchStart, lunchEnd]
                            var overlapStart = actualStart > lunchStart ? actualStart : lunchStart;
                            var overlapEnd = period.End < lunchEnd ? period.End : lunchEnd;
                            if (overlapEnd > overlapStart)
                            {
                                lunchOverlapHours = (overlapEnd - overlapStart).TotalHours;
                            }
                        }

                        var availableHours = Math.Max(0, rawHours - lunchOverlapHours) * workerEff;
                        if (availableHours <= 0)
                            continue;

                        if (remainingHours <= availableHours)
                        {
                            var hoursUsed = remainingHours / workerEff;
                            return currentDate.Date.Add(actualStart).Add(TimeSpan.FromHours(hoursUsed));
                        }
                        remainingHours -= availableHours;
                        if (remainingHours <= 0) return currentDate.Date.Add(period.End);
                    }
                }

                // Hantera övertid
                if (hasOvertime)
                {
                    // Övertid antas ske som ett sammanhängande pass efter dagens sista ordinarie period
                    var lastOrdinaryEnd = dayPeriods.Count > 0 ? dayPeriods[^1].End : TimeSpan.FromHours(8);

                    // Starttid för övertid: efter sista ordinarie sluttid om sådan finns, annars08:00.
                    // Om första dagen och starttiden redan är senare, börja där.
                    var overtimeStart = lastOrdinaryEnd;
                    if (isFirstDay && currentDate.TimeOfDay > overtimeStart)
                    {
                        overtimeStart = currentDate.TimeOfDay;
                    }

                    // FIX: Define overtimeHours from the calendar's Overtime entry for the current date
                    var overtimeEntry = workCalendar.Overtime.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(currentDate.Date));
                    var overtimeHours = overtimeEntry?.TotalHours ?? 0;

                    var availableOvertime = overtimeHours * workerEff; // effekt-justerade timmar
                    if (availableOvertime > 0)
                    {
                        if (remainingHours <= availableOvertime)
                        {
                            var hoursUsed = remainingHours / workerEff;
                            return currentDate.Date.Add(overtimeStart).Add(TimeSpan.FromHours(hoursUsed));
                        }

                        // Konsumera hela övertidspasset denna dag och fortsätt till nästa dag
                        remainingHours -= availableOvertime;
                    }
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

        // För Resource och andra typer kan du lägga till logik här
        return null;
    }

    // Hjälpmetod för att räkna ut om det är arbetsdag
    public bool IsWorkDay(WorkCalendar calendar, DateTime date)
    {
        var dayPeriods = calendar.WorkPeriods.Where(p => p.Day == date.DayOfWeek).ToList();
        return dayPeriods.Count > 0 && !calendar.Holidays.Any(x => x.Date == DateOnly.FromDateTime(date.Date));
    }
    public async Task<DateTime?> CalculateStartAsync(IGanttItem item)
    {
        var resources = await resourceService.GetOrganizationResources();
        return CalculateStart(item, resources);
    }
    public DateTime? CalculateStart(IGanttItem item, IReadOnlyList<Resource> resources)
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
                var dayPeriods = calendar.WorkPeriods
                    .Where(p => p.Day == searchDate.DayOfWeek)
                    .OrderBy(p => p.Start)
                    .ToList();

                if (dayPeriods.Count > 0 && !calendar.Holidays.Any(x => x.Date == DateOnly.FromDateTime(searchDate.Date)))
                {
                    var firstPeriodStart = dayPeriods.First().Start;
                    var lastPeriodEnd = dayPeriods.Last().End;
                    if (searchDate.TimeOfDay < firstPeriodStart)
                    {
                        return searchDate.Date.Add(firstPeriodStart);
                    }
                    else if (searchDate.TimeOfDay >= lastPeriodEnd)
                    {
                        searchDate = searchDate.Date.AddDays(1);
                    }
                    else
                    {
                        // Om tiden är inom en period, använd tiden som den är
                        return searchDate;
                    }
                }
                else
                {
                    // Ingen arbetstid denna dag, gå till nästa dag
                    searchDate = searchDate.Date.AddDays(1);
                }
            }
        }
        return null;
    }
}
