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

namespace Spotted.Pages.Countries
{
	public partial class ListContent : System.Web.UI.UserControl
	{
		protected DataList CountriesDataList, OtherCountriesDataList;

		private void Page_Load(object sender, System.EventArgs e)
		{
			Query q = new Query();
			q.NoLock = true;
			q.QueryCondition = new And(new Q(Country.Columns.Enabled, true), new Q(Country.Columns.TotalEvents, QueryOperator.GreaterThan, 0));
			q.OrderBy = new OrderBy(Country.Columns.Name);
			q.Columns = Templates.Countries.ListFlag.Columns;
			CountrySet cs = new CountrySet(q);
			CountriesDataList.ItemTemplate = this.LoadTemplate("/Templates/Countries/ListFlag.ascx");
			CountriesDataList.DataSource = cs;
			CountriesDataList.DataBind();

			Query q1 = new Query();
			q1.NoLock = true;
			q1.QueryCondition = new Q(Country.Columns.Enabled, true);
			q1.OrderBy = new OrderBy(Country.Columns.Name);
			q1.Columns = Templates.Countries.ListSmall.Columns;
			CountrySet cs1 = new CountrySet(q1);
			OtherCountriesDataList.ItemTemplate = this.LoadTemplate("/Templates/Countries/ListSmall.ascx");
			OtherCountriesDataList.DataSource = cs1;
			OtherCountriesDataList.DataBind();
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
