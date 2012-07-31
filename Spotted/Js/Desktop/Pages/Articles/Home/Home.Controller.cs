namespace Js.Pages.Articles.Home
{
	public class Controller
	{
		public Controller(View view)
		{
			view.ThreadControl.uiCommentsDisplay.ShowComments(int.Parse(view.uiThreadK.Value), int.Parse(view.uiPageNumber.Value));
			view.ThreadControl.CurrentParentObjectK = int.Parse(view.uiArticleK.Value);
		}
	}
}
