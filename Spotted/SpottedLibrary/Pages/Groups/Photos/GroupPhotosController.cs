using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpottedLibrary.Controls.PhotoPage;
using SpottedLibrary.Controls.PhotoBrowserControl;
using Common;
using Bobs;

namespace SpottedLibrary.Pages.Groups.Photos
{
	public class GroupPhotosController : PhotoPageController
	{
		new IGroupPhotosView view;
		public GroupPhotosController(IGroupPhotosView view)
			: base(view)
		{
			this.view = view;
		}

		protected override IPagedDataService<Photo> GetPagedDataService()
		{
			return GetGroupPhotosPagedDataService(view.GroupFromUrl);
		}

		public static IPagedDataService<Photo> GetGroupPhotosPagedDataService(Group group)
		{
			var orderBy = new System.Collections.Generic.KeyValuePair<object, Bobs.OrderBy.OrderDirection>
				(GroupPhoto.Columns.DateTime, OrderBy.OrderDirection.Descending);
			return group.ChildPhotos(new Q(Photo.Columns.IsInCaptionCompetition, false), orderBy);
		}


		protected override string GetTitle()
		{
			return view.GroupFromUrl.Name;
		}
	}
}
