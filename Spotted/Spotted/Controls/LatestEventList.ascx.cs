using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Controls
{
	public partial class LatestEventList : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			if (!hasParent || 
				(CurrentCountry != null && CurrentCountry.K == 224) || 
				(CurrentGroup != null && !CurrentGroup.HasEvents))
			{
				this.Visible = false;
				return;
			}

			NextEventList.Future = true;
			PastEventList.Future = false;

			if (CurrentCountry != null)
			{
				
				NextEventList.ParentObjectType = Model.Entities.ObjectType.Country;
				NextEventList.Filter = new Q(Place.Columns.CountryK, CurrentCountry.K);
				NextEventList.Join = Event.PlaceAllJoin;
				NextEventList.Calendar = CurrentCountry;
				NextEventList.Size = 10;
				
				
				Q photosQ = new Q(true);
				PastEventList.OnlyPhotos = false;
				PastEventList.ParentObjectType = Model.Entities.ObjectType.Country;
				if (Country.FilterK == 0 || Country.Current.Mature)
				{
					photosQ = new Or(
						new Q(Event.Columns.HasSpotter, true),
						new Q(Event.Columns.LivePhotos, QueryOperator.GreaterThan, 0));
					PastEventList.OnlyPhotos = true;
				}
				PastEventList.Filter = new And(
					new Q(Place.Columns.CountryK, CurrentCountry.K),
					photosQ
				);
				PastEventList.Join = Event.PlaceAllJoin;
				PastEventList.Calendar = CurrentCountry;
				PastEventList.Size = 10;
			}
			else if (CurrentPlace != null)
			{
				NextEventList.ParentObjectType = Model.Entities.ObjectType.Place;
				NextEventList.Filter = new Q(Place.Columns.K, CurrentPlace.K);
				NextEventList.Join = Event.PlaceAllJoin;
				NextEventList.Size = 7;
				NextEventList.Calendar = CurrentPlace;

				PastEventList.ParentObjectType = Model.Entities.ObjectType.Place;
				PastEventList.Filter = new Q(Place.Columns.K, CurrentPlace.K);
				PastEventList.Join = Event.PlaceAllJoin;
				PastEventList.Size = 7;
				PastEventList.Calendar = CurrentPlace;
			}
			else if (CurrentVenue != null)
			{
				NextEventList.ParentObjectType = Model.Entities.ObjectType.Venue;
				NextEventList.Filter = new Q(Event.Columns.VenueK, CurrentVenue.K);
				NextEventList.Size = 7;
				NextEventList.Calendar = CurrentVenue;
				NextEventList.AddEventUrl = CurrentVenue.AddEventLink;

				PastEventList.ParentObjectType = Model.Entities.ObjectType.Venue;
				PastEventList.Filter = new Q(Event.Columns.VenueK, CurrentVenue.K);
				PastEventList.Size = 7;
				PastEventList.Calendar = CurrentVenue;
				PastEventList.AddEventUrl = CurrentVenue.AddEventLink;
			}
			else if (CurrentBrand != null)
			{
				NextEventList.ParentObjectType = Model.Entities.ObjectType.Brand;
				NextEventList.Filter = new Q(EventBrand.Columns.BrandK, CurrentBrand.K);
				NextEventList.Size = 7;
				NextEventList.Join = Event.LeftBrandJoin;
				NextEventList.Calendar = CurrentBrand;

				PastEventList.ParentObjectType = Model.Entities.ObjectType.Brand;
				PastEventList.Filter = new Q(EventBrand.Columns.BrandK, CurrentBrand.K);
				PastEventList.Size = 7;
				PastEventList.Join = Event.LeftBrandJoin;
				PastEventList.Calendar = CurrentBrand;
			}
			else if (CurrentGroup != null)
			{
				NextEventList.ParentObjectType = Model.Entities.ObjectType.Group;
				NextEventList.Filter = new Q(GroupEvent.Columns.GroupK, CurrentGroup.K);
				NextEventList.Size = 7;
				NextEventList.Join = Event.LeftGroupJoin;
				NextEventList.Calendar = CurrentGroup;
				
				PastEventList.ParentObjectType = Model.Entities.ObjectType.Group;
				PastEventList.Filter = new Q(GroupEvent.Columns.GroupK, CurrentGroup.K);
				PastEventList.Size = 7;
				PastEventList.Join = Event.LeftGroupJoin;
				PastEventList.Calendar = CurrentGroup;
				
			}

			NextEventList.Bind();
			PastEventList.Bind();

		}

		Country CurrentCountry { get { return Parent as Country; } }
		Place   CurrentPlace   { get { return Parent as Place; } }
		Venue   CurrentVenue   { get { return Parent as Venue; } }
		Brand   CurrentBrand   { get { if (Parent is Brand) return Parent as Brand; else if (Parent is Group && ((Group)Parent).BrandK > 0 && ((Group)Parent).Brand != null) return ((Group)Parent).Brand; else return null; } }
		Group	CurrentGroup   { get { if (Parent is Group && ((Group)Parent).BrandK > 0) return null; else return Parent as Group; } }

		bool hasParent
		{
			get
			{
				return CurrentCountry != null ||
					CurrentPlace != null ||
					CurrentVenue != null ||
					CurrentBrand != null ||
					CurrentGroup != null;
			}
		}

		public object Parent
		{
			get
			{
				if (this.NamingContainer is Latest)
					return ((Latest)this.NamingContainer).Parent;
				else
					return null;
			}
		}

		#region ContainerPage
		public Spotted.Master.DsiPage ContainerPage
		{
			get
			{
				return (Spotted.Master.DsiPage)Page;
			}
		}
		#endregion


	}
}
