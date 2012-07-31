using System;
using System.Collections.Generic;
using System.Text;
using Bobs.Tagging;

namespace SpottedLibrary.Controls.TaggingControl
{
	public interface ITaggingControl
	{
		ITaggable Taggable { set; }
		event EventHandler<TagChangedEventHandler> TagChanged;
	}
	public class TagChangedEventHandler : EventArgs
	{
		public enum TagChangedAction
		{
			Added,
			Removed
		}
		public TagChangedAction Action { get; private set; }
		public string Tag { get; private set; }
		public TagChangedEventHandler(TagChangedAction action, string tag)
		{
			this.Action = action;
			this.Tag = tag;
		}

	}
}
