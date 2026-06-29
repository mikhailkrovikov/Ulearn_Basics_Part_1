using Digger.Architecture;

namespace Digger
{
    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if ((conflictedObject is Player && conflictedObject is Terrain) || conflictedObject is Player)
            {
                Game.Scores += 10;
                return true;
            }

            return !(conflictedObject is Terrain);
        }

        public int GetDrawingPriority() { return 3; }

        public string GetImageFileName() { return "Gold.png"; }
    }
}
