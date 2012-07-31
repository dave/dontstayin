using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Pages
{
	[ClientScript]
	public partial class PlacesIVisit : DsiUserControl
	{
		public PlacesIVisit()
		{
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			uiSaveButton.Click += new EventHandler(uiSaveButton_Click);
			Prefs.Current["SeenVisit"] = "1";
				
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Usr.KickUserIfNotLoggedIn("You must be logged in to use this page.");
			if (!IsPostBack)
			{
				this.uiPlacesChooser.SelectedPlaces = Usr.Current.PlacesVisit(new ColumnSet(Place.Columns.K, Place.Columns.RegionAbbreviation, Place.Columns.CountryK, Place.Columns.Name, Place.Columns.LatitudeDegreesNorth, Place.Columns.LongitudeDegreesWest), -1).ToList();
				this.uiSaveButton.Attributes["disabled"] = "true";
			}
		}

		void uiSaveButton_Click(object sender, EventArgs e)
		{
			if (!uiPlacesChooser.SelectedPlaceKs.Any())
			{
				throw new Exception("You cannot visit nowhere. This should not have been an available option from the site");
			}
			using (var transaction = new Transaction())
			{
				Delete delete = new Delete(TablesEnum.UsrPlaceVisit, new Q(UsrPlaceVisit.Columns.UsrK, Usr.Current.K));
				delete.Run(transaction);
				foreach (var item in this.uiPlacesChooser.SelectedPlaceKs)
				{
					UsrPlaceVisit utv = new UsrPlaceVisit();
					utv.UsrK = Usr.Current.K;
					utv.PlaceK = item;
					utv.Update(transaction);
				}
				
				transaction.Commit();
				Usr.Current.UpdatePlacesVisitCount(true);
			}
			this.uiSaveButton.Attributes["disabled"] = "true";


		}
		 
		
	}
}
