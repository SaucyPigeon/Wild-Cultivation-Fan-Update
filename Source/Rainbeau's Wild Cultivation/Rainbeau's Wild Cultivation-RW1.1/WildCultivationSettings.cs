using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using UnityEngine;

namespace RWC_Code
{
	public class WildCultivationSettings : ModSettings
	{
		public PlantCommonalityModifiers commonalityModifiers = new PlantCommonalityModifiers();

		const float Margin = 10f;
		private float viewHeight;

		public void DoWindowContents(Rect canvas)
		{
			Debug.Out("DoWindowContents start");
			if (!commonalityModifiers.Any())
			{
				commonalityModifiers.Add(new PlantCommonalityRecord(BiomeDefOf.AridShrubland, ThingDefOf.Plant_Potato), 2.0f);
				commonalityModifiers.Add(new PlantCommonalityRecord(BiomeDefOf.AridShrubland, ThingDefOf.Plant_Devilstrand), 0.5f);
				commonalityModifiers.Add(new PlantCommonalityRecord(BiomeDefOf.TropicalRainforest, ThingDefOf.Plant_Devilstrand), 0f);
				Debug.Out("commonalityModifiers added");
			}

			var rect = canvas.ContractedBy(Margin);

			var rect2 = new Rect(0, 0, rect.width - 16f, this.viewHeight);

			using (new WidgetScrollView(rect, rect2, true))
			{
				Debug.Out("widgetscrollview started");

				var rect5 = rect2;
				rect5.width -= 16f;
				rect5.height = 9999f;

				var list = new Listing_Standard()
				{
					ColumnWidth = rect5.width,
					maxOneColumn = true,
					verticalSpacing = 6f
				};
				list.Begin(rect5);
				Debug.Out("list started");

				Debug.Out("Iterating records");
				foreach (var record in commonalityModifiers)
				{
					var recordRect = list.GetRect(32f);
					AddRecordRow(recordRect, list, record);
				}
				Debug.Out("iteration finished");

				if (Event.current.type == EventType.Layout)
				{
					this.viewHeight = list.CurHeight;
				}
				list.End();
				Debug.Out("list ended");

			}
			Debug.Out("DoWindowContents end");

		}

		private static void AddRecordRow(Rect rect, Listing_Standard list, KeyValuePair<PlantCommonalityRecord, float> record)
		{
			Guard.OnArgNull(list, nameof(list));
			Guard.OnArgNull(record, nameof(record));

			// ------------------------------------------------
			// - plant / icon - biome - number enter / slider -
			// ------------------------------------------------

			float singleX = rect.x;
			float singleY = rect.y;

			// Plant
			{
				const string label = "WildCultivation.PlantNoun";
				WildCultivationWidgets.LabelFitted(label.Translate(), singleX, singleY, out Vector2 textSize, height: 30f);

				singleX += textSize.x + 6f;
				float buttonWidth = 100f;

				var rect8 = new Rect(singleX, singleY, buttonWidth, 30f);

				WildCultivationWidgets.ButtonFloatMenu<ThingDef>(rect8, record.Key.plant.label,
					action: (ThingDef def) =>
					{
						record.Key.plant = def;
					},
					predicate: (ThingDef def) => { return def.plant != null; }
				);

				singleX += buttonWidth + 6f;
			}

			// Biome
			{

				const string label = "WildCultivation.Biome";
				WildCultivationWidgets.LabelFitted(label.Translate(), singleX, singleY, out Vector2 textSize, height: 30f);

				singleX += textSize.x + 6f;
				float buttonWidth = 100f;

				var rect8 = new Rect(singleX, singleY, buttonWidth, 30f);

				WildCultivationWidgets.ButtonFloatMenu<BiomeDef>(rect8, record.Key.biome.label, action: (BiomeDef biome) =>
				{
					record.Key.biome = biome;
				});

				singleX += buttonWidth + 6f;
			}

			// Number input
			{
				const string label = "WildCultivation.Modifier";
				WildCultivationWidgets.LabelFitted(label.Translate(), singleX, singleY, out Vector2 textSize, height: 30f);
				singleX += textSize.x + 6f;
				var rect8 = new Rect(singleX, singleY, 100f, 30f);
				string buffer = "Foo";
				Widgets.TextFieldNumeric(rect8, ref record.Value, ref buffer);
			}

		}

		public override void ExposeData()
		{
			base.ExposeData();

			// More here
		}
	}
}
