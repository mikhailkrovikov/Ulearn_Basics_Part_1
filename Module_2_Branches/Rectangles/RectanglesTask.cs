using System;
namespace Rectangles
{
    public static class RectanglesTask
    {
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            return ((r2.Left <= r1.Left && r1.Left <= r2.Right) ||
                 (r1.Left <= r2.Left && r2.Left <= r1.Right)) &&
                ((r1.Top <= r2.Top && r2.Top <= r1.Bottom) ||
                 (r2.Top <= r1.Top && r1.Top <= r2.Bottom));
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            if (AreIntersected(r1, r2))
            {
                int lPoint = Math.Max(r1.Left, r2.Left);
                int rPoint = Math.Min(r2.Right, r1.Right);
                int tPoint = Math.Max(r1.Top, r2.Top);
                int bPoint = Math.Min(r1.Bottom, r2.Bottom);
                if (IndexOfInnerRectangle(r1, r2) == 0) return r1.Height * r1.Width;
                if (IndexOfInnerRectangle(r1, r2) == 1) return r2.Height * r2.Width;
                else return (rPoint - lPoint) * (bPoint - tPoint);
            }
            else return 0;
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if (((r1.Left <= r2.Left && r2.Left <= r1.Right) &&
                 (r1.Left <= r2.Right && r2.Right <= r1.Right)) &&
                ((r1.Top <= r2.Top && r2.Top <= r1.Bottom) &&
                 (r1.Top <= r2.Bottom && r2.Bottom <= r1.Bottom)))
                return 1;
            else if (((r2.Left <= r1.Left && r1.Left <= r2.Right) &&
                      (r2.Left <= r1.Right && r1.Right <= r2.Right)) &&
                     ((r2.Top <= r1.Top && r1.Top <= r2.Bottom) &&
                      (r2.Top <= r1.Bottom && r1.Bottom <= r2.Bottom)))
                return 0;
            else return -1;
        }
    }
}