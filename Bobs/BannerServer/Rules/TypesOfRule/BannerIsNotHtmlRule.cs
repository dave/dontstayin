using Bobs;

namespace Bobs.BannerServer.Rules.TypesOfRule
{
	internal class BannerIsNotHtmlRule : Rule
	{
		internal BannerIsNotHtmlRule() : base() { }

		public override Q Q
		{
			get
			{
				return new Q(Bobs.Banner.Columns.DisplayType, QueryOperator.NotEqualTo, Bobs.Banner.DisplayTypes.CustomHtml);
			}
		}
	}
}
