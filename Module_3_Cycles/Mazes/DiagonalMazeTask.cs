namespace Mazes
{
    public static class DiagonalMazeTask
    {
        static void MoveDown(Robot robot, int stepLengHeight)
        {
            for (int i = 0; i < stepLengHeight; i++)
            {
                robot.MoveTo(Direction.Down);
            }
        }

        static void MoveRight(Robot robot, int stepLengWidth)
        {
            for (int i = 0; i < stepLengWidth; i++)
            {
                robot.MoveTo(Direction.Right);
            }
        }

        public static void MoveOut(Robot robot, int width, int height)
        {
            int smallestWall;
            if (width < height) smallestWall = width;
            else smallestWall = height;
            int stepCount = smallestWall - 3;
            int stepLengWidth = (width - 3) / stepCount;
            int stepLengHeight = (height - 3) / stepCount;
            if (width < height) MoveDown(robot, stepLengHeight);
            for (int i = 0; i < stepCount; i++)
            {
                MoveRight(robot, stepLengWidth);
                MoveDown(robot, stepLengHeight);
            }
            if (width > height) MoveRight(robot, stepLengWidth); ;
        }
    }
}