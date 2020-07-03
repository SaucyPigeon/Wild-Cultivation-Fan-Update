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
		private Vector2 scrollPosition;
		private float viewHeight;

		public void DoWindowContents(Rect canvas)
		{
			var rect = canvas.ContractedBy(Margin);

			var rect2 = new Rect(0, 0, rect.width - 16f, this.viewHeight);

			Widgets.BeginScrollView(rect, ref scrollPosition, rect2, true);
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

			foreach (var record in commonalityModifiers)
			{
				var recordRect = list.GetRect(32f);
				AddRecordRow(recordRect, list, record);
			}
			
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


			var textSize = Text.CalcSize("WildCultivation.PlantNoun".Translate());

			Widgets.Label(new Rect(singleX, singleY, textSize.x, 30f), "WildCultivation.PlantNoun".Translate());

			singleX += textSize.x + 6f;
			float buttonWidth = 100f;

			var rect8 = new Rect(singleX, singleY, buttonWidth, 30f);

			if (Widgets.ButtonText(rect8, record.Key.plant.label, true, false, true))
			{
				var floatMenuOptions = new List<FloatMenuOption>();
				
				foreach (var current in DefDatabase<ThingDef>.AllDefs)
				{
					if (current.plant != null)
					{
						var floatMenuOption = new FloatMenuOption(current.label, () =>
						{
							record.Key.plant = current;
						}, MenuOptionPriority.Default, null, null, 0f, null, null);
						floatMenuOptions.Add(floatMenuOption);
					}
				}
				Find.WindowStack.Add(new FloatMenu(floatMenuOptions));
			}

			singleX += buttonWidth + 6f;


		}

		public override void ExposeData()
		{
			base.ExposeData();

			// More here
		}
	}
}
