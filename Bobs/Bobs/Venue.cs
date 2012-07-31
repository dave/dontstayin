using System;
using System.Collections;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Web;
using Cambro.Misc;
using Cambro.Web;
using Model.Entities.Properties;

namespace Bobs
{

	#region Venue
	/// <summary>
	/// A club or location
	/// </summary>
	[Serializable]
	public partial class Venue : IPic, IName, IReadableReference, IPage, IDiscussable, IBobType, ICalendar, IObjectPage, IRelevanceContributor, IHasArchive, IDeleteAll, IConnectedTo, ILinkable, IStyledEventHolder, IHasSpatialData, IHasParent
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Venue.Columns.K] as int? ?? 0; }
			set { this[Venue.Columns.K] = value; }
		}
		/// <summary>
		/// Name of the venue
		/// </summary>
		public override string Name
		{
			get { return (string)this[Venue.Columns.Name]; }
			set { this[Venue.Columns.Name] = value; }
		}
		/// <summary>
		/// Info about the club, address
		/// </summary>
		public override string DetailsHtml
		{
			get { return (string)this[Venue.Columns.DetailsHtml]; }
			set { this[Venue.Columns.DetailsHtml] = value; }
		}
		/// <summary>
		/// Postcode
		/// </summary>
		public override string Postcode
		{
			get { return (string)this[Venue.Columns.Postcode]; }
			set { this[Venue.Columns.Postcode] = value; }
		}
		/// <summary>
		/// Links to one Place
		/// </summary>
		public override int PlaceK
		{
			get { return (int)this[Venue.Columns.PlaceK]; }
			set
			{
				place = null;
				this[Venue.Columns.PlaceK] = value;
				this.CopySpatialDataFrom(Place);
			}
		}
		/// <summary>
		/// Cropped image between 75*75 and 100*100
		/// </summary>
		public override Guid Pic
		{
			get { return Db.GuidConvertor(this[Venue.Columns.Pic]); }
			set { this[Venue.Columns.Pic] = new SqlGuid(value); }
		}
		/// <summary>
		/// If the owner wants to upload another image after the this has been enabled, it is stored here
		/// </summary>
		public override Guid PicNew
		{
			get { return Db.GuidConvertor(this[Venue.Columns.PicNew]); }
			set { this[Venue.Columns.PicNew] = new SqlGuid(value); }
		}
		/// <summary>
		/// Admin note
		/// </summary>
		public override string AdminNote
		{
			get { return (string)this[Venue.Columns.AdminNote]; }
			set { this[Venue.Columns.AdminNote] = value; }
		}
		/// <summary>
		/// Url of the page with the map on it (if there is a better map than multimep)
		/// </summary>
		public override string OverrideMapUrl
		{
			get { return (string)this[Venue.Columns.OverrideMapUrl]; }
			set { this[Venue.Columns.OverrideMapUrl] = value; }
		}
		/// <summary>
		/// The user that added this event (0 if added by admin)
		/// </summary>
		public override int OwnerUsrK
		{
			get { return (int)this[Venue.Columns.OwnerUsrK]; }
			set { owner = null; this[Venue.Columns.OwnerUsrK] = value; }
		}
		/// <summary>
		/// The capacity of the venue (max number of people)
		/// </summary>
		public override int Capacity
		{
			get { return (int)this[Venue.Columns.Capacity]; }
			set { this[Venue.Columns.Capacity] = value; }
		}
		/// <summary>
		/// The total number of comments
		/// </summary>
		public override int TotalComments
		{
			get { return (int)this[Venue.Columns.TotalComments]; }
			set { this[Venue.Columns.TotalComments] = value; }
		}
		/// <summary>
		/// The date/time of the last post that was posted in this board (including child objects)
		/// </summary>
		public override DateTime LastPost
		{
			get { return (DateTime)this[Venue.Columns.LastPost]; }
			set { this[Venue.Columns.LastPost] = value; }
		}
		/// <summary>
		/// The average date.time of all comments posted in this board (including child objects)
		/// </summary>
		public override DateTime AverageCommentDateTime
		{
			get { return (DateTime)this[Venue.Columns.AverageCommentDateTime]; }
			set { this[Venue.Columns.AverageCommentDateTime] = value; }
		}
		/// <summary>
		/// When was the venue added to the system?
		/// </summary>
		public override DateTime AddedDateTime
		{
			get { return (DateTime)this[Venue.Columns.AddedDateTime]; }
			set { this[Venue.Columns.AddedDateTime] = value; }
		}
		/// <summary>
		/// If true, only the venue owner may upload photos
		/// </summary>
		public override bool NoPhotos
		{
			get { return (bool)this[Venue.Columns.NoPhotos]; }
			set { this[Venue.Columns.NoPhotos] = value; }
		}
		/// <summary>
		/// The email address of the admin contact for sorting spoters with guestlists
		/// </summary>
		public override string AdminEmail
		{
			get { return (string)this[Venue.Columns.AdminEmail]; }
			set { this[Venue.Columns.AdminEmail] = value; }
		}
		/// <summary>
		/// Is the description text or html?
		/// </summary>
		public override bool IsDescriptionText
		{
			get { return (bool)this[Venue.Columns.IsDescriptionText]; }
			set { this[Venue.Columns.IsDescriptionText] = value; }
		}
		/// <summary>
		/// Has the venue been seen by an admin or not?
		/// </summary>
		public override bool IsNew
		{
			get { return (bool)this[Venue.Columns.IsNew]; }
			set { this[Venue.Columns.IsNew] = value; }
		}
		/// <summary>
		/// If true, photos taken at this venue can't be ordered.
		/// </summary>
		public override bool NoPrints
		{
			get { return (bool)this[Venue.Columns.NoPrints]; }
			set { this[Venue.Columns.NoPrints] = value; }
		}
		/// <summary>
		/// Should the description just have "\n" replaced with "&lt;br&gt;"? (This overrides IsDescriptionText)
		/// </summary>
		public override bool IsDescriptionCleanHtml
		{
			get { return (bool)this[Venue.Columns.IsDescriptionCleanHtml]; }
			set { this[Venue.Columns.IsDescriptionCleanHtml] = value; }
		}
		/// <summary>
		/// Has the venue been recently edited?
		/// </summary>
		public override bool IsEdited
		{
			get { return (bool)this[Venue.Columns.IsEdited]; }
			set { this[Venue.Columns.IsEdited] = value; }
		}
		/// <summary>
		/// Guid used to ensure duplicate venues don't get posted if the user refreshes the page after saving.
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Db.GuidConvertor(this[Venue.Columns.DuplicateGuid]); }
			set { this[Venue.Columns.DuplicateGuid] = new SqlGuid(value); }
		}
		/// <summary>
		/// Does the venue host regular (monthly or more often) events?
		/// </summary>
		public override bool RegularEvents
		{
			get { return (bool)this[Venue.Columns.RegularEvents]; }
			set { this[Venue.Columns.RegularEvents] = value; }
		}
		/// <summary>
		/// Unique name used in the url
		/// </summary>
		public override string UrlName
		{
			get { return (string)this[Venue.Columns.UrlName]; }
			set { this[Venue.Columns.UrlName] = value; }
		}
		/// <summary>
		/// Link to the promoter table
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[Venue.Columns.PromoterK]; }
			set { promoter = null; this[Venue.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// State var used to reconstruct cropper when re-cropping
		/// </summary>
		public override string PicState
		{
			get { return (string)this[Venue.Columns.PicState]; }
			set { this[Venue.Columns.PicState] = value; }
		}
		/// <summary>
		/// The Photo that was used to create the Pic.
		/// </summary>
		public override int PicPhotoK
		{
			get { return (int)this[Venue.Columns.PicPhotoK]; }
			set { picPhoto = null; this[Venue.Columns.PicPhotoK] = value; }
		}
		/// <summary>
		/// The Misc that was used to create the Pic.
		/// </summary>
		public override int PicMiscK
		{
			get { return (int)this[Venue.Columns.PicMiscK]; }
			set { picMisc = null; this[Venue.Columns.PicMiscK] = value; }
		}
		/// <summary>
		/// The url fragment - so that the url can be generated without accessing parent database records
		/// </summary>
		public override string UrlFragment
		{
			get { return (string)this[Venue.Columns.UrlFragment]; }
			set { this[Venue.Columns.UrlFragment] = value; }
		}
		/// <summary>
		/// The moderator that has been assigned to moderate this venue
		/// </summary>
		public override int ModeratorUsrK
		{
			get { return (int)this[Venue.Columns.ModeratorUsrK]; }
			set { moderatorUsr = null; this[Venue.Columns.ModeratorUsrK] = value; }
		}
		/// <summary>
		/// The total number of events at this venue
		/// </summary>
		public override int TotalEvents
		{
			get { return (int)this[Venue.Columns.TotalEvents]; }
			set { this[Venue.Columns.TotalEvents] = value; }
		}
		/// <summary>
		/// Has the promoter been confirmed?
		/// </summary>
		public override PromoterStatusEnum PromoterStatus
		{
			get { return (PromoterStatusEnum)this[Venue.Columns.PromoterStatus]; }
			set { this[Venue.Columns.PromoterStatus] = value; }
		}
		/// <summary>
		/// Is the Details plain html? - e.g. rendered outsite the yellow box?
		/// </summary>
		public override bool DetailsPlain
		{
			get { return (bool)this[Venue.Columns.DetailsPlain]; }
			set { this[Venue.Columns.DetailsPlain] = value; }
		}
		/// <summary>
		/// Css to emit for the styled pages
		/// </summary>
		public override string StyledCss
		{
			get { return (string)this[Venue.Columns.StyledCss]; }
			set { this[Venue.Columns.StyledCss] = value; }
		}
		/// <summary>
		/// Xml to emit for the styled pages
		/// </summary>
		public override string StyledXml
		{
			get { return (string)this[Venue.Columns.StyledXml]; }
			set { this[Venue.Columns.StyledXml] = value; }
		}
		bool spatialDataChanged = false;
		/// <summary>
		/// Hierarchical triangular mesh index
		/// </summary>
		public override long HtmId
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}
		/// <summary>
		/// Latitude
		/// </summary>
		public override double Lat
		{
			get {  return (double) this[Columns.Lat]; }
			set { this[Columns.Lat] = value; spatialDataChanged = true; }
		}
		/// <summary>
		/// Longitude
		/// </summary>
		public override double Lon
		{
			get { return (double) this[Columns.Lon] ; }
			set { this[Columns.Lon] = value; spatialDataChanged = true; }
		}

		 
		 
		#endregion

		#region IStyledEventHolder Members
		public bool IsEvent(Event evnt)
		{
			return evnt.VenueK.Equals(this.K);
		}
		public string UrlStyledSetup(params string[] par)
		{
			return IStyledEventHolderExtentions.UrlStyledSetup(this);
		}
		public string UrlStyledCalendar(int year, int month, params string[] par)
		{
			return IStyledEventHolderExtentions.UrlStyledCalendar(this, year, month, par);
		}

		public string UrlStyled(params string[] par)
		{
			return IStyledEventHolderExtentions.UrlStyled(this, par);
		}

		public string UrlStyledApp(string application, params string[] par)
		{
			return IStyledEventHolderExtentions.UrlStyledApp(this, application, par);
		}

		public string UrlStyledEvent(int eventK, params string[] par)
		{
			return IStyledEventHolderExtentions.UrlStyledEvent(this, eventK, par);
		}

		public string UrlStyledEventLink(Event evnt, params string[] par)
		{
			return IStyledEventHolderExtentions.UrlStyledEventLink(this, evnt, par);
		}

		public string UrlCss()
		{
			return IStyledEventHolderExtentions.UrlCss(this);
		}
		#endregion

		#region ILinkable Members

		public string Link(params string[] par)
		{
			return ILinkableExtentions.Link(this, par);
		}
		public string LinkNewWindow(params string[] par)
		{
			return ILinkableExtentions.LinkNewWindow(this, par);
		}

		#endregion

		#region PromoterStatusEnum
		#endregion

		#region MergeAndDelete
		public void MergeAndDelete(Venue merge)
		{
			if (this.K == merge.K)
				throw new DsiUserFriendlyException("Can't merge a venue into itself!");

			Cambro.Web.Helpers.WriteAlertHeader();

			//throw new Exception("This function isn't finished yet!");

			Cambro.Web.Helpers.WriteAlert("Starting merge...", true);

			#region Promoter
			Cambro.Web.Helpers.WriteAlert("Merging promoter...", true);
			if (merge.PromoterK > 0 && merge.PromoterStatus.Equals(Venue.PromoterStatusEnum.Confirmed) && merge.Promoter.IsEnabled)
			{
				this.PromoterK = merge.PromoterK;
				this.PromoterStatus = Venue.PromoterStatusEnum.Confirmed;
			}
			else if (this.PromoterK == 0 && merge.PromoterK > 0)
			{
				this.PromoterK = merge.PromoterK;
				this.PromoterStatus = merge.PromoterStatus;
			}
			Cambro.Web.Helpers.WriteAlert("Done merging promoter...");
			#endregion

			#region Articles
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Moving articles...", true);
				Query q = new Query();
				q.QueryCondition = new And(new Q(Article.Columns.ParentObjectType, Model.Entities.ObjectType.Venue),new Q(Article.Columns.ParentObjectK, merge.K));
				ArticleSet ars = new ArticleSet(q);
				foreach (Article a in ars)
				{
					Cambro.Web.Helpers.WriteAlert("Moving article " + a.K + "...");
					a.ParentObjectK = this.K;
					a.VenueK = this.K;

					if (a.Relevance <= Model.Entities.Article.RelevanceEnum.Place)
						a.PlaceK = this.PlaceK;
					else
						a.PlaceK = 0;

					if (a.Relevance <= Model.Entities.Article.RelevanceEnum.Country)
						a.CountryK = this.Place.CountryK;
					else
						a.CountryK = 0;

					a.UrlFragment = this.UrlFilterPart;
					a.Update();

					#region Threads
					if (true)
					{
						Update u = new Update();
						u.Table = TablesEnum.Thread;
						u.Where = new Q(Thread.Columns.ArticleK, a.K);
						u.Changes.Add(new Assign(Thread.Columns.UrlFragment, a.UrlFilterPart));
						u.Changes.Add(new Assign(Thread.Columns.VenueK, this.K));
						u.Changes.Add(new Assign(Thread.Columns.PlaceK, this.PlaceK));
						u.Changes.Add(new Assign(Thread.Columns.CountryK, this.Place.CountryK));
						u.Run();
					}
					#endregion
					#region Galleries
					if (true)
					{
						Update u = new Update();
						u.Table = TablesEnum.Gallery;
						u.Where = new Q(Gallery.Columns.ArticleK, a.K);
						u.Changes.Add(new Assign(Gallery.Columns.UrlFragment, a.UrlFilterPart));
						u.Run();
					}
					#endregion
					#region Photos
					if (true)
					{
						Update u = new Update();
						u.Table = TablesEnum.Photo;
						u.Where = new Q(Photo.Columns.ArticleK, a.K);
						u.Changes.Add(new Assign(Photo.Columns.UrlFragment, a.UrlFilterPart));
						u.Run();
					}
					#endregion
				}
				Cambro.Web.Helpers.WriteAlert("Done moving articles...", true);
			}
			#endregion

			#region Events
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Moving events...", true);
				EventSet es = new EventSet(new Query(new Q(Event.Columns.VenueK, merge.K)));
				int count = 0;
				foreach (Event ev in es)
				{
					count++;
					Cambro.Web.Helpers.WriteAlert("Moving event "+ev.K+" ("+count+" / "+es.Count+")...");
					ev.ChangeVenue(this.K, true);
				}
				Cambro.Web.Helpers.WriteAlert("Done moving events...");
			}
			#endregion

			#region Thread ParentObjects
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging topics (1/2)...", true);
				Update u = new Update();
				u.Table = TablesEnum.Thread;
				u.Where = new And(
					new Q(Thread.Columns.ParentObjectType, Model.Entities.ObjectType.Venue),
					new Q(Thread.Columns.ParentObjectK, merge.K));
				u.Changes.Add(new Assign(Thread.Columns.ParentObjectK, this.K));
				u.Run();
				Cambro.Web.Helpers.WriteAlert("Done merging topics (1/2)...");
			}
			#endregion

			#region Thread
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging topics (2/2)...", true);
				Update u = new Update();
				u.Table = TablesEnum.Thread;
				u.Where = new And(
					new Q(Thread.Columns.VenueK, merge.K),
					new Q(Thread.Columns.EventK, 0),
					new Q(Thread.Columns.ArticleK, 0));
				u.Changes.Add(new Assign(Thread.Columns.VenueK, this.K));
				u.Changes.Add(new Assign(Thread.Columns.UrlFragment, this.UrlFilterPart));
				u.Run();
				Cambro.Web.Helpers.WriteAlert("Done merging topics (2/2)...");
			}
			#endregion

			#region Pic
			if (!this.HasPic)
			{
				Cambro.Web.Helpers.WriteAlert("Merging picture...", true);
				this.Pic = merge.Pic;
				this.PicMiscK = merge.PicMiscK;
				this.PicPhotoK = merge.PicPhotoK;
				this.PicState = merge.PicState;
				merge.Pic = Guid.Empty;
				merge.PicMiscK = 0;
				merge.PicPhotoK = 0;
				merge.PicState = "";
				merge.Update();
				Cambro.Web.Helpers.WriteAlert("Done merging picture...");
			}
			#endregion

			this.AdminNote += "Venue " + merge.K + " was merged with this one " + DateTime.Now.ToString() + ". The admin note from venue " + merge.K + " is:\n********************\n" + merge.AdminNote + "\n********************\n";

			this.Update();

			int mergePlaceK = merge.PlaceK;

			Cambro.Web.Helpers.WriteAlert("Deleting old venue...", true);
			merge.DeleteAll(null);
			Cambro.Web.Helpers.WriteAlert("Done deleting old venue...");

			if (mergePlaceK != this.PlaceK)
			{
				Place mergePlace = new Place(mergePlaceK);
				Cambro.Web.Helpers.WriteAlert("Updating stats for old place...", true);
				mergePlace.UpdateTotalComments(null);
				mergePlace.UpdateTotalEvents(null);
				Cambro.Web.Helpers.WriteAlert("Done updating stats for old place...");
			}

			Cambro.Web.Helpers.WriteAlert("Updating stats for new venue...", true);
			this.UpdateTotalComments(null);
			this.UpdateTotalEvents(null);
			Cambro.Web.Helpers.WriteAlert("Done updating stats for new venue...");

			this.Update();
			Cambro.Web.Helpers.WriteAlert("Done merging venues!", true);


		}
		#endregion

		#region UpdateTotalEvents()
		public void UpdateTotalEvents(Transaction transaction)
		{
			Query q = new Query();
			q.QueryCondition=new Q(Event.Columns.VenueK,this.K);
			q.ReturnCountOnly=true;
			EventSet allEvents = new EventSet(q);
			this.TotalEvents = allEvents.Count;
			this.Update(transaction);

			this.Place.UpdateTotalEvents(transaction);
		}
		#endregion

		#region IBobType Members
		public string TypeName
		{
			get
			{
				return "Venue";
			}
		}

		public Model.Entities.ObjectType ObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Venue;
			}
		}
		#endregion
		#region AddRelevant
		public void AddRelevant(IRelevanceHolder ContainerPage)
		{
			ContainerPage.RelevantPlacesAdd(this.PlaceK);
		}
		#endregion

		#region IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		public bool IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		{
			if (objectType.Equals(Model.Entities.ObjectType.Venue) && this.K == objectK)
				return true;

			if (objectType.Equals(Model.Entities.ObjectType.Place) && this.PlaceK == objectK)
				return true;

			if (Place.CanBeConnectedToStatic(objectType) && this.Place.IsConnectedTo(objectType, objectK))
				return true;

			return false;

		}
		public static bool CanBeConnectedToStatic(Model.Entities.ObjectType o)
		{
			if (o.Equals(Model.Entities.ObjectType.Place))
				return true;

			if (Place.CanBeConnectedToStatic(o))
				return true;

			return false;
		}
		public bool CanBeConnectedTo(Model.Entities.ObjectType o)
		{
			return Venue.CanBeConnectedToStatic(o);
		}
		#endregion

		#region SimilarVenues
		public VenueSet SimilarVenues
		{
			get
			{
				if (similarVenues==null)
				{
					similarVenues = SimilarVenuesStatic(this.Name,this.Place,this.K,this.Postcode);
				}
				return similarVenues;
			}
			set
			{
				similarVenues = value;
			}
		}
		private VenueSet similarVenues;
		#endregion

		#region SimilarVenuesStatic
		public static VenueSet SimilarVenuesStatic(string name, Place place, int excludeVenueK, string postCode)
		{
			//split name into words and get all > 3 chars...
			ArrayList al = new ArrayList();
			ArrayList commonWords = new ArrayList(Q.CommonWords);
			commonWords.Add("bar");
			commonWords.Add("club");
			string[] words = name.Split(' ');
			int wordsCount = 0;
			foreach (string word in words)
			{
				if (!commonWords.Contains(word.ToLower()) && word.Length>1)
				{
					al.Add(new Q(Columns.Name,QueryOperator.TextContains,word));
					wordsCount++;
				}
			}
			Q wordsOr = new Q(true);
			if (wordsCount>0)
				wordsOr = new Or((Q[])al.ToArray(typeof(Q)));
			else
				wordsOr = new Q(false);

			//10 nearest places also
			Query qPlaces = new Query();
			qPlaces.TopRecords=10;
			qPlaces.OrderBy=place.NearestPlacesOrderBy;
			PlaceSet ps = new PlaceSet(qPlaces);
			ArrayList al1 = new ArrayList();
			foreach (Place p in ps)
			{
				al1.Add(new Q(Columns.PlaceK,p.K));
			}
			Or placesOr = new Or((Q[])al1.ToArray(typeof(Q)));

			Q thisVenue = new Q(true);
			if (excludeVenueK>0)
				thisVenue = new Q(Columns.K,QueryOperator.NotEqualTo,excludeVenueK);

			Q postCodeQ = null;
			if (postCode.Length>0)
				postCodeQ = new Q(Columns.Postcode,postCode);
			else
				postCodeQ = new Q(false);

			Query qSimilar = new Query();
			qSimilar.QueryCondition = new And(new Or(new And(wordsOr, placesOr), postCodeQ), thisVenue);
			VenueSet vsSimilar = new VenueSet(qSimilar);
			return vsSimilar;



		
		}
		#endregion

		#region DeleteReturnStatus
		#endregion
		#region DeleteAllUsr
		public DeleteReturnStatus DeleteAllUsr(Usr u)
		{
			if (!u.IsSuper && u.K!=this.OwnerUsrK)
				return DeleteReturnStatus.FailNoPermission;

			if (this.PromoterK>0 && this.PromoterStatus.Equals(Venue.PromoterStatusEnum.Confirmed))
				return DeleteReturnStatus.FailPromoter;

			if (this.TotalComments>10)
			{
				Mailer smComments = new Mailer();
				smComments.Body+="<p><a href=\"http://"+Vars.DomainName+u.Url()+"\">"+u.NickNameSafe+"</a> ("+u.Email+") attempted to delete venue "+this.K+" (<a href=\"http://"+Vars.DomainName+this.Url()+"\">"+this.FriendlyName+"</a>).</p>";
				smComments.Body+="<p>This operation failed because "+this.Name+" has "+this.TotalComments+" comments.</p>";
				smComments.Subject="Delete venue operation failed because too many comments in venue";
				smComments.TemplateType=Mailer.TemplateTypes.AdminNote;
				smComments.To = "events@dontstayin.com";
				smComments.Send();
				return DeleteReturnStatus.FailComments;
			}

			if (this.Events.Count>3)
			{
				Mailer smEvents = new Mailer();
				smEvents.Body+="<p><a href=\"http://"+Vars.DomainName+u.Url()+"\">"+u.NickNameSafe+"</a> ("+u.Email+") attempted to delete venue "+this.K+" (<a href=\"http://"+Vars.DomainName+this.Url()+"\">"+this.FriendlyName+"</a>).</p>";
				smEvents.Body+="<p>This operation failed because "+this.Name+" has "+this.Events.Count+" events.</p>";
				smEvents.Subject="Delete venue operation failed because too many events";
				smEvents.TemplateType=Mailer.TemplateTypes.AdminNote;
				smEvents.To = "events@dontstayin.com";
				smEvents.Send();
				return DeleteReturnStatus.FailEvents;
			}

			int totalPhotos = 0;
			foreach (Event ev in this.Events)
			{
				totalPhotos += ev.TotalPhotos;
			}
			if (totalPhotos>5)
			{
				Mailer smPhotos = new Mailer();
				smPhotos.Body+="<p><a href=\"http://"+Vars.DomainName+u.Url()+"\">"+u.NickNameSafe+"</a> ("+u.Email+") attempted to delete venue "+this.K+" (<a href=\"http://"+Vars.DomainName+this.Url()+"\">"+this.FriendlyName+"</a>).</p>";
				smPhotos.Body+="<p>This operation failed because events at "+this.Name+" have "+totalPhotos+" photos.</p>";
				smPhotos.Subject="Delete venue operation failed because too many photos in events";
				smPhotos.TemplateType=Mailer.TemplateTypes.AdminNote;
				smPhotos.To = "events@dontstayin.com";
				smPhotos.Send();
				return DeleteReturnStatus.FailPhotos;
			}


			//banners?
			Query qBanners = new Query();
			qBanners.TableElement=new Join(Banner.Columns.EventK, Event.Columns.K);
			qBanners.QueryCondition=new Q(Event.Columns.VenueK,this.K);
			qBanners.ReturnCountOnly=true;
			BannerSet bs = new BannerSet(qBanners);
			if (bs.Count>0)
			{
				Mailer smBanner = new Mailer();
				smBanner.Body+="<p><a href=\"http://"+Vars.DomainName+u.Url()+"\">"+u.NickNameSafe+"</a> ("+u.Email+") attempted to delete venue "+this.K+" (<a href=\"http://"+Vars.DomainName+this.Url()+"\">"+this.FriendlyName+"</a>).</p>";
				smBanner.Body+="<p>This operation failed because "+this.Name+" has "+bs.Count+" banner"+(bs.Count==1?"":"s")+".</p>";
				smBanner.Subject="Delete venue operation failed because venue has a banner";
				smBanner.TemplateType=Mailer.TemplateTypes.AdminNote;
				smBanner.To = "events@dontstayin.com";
				smBanner.Send();
				return DeleteReturnStatus.FailPromoter;
			}
			//guestlists?
			Query qGuestlists = new Query();
			qGuestlists.QueryCondition=new And(new Q(Event.Columns.HasGuestlist,true),new Q(Event.Columns.VenueK,this.K));
			qGuestlists.ReturnCountOnly=true;
			EventSet es = new EventSet(qGuestlists);
			if (es.Count>0)
			{
				Mailer smGuestlist = new Mailer();
				smGuestlist.Body+="<p><a href=\"http://"+Vars.DomainName+u.Url()+"\">"+u.NickNameSafe+"</a> ("+u.Email+") attempted to delete venue "+this.K+" (<a href=\"http://"+Vars.DomainName+this.Url()+"\">"+this.FriendlyName+"</a>).</p>";
				smGuestlist.Body+="<p>This operation failed because "+this.Name+" has "+es.Count+" guestlist"+(es.Count==1?"":"s")+".</p>";
				smGuestlist.Subject="Delete venue operation failed because venue has a guestlist";
				smGuestlist.TemplateType=Mailer.TemplateTypes.AdminNote;
				smGuestlist.To = "events@dontstayin.com";
				smGuestlist.Send();
				return DeleteReturnStatus.FailPromoter;
			}
			//competitions?
			Query qComp = new Query();
			qComp.TableElement=new Join(Comp.Columns.EventK, Event.Columns.K);
			qComp.QueryCondition=new Q(Event.Columns.VenueK,this.K);
			qComp.ReturnCountOnly=true;
			CompSet cs = new CompSet(qComp);
			if (cs.Count>0)
			{
				Mailer smComp = new Mailer();
				smComp.Body+="<p><a href=\"http://"+Vars.DomainName+u.Url()+"\">"+u.NickNameSafe+"</a> ("+u.Email+") attempted to delete venue "+this.K+" (<a href=\"http://"+Vars.DomainName+this.Url()+"\">"+this.FriendlyName+"</a>).</p>";
				smComp.Body+="<p>This operation failed because "+this.Name+" has "+cs.Count+" competition"+(cs.Count==1?"":"s")+".</p>";
				smComp.Subject="Delete venue operation failed because venue has a competition";
				smComp.TemplateType=Mailer.TemplateTypes.AdminNote;
				smComp.To = "events@dontstayin.com";
				smComp.Send();
				return DeleteReturnStatus.FailPromoter;
			}
			//dontations?
			Query qDonated = new Query();
			qDonated.QueryCondition=new And(new Q(Event.Columns.VenueK,this.K),new Q(Event.Columns.Donated,true));
			qDonated.ReturnCountOnly=true;
			EventSet esDon = new EventSet(qDonated);
			if (esDon.Count>0)
			{
				Mailer smDonate = new Mailer();
				smDonate.Body+="<p><a href=\"http://"+Vars.DomainName+u.Url()+"\">"+u.NickNameSafe+"</a> ("+u.Email+") attempted to delete venue "+this.K+" (<a href=\"http://"+Vars.DomainName+this.Url()+"\">"+this.FriendlyName+"</a>).</p>";
				smDonate.Body+="<p>This operation failed because "+this.Name+" has "+esDon.Count+" events with donations.</p>";
				smDonate.Subject="Delete venue operation failed because venue has an event donation";
				smDonate.TemplateType=Mailer.TemplateTypes.AdminNote;
				smDonate.To = "events@dontstayin.com";
				smDonate.Send();
				return DeleteReturnStatus.FailPromoter;
			}
            //ticket runs?
            Query qTicketRuns = new Query();
            qTicketRuns.QueryCondition = new Q(Event.Columns.VenueK, this.K);
            qTicketRuns.TableElement = new Join(Event.Columns.VenueK, Venue.Columns.K);
            qTicketRuns.ReturnCountOnly = true;
            EventSet esTix = new EventSet(qTicketRuns);
            if (esTix.Count > 0)
            {
                Mailer smDonate = new Mailer();
                smDonate.Body += "<p><a href=\"http://" + Vars.DomainName + u.Url() + "\">" + u.NickNameSafe + "</a> (" + u.Email + ") attempted to delete venue " + this.K + " (<a href=\"http://" + Vars.DomainName + this.Url() + "\">" + this.FriendlyName + "</a>).</p>";
                smDonate.Body += "<p>This operation failed because " + this.Name + " has " + esTix.Count + " events with ticket runs.</p>";
                smDonate.Subject = "Delete venue operation failed because venue has at least one event with a ticket run";
                smDonate.TemplateType = Mailer.TemplateTypes.AdminNote;
                smDonate.To = "events@dontstayin.com";
                smDonate.Send();
                return DeleteReturnStatus.FailPromoter;
            }

			try
			{
				Bobs.Delete.DeleteAll(this);

				//Mailer smDone = new Mailer();
				//smDone.Body+="<p><a href=\"http://"+Vars.DomainName+u.Url()+"\">"+u.NickNameSafe+"</a> ("+u.Email+") deleted venue "+this.K+" (<a href=\"http://"+Vars.DomainName+this.Url()+"\">"+this.FriendlyName+"</a>).</p>";
				//smDone.Subject="Venue "+this.K.ToString()+" deleted";
				//smDone.TemplateType=Mailer.TemplateTypes.AdminNote;
				//smDone.To = "events@dontstayin.com";
				//smDone.Send();

				return DeleteReturnStatus.Success;
			}
			catch(Exception ex)
			{
			
				Mailer smException = new Mailer();
				smException.Body+="<p><a href=\"http://"+Vars.DomainName+u.Url()+"\">"+u.NickNameSafe+"</a> ("+u.Email+") attempted to delete venue "+this.K+" (<a href=\"http://"+Vars.DomainName+this.Url()+"\">"+this.FriendlyName+"</a>).</p>";
				smException.Body+="<p>This operation failed because of an unhandled exception:</p><p>"+ex.ToString()+"</p>";
				smException.Subject="Delete venue operation failed because of exception";
				smException.TemplateType=Mailer.TemplateTypes.AdminNote;
				smException.To="d.brophy@dontstayin.com";
				smException.Send();
				return DeleteReturnStatus.FailException;
			}
		}
		#endregion

		#region LastPostFriendlyTime
		public string LastPostFriendlyTime(bool Capital)
		{
			return Utility.FriendlyTime(LastPost,Capital);
		}
		#endregion

		#region UrlCalendar
		public string UrlCalendarGeneric(string Application, int Year, int Month, int Day, int SkipDay, params string[] par)
		{
			DateTime month = new DateTime(Year,Month,1);
			string dayString = Day == 0 ? "" : ("/" + Day.ToString("00"));
			string url = UrlInfo.MakeUrl(UrlFilterPart + "/" + Year + "/" + month.ToString("MMM").ToLower() + dayString, Application, par);
			string skip = "";
			if (SkipDay>0)
				skip = "#Day"+new DateTime(Year, Month, SkipDay).ToString("yyyyMMdd");
			return url+skip;
		}
		public string UrlCalendarDay(bool Tickets, bool FreeGuestlist, int Year, int Month, int Day, params string[] par)
		{
			return UrlCalendarGeneric(FreeGuestlist ? "free" : Tickets ? "tickets" : null, Year, Month, Day, 0, par);
		}
		public string UrlCalendarDay(int Year, int Month, int Day, params string[] par)
		{
			return UrlCalendarGeneric(null, Year, Month, Day, 0, par);
		}
		public string UrlCalendarMonth(bool Tickets, bool FreeGuestlist, int Year, int Month, int SkipDay, params string[] par)
		{
			return UrlCalendarGeneric(FreeGuestlist ? "free" : Tickets ? "tickets" : null, Year, Month, 0, SkipDay, par);
		}
		public string UrlCalendarMonth(int Year, int Month, int SkipDay, params string[] par)
		{
			return UrlCalendarGeneric(null, Year, Month, 0, SkipDay, par);
		}
		public string UrlCalendar(bool Tickets, bool FreeGuestlist, params string[] par)
		{
			return UrlCalendarGeneric(FreeGuestlist ? "free" : Tickets ? "tickets" : null, DateTime.Today.Year, DateTime.Today.Month, 0, DateTime.Today.Day, par);
		}
		public string UrlCalendar(params string[] par)
		{
			return UrlCalendarGeneric(null, DateTime.Today.Year, DateTime.Today.Month, 0, DateTime.Today.Day, par);
		}
		public Event HasSingleEvent(int Year, int Month, int Day)
		{
			Query q = new Query();
			q.NoLock=true;
			q.Columns=new ColumnSet(Event.Columns.K,Event.Columns.VenueK,Event.Columns.DateTime);
			Q DateTimeQ = null;
			if (Year>0 && Month>0 && Day>0)
				DateTimeQ = new Q(Event.Columns.DateTime,new DateTime(Year,Month,Day));
			else if (Year>0 && Month>0)
				DateTimeQ = new And(
					new Q(Event.Columns.DateTime,QueryOperator.GreaterThanOrEqualTo,new DateTime(Year,Month,1)),
					new Q(Event.Columns.DateTime,QueryOperator.LessThan,new DateTime(Year,Month,1).AddMonths(1)));
			else if (Year>0)
				DateTimeQ = new And(
					new Q(Event.Columns.DateTime,QueryOperator.GreaterThanOrEqualTo,new DateTime(Year,1,1)),
					new Q(Event.Columns.DateTime,QueryOperator.LessThan,new DateTime(Year,1,1).AddYears(1)));
			q.TopRecords=2;
			q.QueryCondition=new And(new Q(Event.Columns.VenueK,this.K),DateTimeQ);
			EventSet es = new EventSet(q);
			if (es.Count==1)
				return es[0];
			else
				return null;
		}
		#endregion

		#region Last10Reviews
		public ThreadSet Last10Reviews
		{
			get
			{
				Query qReviews = new Query();
				qReviews.QueryCondition=new And(
					new Q(Thread.Columns.Enabled,true),
					new Q(Thread.Columns.VenueK,K),
					new Q(Thread.Columns.IsReview,true),
					new Q(Thread.Columns.ParentObjectType,Model.Entities.ObjectType.Event)
					);
				qReviews.TableElement = new Join(Thread.Columns.UsrK, Usr.Columns.K);
				qReviews.OrderBy = Thread.NewsOrder;
				qReviews.TopRecords=10;
				ThreadSet tsReviews = new ThreadSet(qReviews);
				return tsReviews;
			}
		}
		#endregion

		#region Links

		public string LinkShort(int maxNameLength)
		{
            return Utilities.Link(Url(), Cambro.Misc.Utility.Snip(Name, maxNameLength), (Name.Length > maxNameLength ? " onmouseover=\"stt('" + HttpUtility.UrlEncodeUnicode(Name).Replace("'", "\\'") + "')\" onmouseout=\"htm();\"" : ""));
		}

		public string LinkShortNewWindow(int maxNameLength)
		{
            return Utilities.Link(Url(), Cambro.Misc.Utility.Snip(Name, maxNameLength), " target=\"_blank\"" + (Name.Length > maxNameLength ? " onmouseover=\"stt('" + HttpUtility.UrlEncodeUnicode(Name).Replace("'", "\\'") + "')\" onmouseout=\"htm();\"" : ""));
		}

		#region AddEventLink
		public string AddEventLink
		{
			get
			{
				return "/pages/events/edit/venuek-" + this.K.ToString();
			}
		}
		#endregion
		#endregion

		#region UpdateTotalComments()
		public void UpdateTotalComments(Transaction transaction)
		{

			Query q = new Query();
			q.QueryCondition = new Q(Thread.Columns.VenueK,this.K);
			q.ExtraSelectElements = ForumStats.ExtraSelectElements;
			q.Columns = new ColumnSet();
			ForumStats cs = new ForumStats(q);
			this.TotalComments=cs.TotalComments;
			this.AverageCommentDateTime=cs.AverageCommentDateTime;
			this.LastPost=cs.LastPost;

			Update(transaction);
			this.Place.UpdateTotalComments(transaction);
		}
		#endregion

		#region DeleteAll(Transaction transaction)
		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;

			//Events
			EventSet es = new EventSet(new Query(new Q(Event.Columns.VenueK,this.K)));
			foreach(Event e in es)
				e.DeleteAll(transaction);

			
			Delete CommentAlertDelete = new Delete(
				TablesEnum.CommentAlert,
				new And(
				new Q(CommentAlert.Columns.ParentObjectK,this.K),
				new Q(CommentAlert.Columns.ParentObjectType,Model.Entities.ObjectType.Venue)
				)
				);
			CommentAlertDelete.Run(transaction);
			
			//Threads
			ThreadSet ts = new ThreadSet(
				new Query(
					new And(
						new Q(Thread.Columns.ParentObjectType,Model.Entities.ObjectType.Venue),
						new Q(Thread.Columns.ParentObjectK,this.K)
					)
				)
			);
			foreach (Thread t in ts)
				t.DeleteAll(transaction);

			//Articles
			ArticleSet ars = new ArticleSet(new Query(new Q(Article.Columns.VenueK, this.K)));
			foreach (Article a in ars)
				a.DeleteAll(transaction);


			Guid oldPic = this.HasPic ? this.Pic : Guid.Empty;
			int oldPicMiscK = this.PicMisc != null ? this.PicMiscK : 0;

			this.Delete(transaction);

			if (oldPic != Guid.Empty)
				Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");

			if (oldPicMiscK > 0)
			{
				Misc m = new Misc(oldPicMiscK);
				m.DeleteAll(transaction);
			}

			this.Place.UpdateTotalEvents(transaction);

			
		}
		#endregion

		#region FriendlyName
		public string FriendlyName
		{
			get
			{
				return this.Name+" in "+this.Place.Name;
			}
		}
		#endregion

		public static Join PlaceJoin
		{
			get
			{
				return new Join(
					new TableElement(TablesEnum.Venue),
					new TableElement(TablesEnum.Place),
					QueryJoinType.Left,
					Venue.Columns.PlaceK,
					Place.Columns.K);
			}
		}
		public static Join CountryJoin
		{
			get
			{
				return new Join(
					Venue.PlaceJoin,
					new TableElement(TablesEnum.Country),
					QueryJoinType.Left,
					Place.Columns.CountryK,
					Country.Columns.K);
			}
		}

		public string FriendlyHtml()
		{
			return FriendlyHtml(true, false);
		}
		public string FriendlyHtml(bool ShowPlace, bool SmallDetails)
		{
			StringBuilder sb = new StringBuilder();
			AppendFriendlyHtml(sb, ShowPlace, SmallDetails);
			return sb.ToString();
		}
		public void AppendFriendlyHtml(StringBuilder sb, bool ShowPlace, bool SmallDetails)
		{
			sb.Append("<a href=\"");
			sb.Append(this.Url());
			sb.Append("\">");
			sb.Append(this.Name);
			sb.Append("</a>");
			if (SmallDetails)
			{
				sb.Append("<small>");
			}
			if (ShowPlace)
			{
				sb.Append(" in <a href=\"");
				sb.Append(this.Place.Url());
				sb.Append("\">");
				sb.Append(this.Place.Name);
				sb.Append("</a>");
			}
			if (SmallDetails)
			{
				sb.Append("</small>");
			}
		}

		#region LinkColumns
		public static ColumnSet LinkColumns
		{
			get
			{
				return new ColumnSet(
					Venue.Columns.K,
					Venue.Columns.Name,
					Venue.Columns.UrlName,
					Venue.Columns.UrlFragment,
					Venue.Columns.PlaceK);
			}
		}
		#endregion

		#region ChangePlace
		public void ChangePlace(int NewPlaceK, bool UpdateChildUrlFragments)
		{
			if (this.PlaceK!=NewPlaceK)
			{
				Place OldPlace = this.Place;
				Place NewPlace = new Place(NewPlaceK);
				
				Update uThreads = new Update();
				uThreads.Table = TablesEnum.Thread;
				uThreads.Where = new Q(Thread.Columns.VenueK,this.K);
				uThreads.Changes.Add(new Assign(Thread.Columns.PlaceK,NewPlace.K));
				uThreads.Changes.Add(new Assign(Thread.Columns.CountryK,NewPlace.CountryK));
				uThreads.Run();

				Update uArticle = new Update();
				uArticle.Table = TablesEnum.Article;
				uArticle.Where = new Q(Article.Columns.VenueK,this.K);
				uArticle.Changes.Add(new Assign(Article.Columns.PlaceK,NewPlace.K));
				uArticle.Changes.Add(new Assign(Article.Columns.CountryK, NewPlace.CountryK));
				uArticle.Run();

				this.PlaceK = NewPlace.K;
				this.Update();

				OldPlace.UpdateTotalComments(null);
				OldPlace.UpdateTotalEvents(null);
				this.Place=null;
				this.UpdateTotalComments(null);
				
				Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Venue, this.K, UpdateChildUrlFragments);
				job.ExecuteAsynchronously();

				this.Place.UpdateTotalEvents(null);
			}
		}
		#endregion

		#region Url
		partial void AfterUpdate(Transaction t)
		{
			if (spatialDataChanged)
			{
				foreach(var child in this.ChildEvents())
				{
					child.CopySpatialDataFrom(this);
					child.Update(t);
				}
			}
		}
		public void UpdateUrlFragment(bool UpdateChildUrlFragments)
		{
			if (!this.UrlFragment.Equals(this.Place.UrlFilterPart))
			{
				this.UrlFragment = this.Place.UrlFilterPart;
				this.Update();
				if (UpdateChildUrlFragments)
				{
					Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Venue, this.K, true);
					job.ExecuteAsynchronously();
				}
			}
		}
		#region CreateUniqueUrlName()
		public void CreateUniqueUrlName(bool UpdateChildUrlFragments)
		{
			string urlName = UrlInfo.GetUrlName(this.Name);
			if (urlName.Length==0)
				urlName = "venue-"+this.K.ToString();
			if (UrlInfo.IsReservedString(urlName))
				urlName = "venue-"+urlName;

			VenueSet vs = null;
			int namePost = 0;
			string newName = urlName;
			while (vs==null || vs.Count>0)
			{
				if (namePost>0)
					newName = urlName+"-"+namePost.ToString();
				Query q = new Query();
				q.NoLock=true;
				q.ReturnCountOnly=true;
				q.QueryCondition=new And(
					new Q(Columns.PlaceK,this.PlaceK),
					new Q(Columns.K,QueryOperator.NotEqualTo,this.K),
					new Q(Columns.UrlName,newName));
				vs = new VenueSet(q);
				namePost++;
			}
			if (!this.UrlName.Equals(newName))
			{
				this.UrlName = newName;
				this.Update();
				if (UpdateChildUrlFragments)
				{
					Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Venue, this.K, true);
					job.ExecuteAsynchronously();
				}
			}
		}
		#endregion
		#region UpdateChildUrlFragments
		public void UpdateChildUrlFragments(bool Cascade)
		{
			Update uEvents = new Update();
			uEvents.Table=TablesEnum.Event;
			uEvents.Changes.Add(new Assign(Event.Columns.UrlFragment,UrlFilterPart));
			uEvents.Where=new Q(Event.Columns.VenueK,this.K);
			uEvents.Run();

			if (Cascade)
			{
				Query q = new Query();
				q.NoLock=true;
				q.QueryCondition=new Q(Event.Columns.VenueK,this.K);
				q.Columns=new ColumnSet(
					Event.Columns.K, 
					Event.Columns.UrlFragment,
					Event.Columns.VenueK,
					Event.Columns.DateTime);
				EventSet es = new EventSet(q);
				foreach (Event e in es)
				{
					try
					{
						Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Event, e.K, true);
						job.ExecuteAsynchronously();
					}
					catch(Exception ex)
					{
						if (Vars.DevEnv)
							throw ex;
					}
				}
			}

			Update uThreads = new Update();
			uThreads.Table=TablesEnum.Thread;
			uThreads.Changes.Add(new Assign(Thread.Columns.UrlFragment,UrlFilterPart));
			uThreads.Where=new And(
				new Q(Thread.Columns.ParentObjectType,Model.Entities.ObjectType.Venue),
				new Q(Thread.Columns.ParentObjectK,this.K));
			uThreads.Run();

			Update uArticles = new Update();
			uArticles.Table=TablesEnum.Article;
			uArticles.Changes.Add(new Assign(Article.Columns.UrlFragment,UrlFilterPart));
			uArticles.Where=new And(
				new Q(Article.Columns.ParentObjectType,Model.Entities.ObjectType.Venue),
				new Q(Article.Columns.ParentObjectK,this.K));
			uArticles.Run();

			if (Cascade)
			{
				Query q = new Query();
				q.NoLock=true;
				q.QueryCondition=new And(
					new Q(Article.Columns.ParentObjectType,Model.Entities.ObjectType.Venue),
					new Q(Article.Columns.ParentObjectK,this.K));
				q.Columns=new ColumnSet(
					Article.Columns.K,
					Article.Columns.UrlFragment,
					Article.Columns.ParentObjectK,
					Article.Columns.ParentObjectType);
				ArticleSet aSet = new ArticleSet(q);
				foreach (Article a in aSet)
				{
					try
					{
						Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Article, a.K, true);
						job.ExecuteAsynchronously();
					}
					catch(Exception ex)
					{
						if (Vars.DevEnv)
							throw ex;
					}
				}
			}
		}
		#endregion
		public string GetUrlFragment()
		{
			return this.Place.UrlFilterPart;
		}
		public string UrlFilterPart
		{
			get
			{
				string urlFragment = "";
				if (Common.Settings.DynamicUrlFragments)
					urlFragment = GetUrlFragment();
				else
					urlFragment = UrlFragment;

				return urlFragment + "/" + this.UrlName;
			}
		}
		public string Url(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart,null,par);
		}
		public string UrlApp(string Application, params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart,Application,par);
		}
		#endregion

		#region Pic
		public bool HasPic
		{
			get
			{
				return !Pic.Equals(Guid.Empty);
			}
		}
		public string PicPath{get{return Storage.Path(Pic);}}
		public string AnyPicPath
		{
			get
			{
				if (HasPic)
					return Storage.Path(Pic);
				else
					return "/gfx/dsi-sign-100.png";
			}
		}
		#endregion

		#region UrlDiscussion
		public string UrlDiscussion(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart,"chat",par);
		}
		#endregion

		#region DetailsHtmlRender
		public string DetailsHtmlRender
		{
			get
			{
				if (this.DetailsPlain)
					return "<div style=\"margin-bottom:13px;\">" + DetailsHtml + "</div>";
				else
				{
					if (IsDescriptionCleanHtml)
						return "<p>" + DetailsHtml.Replace("\n", "<br>") + "</p>";
					if (IsDescriptionText)
						return Helpers.MakeHtml(DetailsHtml);
					else
						return DetailsHtml;
				}
			}
		}
		#endregion

		#region Links to Bobs
		#region Promoter
		public Promoter Promoter
		{
			get
			{
				if (promoter==null && PromoterK>0)
					promoter = new Promoter(PromoterK);
				return promoter;
			}
			set
			{
				promoter = value;
			}
		}
		private Promoter promoter;
		#endregion
		#region Place
		public Place Place
		{
			get
			{
				if (place==null)
					place = new Place(PlaceK,this,Columns.PlaceK);
				return place;
			}
			set
			{
				place=value;
			}
		}
		Place place;
		#endregion
		#region Owner
		public Usr Owner
		{
			get
			{
				if (owner==null)
					owner = new Usr(OwnerUsrK);
				return owner;
			}
		}
		Usr owner;
		#endregion
		#region ModeratorUsr
		public Usr ModeratorUsr
		{
			get
			{
				if (moderatorUsr==null && ModeratorUsrK>0)
					moderatorUsr = new Usr(ModeratorUsrK);
				return moderatorUsr;
			}
			set
			{
				moderatorUsr = value;
			}
		}
		private Usr moderatorUsr;
		#endregion
		#endregion

		#region Links to BobSets

//		#region Threads
//		public ThreadSet Threads
//		{
//			get
//			{
//				if (threads==null)
//				{
//					#region removed
//					/*
//					#region venueQuery
//					string venueQuery=@"
//(
//  SELECT
//  Thread.*
//  FROM 
//  Thread
//    INNER JOIN Event ON Thread.ParentObjectK=Event.K AND Thread.ParentObjectType=2
//  WHERE 
//    Event.VenueK=$
//    AND
//    Thread.Enabled=1
//	AND
//	Thread.Private=0
//)
//UNION
//(
//  SELECT
//  Thread.*
//  FROM
//  Thread
//    INNER JOIN Photo ON Thread.ParentObjectK=Photo.K AND Thread.ParentObjectType=1
//      INNER JOIN Event ON Photo.EventK = Event.K
//  WHERE
//    Event.VenueK=$
//    AND
//    Thread.Enabled=1
//)
//UNION
//(
//  SELECT
//  Thread.*
//  FROM
//  Thread
//  WHERE
//    Thread.ParentObjectK=$
//    AND 
//    Thread.ParentObjectType=3
//    AND
//    Thread.Enabled=1
//)
//ORDER BY 
//Thread.LastPost 
//Desc
//";
//					venueQuery = venueQuery.Replace("$",this.K.ToString());
//					
//					#endregion
//					threads = new ThreadSet (new Query(true,venueQuery));
//					*/
//					#endregion
//					Query q = new Query();
//					q.QueryCondition = new Q(Thread.Columns.VenueK,this.K);
//					q.OrderBy = Thread.Order;
//					threads = new ThreadSet(q);
//				}
//				return threads;
//			}
//			set
//			{
//				threads=value;
//			}
//		}
//		ThreadSet threads;
//		#endregion
		#region Events
		public EventSet Events
		{
			get
			{
				if (events==null)
				{
					events = new EventSet(new Query(new Q(Event.Columns.VenueK, K)));
				}
				return events;
			}
			set
			{
				events = value;
			}
		}
		EventSet events;
		#endregion
		#region GetDayEvents
		public EventSet GetDayEvents(DateTime day)
		{
			Query q = new Query();
			q.NoLock = true;
			q.Columns = new ColumnSet(Event.Columns.K);
			q.QueryCondition = new And( 
				new Q(Event.Columns.VenueK, K),
				new Q(Event.Columns.DateTime, day)
			);
			return new EventSet(q);
		}
		#endregion
		#endregion

		#region IHasParent Members

		public Model.Entities.ObjectType ParentObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Place;
			}
			set
			{
				throw new Exception("Can't set this for Venue type");
			}
		}

		public IBob ParentObject
		{
			get
			{
				return this.Place;
			}
		}

		public int ParentObjectK
		{
			get
			{
				return PlaceK;
			}
			set
			{
				throw new Exception("Can't set this for Venue type");
			}
		}

		#endregion

		#region IHasArchive
		public string UrlArchiveDate(int Year, int Month, int Day, Model.Entities.ArchiveObjectType Type, params string[] par)
		{
			return Vars.GetArchiveUrl(Year,Month,Day,Type,par,UrlFilterPart);
		}
		public string UrlArchive(Model.Entities.ArchiveObjectType type, params string[] par)
		{
			return UrlArchiveDate(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, type, par);
		}
		#endregion

		#region PicMisc and PicPhoto
		#region PicMisc
		public Misc PicMisc
		{
			get
			{
				if (picMisc==null && PicMiscK>0)
					picMisc = new Misc(PicMiscK);
				return picMisc;
			}
			set
			{
				picMisc = value;
			}
		}
		private Misc picMisc;
		#endregion
		#region PicPhoto
		public Photo PicPhoto
		{
			get
			{
				if (picPhoto==null && PicPhotoK>0)
					picPhoto = new Photo(PicPhotoK);
				return picPhoto;
			}
			set
			{
				picPhoto = value;
			}
		}
		private Photo picPhoto;
		#endregion
		#endregion

		#region IReadableReference Members

		public string ReadableReference
		{
			get { return Name; }
		}

		#endregion


		Q IDiscussable.QueryConditionForGettingThreads
		{
			get
			{
				return new Q(Thread.Columns.VenueK, K);
			}
		}
		bool IDiscussable.ShowPrivateThreads { get { return true; } }
		IDiscussable IDiscussable.UsedDiscussable { get { return this; } }
		bool IDiscussable.OnlyShowThreads { get { return false; } }
		public static VenueSet GetTop(int placeK, int top)
		{
			Query qTop = new Query();
			qTop.Columns = new ColumnSet(Venue.Columns.Name, Venue.Columns.K);
			qTop.QueryCondition = new Q(Venue.Columns.PlaceK, placeK);
			qTop.OrderBy = new OrderBy(Venue.Columns.TotalEvents, OrderBy.OrderDirection.Descending);
			qTop.TopRecords = top;
			return new VenueSet(qTop);
		}

		public static VenueSet GetAll(int placeK)
		{
			Query qAll = new Query();
			qAll.Columns = new ColumnSet(Venue.Columns.Name, Venue.Columns.K);
			qAll.OrderBy = new OrderBy(Venue.Columns.Name);
			qAll.QueryCondition = new Q(Venue.Columns.PlaceK, placeK);
			return new VenueSet(qAll);
		}

		public static Venue Add(Usr currentUsr, string name, int? capacity, int placeK, string postcode, bool? regularEvents, Guid? duplicateGuid, string safeDetailsString)
		{
			Venue v = new Venue();


			v.AddedDateTime = DateTime.Now;
			v.Name = Cambro.Web.Helpers.StripHtml(name);
			v.Postcode = Cambro.Web.Helpers.StripHtml(postcode);
			Place selectedPlace = new Place(placeK);
			v.PlaceK = selectedPlace.K;
			v.Capacity = capacity ?? 0;
			v.RegularEvents = regularEvents ?? false;
			v.DetailsHtml = safeDetailsString;
			v.DuplicateGuid = duplicateGuid ?? Guid.NewGuid();

			v.AdminNote += "Venue added by owner " + DateTime.Now.ToString();
			v.OwnerUsrK = currentUsr.K;

			if (!currentUsr.IsSuper)
			{
				v.IsNew = true;
				v.ModeratorUsrK = Usr.GetEventModeratorUsrK();
			}

			v.Update();
			v.CreateUniqueUrlName(false);
			v.UpdateUrlFragment(false);
			v.Update();
			return v;
		}
	}
	#endregion

}

