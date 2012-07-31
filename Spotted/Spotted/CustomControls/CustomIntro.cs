using Bobs;

namespace Spotted.CustomControls
{
	public class CustomIntro : Intro
	{
		IObjectPage objectPage;
		public override IObjectPage ObjectPage
		{
			get { return this.objectPage; }
		}
		public IPicObjectPage PicObjectPage
		{
			get { return (IPicObjectPage)this.objectPage; }
		}
		public void Set(IObjectPage p)
		{
			this.objectPage = p;
		}
	}
}
