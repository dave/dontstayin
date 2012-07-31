using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpottedLibrary.Controls.PhotoPage;
using SpottedLibrary.Controls.PhotoBrowserControl;
using Common;
using Bobs;

namespace SpottedLibrary.Pages.Usrs.Photos
{
	public class UsrPhotosController : PhotoPageController
	{
		new IUsrPhotosView view;
		public UsrPhotosController(IUsrPhotosView view)
			: base(view)
		{
			this.view = view;
		}

		protected override IPagedDataService<Photo> GetPagedDataService()
		{
			return GetUsrPhotosPagedDataService(view.UsrFromUrl, view.SpottedByUsrK);
		}

		public static IPagedDataService<Photo> GetUsrPhotosPagedDataService(Usr usr, int spottedByUsrK)
		{
			var orderBy = new []
			{
				new KeyValuePair<object, OrderBy.OrderDirection>(Photo.Columns.DateTime, OrderBy.OrderDirection.Descending)	,
				new KeyValuePair<object, OrderBy.OrderDirection>(Photo.Columns.K, OrderBy.OrderDirection.Descending)	
			};
			if (spottedByUsrK > 0)
			{
				return usr.ChildPhotosOfMe(new Q(Photo.Columns.UsrK, spottedByUsrK), orderBy);
			}
			else
			{
				return usr.ChildPhotosOfMe(orderBy);
			}
		}


		protected override string GetTitle()
		{
			return "Photos of " + view.UsrFromUrl.NickName;
		}
	}
}
