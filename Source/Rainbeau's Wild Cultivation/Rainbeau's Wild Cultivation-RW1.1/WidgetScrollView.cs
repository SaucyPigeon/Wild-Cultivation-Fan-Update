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
	public class WidgetScrollView : IDisposable
	{
		Vector2 scrollPosition;

		public WidgetScrollView(Rect outRect, Rect viewRect, bool showScrollBars = true)
		{
			Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect, showScrollBars);
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					Widgets.EndScrollView();
					scrollPosition = default(Vector2);
				}
				disposedValue = true;
			}
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
		}
		#endregion
	}
}
