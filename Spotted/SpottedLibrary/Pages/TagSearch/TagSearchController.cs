using System;
using System.Collections.Generic;
using System.Text;
using SpottedLibrary.Controls.PhotoBrowserControl;
using SpottedLibrary.Controls.PagedRepeater;
using Common;
using SpottedLibrary.Controls.PhotoControl;
using SpottedLibrary.Controls.TaggingControl;
using SpottedLibrary.Controls.PaginationControl2;
using SpottedLibrary.Controls.SearchBoxControl;
using Bobs;
using SpottedLibrary.Controls.PhotoPage;
using Bobs.CachedDataAccess;

namespace SpottedLibrary.Pages.TagSearch
{
	public class TagSearchController : PhotoPageController
	{
		new ITagSearchView view;
		public TagSearchController(ITagSearchView view)
			:base(view)
		{
			this.view = view;
			this.view.Init +=new EventHandler(view_Init);
		}

		
	 

		void view_Init(object sender, EventArgs e)
		{
			//this.view.TaggingControl.TagChanged += new EventHandler<TagChangedEventHandler>(TaggingControl_TagChanged);
		}
	
		void TaggingControl_TagChanged(object sender, TagChangedEventHandler e)
		{
			SearchQuery sq = SearchQuery.Parse(view.SearchBoxControl.Text);

			foreach (string tag in sq.TagsQueryParts)
			{
				if (tag == e.Tag)
				{
					view.Reload();
				}
			}
			
		}



		protected override IPagedDataService<Photo> GetPagedDataService()
		{
			SearchQuery sq;
			if (!view.IsPostBack)
			{
				sq = SearchQuery.Parse(this.view.CutDownUrl);
				view.ViewState["SearchQueryString"] = sq.SearchString;
				view.SearchBoxControl.Text = sq.SearchString;
				view.TagCloud.SearchBoxControl.Text = sq.SearchString;
			}
			else
			{
				sq = SearchQuery.Parse(view.ViewState["SearchQueryString"] as string);
			}
			var ds = new CachedSqlSelect<Photo>(new TagSearchCachedSqlSelectDefinition(sq.TagsQueryParts));
			view.TagCloud.Visible = ds.Count == 0;
			view.SearchBoxControlVisible = ds.Count > 0;
			return ds;//.Convert(photo => new P(photo, p => view.Url.CurrentUrl("photo", p.K, "c", null)));
		}

		protected override string GetTitle()
		{
			return "Tag search";
		}
	}
}
