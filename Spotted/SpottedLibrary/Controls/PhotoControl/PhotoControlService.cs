using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace SpottedLibrary.Controls.PhotoControl
{
	class PhotoControlService
	{

		internal string GetUsrsInPhotoHtml(int photoK)
		{
			return new Photo(photoK).UsrsInPhotoHtml;
		}

		internal bool UsrHasBeenSpotted(int photoK, int usrK)
		{		
			try
			{
				UsrPhotoMe u = new UsrPhotoMe(usrK, photoK);
				return true;
			}
			catch
			{
				return false;
			}
		}

		internal bool PhotoIsFavouritedByCurrentUser(int photoK, int usrK)
		{
			try
			{
				UsrPhotoFavourite u = new UsrPhotoFavourite(usrK, photoK);
				return true;
			}
			catch
			{
				return false;
			}
		}
		internal static void SetIsFavouritePhoto(bool isFavourite, Photo photo)
		{
			if (!photo.Validate()) { throw new DsiUserFriendlyException("You don't have permission to add this photo to your favourites!"); }

			photo.SetIsFavouritePhoto(isFavourite);
		}
		internal static void SetUsrSpotted(bool usrHasBeenSpottedInPhoto, Photo photo, Usr spottedUsr, Usr currentUsr)
		{
			if (!photo.Validate()) { throw new DsiUserFriendlyException("You don't have permission to add to this photo!"); }
			spottedUsr.SetSpottedInPhoto(photo, currentUsr, usrHasBeenSpottedInPhoto);
		}

	}
}
