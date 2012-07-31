using System;

namespace Bobs.BannerServer.Traffic
{
    public class ConstantTrafficShape : TrafficShape
    {
        public override double GetNumberOfTrafficBlocksBetweenDates(DateTime startDateTime, DateTime endDateTime, Bobs.Banner.Positions position)
        {
            return endDateTime.Subtract(startDateTime).TotalMinutes;
        }
    }
}
