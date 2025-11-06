using Planity.FrontendBlazorWASM.Shared.Models;
using Planity.FrontendBlazorWASM.Shared.Mock;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class MockedWorkCalendarService : IWorkCalendarService
{
    public Task<WorkCalendar> GetWorkCalendarAsync(string resourceId)
    {
        var existingCalendar = MockedDataStore.WorkCalendars.FirstOrDefault(x => x.ResourceId == resourceId);
        if (existingCalendar is not null)
        {
            return Task.FromResult(existingCalendar);
        }
        else
        {
            var newCalendar = new WorkCalendar
            {
                ResourceId = resourceId,
                WorkPeriods = new HashSet<WorkPeriod>(),
                Holidays = new HashSet<Holyday>(),
                Overtime = new HashSet<Overtime>()
            };
            MockedDataStore.WorkCalendars.Add(newCalendar);
            return Task.FromResult(newCalendar);
        }
    }

    public Task AddWorkPeriod(string resourceId, WorkPeriod period)
    {
        var existingCalendar = MockedDataStore.WorkCalendars.FirstOrDefault(x => x.ResourceId == resourceId);
        if (existingCalendar is not null)
        {
            existingCalendar.WorkPeriods.Add(period);
        }
        return Task.CompletedTask;
    }
    public Task AddWorkPeriods(string resourceId, IEnumerable<WorkPeriod> periods)
    {
        var existingCalendar = MockedDataStore.WorkCalendars.FirstOrDefault(x => x.ResourceId == resourceId);
        if (existingCalendar is not null)
        {
            foreach (var period in periods)
            {
                existingCalendar.WorkPeriods.Add(period);
            }
        }
        return Task.CompletedTask;
    }

    public Task AddHolidayAsync(string resourceId, Holyday holyday)
    {
        var calendar = MockedDataStore.WorkCalendars.FirstOrDefault(x => x.ResourceId == resourceId);
        if (calendar is not null)
            calendar.Holidays.Add(holyday);
        return Task.CompletedTask;
    }

    public Task RemoveHolidayAsync(string resourceId, Holyday holyday)
    {
        var calendar = MockedDataStore.WorkCalendars.FirstOrDefault(x => x.ResourceId == resourceId);

        if (calendar is not null)
        {
            var existingHolyday = calendar.Holidays.FirstOrDefault(h => h.Date == holyday.Date && h.Name == holyday.Name);
            if (existingHolyday is not null)
            {
                calendar.Holidays.Remove(existingHolyday);
            }
        }
        return Task.CompletedTask;
    }

    public Task SetOvertimeAsync(string resourceId, Overtime overtime)
    {
        var calendar = MockedDataStore.WorkCalendars.FirstOrDefault(x => x.ResourceId == resourceId);
        if (calendar is not null)
        {
            var existingOvertime = calendar.Overtime.FirstOrDefault(o => o.Date == overtime.Date);
            if (existingOvertime is not null)
            {
                calendar.Overtime.Remove(existingOvertime);
            }
            calendar.Overtime.Add(overtime);
        }
        return Task.CompletedTask;
    }

    public Task RemoveOvertimeAsync(string resourceId, Overtime overtime)
    {
        var calendar = MockedDataStore.WorkCalendars.FirstOrDefault(x => x.ResourceId == resourceId);
        if (calendar is not null)
        {
            var existingOvertime = calendar.Overtime.FirstOrDefault(o => o.Date == overtime.Date);
            if (existingOvertime is not null)
            {
                calendar.Overtime.Remove(existingOvertime);
            }
        }
        return Task.CompletedTask;
    }
}
