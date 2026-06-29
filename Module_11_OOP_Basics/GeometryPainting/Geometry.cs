using System;

namespace GeometryPainting
{
    public static class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            var vector3 = new Vector();
            vector3.X = vector1.X + vector2.X;
            vector3.Y = vector1.Y + vector2.Y;
            return vector3;
        }

        public static double GetLength(Segment segment)
        {
            return Math.Sqrt(Math.Pow(segment.Begin.X - segment.End.X, 2) + Math.Pow(segment.Begin.Y - segment.End.Y, 2));
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            double segmentLegth = Math.Sqrt(Math.Pow(segment.End.X - segment.Begin.X, 2) + Math.Pow(segment.End.Y - segment.Begin.Y, 2));
            double vectorBegLength = Math.Sqrt(Math.Pow(vector.X - segment.Begin.X, 2) + Math.Pow(vector.Y - segment.Begin.Y, 2));
            double vectorEndLength = Math.Sqrt(Math.Pow(vector.X - segment.End.X, 2) + Math.Pow(vector.Y - segment.End.Y, 2));
            return vectorBegLength + vectorEndLength == segmentLegth;
        }
    }
}
