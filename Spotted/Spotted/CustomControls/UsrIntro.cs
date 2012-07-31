using Bobs;

namespace Spotted.CustomControls
{
	public class UsrIntro : Intro
	{
		public override IObjectPage ObjectPage
		{
			get { return this.DsiPage.Url.HasUsrObjectFilter ? this.DsiPage.Url.ObjectFilterUsr : null; }
		}
	}
}
