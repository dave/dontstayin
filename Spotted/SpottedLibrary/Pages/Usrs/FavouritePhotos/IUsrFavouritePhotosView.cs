using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpottedLibrary.Controls.PhotoPage;

namespace SpottedLibrary.Pages.Usrs.FavouritePhotos
{
	public interface IUsrFavouritePhotosView : IPhotoPageView
	{
		Bobs.Usr UsrFromUrl { get; }
	}
}
