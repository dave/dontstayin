using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities.Properties;

namespace Bobs
{

	public static class IHasSpatialDataExtensions
	{
		public static void CopySpatialDataFrom(this IHasSpatialData dest, IHasSpatialData source)
		{
			dest.Lat = source.Lat;
			dest.Lon = source.Lon;
		}
		
	}
}
