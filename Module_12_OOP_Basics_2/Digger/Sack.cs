using Digger.Architecture;

namespace Digger
{
    public class Sack : ICreature
    {
        int fallCounter = 0;
        public CreatureCommand Act(int x, int y)
        {
            if (y + 1 < Game.MapHeight)
            {
                ICreature element = Game.Map[x, y + 1];
                if (element == null || (fallCounter > 0 && (element is Player)))
                {
                    fallCounter++;
                    return new CreatureCommand() { DeltaY = 1 };
                }
            }

            if (fallCounter > 1)
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            fallCounter = 0;
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority() { return 4; }

        public string GetImageFileName() { return "Sack.png"; }
    }
}
