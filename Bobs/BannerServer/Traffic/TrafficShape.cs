using System;
using Common.Clocks;
using Common;

namespace Bobs.BannerServer.Traffic
{
    public abstract class TrafficShape
    {
        public abstract double GetNumberOfTrafficBlocksBetweenDates(DateTime startDateTime, DateTime endDateTime, Bobs.Banner.Positions position);

		public double GetNumberOfTrafficBlocksUntil(DateTime endDate, Bobs.Banner.Positions position)
        {
            return GetNumberOfTrafficBlocksBetweenDates(Time.Now, endDate, position);
        }
    }
}
