using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace SpottedLibrary.Controls.BuddyChooser
{
	public class BuddyChooserController
	{
		IBuddyChooserView view;
		public BuddyChooserController(IBuddyChooserView view)
		{
			this.view = view;
			this.view.UsrSet += new EventHandler<EventArgs<Bobs.Usr>>(view_UsrSet);
		}

		void view_UsrSet(object sender, EventArgs<Bobs.Usr> e)
		{
			if (e.Value != null)
			{
				this.view.Buddies = new List<Usr>(e.Value.BuddiesFull).ConvertAll(u => new KeyValuePair<string, int>(u.NickName, u.K));
			}
		}
	}
}
