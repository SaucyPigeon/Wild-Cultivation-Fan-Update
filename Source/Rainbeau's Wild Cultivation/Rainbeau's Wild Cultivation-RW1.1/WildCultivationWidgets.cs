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
	public class WildCultivationWidgets
	{
		public class ButtonArgs
		{
			public bool drawBackground = true;
			public bool doMouseoverSound = true;
			public bool active = true;

			public ButtonArgs()
			{

			}

			public ButtonArgs(bool drawBackground, bool doMouseoverSound, bool active)
			{
				this.drawBackground = drawBackground;
				this.doMouseoverSound = doMouseoverSound;
				this.active = active;
			}
		}

		public static void ButtonFloatMenu<T>(Rect rect, string label, Action<T> action, Predicate<T> predicate = null, ButtonArgs args = null)
			where T : Def
		{
			Guard.OnArgNull(action, nameof(action));
			Guard.OnArgNull(label, nameof(label));

			if (args == null)
			{
				args = new ButtonArgs();
			}

			if (Widgets.ButtonText(rect, label, args.drawBackground, args.doMouseoverSound, args.active))
			{
				var floatMenuOptions = new List<FloatMenuOption>();

				foreach (var element in DefDatabase<T>.AllDefs)
				{
					if (predicate == null || predicate.Invoke(element))
					{
						var floatMenuOption = new FloatMenuOption(element.label, () => { action.Invoke(element); }, MenuOptionPriority.Default, null, null, 0f, null, null);
						floatMenuOptions.Add(floatMenuOption);
					}
				}
				Find.WindowStack.Add(new FloatMenu(floatMenuOptions));
			}
		}

		public static void LabelFitted(string label, Vector2 position, out Vector2 textSize, float width = -1, float height = -1)
		{
			textSize = Text.CalcSize(label);

			if (width == -1)
			{
				width = textSize.x;
			}
			if (height == -1)
			{
				height = textSize.y;
			}

			Widgets.Label(new Rect(position, new Vector2(width, height)), label);
		}

		public static void LabelFitted(string label, float x, float y, out Vector2 textSize, float width = -1, float height = -1)
		{
			LabelFitted(label, new Vector2(x, y), out textSize, width, height);
		}


		private WildCultivationWidgets()
		{
		}
	}
}
