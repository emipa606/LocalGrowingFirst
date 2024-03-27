using Verse;

namespace LocalGrowingFirst;

public static class Utilities
{
    public static LinkDirections EdgeFacingRotation(Rot4 rot)
    {
        if (rot == Rot4.North)
        {
            return LinkDirections.Up;
        }

        if (rot == Rot4.East)
        {
            return LinkDirections.Right;
        }

        if (rot == Rot4.South)
        {
            return LinkDirections.Down;
        }

        return rot == Rot4.West ? LinkDirections.Left : LinkDirections.None;
    }
}