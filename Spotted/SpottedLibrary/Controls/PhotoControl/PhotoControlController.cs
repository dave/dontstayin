using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Bobs;

namespace SpottedLibrary.Controls.PhotoControl
{
	public class PhotoControlController
	{
		IPhotoControlView view;
		PhotoControlService service;
		public PhotoControlController(IPhotoControlView view)
		{
			this.view = view;
			this.service = new PhotoControlService();
			this.view.PhotoSet += new EventHandler<EventArgs<Photo>>(view_PhotoSet);
			this.view.UsrSpottedSet += new EventHandler<EventArgs<bool>>(view_UsrSpottedSet);
			this.view.IsFavouritePhotoSet += new EventHandler<EventArgs<bool>>(view_IsFavouritePhotoSet);
			this.view.BuddySpottedInPhoto += new EventHandler(view_BuddySpottedInPhoto);
			//this.view.PhotoOfWeekToggled += new EventHandler<EventArgs<bool>>(view_PhotoOfWeekToggled);

		}

		//void view_PhotoOfWeekToggled(object sender, EventArgs<bool> e)
		//{
		//    Photo photo = view.Photo;
		//    if (photo != null)
		//    {
		//        photo.SetAsPhotoOfWeek(e.Value, view.PhotoOfWeekCaption, true, true);
		//    }
		//}

		void view_BuddySpottedInPhoto(object sender, EventArgs e)
		{
			if (view.BuddyChooser.SelectedBuddyK != null)
			{
				PhotoControlService.SetUsrSpotted(true, view.Photo, new Usr(view.BuddyChooser.SelectedBuddyK.Value), Usr.Current);
				this.view.UsrsInPhotoHtml = service.GetUsrsInPhotoHtml(view.Photo.K);
				this.view.BuddyChooser.SelectedBuddyK = null;
			}
			this.view.DataBind();
		}

		
 
		void view_IsFavouritePhotoSet(object sender, EventArgs<bool> e)
		{
			CheckUserIsLoggedIn();
			PhotoControlService.SetIsFavouritePhoto(e.Value, view.Photo);
			this.view.IsFavouritePhoto = Usr.Current != null && service.PhotoIsFavouritedByCurrentUser(view.Photo.K, view.CurrentUsrK.Value);
			this.view.DataBind();
		}

		void view_UsrSpottedSet(object sender, EventArgs<bool> e)
		{
			CheckUserIsLoggedIn();
			PhotoControlService.SetUsrSpotted(e.Value, view.Photo, Usr.Current, Usr.Current);
			this.view.UsrsInPhotoHtml = service.GetUsrsInPhotoHtml(view.Photo.K);
			this.view.UsrSpotted = view.CurrentUsrK != null && service.UsrHasBeenSpotted(view.Photo.K, view.CurrentUsrK.Value);
			this.view.DataBind();
		}
		void CheckUserIsLoggedIn()
		{
			if (Usr.Current == null)
			{
				view.Redirect(@"/pages/login?url=" + Microsoft.JScript.GlobalObject.escape(view.Url));
			}
		}
		void view_PhotoSet(object sender, EventArgs<Photo> e)
		{
			LoadPhotoIntoView(e.Value);
			this.view.DataBind();
		}
		void LoadPhotoIntoView(Photo photo)
		{
			if (photo != null)
			{
				//this.view.TaggingControl.Taggable = photo;
				this.view.UsrsInPhotoHtml = service.GetUsrsInPhotoHtml(view.Photo.K);
				this.view.UsrSpotted = view.CurrentUsrK != null && service.UsrHasBeenSpotted(photo.K, view.CurrentUsrK.Value);
				this.view.IsFavouritePhoto = Usr.Current != null && service.PhotoIsFavouritedByCurrentUser(photo.K, view.CurrentUsrK.Value);
				this.view.DisplayMakeFrontPageOptions = photo.EventK > 0 && photo.Event != null && photo.Event.Venue.Place.Country.IsCurrentUsrAdmin;
				if (Usr.Current != null && Usr.Current.IsAdmin)
				{
					view.DisplayAdminOptions();
				}
			}
			else
			{
				this.view.DisplayMakeFrontPageOptions = false;
			}
		}
	}
}
