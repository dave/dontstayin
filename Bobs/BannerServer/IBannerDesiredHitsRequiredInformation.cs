using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs.BannerServer
{
	public interface IBannerDesiredHitsRequiredInformation
	{
		int K { get; }
		DateTime FirstDay { get; }
		DateTime LastDay { get; }
		Banner.Positions Position { get; }
		int RemainingImpressions { get; }
		long TotalHits { get; }
		int TotalRequiredImpressions { get; }
		
	}
	public static class IBannerDesiredHitsRequiredInformationExtensions
	{
		public static int RemainingImpressionsWithMultiplier(this IBannerDesiredHitsRequiredInformation b, double multiplier)
		{
			if (b.TotalHits >= b.TotalRequiredImpressions) return 0;
			return Convert.ToInt32(multiplier * b.TotalRequiredImpressions) - (int)b.TotalHits;
		}
	}
}
