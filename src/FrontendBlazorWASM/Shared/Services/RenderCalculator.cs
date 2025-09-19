using MudBlazor.Extensions;
using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services
{
    public struct BarCoordinates
    {
        public string X { get; set; }
        
        public string Y { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }

    public struct ConnectionCoordinates
    {
        public string StartX { get; set; }
        public string StartY { get; set; }
        public string EndX { get; set; }
        public string EndY { get; set; }
    }

    public struct DragHandleCoordinates
    {
        public string X { get; set; }
        public string Y { get; set; }
    }
    public struct MileStoneCoordinates
    {
        public string CenterX { get; set; }
        public string CenterY { get; set; }
        public string Points { get; set; }
    }
    public interface IRenderCalculator
    {
        BarCoordinates? CalculateBarCoordinates(
            IGanttItem item,
            IEnumerable<object> resources,
            DateTime viewStart,
            DateTime viewEnd,
            double pixelsPerDay,
            double rowHeight,
            int index);

        MileStoneCoordinates? CalculateMilestoneCoordinates(
            IGanttItem item,
            DateTime viewStart,
            double pixelsPerDay,
            double rowHeight,
            int index);

        ConnectionCoordinates? CalculateConnectionCoordinates(
            IGanttItem fromItem,
            IGanttItem toItem,
            IEnumerable<object> resources,
            DateTime viewStart,
            DateTime viewEnd,
            double pixelsPerDay,
            double rowHeight,
            int fromIndex,
            int toIndex);

        DragHandleCoordinates? CalculateDragHandleCoordinates(
            IGanttItem item,
            IEnumerable<object> resources,
            DateTime viewStart,
            DateTime viewEnd,
            double pixelsPerDay,
            double rowHeight,
            int index,
            bool isStartHandle);
    }

    public class RenderCalculator : IRenderCalculator
    {
        private readonly IDateCalculator _dateCalculator;

        public RenderCalculator(IDateCalculator dateCalculator)
        {
            _dateCalculator = dateCalculator;
        }

        public BarCoordinates? CalculateBarCoordinates(
            IGanttItem item,
            IEnumerable<object> resources,
            DateTime viewStart,
            DateTime viewEnd,
            double pixelsPerDay,
            double rowHeight,
            int index)
        {
            var resourceList = resources.OfType<Resource>().ToList();
            var start = _dateCalculator.CalculateStart(item, resourceList);
            var end = _dateCalculator.CalculateEnd(item, resourceList);
            if (!start.HasValue || !end.HasValue) return null;
            var renderStart = start.Value < viewStart ? viewStart : start.Value;
            var renderEnd = end.Value > viewEnd ? viewEnd : end.Value;
            var startX = (renderStart - viewStart).TotalDays * pixelsPerDay;
            var endX = (renderEnd - viewStart).TotalDays * pixelsPerDay;
            var y = (index * rowHeight + 4);
            var height = rowHeight - 8;
            return new BarCoordinates
            {
                X = startX.ToInvariantString(),
                Width = (endX - startX).ToInvariantString(),
                Y = y.ToInvariantString(),
                Height = height.ToInvariantString()
            };
        }

        public MileStoneCoordinates? CalculateMilestoneCoordinates(
            IGanttItem item,
            DateTime viewStart,
            double pixelsPerDay,
            double rowHeight,
            int index)
        {
            if (!item.Start.HasValue) return null;
            var centerX = ((item.Start.Value - viewStart).TotalDays * pixelsPerDay);
            var centerY = (index * rowHeight) + (rowHeight / 2);
            var halfSize = rowHeight / 2 * 0.7;
            var topY = (centerY - halfSize);
            var rightX = centerX + halfSize;
            var bottomY = centerY + halfSize;
            var leftX = centerX - halfSize;
            return new MileStoneCoordinates
            {
                CenterX = centerX.ToInvariantString(),
                CenterY = centerY.ToInvariantString(),
                Points = $"{centerX.ToInvariantString()},{topY.ToInvariantString()} {rightX.ToInvariantString()},{centerY.ToInvariantString()} {centerX.ToInvariantString()},{bottomY.ToInvariantString()} {leftX.ToInvariantString()},{centerY.ToInvariantString()}"
            };
        }

        public ConnectionCoordinates? CalculateConnectionCoordinates(
            IGanttItem fromItem,
            IGanttItem toItem,
            IEnumerable<object> resources,
            DateTime viewStart,
            DateTime viewEnd,
            double pixelsPerDay,
            double rowHeight,
            int fromIndex,
            int toIndex)
        {
            var start = CalculateItemPoint(fromItem, resources, viewStart, viewEnd, pixelsPerDay, rowHeight, fromIndex, false);
            var end = CalculateItemPoint(toItem, resources, viewStart, viewEnd, pixelsPerDay, rowHeight, toIndex, true);
            if (!start.HasValue || !end.HasValue) return null;
            return new ConnectionCoordinates
            {
                StartX = start.Value.X,
                StartY = start.Value.Y,
                EndX = end.Value.X,
                EndY = end.Value.Y
            };
        }

        public DragHandleCoordinates? CalculateDragHandleCoordinates(
            IGanttItem item,
            IEnumerable<object> resources,
            DateTime viewStart,
            DateTime viewEnd,
            double pixelsPerDay,
            double rowHeight,
            int index,
            bool isStartHandle)
        {
            return CalculateItemPoint(item, resources, viewStart, viewEnd, pixelsPerDay, rowHeight, index, isStartHandle);
        }

        private DragHandleCoordinates? CalculateItemPoint(
            IGanttItem item,
            IEnumerable<object> resources,
            DateTime viewStart,
            DateTime viewEnd,
            double pixelsPerDay,
            double rowHeight,
            int index,
            bool isStart)
        {
            var resourceList = resources.OfType<Resource>().ToList();
            DateTime? date = isStart ? _dateCalculator.CalculateStart(item, resourceList) : _dateCalculator.CalculateEnd(item, resourceList);
            if (!date.HasValue) return null;
            var renderDate = isStart
                ? (date.Value < viewStart ? viewStart : date.Value)
                : (date.Value > viewEnd ? viewEnd : date.Value);
            var x = (renderDate - viewStart).TotalDays * pixelsPerDay;
            var y = (index * rowHeight) + (rowHeight / 2);
            return new DragHandleCoordinates { X = x, Y = y };
        }
    }
}
