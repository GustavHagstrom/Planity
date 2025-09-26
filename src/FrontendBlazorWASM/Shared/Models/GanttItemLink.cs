using Planity.FrontendBlazorWASM.Shared.Abstractions;
using System;

namespace Planity.FrontendBlazorWASM.Shared.Models;


public class GanttItemLink
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public IGanttItem From { get; set; } = null!;
    public IGanttItem To { get; set; } = null!;
    public string? Comment { get; set; }
    public GanttItemLink Clone()
    {
        return new GanttItemLink
        {
            Id = this.Id,
            From = this.From,
            To = this.To,
            Comment = this.Comment
        };
    }
}
