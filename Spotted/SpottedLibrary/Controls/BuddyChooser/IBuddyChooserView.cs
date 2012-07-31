using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Bobs;

namespace SpottedLibrary.Controls.BuddyChooser
{
	public interface IBuddyChooserView : IView
	{
		IEnumerable<KeyValuePair<string, int>> Buddies { set; }
		event EventHandler<EventArgs<Usr>> UsrSet;
	}
}
