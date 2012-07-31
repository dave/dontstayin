using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Entities.Properties
{
	public interface IHasSpatialData
	{
		double Lat { get; set; }
		double Lon { get; set; }
		long HtmId { get; }
	}
}
