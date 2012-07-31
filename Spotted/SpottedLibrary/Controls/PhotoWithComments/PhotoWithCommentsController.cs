using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace SpottedLibrary.Controls.PhotoWithComments
{
	public class PhotoWithCommentsController
	{
		IPhotoWithCommentsView view;
		public PhotoWithCommentsController(IPhotoWithCommentsView view)
		{
			this.view = view;
			this.view.Init += new EventHandler(view_Init);
		}


		void view_Init(object sender, EventArgs e)
		{
			this.view.PhotoControl.PhotoSet += new EventHandler<EventArgs<Photo>>(PhotoControl_PhotoSet);
		}

		void PhotoControl_PhotoSet(object sender, EventArgs<Photo> e)
		{
			int currentPage;
			if (view.IsPostBack)
			{
				currentPage = this.view.ViewState["CommentsPage"] as int? ?? 1;
			}
			else
			{
				currentPage = view.CutDownUrl["c"].ValueInt == 0 ? 1 : view.CutDownUrl["c"].ValueInt;
				this.view.ViewState["CommentsPage"] = currentPage;
			}
			this.view.ThreadControl.Visible = e.Value != null;
			this.view.ThreadControl.ThreadK = e.Value == null ? null : e.Value.ThreadK;
			this.view.ThreadControl.ParentObjectK = e.Value == null ? (int?)null : e.Value.K;
			this.view.ThreadControl.ParentObjectType = e.Value == null ? (Model.Entities.ObjectType?)null : e.Value.ObjectType;
			this.view.ThreadControl.CurrentPage = currentPage;
	
			this.view.ThreadControl.Initialise();

			//this.view.ThreadControl.DataBind();
			this.view.LatestChat.Visible = e.Value != null;
			this.view.LatestChat.Discussable = e.Value;
			
			this.view.LatestChat.DataBind();
		}

	}
}
