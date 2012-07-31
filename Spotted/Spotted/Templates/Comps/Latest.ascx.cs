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

namespace Spotted.Templates.Comps
{
	public partial class Latest : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

		}

		public static ColumnSet Columns
		{
			get
			{

				return new ColumnSet(
					Comp.Columns.K,
					Comp.Columns.EventK,
					Comp.Columns.BrandK,
					Comp.Columns.Pic,
					Comp.Columns.LinkType,
					Comp.Columns.Winners,
					Comp.Columns.Prize
				);
			}
		}

		public static TableElement PerformJoins(TableElement tIn)
		{
			if (tIn == null)
				tIn = new TableElement(TablesEnum.Comp);
			TableElement t = tIn;
			return t;
		}

		protected string Details
		{
			get
			{
				if (CurrentComp.LinkType.Equals(Comp.LinkTypes.Event) && CurrentComp.Event != null)
				{
					bool showVenue = true;
					bool showPlace = true;
					bool showEvent = true;
					
					object parent = null;

					if (NamingContainer.NamingContainer.NamingContainer.NamingContainer is Spotted.Controls.Latest)
						parent = ((Spotted.Controls.Latest)(NamingContainer.NamingContainer.NamingContainer.NamingContainer)).Parent;

					if (parent != null && parent is Place)
					{
						showPlace = false;
					}
					else if (parent != null && parent is Venue)
					{
						showPlace = false;
						showVenue = false;
					}
					else if (parent != null && parent is Event)
					{
						showPlace = false;
						showVenue = false;
						showEvent = false;
					}

					if (showEvent)
						return "<br><small>" + CurrentComp.Event.FriendlyHtml(showVenue, showPlace, true, false) + "</small>";
					else
						return "";
				}
				else if (CurrentComp.LinkType.Equals(Comp.LinkTypes.Brand) && CurrentComp.Brand != null)
				{
					return "<br><small>Prize donated by <a href=\"" + CurrentComp.Brand.Url() + "\">" + CurrentComp.Brand.Name + "</a></small>";
				}
				else
					return "";
			}


		}
		#region CurrentComp
		public Comp CurrentComp
		{
			get
			{
				if (currentComp == null)
					currentComp = ((Comp)((DataListItem)NamingContainer).DataItem);
				return currentComp;
			}
			set
			{
				currentComp = value;
			}
		}
		private Comp currentComp;
		#endregion

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
