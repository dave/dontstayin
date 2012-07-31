using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using System.Collections;

namespace Spotted.Pages
{
	public partial class Out : DsiUserControl
	{

		UsrEventAttendedSet get(bool forceAllCountries)
		{
			Q sexQ = 
				SexTypeFromUrl == SexType.Boys ? new Q(Usr.Columns.IsMale, true) : 
				SexTypeFromUrl == SexType.Girls ? new Q(Usr.Columns.IsMale, false) : 
				new Q(true);

			Q countryQ = new Q(true);

			if (!forceAllCountries && LocationTypeFromUrl == LocationType.Country)
				countryQ = new Q(Place.Columns.CountryK, CountryFromUrl.K);


			Query q = new Query();
			q.TableElement = new Join(new Join(Event.UsrAttendedJoin, Venue.Columns.K, Event.Columns.VenueK), Place.Columns.K, Venue.Columns.PlaceK);
			q.QueryCondition = new And(
				new Q(Usr.Columns.IsSkeleton, false),
				new Q(Usr.Columns.ExDirectory, false),
				new Q(Usr.Columns.PhotosMeCount, QueryOperator.GreaterThan, 0),
				sexQ,
				countryQ,
				Event.FutureEventsQueryCondition,
				new Q(Event.Columns.DateTime, QueryOperator.LessThan, DateTime.Today.AddDays(7)));
			q.OrderBy = new OrderBy(Usr.Columns.K);
			q.Columns = new ColumnSet(UsrEventAttended.Columns.UsrK);
			q.CacheDuration = new TimeSpan(1, 0, 0);
			return new UsrEventAttendedSet(q);
		}

		public enum SexType
		{
			NoneSpecified,
			Boys,
			Girls, 
			All
		}
		public enum LocationType
		{
			NoneSpecified,
			Worldwide,
			Country
		}


		protected void Page_Load(object sender, EventArgs e)
		{
			Bobs.Log.Increment(Model.Entities.Log.Items.WhosGoingOutPage);
			if (Visit.HasCurrent && !Visit.Current.IsCrawler)
				Bobs.Log.Increment(Model.Entities.Log.Items.WhosGoingOutPageNoCrawlers);

			if (SexTypeFromUrl != SexType.NoneSpecified && LocationTypeFromUrl != LocationType.NoneSpecified)
			{
				
				UsrEventAttendedSet ueas = get(false);

				if (ueas.Count <= 1)
					ueas = get(true);

				if (ueas.Count > 1)
				{
					int tries = 0;
					int index = ContainerPage.Random.Next(ueas.Count);
					Usr newU = ueas[index].Usr;
					while (CurrentUsr != null && newU.K == CurrentUsr.K && tries < 10)
					{
						index = ContainerPage.Random.Next(ueas.Count);
						newU = ueas[index].Usr;
						tries++;
					}

					Next.HRef = GetUrl(SexTypeFromUrl, LocationTypeFromUrl, newU.K);
						//"/pages/out/" + (ContainerPage.Url[0] == "boys" ? "boys" : "girls") + "/" + newU.K.ToString();

					if (CurrentUsr == null)
						Response.Redirect(Next.HRef);
				}
			}
			else
			{
				TopP.Visible = false;
				TopPChoose.Visible = true;
			}

			if (CurrentUsr != null)
			{

				string mainPicPath = "";
				List<Photo> photos = new List<Photo>();
				List<int> photoKs = new List<int>();

				bool hasPic = false;
				if (CurrentUsr.HasPicNotFacebook)
				{
					hasPic = true;
					mainPicPath = CurrentUsr.PicPath;
					photos.Add(CurrentUsr.PicPhoto);
					photoKs.Add(CurrentUsr.PicPhotoK);
				}

				{
					Query q = new Query();
					q.TableElement = new Join(Photo.UsrMeJoin, UsrPhotoFavourite.Columns.PhotoK, Photo.Columns.K);
					if (photoKs.Count > 0)
					{
						q.QueryCondition = new And(new Q(Photo.Columns.MediaType, Photo.MediaTypes.Image), new Q(Usr.Columns.K, CurrentUsr.K), new Q(UsrPhotoFavourite.Columns.UsrK, CurrentUsr.K), new NotQ(new InListQ(Photo.Columns.K, photoKs)));
					}
					else
					{
						q.QueryCondition = new And(new Q(Photo.Columns.MediaType, Photo.MediaTypes.Image), new Q(Usr.Columns.K, CurrentUsr.K), new Q(UsrPhotoFavourite.Columns.UsrK, CurrentUsr.K));
					}
					q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Descending);
					q.TopRecords = 5;
					q.CacheDuration = new TimeSpan(1, 0, 0);
					PhotoSet ps = new PhotoSet(q);

					foreach (Photo p in ps)
					{
						if (photos.Count == 0)
							mainPicPath = p.IconPath;

						photos.Add(p);
						photoKs.Add(p.K);
					}
				}

				if (photos.Count < 5)
				{
					Query q = new Query();
					q.TableElement = Photo.UsrMeJoin;
					if (photoKs.Count > 0)
					{
						q.QueryCondition = new And(new Q(Photo.Columns.MediaType, Photo.MediaTypes.Image), new Q(Usr.Columns.K, CurrentUsr.K), new NotQ(new InListQ(Photo.Columns.K, photoKs)));
					}
					else
					{
						q.QueryCondition = new And(new Q(Photo.Columns.MediaType, Photo.MediaTypes.Image), new Q(Usr.Columns.K, CurrentUsr.K));
					}
					q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Descending);
					q.TopRecords = 5;
					q.CacheDuration = new TimeSpan(1, 0, 0);
					PhotoSet ps = new PhotoSet(q);

					foreach (Photo p in ps)
					{
						if (photos.Count == 0)
							mainPicPath = p.IconPath;

						photos.Add(p);
						photoKs.Add(p.K);
					}
				}

				if (photos.Count == 0)
				{
					WebHolder.Visible = false;
					Thumb1.Visible = false;
					Thumb2.Visible = false;
					Thumb3.Visible = false;
					Thumb4.Visible = false;
					Thumb5.Visible = false;
				}
				else
				{
					Thumb1.Src = mainPicPath;
					Web.Src = photos[0].WebPath;
					Web.Width = photos[0].WebWidth;
					Web.Height = photos[0].WebHeight;
					Link.HRef = photos[0].Url();
					Thumb1.Attributes["onmouseover"] = "change('" + photos[0].WebPath + "', " + photos[0].WebWidth + ", " + photos[0].WebHeight + ", '" + photos[0].Url() + "');";
					Thumb1.Attributes["onclick"] = "change('" + photos[0].WebPath + "', " + photos[0].WebWidth + ", " + photos[0].WebHeight + ", '" + photos[0].Url() + "');";

					if (photos.Count > 1)
					{
						Thumb2.Src = photos[1].IconPath;
						Thumb2.Attributes["onmouseover"] = "change('" + photos[1].WebPath + "', " + photos[1].WebWidth + ", " + photos[1].WebHeight + ", '" + photos[1].Url() + "');";
						Thumb2.Attributes["onclick"] = "change('" + photos[1].WebPath + "', " + photos[1].WebWidth + ", " + photos[1].WebHeight + ", '" + photos[1].Url() + "');";

						if (photos.Count > 2)
						{
							Thumb3.Src = photos[2].IconPath;
							Thumb3.Attributes["onmouseover"] = "change('" + photos[2].WebPath + "', " + photos[2].WebWidth + ", " + photos[2].WebHeight + ", '" + photos[2].Url() + "');";
							Thumb3.Attributes["onclick"] = "change('" + photos[2].WebPath + "', " + photos[2].WebWidth + ", " + photos[2].WebHeight + ", '" + photos[2].Url() + "');";

							if (photos.Count > 3)
							{
								Thumb4.Src = photos[3].IconPath;
								Thumb4.Attributes["onmouseover"] = "change('" + photos[3].WebPath + "', " + photos[3].WebWidth + ", " + photos[3].WebHeight + ", '" + photos[3].Url() + "');";
								Thumb4.Attributes["onclick"] = "change('" + photos[3].WebPath + "', " + photos[3].WebWidth + ", " + photos[3].WebHeight + ", '" + photos[3].Url() + "');";


								if (photos.Count > 4)
								{
									Thumb5.Src = photos[4].IconPath;
									Thumb5.Attributes["onmouseover"] = "change('" + photos[4].WebPath + "', " + photos[4].WebWidth + ", " + photos[4].WebHeight + ", '" + photos[4].Url() + "');";
									Thumb5.Attributes["onclick"] = "change('" + photos[4].WebPath + "', " + photos[4].WebWidth + ", " + photos[4].WebHeight + ", '" + photos[4].Url() + "');";
								}
								else
								{
									Thumb5.Visible = false;
								}
							}
							else
							{
								Thumb4.Visible = false;
								Thumb5.Visible = false;
							}
						}
						else
						{
							Thumb3.Visible = false;
							Thumb4.Visible = false;
							Thumb5.Visible = false;
						}
					}
					else
					{
						Thumb2.Visible = false;
						Thumb3.Visible = false;
						Thumb4.Visible = false;
						Thumb5.Visible = false;
					}

				}

				BottomPara.InnerHtml = "Showing ";

				BottomPara.InnerHtml += SexTypeFromUrl == SexType.Girls ? "girls " : "";
				BottomPara.InnerHtml += SexTypeFromUrl == SexType.Boys ? "boys " : "";
				BottomPara.InnerHtml += SexTypeFromUrl == SexType.All || SexTypeFromUrl == SexType.NoneSpecified ? "girls and boys " : "";

				BottomPara.InnerHtml += " going out ";

				BottomPara.InnerHtml += LocationTypeFromUrl == LocationType.Country ? ("in " + CountryFromUrl.FriendlyName + ".") : "";
				BottomPara.InnerHtml += LocationTypeFromUrl == LocationType.Worldwide || LocationTypeFromUrl == LocationType.NoneSpecified ? "worldwide." : "";

				BottomPara.InnerHtml += " Click for: ";

				BottomPara.InnerHtml += SexTypeFromUrl == SexType.Girls ? "" : ("<a href=\"" + GetUrl(SexType.Girls, LocationTypeFromUrl, 0) + "\">girls</a>, ");
				BottomPara.InnerHtml += SexTypeFromUrl == SexType.Boys ? "" : ("<a href=\"" + GetUrl(SexType.Boys, LocationTypeFromUrl, 0) + "\">boys</a>, ");
				BottomPara.InnerHtml += SexTypeFromUrl == SexType.All || SexTypeFromUrl == SexType.NoneSpecified ? "" : ("<a href=\"" + GetUrl(SexType.All, LocationTypeFromUrl, 0) + "\">girls and boys</a>, ");

				BottomPara.InnerHtml += LocationTypeFromUrl == LocationType.Country ? "" : ("<a href=\"" + GetUrl(SexTypeFromUrl, LocationType.Country, 0) + "\">going out in " + HomeCountryFromVisitor.FriendlyName + "</a>.");
				BottomPara.InnerHtml += LocationTypeFromUrl == LocationType.Worldwide || LocationTypeFromUrl == LocationType.NoneSpecified ? "" : ("going out <a href=\"" + GetUrl(SexTypeFromUrl, LocationType.Worldwide, 0) + "\">worldwide</a>.");

				PlaceHolder para = photos.Count > 0 ? Para : ParaTop;

				{
					Query q = new Query();
					q.TableElement = Event.UsrAttendedJoin;
					q.QueryCondition = new And(
						new Q(Usr.Columns.K, CurrentUsr.K),
						Event.FutureEventsQueryCondition);
					q.OrderBy = Event.FutureEventOrder;
					q.Columns = new ColumnSet(UsrEventAttended.Columns.EventK);
					q.CacheDuration = new TimeSpan(1, 0, 0);

					UsrEventAttendedSet ueas = new UsrEventAttendedSet(q);

					if (ueas.Count > 0)
					{
						UsrEventAttended uea = ueas[0];

						Event ev = uea.Event;

						para.Controls.Add(
							new LiteralControl(
								"<p" + (photos.Count > 0 ? " style=\"margin-top:15px;\"" : "") + "><center>" + CurrentUsr.Link() + " is going to " + ev.FriendlyHtml(true, true, true, true, false) + (photos.Count == 0 ? ", but has no photos." : "") + "</center></p>"
							)
						);

					}
					else
					{
						para.Controls.Add(
							new LiteralControl(
								"<p" + (photos.Count > 0 ? " style=\"margin-top:15px;\"" : "") + "><center>" + CurrentUsr.Link() + " isn't going out soon" + (photos.Count == 0 ? ", and has no photos." : "") + "</center></p>"							)
						);
					}
					para.Controls.Add(
						new LiteralControl(
							"<p><center>Want to say hi? <a href=\"" + CurrentUsr.Url() + "#PrivateMessage\">Send " + CurrentUsr.NickName + " a private message.</a></center></p>")
					);
				}
			}
			else
			{
				WebHolder.Visible = false;
				Thumb2.Visible = false;
				Thumb1.Visible = false;
				Thumb3.Visible = false;
			}

		}

		public SexType SexTypeFromUrl
		{
			get
			{
				if (ContainerPage.Url[0].Exists)
				{
					return ContainerPage.Url[0] == "boys" ? SexType.Boys : 
						ContainerPage.Url[0] == "girls" ? SexType.Girls : 
						ContainerPage.Url[0] == "all" ? SexType.All : SexType.NoneSpecified;
				}
				else
					return SexType.NoneSpecified;
			}
		}

		public LocationType LocationTypeFromUrl
		{
			get
			{
				if (ContainerPage.Url[1].Exists)
				{
					if (ContainerPage.Url[1] == "worldwide")
					{
						return LocationType.Worldwide;
					}
					else
					{
						CountrySet cs = new CountrySet(new Query(new Q(Country.Columns.UrlName, ContainerPage.Url[1].ToString())));
						if (cs.Count > 0)
							return LocationType.Country;
						else
							return LocationType.NoneSpecified;
					}
				}
				else
					return LocationType.NoneSpecified;
			}
		}

		Country CountryFromUrl
		{
			get
			{
				if (countryFromUrl == null)
					countryFromUrl = new Country(CountryKFromUrl);
				return countryFromUrl;
			}
		}
		Country countryFromUrl;

		int CountryKFromUrl
		{
			get
			{
				if (ContainerPage.Url[1].Exists)
				{
					if (ContainerPage.Url[1] == "worldwide")
					{
						return 0;
					}
					else
					{
						CountrySet cs = new CountrySet(new Query(new Q(Country.Columns.UrlName, ContainerPage.Url[1].ToString())));
						if (cs.Count > 0)
							return cs[0].K;
						else
							return 0;
					}
				}
				else
					return 0;
			}
		}

		public string GetUrl(SexType sex, LocationType location, int usrK)
		{
			return "/pages/out" +
				(sex != SexType.NoneSpecified ? ("/" + sexString(sex)) : "") +
				(location != LocationType.NoneSpecified ? ("/" + locationString(location)) : "") +
				(usrK > 0 ? ("/" + usrK.ToString()) : "");

		}
		string sexString(SexType sex)
		{
			switch (sex)
			{
				case SexType.All: return "all";
				case SexType.Boys: return "boys";
				case SexType.Girls: return "girls";
				default: return "";
			}
		}
		string locationString(LocationType location)
		{
			switch (location)
			{
				case LocationType.Country: return HomeCountryFromVisitor.UrlName;
				case LocationType.Worldwide: return "worldwide";
				default: return "";
			}
		}

		public Country HomeCountryFromVisitor
		{
			get
			{
				return Usr.Current != null ? Usr.Current.Home.Country : Visit.HasCurrent ? Visit.Current.Country : IpCountry.ClientCountry;
			}
		}

		public Usr CurrentUsr
		{
			get
			{
				if (currentUsr == null)
				{
					try
					{
						currentUsr = new Usr(ContainerPage.Url[2]);
					}
					catch
					{
					
					}
				}
				return currentUsr;
			}
		}
		Usr currentUsr;
	}
}
