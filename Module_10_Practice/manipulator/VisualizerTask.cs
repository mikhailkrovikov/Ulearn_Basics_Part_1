using System;
using System.Globalization;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media;

namespace Manipulation;

public static class VisualizerTask
{
    public static double X = 220;
    public static double Y = -100;
    public static double Alpha = 0.05;
    public static double Wrist = 2 * Math.PI / 3;
    public static double Elbow = 3 * Math.PI / 4;
    public static double Shoulder = Math.PI / 2;

    public static Brush UnreachableAreaBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
    public static Brush ReachableAreaBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
    public static Pen ManipulatorPen = new Pen(Brushes.Black, 4);
    public static Brush JointBrush = new SolidColorBrush(Colors.Gray);

    private static double step = Math.PI / 360;
    public static void KeyDown(Visual visual, KeyEventArgs key)
    {
        switch (key.Key)
        {
            case Key.Q:
                Shoulder += step;
                CalculateWrist();
                break;
            case Key.A:
                Shoulder -= step;
                CalculateWrist();
                break;
            case Key.W:
                Elbow += step;
                CalculateWrist();
                break;
            case Key.S:
                Elbow -= step;
                CalculateWrist();
                break;
            default: break;
        }
        visual.InvalidateVisual();
    }

    private static void CalculateWrist()
    {
        Wrist = -Alpha - Shoulder - Elbow;
    }

    public static void MouseMove(Visual visual, PointerEventArgs e)
    {
        var point = e.GetPosition(visual);
        X = ConvertWindowToMath(point, GetShoulderPos(visual)).X;
        Y = ConvertWindowToMath(point, GetShoulderPos(visual)).Y;

        UpdateManipulator();
        visual.InvalidateVisual();
    }

    public static void MouseWheel(Visual visual, PointerWheelEventArgs e)
    {
        Alpha += e.Delta.Y / (2 * Math.PI);
        UpdateManipulator();
        visual.InvalidateVisual();
    }

    public static void UpdateManipulator()
    {
        var m = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);
        if (!double.IsNaN(m[0]) && !double.IsNaN(m[1]) && !double.IsNaN(m[2]))
        {
            Shoulder = m[0];
            Elbow = m[1];
            Wrist = m[2];
        }
    }

    public static void DrawManipulator(DrawingContext context, Point shoulderPos)
    {
        var joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);

        DrawReachableZone(context, ReachableAreaBrush, UnreachableAreaBrush, shoulderPos, joints);

        var formattedText = new FormattedText(
            $"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}",
            CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            Typeface.Default,
            18,
            Brushes.Black
        )
        {
            TextAlignment = TextAlignment.Center
        };
        context.DrawText(formattedText, new Point(10, 10));

        Point[] points = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);

        context.DrawLine(ManipulatorPen,
            ConvertMathToWindow(new Point(0, 0), shoulderPos),
            ConvertMathToWindow(points[0], shoulderPos));

        context.DrawLine(ManipulatorPen,
            ConvertMathToWindow(points[0], shoulderPos),
            ConvertMathToWindow(points[1], shoulderPos));

        context.DrawLine(ManipulatorPen,
            ConvertMathToWindow(points[1], shoulderPos),
            ConvertMathToWindow(points[2], shoulderPos));

        context.DrawEllipse(JointBrush, null, ConvertMathToWindow(new Point(0, 0), shoulderPos), 10, 10);
        context.DrawEllipse(JointBrush, null, ConvertMathToWindow(points[0], shoulderPos), 10, 10);
        context.DrawEllipse(JointBrush, null, ConvertMathToWindow(points[1], shoulderPos), 10, 10);
        context.DrawEllipse(JointBrush, null, ConvertMathToWindow(points[2], shoulderPos), 10, 10);
    }

    private static void DrawReachableZone(
        DrawingContext context,
        Brush reachableBrush,
        Brush unreachableBrush,
        Point shoulderPos,
        Point[] joints)
    {
        var rmin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
        var rmax = Manipulator.UpperArm + Manipulator.Forearm;
        var mathCenter = new Point(joints[2].X - joints[1].X, joints[2].Y - joints[1].Y);
        //var windowCenter = ConvertMathToWindow(mathCenter, shoulderPos);
        context.DrawEllipse(reachableBrush,
            null,
            new Point(shoulderPos.X, shoulderPos.Y),
            rmax, rmax);
        context.DrawEllipse(unreachableBrush,
            null,
            new Point(shoulderPos.X, shoulderPos.Y),
            rmin, rmin);
    }

    public static Point GetShoulderPos(Visual visual)
    {
        return new Point(visual.Bounds.Width / 2, visual.Bounds.Height / 2);
    }

    public static Point ConvertMathToWindow(Point mathPoint, Point shoulderPos)
    {
        return new Point(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
    }

    public static Point ConvertWindowToMath(Point windowPoint, Point shoulderPos)
    {
        return new Point(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
    }
}