using Digger.Architecture;
using System;

namespace Digger
{
    class SpecialCommands
    {
        public static readonly CreatureCommand EmptyMove = new();
    }
    public class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var (isPlayerAlive, playerX, playerY) = FindPlayerIfAlive();
            if (!isPlayerAlive)
                return SpecialCommands.EmptyMove;
            var result = new CreatureCommand();
            if (playerX == x)
                result.DeltaY = Math.Sign(playerY - y);
            else
                result.DeltaX = Math.Sign(playerX - x);
            if (CanMove(x + result.DeltaX, y + result.DeltaY))
                return result;
            return SpecialCommands.EmptyMove;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return (conflictedObject is Sack) || (conflictedObject is Monster);
        }

        public int GetDrawingPriority()
        {
            return -1;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }

        private static (bool, int, int) FindPlayerIfAlive()
        {
            for (int i = 0; i < Game.MapWidth; i++)
                for (int j = 0; j < Game.MapHeight; j++)
                {
                    var cell = Game.Map[i, j];
                    if (cell is Player)
                        return (true, i, j);
                }
            return (false, 0, 0);
        }

        private static bool CanMove(int x, int y)
        {
            var destinationCell = Game.Map[x, y];
            return (destinationCell is null)
                || (destinationCell is Gold)
                || (destinationCell is Player);
        }
    }
}
