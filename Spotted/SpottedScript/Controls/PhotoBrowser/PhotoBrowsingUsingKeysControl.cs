using Sys;
using Sys.UI;
using System;
using System.DHTML;

namespace SpottedScript.Controls.PhotoBrowser
{
	public abstract class PhotoBrowsingUsingKeysControl
	{
		internal EventHandler OnPhotoNextClick;
		internal EventHandler OnPhotoPrevClick;
		internal EventHandler OnPhotoUpClick;
		internal EventHandler OnPhotoDownClick;
		internal EventHandler OnArrowKeyPress;

		public PhotoBrowsingUsingKeysControl(DOMElement[] focusControls)
		{
			for (int i = 0; i < focusControls.Length; i++)
			{
				DomEvent.AddHandler(focusControls[i], "keydown", new DomEventHandler(KeyDown));
			}
		}

		protected void KeyDown(DomEvent e)
		{
			if (e.KeyCode == (int)Key.Right)
			{
				if (OnArrowKeyPress != null)
					OnArrowKeyPress(this, EventArgs.Empty);
				if (OnPhotoNextClick != null)
					OnPhotoNextClick(this, EventArgs.Empty);
				e.PreventDefault();
			}
			else if (e.KeyCode == (int)Key.Left)
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
		protected virtual void NonArrowKeyDown(DomEvent e) { }
	}
}
