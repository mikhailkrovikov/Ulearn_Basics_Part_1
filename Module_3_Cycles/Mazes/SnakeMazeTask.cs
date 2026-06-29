namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            MoveRight(robot, width);
            while (robot.Y != height - 2)
            {
                if (robot.X == width - 2)
                    DownAndLeft(robot, width);
                else
                    DownAndRight(robot, width);
            }
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
            robot.MoveTo(Direction.Down);
            robot.MoveTo(Direction.Down);
        }

        public static void MoveLeft(Robot robot, int width)
        {
            if (robot.X == width - 2)
                while (robot.X > 1)
                {
                    robot.MoveTo(Direction.Left);
                }
        }

        public static void DownAndLeft(Robot robot, int width)
        {
            robot.MoveTo(Direction.Down);
            robot.MoveTo(Direction.Down);
            MoveLeft(robot, width);
        }

        public static void DownAndRight(Robot robot, int width)
        {
            robot.MoveTo(Direction.Down);
            robot.MoveTo(Direction.Down);
            MoveRight(robot, width);
        }
    }
}