using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Common;
using SpottedLibrary.Controls.PhotoBrowserControl;
using SpottedLibrary.Controls.PhotoPage;
using Pair = System.Collections.Generic.KeyValuePair<object, Bobs.OrderBy.OrderDirection>;
namespace SpottedLibrary.Pages.Articles.Photos
{
	public class ArticlePhotosController : PhotoPageController
	{
		new IArticlePhotosView view;
		public ArticlePhotosController(IArticlePhotosView view)
			: base(view)
		{
			this.view = view;
		}

 

		protected override IPagedDataService<Photo> GetPagedDataService()
		{
			Article article = view.ArticleFromUrl;
			IPagedDataService<Photo> photoPagedDataService;
			Gallery gallery = article.ChildGallerys()[0];
			photoPagedDataService = gallery.ChildPhotos(Photo.EnabledQueryCondition, Photo.DefaultOrder);
			return photoPagedDataService;
		}

		protected override string GetTitle()
		{
			return view.ArticleFromUrl.Name;
		}
	}

}
