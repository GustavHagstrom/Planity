using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace Planity.FrontendBlazorWASM.Shared.Components.Gantt
{
    public static class GanttSvgHelper
    {
        public static MarkupString RenderRect(double x, double y, double width, double height, string fill)
        {
            var xStr = x.ToString(CultureInfo.InvariantCulture);
            var yStr = y.ToString(CultureInfo.InvariantCulture);
            var widthStr = width.ToString(CultureInfo.InvariantCulture);
            var heightStr = height.ToString(CultureInfo.InvariantCulture);
            return new MarkupString($"<rect x=\"{xStr}\" y=\"{yStr}\" width=\"{widthStr}\" height=\"{heightStr}\" fill=\"{fill}\" />");
        }

        public static MarkupString RenderLine(double x1, double x2, double y1, double y2, string stroke, double strokeWidth)
        {
            var x1Str = x1.ToString(CultureInfo.InvariantCulture);
            var x2Str = x2.ToString(CultureInfo.InvariantCulture);
            var y1Str = y1.ToString(CultureInfo.InvariantCulture);
            var y2Str = y2.ToString(CultureInfo.InvariantCulture);
            var strokeWidthStr = strokeWidth.ToString(CultureInfo.InvariantCulture);
            return new MarkupString($"<line x1=\"{x1Str}\" x2=\"{x2Str}\" y1=\"{y1Str}\" y2=\"{y2Str}\" stroke=\"{stroke}\" stroke-width=\"{strokeWidthStr}\" />");
        }

        public static MarkupString RenderText(double x, double y, string text, int fontSize = 12, string anchor = "middle")
        {
            var xStr = x.ToString(CultureInfo.InvariantCulture);
            var yStr = y.ToString(CultureInfo.InvariantCulture);
            return new MarkupString($"<text x=\"{xStr}\" y=\"{yStr}\" font-size=\"{fontSize}\" text-anchor=\"{anchor}\">{text}</text>");
        }
    }
}
