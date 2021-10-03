using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComunaHealth
{
	/// <summary>
	/// Representa una region de salud de la provincia de buenos aires
	/// </summary>
	[Flags]
	public enum ERegionSanitariaBSAS
	{
		Region1 = 1<<0,
		Region2 = 1<<1,
		Region3 = 1<<2,
		Region4 = 1<<3,
		Region5 = 1<<4,
		Region6 = 1<<5,
		Region7 = 1<<6,
		Region8 = 1<<7,
		Region9 = 1<<8,
		Region10 = 1<<9,
		Region11 = 1<<10,
		Region12 = 1<<11,

		NINGUNA = 0
	}
}
