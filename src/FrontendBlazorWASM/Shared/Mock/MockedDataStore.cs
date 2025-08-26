using System;
using System.Collections.Generic;
using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Mock;

public static class MockedDataStore
{
    public static Dictionary<string, Resource> Resources { get; } = new()
    {
        ["1"] = new Resource { Id = "1", Name = "Produktionslina 1", OrganizationId = "Org1", Workers = 5, Efficiency = 1 },
        ["2"] = new Resource { Id = "2", Name = "Bertil Berg", OrganizationId = "Org1", Workers = 1, Efficiency = 1 },
        ["3"] = new Resource { Id = "3", Name = "Cecilia Carlsson", OrganizationId = "Org1", Workers = 1, Efficiency = 1.1 }
    };

    public static Dictionary<string, WorkCalendar> WorkCalendars { get; } = new()
    {
        ["1"] = CreateWorkCalendar1(),
        ["2"] = CreateWorkCalendar2(),
        ["3"] = CreateWorkCalendar3()
    };

    private static WorkCalendar CreateWorkCalendar1()
    {
        var calendar = new WorkCalendar
        {
            ResourceId = "1",
            Holidays = [new DateTime(DateTime.Now.Year, 4, 1)],
            OvertimeHours = new() { { DateTime.Today, 2 } }
        };
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Monday, 8.5);
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Tuesday, 8.5);
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Wednesday, 8.5);
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Thursday, 8.5);
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Friday, 6);
        // Lördag och söndag har redan 0 timmar som default
        return calendar;
    }

    private static WorkCalendar CreateWorkCalendar2()
    {
        var calendar = new WorkCalendar
        {
            ResourceId = "2",
            Holidays = [new DateTime(DateTime.Now.Year, 12, 24)],
            OvertimeHours = new() { { DateTime.Today.AddDays(1), 1.5 } }
        };
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Monday, 7);
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Tuesday, 7);
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Wednesday, 7);
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Thursday, 7);
        // Fredag, lördag och söndag har 0 timmar som default
        return calendar;
    }

    private static WorkCalendar CreateWorkCalendar3()
    {
        var calendar = new WorkCalendar
        {
            ResourceId = "3",
            Holidays = [],
            OvertimeHours = []
        };
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Monday, 8);
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Tuesday, 8);
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Wednesday, 8);
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Thursday, 8);
        calendar.SetWorkHoursByDayOfWeek(DayOfWeek.Friday, 8);
        // Lördag och söndag har redan 0 timmar som default
        return calendar;
    }

    public static List<Project> Projects { get; } =
    [
        new Project
        {
            Id = "P1",
            Name = "Bygg nytt garage",
            Description = "Bygga ett nytt dubbelgarage.",
            Tasks =
            [
                new ProjectTask { Id = "T1", Name = "Grävning", Start = DateTime.Today, ResourceId = "1", WorkHours = 160 },
                new ProjectTask { Id = "T2", Name = "Gjutning platta", Start = DateTime.Today.AddDays(3), ResourceId = "2", WorkHours = 12 }
            ],
            Milestones =
            [
                new Milestone { Id = "M1", Name = "Platta klar", Start = DateTime.Today.AddDays(5), ProjectId = Guid.Empty }
            ]
        },
        new Project
        {
            Id = "P2",
            Name = "Renovera kök",
            Description = "Totalrenovering av kök.",
            Tasks =
            [
                new ProjectTask { Id = "T3", Name = "Spika", Start = DateTime.Today.AddDays(8.5), ResourceId = "2", WorkHours = 6 },
                new ProjectTask { Id = "T4", Name = "Gjutning platta", Start = DateTime.Today.AddDays(9), ResourceId = "3", WorkHours = 20 }
            ],
            Milestones = []
        },
        new Project
        {
            Id = "P3",
            Name = "Måla om huset",
            Description = "Fasadmålning och fönsterrenovering.",
            Tasks =
            [
                new ProjectTask { Id = "T5", Name = "Skrapa färg", Start = DateTime.Today.AddDays(1), ResourceId = "3", WorkHours = 10 },
                new ProjectTask { Id = "T6", Name = "Grundmåla", Start = DateTime.Today.AddDays(2), ResourceId = "1", WorkHours = 180 },
                new ProjectTask { Id = "T7", Name = "Slutmåla", Start = DateTime.Today.AddDays(3), ResourceId = "2", WorkHours = 8 }
            ],
            Milestones =
            [
                new Milestone { Id = "M2", Name = "Halva huset klart", Start = DateTime.Today.AddDays(4), ProjectId = Guid.Empty },
                new Milestone { Id = "M3", Name = "Färdigt!", Start = DateTime.Today.AddDays(5), ProjectId = Guid.Empty }
            ]
        }
    ];
}
