using System;
using System.Collections.Generic;
using System.Text;
using SpottedLibrary.Controls.PhotoControl;
using SpottedLibrary.Controls.ThreadControl;
using SpottedLibrary.Controls.LatestChat;

namespace SpottedLibrary.Controls.PhotoWithComments
{
	public interface IPhotoWithCommentsView : IView
	{
		IPhotoControl PhotoControl { get; }
		IThreadControl ThreadControl { get; }
		ILatestChat LatestChat { get; }
		

	}
}
