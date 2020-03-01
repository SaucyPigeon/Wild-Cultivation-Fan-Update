using System;
using System.Linq;

namespace RWC_Code
{
	public static class Designator_ZoneAdd_Growing_RWC
	{
		public static bool CanDesignateCellPrefix(ref float __state)
		{
			__state = ThingDefOf.Plant_Potato.plant.fertilityMin;
			ThingDefOf.Plant_Potato.plant.fertilityMin = 0.01f;
			return true;
		}

		public static void CanDesignateCellPostfix(ref float __state)
		{
			ThingDefOf.Plant_Potato.plant.fertilityMin = __state;
		}
	}
}
