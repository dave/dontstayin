using System;
using System.Collections.Generic;
using System.Text;
using SpottedLibrary.Controls.TaggingControl;
using Bobs.Tagging;
using Common;
using Bobs;
using SpottedLibrary.Controls.BuddyChooser;

namespace SpottedLibrary.Controls.PhotoControl
{
	public interface IPhotoControlView : IView
	{
		Photo Photo { get; }
		//ITaggingControl TaggingControl { get; }
		int? CurrentUsrK { get; }
		event EventHandler<EventArgs<bool>> IsFavouritePhotoSet;
		event EventHandler<EventArgs<bool>> UsrSpottedSet;
		//event EventHandler<EventArgs<bool>> PhotoOfWeekToggled;
		event EventHandler<EventArgs<Photo>> PhotoSet;
		event EventHandler BuddySpottedInPhoto;

		IBuddyChooser BuddyChooser { get; }
		bool Visible { set; }
		bool IsFavouritePhoto { set; }
		string UsrsInPhotoHtml { set; }
		bool UsrSpotted { set; }
		string Url { get; }
		void Redirect(string url);
		bool DisplayMakeFrontPageOptions { set; }
		//string PhotoOfWeekCaption { get; }
		void DisplayAdminOptions();
	}
}
