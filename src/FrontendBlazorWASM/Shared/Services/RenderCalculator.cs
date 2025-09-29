using MudBlazor.Extensions;
using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Models;
using System.Globalization;

namespace Planity.FrontendBlazorWASM.Shared.Services
{
    public struct Bar
    {
        public Point Start { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public Point StartHandle { get; set; }
        public Point EndHandle { get; set; }
    }
    public struct MileStone
    {
        public Point Center { get; set; }
        public string ShapePoints { get; set; }
    }
    public struct Connection
    {
        public Point Start { get; set; }
        public Point End { get; set; }
    }
    public struct Point
    {
        public string X { get; set; }
        public string Y { get; set; }
    }
    
    public interface IRenderCalculator
    {
        Bar? CalculateBar(
            IGanttItem item,
            List<Resource> resources,
            DateTime viewStart,
            double pixelsPerDay,
            double rowHeight,
            int index);

        MileStone? CalculateMilestone(
            IGanttItem item,
            DateTime viewStart,
            double pixelsPerDay,
            double rowHeight,
            int index);

        Connection? CalculateConnection(
            IGanttItem fromItem,
            IGanttItem toItem,
            List<Resource> resources,
            DateTime viewStart,
            double pixelsPerDay,
            double rowHeight,
            int fromIndex,
            int toIndex);

        Point? CalculateBarDragHandlePoint(
            DateTime viewStart,
            DateTime date,
            double pixelsPerDay,
            double rowHeight,
            int index,
            bool isStart);
    }

    public class RenderCalculator : IRenderCalculator
    {
        private readonly IDateCalculator _dateCalculator;
        private const double HandleOffset = 8;
        private const double MilestoneSizeFactor = 0.7;

        double MilestoneHalfSize(double rowHeight) => (rowHeight / 2) * MilestoneSizeFactor;
        public RenderCalculator(IDateCalculator dateCalculator)
        {
            _dateCalculator = dateCalculator;
        }
        public Bar? CalculateBar(
            IGanttItem item,
            List<Resource> resources,
            DateTime viewStart,
            double pixelsPerDay,
            double rowHeight,
            int index)
        {
            var start = _dateCalculator.CalculateStart(item, resources);
            var end = _dateCalculator.CalculateEnd(item, resources);
            if (!start.HasValue || !end.HasValue) return null;

            var startHandle = CalculateBarDragHandlePoint(viewStart, start.Value, pixelsPerDay, rowHeight, index, true);
            var endHandle = CalculateBarDragHandlePoint(viewStart, end.Value, pixelsPerDay, rowHeight, index, false);
            if (!startHandle.HasValue || !endHandle.HasValue) return null;

            var startX = (start.Value - viewStart).TotalDays * pixelsPerDay;
            var endX = (end.Value - viewStart).TotalDays * pixelsPerDay;
            
            var y = (index * rowHeight + 4);
            var height = rowHeight - 8;
            return new Bar
            {
                Start = new Point
                {
                    X = startX.ToInvariantString(),
                    Y = y.ToInvariantString()
                },
                Width = (endX - startX).ToInvariantString(),
                Height = height.ToInvariantString(),
                StartHandle = startHandle.Value,
                EndHandle = endHandle.Value
            };
        }

        public MileStone? CalculateMilestone(
            IGanttItem item,
            DateTime viewStart,
            double pixelsPerDay,
            double rowHeight,
            int index)
        {
            if (!item.Start.HasValue) return null;
            var centerX = ((item.Start.Value - viewStart).TotalDays * pixelsPerDay);
            var centerY = (index * rowHeight) + (rowHeight / 2);
            var halfSize = MilestoneHalfSize(rowHeight);
            var topY = (centerY - halfSize);
            var rightX = centerX + halfSize;
            var bottomY = centerY + halfSize;
            var leftX = centerX - halfSize;
            return new MileStone
            {
                Center = new Point
                {
                    X = centerX.ToInvariantString(),
                    Y = centerY.ToInvariantString()
                },
                ShapePoints = $"{centerX.ToInvariantString()},{topY.ToInvariantString()} {rightX.ToInvariantString()},{centerY.ToInvariantString()} {centerX.ToInvariantString()},{bottomY.ToInvariantString()} {leftX.ToInvariantString()},{centerY.ToInvariantString()}"
            };
        }

        public Connection? CalculateConnection(
            IGanttItem fromItem,
            IGanttItem toItem,
            List<Resource> resources,
            DateTime viewStart,
            double pixelsPerDay,
            double rowHeight,
            int fromIndex,
            int toIndex)
        {
            var startDate = _dateCalculator.CalculateEnd(fromItem, resources);
            var endDate = _dateCalculator.CalculateStart(toItem, resources);
            if (!startDate.HasValue || !endDate.HasValue) return null;

            Point? start;
            Point? end;

            if (fromItem.Type == GanttItemType.Milestone)
            {

                start = CalculateMilestone(fromItem, viewStart, pixelsPerDay, rowHeight, fromIndex)?.Center;
                start = FixConnectionOffset(start, MilestoneHalfSize(rowHeight));                    
            }
            else
            {
                start = CalculateBarDragHandlePoint(viewStart, startDate.Value, pixelsPerDay, rowHeight, fromIndex, false);
                start = FixConnectionOffset(start, HandleOffset);
            }
            if (toItem.Type == GanttItemType.Milestone)
            {

                end = CalculateMilestone(toItem, viewStart, pixelsPerDay, rowHeight, toIndex)?.Center;
                end = FixConnectionOffset(end, -MilestoneHalfSize(rowHeight));
            }
            else
            {
                end = CalculateBarDragHandlePoint(viewStart, endDate.Value, pixelsPerDay, rowHeight, toIndex, true);
                end = FixConnectionOffset(end, -HandleOffset);
            }

            if (!start.HasValue || !end.HasValue) return null;

            return new Connection
            {
                Start = start.Value,
                End = end.Value,
            };
        }
        private Point? FixConnectionOffset(Point? point, double offset)
        {
            if(!point.HasValue) return point;
            point = new Point
            {
                X = (double.Parse(point.Value.X, CultureInfo.InvariantCulture) + offset).ToInvariantString(),
                Y = point.Value.Y
            };
            return point;

        }
        public Point? CalculateBarDragHandlePoint(
            DateTime viewStart,
            DateTime date,
            double pixelsPerDay,
            double rowHeight,
            int index,
            bool isStart)
        {
            var offset = isStart ? HandleOffset : HandleOffset * -1;
            var x = (date - viewStart).TotalDays * pixelsPerDay + offset;
            var y = (index * rowHeight) + (rowHeight / 2);
            return new Point { X = x.ToInvariantString(), Y = y.ToInvariantString() };
        }

        
    }
}
