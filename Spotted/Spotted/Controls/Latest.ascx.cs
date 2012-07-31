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
using Bobs;


namespace Spotted.Controls
{
	public partial class Latest : System.Web.UI.UserControl
	{
		protected Controls.LatestEventList LatestEventList1;
		protected Controls.LatestChatHotTopics LatestHotTopicsUc;
		public Controls.EventBox EventBox;
		public Panel ChatHolderOuter;
		public global::Spotted.Controls.LatestContent LatestContentUc;

		public Latest()
		{
			this.Init += new EventHandler(Latest_Init);
			this.Load += new EventHandler(Latest_Load);
			this.PreRender += new EventHandler(Latest_PreRender);
		}

		void Latest_Load(object sender, EventArgs e)
		{
			if (ParentObjectType == Model.Entities.ObjectType.None ||
				ParentObjectType == Model.Entities.ObjectType.Country ||
				ParentObjectType == Model.Entities.ObjectType.Place)
			{

				EventBox.ObjectType = ParentObjectType;
				EventBox.ObjectK = ParentObjectK;
				EventBox.Build();
			}
			else
			{
				EventBox.Visible = false;
			}
		}

		void Latest_Init(object sender, EventArgs e)
		{
			
			LatestChatUc.Items = Items;
			LatestChatUc.ExternalHeader = ChatHeader;

			if (LatestHotTopicsUc != null)
				LatestHotTopicsUc.Items = Items;

			if (Parent != null && Parent is IDiscussable)
			{
				LatestChatUc.Discussable = (IDiscussable)Parent;
				if (LatestHotTopicsUc != null)
					LatestHotTopicsUc.Discussable = (IDiscussable)Parent;
			}
		}

		void Latest_PreRender(object sender, EventArgs e)
		{

			if (!LatestChatUc.CurrentForumCheckPermissionRead)
			{
				AddThreadLinkPanel.Visible = false;
			}

			

			if (AddThreadStatusHidden.Value.Equals("1"))
			{
				AddThreadPanel.Style["display"] = "";
				AddThreadLinkP.Style["display"] = "none";
			}
			else
			{
				AddThreadPanel.Style["display"] = "none";
				AddThreadLinkP.Style["display"] = "";
			}

			if ( Parent != null && Parent is IDiscussable && ( ParentObjectType == Model.Entities.ObjectType.Photo || ParentObjectType == Model.Entities.ObjectType.Article ) )
			{
				AddThreadLinkPanel.Visible = false;
			}


			if (!LatestChatUc.HasContent)
			{
				HotTopicsHeader.Visible = false;
				LatestChatUcHolder.Visible = false;
				TabClientScript.Visible = false;
				ChatHeader.Attributes["onclick"] = "";
			}
			
		}

		public Model.Entities.ObjectType ParentObjectType
		{
			get
			{
				if (Parent == null)
					return Model.Entities.ObjectType.None;
				else
					return ((IDiscussable)Parent).ObjectType;
			}
		}
		public int ParentObjectK
		{
			get
			{
				if (Parent == null)
					return 0;
				else
					return ((IHasSinglePrimaryKey)Parent).K;
			}
		}

		public object Parent
		{
			get
			{
				return parent;
			}
			set
			{
				parent = value;
				if (parent != null && parent is IDiscussable)
				{
					if (LatestChatUc != null)
						LatestChatUc.Discussable = (IDiscussable)parent;

					if (LatestHotTopicsUc != null)
						LatestHotTopicsUc.Discussable = (IDiscussable)parent;
				}
			}
		}
		object parent;

		public int Items
		{
			get
			{
				return items;
			}
			set
			{
				items = value;

				if (LatestChatUc != null)
					LatestChatUc.Items = items;

				if (LatestHotTopicsUc != null)
					LatestHotTopicsUc.Items = items;
			}
		}
		int items = 5;

		#region ContaierPage
		Master.DsiPage ContainerPage
		{
			get { return (Master.DsiPage)Page; }
		}
		#endregion
		#region HasContent
		public bool HasContent
		{
			get { return LatestChatUc.HasContent || LatestContentUc.Visible; }
		}
		#endregion




	}
}
