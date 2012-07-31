namespace SpottedScript.Pages.Articles.Home
{
	public class Controller
	{
		public Controller(View view)
		{
			view.ThreadControl.uiCommentsDisplay.ShowComments(int.ParseInvariant(view.uiThreadK.Value), int.ParseInvariant(view.uiPageNumber.Value));
			view.ThreadControl.CurrentParentObjectK = int.ParseInvariant(view.uiArticleK.Value);
		}
	}
}
