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

namespace Spotted.Blank
{
	public partial class Invoice : BlankUserControl
	{
		protected DataGrid ItemsDataGrid;

		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (CurrentInvoice.PromoterK == 0)
				throw new Exception("no promoter!");

			if (!Usr.Current.IsAdmin)
			{
				if (!Usr.Current.IsPromoterK(CurrentInvoice.PromoterK))
				{
					throw new Exception("You can't view this invoice!");
				}
			}

			ItemsDataGrid.DataSource = CurrentInvoice.Items;

			this.DataBind();
		}
		#region CurrentInvoice
		public Bobs.Invoice CurrentInvoice
		{
			get
			{
				if (currentInvoice == null && ContainerPage.Url[0].IsInt)
					currentInvoice = new Bobs.Invoice(ContainerPage.Url[0]);
				return currentInvoice;
			}
			set
			{
				currentInvoice = value;
			}
		}
		private Bobs.Invoice currentInvoice;
		#endregion
	}
}
