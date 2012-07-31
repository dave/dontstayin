using System;
using Js.Controls.PhotoControl;

namespace Js.Controls.PhotoBrowser
{
	public class PhotoEventArgs : EventArgs
	{
		public PhotoStub Photo;
		public PhotoEventArgs(PhotoStub photo)
			: base()
		{
			this.Photo = photo;
		}
	}

	public class PhotoSetEventArgs : EventArgs
	{
		public PhotoStub[] PhotoSet;
		public int SelectedIndex;
		public PhotoSetEventArgs(PhotoStub[] photoSet, int selectedIndex)
			: base()
		{
			this.PhotoSet = photoSet;
			this.SelectedIndex = selectedIndex;
		}
	}
}
