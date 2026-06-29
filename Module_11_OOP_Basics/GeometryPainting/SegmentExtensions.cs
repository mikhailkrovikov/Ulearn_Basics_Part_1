// Вставьте сюда финальное содержимое файла SegmentExtensions.cs
using System.Collections.Generic;
using System.Drawing;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        public static Dictionary<Segment, Color> SegmentColours = new Dictionary<Segment, Color>();
        public static void SetColor(this Segment segment, Color colour)
        {
            SegmentColours[segment] = colour;
            if (colour == null) colour = Color.Black;
        }

        public static Color GetColor(this Segment segment)
        {
            if (!SegmentColours.ContainsKey(segment)) return Color.Black;
            else return SegmentColours[segment];
        }
    }
}