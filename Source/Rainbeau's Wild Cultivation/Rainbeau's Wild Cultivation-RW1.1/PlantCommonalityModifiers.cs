using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using System.Collections;

namespace RWC_Code
{
	public class PlantCommonalityModifiers : IDictionary<PlantCommonalityRecord, float>
	{
		private readonly Dictionary<PlantCommonalityRecord, float> dict;

		public float GetModifier(BiomeDef biome, ThingDef plant)
		{
			Guard.OnArgNull(biome, nameof(biome));
			Guard.OnArgNull(plant, nameof(plant));

			var record = new PlantCommonalityRecord(biome, plant);
			if (!dict.ContainsKey(record))
			{
				throw new InvalidOperationException($"Dictionary does not have record for biome {biome.ToString()}, plant {plant.ToString()}.");
			}
			return dict[record];
		}

		public PlantCommonalityModifiers()
		{
			this.dict = new Dictionary<PlantCommonalityRecord, float>();
		}

		#region Implementation

		public float this[PlantCommonalityRecord key] { get => dict[key]; set => dict[key] = value; }

		public ICollection<PlantCommonalityRecord> Keys => ((IDictionary<PlantCommonalityRecord, float>)dict).Keys;

		public ICollection<float> Values => ((IDictionary<PlantCommonalityRecord, float>)dict).Values;

		public int Count => dict.Count;

		public bool IsReadOnly => ((IDictionary<PlantCommonalityRecord, float>)dict).IsReadOnly;

		public void Add(PlantCommonalityRecord key, float value)
		{
			dict.Add(key, value);
		}

		public void Add(KeyValuePair<PlantCommonalityRecord, float> item)
		{
			((IDictionary<PlantCommonalityRecord, float>)dict).Add(item);
		}

		public void Clear()
		{
			dict.Clear();
		}

		public bool Contains(KeyValuePair<PlantCommonalityRecord, float> item)
		{
			return ((IDictionary<PlantCommonalityRecord, float>)dict).Contains(item);
		}

		public bool ContainsKey(PlantCommonalityRecord key)
		{
			return dict.ContainsKey(key);
		}

		public void CopyTo(KeyValuePair<PlantCommonalityRecord, float>[] array, int arrayIndex)
		{
			((IDictionary<PlantCommonalityRecord, float>)dict).CopyTo(array, arrayIndex);
		}

		public IEnumerator<KeyValuePair<PlantCommonalityRecord, float>> GetEnumerator()
		{
			return ((IDictionary<PlantCommonalityRecord, float>)dict).GetEnumerator();
		}

		public bool Remove(PlantCommonalityRecord key)
		{
			return dict.Remove(key);
		}

		public bool Remove(KeyValuePair<PlantCommonalityRecord, float> item)
		{
			return ((IDictionary<PlantCommonalityRecord, float>)dict).Remove(item);
		}

		public bool TryGetValue(PlantCommonalityRecord key, out float value)
		{
			return dict.TryGetValue(key, out value);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IDictionary<PlantCommonalityRecord, float>)dict).GetEnumerator();
		}

		#endregion
	}
}
