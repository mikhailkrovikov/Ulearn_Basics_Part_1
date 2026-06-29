using System;
using Avalonia;
using NUnit.Framework;
using static Manipulation.Manipulator;
namespace Manipulation;

public static class AnglesToCoordinatesTask
{
    /// <summary>
    /// По значению углов суставов возвращает массив координат суставов
    /// в порядке new []{elbow, wrist, palmEnd}
    /// </summary>
    public static Point[] GetJointPositions(double shoulder, double elbow, double wrist)
    {
        var elbowPos = new Point(
            UpperArm * Math.Cos(shoulder),
            UpperArm * Math.Sin(shoulder));

        var wristPos = new Point(
            elbowPos.X + Forearm * Math.Cos(shoulder + Math.PI + elbow),
            elbowPos.Y + Forearm * Math.Sin(shoulder + Math.PI + elbow));

        var palmEndPos = new Point(
            wristPos.X + Palm * Math.Cos(shoulder + 2 * Math.PI + elbow + wrist),
            wristPos.Y + Palm * Math.Sin(shoulder + 2 * Math.PI + elbow + wrist));

        return new[]
        {
            elbowPos,
            wristPos,
            palmEndPos
        };
    }
}

[TestFixture]
public class AnglesToCoordinatesTask_Tests
{
    [TestCase(Math.PI / 2, Math.PI, Math.PI, 0, UpperArm + Forearm + Palm)]
    [TestCase(0, Math.PI, Math.PI, UpperArm + Forearm + Palm, 0)]
    [TestCase(Math.PI / 2, Math.PI, Math.PI, 0, UpperArm + Forearm + Palm)]
    [TestCase(0, Math.PI, Math.PI, UpperArm + Forearm + Palm, 0)]
    public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
    {
        var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
        NUnit.Framework.Legacy.ClassicAssert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
        NUnit.Framework.Legacy.ClassicAssert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
        NUnit.Framework.Legacy.ClassicAssert.AreEqual(GetLengthByCoordinates(joints[1], joints[0]),
Forearm, 1, "Forearm length");
        NUnit.Framework.Legacy.ClassicAssert.AreEqual(GetLengthByCoordinates(joints[2], joints[1]),
Palm, 1, "Palm length");
        NUnit.Framework.Legacy.ClassicAssert.AreEqual(GetLengthByCoordinates(joints[0], new Point(0, 0)),
UpperArm, 1, "UpperArm length");
    }

    public double GetLengthByCoordinates(Point p2, Point p1)
    {
        return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
    }
}