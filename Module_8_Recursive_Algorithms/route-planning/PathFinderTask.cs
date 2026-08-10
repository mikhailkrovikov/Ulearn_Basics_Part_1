using System;
using System.Drawing;

namespace RoutePlanning;

public static class PathFinderTask
{
    public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
    {
        var shortest = double.PositiveInfinity;
        var order = new int[checkpoints.Length];
        var bestOrder = new int[checkpoints.Length];
        MakePermutations(order, 1, checkpoints, ref shortest, ref bestOrder);
        return bestOrder;
    }

    private static int[] MakePermutations(int[] order,
        int position,
        Point[] checkpoints,
        ref double shortest,
        ref int[] bestOrder)
    {
        var currentOrder = new int[position];
        Array.Copy(order, currentOrder, position);
        var pathLength = PointExtensions.GetPathLength(checkpoints, currentOrder);
        if (pathLength < shortest)
        {
            if (position == order.Length)
            {
                shortest = pathLength;
                bestOrder = (int[])order.Clone();
                return order;
            }

            for (int i = 1; i < order.Length; i++)
            {
                var index = Array.IndexOf(order, i, 0, position);
                if (index != -1)
                    continue;
                order[position] = i;
                MakePermutations(order, position + 1, checkpoints, ref shortest, ref bestOrder);
            }
        }
        return order;
    }
}