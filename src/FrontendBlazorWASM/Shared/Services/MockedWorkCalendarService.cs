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

    public Task AddWorkPeriod(string resourceId, WorkPeriod period)
    {
        if (MockedDataStore.WorkCalendars.TryGetValue(resourceId, out var calendar))
        {
            calendar.WorkPeriods.Add(period);
        }
        return Task.CompletedTask;
    }
    public Task AddWorkPeriods(string resourceId, IEnumerable<WorkPeriod> periods)
    {
        if (MockedDataStore.WorkCalendars.TryGetValue(resourceId, out var calendar))
        {
            foreach (var period in periods)
                calendar.WorkPeriods.Add(period);
        }
        return Task.CompletedTask;
    }

    public Task AddHolidayAsync(string resourceId, Holyday holyday)
    {
        if (MockedDataStore.WorkCalendars.TryGetValue(resourceId, out var calendar))
            calendar.Holidays.Add(holyday);
        return Task.CompletedTask;
    }

    public Task RemoveHolidayAsync(string resourceId, Holyday holyday)
    {
        if (MockedDataStore.WorkCalendars.TryGetValue(resourceId, out var calendar))
            calendar.Holidays.Remove(holyday);
        return Task.CompletedTask;
    }

    public Task SetOvertimeAsync(string resourceId, Overtime overtime)
    {
        if (MockedDataStore.WorkCalendars.TryGetValue(resourceId, out var calendar))
            calendar.Overtime.Add(overtime);
        return Task.CompletedTask;
    }

    public Task RemoveOvertimeAsync(string resourceId, Overtime overtime)
    {
        if (MockedDataStore.WorkCalendars.TryGetValue(resourceId, out var calendar))
            calendar.Overtime.Remove(overtime);
        return Task.CompletedTask;
    }
}
