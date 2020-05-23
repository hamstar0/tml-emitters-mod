using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emitters
{
	public class EmitterUtils
	{
		internal static double NextFloat(Random random)
		{
			double val = random.NextDouble(); // range 0.0 to 1.0
			return float.MaxValue * (float)val;
		}
	}
}
