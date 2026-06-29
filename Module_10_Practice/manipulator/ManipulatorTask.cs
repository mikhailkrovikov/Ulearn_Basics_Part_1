using System;
using Avalonia;
using NUnit.Framework;
using static Manipulation.Manipulator;

namespace Manipulation;

public static class ManipulatorTask
{
    public static double[] MoveManipulatorTo(double x, double y, double alpha)
    {
        double wristX = x - Palm * Math.Cos(alpha);
        double wristY = y + Palm * Math.Sin(alpha);
        double shoulderToWrist = Math.Sqrt(wristX * wristX + wristY * wristY);

        double elbow = TriangleTask.GetABAngle(
            UpperArm,
            Forearm,
            shoulderToWrist);

        double shoulder = TriangleTask.GetABAngle(
            shoulderToWrist,
            UpperArm,
            Forearm) + Math.Atan2(wristY, wristX);

        double wrist = -alpha - shoulder - elbow;
        wrist %= Math.PI * 2;

        if (double.IsNaN(elbow) || double.IsNaN(shoulder) || double.IsNaN(wrist))
            return new[] { double.NaN, double.NaN, double.NaN };
        else return new[] { shoulder, elbow, wrist };
    }
}

[TestFixture]
public class ManipulatorTask_Tests
{
    [Test]
    public void TestMoveManipulatorTo() { }
}