namespace Mazes
{
    public static class EmptyMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            MoveRight(robot, width);
            MoveDown(robot, height);
        }

        public static void MoveRight(Robot robot, int width)
        {
            while (robot.X < width - 2)
            {
                robot.MoveTo(Direction.Right);
            }
        }

        public static void MoveDown(Robot robot, int hight)
        {
            while (robot.Y < hight - 2)
            {
                robot.MoveTo(Direction.Down);
            }
        }
    }
}