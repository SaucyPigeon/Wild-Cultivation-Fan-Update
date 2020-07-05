﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using HarmonyLib;
using System.Reflection.Emit;

namespace RWC_Code.Patches
{
	[HarmonyPatch(typeof(BiomeDef))]
	[HarmonyPatch("CachePlantCommonalitiesIfShould")]
	public class BiomeDef_CachePlantCommalitiesIfShould
	{
		// Whenever BiomePlantRecord::commonality is accessed, multiply by corresponding modifier.
		// Whenever PlantBiomeRecord::commonality is accessed, multiply by corresponding modifier
		// this.wildPlants[i].commonality -> GetModifiedCommonality(this.wildPlants[i], this)
		// this.wildBiomes[j].commonality -> GetModifiedCommonality(current.plant.wildBiomes[j], current)
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			var fi_BiomePlantRecord_commonality = AccessTools.Field(typeof(BiomePlantRecord), nameof(BiomePlantRecord.commonality));
			var fi_PlantBiomeRecord_commonality = AccessTools.Field(typeof(PlantBiomeRecord), nameof(BiomePlantRecord.commonality));

			// static float(BiomePlantRecord, BiomeDef)
			var mi_GetModifiedCommonality_bpr = AccessTools.Method(typeof(BiomeDef_CachePlantCommalitiesIfShould), nameof(GetModifiedCommonality_bpr));
			// static float(PlantBiomeRecord, ThingDef)
			var mi_GetModifiedCommonality_pbr = AccessTools.Method(typeof(BiomeDef_CachePlantCommalitiesIfShould), nameof(GetModifiedCommonality_pbr));


			foreach (var instruction in instructions)
			{
				if (instruction.LoadsField(fi_BiomePlantRecord_commonality))
				{
					yield return new CodeInstruction(OpCodes.Ldarg_0);
					yield return new CodeInstruction(OpCodes.Call, mi_GetModifiedCommonality_bpr);
				}
				else if (instruction.LoadsField(fi_PlantBiomeRecord_commonality))
				{
					yield return new CodeInstruction(OpCodes.Ldloc_2);
					yield return new CodeInstruction(OpCodes.Call, mi_GetModifiedCommonality_pbr);
				}
				else
				{
					yield return instruction;
				}
			}
		}

		private static float GetModifiedCommonality_bpr(BiomePlantRecord record, BiomeDef biome)
		{
			Guard.OnArgNull(record, nameof(record));
			Guard.OnArgNull(biome, nameof(biome));

			float modifier = WildCultivationMod.Settings.commonalityModifiers.GetModifier(biome, record.plant);
			return record.commonality * modifier;
		}

		private static float GetModifiedCommonality_pbr(PlantBiomeRecord record, ThingDef plant)
		{
			Guard.OnArgNull(plant, nameof(plant));
			Guard.OnArgNull(record, nameof(record));

			float modifier = WildCultivationMod.Settings.commonalityModifiers.GetModifier(record.biome, plant);
			return record.commonality * modifier;
		}
	}
}
