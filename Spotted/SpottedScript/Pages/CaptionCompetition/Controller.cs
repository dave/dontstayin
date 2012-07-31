using Sys.UI;

namespace SpottedScript.Pages.CaptionCompetition
{
	public class Controller
	{
		public Controller(View view)
		{
			view.uiCommentsDisplay.ShowComments(
				view.ThreadK.Value != "" ? int.ParseInvariant(view.ThreadK.Value) : 0,
			    view.PageNumber.Value != "" ? int.ParseInvariant(view.PageNumber.Value) : 0);
		}
	}
}
