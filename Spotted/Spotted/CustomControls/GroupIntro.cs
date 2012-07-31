using Bobs;

namespace Spotted.CustomControls
{
	public class GroupIntro : Intro
	{
		public override IObjectPage ObjectPage
		{
			get { return this.DsiPage.Url.HasGroupObjectFilter ? this.DsiPage.Url.ObjectFilterGroup : null; }
		}
	}
}
