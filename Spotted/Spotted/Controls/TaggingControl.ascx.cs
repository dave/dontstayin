using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SpottedLibrary.Controls.TaggingControl;
using System.Collections.Generic;
using Bobs.Tagging;
using Bobs;

namespace Spotted.Controls
{
	public partial class TaggingControl : EnhancedUserControl, ITaggingControlView, ITaggingControl
	{
		TaggingControlController controller;
		public TaggingControl()
		{
			controller = new TaggingControlController(this);
		}
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.uiAddTagButton.Click += new ImageClickEventHandler(uiAddTagButton_Click);
			this.uiTagRepeater.ItemCommand += new RepeaterCommandEventHandler(uiTagRepeater_ItemCommand);
			this.uiTagRepeater.ItemDataBound += new RepeaterItemEventHandler(uiTagRepeater_ItemDataBound);
		}

		void uiTagRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			ImageButton button = e.Item.FindControl("uiRemove") as ImageButton;
			if (button != null)
			{
				button.Attributes["onclick"] = "return removeConfirm('" + ((Tag)e.Item.DataItem).TagText + "')";
			}
		
		}

		void uiAddTagButton_Click(object sender, ImageClickEventArgs e)
		{
			string tag = this.uiTagAutoSuggest.Text;
			if (Tags.ConvertAll(t => t.TagText).Contains(tag)) { return; }
			if (this.AddTagClick != null) { this.AddTagClick(this, new EventArgs<string>(tag)); }
			if (this.TagChanged != null)
			{
				this.TagChanged(this, new TagChangedEventHandler(TagChangedEventHandler.TagChangedAction.Added, tag));
			}
		}

		void uiTagRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "Remove")
			{
				string tag = (string) e.CommandArgument;

				if (this.RemoveTagClick != null) { this.RemoveTagClick(this, new EventArgs<string>(tag)); }
				if (this.TagChanged != null)
				{
					this.TagChanged(this, new TagChangedEventHandler(TagChangedEventHandler.TagChangedAction.Removed, tag));
				}
			}
		}

		public bool DisplayRemoveTagOptions { get; set; }
		public string AddTagText { set { this.uiTagAutoSuggest.Text = value; } }
		public List<Tag> Tags
		{
			set
			{
				ViewState["Tags"] = value;
				if (Tags.Count == 0)
				{
					this.uiTagRepeater.Visible = false;
				}
				else
				{
					this.uiTagRepeater.Visible = true;
					this.uiTagRepeater.DataSource = Tags;
					this.uiTagRepeater.DataBind();
				}
			}
			protected get { return ViewState["Tags"] as List<Tag>; }
		}

		public ITaggable Taggable
		{
			get { return ViewState["ITaggable"] as ITaggable; }
			set
			{
				if (value == null)
				{
					ViewState["ITaggable"] = null;
				}
				else
				{
					ViewState["ITaggable"] = new Taggable(value.K, value.ItemOwnerUsrK);
					if (this.TaggableChanged != null) { this.TaggableChanged(this, new EventArgs()); }
				}
			}
		}
		public event EventHandler<EventArgs<string>> AddTagClick;
		public event EventHandler<EventArgs<string>> RemoveTagClick;
		public event EventHandler TaggableChanged;
		public event EventHandler<TagChangedEventHandler> TagChanged;
	}
}
