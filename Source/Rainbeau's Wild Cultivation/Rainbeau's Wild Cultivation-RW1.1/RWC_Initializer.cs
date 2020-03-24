using HarmonyLib;
using RimWorld;
using Verse;

namespace RWC_Code
{
	[StaticConstructorOnStartup]
	internal static class RWC_Initializer
	{
		static RWC_Initializer()
		{
			const string Id = "net.rainbeau.rimworld.mod.wildcultivation";
			var harmony = new Harmony(Id);

			// Will update to require Harmony as external mod soon
			Log.Warning($"Wild Cultivation - Fan Update will be using Harmony as an external dependency when it next updates. To avoid wiping your mod list, please download and use Harmony now.");

			harmony.Patch(AccessTools.Method(typeof(Designator_ZoneAdd_Growing), "CanDesignateCell"), new HarmonyMethod(typeof(Designator_ZoneAdd_Growing_RWC), "CanDesignateCellPrefix"), null);
			harmony.Patch(AccessTools.Method(typeof(Designator_ZoneAdd_Growing), "CanDesignateCell"), null, new HarmonyMethod(typeof(Designator_ZoneAdd_Growing_RWC), "CanDesignateCellPostfix"));

			LongEventHandler.QueueLongEvent(Setup, "LibraryStartup", false, null);
		}

		public static void Setup()
		{
			// Update wild cotton harvest to standard cotton
			ThingDef.Named("PlantWildCotton").plant.harvestedThingDef = ThingDefOf.Plant_Cotton.plant.harvestedThingDef;
			ThingDef.Named("PlantWildCotton").plant.harvestYield = ThingDefOf.Plant_Cotton.plant.harvestYield;

			// Update wild devilstrand harvest to standard devilstrand
			ThingDef.Named("PlantWildDevilstrand").plant.harvestedThingDef = ThingDefOf.Plant_Devilstrand.plant.harvestedThingDef;
			ThingDef.Named("PlantWildDevilstrand").plant.harvestYield = ThingDefOf.Plant_Devilstrand.plant.harvestYield;
		}
	}
    
}
