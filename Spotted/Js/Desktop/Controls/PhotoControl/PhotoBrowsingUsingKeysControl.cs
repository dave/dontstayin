using System;
using System.Html;
using jQueryApi;

namespace Js.Controls.PhotoBrowser
{
	public abstract class PhotoBrowsingUsingKeysControl
	{
		public EventHandler OnPhotoNextClick;
		public EventHandler OnPhotoPrevClick;
		public EventHandler OnPhotoUpClick;
		public EventHandler OnPhotoDownClick;
		public EventHandler OnArrowKeyPress;

		public PhotoBrowsingUsingKeysControl(jQueryObject[] focusControls)
		{
			for (int i = 0; i < focusControls.Length; i++)
			{
				focusControls[i].Keydown(KeyDown);
			}
		}

		protected void KeyDown(jQueryEvent e)
		{
			if (e.Which == (int)39/*Key.Right*/)
			{
				if (OnArrowKeyPress != null)
					OnArrowKeyPress(this, EventArgs.Empty);
				if (OnPhotoNextClick != null)
					OnPhotoNextClick(this, EventArgs.Empty);
				e.PreventDefault();
			}
			else if (e.Which == (int)37/*Key.Left*/)
			{
				if (OnArrowKeyPress != null)
					OnArrowKeyPress(this, EventArgs.Empty); 
				if (OnPhotoPrevClick != null)
					OnPhotoPrevClick(this, EventArgs.Empty);
				e.PreventDefault();
			}
			//else if (e.KeyCode == (int)Key.Up)
			//{
			//    if (OnArrowKeyPress != null)
			//        OnArrowKeyPress(this, EventArgs.Empty);
			//    if (OnPhotoUpClick != null)
			//        OnPhotoUpClick(this, EventArgs.Empty);
			//    e.PreventDefault();
			//}
			//else if (e.KeyCode == (int)Key.Down)
			//{
			//    if (OnArrowKeyPress != null)
			//        OnArrowKeyPress(this, EventArgs.Empty);
			//    if (OnPhotoDownClick != null)
			//        OnPhotoDownClick(this, EventArgs.Empty);
			//    e.PreventDefault();
			//}
			else
			{
				NonArrowKeyDown(e);
			}
		}
		protected virtual void NonArrowKeyDown(jQueryEvent e) { }
	}
}
