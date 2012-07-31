using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Bobs.Tagging;
using Bobs;

namespace SpottedLibrary.Controls.TaggingControl
{
	public interface ITaggingControlView : IView
	{
		bool DisplayRemoveTagOptions { set; }
		event EventHandler<EventArgs<string>> AddTagClick;
		event EventHandler<EventArgs<string>> RemoveTagClick;
		event EventHandler TaggableChanged;
		string AddTagText { set; }

		List<Tag> Tags { set; }
		ITaggable Taggable { get; }
	}
}
