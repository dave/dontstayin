using Bobs;

namespace Bobs.BannerServer.Rules.TypesOfRule
{
	internal class BannerKRule : Rule
	{
        int bannerK;
		internal BannerKRule(int bannerK) 
		{
            this.bannerK = bannerK;
		}


		public override Q Q
		{
			get { return new Q(Bobs.Banner.Columns.K, QueryOperator.EqualTo, this.bannerK); }
		}

	}
}
