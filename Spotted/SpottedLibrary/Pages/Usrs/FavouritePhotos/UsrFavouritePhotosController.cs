using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpottedLibrary.Controls.PhotoPage;
using SpottedLibrary.Controls.PhotoBrowserControl;
using Common;
using Bobs;

namespace SpottedLibrary.Pages.Usrs.FavouritePhotos
{
	public class UsrFavouritePhotosController : PhotoPageController
	{
		new IUsrFavouritePhotosView view;
		public UsrFavouritePhotosController(IUsrFavouritePhotosView view)
			: base(view)
		{
			this.view = view;
		}

		protected override IPagedDataService<Photo> GetPagedDataService()
		{
			return GetUsrPhotosPagedDataService(view.UsrFromUrl);
		}

		public static IPagedDataService<Photo> GetUsrPhotosPagedDataService(Usr usr)
		{
			var orderBy = new []
			{
				new KeyValuePair<object, OrderBy.OrderDirection>(Photo.Columns.DateTime, OrderBy.OrderDirection.Descending),
				new KeyValuePair<object, OrderBy.OrderDirection>(Photo.Columns.K, OrderBy.OrderDirection.Descending)
			};
			return usr.ChildFavouritePhotos(orderBy);
		}


		protected override string GetTitle()
		{
			return view.UsrFromUrl.NickName + "'s favourite photos";
		}
	}
}
