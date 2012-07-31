using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using Bobs;

namespace Spotted.Admin
{
	public partial class MixmagListings : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				DateTime dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

				IssueDrop.Items.Clear();
				for (int i = -1; i <= 5; i++)
				{
					IssueDrop.Items.Add(new ListItem(dt.AddMonths(i).ToString("MM/yyyy"), dt.AddMonths(i).Ticks.ToString()));
				}

				ZoneDrop.Items.Clear();
				foreach (Model.Entities.Place.MixmagZone zone in Enum.GetValues(typeof(Model.Entities.Place.MixmagZone)))
				{
					ZoneDrop.Items.Add(new ListItem(zone.ToString(), ((int)zone).ToString()));
				}
			}
		}
		protected void Generate(object sender, EventArgs eventArgs)
		{

			DateTime issueDate = new DateTime(long.Parse(IssueDrop.SelectedValue));

			DateTime min = new DateTime(issueDate.AddMonths(-1).Year, issueDate.AddMonths(-1).Month, 10);
			DateTime max = new DateTime(issueDate.AddMonths(1).Year, issueDate.AddMonths(1).Month, 1).AddDays(-1);

			Query q = new Query();
			q.TableElement = Bobs.Event.VenueAllJoin;
			q.QueryCondition = new And(
				new Q(Bobs.Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, min),
				new Q(Bobs.Event.Columns.DateTime, QueryOperator.LessThanOrEqualTo, max),
				placeQ
				);
			EventSet es = new EventSet(q);

			Response.Clear();
			Response.ContentType = "application/csv";
			foreach (Bobs.Event e in es)
			{
				Response.Write("");
				Response.Write("[venue id?]" + "\t");//MMV09299
				Response.Write(e.Venue.Place.Name + "\t");//London
				Response.Write(e.Venue.Place.Name + "\t");//london
				Response.Write(e.Venue.Capacity.ToString("#,##0") + "\t");//[capacity]
				Response.Write("[venue address 1]" + "\t");//Old QueenÕs Head
				Response.Write("[venue address 2]" + "\t");//44 Essex Road, N1
				Response.Write("[event id?]" + "\t");//MMN09724
				Response.Write((e.Brands.Count > 0 ? e.Brands[0].Name.TruncateWithDots(30) : e.Name.TruncateWithDots(30)) + "\t");//Apocalypso
				Response.Write(e.DateTime.ToString("dddd") + "\t");//Friday
				Response.Write("[start-time]" + "\t");//8pm
				Response.Write("[end-time]" + "\t");//2am
				Response.Write("[price]" + "\t");//£4
				Response.Write("[phone]" + "\t");//020 7354 9993
				Response.Write("[???]" + "\t");//[???]
				Response.Write("[web address]" + "\t");//www.theoldqueenshead.com
				Response.Write("[main brand description]" + "\t");//Electro and indie dance at this party boozer.
				Response.Write("[mixmag offer]" + "\t");//HALF PRICE
				Response.Write(e.DateTime.ToString("dd/mm/yyyy") + "\t");//29/05/2009
				Response.Write(e.DateTime.ToString("d/m") + "\t");//29/5
				Response.Write(e.ShortDetailsHtml.Replace("\r", string.Empty).Replace("\n", string.Empty).TruncateWithDots(40) + "\t");//Dekker & Johan
				Response.Write("[???]" + "\t");//[???]
				Response.Write("[???]" + "\t");//[???]
				Response.Write("[???]" + "\t");//[???]
				Response.Write("[locality?]");//Angel
				Response.Write("\n");
			}
			Response.Flush();
			Response.Close();
			Response.End();
		}

		Q placeQ
		{
			get
			{
				return new Q(Bobs.Venue.Columns.PlaceK, 1); 
			}
		}
	}
}
