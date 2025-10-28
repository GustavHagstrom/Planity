using System;
using System.Collections.Generic;
using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Mock;

public static class MockedDataStore
{
    public static Dictionary<string, Resource> Resources { get; } = new()
    {
        ["1"] = new Resource { Id = "1", Name = "Produktionslina 1", OrganizationId = "OrganizationID123", Workers = 5, Efficiency = 1 },
        ["2"] = new Resource { Id = "2", Name = "Bertil Berg", OrganizationId = "OrganizationID123", Workers = 1, Efficiency = 1 },
        ["3"] = new Resource { Id = "3", Name = "Cecilia Carlsson", OrganizationId = "OrganizationID123", Workers = 1, Efficiency = 1.1 }
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
            Holidays = [new Holyday { Date = new DateOnly(DateTime.Now.Year, 4, 1), Name = "Första april" }],
            Overtime = [new Overtime { Date = DateOnly.FromDateTime(DateTime.Today), From = TimeSpan.FromHours(17), To = TimeSpan.FromHours(19) }],
            // Måndag–Torsdag: 08:00–12:00, 13:00–16:30 (raster 12:00–13:00), Fredag: 08:00–14:00
            WorkPeriods =
            [
                new WorkPeriod { Day = DayOfWeek.Monday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(16.5), BreakDuration = 1 },
                new WorkPeriod { Day = DayOfWeek.Tuesday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(16.5), BreakDuration = 1 },
                new WorkPeriod { Day = DayOfWeek.Wednesday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(16.5), BreakDuration = 1 },
                new WorkPeriod { Day = DayOfWeek.Thursday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(16.5), BreakDuration = 1 },
                new WorkPeriod { Day = DayOfWeek.Friday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(14), BreakDuration = 1 }
            ]
        };
        // Lördag och söndag har redan tomma perioder
        return calendar;
    }

    private static WorkCalendar CreateWorkCalendar2()
    {
        var calendar = new WorkCalendar
        {
            ResourceId = "2",
            Holidays = [new Holyday { Date = new DateOnly(DateTime.Now.Year, 12, 24), Name = "Julafton" }],
            Overtime = [new Overtime { Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)), From = TimeSpan.FromHours(17), To = TimeSpan.FromHours(19) }],
            // Måndag–Torsdag: 08:00–12:00, 13:00–15:00 (raster 12:00–13:00)
            WorkPeriods =
            [
                new WorkPeriod { Day = DayOfWeek.Monday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(12), BreakDuration = 1 },
                new WorkPeriod { Day = DayOfWeek.Tuesday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(12), BreakDuration = 1 },
                new WorkPeriod { Day = DayOfWeek.Wednesday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(12), BreakDuration = 1 },
                new WorkPeriod { Day = DayOfWeek.Thursday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(12), BreakDuration = 1 }
            ]
        };
        // Fredag, lördag och söndag har tomma perioder
        return calendar;
    }

    private static WorkCalendar CreateWorkCalendar3()
    {
        var calendar = new WorkCalendar
        {
            ResourceId = "3",
            // Måndag–Fredag: 08:00–12:00, 13:00–16:00 (raster 12:00–13:00)
            WorkPeriods =
            [
                new WorkPeriod { Day = DayOfWeek.Monday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(12), BreakDuration = 1 },
                new WorkPeriod { Day = DayOfWeek.Tuesday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(12), BreakDuration = 1 },
                new WorkPeriod { Day = DayOfWeek.Wednesday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(12), BreakDuration = 1 },
                new WorkPeriod { Day = DayOfWeek.Thursday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(12), BreakDuration = 1 },
                new WorkPeriod { Day = DayOfWeek.Friday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(12), BreakDuration = 1 }
            ]
        };
        // Lördag och söndag har tomma perioder
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
                new() { Id = "T1", Name = "Grävning", Start = DateTime.Today.AddHours(8), ResourceId = "1", WorkHours = 160 },
                new() { Id = "T2", Name = "Gjutning platta", Start = DateTime.Today.AddDays(3).AddHours(9), ResourceId = "2", WorkHours = 12 }
            ],
            Milestones =
            [
                new Milestone { Id = "M1", Name = "Platta klar", Start = DateTime.Today.AddDays(5).AddHours(15), ProjectId = Guid.Empty }
            ]
        },
        new Project
        {
            Id = "P2",
            Name = "Renovera kök",
            Description = "Totalrenovering av kök.",
            Tasks =
            [
                new ProjectTask { Id = "T3", Name = "Spika", Start = DateTime.Today.AddDays(8.5).AddHours(10), ResourceId = "2", WorkHours = 6 },
                new ProjectTask { Id = "T4", Name = "Gjutning platta", Start = DateTime.Today.AddDays(9).AddHours(8), ResourceId = "3", WorkHours = 20 }
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
                new ProjectTask { Id = "T5", Name = "Skrapa färg", Start = DateTime.Today.AddDays(1).AddHours(8), ResourceId = "3", WorkHours = 10 },
                new ProjectTask { Id = "T6", Name = "Grundmåla", Start = DateTime.Today.AddDays(2).AddHours(8), ResourceId = "1", WorkHours = 180 },
                new ProjectTask { Id = "T7", Name = "Slutmåla", Start = DateTime.Today.AddDays(3).AddHours(13), ResourceId = "2", WorkHours = 8 }
            ],
            Milestones =
            [
                new() { Id = "M2", Name = "Halva huset klart", Start = DateTime.Today.AddDays(4).AddHours(12), ProjectId = Guid.Empty },
                new() { Id = "M3", Name = "Färdigt!", Start = DateTime.Today.AddDays(5).AddHours(16), ProjectId = Guid.Empty }
            ]
        }
    ];
}
