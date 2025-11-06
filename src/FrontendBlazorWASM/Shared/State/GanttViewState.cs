using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Components.Gantt;
using Planity.FrontendBlazorWASM.Shared.Models;
using Planity.FrontendBlazorWASM.Shared.Services;

namespace Planity.FrontendBlazorWASM.Shared.State;

public class GanttViewState
{
    public DateTime Start { get; private set; } = DateTime.Today.AddMonths(-1);
    public DateTime End { get; private set; } = DateTime.Today.AddMonths(3);
    public GanttViewMode Mode { get; private set; } = GanttViewMode.Month;
    public IEnumerable<IGanttItem> Items { get; private set; } = [];
    public IGanttItem? SelectedItem { get; private set; }
    public List<Resource> Resources { get; private set; } = [];
    public IEnumerable<GanttItemLink> Links { get;  private set; } = [];
    public Dictionary<string, IGanttItem> ItemIdMap { get; private set; } = [];
    public HashSet<LinkKey> LinkKeys { get; private set; } = new();
    public GanttDragMode DragMode { get; private set; } = GanttDragMode.None;

    public int RowCount => GanttItemUtils.ItemsWithExpand(Items).TryGetNonEnumeratedCount(out int count) ? count : GanttItemUtils.ItemsWithExpand(Items).Count();


    public double Zoom { get; private set; } = 1.0;
    public string SvgWidthPx => $"{SvgWidth}px";
    
    public record struct LinkKey(string FromId, string ToId);
    public string LeftWidthPx => $"220px";
    public double RowHeight => 30;
    public string RowHeightPx => $"{RowHeight}px";
    public double HeaderHeight => 60;
    public string HeaderHeightPx => $"{HeaderHeight}px";
    public double PixelsPerDay => Mode switch
    {
        GanttViewMode.Year => 5 * Zoom,
        GanttViewMode.Month => 30 * Zoom,
        GanttViewMode.Week => 108 * Zoom,
        _ => throw new ArgumentOutOfRangeException()
    };
    public double SvgWidth => Mode switch
    {
        GanttViewMode.Year => PixelsPerDay * (End - Start).Days,
        GanttViewMode.Month => PixelsPerDay * (End - Start).Days,
        GanttViewMode.Week => PixelsPerDay * (End - Start).Days,
        _ => throw new ArgumentOutOfRangeException()
    };
   

    

}
