using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpottedLibrary.Controls.PhotoPage;

namespace SpottedLibrary.Pages.Groups.Photos
{
	public interface IGroupPhotosView : IPhotoPageView
	{
		Bobs.Group GroupFromUrl { get; }
	}
}
