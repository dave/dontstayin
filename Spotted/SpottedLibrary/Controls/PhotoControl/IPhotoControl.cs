using System;
using System.Collections.Generic;
using System.Text;
using SpottedLibrary.Controls.TaggingControl;
using Bobs;

namespace SpottedLibrary.Controls.PhotoControl
{
	public interface IPhotoControl
	{
		Photo Photo { set; }
		//ITaggingControl TaggingControl { get; }
		string Title { set; }
		event EventHandler<EventArgs<Photo>> PhotoSet;
		//event EventHandler PhotoClick;
		//event EventHandler NextButtonClick;
		//event EventHandler PrevButtonClick;
		bool GallerySelected { set; }
	}
}
