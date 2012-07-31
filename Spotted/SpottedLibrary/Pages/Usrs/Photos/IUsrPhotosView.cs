using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpottedLibrary.Controls.PhotoPage;

namespace SpottedLibrary.Pages.Usrs.Photos
{
	public interface IUsrPhotosView : IPhotoPageView
	{
		Bobs.Usr UsrFromUrl { get; }
		int SpottedByUsrK { get; }
	}
}
