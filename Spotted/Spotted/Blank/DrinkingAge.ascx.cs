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
	public partial class DrinkingAge : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				CountryDrop.Items.Clear();
				CountryDrop.Items.Add(new ListItem("select here...", "0"));
				Query qTop = new Query();
				qTop.Columns = new ColumnSet(Country.Columns.FriendlyName, Country.Columns.K);
				qTop.OrderBy = new OrderBy(Country.Columns.TotalEvents, OrderBy.OrderDirection.Descending);
				qTop.QueryCondition = new Q(Country.Columns.Enabled, true);
				qTop.TopRecords = 10;
				CountrySet csTop = new CountrySet(qTop);
				CountryDrop.Items.Add(new ListItem("", "0"));
				CountryDrop.Items.Add(new ListItem("- POPULAR COUNTRIES -", "0"));
				foreach (Country c in csTop)
					CountryDrop.Items.Add(new ListItem(Cambro.Misc.Utility.Snip(c.FriendlyName, 25), c.K.ToString()));

				Query qAll = new Query();
				qAll.Columns = new ColumnSet(Country.Columns.FriendlyName, Country.Columns.K);
				qAll.OrderBy = new OrderBy(Country.Columns.FriendlyName);
				qAll.QueryCondition = new Q(Country.Columns.Enabled, true);
				CountrySet csAll = new CountrySet(qAll);
				CountryDrop.Items.Add(new ListItem("", "0"));
				CountryDrop.Items.Add(new ListItem("--- ALL COUNTRIES ---", "0"));
				foreach (Country c in csAll)
					CountryDrop.Items.Add(new ListItem(Cambro.Misc.Utility.Snip(c.FriendlyName, 25), c.K.ToString()));
			}
		}
		#region Entry_Val
		protected void Entry_Val(object sender, ServerValidateEventArgs eventArgs)
		{
			try
			{
				eventArgs.IsValid = int.Parse(CountryDrop.SelectedValue) > 0 &&
					int.Parse(Year.SelectedValue) > 0 &&
					int.Parse(Month.SelectedValue) > 0 &&
					int.Parse(Day.SelectedValue) > 0 &&
					new DateTime(int.Parse(Year.SelectedValue), int.Parse(Month.SelectedValue), int.Parse(Day.SelectedValue)) > DateTime.MinValue;
			}
			catch
			{
				eventArgs.IsValid = false;
			}
		}
		#endregion
		//<asp:CustomValidator Display="dynamic" ID="DateVal" runat="server" EnableClientScript="false" OnServerValidate="Date_Val" ErrorMessage="<p>Oops! It doesn't look like you're old enough to drink. Click the Back button to return to the previous page.</p>"></asp:CustomValidator>
		//#region Date_Val
		//protected void Date_Val(object sender, ServerValidateEventArgs eventArgs)
		//{
		//    EntryVal.Validate();
		//    if (!EntryVal.IsValid)
		//    {
		//        eventArgs.IsValid = true; //make sure both error messages don't show up at once
		//        return;
		//    }

		//    try
		//    {
		//        eventArgs.IsValid = Usr.IsOfLegalDrinkingAgeInHomeCountryStatic(int.Parse(CountryDrop.SelectedValue), new DateTime(int.Parse(Year.SelectedValue), int.Parse(Month.SelectedValue), int.Parse(Day.SelectedValue)));
		//    }
		//    catch
		//    {
		//        eventArgs.IsValid = false;
		//    }
		//}
		//#endregion
		#region Continue_Click
		protected void Continue_Click(object sender, EventArgs eventArgs)
		{
			Page.Validate();

			if (Page.IsValid)
			{
				DateTime dateOfBirth = new DateTime(int.Parse(Year.SelectedValue), int.Parse(Month.SelectedValue), int.Parse(Day.SelectedValue));
				int homeCountryK = int.Parse(CountryDrop.SelectedValue);

				Prefs.Current["DateOfBirth"] = dateOfBirth.ToString();
				Prefs.Current["HomeCountryK"] = homeCountryK;

				if (Usr.IsOfLegalDrinkingAgeInHomeCountryStatic(homeCountryK, dateOfBirth))
					Prefs.Current["Drink"] = 1;
				else
					Prefs.Current.Remove("Drink");

				if (Request.QueryString["Url"] != null)
					Response.Redirect(Request.QueryString["Url"]);
				else
					Response.Redirect("/");
			}
		}
		#endregion

		#region Back_Click
		protected void Back_Click(object sender, EventArgs eventArgs)
		{
			if (Request.QueryString["Back"] != null)
				Response.Redirect(Request.QueryString["Back"]);
			else
				Response.Redirect("/pages/home");
		}
		#endregion
	}
}
