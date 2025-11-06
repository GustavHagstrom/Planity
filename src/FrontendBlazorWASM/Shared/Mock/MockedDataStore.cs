using System;
using System.Collections.Generic;
using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Mock;

public static class MockedDataStore
{
    public static List<Project> Projects { get; } =
    [
        new Project
        {
            Id = "P1",
            Name = "Bygg nytt garage",
            Description = "Bygga ett nytt dubbelgarage."
        },
        new Project
        {
            Id = "P2",
            Name = "Renovera kök",
            Description = "Totalrenovering av kök."
        },
        new Project
        {
            Id = "P3",
            Name = "Måla om huset",
            Description = "Fasadmålning och fönsterrenovering."
        }
    ];
    public static List<Resource> Resources { get; } =
    [
        new Resource { Id = "1", Name = "Produktionslina 1", OrganizationId = "OrganizationID123", Workers = 5, Efficiency = 1 },
        new Resource { Id = "2", Name = "Bertil Berg", OrganizationId = "OrganizationID123", Workers = 1, Efficiency = 1 },
        new Resource { Id = "3", Name = "Cecilia Carlsson", OrganizationId = "OrganizationID123", Workers = 1, Efficiency = 1.1 }
    ];
    public static List<ProjectTask> Tasks { get; } =
    [
        new() { Id = "T1", ProjectId = "P1", ResourceId = "1", Name = "Grävning", Start = DateTime.Today.AddHours(8), WorkHours = 160 },
        new() { Id = "T2", ProjectId = "P1", ResourceId = "2", Name = "Gjutning platta", Start = DateTime.Today.AddDays(3).AddHours(9), WorkHours = 12 },
        new() { Id = "T3", ProjectId = "P2", ResourceId = "2", Name = "Spika", Start = DateTime.Today.AddDays(8.5).AddHours(10), WorkHours = 6 },
        new() { Id = "T4", ProjectId = "P2", ResourceId = "3", Name = "Gjutning platta", Start = DateTime.Today.AddDays(9).AddHours(8), WorkHours = 20 },
        new() { Id = "T5", ProjectId = "P3", ResourceId = "3", Name = "Skrapa färg", Start = DateTime.Today.AddDays(1).AddHours(8), WorkHours = 10 },
        new() { Id = "T6", ProjectId = "P3", ResourceId = "1", Name = "Grundmåla", Start = DateTime.Today.AddDays(2).AddHours(8), WorkHours = 180 },
        new() { Id = "T7", ProjectId = "P3", ResourceId = "2", Name = "Slutmåla", Start = DateTime.Today.AddDays(3).AddHours(13), WorkHours = 8 }

    ];
    public static List<Milestone> Milestones { get; } =
    [
        new Milestone { Id = "M1", ProjectId = "P1", Name = "Platta klar", Start = DateTime.Today.AddDays(5).AddHours(15) },
        new Milestone { Id = "M2", ProjectId = "P3", Name = "Halva huset klart", Start = DateTime.Today.AddDays(4).AddHours(12) },
        new Milestone { Id = "M3", ProjectId = "P3", Name = "Färdigt!", Start = DateTime.Today.AddDays(5).AddHours(16) }
    ];
    public static List<WorkCalendar> WorkCalendars { get; } =
    [
        CreateWorkCalendar1(),
        CreateWorkCalendar2(),
        CreateWorkCalendar3()
    ];


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

    
}
