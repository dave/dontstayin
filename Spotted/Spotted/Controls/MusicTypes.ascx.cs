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
using System.Collections.Generic;
namespace Spotted.Controls
{
	public partial class MusicTypes : System.Web.UI.UserControl
	{
		protected PlaceHolder Tree;
		public MusicTypeSet InitialMusicTypes
		{
			set
			{
				initialMusicTypes = value;
			}
		}
		MusicTypeSet initialMusicTypes;

		public List<int> SelectedMusicTypes
		{
			get
			{
				List<int> tmpArrayList = new List<int>();
				MusicType mtRoot = new MusicType(1);
				CheckBox cb = (CheckBox)Cambro.Web.Helpers.SearchControl(Tree, "ItemCb1");
				if (cb.Checked)
					tmpArrayList.Add(1);
				else
					tmpArrayList.AddRange(SelectedChildren(mtRoot.Children));
				return tmpArrayList;
			}
		}
		public List<int> SelectedChildren(MusicTypeSet children)
		{
			List<int> tmpArrayList = new List<int>();

			foreach (MusicType mt in children)
			{
					CheckBox cb = (CheckBox)Cambro.Web.Helpers.SearchControl(Tree, "ItemCb" + mt.K);
					if (cb.Checked)
						tmpArrayList.Add(mt.K);
					else if (mt.Children.Count > 0)
						tmpArrayList.AddRange(SelectedChildren(mt.Children));
				}

			return tmpArrayList;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				SetState();
			}
			SetChildVisibility();
		}
		public void Page_Init(object o, System.EventArgs e)
		{
			MusicType mtRoot = new MusicType(1);
			Tree.Controls.Add(MusicNode(mtRoot, 0));
		}
		public void SetState()
		{
			Query q = new Query();
			q.OrderBy = MusicType.OrderBy;
			MusicTypeSet mts = new MusicTypeSet(q);
			foreach (MusicType mt in mts)
			{
					CheckBox cb = (CheckBox)Cambro.Web.Helpers.SearchControl(Tree, "ItemCb" + mt.K);
					cb.Checked = false;
				if (mt.Children.Count > 0)
				{
					HtmlGenericControl children = (HtmlGenericControl)Cambro.Web.Helpers.SearchControl(Tree, "Children" + mt.K);
					children.Style["display"] = null;
					Label lab = (Label)Cambro.Web.Helpers.SearchControl(Tree, "LabCb" + mt.K);
					lab.Style["display"] = "none";
				}
			}
			if (initialMusicTypes != null)
			{
				foreach (MusicType mt in initialMusicTypes)
				{
					CheckBox cb = (CheckBox)Cambro.Web.Helpers.SearchControl(Tree, "ItemCb" + mt.K);
					cb.Checked = true;
					if (mt.Children.Count > 0)
					{
						HtmlGenericControl children = (HtmlGenericControl)Cambro.Web.Helpers.SearchControl(Tree, "Children" + mt.K);
						children.Style["display"] = "none";
						Label lab = (Label)Cambro.Web.Helpers.SearchControl(Tree, "LabCb" + mt.K);
						lab.Style["display"] = null;
					}
				}
			}
		}
		public void SetChildVisibility()
		{
			Query q = new Query();
			q.OrderBy = MusicType.OrderBy;
			MusicTypeSet mts = new MusicTypeSet(q);
			foreach (MusicType mt in mts)
			{
				CheckBox cb = (CheckBox)Cambro.Web.Helpers.SearchControl(Tree, "ItemCb" + mt.K);
				if (mt.Children.Count > 0)
				{
					HtmlGenericControl children = (HtmlGenericControl)Cambro.Web.Helpers.SearchControl(Tree, "Children" + mt.K);
					if (cb.Checked)
					{
						children.Style["display"] = "none";
					}
					else
					{
						children.Style["display"] = null;
					}
				}
			}
		}
		Control MusicNode(MusicType mt, int level)
		{
			Control c = new Control();
			HtmlGenericControl ItemDiv = new HtmlGenericControl("div");
			ItemDiv.Style["margin-left"] = ((int)(level * 30)).ToString() + "px";
			ItemDiv.ID = "Item" + mt.K.ToString();

			CheckBox ItemCb = new CheckBox();
			ItemCb.ID = "ItemCb" + mt.K.ToString();
			ItemCb.Text = mt.Name;
			if (mt.Children.Count > 0)
			{
				ItemCb.Text += " ...";
				ItemCb.Attributes["onclick"] = "showHide('" + this.ClientID + "'," + mt.K.ToString() + ");";

			}

			ItemDiv.Controls.Add(ItemCb);
			if (mt.Children.Count > 0)
			{
				Label LabCb = new Label();
				LabCb.ID = "LabCb" + mt.K.ToString();
				LabCb.Text = " <b>un-tick <i>" + mt.GenericName.ToLower() + "</i> to show more music types</b>";
				ItemDiv.Controls.Add(LabCb);
			}
			c.Controls.Add(ItemDiv);
			if (mt.Children.Count > 0)
			{
				HtmlGenericControl ChildrenDiv = new HtmlGenericControl("div");
				ChildrenDiv.ID = "Children" + mt.K.ToString();

				foreach (MusicType mtChild in mt.Children)
				{
					ChildrenDiv.Controls.Add(MusicNode(mtChild, level + 1));
				}
				c.Controls.Add(ChildrenDiv);
			}
			return c;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
