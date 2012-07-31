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
using System.Data.SqlClient;

namespace Spotted.Admin
{
	public partial class NewObjects : AdminUserControl
	{
		protected Label NewSpotterNumberLabel, SpotterCardRequestLabel;
		protected DataList SpotterDl;
		protected DataGrid GalleriesDataGrid;
		protected Label NewEvents;
		protected Controls.Admin.BannerDataGrid LiveEmailBanners, LivePhotoBanners, LiveHotBoxes, LiveLeaderboards, LiveSkyscrapers, PaidForBanners, NotPaidForBanners;
		protected DataGrid BannedUsrDataGrid, GuestlistDataGrid;
		protected Repeater BrandRepeater;
		protected Panel PanelUnconfirmedBrands;
		protected Panel UpdateDonePanel;


		private void Page_Load(object sender, System.EventArgs e)
		{
			
			//Query qBrands = new Query();
			//qBrands.NoLock = true;
			//qBrands.TableElement = new Join(Brand.Columns.PromoterK, Promoter.Columns.K);
			//qBrands.QueryCondition = new And(
			//    Promoter.EnabledQ,
			//    new Q(Brand.Columns.PromoterStatus, Brand.PromoterStatusEnum.Unconfirmed)
			//);
			//BrandSet bsUnconfirmed = new BrandSet(qBrands);
			//if (bsUnconfirmed.Count > 0)
			//{
			//    PanelUnconfirmedBrands.Visible = true;
			//    BrandRepeater.DataSource = bsUnconfirmed;
			//    BrandRepeater.DataBind();
			//}
			//else
			//    PanelUnconfirmedBrands.Visible = false;

			//Query qVenues = new Query();
			//qVenues.TableElement = new Join(Venue.Columns.PromoterK, Promoter.Columns.K);
			//qVenues.QueryCondition = new And(
			//    Promoter.EnabledQ,
			//    new Q(Venue.Columns.PromoterStatus, Venue.PromoterStatusEnum.Unconfirmed)
			//);
			//VenueSet vsUnconfirmed = new VenueSet(qVenues);
			//if (vsUnconfirmed.Count > 0)
			//{
			//    PanelUnconfirmedVenues.Visible = true;
			//    VenueRepeater.DataSource = vsUnconfirmed;
			//    VenueRepeater.DataBind();
			//}
			//else
			//    PanelUnconfirmedVenues.Visible = false;
			

			//Query qGuestlists = new Query();
			//qGuestlists.NoLock = true;
			//qGuestlists.QueryCondition = new And(
			//    new Q(Event.Columns.DateTime, QueryOperator.GreaterThan, DateTime.Today.AddDays(-7)),
			//    new Q(Event.Columns.HasGuestlist, true)
			//    );
			//qGuestlists.OrderBy = Event.FutureEventOrder;
			//EventSet guestlists = new EventSet(qGuestlists);
			//GuestlistDataGrid.DataSource = guestlists;
			//GuestlistDataGrid.DataBind();




			Query qBannedUsr = new Query();
			qBannedUsr.QueryCondition = new Q(Usr.Columns.Banned, true);
			qBannedUsr.NoLock = true;
			qBannedUsr.TopRecords = 10;
			qBannedUsr.OrderBy = new OrderBy(Usr.Columns.BannedDateTime, OrderBy.OrderDirection.Descending);
			UsrSet usBanned = new UsrSet(qBannedUsr);
			BannedUsrDataGrid.DataSource = usBanned;
			BannedUsrDataGrid.DataBind();


			Query qNotBookedBanners = new Query();
			qNotBookedBanners.QueryCondition = new And(
				new NotQ(Banner.IsBookedQ),
				new Q(Banner.Columns.LastDay, QueryOperator.GreaterThanOrEqualTo, DateTime.Today),
				new Q(Banner.Columns.PromoterK, QueryOperator.GreaterThan, 1)
				);
			qNotBookedBanners.NoLock = true;
			qNotBookedBanners.OrderBy = new OrderBy(Banner.Columns.FirstDay, OrderBy.OrderDirection.Descending);
			BannerSet notBookedBanners = new BannerSet(qNotBookedBanners);
			NotPaidForBanners.Banners = notBookedBanners;

			Query qBookedBanners = new Query();
			qBookedBanners.QueryCondition = new And(
				Banner.IsBookedQ,
				new NotQ(Banner.IsLiveQ),
				new Q(Banner.Columns.LastDay, QueryOperator.GreaterThanOrEqualTo, DateTime.Today)
			);
			qBookedBanners.NoLock = true;
			qBookedBanners.OrderBy = new OrderBy(Banner.Columns.FirstDay, OrderBy.OrderDirection.Descending);
			BannerSet bookedBanners = new BannerSet(qBookedBanners);
			PaidForBanners.Banners = bookedBanners;

			Query qLeaderboards = new Query();
			qLeaderboards.QueryCondition = new And(
				Banner.IsLiveQ,
				new Q(Banner.Columns.Position, Banner.Positions.Leaderboard)
				);
			qLeaderboards.NoLock = true;
			qLeaderboards.OrderBy = new OrderBy(Banner.Columns.FirstDay, OrderBy.OrderDirection.Descending);
			BannerSet leaderboards = new BannerSet(qLeaderboards);
			LiveLeaderboards.Banners = leaderboards;

			Query qHotBoxes = new Query();
			qHotBoxes.QueryCondition = new And(
				Banner.IsLiveQ,
				new Q(Banner.Columns.Position, Banner.Positions.Hotbox)
				);
			qHotBoxes.NoLock = true;
			qHotBoxes.OrderBy = new OrderBy(Banner.Columns.FirstDay, OrderBy.OrderDirection.Descending);
			BannerSet hotBoxes = new BannerSet(qHotBoxes);
			LiveHotBoxes.Banners = hotBoxes;

			Query qSkyscrapers = new Query();
			qSkyscrapers.QueryCondition = new And(
				Banner.IsLiveQ,
				new Q(Banner.Columns.Position, Banner.Positions.Skyscraper)
				);
			qSkyscrapers.NoLock = true;
			qSkyscrapers.OrderBy = new OrderBy(Banner.Columns.FirstDay, OrderBy.OrderDirection.Descending);
			BannerSet skyscrapers = new BannerSet(qSkyscrapers);
			LiveSkyscrapers.Banners = skyscrapers;

			Query qPhotoBanners = new Query();
			qPhotoBanners.QueryCondition = new And(
				Banner.IsLiveQ,
				new Q(Banner.Columns.Position, Banner.Positions.PhotoBanner)
				);
			qPhotoBanners.NoLock = true;
			qPhotoBanners.OrderBy = new OrderBy(Banner.Columns.FirstDay, OrderBy.OrderDirection.Descending);
			BannerSet photoBanners = new BannerSet(qPhotoBanners);
			LivePhotoBanners.Banners = photoBanners;


			Query qEmailBanners = new Query();
			qEmailBanners.QueryCondition = new And(
				Banner.IsLiveQ,
				new Q(Banner.Columns.Position, Banner.Positions.EmailBanner)
				);
			qEmailBanners.NoLock = true;
			qEmailBanners.OrderBy = new OrderBy(Banner.Columns.FirstDay, OrderBy.OrderDirection.Descending);
			BannerSet emailBanners = new BannerSet(qEmailBanners);
			LiveEmailBanners.Banners = emailBanners;

			//	LinkP.Visible=(us.Count>0);
			//	LinkP1.Visible=(usReq.Count>0);

			//Query vq = new Query();
			//vq.NoLock = false;
			//vq.QueryCondition = new Or(new Q(Venue.Columns.IsNew, true), new Q(Venue.Columns.IsEdited, true));
			//vq.ReturnCountOnly = true;
			//VenueSet vs = new VenueSet(vq);

			//Query eq = new Query();
			//eq.NoLock = false;
			//eq.QueryCondition = new Or(new Q(Event.Columns.IsNew, true), new Q(Event.Columns.IsEdited, true));
			//eq.ReturnCountOnly = true;
			//EventSet es = new EventSet(eq);

			//if (vs.Count == 0 && es.Count == 0)
			//    NewEvents.Text = "No new events / venues.";
			//else
			//    NewEvents.Text = es.Count.ToString() + " new/edited event(s) and " + vs.Count.ToString() + " new/edited venue(s). <a href=\"/pages/venues/moderate\">Click here to view them</a>.";

			GallerySet gs = new GallerySet(new Query(new Q(Gallery.Columns.TotalPhotos, QueryOperator.NotEqualTo, Gallery.Columns.LivePhotos, true)));
			GalleriesDataGrid.DataSource = gs;
			GalleriesDataGrid.DataBind();

			this.DataBind();

		}
		
		protected string Number(int DateSpan, int StatType)
		{
			TimeSpan ts = DateTime.Today.Subtract(new DateTime(1970, 1, 1));
			int dayIndexNow = ts.Days;
			//return dayIndexNow.ToString();
			int dayMin = dayIndexNow;
			int dayMax = dayIndexNow;
			if (DateSpan == 1)
			{
				dayMin = dayIndexNow - 1;
				dayMax = dayIndexNow - 1;
			}
			if (DateSpan == 2)
			{
				dayMin = dayIndexNow - 7;
				dayMax = dayIndexNow - 1;
			}
			if (DateSpan == 3)
			{
				dayMin = dayIndexNow - 30;
				dayMax = dayIndexNow - 1;
			}
			if (DateSpan == 4)
			{
				dayMin = dayIndexNow - 365;
				dayMax = dayIndexNow - 1;
			}
			if (DateSpan == 5)
			{
				dayMin = 0;
				dayMax = dayIndexNow;
			}
			string tag = "100";
			if (StatType == 2)
				tag = "104";
			if (StatType == 3)
				tag = "105";


			try
			{
				string a = "";
				SqlConnection conn = new SqlConnection("Data Source=(local); Initial Catalog=db_livestats; Integrated Security=SSPI;");
				try
				{
					SqlCommand myCommand = new SqlCommand("select sum(total) from server0000000003_history where crcid=24 and tag=" +
						tag + " and day>=" + dayMin.ToString() + " and day<=" + dayMax.ToString(), conn);
					myCommand.Connection.Open();
					SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
					dr.Read();
					a = ((Int64)dr[0]).ToString("###,##0");
					dr.Close();
				}
				finally
				{
					conn.Close();
					conn.Dispose();
				}
				return a;

			}
			catch
			{
				return "n/a";
			}


		}
	}
}
