using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace SpottedLibrary.Controls.LatestChat
{
	public interface ILatestChat : IView
	{
		IDiscussable Discussable { set; }
	}
}
