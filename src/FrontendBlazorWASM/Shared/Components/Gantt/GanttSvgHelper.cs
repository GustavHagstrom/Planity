using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace Planity.FrontendBlazorWASM.Shared.Components.Gantt
{
    public static class GanttSvgHelper
    {
        // Overloads with string parameters
        public static MarkupString RenderRect(string x, string y, string width, string height, string fill, string opacity = "1.0")
        {
            return new MarkupString($"<rect x=\"{x}\" y=\"{y}\" width=\"{width}\" height=\"{height}\" fill=\"{fill}\" opacity=\"{opacity}\" />");
        }

        public static MarkupString RenderLine(string x1, string x2, string y1, string y2, string stroke, string strokeWidth, string opacity = "1.0")
        {
            return new MarkupString($"<line x1=\"{x1}\" x2=\"{x2}\" y1=\"{y1}\" y2=\"{y2}\" stroke=\"{stroke}\" stroke-width=\"{strokeWidth}\"  opacity=\"{opacity}\"/>");
        }

        public static MarkupString RenderText(string x, string y, string text, string fontSize = "12", string anchor = "middle", string opacity = "1.0")
        {
            return new MarkupString($"<text x=\"{x}\" y=\"{y}\" font-size=\"{fontSize}\" text-anchor=\"{anchor}\"  opacity=\"{opacity}\">{text}</text>");
        }

        // Existing methods now call the string overloads
        public static MarkupString RenderRect(double x, double y, double width, double height, string fill, double opacity = 1.0)
        {
            return RenderRect(
                x.ToString(CultureInfo.InvariantCulture),
                y.ToString(CultureInfo.InvariantCulture),
                width.ToString(CultureInfo.InvariantCulture),
                height.ToString(CultureInfo.InvariantCulture),
                fill,
                opacity.ToString(CultureInfo.InvariantCulture)
            );
        }

        public static MarkupString RenderLine(double x1, double x2, double y1, double y2, string stroke, double strokeWidth, double opacity = 1.0)
        {
            return RenderLine(
                x1.ToString(CultureInfo.InvariantCulture),
                x2.ToString(CultureInfo.InvariantCulture),
                y1.ToString(CultureInfo.InvariantCulture),
                y2.ToString(CultureInfo.InvariantCulture),
                stroke,
                strokeWidth.ToString(CultureInfo.InvariantCulture),
                opacity.ToString(CultureInfo.InvariantCulture)
            );
        }

        public static MarkupString RenderText(double x, double y, string text, int fontSize = 12, string anchor = "middle", double opacity = 1.0)
        {
            return RenderText(
                x.ToString(CultureInfo.InvariantCulture),
                y.ToString(CultureInfo.InvariantCulture),
                text,
                fontSize.ToString(CultureInfo.InvariantCulture),
                anchor,
                opacity.ToString(CultureInfo.InvariantCulture)
            );
        }
    }
}
