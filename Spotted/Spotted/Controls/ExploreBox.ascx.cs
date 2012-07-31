using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Controls
{
	public partial class ExploreBox : System.Web.UI.UserControl
	{

		//<h1 class="TabHolder" runat="server" visible="false">
		//    <a href="/" class="TabbedHeading Selected" id="ExploreBoxExploreHeader" onclick="exploreBoxTabClick(0);return false;">Explore Don't Stay In</a>
		//    <a href="/" class="TabbedHeading"          id="ExploreBoxFindHeader" onclick="exploreBoxTabClick(1);return false;">Find your photo</a>
		//</h1>

		//function exploreBoxTabClick(tabID)
		//{
		//    Sys.Application.addHistoryPoint({ Home_ExploreTab: tabID }, tabID == 0 ? "Explore" : "Find your photo");
		//}
		//function exploreBoxTabNavigate(sender, e)
		//{
		//    var tabID = e.get_state().Home_ExploreTab;
		//    document.getElementById("<%= ExploreBoxExploreHolder.ClientID %>").style.display = (tabID == 1 ? "none" : "");
		//    document.getElementById("ExploreBoxFindHolder").style.display = (tabID == 1 ? "" : "none");

		//    document.getElementById("ExploreBoxExploreHeader").className = (tabID == 1 ? "TabbedHeading" : "TabbedHeading Selected");
		//    document.getElementById("ExploreBoxFindHeader").className = (tabID == 1 ? "TabbedHeading Selected" : "TabbedHeading");

		//}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			JQuery.Include(Page, "selectboxes", "selectboxes");

			int defaultCountryK = 0;
			if (Usr.Current == null)
			{
				if (Visit.Current.CountryK > 0)
					defaultCountryK = Visit.Current.CountryK;
			}
			else
			{
				defaultCountryK = Usr.Current.Home.CountryK;
			}
			bool foundDefault = false;



			//http://www.texotela.co.uk/code/jquery/select/
			StringBuilder sb = new StringBuilder();
			int maxLength = 20;
			append(sb, "Select a country...", "0");
			//CountryDropDown.Items.Add(new ListItem("Select a country...", "0"));
			Query qTop = new Query();
			qTop.Columns = new ColumnSet(Country.Columns.FriendlyName, Country.Columns.K);
			qTop.OrderBy = new OrderBy(Country.Columns.TotalEvents, OrderBy.OrderDirection.Descending);
			qTop.QueryCondition = new Q(Country.Columns.Enabled, true);
			qTop.TopRecords = 10;
			qTop.CacheDuration = TimeSpan.FromDays(1);
			CountrySet csTop = new CountrySet(qTop);
			append(sb, "", "0");
			append(sb, "--- TOP COUNTRIES ---", "0");
			//CountryDropDown.Items.Add(new ListItem("", "0"));
			//CountryDropDown.Items.Add(new ListItem("--- TOP COUNTRIES ---", "0"));
			foreach (Country c in csTop)
			{
				bool thisIsDefault = false;
				if (c.K == defaultCountryK)
				{
					thisIsDefault = true;
					foundDefault = true;
				}

				append(sb, c.FriendlyName.TruncateWithDots(maxLength), c.K.ToString(), thisIsDefault);
				//CountryDropDown.Items.Add(new ListItem(c.FriendlyName.TruncateWithDots(maxLength), c.K.ToString()));
			}
			Query qAll = new Query();
			qAll.Columns = new ColumnSet(Country.Columns.FriendlyName, Country.Columns.K);
			qAll.OrderBy = new OrderBy(Country.Columns.FriendlyName);
			qAll.QueryCondition = new And(new Q(Country.Columns.Enabled, true), new StringQueryCondition("(SELECT COUNT(*) FROM [Place] WHERE [Place].[Enabled] = 1 AND [Place].[CountryK] = [Country].[K]) > 0"));
			qAll.CacheDuration = TimeSpan.FromDays(1);
			CountrySet csAll = new CountrySet(qAll);
			append(sb, "", "0");
			append(sb, "--- ALL COUNTRIES ---", "0");
			//CountryDropDown.Items.Add(new ListItem("", "0"));
			//CountryDropDown.Items.Add(new ListItem("--- ALL COUNTRIES ---", "0"));
			foreach (Country c in csAll)
			{
				bool thisIsDefault = false;
				if (!foundDefault && c.K == defaultCountryK)
				{
					thisIsDefault = true;
					foundDefault = true;
				}

				append(sb, c.FriendlyName.TruncateWithDots(maxLength), c.K.ToString(), thisIsDefault);
				//CountryDropDown.Items.Add(new ListItem(c.FriendlyName.TruncateWithDots(maxLength), c.K.ToString()));
			}
			ExploreCountryPh.Controls.Add(new LiteralControl(sb.ToString()));

			bindPlaceDrop(defaultCountryK);
			ExploreDefaultCountryPh.Controls.Add(new LiteralControl(defaultCountryK.ToString()));




			
		}

		

		void bindPlaceDrop(int SelectedCountryK)
		{
			StringBuilder sb = new StringBuilder();
			int maxLength = 30;
			if (SelectedCountryK == 0)
			{
				append(sb, "Select a country first...", "0");
				//TownDropDown.Items.Add(new ListItem("Select a country first...", "0"));
				//return;
			}
			else
			{
				Country country = new Country(SelectedCountryK);

				Query qTop = new Query();
				qTop.Columns = new ColumnSet(Place.Columns.Name, Place.Columns.K, Place.LinkColumns);
				qTop.TopRecords = 10;
				qTop.QueryCondition = new And(new Q(Place.Columns.CountryK, SelectedCountryK), new Q(Place.Columns.Enabled, true));
				qTop.OrderBy = new OrderBy(Place.Columns.TotalEvents, OrderBy.OrderDirection.Descending);
				PlaceSet psTop = new PlaceSet(qTop);
				if (psTop.Count == 0)
				{
					append(sb, "No towns in our database for this country", "0");
					//TownDropDown.Items.Add(new ListItem("No towns in our database for this country", "0"));
				}
				else
				{
					append(sb, "Towns in " + country.FriendlyName + "...", "0");
					append(sb, "", "0");
					append(sb, "--- TOP TOWNS ---", "0");
					//TownDropDown.Items.Add(new ListItem("Select a town...", "0"));
					//TownDropDown.Items.Add(new ListItem("", "0"));
					//TownDropDown.Items.Add(new ListItem("--- TOP TOWNS ---", "0"));
					foreach (Place p in psTop)
					{
						append(sb, p.Name.TruncateWithDots(maxLength), p.Url());
						//TownDropDown.Items.Add(new ListItem(p.Name.TruncateWithDots(maxLength), p.Url()));
					}

					Query qAll = new Query();
					qAll.Columns = new ColumnSet(Place.Columns.Name, Place.Columns.K, Place.LinkColumns);
					qAll.OrderBy = new OrderBy(Place.Columns.Name);
					qAll.QueryCondition = new And(new Q(Place.Columns.CountryK, SelectedCountryK), new Q(Place.Columns.Enabled, true));
					PlaceSet psAll = new PlaceSet(qAll);
					append(sb, "", "0");
					append(sb, "--- ALL TOWNS ---", "0");
					//TownDropDown.Items.Add(new ListItem("", "0"));
					//TownDropDown.Items.Add(new ListItem("--- ALL TOWNS ---", "0"));
					foreach (Place p in psAll)
					{
						append(sb, p.Name.TruncateWithDots(maxLength), p.Url());
						//TownDropDown.Items.Add(new ListItem(p.Name.TruncateWithDots(maxLength), p.Url()));
					}
				}
			}
			ExploreTownPh.Controls.Add(new LiteralControl(sb.ToString()));
		}

		void append(StringBuilder sb, string name, string value, bool selected)
		{
			sb.Append("<option ");
			if (selected)
				sb.Append("selected ");
			sb.Append("value=\"");
			sb.Append(value);
			sb.Append("\">");
			sb.Append(name);
			sb.Append("</option>");
			sb.AppendLine("");
		}

		void append(StringBuilder sb, string name, string value)
		{
			append(sb, name, value, false);
		}


	}
}
