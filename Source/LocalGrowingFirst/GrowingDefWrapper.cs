using System.Linq;
using RimWorld;
using Verse;

namespace LocalGrowingFirst;

[StaticConstructorOnStartup]
public static class GrowingDefWrapper
{
    static GrowingDefWrapper()
    {
        setupWrappedDefs();
        WorkTypeDefOf.Growing.workGiversByPriority.Clear();
        WorkTypeDefOf.Growing.ResolveReferences();
    }

    private static void setupWrappedDefs()
    {
        var growingWGs = DefDatabase<WorkGiverDef>.AllDefs.Where(wg => wg.workType == WorkTypeDefOf.Growing
                                                                       && typeof(WorkGiver_Scanner)
                                                                           .IsAssignableFrom(wg.giverClass))
            .ToList();
        var maxWorkerGiverPriority = growingWGs.Max(wg => wg.priorityInType);

        DefDatabase<WorkGiverDef>.Add(growingWGs.Select(wg =>
            createLocalWorkGiverDef(wg, maxWorkerGiverPriority + 1)));
    }

    private static WorkGiverDef createLocalWorkGiverDef(WorkGiverDef def, int priorityAdj)
    {
        return new WorkGiverDef
        {
            giverClass = typeof(WorkGiver_LocalZoneWrapper),
            defName = $"LGF_Local{def.defName}",
            label = "LGF_Local".Translate().CapitalizeFirst() + " " + def.label,
            workType = def.workType,
            priorityInType = def.priorityInType + priorityAdj,
            verb = def.verb,
            gerund = def.gerund,
            emergency = def.emergency,
            requiredCapacities = [..def.requiredCapacities],
            directOrderable = def.directOrderable,
            scanCells = def.scanCells,
            scanThings = def.scanThings,
            tagToGive = def.tagToGive,
            modExtensions =
            [
                new DefModExt_LocalZoneWrapping
                {
                    defToWrap = def
                }
            ]
        };
    }
}