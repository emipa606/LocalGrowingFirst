using RimWorld;
using Verse;

namespace LocalGrowingFirst
{
    public class DefModExt_LocalZoneWrapping : DefModExtension
    {
        public WorkGiverDef defToWrap;
        public string Tag => "Dynamic"; //Advises this is a dynamic job and shouldn't be further manipulated
    }
}