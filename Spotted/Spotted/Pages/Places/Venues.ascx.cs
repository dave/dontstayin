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

namespace Spotted.Pages.Places
{
	public partial class Venues : DsiUserControl
	{

		protected Panel AllVenuesPanel,
			LargeRegularVenuePanel,
			MediumRegularVenuePanel,
			SmallRegularVenuePanel,
			NotRegularVenuePanel;
		protected DataList LargeRegularVenueDataList,
			MediumRegularVenueDataList,
			SmallRegularVenueDataList,
			NotRegularVenueDataList;
		protected HtmlAnchor AllVenuesPlaceLink;
		protected HtmlContainerControl PageHeadingAllVenues;

		Place CurrentPlace
		{
			get
			{
				return ContainerPage.Url.ObjectFilterPlace;
			}
		}

		protected HtmlGenericControl AllVenuesPanelNoVenues;
		protected void Page_Load(object sender, EventArgs e)
		{
			AllVenuesPlaceLink.InnerText = CurrentPlace.Name;
			AllVenuesPlaceLink.HRef = CurrentPlace.Url();
			SetPageTitle("Venues in " + CurrentPlace.Name);
			PageHeadingAllVenues.InnerText = "Venues in " + CurrentPlace.Name;

			if (CurrentPlace.Venues.Count > 0)
			{
				Query LargeVenuesQ = new Query();
				LargeVenuesQ.QueryCondition = new And(
					new Q(Venue.Columns.PlaceK, CurrentPlace.K),
					new Q(Venue.Columns.RegularEvents, true),
					new Q(Venue.Columns.Capacity, QueryOperator.GreaterThanOrEqualTo, 800)
				);
				LargeVenuesQ.OrderBy = new OrderBy(Venue.Columns.Name);
				VenueSet LargeVenueSet = new VenueSet(LargeVenuesQ);
				if (LargeVenueSet.Count > 0)
				{
					if (LargeVenueSet.Count <= 18)
						LargeRegularVenueDataList.ItemTemplate = this.LoadTemplate("/Templates/Venues/PlaceVenuesList.ascx");
					else
					{
						LargeRegularVenueDataList.ItemTemplate = this.LoadTemplate("/Templates/Venues/PlaceVenuesListSmall.ascx");
						LargeRegularVenueDataList.RepeatColumns = 5;
						LargeRegularVenueDataList.CellPadding = 3;
						LargeRegularVenueDataList.ItemStyle.VerticalAlign = VerticalAlign.Top;
						LargeRegularVenueDataList.ItemStyle.Width = Unit.Percentage(20.0);
					}
					LargeRegularVenueDataList.DataSource = LargeVenueSet;
					LargeRegularVenueDataList.DataBind();
				}
				else
					LargeRegularVenuePanel.Visible = false;

				Query MediumVenuesQ = new Query();
				MediumVenuesQ.QueryCondition = new And(
					new Q(Venue.Columns.PlaceK, CurrentPlace.K),
					new Q(Venue.Columns.RegularEvents, true),
					new Q(Venue.Columns.Capacity, QueryOperator.GreaterThanOrEqualTo, 300),
					new Q(Venue.Columns.Capacity, QueryOperator.LessThan, 800)
					);
				MediumVenuesQ.OrderBy = new OrderBy(Venue.Columns.Name);
				VenueSet MediumVenueSet = new VenueSet(MediumVenuesQ);
				if (MediumVenueSet.Count > 0)
				{
					if (MediumVenueSet.Count <= 18)
						MediumRegularVenueDataList.ItemTemplate = this.LoadTemplate("/Templates/Venues/PlaceVenuesList.ascx");
					else
					{
						MediumRegularVenueDataList.ItemTemplate = this.LoadTemplate("/Templates/Venues/PlaceVenuesListSmall.ascx");
						MediumRegularVenueDataList.RepeatColumns = 5;
						MediumRegularVenueDataList.CellPadding = 3;
						MediumRegularVenueDataList.ItemStyle.VerticalAlign = VerticalAlign.Top;
						MediumRegularVenueDataList.ItemStyle.Width = Unit.Percentage(20.0);
					}
					MediumRegularVenueDataList.DataSource = MediumVenueSet;
					MediumRegularVenueDataList.DataBind();
				}
				else
					MediumRegularVenuePanel.Visible = false;

				Query SmallVenuesQ = new Query();
				SmallVenuesQ.QueryCondition = new And(
					new Q(Venue.Columns.PlaceK, CurrentPlace.K),
					new Q(Venue.Columns.RegularEvents, true),
					new Q(Venue.Columns.Capacity, QueryOperator.LessThan, 300)
					);
				SmallVenuesQ.OrderBy = new OrderBy(Venue.Columns.Name);
				VenueSet SmallVenueSet = new VenueSet(SmallVenuesQ);
				if (SmallVenueSet.Count > 0)
				{
					if (SmallVenueSet.Count <= 18)
						SmallRegularVenueDataList.ItemTemplate = this.LoadTemplate("/Templates/Venues/PlaceVenuesList.ascx");
					else
					{
						SmallRegularVenueDataList.ItemTemplate = this.LoadTemplate("/Templates/Venues/PlaceVenuesListSmall.ascx");
						SmallRegularVenueDataList.RepeatColumns = 5;
						SmallRegularVenueDataList.CellPadding = 3;
						SmallRegularVenueDataList.ItemStyle.VerticalAlign = VerticalAlign.Top;
						SmallRegularVenueDataList.ItemStyle.Width = Unit.Percentage(20.0);
					}
					SmallRegularVenueDataList.DataSource = SmallVenueSet;
					SmallRegularVenueDataList.DataBind();
				}
				else
					SmallRegularVenuePanel.Visible = false;

				Query NotRegularVenuesQ = new Query();
				NotRegularVenuesQ.QueryCondition = new And(
					new Q(Venue.Columns.PlaceK, CurrentPlace.K),
					new Q(Venue.Columns.RegularEvents, false)
				);
				NotRegularVenuesQ.OrderBy = new OrderBy(Venue.Columns.Name);
				VenueSet NotRegularVenueSet = new VenueSet(NotRegularVenuesQ);
				if (NotRegularVenueSet.Count > 0)
				{
					NotRegularVenueDataList.ItemTemplate = this.LoadTemplate("/Templates/Venues/PlaceVenuesListSmall.ascx");
					NotRegularVenueDataList.RepeatColumns = 5;
					NotRegularVenueDataList.CellPadding = 3;
					NotRegularVenueDataList.ItemStyle.VerticalAlign = VerticalAlign.Top;
					NotRegularVenueDataList.ItemStyle.Width = Unit.Percentage(20.0);
					NotRegularVenueDataList.DataSource = NotRegularVenueSet;
					NotRegularVenueDataList.DataBind();
				}
				else
					NotRegularVenuePanel.Visible = false;
			}
			else
				AllVenuesPanelNoVenues.Visible = true;

		}
	}
}
