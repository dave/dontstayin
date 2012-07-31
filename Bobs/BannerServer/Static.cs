using System;

using Bobs.BannerServer.Traffic;


namespace Bobs.BannerServer
{
	public static class Static
	{
		private static TrafficShape trafficShapeInstance = new DataDrivenTrafficShape();

		public static TrafficShape TrafficShape
		{
			get { return trafficShapeInstance; }
			set
			{
				lock (trafficShapeInstance)
				{
					trafficShapeInstance = value;
				}
			}
		}
	}
}
