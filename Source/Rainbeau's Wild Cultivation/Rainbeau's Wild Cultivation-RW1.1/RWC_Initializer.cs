using HarmonyLib;
using RimWorld;
using Verse;
using System.Reflection;

namespace RWC_Code
{
	[StaticConstructorOnStartup]
	internal static class RWC_Initializer
	{
		static RWC_Initializer()
		{
			const string Id = "net.rainbeau.rimworld.mod.wildcultivation";
			var harmony = new Harmony(Id);

			harmony.PatchAll(Assembly.GetExecutingAssembly());

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
