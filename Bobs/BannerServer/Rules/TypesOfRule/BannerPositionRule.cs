using Bobs;

namespace Bobs.BannerServer.Rules.TypesOfRule
{
	internal class BannerPositionRule : Rule
	{
        Bobs.Banner.Positions position;

		internal BannerPositionRule(Bobs.Banner.Positions position)
		{
            this.position = position;
		}


		public override Q Q
		{
			get { return new Q(Bobs.Banner.Columns.Position, this.position); }
		}

	}
}
