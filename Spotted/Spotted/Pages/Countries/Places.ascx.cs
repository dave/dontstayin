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
using System.Text;

namespace Spotted.Pages.Countries
{
	public partial class Places : DsiUserControl
	{
		protected HtmlContainerControl PageHeadingNoPlace;
		protected LinkButton SizeOrderLink, NameOrderLink, LatitudeOrderLink, LongitudeOrderLink, RegionOrderLink, EventOrderLink;
		protected DataList PlacesDataList;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (CurrentCountry.RegionName.Length > 0)
			{
				RegionOrderLink.Text = CurrentCountry.RegionName.ToLower();
			}
			string order = "Name";
			if (Prefs.Current["AllPlacesPlaceOrder"].Exists)
				order = Prefs.Current["AllPlacesPlaceOrder"];
			UpdateLinks(order);
			BindPlacesList(order);
		}

		void UpdateLinks(string order)
		{
			SizeOrderLink.Enabled = !order.Equals("Size");
			NameOrderLink.Enabled = !order.Equals("Name"); ;
			LatitudeOrderLink.Enabled = !order.Equals("Latitude");
			LongitudeOrderLink.Enabled = !order.Equals("Longitude");
			RegionOrderLink.Enabled = !order.Equals("Region");
			EventOrderLink.Enabled = !order.Equals("Event");
		}


		#region BindPlacesList
		protected HtmlImage NoPlaceSelectedFlagImg;
		protected Panel NoPlaceSelectedHomeCountryPanel, NoPlaceSelectedCountryPanel;
		protected HtmlAnchor NoPlaceSelectedCountryLink, NoPlaceSelectedFlagAnchor;
		#region CurrentCountry
		public Country CurrentCountry
		{
			get
			{
				if (placeListCountry == null)
				{
					if (ContainerPage.Url.HasCountryObjectFilter)
						placeListCountry = ContainerPage.Url.ObjectFilterCountry;
					else
						placeListCountry = Country.Current;
				}
				return placeListCountry;
			}
			set
			{
				placeListCountry = value;
			}
		}
		private Country placeListCountry;
		#endregion
		void BindPlacesList(string order)
		{

			OrderBy o = new OrderBy(Bobs.Place.Columns.Name, OrderBy.OrderDirection.Ascending);
			if (order.Equals("Size"))
				o = new OrderBy(Bobs.Place.Columns.Population, OrderBy.OrderDirection.Descending);
			else if (order.Equals("Latitude"))
				o = new OrderBy(Bobs.Place.Columns.LatitudeDegreesNorth, OrderBy.OrderDirection.Descending);
			else if (order.Equals("Longitude"))
				o = new OrderBy(Bobs.Place.Columns.LongitudeDegreesWest, OrderBy.OrderDirection.Descending);
			else if (order.Equals("Region"))
				o = Bobs.Region.RegionOrder;
			else if (order.Equals("Event"))
				o = new OrderBy(Bobs.Place.Columns.TotalEvents, OrderBy.OrderDirection.Descending);

			NoPlaceSelectedFlagImg.Src = CurrentCountry.FlagUrl();

			PageHeadingNoPlace.InnerText = PageHeadingNoPlace.InnerText.Replace("???", CurrentCountry.FriendlyName);
			ContainerPage.SetPageTitle("Places in " + CurrentCountry.FriendlyName);

			NoPlaceSelectedHomeCountryPanel.Visible = (!ContainerPage.Url.HasCountryObjectFilter);
			NoPlaceSelectedCountryPanel.Visible = (ContainerPage.Url.HasCountryObjectFilter);

			NoPlaceSelectedFlagAnchor.HRef = CurrentCountry.Url();

			if (ContainerPage.Url.HasCountryObjectFilter)
			{
				NoPlaceSelectedCountryLink.InnerText = CurrentCountry.FriendlyName;
				NoPlaceSelectedCountryLink.HRef = CurrentCountry.Url();
			}

			PlaceSet ts = new PlaceSet(
				new Query(
					new And(
					new Q(Place.Columns.Enabled, true),
					new Q(Place.Columns.CountryK, CurrentCountry.K)
					),
					o
				)
			);
			PlacesDataList.DataSource = ts;
			PlacesDataList.DataBind();

			//	PlaceSet tsVisit = Usr.Current.PlacesVisit;
			//	foreach (Place t in tsVisit)
			//	{
			//		Cob c = this.CurrentCob;
			//		HtmlAnchor a = new HtmlAnchor();
			//		a.HRef=t.Url();
			//		a.InnerText=t.Name;
			//	}
		}
		#endregion
		#region PlacesDataList_OnItemDataBound
		public void PlacesDataList_OnItemDataBound(object o, DataListItemEventArgs e)
		{
			StringBuilder sb = new StringBuilder();

			Place t = (Place)e.Item.DataItem;

			if (t.TotalEvents == 0)
				sb.Append("<small>");
			sb.Append("<a href=\"");
			sb.Append(t.Url());
			sb.Append("\">");
			sb.Append(t.NamePlainRegion);
			sb.Append("</a>");
			if (t.TotalEvents > 0)
			{
				sb.Append(" <small>(");
				sb.Append(t.TotalEvents.ToString("#,##0"));
				sb.Append(")");
			}
			sb.Append("</small>");

			e.Item.Controls.Add(new LiteralControl(sb.ToString()));
		}
		#endregion
		#region ReOrder
		public void ReOrder(object o, CommandEventArgs e)
		{
			Prefs.Current["AllPlacesPlaceOrder"] = e.CommandArgument.ToString();
			BindPlacesList(e.CommandArgument.ToString());
			UpdateLinks(e.CommandArgument.ToString());
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
