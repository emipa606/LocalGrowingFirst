using System;
using Verse;
using RimWorld;

namespace LocalGrowingFirst
{
	public static class Utilities
	{
		public static LinkDirections EdgeFacingRotation(Rot4 rot)
		{
			if (rot == Rot4.North)
				return LinkDirections.Up;
			if (rot == Rot4.East)
				return LinkDirections.Right;
			if (rot == Rot4.South)
				return LinkDirections.Down;
			if (rot == Rot4.West)
				return LinkDirections.Left;
			return LinkDirections.None;
		}
	}
}

