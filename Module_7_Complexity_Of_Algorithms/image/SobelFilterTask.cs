using System;

namespace Recognizer;
internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        var width = g.GetLength(0);
        var height = g.GetLength(1);
        var sxWidth = sx.GetLength(0);
        var sxHeight = sx.GetLength(1);
        var halfX = sxWidth / 2;
        var halfY = sxHeight / 2;
        var result = new double[width, height];

        for (int i = halfX; i < width - halfX; i++)
            for (int j = halfY; j < height - halfY; j++)
            {
                var gx = 0.0;
                var gy = 0.0;

                for (int k = 0; k < sxWidth; k++)
                    for (int l = 0; l < sxHeight; l++)
                    {
                        var p = g[i + k - halfX, j + l - halfY];
                        gx += p * sx[k, l];
                        gy += p * sx[l, k];
                    }
                result[i, j] = Math.Sqrt(gx * gx + gy * gy);
            }
        return result;
    }
}
