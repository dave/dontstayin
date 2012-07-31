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

namespace Spotted.Pages
{
	public partial class MyGalleries : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			ContainerPage.SetPageTitle("My galleries");
			BindGalleries();
		}

		#region GalleriesPanel
		void BindGalleries()
		{
			Query q = new Query();
			q.QueryCondition = new Q(Gallery.Columns.OwnerUsrK, Usr.Current.K);
			//q.TableElement=Gallery.EventVenueJoin;
			q.OrderBy = new OrderBy(Gallery.Columns.CreateDateTime, OrderBy.OrderDirection.Descending);
			GallerySet gs = new GallerySet(q);
			if (gs.Count > 0)
			{
				GalleriesDataGrid.AllowPaging = (gs.Count > GalleriesDataGrid.PageSize);
				GalleriesDataGrid.DataSource = gs;
				GalleriesDataGrid.DataBind();
			}
			else
			{
				GalleriesPanel.Visible = false;
				NoGalleriesPanel.Visible = true;
			}
		}
		public void GalleriesDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			GalleriesDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindGalleries();
		}
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
