using Planity.FrontendBlazorWASM.Shared.Abstractions;
using System;

namespace Planity.FrontendBlazorWASM.Shared.Models;


public class GanttItemLink
{
    public IGanttItem From { get; set; } = null!;
    public IGanttItem To { get; set; } = null!;
    public string? Comment { get; set; }
    public GanttItemLink Clone()
    {
        return new GanttItemLink
        {
            From = this.From,
            To = this.To,
            Comment = this.Comment
        };
    }
}
