using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using RimWorld;
using Verse;

namespace RWC_Code
{
	class Debug
	{
		[Conditional("DEBUG")]
		public static void Out(string message)
		{
			Log.Message(message, true);
		}

		private Debug()
		{ }
	}
}
