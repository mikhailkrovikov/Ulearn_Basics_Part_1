using System.Collections.Generic;
using System.Linq;

namespace Recognizer;

public static class ThresholdFilterTask
{
    public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
    {
        var length = original.GetLength(0);
        var height = original.GetLength(1);
        var t = (int)(original.Length * whitePixelsFraction);
        var list = new List<double>();
        for (int i = 0; i <= length - 1; i++)
            for (int j = 0; j <= height - 1; j++)        
                list.Add(original[i, j]);
              
        list = list.OrderByDescending(x => x).Take(t).ToList();
        for(int i = 0; i <= length - 1; i++)
            for (int j = 0; j <= height - 1; j++)            
                if (list.Contains(original[i, j]))            
                    original[i, j] = 1;        
                else               
                    original[i, j] = 0;                 
        return original;
    }
}
