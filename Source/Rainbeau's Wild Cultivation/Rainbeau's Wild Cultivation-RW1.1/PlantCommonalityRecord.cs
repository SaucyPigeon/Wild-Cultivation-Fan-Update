using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace RWC_Code
{
	public class PlantCommonalityRecord : IEquatable<PlantCommonalityRecord>
	{
		public ThingDef plant;
		public BiomeDef biome;

		public PlantCommonalityRecord(BiomeDef biome, ThingDef plant)
		{
			Guard.OnArgNull(biome, nameof(biome));
			Guard.OnArgNull(plant, nameof(plant));

			this.biome = biome;
			this.plant = plant;
		}

		#region Implementation

		public override bool Equals(object obj)
		{
			return Equals(obj as PlantCommonalityRecord);
		}

		public bool Equals(PlantCommonalityRecord other)
		{
			return other != null &&
				   EqualityComparer<ThingDef>.Default.Equals(plant, other.plant) &&
				   EqualityComparer<BiomeDef>.Default.Equals(biome, other.biome);
		}

		public override int GetHashCode()
		{
			var hashCode = 1356709157;
			hashCode = hashCode * -1521134295 + EqualityComparer<ThingDef>.Default.GetHashCode(plant);
			hashCode = hashCode * -1521134295 + EqualityComparer<BiomeDef>.Default.GetHashCode(biome);
			return hashCode;
		}

		public static bool operator==(PlantCommonalityRecord x, PlantCommonalityRecord y)
		{
			return x.plant == y.plant && x.biome == y.biome;
		}

		public static bool operator!=(PlantCommonalityRecord x, PlantCommonalityRecord y)
		{
			return !(x == y);
		}

		#endregion
	}
}
