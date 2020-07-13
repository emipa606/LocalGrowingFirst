using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Verse;
using RimWorld;
using Verse.AI;
using Verse.Noise;

namespace LocalGrowingFirst
{
    public class WorkGiver_LocalZoneWrapper : WorkGiver_Scanner
    {
		public WorkGiver_Scanner wrappedScanner;

		void CreateWrappedScanner()
		{
            Type scannerType = (def.modExtensions.First(ext => ext is DefModExt_LocalZoneWrapping)
                            as DefModExt_LocalZoneWrapping).defToWrap.giverClass;
            wrappedScanner = (WorkGiver_Scanner)Activator.CreateInstance(scannerType);
		}

		public WorkGiver_LocalZoneWrapper() { }                            

		public override ThingRequest PotentialWorkThingRequest => wrappedScanner.PotentialWorkThingRequest;

		public override int MaxRegionsToScanBeforeGlobalSearch => wrappedScanner.MaxRegionsToScanBeforeGlobalSearch;

		public override bool Prioritized => wrappedScanner.Prioritized;

		public override bool AllowUnreachable => wrappedScanner.AllowUnreachable;

		public override PathEndMode PathEndMode => wrappedScanner.PathEndMode;

		public override float GetPriority(Pawn pawn, TargetInfo t)
		{
			return wrappedScanner.GetPriority(pawn, t);
		}
        
        public override bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            return wrappedScanner.HasJobOnCell(pawn, c, forced);
        }

		public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			return wrappedScanner.HasJobOnThing(pawn, t, forced);
		}

		public override Job JobOnCell(Pawn pawn, IntVec3 cell, bool forced = false)
		{
			return wrappedScanner.JobOnCell(pawn, cell, forced);
		}

		public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			return wrappedScanner.JobOnThing(pawn, t, forced);
		}

		public override Danger MaxPathDanger(Pawn pawn)
		{
			return wrappedScanner.MaxPathDanger(pawn);
		}

		public override Job NonScanJob(Pawn pawn)
		{
			if(wrappedScanner == null)
				CreateWrappedScanner();
        
			return wrappedScanner.NonScanJob(pawn);
		}

		public override IEnumerable<IntVec3> PotentialWorkCellsGlobal(Pawn pawn)
		{
			if (wrappedScanner == null)
				CreateWrappedScanner();
			//Adapted from latter half of WorkGiver_Grower PotentialWorkCellsGlobal
			Danger maxDanger = pawn.NormalMaxDanger();
			FieldInfo wantedPlantDef = wrappedScanner.GetType().GetField("wantedPlantDef"
                                       , BindingFlags.NonPublic | BindingFlags.Public |  BindingFlags.Instance);
			wantedPlantDef?.SetValue(wrappedScanner, null);
            Zone_Growing growZone = pawn.Map.zoneManager.ZoneAt (pawn.Position) as Zone_Growing;

            if (growZone == null) { //Try edge cells in pawn facing direction next
                growZone = GenAdj.CellsAdjacentAlongEdge (pawn.Position, pawn.Rotation, new IntVec2 (1, 1), Utilities.EdgeFacingRotation (pawn.Rotation)).
                    Select (p => pawn.Map.zoneManager.ZoneAt (p)).OfType<Zone_Growing> ().FirstOrDefault ();

                if (growZone == default(Zone_Growing))
                    yield break;
            }

			if (growZone.cells.Count == 0)
            {
                Log.ErrorOnce("Grow zone has 0 cells: " + growZone, -563487);
            }
            else if (!growZone.ContainsStaticFire)
            {   //If there is an extraRequirement then check it, otherwise true
				MethodInfo extraRequirements = wrappedScanner.GetType().GetMethod("ExtraRequirements"
                                        , BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if((bool?)extraRequirements?.Invoke(wrappedScanner, new object[2] { growZone, pawn }) ?? true)                                        
                {
                    if (pawn.CanReach(growZone.Cells[0], PathEndMode.OnCell, maxDanger, false, TraverseMode.ByPawn))
                    {
						bool addCells = true;
						if(wrappedScanner.ToString().Contains("Sow"))
                        {
							ThingDef plantToSow = growZone.GetPlantDefToGrow();
							addCells = plantToSow.plant.sowMinSkill <= pawn.skills.GetSkill(SkillDefOf.Plants).Level;
							//Log.Message("Pawn: " + pawn.NameShortColored + " GetPlantDefToGrow: " + growZone.GetPlantDefToGrow() + " CanSow: " + addCells);
						}
						if (addCells)
						{
							for (int k = 0; k < growZone.cells.Count; k++)
							{
								yield return growZone.cells[k];
							}
						}
                        wantedPlantDef?.SetValue(wrappedScanner, null);
                    }
                }
            }
            wantedPlantDef?.SetValue(wrappedScanner, null);
		}

		public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
		{
			return wrappedScanner.PotentialWorkThingsGlobal(pawn);
		}

		public override bool ShouldSkip(Pawn pawn, bool forced = false)
		{
            if(wrappedScanner == null)
                CreateWrappedScanner();
			return wrappedScanner.ShouldSkip(pawn, forced);
		}
	}
}
