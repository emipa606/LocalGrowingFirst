using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;

namespace LocalGrowingFirst
{
    [StaticConstructorOnStartup]
	static public class GrowingDefWrapper
    {
		static GrowingDefWrapper()
		{
			SetupWrappedDefs();
			WorkTypeDefOf.Growing.workGiversByPriority.Clear();
			WorkTypeDefOf.Growing.ResolveReferences();
		}   
    
		static public void SetupWrappedDefs()
		{
			var growingWGs = DefDatabase<WorkGiverDef>.AllDefs.Where(wg => wg.workType == WorkTypeDefOf.Growing
                                        && typeof(WorkGiver_Scanner).IsAssignableFrom(wg.giverClass)).ToList();
			int maxWorkerGiverPriority = growingWGs.Max(wg => wg.priorityInType);

			DefDatabase<WorkGiverDef>.Add(growingWGs.Select(wg => CreateLocalWorkGiverDef(wg, maxWorkerGiverPriority + 1)));
		}

        static WorkGiverDef CreateLocalWorkGiverDef(WorkGiverDef def, int priorityAdj)
        {
        	return new WorkGiverDef() {
        		giverClass = typeof(WorkGiver_LocalZoneWrapper),
                defName = "LGF_Local" + def.defName,
        		label = "LGF_Local".Translate().CapitalizeFirst() + " " + def.label,
        		workType = def.workType,
        		priorityInType = def.priorityInType + priorityAdj,
        		verb = def.verb,
        		gerund = def.gerund,
        		emergency = def.emergency,
        		requiredCapacities = new List<PawnCapacityDef>(def.requiredCapacities),
        		directOrderable = def.directOrderable,
                scanCells = def.scanCells,
                scanThings = def.scanThings,
                tagToGive = def.tagToGive,
                modExtensions = new List<DefModExtension>(1) { new DefModExt_LocalZoneWrapping() {
        											defToWrap = def } }
        	};
        }
    }
}
