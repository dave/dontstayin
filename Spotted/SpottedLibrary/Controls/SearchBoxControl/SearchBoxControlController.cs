using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using System.Linq;

namespace SpottedLibrary.Controls.SearchBoxControl
{
	public class SearchBoxControlController
	{
		ISearchBoxControlView view;
		public SearchBoxControlController(ISearchBoxControlView view)
		{
			this.view = view;
			this.view.Init += new EventHandler(view_Init);
		}

		void view_Init(object sender, EventArgs e)
		{
			this.view.SearchButtonClick += new EventHandler(view_SearchButtonClick);
		}

		void view_SearchButtonClick(object sender, EventArgs e)
		{
			SearchQuery sq = SearchQuery.Parse(this.view.Text);
			var tags = String.Join("/", sq.TagsQueryParts.ToList().ConvertAll(t => Cambro.Web.Helpers.UrlTextSerialize(t)).ToArray());
			this.view.Redirect(UrlInfo.MakeUrl("/tags/" + tags, null));
		}
	}
}
