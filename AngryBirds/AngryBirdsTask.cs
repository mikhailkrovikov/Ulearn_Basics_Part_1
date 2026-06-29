using System;

namespace AngryBirds
{
    public static class AngryBirdsTask
    {
        static double g = 9.8;
        public static double FindSightAngle(double v, double distance)
        {
            return Math.Asin((distance * g) / (v * v)) / 2;
        }
    }
}