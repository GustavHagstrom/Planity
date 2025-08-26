using Planity.FrontendBlazorWASM.Shared.Models;
using Planity.FrontendBlazorWASM.Shared.Mock;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class MockedWorkCalendarService : IWorkCalendarService
{
    public Task<WorkCalendar> GetWorkCalendarAsync(string resourceId)
    {
        if (MockedDataStore.WorkCalendars.TryGetValue(resourceId, out var calendar))
            return Task.FromResult(calendar);
        return Task.FromResult(new WorkCalendar { ResourceId = resourceId });
    }

    public Task SetWorkHoursByDayOfWeekAsync(string resourceId, Dictionary<DayOfWeek, double> workHoursByDayOfWeek)
    {
        if (MockedDataStore.WorkCalendars.TryGetValue(resourceId, out var calendar))
        {
            foreach (var kvp in workHoursByDayOfWeek)
            {
                calendar.SetWorkHoursByDayOfWeek(kvp.Key, kvp.Value);
            }
        }
        return Task.CompletedTask;
    }

    public Task AddHolidayAsync(string resourceId, DateTime holiday)
    {
        if (MockedDataStore.WorkCalendars.TryGetValue(resourceId, out var calendar))
            calendar.Holidays.Add(holiday.Date);
        return Task.CompletedTask;
    }

    public Task RemoveHolidayAsync(string resourceId, DateTime holiday)
    {
        if (MockedDataStore.WorkCalendars.TryGetValue(resourceId, out var calendar))
            calendar.Holidays.Remove(holiday.Date);
        return Task.CompletedTask;
    }

    public Task SetOvertimeAsync(string resourceId, DateTime date, double overtimeHours)
    {
        if (MockedDataStore.WorkCalendars.TryGetValue(resourceId, out var calendar))
            calendar.OvertimeHours[date.Date] = overtimeHours;
        return Task.CompletedTask;
    }


    
}
