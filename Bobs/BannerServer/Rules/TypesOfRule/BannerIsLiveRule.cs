using Bobs;

namespace Bobs.BannerServer.Rules.TypesOfRule
{
	internal class BannerIsLiveRule : Rule
	{
		internal BannerIsLiveRule() : base() { }

		public override Q Q
		{
			get
			{
				return Bobs.Banner.IsLiveQ;
			}
		}
	}
}
