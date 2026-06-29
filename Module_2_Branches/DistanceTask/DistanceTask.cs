using System;
namespace DistanceTask
{
    public static class DistanceTask
    {
        // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            double ab = Math.Sqrt((ax - bx) * (ax - bx) + (ay - by) * (ay - by));
            double am = Math.Sqrt((ax - x) * (ax - x) + (ay - y) * (ay - y));
            double bm = Math.Sqrt((bx - x) * (bx - x) + (by - y) * (by - y));
            double p = (ab + am + bm) / 2;
            double S = Math.Sqrt(p * (p - ab) * (p - am) * (p - bm));
            double h = (2 * S) / ab;
            double cosMB = (am * am + ab * ab - bm * bm) / (2 * am * bm);
            double cosAM = (bm * bm + ab * ab - am * am) / (2 * bm * ab);
            if (cosMB > 0 && cosAM < 0) return bm;
            if (cosMB < 0 && cosAM > 0) return am;
            if (ab == 0) return am;
            else return h;
        }
    }
}