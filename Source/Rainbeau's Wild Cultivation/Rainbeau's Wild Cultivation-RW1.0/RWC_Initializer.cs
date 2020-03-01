using Harmony;
using RimWorld;
using Verse;

namespace RWC_Code
{
	[StaticConstructorOnStartup]
	internal static class RWC_Initializer
	{
		static RWC_Initializer()
		{
			HarmonyInstance harmony = HarmonyInstance.Create("net.rainbeau.rimworld.mod.wildcultivation");
			harmony.Patch(AccessTools.Method(typeof(Designator_ZoneAdd_Growing), "CanDesignateCell"), new HarmonyMethod(typeof(Designator_ZoneAdd_Growing_RWC), "CanDesignateCellPrefix"), null);
			harmony.Patch(AccessTools.Method(typeof(Designator_ZoneAdd_Growing), "CanDesignateCell"), null, new HarmonyMethod(typeof(Designator_ZoneAdd_Growing_RWC), "CanDesignateCellPostfix"));
			LongEventHandler.QueueLongEvent(Setup, "LibraryStartup", false, null);
		}

		public static void Setup()
		{
			ThingDef.Named("PlantWildCotton").plant.harvestedThingDef = ThingDefOf.Plant_Cotton.plant.harvestedThingDef;
			ThingDef.Named("PlantWildCotton").plant.harvestYield = ThingDefOf.Plant_Cotton.plant.harvestYield;

			ThingDef.Named("PlantWildDevilstrand").plant.harvestedThingDef = ThingDefOf.Plant_Devilstrand.plant.harvestedThingDef;
			ThingDef.Named("PlantWildDevilstrand").plant.harvestYield = ThingDefOf.Plant_Devilstrand.plant.harvestYield;
		}
	}
    
}
