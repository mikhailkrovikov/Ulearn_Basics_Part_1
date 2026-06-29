using Digger.Architecture;

namespace Digger
{
    public class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand();
            switch (Game.KeyPressed)
            {
                case Avalonia.Input.Key.Up:
                    command.DeltaY = -1;
                    break;
                case Avalonia.Input.Key.Down:
                    command.DeltaY = 1;
                    break;
                case Avalonia.Input.Key.Right:
                    command.DeltaX = 1;
                    break;
                case Avalonia.Input.Key.Left:
                    command.DeltaX = -1;
                    break;
                default:
                    break;
            }

            if (x + command.DeltaX >= 0 && x + command.DeltaX < Game.MapWidth &&
                y + command.DeltaY >= 0 && y + command.DeltaY < Game.MapHeight)
            {
                if (!(Game.Map[x + command.DeltaX, y + command.DeltaY] is Sack))
                    return command;
            }
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Sack)
            {
                Game.IsOver = true;
                return true;
            }

            return false;
        }

        public int GetDrawingPriority() { return 0; }

        public string GetImageFileName() { return "Digger.png"; }
    }
}
