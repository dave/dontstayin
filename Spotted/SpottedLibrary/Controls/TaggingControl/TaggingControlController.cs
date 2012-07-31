using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Bobs.Tagging;
namespace SpottedLibrary.Controls.TaggingControl
{
	public class TaggingControlController
	{
		ITaggingControlView view;
		public TaggingControlController(ITaggingControlView view)
		{
			this.view = view;
			this.view.AddTagClick += new EventHandler<EventArgs<string>>(view_AddTagClick);
			this.view.RemoveTagClick += new EventHandler<EventArgs<string>>(view_RemoveTagClick);
			this.view.TaggableChanged += new EventHandler(view_TaggableChanged);			
		}

		void view_TaggableChanged(object sender, EventArgs e)
		{
			var tags = view.Taggable.ChildTags(new And(new Q(TagPhoto.Columns.Disabled, false), new Q(Tag.Columns.Blocked, false)));
			view.Tags = new List<Tag>(tags);
			view.DisplayRemoveTagOptions = tags.Count > 0;
		}

		void view_RemoveTagClick(object sender, EventArgs<string> e)
		{
			Tag tag = Tag.GetTag(e.Value);
			Query query = new Query(new Q(Bobs.TagPhoto.Columns.TagK, tag.K), new Q(Bobs.TagPhoto.Columns.PhotoK, view.Taggable.K));
			TagPhotoSet set = new TagPhotoSet(query);
			if (set.Count > 0)
			{
				set[0].SetDisabledAndUpdate(true);
			}
			view_TaggableChanged(this, null);
		}

		void view_AddTagClick(object sender, EventArgs<string> e)
		{
			if (Tag.AddTag(e.Value, this.view.Taggable, Usr.Current) != null)
				view_TaggableChanged(this, null);

			this.view.AddTagText = "";

		}
	}
}
