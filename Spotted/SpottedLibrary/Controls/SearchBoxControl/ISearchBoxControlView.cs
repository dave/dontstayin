﻿using System;
using System.Collections.Generic;
using System.Text;
using SpottedLibrary.Controls.PagedRepeater;
using SpottedLibrary.Controls.PhotoBrowserControl;

namespace SpottedLibrary.Controls.SearchBoxControl
{
	public interface ISearchBoxControlView : IView
	{
		string Text { get; set; }
		event EventHandler SearchButtonClick;
		void Redirect(string url);
	}
}
