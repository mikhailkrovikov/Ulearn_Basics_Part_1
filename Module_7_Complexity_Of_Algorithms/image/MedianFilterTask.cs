using System.Collections.Generic;
namespace Recognizer;

internal static class MedianFilterTask
{
    private static int _length;
    private static int _height;
    public static double[,] MedianFilter(double[,] original)
    {
        _length = original.GetLength(0);
        _height = original.GetLength(1);
        var array = new double[_length, _height];
        for (int i = 0; i <= _length - 1; i++)
            for (int j = 0; j <= _height - 1; j++)
                array[i, j] = GetMedian(i, j, original);
        return array;
    }

    public static double GetMedian(int i, int j, double[,] array)
    {
        var list = new List<double>();
        for (int x = i + 1; x >= i - 1; x--)
            for (int y = j + 1; y >= j - 1; y--)
                if (x >= 0 && x < _length && y >= 0 && y < _height)
                    list.Add(array[x, y]);
        list.Sort();
        return (list[(list.Count - 1) / 2] + list[(list.Count) / 2]) / 2;
    }
}