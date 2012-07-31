
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Cambro.Misc;
using Cambro.Web;
using Common;
using Model.Entities.Properties;
using Js.Controls.EventBox;

namespace Bobs
{

	#region Event
	/// <summary>
	/// One single event - e.g Rhino Sat June 17th 2003 or Glasto 2003 Saturday Dance Tent
	/// </summary>
	[Serializable]
	public partial class Event : IPic, IPage, IName, IReadableReference, IDiscussable, IBobType, IObjectPage, IRelevanceContributor, IArchive, IDeleteAll, IBuyableCredits, IConnectedTo, ILinkable, IBobAsHTML, IHasSpatialData, IHasParent
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Event.Columns.K] as int? ?? 0; }
			set { this[Event.Columns.K] = value; }
		}
		/// <summary>
		/// Name of the event
		/// </summary>
		public override string Name
		{
			get { return (string)this[Event.Columns.Name]; }
			set { this[Event.Columns.Name] = value; }
		}
		/// <summary>
		/// All the details about the event for summary boxes
		/// </summary>
		public override string ShortDetailsHtml
		{
			get { return (string)this[Event.Columns.ShortDetailsHtml]; }
			set { this[Event.Columns.ShortDetailsHtml] = value; }
		}
		/// <summary>
		/// All the details about the event main event page
		/// </summary>
		public override string LongDetailsHtml
		{
			get { return (string)this[Event.Columns.LongDetailsHtml]; }
			set { this[Event.Columns.LongDetailsHtml] = value; }
		}
		/// <summary>
		/// Is the longDetails plain html? - e.g. rendered outsite the yellow box?
		/// </summary>
		public override bool LongDetailsPlain
		{
			get { return (bool)this[Event.Columns.LongDetailsPlain]; }
			set { this[Event.Columns.LongDetailsPlain] = value; }
		}
		/// <summary>
		/// Cropped image 100*100
		/// </summary>
		public override Guid Pic
		{
			get { return Db.GuidConvertor(this[Event.Columns.Pic]); }
			set { this[Event.Columns.Pic] = new SqlGuid(value); }
		}
		/// <summary>
		/// If the owner wants to upload another image after this has been enabled, it is stored here
		/// </summary>
		public override Guid PicNew
		{
			get { return Db.GuidConvertor(this[Event.Columns.PicNew]); }
			set { this[Event.Columns.PicNew] = new SqlGuid(value); }
		}
		/// <summary>
		/// Date that the event takes place
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[Event.Columns.DateTime]; }
			set { this[Event.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Link to one Venue
		/// </summary>
		public override int VenueK
		{
			get { return (int)this[Event.Columns.VenueK]; }
			set
			{
				this.venue = null;
				this[Event.Columns.VenueK] = value;
				this.CopySpatialDataFrom(Venue);
					
			}
		}
		/// <summary>
		/// Note for admins only
		/// </summary>
		public override string AdminNote
		{
			get { return (string)this[Event.Columns.AdminNote]; }
			set { this[Event.Columns.AdminNote] = value; }
		}
		/// <summary>
		/// Capacity of the event (max number of people that can attent)
		/// </summary>
		public override int Capacity
		{
			get { return (int)this[Event.Columns.Capacity]; }
			set { this[Event.Columns.Capacity] = value; }
		}
		/// <summary>
		/// The user that added this event (0 if added by admin)
		/// </summary>
		public override int OwnerUsrK
		{
			get { return (int)this[Event.Columns.OwnerUsrK]; }
			set { owner = null; this[Event.Columns.OwnerUsrK] = value; }
		}
		/// <summary>
		/// The total number of comments
		/// </summary>
		public override int TotalComments
		{
			get { return (int)this[Event.Columns.TotalComments]; }
			set { this[Event.Columns.TotalComments] = value; }
		}
		/// <summary>
		/// The date/time of the last post that was posted in this board (including child objects)
		/// </summary>
		public override DateTime LastPost
		{
			get { return (DateTime)this[Event.Columns.LastPost]; }
			set { this[Event.Columns.LastPost] = value; }
		}
		/// <summary>
		/// The average date.time of all comments posted in this board (including child objects)
		/// </summary>
		public override DateTime AverageCommentDateTime
		{
			get { return (DateTime)this[Event.Columns.AverageCommentDateTime]; }
			set { this[Event.Columns.AverageCommentDateTime] = value; }
		}
		/// <summary>
		/// Total number of photos in the event (live + disabled + new)
		/// </summary>
		public override int TotalPhotos
		{
			get { return (int)this[Event.Columns.TotalPhotos]; }
			set { this[Event.Columns.TotalPhotos] = value; }
		}
		/// <summary>
		/// The number of live photos on this event
		/// </summary>
		public override int LivePhotos
		{
			get { return (int)this[Event.Columns.LivePhotos]; }
			set { this[Event.Columns.LivePhotos] = value; }
		}
		/// <summary>
		/// DateTime that the last photo was made live
		/// </summary>
		public override DateTime LastLivePhoto
		{
			get { return (DateTime)this[Event.Columns.LivePhotos]; }
			set { this[Event.Columns.LivePhotos] = value; }
		}
		/// <summary>
		/// Does the event have a spotter?
		/// </summary>
		public override bool HasSpotter
		{
			get { return (bool)this[Event.Columns.HasSpotter]; }
			set { this[Event.Columns.HasSpotter] = value; }
		}
		/// <summary>
		/// When was the event added to the system?
		/// </summary>
		public override DateTime AddedDateTime
		{
			get { return (DateTime)this[Event.Columns.AddedDateTime]; }
			set { this[Event.Columns.AddedDateTime] = value; }
		}
		/// <summary>
		/// Does the event have a guestlist?
		/// </summary>
		public override bool HasGuestlist
		{
			get { return (bool)this[Event.Columns.HasGuestlist]; }
			set { this[Event.Columns.HasGuestlist] = value; }
		}
		/// <summary>
		/// Whether the guestlist is running - the event owner can disable it when they want it closed
		/// </summary>
		public override bool GuestlistOpen
		{
			get { return (bool)this[Event.Columns.GuestlistOpen]; }
			set { this[Event.Columns.GuestlistOpen] = value; }
		}
		/// <summary>
		/// Once this is set to true, the owner can't open the list again - they've been billed.
		/// </summary>
		public override bool GuestlistFinished
		{
			get { return (bool)this[Event.Columns.GuestlistFinished]; }
			set { this[Event.Columns.GuestlistFinished] = value; }
		}
		/// <summary>
		/// Limit the total number of people on the guestlist?
		/// </summary>
		public override int GuestlistLimit
		{
			get { return (int)this[Event.Columns.GuestlistLimit]; }
			set { this[Event.Columns.GuestlistLimit] = value; }
		}
		/// <summary>
		/// Total number of people currently on the list
		/// </summary>
		public override int GuestlistCount
		{
			get { return (int)this[Event.Columns.GuestlistCount]; }
			set { this[Event.Columns.GuestlistCount] = value; }
		}
		/// <summary>
		/// Html details about the guestlist
		/// </summary>
		public override string GuestlistDetails
		{
			get { return (string)this[Event.Columns.GuestlistDetails]; }
			set { this[Event.Columns.GuestlistDetails] = value; }
		}
		/// <summary>
		/// Promoter that controls this guestlist
		/// </summary>
		public override int GuestlistPromoterK
		{
			get { return (int)this[Event.Columns.GuestlistPromoterK]; }
			set { guestlistPromoter = null; this[Event.Columns.GuestlistPromoterK] = value; }
		}
		/// <summary>
		/// Regular (non-guestlist) price
		/// </summary>
		public override double GuestlistRegularPrice
		{
			get { return (double)this[Event.Columns.GuestlistRegularPrice]; }
			set { this[Event.Columns.GuestlistRegularPrice] = value; }
		}
		/// <summary>
		/// Reduced guestlist price
		/// </summary>
		public override double GuestlistPrice
		{
			get { return (double)this[Event.Columns.GuestlistPrice]; }
			set { this[Event.Columns.GuestlistPrice] = value; }
		}
		/// <summary>
		/// Is the guestlist promoted on the front page of the site?
		/// </summary>
		public override bool GuestlistPromotion
		{
			get { return (bool)this[Event.Columns.GuestlistPromotion]; }
			set { this[Event.Columns.GuestlistPromotion] = value; }
		}
		/// <summary>
		/// When is the event? Morning, Daytime or Evening?
		/// </summary>
		public override StartTimes StartTime
		{
			get { return (StartTimes)this[Event.Columns.StartTime]; }
			set { this[Event.Columns.StartTime] = value; }
		}
		/// <summary>
		/// The email address of the admin contact for sorting spoters with guestlists
		/// </summary>
		public override string AdminEmail
		{
			get { return (string)this[Event.Columns.AdminEmail]; }
			set { this[Event.Columns.AdminEmail] = value; }
		}
		/// <summary>
		/// Donated - highlight the event, and give it a duck
		/// </summary>
		public override bool Donated
		{
			get { return (bool)this[Event.Columns.Donated]; }
			set { this[Event.Columns.Donated] = value; }
		}
		/// <summary>
		/// Is the description text or html?
		/// </summary>
		public override bool IsDescriptionText
		{
			get { return (bool)this[Event.Columns.IsDescriptionText]; }
			set { this[Event.Columns.IsDescriptionText] = value; }
		}
		/// <summary>
		/// Has the event been seen by an admin or not?
		/// </summary>
		public override bool IsNew
		{
			get { return (bool)this[Event.Columns.IsNew]; }
			set { this[Event.Columns.IsNew] = value; }
		}
		/// <summary>
		/// Should the description just have "\n" replaced with "&lt;br&gt;"? (This overrides IsDescriptionText)
		/// </summary>
		public override bool IsDescriptionCleanHtml
		{
			get { return (bool)this[Event.Columns.IsDescriptionCleanHtml]; }
			set { this[Event.Columns.IsDescriptionCleanHtml] = value; }
		}
		/// <summary>
		/// Has the event been recently edited?
		/// </summary>
		public override bool IsEdited
		{
			get { return (bool)this[Event.Columns.IsEdited]; }
			set { this[Event.Columns.IsEdited] = value; }
		}
		/// <summary>
		/// Guid used to ensure duplicate events don't get posted if the user refreshes the page after saving.
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Db.GuidConvertor(this[Event.Columns.DuplicateGuid]); }
			set { this[Event.Columns.DuplicateGuid] = new SqlGuid(value); }
		}
		/// <summary>
		/// State var used to reconstruct cropper when re-cropping
		/// </summary>
		public override string PicState
		{
			get { return (string)this[Event.Columns.PicState]; }
			set { this[Event.Columns.PicState] = value; }
		}
		/// <summary>
		/// The Photo that was used to create the Pic.
		/// </summary>
		public override int PicPhotoK
		{
			get { return (int)this[Event.Columns.PicPhotoK]; }
			set { picPhoto = null; this[Event.Columns.PicPhotoK] = value; }
		}
		/// <summary>
		/// The Misc that was used to create the Pic.
		/// </summary>
		public override int PicMiscK
		{
			get { return (int)this[Event.Columns.PicMiscK]; }
			set { picMisc = null; this[Event.Columns.PicMiscK] = value; }
		}
		/// <summary>
		/// The url fragment - so that the url can be generated without accessing parent database records
		/// </summary>
		public override string UrlFragment
		{
			get { return (string)this[Event.Columns.UrlFragment]; }
			set { this[Event.Columns.UrlFragment] = value; }
		}
		/// <summary>
		/// Music types, comma seperated
		/// </summary>
		public override string MusicTypesString
		{
			get { return (string)this[Event.Columns.MusicTypesString]; }
			set { this[Event.Columns.MusicTypesString] = value; }
		}
		/// <summary>
		/// The moderator that has been assigned to moderate this event
		/// </summary>
		public override int ModeratorUsrK
		{
			get { return (int)this[Event.Columns.ModeratorUsrK]; }
			set { moderatorUsr = null; this[Event.Columns.ModeratorUsrK] = value; }
		}
		/// <summary>
		/// Time stamp to record when someone is trying to purchase an IBuyable item that is linked to this Bob.
		/// </summary>
		public override DateTime BuyableLockDateTime
		{
			get { return (DateTime)this[Event.Columns.BuyableLockDateTime]; }
			set { this[Event.Columns.BuyableLockDateTime] = value; }
		}
		/// <summary>
		/// Are tickets currently available for this event? (Updated hourly)
		/// </summary>
		public override bool IsTicketsAvailable
		{
			get { return (bool)this[Event.Columns.IsTicketsAvailable]; }
			set { this[Event.Columns.IsTicketsAvailable] = value; }
		}
		/// <summary>
		/// How hot are the tickets (Tickets sold in the last 48 hrs x booking fee)
		/// </summary>
		public override double TicketHeat
		{
			get { return (double)this[Event.Columns.TicketHeat]; }
			set { this[Event.Columns.TicketHeat] = value; }
		}
		/// <summary>
		/// Shall we display this event with a hilight?
		/// </summary>
		public override bool HasHilight
		{
			get { return (bool)this[Event.Columns.HasHilight]; }
			set { this[Event.Columns.HasHilight] = value; }
		}
		/// <summary>
		/// Number of our members signed up as going
		/// </summary>
		public override int UsrAttendCount
		{
			get { return (int)this[Event.Columns.UsrAttendCount]; }
			set { this[Event.Columns.UsrAttendCount] = value; }
		}
		/// <summary>
		/// Admin override to fix discount level for price of credits for this event donation
		/// </summary>
		public override double FixedDiscount
		{
			get { return (double)this[Event.Columns.FixedDiscount]; }
			set { this[Event.Columns.FixedDiscount] = value; }
		}
		/// <summary>
		/// Flag to indicate if price is fixed and if to use FixedDiscount
		/// </summary>
		public override bool IsPriceFixed
		{
			get { return (bool)this[Event.Columns.IsPriceFixed]; }
			set { this[Event.Columns.IsPriceFixed] = value; }
		}
		

		/// <summary>
		/// Hierarchical triangular mesh index
		/// </summary>
		long IHasSpatialData.HtmId
		{
			get { throw new NotImplementedException(); }
			
		}
		/// <summary>
		/// Latitude
		/// </summary>
		public override double Lat
		{
			get { return (double) this[Columns.Lat] ; }
			set { this[Columns.Lat] = value; }
		}
		/// <summary>
		/// Longitude
		/// </summary>
		public override double Lon
		{
			get { return (double) this[Columns.Lon] ; }
			set { this[Columns.Lon] = value; }
		}

		public override long HtmId
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}
		/// <summary>
		/// Exclude this event from showing "Find Hotel" banners etc
		/// </summary>
		public override bool? DontShowHotelLink
		{
			get { return (bool?)this[Event.Columns.DontShowHotelLink]; }
			set { this[Event.Columns.DontShowHotelLink] = value; }
		}
		/// <summary>
		/// Display the spotter request panel?
		/// </summary>
		public override bool? SpotterRequest
		{
			get { return (bool?)this[Event.Columns.SpotterRequest]; }
			set { this[Event.Columns.SpotterRequest] = value; }
		}
		/// <summary>
		/// Name for spotter request panel
		/// </summary>
		public override string SpotterRequestName
		{
			get { return (string)this[Event.Columns.SpotterRequestName]; }
			set { this[Event.Columns.SpotterRequestName] = value; }
		}
		/// <summary>
		/// Number for spotter request panel
		/// </summary>
		public override string SpotterRequestNumber
		{
			get { return (string)this[Event.Columns.SpotterRequestNumber]; }
			set { this[Event.Columns.SpotterRequestNumber] = value; }
		}
		/// <summary>
		/// Facebook event id
		/// </summary>
		public override long? FacebookEventId
		{
			get { return (long?)this[Event.Columns.FacebookEventId]; }
			set { this[Event.Columns.FacebookEventId] = value; }
		}
		#endregion

		#region AddEvent
		public static Event AddEvent(
			string name, 
			int venueK, 
			StartTimes? startTime, 
			DateTime date, 
			string shortDetails, 
			string safeLongDetails, 
			Guid? duplicateGuid, 
			int? capacity, 
			Usr usr, 
			int[] musicTypeKs, 
			int[] brandKs,
			bool spotterRequest,
			string spotterRequestName,
			string spotterRequestNumber)
		{
			Event ev = new Event();

			Venue venue = new Venue(venueK);
			Transaction t = null;//new Transaction();
			try
			{
				ev.AddedDateTime = DateTime.Now;
				ev.VenueK = venue.K;
				ev.Name = Cambro.Web.Helpers.StripHtml(name).Trim();
				ev.StartTime = startTime ?? StartTimes.Evening;;
				ev.DateTime = date;
				ev.ShortDetailsHtml = Cambro.Misc.Utility.Snip(Cambro.Web.Helpers.StripHtml(shortDetails ?? ""), 500);

				ev.LongDetailsHtml = safeLongDetails;

				ev.DuplicateGuid = duplicateGuid ?? Guid.NewGuid();

				ev.Capacity = capacity ?? venue.Capacity;

				ev.AdminNote += "Event added by owner " + DateTime.Now.ToString();

				ev.OwnerUsrK = Usr.Current.K;

				ev.SpotterRequest = spotterRequest;
				ev.SpotterRequestName = spotterRequestName;
				ev.SpotterRequestNumber = spotterRequestNumber;

				if (!usr.IsSuper)
				{
					ev.IsNew = true;
					ev.ModeratorUsrK = Usr.GetEventModeratorUsrK();
				}
				ev.InitUrlFragment();
				ev.Update(t);

				foreach (int musicTypeK in musicTypeKs ?? new int[]{})
				{
					MusicType mt = new MusicType(musicTypeK);
					EventMusicType emt = new EventMusicType();
					emt.EventK = ev.K;
					emt.MusicTypeK = mt.K;
					emt.Update(t);
				}
				foreach (int brandK in brandKs ?? new int[] { })
				{
					EventBrand eb = new EventBrand();
					eb.BrandK = brandK;
					eb.EventK = ev.K;
					eb.Update(t);
				}

				ev.UpdateMusicTypesString(t);

				ev.Venue.UpdateTotalEvents(t);
				ev.Owner.UpdateEventCount(t);

				//t.Commit();
				

			}
			catch (Exception ex)
			{
				//t.Rollback();
				ev.DeleteAll(null);
				throw ex;
			}
			finally
			{
				//t.Close();
			}

			Mailer sm = new Mailer();
			sm.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
			sm.UsrRecipient = Usr.Current;
			sm.To = Usr.Current.Email;
			sm.Subject = "You've added an event!";
			sm.Body += "<p>You've added an event to the DontStayIn events database.</p>";
			sm.Body += "<p>Click the link below to view the event:</p>";
			sm.Body += "<p><a href=\"[LOGIN(" + ev.Url() + ")]\">" + HttpUtility.HtmlEncode(ev.FriendlyName) + "</a></p>";
			sm.Body += "<h2>Make changes</h2>";
			sm.Body += "<p>You can make changes or corrections to the event details by clicking the link below:</p>";
			sm.Body += "<p><a href=\"[LOGIN(/event-" + ev.K.ToString() + "/edit)]\">Edit your event</a></p>";
			sm.Body += "<h2>How about a banner advert?</h2>";
			sm.Body += "<p>You can add a banner advert to your event by using the link below:</p>";
			sm.Body += "<p><a href=\"[LOGIN(/pages/bannerpreview/eventk-" + ev.K.ToString() + ")]\">Add a banner</a></p>";
			sm.Body += "<h2>Add photos or a review</h2>";
			sm.Body += "<p>After the event you can upload photos or add a review with the links below:</p>";
			sm.Body += "<p><a href=\"[LOGIN(/pages/galleries/add/eventk-" + ev.K.ToString() + ")]\">Add photos</a> or <a href=\"[LOGIN(" + ev.UrlApp("review") + ")]\">add a review</a></p>";
			sm.Body += "<h2>Are you the event promoter?</h2>";
			sm.Body += "<p>If you organise this event, you can sign up for a FREE promoter account by using the link below:</p>";
			sm.Body += "<p><a href=\"[LOGIN(/pages/promoters/edit)]\">Apply for a promoter account</a></p>";
			sm.RedirectUrl = ev.Url();
			sm.Send();
			return ev;

		}
		#endregion
		#region SendAfterEventReminders
		public static void SendAfterEventReminders()
		{
			Random r = new Random();
			UsrEventAttendedSet ueas;
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Today.AddDays(-1)),
					new Q(Event.Columns.DateTime, QueryOperator.LessThan, DateTime.Today),
					new Q(UsrEventAttended.Columns.SendUpdate, true)
				);
				q.TableElement = new Join(UsrEventAttended.Columns.EventK, Event.Columns.K);
				ueas = new UsrEventAttendedSet(q);
			}

			foreach (UsrEventAttended uea in ueas)
			{
				Console.Write(uea.Usr.NickName);
				try
				{
					
					string subject = "You attended \"" + uea.Event.Name.TruncateWithDots(40) + "\"";
					StringBuilder sb = new StringBuilder();

					sb.Append("<p>Hi ");
					sb.Append(uea.Usr.FirstName);
					sb.Append("</p>");


					sb.Append("<p>You recently attended ");
					uea.Event.AppendFriendlyHtml(sb, true, false, false, true, false, 30, true);
					sb.Append(".</p><p>Don't forget to:</p>");
					sb.Append("<ul>");
					
					sb.Append("<li><a href=\"[LOGIN(");
					sb.Append(uea.Event.UrlGalleryEdit);
					sb.Append(")]\">Upload your photos</a></li>");

					sb.Append("<li><a href=\"[LOGIN(");
					sb.Append(uea.Event.UrlApp("review", ""));
					sb.Append(")]\">Write a review</a></li>");

					sb.Append("<li><a href=\"[LOGIN(");
					sb.Append(uea.Event.UrlApp("chat", ""));
					sb.Append(")]\">Chat about the event</a></li>");

					sb.Append("</ul>");
					

						
					if (uea.Event.EventBrands.Count > 0)
					{
						if (uea.Event.EventBrands.Count == 1)
						{
							sb.Append("<p>You can keep up to date with future events by joining the ");
							sb.Append("<a href=\"[LOGIN(");
							sb.Append(uea.Event.EventBrands[0].Brand.Url());
							sb.Append(")]\">");
							sb.Append(uea.Event.EventBrands[0].Brand.Name);
							sb.Append("</a>");
							sb.Append(" group.</p>");
						}
						else
						{
							sb.Append("<p>You can keep up to date with future events by joining the following groups:</p>");
							sb.Append("<ul>");
							foreach (EventBrand eb in uea.Event.EventBrands)
							{
								sb.Append("<li>");
								sb.Append("<a href=\"[LOGIN(");
								sb.Append(eb.Brand.Url());
								sb.Append(")]\">");
								sb.Append(eb.Brand.Name);
								sb.Append("</a>");
								sb.Append("</li>");
							}
							sb.Append("</ul>");
						}
					}
					sb.Append("<p>We hope you enjoyed the event!</p>");

					Mailer m = new Mailer(r);
					m.Body = sb.ToString();
					m.Bulk = true;
					m.Subject = subject;
					m.UsrRecipient = uea.Usr;
					m.RedirectUrl = uea.Event.Url();
					m.Send();

						
					
					Console.Write("... done.");
					Console.WriteLine();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}

			
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

		#region IBobAsHTML methods
		public string AsHTML()
		{
			string lineReturn = Vars.HTML_LINE_RETURN;
			StringBuilder sb = new StringBuilder();

			sb.Append(lineReturn);
			sb.Append(lineReturn);
			sb.Append("<u>Event details</u>");
			sb.Append(lineReturn);
			sb.Append("K: ");
			sb.Append(this.K.ToString());
			sb.Append(lineReturn);
			sb.Append("Event: ");
			sb.Append(this.FriendlyName);
			sb.Append(lineReturn);
			if (this.Owner != null)
			{
				sb.Append("Owner: ");
				sb.Append(this.Owner.NickName);
				sb.Append(" (K: ");
				sb.Append(this.OwnerUsrK.ToString());
				sb.Append(")");
				sb.Append(lineReturn);
			}
			if (this.TicketRuns.Count > 0)
			{
				sb.Append("Ticket runs:");
				sb.Append(lineReturn);
				foreach (TicketRun tr in this.TicketRuns)
				{
					sb.Append("  ");
					sb.Append(tr.SoldTickets);
					sb.Append(" x ");
					sb.Append(tr.PriceBrandName);
					sb.Append(lineReturn);
				}
			}

			return sb.ToString();
		}
		#endregion

		#region Promoter Html ticks / crosses
		public string PromoterHtmlTickets(Promoter p)
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(TicketRun.Columns.PromoterK, p.K), new Q(TicketRun.Columns.EventK, this.K));
			q.ReturnCountOnly = true;
			TicketRunSet ts = new TicketRunSet(q);
			return TickCrossHtml(ts.Count > 0);
		}
		public string PromoterHtmlBanner(Promoter p)
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(Banner.Columns.PromoterK, p.K), new Q(Banner.Columns.EventK, this.K), Banner.IsBookedQ);
			q.ReturnCountOnly = true;
			BannerSet bs = new BannerSet(q);
			return TickCrossHtml(bs.Count > 0);
		}
		public string PromoterHtmlLeaderboard(Promoter p)
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(Banner.Columns.PromoterK, p.K), new Q(Banner.Columns.EventK, this.K), Banner.IsBookedQ, new Q(Banner.Columns.Position, Banner.Positions.Leaderboard));
			q.ReturnCountOnly = true;
			BannerSet bs = new BannerSet(q);
			return TickCrossHtml(bs.Count > 0);
		}
		public string PromoterHtmlHotbox(Promoter p)
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(Banner.Columns.PromoterK, p.K), new Q(Banner.Columns.EventK, this.K), Banner.IsBookedQ, new Q(Banner.Columns.Position, Banner.Positions.Hotbox));
			q.ReturnCountOnly = true;
			BannerSet bs = new BannerSet(q);
			return TickCrossHtml(bs.Count > 0);
		}
		public string PromoterHtmlSkyscraper(Promoter p)
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(Banner.Columns.PromoterK, p.K), new Q(Banner.Columns.EventK, this.K), Banner.IsBookedQ, new Q(Banner.Columns.Position, Banner.Positions.Skyscraper));
			q.ReturnCountOnly = true;
			BannerSet bs = new BannerSet(q);
			return TickCrossHtml(bs.Count > 0);
		}
		public string PromoterHtmlPhotoBanner(Promoter p)
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(Banner.Columns.PromoterK, p.K), new Q(Banner.Columns.EventK, this.K), Banner.IsBookedQ, new Q(Banner.Columns.Position, Banner.Positions.PhotoBanner));
			q.ReturnCountOnly = true;
			BannerSet bs = new BannerSet(q);
			return TickCrossHtml(bs.Count > 0);
		}
		public string PromoterHtmlEmailBanner(Promoter p)
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(Banner.Columns.PromoterK, p.K), new Q(Banner.Columns.EventK, this.K), Banner.IsBookedQ, new Q(Banner.Columns.Position, Banner.Positions.EmailBanner));
			q.ReturnCountOnly = true;
			BannerSet bs = new BannerSet(q);
			return TickCrossHtml(bs.Count > 0);
		}
		public string PromoterHtmlNews(Promoter p)
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(Thread.Columns.EventK, this.K), new Q(Thread.Columns.IsNews, true), new Q(Brand.Columns.PromoterK, p.K));
			q.TableElement = new Join(
				new TableElement(TablesEnum.Thread),
				new Join(Group.Columns.BrandK, Brand.Columns.K),
				QueryJoinType.Inner,
				new Q(Thread.Columns.GroupK, Group.Columns.K, true)
			);
			q.ReturnCountOnly = true;
			ThreadSet ts = new ThreadSet(q);
			return TickCrossHtml(ts.Count > 0);
		}
		public string PromoterHtmlArticle(Promoter p)
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(Article.Columns.EventK, this.K), Article.EnabledQueryCondition);
			q.ReturnCountOnly = true;
			ArticleSet ars = new ArticleSet(q);
			return TickCrossHtml(ars.Count > 0);
		}
		public string PromoterHtmlCompetition(Promoter p)
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(Comp.Columns.EventK, this.K), new Q(Comp.Columns.Status, Comp.StatusEnum.Enabled));
			q.ReturnCountOnly = true;
			CompSet cs = new CompSet(q);
			return TickCrossHtml(cs.Count > 0);
		}
		public string PromoterHtmlGuestlist(Promoter p)
		{
			return TickCrossHtml(this.HasGuestlist && !this.GuestlistFull);
		}
		public string PromoterHtmlSpotterInvite(Promoter p)
		{
			return TickCrossHtml(this.SpotterRequest.HasValue && this.SpotterRequest.Value);
		}
		public string PromoterHtmlEventDonate(Promoter p)
		{
			return TickCrossHtml(this.HasHilight);
		}
		public string TickCrossHtml(bool tick)
		{
			return Utilities.TickCrossHtml(tick);
		}

		#endregion

		//#region Event info HTML
		//string infoHtml = null;
		//public string InfoHtml
		//{
		//    get { return infoHtml ?? (infoHtml = FriendlyHtml(true, true, true, false)); }
		//}
		//#endregion

		#region JoinedUsrEventAttend
		public UsrEventAttended JoinedUsrEventAttend
		{
			get
			{
				if (joinedUsrEventAttend == null)
				{
					joinedUsrEventAttend = new UsrEventAttended(this, Event.Columns.K);
				}
				return joinedUsrEventAttend;
			}
			set
			{
				joinedUsrEventAttend = value;
			}
		}
		private UsrEventAttended joinedUsrEventAttend;
		#endregion

		#region MergeAndDelete
		public void MergeAndDelete(Event merge)
		{
			if (this.K == merge.K)
				throw new DsiUserFriendlyException("Can't merge event into itself!");

			Cambro.Web.Helpers.WriteAlertHeader();

			//throw new Exception("This function isn't finished yet!");

			Cambro.Web.Helpers.WriteAlert("Starting merge...", 1);

			#region Articles
			if (true)
			{

				Query q = new Query();
				q.QueryCondition = new Q(Article.Columns.EventK, merge.K);
				ArticleSet ars = new ArticleSet(q);
				foreach (Article a in ars)
				{
					Cambro.Web.Helpers.WriteAlert("Merging article " + a.K + "...", 2);
					a.ParentObjectK = this.K;
					a.EventK = this.K;

					if (a.Relevance <= Model.Entities.Article.RelevanceEnum.Venue)
						a.VenueK = this.VenueK;
					else
						a.VenueK = 0;

					if (a.Relevance <= Model.Entities.Article.RelevanceEnum.Place)
						a.PlaceK = this.Venue.PlaceK;
					else
						a.PlaceK = 0;

					if (a.Relevance <= Model.Entities.Article.RelevanceEnum.Country)
						a.CountryK = this.Venue.Place.CountryK;
					else
						a.CountryK = 0;

					a.UrlFragment = this.UrlFilterPartVenueDate;
					a.Update();


					#region Threads
					if (true)
					{
						Update u = new Update();
						u.Table = TablesEnum.Thread;
						u.Where = new Q(Thread.Columns.ArticleK, a.K);
						u.Changes.Add(new Assign(Thread.Columns.UrlFragment, a.UrlFilterPart));
						u.Changes.Add(new Assign(Thread.Columns.EventK, this.K));
						u.Changes.Add(new Assign(Thread.Columns.VenueK, this.VenueK));
						u.Changes.Add(new Assign(Thread.Columns.PlaceK, this.Venue.PlaceK));
						u.Changes.Add(new Assign(Thread.Columns.CountryK, this.Venue.Place.CountryK));
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
				Cambro.Web.Helpers.WriteAlert("Done merging articles...", 2);
			}
			#endregion
			#region Banners
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging banners...", 4);
				Update u = new Update();
				u.Table = TablesEnum.Banner;
				u.Where = new Q(Banner.Columns.EventK, merge.K);
				u.Changes.Add(new Assign(Banner.Columns.EventK, this.K));
				u.Run();
				Cambro.Web.Helpers.WriteAlert("Done merging banners...", 4);
			}
			#endregion
			#region Comp
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging competitions...", 5);
				Update u = new Update();
				u.Table = TablesEnum.Comp;
				u.Where = new Q(Comp.Columns.EventK, merge.K);
				u.Changes.Add(new Assign(Comp.Columns.EventK, this.K));
				u.Run();
				Cambro.Web.Helpers.WriteAlert("Done merging competitions...", 5);
			}
			#endregion
			#region EventMusicType
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging music types...", 6);
				Query q1 = new Query();
				q1.TableElement = MusicType.EventJoin;
				q1.QueryCondition = new Q(Event.Columns.K, merge.K);
				MusicTypeSet ems1 = new MusicTypeSet(q1);

				Query q2 = new Query();
				q2.TableElement = MusicType.EventJoin;
				q2.QueryCondition = new Q(Event.Columns.K, this.K);
				MusicTypeSet ems2 = new MusicTypeSet(q2);

				bool hasAllMuisc = false;
				ArrayList majorTypes = new ArrayList();
				ArrayList relevantTypes = new ArrayList();

				#region find major types
				foreach (MusicType mt in ems1)
				{
					if (mt.K == 1)
					{
						hasAllMuisc = true;
						break;
					}
					if (mt.ParentK == 1 && !majorTypes.Contains(mt.K))
						majorTypes.Add(mt.K);
				}
				if (!hasAllMuisc)
				{
					foreach (MusicType mt in ems2)
					{
						if (mt.K == 1)
						{
							hasAllMuisc = true;
							break;
						}
						if (mt.ParentK == 1 && !majorTypes.Contains(mt.K))
							majorTypes.Add(mt.K);
					}
				}
				#endregion
				if (!hasAllMuisc)
				{
					foreach (MusicType mt in ems1)
					{
						if (!majorTypes.Contains(mt.ParentK) && !relevantTypes.Contains(mt.K))
							relevantTypes.Add(mt.K);
					}
					foreach (MusicType mt in ems2)
					{
						if (!majorTypes.Contains(mt.ParentK) && !relevantTypes.Contains(mt.K))
							relevantTypes.Add(mt.K);
					}
				}
				if (hasAllMuisc || relevantTypes.Count == 0)
				{
					Delete d = new Delete(TablesEnum.EventMusicType, new Q(EventMusicType.Columns.EventK, this.K));
					d.Run();
					EventMusicType emt = new EventMusicType();
					emt.EventK = this.K;
					emt.MusicTypeK = 1;
					emt.Update();
				}
				else
				{
					Delete d = new Delete(TablesEnum.EventMusicType, new Q(EventMusicType.Columns.EventK, this.K));
					d.Run();
					foreach (int mtK in relevantTypes)
					{
						EventMusicType emt = new EventMusicType();
						emt.EventK = this.K;
						emt.MusicTypeK = mtK;
						emt.Update();
					}
				}
				Cambro.Web.Helpers.WriteAlert("Done merging music types...", 6);
			}
			#endregion
			#region EventBrand
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging brands...", 7);
				EventBrandSet ebs = new EventBrandSet(new Query(new Q(EventBrand.Columns.EventK, merge.K)));
				foreach (EventBrand eb in ebs)
				{
					try
					{
						EventBrand ebThis = new EventBrand(this.K, eb.BrandK);
					}
					catch
					{
						EventBrand ebThis = new EventBrand();
						ebThis.EventK = this.K;
						ebThis.BrandK = eb.BrandK;
						ebThis.Update();
					}
				}
				Cambro.Web.Helpers.WriteAlert("Done merging brands...", 7);
			}
			#endregion
			 
			#region Gallery
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging galleries...", 9);
				Update u = new Update();
				u.Table = TablesEnum.Gallery;
				u.Where = new Q(Gallery.Columns.EventK, merge.K);
				u.Changes.Add(new Assign(Gallery.Columns.EventK, this.K));
				u.Changes.Add(new Assign(Gallery.Columns.UrlFragment, this.UrlFilterPartVenueDate));
				u.Run();
				Cambro.Web.Helpers.WriteAlert("Done merging galleries...", 9);
			}
			#endregion
			#region Photo
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging photos...", 10);
				Update u = new Update();
				u.Table = TablesEnum.Photo;
				u.Where = new Q(Photo.Columns.EventK, merge.K);
				u.Changes.Add(new Assign(Photo.Columns.EventK, this.K));
				u.Changes.Add(new Assign(Photo.Columns.UrlFragment, this.UrlFilterPartVenueDate));
				u.Run();
				Cambro.Web.Helpers.WriteAlert("Done merging photos...", 10);
			}
			#endregion
			#region GroupEvent
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging group-event links...", 11);
				GroupEventSet ges = new GroupEventSet(new Query(new Q(GroupEvent.Columns.EventK, merge.K)));
				foreach (GroupEvent ge in ges)
				{
					try
					{
						GroupEvent geThis = new GroupEvent(ge.GroupK, this.K);
					}
					catch
					{
						GroupEvent geThis = new GroupEvent();
						geThis.EventK = this.K;
						geThis.GroupK = ge.GroupK;
						geThis.Update();
					}
				}
				Cambro.Web.Helpers.WriteAlert("Done merging group-event links...", 11);
			}
			#endregion
			#region Thread ParentObjects
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging topics (1/2)...", 12);
				Update u = new Update();
				u.Table = TablesEnum.Thread;
				u.Where = new And(
					new Q(Thread.Columns.ParentObjectType, Model.Entities.ObjectType.Event),
					new Q(Thread.Columns.ParentObjectK, merge.K));
				u.Changes.Add(new Assign(Thread.Columns.ParentObjectK, this.K));
				u.Run();
				Cambro.Web.Helpers.WriteAlert("Done merging topics (1/2)...", 12);
			}
			#endregion
			#region Thread
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging topics (2/2)...", 13);
				Update u = new Update();
				u.Table = TablesEnum.Thread;
				u.Where = new And(
					new Q(Thread.Columns.EventK, merge.K),
					new Q(Thread.Columns.ArticleK, 0));
				u.Changes.Add(new Assign(Thread.Columns.EventK, this.K));
				u.Changes.Add(new Assign(Thread.Columns.UrlFragment, this.UrlFilterPartVenueDate));
				u.Run();
				Cambro.Web.Helpers.WriteAlert("Done merging topics (2/2)...", 13);
			}
			#endregion
			#region UsrEventAttended
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging members...", 14);
				UsrEventAttendedSet ueas = new UsrEventAttendedSet(new Query(new Q(UsrEventAttended.Columns.EventK, merge.K)));
				foreach (UsrEventAttended uea in ueas)
				{
					try
					{
						UsrEventAttended ueaThis = new UsrEventAttended(uea.UsrK, this.K);
						bool changed = false;
						if (!uea.SendUpdate && ueaThis.SendUpdate)
						{
							ueaThis.SendUpdate = false;
							changed = true;
						}
						if (uea.Spotter && !ueaThis.Spotter)
						{
							ueaThis.Spotter = true;
							changed = true;
						}
						if (changed)
							ueaThis.Update();
					}
					catch
					{
						UsrEventAttended ueaThis = new UsrEventAttended();
						ueaThis.EventK = this.K;
						ueaThis.UsrK = uea.UsrK;
						ueaThis.SendUpdate = uea.SendUpdate;
						ueaThis.Spotter = uea.Spotter;
						ueaThis.Update();
					}
				}
				Cambro.Web.Helpers.WriteAlert("Done merging members...", 14);
			}
			#endregion

			if (!this.HasPic)
			{
				Cambro.Web.Helpers.WriteAlert("Merging picture...", 15);
				this.Pic = merge.Pic;
				this.PicMiscK = merge.PicMiscK;
				this.PicPhotoK = merge.PicPhotoK;
				this.PicState = merge.PicState;
				merge.Pic = Guid.Empty;
				merge.PicMiscK = 0;
				merge.PicPhotoK = 0;
				merge.PicState = "";
				merge.Update();
				Cambro.Web.Helpers.WriteAlert("Done merging picture...", 15);
			}

			this.AdminNote += "Event " + merge.K + " was merged with this one " + DateTime.Now.ToString() + ". The admin note from event " + merge.K + " is:\n********************\n" + merge.AdminNote + "\n********************\n";

			if (!this.HasGuestlist && merge.HasGuestlist)
			{
				Cambro.Web.Helpers.WriteAlert("Merging guestlist...", 16);
				this.HasGuestlist = true;
				this.GuestlistOpen = merge.GuestlistOpen;
				this.GuestlistFinished = merge.GuestlistOpen;
				this.GuestlistLimit = merge.GuestlistLimit;
				this.GuestlistCount = merge.GuestlistCount;
				this.GuestlistDetails = merge.GuestlistDetails;
				this.GuestlistPromoterK = merge.GuestlistPromoterK;
				this.GuestlistRegularPrice = merge.GuestlistRegularPrice;
				this.GuestlistPrice = merge.GuestlistPrice;
				this.GuestlistPromotion = merge.GuestlistPromotion;

				Delete d = new Delete(TablesEnum.UsrEventGuestlist, new Q(UsrEventGuestlist.Columns.EventK, this.K));
				d.Run();

				Update u = new Update();
				u.Table = TablesEnum.UsrEventGuestlist;
				u.Where = new Q(UsrEventGuestlist.Columns.EventK, merge.K);
				u.Changes.Add(new Assign(UsrEventGuestlist.Columns.EventK, this.K));
				u.Run();
				Cambro.Web.Helpers.WriteAlert("Done merging guestlist...", 16);
			}

			if (merge.Donated)
				this.Donated = true;

			if (merge.HasHilight)
				this.HasHilight = true;

			this.UpdateUsrAttendCount(false);

			this.Update();

			int mergeVenueK = merge.VenueK;

			Cambro.Web.Helpers.WriteAlert("Deleting old event...", 17);
			merge.DeleteAll(null);
			Cambro.Web.Helpers.WriteAlert("Done deleting old event...", 17);

			if (mergeVenueK != this.VenueK)
			{
				Venue mergeVenue = new Venue(mergeVenueK);
				Cambro.Web.Helpers.WriteAlert("Updating stats for old venue...", 18);
				mergeVenue.UpdateTotalComments(null);
				mergeVenue.UpdateTotalEvents(null);
				Cambro.Web.Helpers.WriteAlert("Done updating stats for old venue...", 18);
			}

			Cambro.Web.Helpers.WriteAlert("Updating stats for new event...", 19);
			this.UpdateMusicTypesStringNoUpdate();
			this.UpdateTotalComments(null);
			this.UpdateHasSpotter(null);
			this.UpdateTotalPhotos(null);
			this.Venue.UpdateTotalEvents(null);
			Cambro.Web.Helpers.WriteAlert("Done updating stats for new event...", 19);

			this.Update();
			Cambro.Web.Helpers.WriteAlert("Done merging events!", 20);


		}
		#endregion

		#region UpdateIsTicketsAvailable(bool update)
		public void UpdateIsTicketsAvailable(bool update)
		{
			foreach (TicketRun tr in this.TicketRuns)
			{
				if (tr.Status.Equals(TicketRun.TicketRunStatus.Running))
				{
					if (!this.IsTicketsAvailable)
					{
						this.IsTicketsAvailable = true;
						if (update)
							this.Update();
					}
					return;
				}
			}
			if (this.IsTicketsAvailable)
			{
				this.IsTicketsAvailable = false;
				if (update)
					this.Update();
			}
		}
		#endregion

		#region UpdateHasHighlight(bool update)
		public void UpdateHasHighlight(bool update)
		{
			bool newHighlight = this.Donated || this.IsTicketsAvailable;
			if (this.HasHilight != newHighlight)
			{
				this.HasHilight = newHighlight;
				if (update)
					this.Update();
			}
		}
		#endregion

		#region UpdateUsrAttendCount(bool update)
		public void UpdateUsrAttendCount(bool update)
		{
			Query q = new Query();
			q.QueryCondition = new Q(UsrEventAttended.Columns.EventK, this.K);
			q.ReturnCountOnly = true;
			UsrEventAttendedSet uus = new UsrEventAttendedSet(q);

			if (uus.Count != this.UsrAttendCount)
			{
				this.UsrAttendCount = uus.Count;
				if (update)
					this.Update();
			}
		}
		#endregion

		#region UpdateUsrAttendCountStatic(int EventK)
		public static void UpdateUsrAttendCountStatic(int EventK)
		{
			Update u = new Update();
			u.Changes.Add(new Assign.Override(Event.Columns.UsrAttendCount, "( SELECT COUNT(*) FROM [UsrEventAttended] WHERE [UsrEventAttended].[EventK] = [Event].[K] )"));
			u.Table = TablesEnum.Event;
			u.Where = new Q(Event.Columns.K, EventK);
			u.Run();
		}
		#endregion

		#region FriendlyLinkColumns
		public static ColumnSet FriendlyLinkColumns
		{
			get
			{
				return new ColumnSet(
					Event.LinkColumns,
					Event.Columns.DateTime,
					Event.Columns.VenueK,
					Venue.Columns.K,
					Venue.LinkColumns,
					Venue.Columns.PlaceK,
					Place.Columns.K,
					Place.LinkColumns,
					Place.Columns.CountryK,
					Country.Columns.K,
					Country.LinkColumns);
			}
		}
		#endregion

		#region LinkColumns
		public static ColumnSet LinkColumns
		{
			get
			{
				return new ColumnSet(
					Columns.K,
					Columns.Name,
					Columns.UrlFragment,
					Columns.VenueK);
			}
		}
		#endregion
		#region StartTimes
		#endregion

		public bool NoPhotos
		{
			get
			{
				if (this.Venue.NoPhotos)
					return true;

				Query q = new Query();
				q.QueryCondition = new And(new Q(EventBrand.Columns.EventK, this.K), new Q(Brand.Columns.NoPhotos, true));
				q.TableElement = new Join(EventBrand.Columns.BrandK, Brand.Columns.K);
				q.ReturnCountOnly = true;
				BrandSet bs = new BrandSet(q);
				if (bs.Count > 0)
					return true;

				return false;
			}
		}

		public static void SendGuestlistReminders()
		{
			Query q = new Query();
			q.NoLock = true;
			q.QueryCondition = new And(new Q(Columns.HasGuestlist, true), new Or(new Q(Columns.DateTime, DateTime.Today), new Q(Columns.DateTime, DateTime.Today.AddDays(1))));
			EventSet es = new EventSet(q);
			foreach (Event e in es)
			{
				try
				{
					if (e.GuestlistPromoter != null)
					{

						foreach (Usr u in e.GuestlistPromoter.AdminUsrs)
						{
							try
							{

								Mailer sm = new Mailer();
								sm.To = u.Email;
								sm.UsrRecipient = u;
								sm.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
								sm.RedirectUrl = e.GuestlistPromoter.UrlApp("guestlists");
								sm.Subject = "You have a DontStayIn guestlist for an event soon";
								sm.Body = "<p>You have a DontStayIn guestlist for an event soon. Please remember to close the list and print out the names before the event!!!</p><p>Click the link below to see the details.</p>";
								sm.Send();
							}
							catch { }
						}
					}
				}
				catch { }
			}
		}

		public void AddRelevant(IRelevanceHolder ContainerPage)
		{
			this.Venue.AddRelevant(ContainerPage);
			foreach (MusicType mt in this.MusicTypes)
			{
				ContainerPage.RelevantMusicAdd(mt.K);
			}
		}

		#region IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		public bool IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		{
			if (objectType.Equals(Model.Entities.ObjectType.Event) && this.K == objectK)
				return true;

			if (objectType.Equals(Model.Entities.ObjectType.Venue) && this.VenueK == objectK)
				return true;

			bool brandCanBeConnected = Brand.CanBeConnectedToStatic(objectType);
			if (objectType.Equals(Model.Entities.ObjectType.Brand) || brandCanBeConnected)
			{
				foreach (Brand b in this.Brands)
				{
					if (objectType.Equals(Model.Entities.ObjectType.Brand) && b.K == objectK)
						return true;

					if (brandCanBeConnected && b.IsConnectedTo(objectType, objectK))
						return true;
				}
			}

			if (Venue.CanBeConnectedToStatic(objectType) && this.Venue.IsConnectedTo(objectType, objectK))
				return true;

			return false;

		}
		public static bool CanBeConnectedToStatic(Model.Entities.ObjectType o)
		{
			if (o.Equals(Model.Entities.ObjectType.Venue) ||
				o.Equals(Model.Entities.ObjectType.Brand))
				return true;

			if (Brand.CanBeConnectedToStatic(o))
				return true;

			if (Venue.CanBeConnectedToStatic(o))
				return true;

			return false;
		}
		public bool CanBeConnectedTo(Model.Entities.ObjectType o)
		{
			return Event.CanBeConnectedToStatic(o);
		}
		#endregion

		#region AssignBrand
		public void AssignBrand(int BrandK, bool Add, Transaction transaction)
		{
			try
			{
				EventBrand eb = new EventBrand(this.K, BrandK);
				if (!Add)
				{
					eb.Delete(transaction);
				}
			}
			catch
			{
				if (Add)
				{
					Brand b = new Brand(BrandK);
					EventBrand eb = new EventBrand();
					eb.EventK = this.K;
					eb.BrandK = b.K;
					eb.Update(transaction);
				}
			}
		}
		#endregion

		#region DeleteAllUsr

		public DeleteReturnStatus DeleteAllUsr(Usr u)
		{
			if (!u.IsSuper && u.K != this.OwnerUsrK)
				return DeleteReturnStatus.FailNoPermission;

			if (this.TotalComments > 10)
			{
				Mailer smComments = new Mailer();
				smComments.Body += "<p><a href=\"http://" + Vars.DomainName + u.Url() + "\">" + u.NickNameSafe + "</a> (" + u.Email + ") attempted to delete event " + this.K + " (<a href=\"http://" + Vars.DomainName + this.Url() + "\">" + this.FriendlyName + "</a>).</p>";
				smComments.Body += "<p>This operation failed because " + this.Name + " has " + this.TotalComments + " comments.</p>";
				smComments.Subject = "Delete event operation failed because too many comments in event";
				smComments.TemplateType = Mailer.TemplateTypes.AdminNote;
				smComments.To = "events@dontstayin.com";
				smComments.Send();
				return DeleteReturnStatus.FailComments;
			}

			if (this.TotalPhotos > 5)
			{
				Mailer smPhotos = new Mailer();
				smPhotos.Body += "<p><a href=\"http://" + Vars.DomainName + u.Url() + "\">" + u.NickNameSafe + "</a> (" + u.Email + ") attempted to delete event " + this.K + " (<a href=\"http://" + Vars.DomainName + this.Url() + "\">" + this.FriendlyName + "</a>).</p>";
				smPhotos.Body += "<p>This operation failed because " + this.Name + " has " + this.TotalPhotos + " photos.</p>";
				smPhotos.Subject = "Delete event operation failed because too many photos in event";
				smPhotos.TemplateType = Mailer.TemplateTypes.AdminNote;
				smPhotos.To = "events@dontstayin.com";
				smPhotos.Send();
				return DeleteReturnStatus.FailPhotos;
			}

			//banners?
			Query qBanners = new Query();
			qBanners.QueryCondition = new Q(Banner.Columns.EventK, this.K);
			qBanners.ReturnCountOnly = true;
			BannerSet bs = new BannerSet(qBanners);
			if (bs.Count > 0)
			{
				Mailer smBanner = new Mailer();
				smBanner.Body += "<p><a href=\"http://" + Vars.DomainName + u.Url() + "\">" + u.NickNameSafe + "</a> (" + u.Email + ") attempted to delete event " + this.K + " (<a href=\"http://" + Vars.DomainName + this.Url() + "\">" + this.FriendlyName + "</a>).</p>";
				smBanner.Body += "<p>This operation failed because " + this.Name + " has " + bs.Count + " banner" + (bs.Count == 1 ? "" : "s") + ".</p>";
				smBanner.Subject = "Delete event operation failed because event has a banner";
				smBanner.TemplateType = Mailer.TemplateTypes.AdminNote;
				smBanner.To = "events@dontstayin.com";
				smBanner.Send();
				return DeleteReturnStatus.FailPromoter;
			}
			//guestlists?
			if (this.HasGuestlist)
			{
				Mailer smGuestlists = new Mailer();
				smGuestlists.Body += "<p><a href=\"http://" + Vars.DomainName + u.Url() + "\">" + u.NickNameSafe + "</a> (" + u.Email + ") attempted to delete event " + this.K + " (<a href=\"http://" + Vars.DomainName + this.Url() + "\">" + this.FriendlyName + "</a>).</p>";
				smGuestlists.Body += "<p>This operation failed because " + this.Name + " has a guestlist.</p>";
				smGuestlists.Subject = "Delete event operation failed because event has a guestlist";
				smGuestlists.TemplateType = Mailer.TemplateTypes.AdminNote;
				smGuestlists.To = "events@dontstayin.com";
				smGuestlists.Send();
				return DeleteReturnStatus.FailPromoter;
			}
			//competitions?
			Query qComp = new Query();
			qComp.QueryCondition = new Q(Comp.Columns.EventK, this.K);
			qComp.ReturnCountOnly = true;
			CompSet cs = new CompSet(qComp);
			if (cs.Count > 0)
			{
				Mailer smComp = new Mailer();
				smComp.Body += "<p><a href=\"http://" + Vars.DomainName + u.Url() + "\">" + u.NickNameSafe + "</a> (" + u.Email + ") attempted to delete event " + this.K + " (<a href=\"http://" + Vars.DomainName + this.Url() + "\">" + this.FriendlyName + "</a>).</p>";
				smComp.Body += "<p>This operation failed because " + this.Name + " has " + cs.Count + " competition" + (cs.Count == 1 ? "" : "s") + ".</p>";
				smComp.Subject = "Delete event operation failed because event has a competition";
				smComp.TemplateType = Mailer.TemplateTypes.AdminNote;
				smComp.To = "events@dontstayin.com";
				smComp.Send();
				return DeleteReturnStatus.FailPromoter;
			}
			//ticket runs?
			if (this.TicketRuns.Count > 0)
			{
				string ticketRuns = (this.TicketRuns.Count > 1 ? "ticket runs" : "a ticket run");
				Mailer smTicketRuns = new Mailer();
				smTicketRuns.Body += "<p><a href=\"http://" + Vars.DomainName + u.Url() + "\">" + u.NickNameSafe + "</a> (" + u.Email + ") attempted to delete event " + this.K + " (<a href=\"http://" + Vars.DomainName + this.Url() + "\">" + this.FriendlyName + "</a>).</p>";
				smTicketRuns.Body += "<p>This operation failed because " + this.Name + " has " + ticketRuns + ".</p>";
				smTicketRuns.Subject = "Delete event operation failed because event has " + ticketRuns;
				smTicketRuns.TemplateType = Mailer.TemplateTypes.AdminNote;
				smTicketRuns.To = "events@dontstayin.com";
				smTicketRuns.Send();
				return DeleteReturnStatus.FailPromoter;
			}
			if (this.Donated)
			{
				Mailer smDonated = new Mailer();
				smDonated.Body += "<p><a href=\"http://" + Vars.DomainName + u.Url() + "\">" + u.NickNameSafe + "</a> (" + u.Email + ") attempted to delete event " + this.K + " (<a href=\"http://" + Vars.DomainName + this.Url() + "\">" + this.FriendlyName + "</a>).</p>";
				smDonated.Body += "<p>This operation failed because the event has donated.</p>";
				smDonated.Subject = "Delete event operation failed because event has a donation";
				smDonated.TemplateType = Mailer.TemplateTypes.AdminNote;
				smDonated.To = "events@dontstayin.com";
				smDonated.Send();
				return DeleteReturnStatus.FailPromoter;
			}

			if (this.TotalPhotos > 5)
			{
				Mailer smPhotos = new Mailer();
				smPhotos.Body += "<p><a href=\"http://" + Vars.DomainName + u.Url() + "\">" + u.NickNameSafe + "</a> (" + u.Email + ") attempted to delete event " + this.K + " (<a href=\"http://" + Vars.DomainName + this.Url() + "\">" + this.FriendlyName + "</a>).</p>";
				smPhotos.Body += "<p>This operation failed because " + this.Name + " has " + this.TotalPhotos + " photos.</p>";
				smPhotos.Subject = "Delete event operation failed because too many photos in event";
				smPhotos.TemplateType = Mailer.TemplateTypes.AdminNote;
				smPhotos.To = "events@dontstayin.com";
				smPhotos.Send();
				return DeleteReturnStatus.FailPhotos;
			}

			try
			{
				Bobs.Delete.DeleteAll(this);
				//Mailer smDone = new Mailer();
				//smDone.Body+="<p><a href=\"http://"+Vars.DomainName+u.Url()+"\">"+u.NickNameSafe+"</a> ("+u.Email+") deleted event "+this.K+" (<a href=\"http://"+Vars.DomainName+this.Url()+"\">"+this.FriendlyName+"</a>).</p>";
				//smDone.Subject="Event "+this.K.ToString()+" deleted";
				//smDone.TemplateType=Mailer.TemplateTypes.AdminNote;
				//smDone.To="events@dontstayin.com";
				//smDone.Send();

				return DeleteReturnStatus.Success;
			}
			catch (Exception ex)
			{
				Mailer smException = new Mailer();
				smException.Body += "<p><a href=\"http://" + Vars.DomainName + u.Url() + "\">" + u.NickNameSafe + "</a> (" + u.Email + ") attempted to delete event " + this.K + " (<a href=\"http://" + Vars.DomainName + this.Url() + "\">" + this.FriendlyName + "</a>).</p>";
				smException.Body += "<p>This operation failed because of an unhandled exception:</p><p>" + ex.ToString() + "</p>";
				smException.Subject = "Delete event operation failed because of exception";
				smException.TemplateType = Mailer.TemplateTypes.AdminNote;
				smException.To = "d.brophy@dontstayin.com";
				smException.Send();
				return DeleteReturnStatus.FailException;
			}
		}
		#endregion

		#region Rollover

		string mouseOverText
		{
			get
			{
				if (HasPic)
					return "stma('" + Pic.ToString() + "');";
				else
					return "";
			}
		}

		public string Rollover
		{
			get
			{
				if (HasPic)
					return "onmouseover=\"" + mouseOverText + "\" onmouseout=\"htm();\"";
				else
					return "";
			}
		}

		public void MakeRollover(HtmlControl c)
		{
			if (HasPic)
			{
				c.Attributes["onmouseover"] = mouseOverText;
				c.Attributes["onmouseout"] = "htm();";
			}
		}
		public void MakeRollover(WebControl c)
		{
			if (HasPic)
			{
				c.Attributes["onmouseover"] = mouseOverText;
				c.Attributes["onmouseout"] = "htm();";
			}
		}
		#endregion

		#region IBobType Members
		public string TypeName
		{
			get
			{
				return "Event";
			}
		}

		public Model.Entities.ObjectType ObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Event;
			}
		}
		#endregion

		#region Guestlist
		public bool GuestlistFull
		{
			get
			{
				return GuestlistCount >= GuestlistLimit;
			}
		}
		public void UpdateGuestlistCount()
		{
			UpdateGuestlistCount(null);
		}
		public void UpdateGuestlistCount(Transaction transaction)
		{
			if (this.HasGuestlist && !this.GuestlistFinished)
			{
				int count = this.GetGuestlistCount();
				if (this.GuestlistCount != count)
				{
					this.GuestlistCount = count;
					this.Update(transaction);
				}
			}
		}
		public int GetGuestlistCount()
		{
			Query q = new Query();
			q.NoLock = true;
			q.ReturnCountOnly = true;
			q.QueryCondition = new Q(UsrEventGuestlist.Columns.EventK, this.K);
			return new UsrEventGuestlistSet(q).Count;
		}
		public string GuestlistOptionsHtml
		{
			get
			{
				Event e = this;

				StringBuilder str = new StringBuilder();
				if (e.GuestlistFinished)
				{
					str.Append(str.Length > 0 ? " | " : "");
					str.Append("Print list: <a href=\"");
					str.Append("/popup/guestlist?EventK=");
					str.Append(e.K);
					str.Append("\" target=\"_blank\">with pictures</a> or <a href=\"");
					str.Append("/popup/guestlist?EventK=");
					str.Append(e.K);
					str.Append("&type=1\" target=\"_blank\">plain</a>");
				}
				else
				{
					str.Append(str.Length > 0 ? " | " : "");
					str.Append("<a href=\"");
					str.Append(this.GuestlistPromoter.UrlApp("guestlists", "mode", "edit", "eventk", e.K.ToString()));
					str.Append("\">Edit</a>");

					if (e.GuestlistOpen)
					{
						str.Append(str.Length > 0 ? " | " : "");
						str.Append("<a href=\"");
						str.Append(this.GuestlistPromoter.UrlApp("guestlists", "mode", "pause", "eventk", e.K.ToString()));
						str.Append("\">Pause</a>");
					}
					else
					{
						str.Append(str.Length > 0 ? " | " : "");
						str.Append("<a href=\"");
						str.Append(this.GuestlistPromoter.UrlApp("guestlists", "mode", "open", "eventk", e.K.ToString()));
						str.Append("\">Open</a>");
					}

					if (e.GuestlistCount > 0)
					{
						str.Append(str.Length > 0 ? " | " : "");
						str.Append("<a href=\"");
						str.Append(this.GuestlistPromoter.UrlApp("guestlists", "mode", "close", "eventk", e.K.ToString()));
						str.Append("\">Close</a>");
					}
					else
					{
						str.Append(str.Length > 0 ? " | " : "");
						str.Append("<a href=\"");
						str.Append(this.GuestlistPromoter.UrlApp("guestlists", "mode", "delete", "eventk", e.K.ToString()));
						str.Append("\">Delete</a>");
					}
				}
				return str.ToString();

			}
		}
		#endregion

		#region UpdateHasSpotter(Transaction transaction)
		public void UpdateHasSpotter(Transaction transaction)
		{
			Query q = new Query();
			q.NoLock = true;
			q.TopRecords = 1;
			q.QueryCondition = new And(new Q(UsrEventAttended.Columns.EventK, this.K), new Q(UsrEventAttended.Columns.Spotter, true));
			UsrEventAttendedSet uess = new UsrEventAttendedSet(q);
			this.HasSpotter = uess.Count > 0;
			this.Update(transaction);
		}
		#endregion

		#region DeleteAll(Transaction transaction)
		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;

			// TicketRuns and Tickets
			this.ticketRuns = null;
			if (this.TicketRuns != null && this.TicketRuns.Count > 0)
			{
				foreach (TicketRun tr in this.TicketRuns)
				{
					if (tr.Status != TicketRun.TicketRunStatus.Ended)
						throw new DsiUserFriendlyException("Cannot delete an event that has ticket runs that have not ended.");

					if (tr.SoldTickets > 0 || tr.Tickets.Count > 0)
						throw new DsiUserFriendlyException("Cannot delete an event that has ticket runs that have sold tickets.");
				}

				Delete TicketRunDelete = new Delete(TablesEnum.TicketRun, new Q(TicketRun.Columns.EventK, this.K));
				TicketRunDelete.Run(transaction);
			}
			//EventBrands
			Delete BrandDelete = new Delete(
				TablesEnum.EventBrand,
				new Q(EventBrand.Columns.EventK, this.K)
				);
			BrandDelete.Run(transaction);

			//UsrEventAttended
			Delete UsrEventAttendedDelete = new Delete(
				TablesEnum.UsrEventAttended,
				new Q(UsrEventAttended.Columns.EventK, this.K)
				);
			UsrEventAttendedDelete.Run(transaction);

			//Galleries
			foreach (Gallery g in this.AllGalleries)
				g.DeleteAll(transaction);

			//Threads
			ThreadSet ts = new ThreadSet(new Query(new And(new Q(Thread.Columns.ParentObjectType, Model.Entities.ObjectType.Event), new Q(Thread.Columns.ParentObjectK, this.K))));
			foreach (Thread t in ts)
				t.DeleteAll(transaction);

			//EventMusicType
			Delete EventMusicTypeDelete = new Delete(
				TablesEnum.EventMusicType,
				new Q(EventMusicType.Columns.EventK, this.K)
			);
			EventMusicTypeDelete.Run(transaction);

			
			//delete CommentAlerts
			Delete CommentAlertDelete = new Delete(
				TablesEnum.CommentAlert,
				new And(
					new Q(CommentAlert.Columns.ParentObjectK, this.K),
					new Q(CommentAlert.Columns.ParentObjectType, Model.Entities.ObjectType.Event)
				)
			);
			CommentAlertDelete.Run(transaction);

			//Articles
			ArticleSet ars = new ArticleSet(new Query(new Q(Article.Columns.EventK, this.K)));
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

			this.Venue.UpdateTotalEvents(transaction);
			this.Owner.UpdateEventCount(transaction);

		}
		#endregion

		#region LastPostFriendlyTime
		public string LastPostFriendlyTime(bool Capital)
		{
			return Utility.FriendlyTime(LastPost, Capital);
		}
		#endregion

		#region SpotterSignUpUrl
		public string SpotterSignUpUrl
		{
			get
			{
				return "/pages/spotters/eventk-" + this.K.ToString();
			}
		}
		#endregion

		#region SpotterUploadUrl
		public string SpotterUploadUrl
		{
			get
			{
				return "/pages/galleries/add/eventk-" + this.K.ToString();
			}
		}
		#endregion

		#region UpdateTotalPhotos
		public void UpdateTotalPhotos(Transaction transaction)
		{
			int totalPhotos = 0;
			int livePhotos = 0;
			foreach (Gallery g in this.AllGalleries)
			{
				totalPhotos += g.TotalPhotos;
				livePhotos += g.LivePhotos;
			}
			this.TotalPhotos = totalPhotos;
			this.LivePhotos = livePhotos;
			this.Update(transaction);
		}
		#endregion

		#region ChangeVenue
		public void ChangeVenue(int NewVenueK, bool UpdateChildUrlFragments)
		{
			if (this.VenueK != NewVenueK)
			{
				Venue OldVenue = this.Venue;
				Venue NewVenue = new Venue(NewVenueK);

				Update uThreads = new Update();
				uThreads.Table = TablesEnum.Thread;
				uThreads.Where = new Q(Thread.Columns.EventK, this.K);
				uThreads.Changes.Add(new Assign(Thread.Columns.VenueK, NewVenue.K));
				uThreads.Changes.Add(new Assign(Thread.Columns.PlaceK, NewVenue.PlaceK));
				uThreads.Changes.Add(new Assign(Thread.Columns.CountryK, NewVenue.Place.CountryK));
				uThreads.Run();

				Update uArticle = new Update();
				uArticle.Table = TablesEnum.Article;
				uArticle.Where = new Q(Article.Columns.EventK, this.K);
				uArticle.Changes.Add(new Assign(Article.Columns.VenueK, NewVenue.K));
				uArticle.Changes.Add(new Assign(Article.Columns.PlaceK, NewVenue.PlaceK));
				uArticle.Changes.Add(new Assign(Article.Columns.CountryK, NewVenue.Place.CountryK));
				uArticle.Run();

				this.VenueK = NewVenue.K;
				this.Update();

				OldVenue.UpdateTotalComments(null);
				this.Venue = null;
				this.UpdateTotalComments(null);

				Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Event, this.K, UpdateChildUrlFragments);
				job.ExecuteAsynchronously();

			}
		}
		#endregion

		#region UpdateTotalComments()
		public void UpdateTotalComments(Transaction transaction)
		{
			Query q = new Query();
			q.QueryCondition = new Q(Thread.Columns.EventK, this.K);
			q.ExtraSelectElements = ForumStats.ExtraSelectElements;
			q.Columns = new ColumnSet();
			ForumStats cs = new ForumStats(q);
			this.TotalComments = cs.TotalComments;
			this.AverageCommentDateTime = cs.AverageCommentDateTime;
			this.LastPost = cs.LastPost;

			Update(transaction);
			foreach (Brand b in this.Brands)
			{
				b.UpdateTotalComments(transaction);
			}
			this.Venue.UpdateTotalComments(transaction);

		}
		#endregion

		#region OrderBy
		public static OrderBy PastEventOrder
		{
			get
			{
				return new OrderBy(
					new OrderBy[] {
						new OrderBy(Columns.DateTime,OrderBy.OrderDirection.Descending), 
						new OrderBy(Columns.StartTime,OrderBy.OrderDirection.Ascending), 
						new OrderBy(Columns.HasHilight,OrderBy.OrderDirection.Descending), 
						new OrderBy(Columns.LivePhotos,OrderBy.OrderDirection.Descending), 
						new OrderBy(Columns.UsrAttendCount,OrderBy.OrderDirection.Descending),
						new OrderBy(Columns.Capacity,OrderBy.OrderDirection.Descending)
					}
				);
			}
		}
		public static OrderBy FutureEventOrder
		{
			get
			{
				return new OrderBy(
					new OrderBy[] {
						new OrderBy(Columns.DateTime,OrderBy.OrderDirection.Ascending), 
						new OrderBy(Columns.StartTime,OrderBy.OrderDirection.Ascending), 
						new OrderBy(Columns.HasHilight,OrderBy.OrderDirection.Descending),
						new OrderBy(Columns.SpotterRequest,OrderBy.OrderDirection.Descending),
						new OrderBy(Columns.UsrAttendCount,OrderBy.OrderDirection.Descending),
						new OrderBy(Columns.Capacity,OrderBy.OrderDirection.Descending)
					}
				);
			}
		}
		#endregion

		#region Url
		public void UpdateChildUrlFragments(bool Cascade)
		{
			Update uGalleries = new Update();
			uGalleries.Table = TablesEnum.Gallery;
			uGalleries.Changes.Add(new Assign(Gallery.Columns.UrlFragment, UrlFilterPartVenueDate));
			uGalleries.Where = new Q(Gallery.Columns.EventK, this.K);
			uGalleries.Run();

			Update uPhotos = new Update();
			uPhotos.Table = TablesEnum.Photo;
			uPhotos.Changes.Add(new Assign(Photo.Columns.UrlFragment, UrlFilterPartVenueDate));
			uPhotos.Where = new Q(Photo.Columns.EventK, this.K);
			uPhotos.Run();

			Update uThreads = new Update();
			uThreads.Table = TablesEnum.Thread;
			uThreads.From = new Join(
				new TableElement(TablesEnum.Thread),
				new TableElement(TablesEnum.Photo),
				QueryJoinType.Left,
				Thread.Columns.PhotoK,
				Photo.Columns.K
			);
			uThreads.Changes.Add(new Assign(Thread.Columns.UrlFragment, UrlFilterPart));
			uThreads.Where = new Or(
				new Q(Photo.Columns.EventK, this.K),
				new And(
					new Q(Thread.Columns.ParentObjectType, Model.Entities.ObjectType.Event),
					new Q(Thread.Columns.ParentObjectK, this.K)
				)
			);
			uThreads.Run();

			Update uArticles = new Update();
			uArticles.Table = TablesEnum.Article;
			uArticles.Changes.Add(new Assign(Article.Columns.UrlFragment, UrlFilterPartVenueDate));
			uArticles.Where = new And(
				new Q(Article.Columns.ParentObjectType, Model.Entities.ObjectType.Event),
				new Q(Article.Columns.ParentObjectK, this.K));
			uArticles.Run();

			if (Cascade)
			{
				Query q = new Query();
				q.NoLock = true;
				q.QueryCondition = new And(
					new Q(Article.Columns.ParentObjectType, Model.Entities.ObjectType.Event),
					new Q(Article.Columns.ParentObjectK, this.K));
				q.Columns = new ColumnSet(
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
					catch (Exception ex)
					{
						if (Vars.DevEnv)
							throw ex;
					}
				}
			}
		}
		public bool InitUrlFragment()
		{
			string oldUrlFragment = this.UrlFragment;
			this.UrlFragment = this.Venue.UrlFilterPart;
			return !oldUrlFragment.Equals(this.Venue.UrlFilterPart);
		}
		public void UpdateUrlFragment(bool UpdateChildUrlFragments)
		{
			bool changed = InitUrlFragment();
			if (changed)
			{
				this.Update();
				if (UpdateChildUrlFragments)
				{
					Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Event, this.K, true);
					job.ExecuteAsynchronously();
				}
			}
		}
		public string GetUrlFragment()
		{
			return this.Venue.UrlFilterPart;
		}
		public string UrlFilterPartVenueDate
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				if (Settings.DynamicUrlFragments)
					sb.Append(GetUrlFragment());
				else
					sb.Append(this.UrlFragment);
				sb.Append("/");
				sb.Append(this.DateTime.Year.ToString("0000"));
				sb.Append("/");
				sb.Append(this.DateTime.ToString("MMM").ToLower());
				sb.Append("/");
				sb.Append(this.DateTime.Day.ToString("00"));
				return sb.ToString();
			}
		}
		public string UrlFilterPartShort
		{
			get
			{
				return "/event-" + this.K.ToString();
			}
		}
		public string UrlFilterPart
		{
			get
			{
				return UrlFilterPartVenueDate + UrlFilterPartShort;
			}
		}
		public string UrlTicketFeedback()
		{
			return UrlTicketFeedback(Ticket.FeedbackEnum.None);
		}

		public string UrlTicketFeedback(Ticket.FeedbackEnum feedback)
		{
			if (feedback == Ticket.FeedbackEnum.None)
				return UrlInfo.MakeUrl(UrlFilterPart, "feedback", null);
			else
				return UrlInfo.MakeUrl(UrlFilterPart, "feedback", "ticketfeedback", (feedback == Ticket.FeedbackEnum.Good ? "true" : "false"));
		}
		public string UrlShort(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPartShort, null, par);
		}
		public string Url(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart, null, par);
		}
		public string UrlApp(string Application, params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart, Application, par);
		}
		#endregion

		#region Joins

		public static Join UsrAttendedJoin
		{
			get
			{
				return new Join(new Join(Columns.K, UsrEventAttended.Columns.EventK), Usr.Columns.K, UsrEventAttended.Columns.UsrK);
			}
		}

		public static Join EventTicketJoin
		{
			get
			{
				return new Join(new Join(Columns.K, TicketRun.Columns.EventK), Ticket.Columns.TicketRunK, TicketRun.Columns.K);
			}
		}

		public static Join EventBrandJoin
		{
			get
			{
				return new Join(Columns.K, EventBrand.Columns.EventK);
			}
		}

		public static Join LeftEventBrandJoin
		{
			get
			{
				return new Join(
					new TableElement(TablesEnum.Event),
					new TableElement(TablesEnum.EventBrand),
					QueryJoinType.Left,
					Columns.K,
					EventBrand.Columns.EventK);
			}
		}

		public static TableElement JoinTo(Group g)
		{
			return JoinTo(new TableElement(TablesEnum.Event), g);
		}
		public static TableElement JoinTo(TableElement joinWhat, Group g)
		{
			if (g.BrandK > 0)
			{
				return new Join(
					joinWhat,
					new TableElement(TablesEnum.EventBrand),
					QueryJoinType.Inner,
					new And(
					new Q(Event.Columns.K, EventBrand.Columns.EventK, true),
					new Q(EventBrand.Columns.BrandK, g.BrandK))
				);
			}
			else
			{
				return new Join(
					joinWhat,
					new TableElement(TablesEnum.GroupEvent),
					QueryJoinType.Inner,
					new And(
					new Q(Event.Columns.K, GroupEvent.Columns.EventK, true),
					new Q(GroupEvent.Columns.GroupK, g.K))
				);
			}
		}

		public static Join GroupJoin
		{
			get
			{
				return new JoinDouble(Columns.K, GroupEvent.Columns.EventK, GroupEvent.Columns.GroupK, Group.Columns.K);
			}
		}
		public static Join LeftGroupJoin
		{
			get
			{
				Join j = new JoinLeft(Event.Columns.K, GroupEvent.Columns.EventK);
				return new Join(
					j,
					new TableElement(TablesEnum.Group),
					QueryJoinType.Left,
					GroupEvent.Columns.GroupK,
					Group.Columns.K);
			}
		}
		public static Join BrandJoin
		{
			get
			{
				return new Join(Event.EventBrandJoin, Brand.Columns.K, EventBrand.Columns.BrandK);
			}
		}
		public static Join LeftBrandJoin
		{
			get
			{
				return new Join(
					Event.LeftEventBrandJoin,
					new TableElement(TablesEnum.Brand),
					QueryJoinType.Left,
					EventBrand.Columns.BrandK,
					Brand.Columns.K);
			}
		}
		public static Join PromoterJoinAll
		{
			get
			{
				return new Join(
					Event.BrandJoin,
					Promoter.Columns.K,
					Brand.Columns.PromoterK
				);
			}
		}
		public static Join PromoterJoin
		{
			get
			{
				return new Join(
					Event.BrandJoin,
					new TableElement(TablesEnum.Promoter),
					QueryJoinType.Inner,
					new And(
						new Q(Brand.Columns.PromoterK, Promoter.Columns.K, true),
						new Q(Brand.Columns.PromoterStatus, Brand.PromoterStatusEnum.Confirmed)
					)
				);
			}
		}

		public static Join LeftBrandAndVenueJoin
		{
			get
			{
				return new Join(
					Event.LeftBrandJoin,
					new TableElement(TablesEnum.Venue),
					QueryJoinType.Left,
					Columns.VenueK,
					Venue.Columns.K);
			}
		}

		public static Join PromoterJoinWithVenue
		{
			get
			{
				return new Join(
					Event.LeftBrandAndVenueJoin,
					new TableElement(TablesEnum.Promoter),
					QueryJoinType.Inner,
					new Or(
						new And(
							new Q(Brand.Columns.PromoterK, Promoter.Columns.K, true),
							new Q(Brand.Columns.PromoterStatus, Brand.PromoterStatusEnum.Confirmed)
						),
						new And(
							new Q(Venue.Columns.PromoterK, Promoter.Columns.K, true),
							new Q(Venue.Columns.PromoterStatus, Venue.PromoterStatusEnum.Confirmed)
						)
					)
				);
			}
		}

		public static Join PromoterJoinAllWithVenue
		{
			get
			{
				return new Join(
					Event.LeftBrandAndVenueJoin,
					new TableElement(TablesEnum.Promoter),
					QueryJoinType.Inner,
					new Or(
						new Q(Brand.Columns.PromoterK, Promoter.Columns.K, true),
						new Q(Venue.Columns.PromoterK, Promoter.Columns.K, true)
					)
				);
			}
		}

		public static Join VenueJoin
		{
			get
			{
				return new JoinLeft(Event.Columns.VenueK, Venue.Columns.K);
			}
		}
		public static Join VenueAllJoin
		{
			get
			{
				return new JoinLeft(Event.Columns.VenueK, Venue.Columns.K);
			}
		}


		public static Join PlaceJoin
		{
			get
			{
				return new Join(
					Event.VenueJoin,
					new TableElement(TablesEnum.Place),
					QueryJoinType.Left,
					Venue.Columns.PlaceK,
					Place.Columns.K);
			}
		}
		public static Join PlaceAllJoin
		{
			get
			{
				return new Join(
					Event.VenueJoin,
					new TableElement(TablesEnum.Place),
					QueryJoinType.Left,
					Venue.Columns.PlaceK,
					Place.Columns.K);
			}
		}
		public static Join CountryAllJoin
		{
			get
			{
				return new Join(
					Event.PlaceAllJoin,
					new TableElement(TablesEnum.Country),
					QueryJoinType.Left,
					Place.Columns.CountryK,
					Country.Columns.K);
			}
		}
		public static TableElement EventCountryJoin(TableElement tIn)
		{
			TableElement t = tIn;
			t = new Join(t, Venue.Columns.K, Event.Columns.VenueK);
			t = new Join(t, Place.Columns.K, Venue.Columns.PlaceK);
			t = new Join(t, Country.Columns.K, Place.Columns.CountryK);
			return t;
		}


		public static Join PlaceAndMusicTypeJoin
		{
			get
			{
				return new Join(Event.PlaceJoin, EventMusicType.Columns.EventK, Columns.K);
			}
		}
		public static Join PlaceAndMusicTypeLeftJoin
		{
			get
			{
				return new Join(
					new Join(
						Event.PlaceJoin,
						new TableElement(TablesEnum.EventMusicType),
						QueryJoinType.Left,
						Columns.K,
						EventMusicType.Columns.EventK),
					new TableElement(TablesEnum.MusicType),
					QueryJoinType.Left,
					EventMusicType.Columns.MusicTypeK,
					MusicType.Columns.K
				);
			}
		}

		public static Join UsrSpotterJoin
		{
			get
			{
				return new Join(
					new Join(
						new TableElement(TablesEnum.Event),
						new TableElement(TablesEnum.UsrEventAttended),
						QueryJoinType.Inner,
						new And(
							new Q(Event.Columns.K, UsrEventAttended.Columns.EventK, true),
							new Q(UsrEventAttended.Columns.Spotter, true))),
					new TableElement(TablesEnum.Usr),
					QueryJoinType.Inner,
					UsrEventAttended.Columns.UsrK,
					Usr.Columns.K);
			}
		}
		#endregion

		#region QueryCondition
		public static Q PreviousEventsQueryCondition
		{
			get
			{
				return new Q(Columns.DateTime, QueryOperator.LessThan, DateTime.Today);
			}
		}
		public static Q FutureEventsQueryCondition
		{
			get
			{
				return new Q(Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Today);
			}
		}
		#endregion

		public static EventSet GetEventSetFromEventBoxKey(string key)
		{
			EventPageStub data = EventPageDetails.GetStubFromKey(key);

			Query q = new Query();

			Q dateQ = new Q(true);
			Q ticketsQ = new Q(true);
			if (data.tabType == TabType.Tickets)
			{
				q.OrderBy = new OrderBy(@"
1.0 * (LOG(2) / LOG((DATEDIFF(wk, GETDATE(), [Event].[DateTime]) * 0.25) + 2)) +
1.0 * (1 - (LOG((1.0 / (([TicketHeat] / 10.0) + 1)) + 1) / LOG(2))) +
1.0 * (1 - (LOG((1.0 / (([Event].[UsrAttendCount] / 30.0) + 1)) + 1) / LOG(2))) --+ 
--1.0 * (1 - (LOG((1.0 / (([Event].[TotalComments] / 100.0) + 1)) + 1) / LOG(2)))
DESC, [Event].[K]", Event.Columns.DateTime, Event.Columns.UsrAttendCount, Event.Columns.TotalComments);

				dateQ = Event.FutureEventsQueryCondition;
				ticketsQ = new Q(Event.Columns.IsTicketsAvailable, true);

			}
			else if (data.tabType == TabType.Future)
			{
				q.OrderBy = new OrderBy(@"
1.0 * (LOG(2) / LOG((DATEDIFF(wk, GETDATE(), [Event].[DateTime]) * 0.25) + 2)) +
1.0 * (1 - (LOG((1.0 / (([Event].[UsrAttendCount] / 30.0) + 1)) + 1) / LOG(2))) --+ 
--1.0 * (1 - (LOG((1.0 / (([Event].[TotalComments] / 100.0) + 1)) + 1) / LOG(2)))
DESC, [Event].[K]", Event.Columns.DateTime, Event.Columns.UsrAttendCount, Event.Columns.TotalComments);

				dateQ = Event.FutureEventsQueryCondition;

			}
			else
			{
				q.OrderBy = new OrderBy(@"
1.0 * (LOG(2) / LOG(0 - (DATEDIFF(wk, GETDATE(), [Event].[DateTime]) * 0.25) + 2)) +
1.0 * (1 - (LOG((1.0 / (([Event].[UsrAttendCount] / 30.0) + 1)) + 1) / LOG(2))) --+ 
--0.5 * (1 - (LOG((1.0 / (([Event].[TotalComments] / 100.0) + 1)) + 1) / LOG(2))) +
--0.5 * (1 - (LOG((1.0 / (([Event].[LivePhotos] / 75.0) + 1)) + 1) / LOG(2))) + 
DESC, [Event].[K]", Event.Columns.DateTime, Event.Columns.UsrAttendCount, Event.Columns.TotalComments, Event.Columns.LivePhotos);

				//q.OrderBy = Event.PastEventOrder;

				dateQ = new And(Event.PreviousEventsQueryCondition, new Q(Event.Columns.DateTime, QueryOperator.GreaterThan, DateTime.Today.AddDays(-60)));
			}

			//supported object types: None, Brand, Country, Place
			Q parentObjectQ;
			TableElement parentObjectTableElement;
			if (data.parentObjectType == Model.Entities.ObjectType.None)
			{
				parentObjectTableElement = new TableElement(TablesEnum.Event);
				parentObjectQ = new Q(true);
			}
			else if (data.parentObjectType == Model.Entities.ObjectType.Country)
			{
				parentObjectTableElement = Event.PlaceAllJoin;
				parentObjectQ = new Q(Place.Columns.CountryK, data.parentObjectK);
			}
			else if (data.parentObjectType == Model.Entities.ObjectType.Place)
			{
				parentObjectTableElement = Event.VenueAllJoin;
				parentObjectQ = new Q(Venue.Columns.PlaceK, data.parentObjectK);
			}
			else if (data.parentObjectType == Model.Entities.ObjectType.Brand)
			{
				parentObjectTableElement = Event.EventBrandJoin;
				parentObjectQ = new Q(EventBrand.Columns.BrandK, data.parentObjectK);
			}
			else
				throw new Exception("Unsupported object type");

			q.TableElement = parentObjectTableElement;

			Q musicTypeQ = new Q(true);
			if (data.musicTypeK > 1)
			{
				List<int> al = new List<int>();
				MusicType mt = new MusicType(data.musicTypeK);
				al.Add(mt.K);
				if (mt.ParentK == 1)
				{
					foreach (MusicType mtChild in mt.Children)
					{
						al.Add(mtChild.K);
					}
				}
				musicTypeQ = new InListQ(EventMusicType.Columns.MusicTypeK, al);

				q.TableElement = new Join(
					parentObjectTableElement,
					new TableElement(TablesEnum.EventMusicType),
					QueryJoinType.Left,
					Event.Columns.K,
					EventMusicType.Columns.EventK);
				q.Distinct = true;
				q.DistinctColumn = Event.Columns.K;

			}

			q.QueryCondition = new And(
				dateQ,
				parentObjectQ,
				musicTypeQ,
				ticketsQ);

			q.CacheDuration = TimeSpan.FromDays(1);

			q.Paging.RecordsPerPage = 8;
			q.Paging.RequestedPageIndex = data.pageIndex;

			EventSet es = new EventSet(q);
			return es;
		}

		#region FriendlyDate
		public string FriendlyDate(bool Capital)
		{
			return Utility.FriendlyDate(DateTime, Capital);
		}
		#endregion

		#region UpdateMusicTypesString
		public void UpdateMusicTypesString(Transaction transaction)
		{
			this.UpdateMusicTypesStringNoUpdate();
			this.Update(transaction);
		}
		public void UpdateMusicTypesStringNoUpdate()
		{
			Query q = new Query();
			q.NoLock = true;
			q.Columns = new ColumnSet(MusicType.Columns.GenericName);
			q.TableElement = new Join(MusicType.Columns.K, EventMusicType.Columns.MusicTypeK);
			q.QueryCondition = new Q(EventMusicType.Columns.EventK, K);
			q.OrderBy = MusicType.OrderBy;
			MusicTypeSet mts = new MusicTypeSet(q);

			string music = "";

			for (int i = 0; i < mts.Count; i++)
			{
				music += (i > 0 ? (i == mts.Count - 1 ? " and " : ", ") : "") + mts[i].GenericName;
			}
			this.MusicTypesString = music;
		}
		#endregion

		#region IsFuture
		public bool IsFuture
		{
			get
			{
				if (this.DateTime.AddDays(1) > DateTime.Now)
					return true;
				else
					return false;
			}
		}
		#endregion

		#region DaysToGo
		public int DaysToGo
		{
			get
			{
				DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
				DateTime dateD = new DateTime(this.DateTime.Year, this.DateTime.Month, this.DateTime.Day);

				TimeSpan ts = now - dateD;
				return -ts.Days;
			}
		}
		#endregion

		#region FriendlyName
		/// <summary>
		/// Don't use this too often in lists etc - it does a bunch of queries.
		/// </summary>
		public string FriendlyName
		{
			get
			{
				return Name + (Venue != null ? " @ " + Venue.Name + (Venue.Place != null ? " in " + Venue.Place.Name : "") : "") + ", " + FriendlyDate(false);
			}
		}
		public string FriendlyNameNoPlace
		{
			get
			{
				return Name + (Venue != null ? " @ " + Venue.Name : "") + ", " + FriendlyDate(false);
			}
		}
		public string FriendlyNameNoVenue
		{
			get
			{
				return Name + ", " + FriendlyDate(false);
			}
		}
		public string FriendlyNameNoDate
		{
			get
			{
				return Name + (Venue != null ? " @ " + Venue.Name + (Venue.Place != null ? " in " + Venue.Place.Name : "") : "");
			}
		}
		public string FriendlyNameNoDateTruncated(int size)
		{
			return Name.TruncateWithDots(size) + (Venue != null ? " @ " + Venue.Name.TruncateWithDots(size) + (Venue.Place != null ? " in " + Venue.Place.Name.TruncateWithDots(size) : "") : "");
		}

		public string FriendlyNameGeneric(bool name, bool venue, bool place, bool date)
		{
			string s = "";

			if (name)
				s += Name;

			if (venue && Venue != null)
			{
				if (s.Length > 0)
					s += " @ ";
				s += Venue.Name;
			}

			if (place && Venue != null && Venue.Place != null)
			{
				if (s.Length > 0)
					s += " in ";
				s += Venue.Place.Name;
			}

			if (date)
			{
				if (s.Length > 0)
					s += ", ";
				s += FriendlyDate(s.Length == 0);
			}

			return s;
		}
		#endregion

		public string FriendlyHtml()
		{
			return FriendlyHtml(true, true, true, true, false, 0);
		}
		public string FriendlyHtml(bool ShowVenue, bool ShowPlace, bool ShowDate, bool SmallDetails)
		{
			StringBuilder sb = new StringBuilder();
			AppendFriendlyHtml(sb, ShowVenue, ShowPlace, true, ShowDate, SmallDetails, 0);
			return sb.ToString();
		}
		public string FriendlyHtml(bool ShowVenue, bool ShowPlace, bool ShowCountryIfNotHome, bool ShowDate, bool SmallDetails)
		{
			StringBuilder sb = new StringBuilder();
			AppendFriendlyHtml(sb, ShowVenue, ShowPlace, ShowCountryIfNotHome, ShowDate, SmallDetails, 0);
			return sb.ToString();
		}
		public string FriendlyHtml(bool ShowVenue, bool ShowPlace, bool ShowCountryIfNotHome, bool ShowDate, bool SmallDetails, int SnipLength)
		{
			StringBuilder sb = new StringBuilder();
			AppendFriendlyHtml(sb, ShowVenue, ShowPlace, ShowCountryIfNotHome, ShowDate, SmallDetails, SnipLength);
			return sb.ToString();
		}
		public void AppendFriendlyHtml(StringBuilder sb, bool ShowVenue, bool ShowPlace, bool ShowCountryIfNotHome, bool ShowDate, bool SmallDetails)
		{
			AppendFriendlyHtml(sb, ShowVenue, ShowPlace, ShowCountryIfNotHome, ShowDate, SmallDetails, 0, false);
		}
		public void AppendFriendlyHtml(StringBuilder sb, bool ShowVenue, bool ShowPlace, bool ShowCountryIfNotHome, bool ShowDate, bool SmallDetails, int SnipLength)
		{
			AppendFriendlyHtml(sb, ShowVenue, ShowPlace, ShowCountryIfNotHome, ShowDate, SmallDetails, SnipLength, false);
		}
		public void AppendFriendlyHtml(StringBuilder sb, bool ShowVenue, bool ShowPlace, bool ShowCountryIfNotHome, bool ShowDate, bool SmallDetails, int SnipLength, bool urlsForEmail)
		{
			sb.Append("<a href=\"");
			if (urlsForEmail) sb.Append("[LOGIN(");
			sb.Append(this.Url());
			if (urlsForEmail) sb.Append(")]");
			sb.Append("\">");
			sb.Append(Cambro.Misc.Utility.Snip(this.Name, SnipLength));
			sb.Append("</a>");
			if (SmallDetails)
			{
				sb.Append("<small>");
			}
			if (ShowVenue)
			{
				sb.Append(" @ <a href=\"");
				if (urlsForEmail) sb.Append("[LOGIN(");
				sb.Append(this.Venue.Url());
				if (urlsForEmail) sb.Append(")]");
				sb.Append("\">");
				sb.Append(Cambro.Misc.Utility.Snip(this.Venue.Name, SnipLength));
				sb.Append("</a>");
			}
			if (ShowPlace)
			{

				sb.Append(" in <a href=\""); 
				if (urlsForEmail) sb.Append("[LOGIN(");
				sb.Append(this.Venue.Place.Url());
				if (urlsForEmail) sb.Append(")]");
				sb.Append("\">");
				if (ShowCountryIfNotHome)
				{
					sb.Append(Cambro.Misc.Utility.Snip(this.Venue.Place.Name, SnipLength));
				}
				else
				{
					sb.Append(Cambro.Misc.Utility.Snip(this.Venue.Place.NamePlain, SnipLength));
				}
				sb.Append("</a>");

			}

			if (ShowDate)
			{
				sb.Append(", " + this.FriendlyDate(false));
			}
			if (SmallDetails)
			{
				sb.Append("</small>");
			}
		}

		public string EventListFriendlyHtml(bool ShowVenue, bool ShowPlace, bool ShowCountryIfNotHome, bool ShowDate)
		{
			StringBuilder sb = new StringBuilder();
			AppendEventListFriendlyHtml(sb, ShowVenue, ShowPlace, ShowCountryIfNotHome, ShowDate);
			return sb.ToString();
		}
		public void AppendEventListFriendlyHtml(StringBuilder sb, bool ShowVenue, bool ShowPlace, bool ShowCountryIfNotHome, bool ShowDate)
		{
			if (ShowVenue)
			{
				sb.Append(" @ <a href=\"");
				sb.Append(this.Venue.Url());
				sb.Append("\">");
				sb.Append(this.Venue.Name);
				sb.Append("</a>");
			}
			if (ShowPlace)
			{
				sb.Append(" in <a href=\"");
				sb.Append(this.Venue.Place.Url());
				sb.Append("\">");
				if (ShowCountryIfNotHome)
				{
					sb.Append(this.Venue.Place.Name);
				}
				else
				{
					sb.Append(this.Venue.Place.NamePlain);
				}
				sb.Append("</a>");
			}
			if (ShowDate)
			{
				if (ShowVenue || ShowPlace)
					sb.Append(" - ");

				sb.Append(this.FriendlyDate(false));
			}
		}

		#region DbComboName
		public string DbComboName
		{
			get
			{
				return Name + " @ " + Venue.Name + " in " + Venue.Place.Name + ", " + DateTime.ToShortDateString();
			}
		}
		#endregion

		#region TitleNoteHtml
		public string TitleNoteHtml
		{
			get
			{
				string html = "";

				if (DateTime.Now.Date == this.DateTime.Date)
					html += " <b class=\"ForegroundAttentionRed\">Today!</b> ";
				else if (this.DateTime.Day == 31 && this.DateTime.Month == 12)
					html += " <b class=\"ForegroundAttentionRed\">NYE!</b> ";
				else if (this.DateTime.Day == 24 && this.DateTime.Month == 12)
					html += " <b class=\"ForegroundAttentionRed\">Xmas eve!</b> ";
				else if (IsFuture && DaysToGo < 3)
					html += " <b class=\"ForegroundAttentionRed\">Soon!</b> ";

				if (!StartTime.Equals(StartTimes.Evening))
				{
					if (StartTime.Equals(StartTimes.Daytime))
						html += " <b class=\"ForegroundAttentionBlue\">Daytime</b> ";
					else if (StartTime.Equals(StartTimes.Morning))
						html += " <b class=\"ForegroundAttentionBlue\">Morning</b> ";
				}

				if (!IsFuture && this.LivePhotos > 0)
				{
					html += " <b class=\"ForegroundAttentionRed\">" + this.LivePhotos.ToString() + " photos!</b> ";
				}

				return html;

			}
		}
		public string TitleNoteHtmlCalendar
		{
			get
			{
				string html = "";

				if (!StartTime.Equals(StartTimes.Evening))
				{
					if (StartTime.Equals(StartTimes.Daytime))
						html += " <b class=\"ForegroundAttentionBlue\">Daytime</b> ";
					else if (StartTime.Equals(StartTimes.Morning))
						html += " <b class=\"ForegroundAttentionBlue\">Morning</b> ";
				}

				if (!IsFuture && this.LivePhotos > 0)
				{
					html += " <b class=\"ForegroundAttentionRed\">" + this.LivePhotos.ToString() + " photos!</b> ";
				}

				return html;

			}
		}
		#endregion

		#region Last10Reviews
		public ThreadSet Last10Reviews
		{
			get
			{
				Query qReviews = new Query();
				qReviews.QueryCondition = new And(
					new Q(Thread.Columns.Enabled, true),
					new Q(Thread.Columns.EventK, this.K),
					new Q(Thread.Columns.IsReview, true),
					new Q(Thread.Columns.ParentObjectType, Model.Entities.ObjectType.Event),
					new Q(Thread.Columns.ParentObjectK, this.K)
					);
				qReviews.TableElement = new Join(Thread.Columns.UsrK, Usr.Columns.K);
				qReviews.OrderBy = Thread.NewsOrder;
				qReviews.TopRecords = 10;
				ThreadSet tsReviews = new ThreadSet(qReviews);
				return tsReviews;
			}
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
		public string PicPath { get { return Storage.Path(Pic); } }
		#endregion

		#region AnyPic
		public bool HasAnyPic
		{
			get
			{
				return (HasPic || Venue.HasPic || Venue.Place.HasPic);
			}
		}
		public Guid AnyPic
		{
			get
			{
				if (HasPic)
					return Pic;
				else if (Venue.HasPic)
					return Venue.Pic;
				else if (Venue.Place.HasPic)
					return Venue.Place.Pic;
				else
					return Guid.Empty;
			}
		}
		public string AnyPicPath
		{
			get
			{
				if (HasPic)
					return PicPath;
				else if (Venue.HasPic)
					return Venue.PicPath;
				else if (Venue.Place.HasPic)
					return Venue.Place.PicPath;
				else
					return "/gfx/dsi-sign-100.png";
			}
		}
		#endregion

		#region UrlDiscussion
		public string UrlDiscussion(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart, "chat", par);
		}
		#endregion

		public string GuestlistDetailsString
		{
			get
			{
				string str = "";
				if (GuestlistPrice != GuestlistRegularPrice)
				{
					if (GuestlistPrice == 0.0)
						str += "Free entry (normal price " + GuestlistRegularPrice.ToString("�0.##") + ").";
					else
						str += GuestlistPrice.ToString("�0.##") + " entry (normally " + GuestlistRegularPrice.ToString("�0.##") + ").";
				}
				if (GuestlistDetails.Length > 0)
					str += (str.Length > 0 ? " " : "") + GuestlistDetails + (GuestlistDetails.EndsWith(".") ? "" : ".");
				return str;
			}
		}

		public string ShortDetailsHtmlGeneric(bool addLineBreaks, int shortenTo, bool addParagraphTags)
		{
			return Helpers.MakeHtml(ShortDetailsHtml, addParagraphTags, addLineBreaks, shortenTo);
		}

		public string ShortDetailsHtmlRender
		{
			get
			{
				return Helpers.MakeHtml(ShortDetailsHtml, true, true, 0);
			}
		}

		public string LongDetailsHtmlRender
		{
			get
			{
				if (this.LongDetailsPlain)
					return "<div style=\"margin-bottom:13px;\">" + LongDetailsHtml + "</div>";
				else
				{
					if (IsDescriptionCleanHtml)
						return "<p>" + LongDetailsHtml.Replace("\n", "<br>") + "</p>";
					else if (IsDescriptionText)
						return Helpers.MakeHtml(LongDetailsHtml);
					else
						return LongDetailsHtml;
				}
			}
		}

		public bool IsBrand(int brandK)
		{
			foreach (Brand b in this.Brands)
			{
				if (b.K == brandK)
					return true;
			}
			return false;
		}
		public bool IsPromoter(int promoterK)
		{
			foreach (Brand b in this.Brands)
			{
				if (b.PromoterK == promoterK)
					return true;
			}
			if (this.Venue.PromoterK == promoterK)
				return true;
			return false;
		}
		public bool IsPromoter(Usr user)
		{
			foreach (Brand b in this.Brands)
			{
				if (user.IsPromoterK(b.PromoterK))
					return true;
			}
			if (user.IsPromoterK(this.Venue.PromoterK))
				return true;
			return false;
		}
		public bool IsConfirmedPromoter(int promoterK)
		{
			foreach (Brand b in this.Brands)
			{
				if (b.PromoterK == promoterK && b.PromoterStatus.Equals(Brand.PromoterStatusEnum.Confirmed))
					return true;
			}
			if (this.Venue.PromoterK == promoterK && this.Venue.PromoterStatus.Equals(Venue.PromoterStatusEnum.Confirmed))
				return true;

			return false;
		}

		public string ToStyledHtml()
		{
			return "<span class=\"EventDate\"><nobr>" + this.DateTime.ToString("MMM dd, yy") + "</nobr></span><span class=\"EventLinkSpacer\">&nbsp;:&nbsp;</span><span class=\"EventName\">" + this.Name + "</span><span class=\"ClubName\"> @ " + this.Venue.Name + "</span><span class=\"CityName\"> in " + this.Venue.Place.Name + "</span>";
		}

		#region Links to Bobs
		#region Venue
		public Venue Venue
		{
			get
			{
				if (venue == null)
					venue = new Venue(VenueK, this, Event.Columns.VenueK);
				return venue;
			}
			set
			{
				venue = value;
			}
		}
		Venue venue;
		#endregion
		#region Owner
		public Usr Owner
		{
			get
			{
				if (owner == null)
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
				if (moderatorUsr == null && ModeratorUsrK > 0)
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
		#region GuestlistPromoter
		public Promoter GuestlistPromoter
		{
			get
			{
				if (guestlistPromoter == null && GuestlistPromoterK > 0)
					guestlistPromoter = new Promoter(GuestlistPromoterK);
				return guestlistPromoter;
			}
			set
			{
				guestlistPromoter = value;
			}
		}
		private Promoter guestlistPromoter;
		#endregion
		#region TicketRuns
		public TicketRunSet TicketRuns
		{
			get
			{
				if (ticketRuns == null)
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition = new Q(TicketRun.Columns.EventK, this.K);
					q.OrderBy = new OrderBy(new OrderBy(TicketRun.Columns.ListOrder), new OrderBy(TicketRun.Columns.Price));
					ticketRuns = new TicketRunSet(q);
				}
				return ticketRuns;
			}
		}
		private TicketRunSet ticketRuns;

		public List<TicketRun> RunningTicketRuns
		{
			get
			{
				if (runningTicketRuns.Count == 0)
				{
					foreach (TicketRun ticketRun in TicketRuns)
					{
						if (ticketRun.Status.Equals(TicketRun.TicketRunStatus.Running))
							runningTicketRuns.Add(ticketRun);
					}
				}
				return runningTicketRuns;
			}
		}
		private List<TicketRun> runningTicketRuns = new List<TicketRun>();

		public bool HasRunningTicketRuns
		{
			get
			{
				return RunningTicketRuns.Count > 0;
			}
		}

		public bool HaveTicketRunsSoldOut
		{
			get
			{
				foreach (TicketRun ticketRun in TicketRuns)
				{
					if (!ticketRun.Status.Equals(TicketRun.TicketRunStatus.SoldOut))
						return false;
				}
				return true;
			}
		}

		public bool HaveTicketRunsFinished
		{
			get
			{
				foreach (TicketRun ticketRun in TicketRuns)
				{
					if (!(ticketRun.Status.Equals(TicketRun.TicketRunStatus.SoldOut) || ticketRun.Status.Equals(TicketRun.TicketRunStatus.Ended)))
						return false;
				}
				return true;
			}
		}

		public int TicketRunBrandCount
		{
			get
			{
				if (ticketRunBrandCount == -1)
				{
					ticketRunBrandCount = 0;
					if (ticketRuns == null)
					{
						Query q = new Query();
						q.Columns = new ColumnSet(TicketRun.Columns.BrandK);
						q.DistinctColumn = TicketRun.Columns.BrandK;
						q.Distinct = true;
						q.QueryCondition = new Q(TicketRun.Columns.EventK, this.K);
						q.GroupBy = new GroupBy(TicketRun.Columns.BrandK);
						q.ReturnCountOnly = true;
						ticketRunBrandCount = new TicketRunSet(q).Count;
					}
					else
					{
						List<int> brandKs = new List<int>();
						foreach (TicketRun ticketRun in TicketRuns)
						{
							if (ticketRun.BrandK > 0 && !brandKs.Contains(ticketRun.BrandK))
								brandKs.Add(ticketRun.BrandK);
						}
						ticketRunBrandCount = brandKs.Count;
					}
				}

				return ticketRunBrandCount;
			}
		}
		private int ticketRunBrandCount = -1;

		public List<TicketRun> RunningTicketRunsForPromoter(int promoterK)
		{
			foreach (TicketRun ticketRun in TicketRuns)
			{
				if (ticketRun.Status.Equals(TicketRun.TicketRunStatus.Running) && ticketRun.PromoterK == promoterK)
					runningTicketRuns.Add(ticketRun);
			}
			return runningTicketRuns;
		}

		public TicketSet TicketsSold()
		{
			Query ticketQuery = new Query(new And(new Q(Ticket.Columns.EventK, this.K),
												  Ticket.SoldTicketsQ));
			ticketQuery.OrderBy = new OrderBy(Ticket.Columns.K);

			return new TicketSet(ticketQuery);
		}

		public TicketSet TicketsSold(int promoterK)
		{
			Query ticketQuery = new Query(new And(new Q(Ticket.Columns.EventK, this.K),
												  new Q(TicketRun.Columns.PromoterK, promoterK),
												  Ticket.SoldTicketsQ));
			ticketQuery.OrderBy = new OrderBy(Ticket.Columns.K);
			ticketQuery.TableElement = new Join(Ticket.Columns.TicketRunK, TicketRun.Columns.K);

			return new TicketSet(ticketQuery);
		}

		public TicketSet TicketsAwaitingPayment()
		{
			Query ticketQuery = new Query(new And(new Q(Ticket.Columns.EventK, this.K),
												  new Q(Ticket.Columns.ReserveDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddSeconds(-1 * Vars.TICKETS_RESERVE_SECONDS)),
												  new Q(Ticket.Columns.Enabled, 0)));
			return new TicketSet(ticketQuery);
		}

		public TicketSet TicketsAwaitingPaymentForUsr(Usr usr)
		{
			Query ticketQuery = new Query(new And(new Q(Ticket.Columns.EventK, this.K),
												  new Q(Ticket.Columns.BuyerUsrK, usr.K),
												  new Q(Ticket.Columns.ReserveDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddSeconds(-1 * Vars.TICKETS_RESERVE_SECONDS)),
												  new Q(Ticket.Columns.Enabled, 0)));
			return new TicketSet(ticketQuery);
		}

		public int TicketsAwaitingPaymentForUsrTotal(int usrK)
		{
			Query ticketQuery = new Query(new And(new Q(Ticket.Columns.EventK, this.K),
												  new Q(Ticket.Columns.BuyerUsrK, usrK),
												  new Q(Ticket.Columns.ReserveDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddSeconds(-1 * Vars.TICKETS_RESERVE_SECONDS)),
												  new Q(Ticket.Columns.Enabled, 0)));
			ticketQuery.ExtraSelectElements.Add("SumAwaitingPaymentTickets", "SUM([Ticket].[Quantity])");
			ticketQuery.Columns = new ColumnSet();

			TicketSet tickets = new TicketSet(ticketQuery);
			if (tickets.Count > 0 && tickets[0].ExtraSelectElements["SumAwaitingPaymentTickets"] != DBNull.Value)
				return Convert.ToInt32(tickets[0].ExtraSelectElements["SumAwaitingPaymentTickets"]);
			else
				return 0;
		}

		public int TicketsAwaitingPaymentForCardTotal(Guid cardNumberHash)
		{
			Query ticketQuery = new Query(new And(new Q(Ticket.Columns.EventK, this.K),
												  new Q(Ticket.Columns.CardNumberHash, cardNumberHash),
												  new Q(Ticket.Columns.ReserveDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddSeconds(-1 * Vars.TICKETS_RESERVE_SECONDS)),
												  new Q(Ticket.Columns.Enabled, 0)));
			ticketQuery.ExtraSelectElements.Add("SumAwaitingPaymentTickets", "SUM([Ticket].[Quantity])");
			ticketQuery.Columns = new ColumnSet();

			TicketSet tickets = new TicketSet(ticketQuery);
			if (tickets.Count > 0 && tickets[0].ExtraSelectElements["SumAwaitingPaymentTickets"] != DBNull.Value)
				return Convert.ToInt32(tickets[0].ExtraSelectElements["SumAwaitingPaymentTickets"]);
			else
				return 0;
		}

		public int TicketsSoldTotal
		{
			get
			{
				if (ticketsSoldTotal == 0)
				{
					if (ticketRuns == null)
					{
						Query q = new Query();
						q.ExtraSelectElements.Add("SumSoldTickets", "SUM([TicketRun].[SoldTickets])");
						q.Columns = new ColumnSet();
						q.QueryCondition = new Q(TicketRun.Columns.EventK, this.K);

						TicketRunSet tickets = new TicketRunSet(q);
						if (tickets.Count > 0 && tickets[0].ExtraSelectElements["SumSoldTickets"] != DBNull.Value)
							ticketsSoldTotal = Convert.ToInt32(tickets[0].ExtraSelectElements["SumSoldTickets"]);
					}
					else
					{
						foreach (TicketRun ticketRun in TicketRuns)
							ticketsSoldTotal += ticketRun.SoldTickets;
					}
				}
				return ticketsSoldTotal;
			}
		}
		private int ticketsSoldTotal = 0;

		public int TicketsSoldForUsr(int usrK)
		{
			Q queryCondition = new Q(Ticket.Columns.BuyerUsrK, usrK);
			return TicketsSold(queryCondition);
		}

		public int TicketsSoldForCard(Guid cardNumberHash)
		{
			Q queryCondition = new Q(Ticket.Columns.CardNumberHash, cardNumberHash);
			return TicketsSold(queryCondition);
		}

		private int TicketsSold(Q queryCondition)
		{
			Query q = new Query(new And(new Q(Ticket.Columns.EventK, this.K),
										Ticket.SoldTicketsQ,
										queryCondition));
			q.ExtraSelectElements.Add("SumTickets", "SUM([Ticket].[Quantity])");
			q.Columns = new ColumnSet();

			TicketSet tickets = new TicketSet(q);
			if (tickets.Count > 0 && tickets[0].ExtraSelectElements["SumTickets"] != DBNull.Value)
				return Convert.ToInt32(tickets[0].ExtraSelectElements["SumTickets"]);
			else
				return 0;
		}

		#endregion
		#region Brands
		public BrandSet Brands
		{
			get
			{
				if (brands == null)
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition = new Q(Columns.K, this.K);
					q.OrderBy = new OrderBy(Brand.Columns.Name);
					q.TableElement = Brand.EventJoin;
					brands = new BrandSet(q);
				}
				return brands;
			}
			set
			{
				brands = value;
			}
		}
		private BrandSet brands;
		public string BrandsHtml
		{
			get
			{
				Query q = new Query();
				q.QueryCondition = new Q(EventBrand.Columns.EventK, this.K);
				q.OrderBy = new OrderBy(Brand.Columns.Name);
				q.TableElement = new Join(Brand.Columns.K, EventBrand.Columns.BrandK);
				q.Columns = Brand.LinkColumns;
				BrandSet bs = new BrandSet(q);
				StringBuilder sb = new StringBuilder();
				bs.WriteLinks(sb);
				return sb.ToString();
			}
		}
		#endregion

		#region GuestlistUsrs
		public UsrSet GuestlistUsrs
		{
			get
			{
				if (guestlistUsrs == null)
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition = new Q(UsrEventGuestlist.Columns.EventK, this.K);
					q.OrderBy = new OrderBy(new OrderBy(Usr.Columns.FirstName), new OrderBy(Usr.Columns.LastName));
					q.TableElement = new Join(Usr.Columns.K, UsrEventGuestlist.Columns.UsrK);
					guestlistUsrs = new UsrSet(q);
				}
				return guestlistUsrs;
			}
			set
			{
				guestlistUsrs = value;
			}
		}
		private UsrSet guestlistUsrs;
		#endregion

		#region Galleries
		public GallerySet GalleriesWithJoinedGalleryUsr(OrderBy orderBy, int currentUsrK)
		{
			if (galleries == null)
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Q(Gallery.Columns.EventK, K), 
					//new Or(
						new Q(Gallery.Columns.LivePhotos, QueryOperator.GreaterThan, 0)//,
						//new Q(Gallery.Columns.OwnerUsrK, currentUsrK)
					//)
				);
				//q.TableElement = Gallery.OwnerJoin;
				//q.OrderBy = new OrderBy(" (CASE WHEN [Usr].[SpotterK]>0 THEN 1 ELSE 0 END) DESC, [Usr].[LoginCount] DESC ");
				q.OrderBy = orderBy;
				if (currentUsrK > 0)
				{
					q.TableElement = new Join(
						new TableElement(TablesEnum.Gallery),
						new TableElement(TablesEnum.GalleryUsr),
						QueryJoinType.Left,
						new And(
							new Q(Gallery.Columns.K, GalleryUsr.Columns.GalleryK, true),
							new Q(GalleryUsr.Columns.UsrK, currentUsrK)
						)
					);
				}
				galleries = new GallerySet(q);


			}
			return galleries;
		}
		private GallerySet galleries;
		public static OrderBy SpotterOrderBy { get { return new OrderBy(new OrderBy(Usr.Columns.IsProSpotter, OrderBy.OrderDirection.Descending), new OrderBy(Usr.Columns.IsSpotter, OrderBy.OrderDirection.Descending), new OrderBy(Usr.Columns.SpottingsTotal, OrderBy.OrderDirection.Descending)); } }

		#endregion
		#region AllGalleries
		public GallerySet AllGalleries
		{
			get
			{
				if (allGalleries == null)
				{
					Query q = new Query();
					q.QueryCondition = new Q(Gallery.Columns.EventK, K);
					allGalleries = new GallerySet(q);
				}
				return allGalleries;
			}
			set
			{
				allGalleries = value;
			}
		}
		private GallerySet allGalleries;
		#endregion
		#region MusicTypes
		public MusicTypeSet MusicTypes
		{
			get
			{
				if (musicTypes == null)
				{
					Query q = new Query();
					q.TableElement = MusicType.EventAllJoin;
					q.QueryCondition = new Q(Event.Columns.K, this.K);
					q.OrderBy = MusicType.OrderBy;
					musicTypes = new MusicTypeSet(q);
				}
				return musicTypes;
			}
			set { musicTypes = value; }
		}
		MusicTypeSet musicTypes;
		#endregion
		#region EventMusicTypes
		public EventMusicTypeSet EventMusicTypes
		{
			get
			{
				if (eventMusicTypes == null)
					eventMusicTypes = new EventMusicTypeSet(new Query(new Q(EventMusicType.Columns.EventK, K)));
				return eventMusicTypes;
			}
			set { eventMusicTypes = value; }
		}
		EventMusicTypeSet eventMusicTypes;
		#endregion
		#region EventBrands
		public EventBrandSet EventBrands
		{
			get
			{
				if (eventBrands == null)
					eventBrands = new EventBrandSet(new Query(new Q(EventBrand.Columns.EventK, K)));
				return eventBrands;
			}
			set { eventBrands = value; }
		}
		EventBrandSet eventBrands;
		#endregion
		
		#region UsrAttended
		public UsrSet UsrAttended
		{
			get
			{
				if (usrAttended == null)
					usrAttended = new UsrSet(
						new Query(
							Usr.EventAttendedJoin,
							new And(
								Usr.IsDisplayedInUsrLists,
								new Q(Columns.K, K)
							),
							new OrderBy(Usr.Columns.DateTimeSignUp), -1
						)
					);
				return usrAttended;
			}
			set { usrAttended = value; }
		}
		UsrSet usrAttended;
		#endregion
		#region Spotters
		public UsrSet Spotters
		{
			get
			{
				if (spotters == null)
				{
					Query q = new Query();
					q.NoLock = true;
					q.TableElement = Usr.EventSpotterJoin;
					q.QueryCondition = new Q(Columns.K, this.K);
					q.OrderBy = new OrderBy(Usr.Columns.SpottingsTotal, OrderBy.OrderDirection.Descending);
					spotters = new UsrSet(q);
				}
				return spotters;
			}
			set { spotters = value; }
		}
		UsrSet spotters;
		#endregion
		#endregion

		#region IHasParent Members

		public Model.Entities.ObjectType ParentObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Venue;
			}
			set
			{
				throw new Exception("Can't set this for Event type");
			}
		}

		public IBob ParentObject
		{
			get
			{
				return this.Venue;
			}
		}

		public int ParentObjectK
		{
			get
			{
				return VenueK;
			}
			set
			{
				throw new Exception("Can't set this for Event type");
			}
		}

		#endregion

		#region IArchive Members

		public DateTime ArchiveDateTime
		{
			get
			{
				return this.DateTime;
			}
		}

		public string ArchiveHtml(bool showCountry, bool showPlace, bool showVenue, bool showEvent, int size)
		{

			string rolloverHtml = "<div style=\"width:250px;\"><b>";

			if (this.GuestlistPrice == 0.0)
				rolloverHtml += "Free entry (normally " + this.GuestlistRegularPrice.ToString("�0.##") + ")</b><br>";
			else
				rolloverHtml += this.GuestlistPrice.ToString("�0.##") + " entry (normally " + this.GuestlistRegularPrice.ToString("�0.##") + ")</b><br>";

			rolloverHtml += this.Name;

			if (showVenue)
				rolloverHtml += " @ " + this.Venue.Name;

			if (showPlace)
				rolloverHtml += " in " + this.Venue.Place.Name;

			rolloverHtml += "</div>";
			rolloverHtml = HttpUtility.UrlEncodeUnicode(rolloverHtml).Replace("'", "\\'");
			string attribs = " onmouseover=\"stt('" + rolloverHtml + "');\" onmouseout=\"htm();\"";

			return "<a href=\"" + this.Url() + "\"><img " + attribs + " src=\"" + this.AnyPicPath + "\" border=\"0\" class=\"ArchiveItem BorderBlack All\" width=\"" + size.ToString() + "\" height=\"" + size.ToString() + "\"></a>";
		}

		#endregion

		#region Links

		public string LinkFriendlyName
		{
			get
			{
				return Utilities.Link(Url(), FriendlyName);
			}
		}
		public string LinkFriendlyNameNewWindow
		{
			get
			{
				return Utilities.LinkNewWindow(Url(), FriendlyName);
			}
		}

		public string LinkEmail
		{
			get
			{
				return @"<a href=""[LOGIN(" + this.Url() + "\")]>" + this.FriendlyName + "</a>";
			}
		}

		public string LinkEmailFull
		{
			get
			{
				return @"<p>Event: " + LinkEmail + "</p>";
			}
		}

		public string LinkShort(int maxNameLength)
		{
			return LinkShort(maxNameLength, false);
		}

		public string LinkShort(int maxNameLength, bool includeDate)
		{
			return Utilities.Link(Url(), Cambro.Misc.Utility.Snip(Name, maxNameLength) + (includeDate ? " on " + Utilities.DateToString(this.DateTime) : ""),
									(Name.Length > maxNameLength ? " onmouseover=\"stt('" + HttpUtility.UrlEncodeUnicode(Name).Replace("'", "\\'") + "');\" onmouseout=\"htm();\"" : ""));
		}

		public string LinkShortNewWindow(int maxNameLength)
		{
			return LinkShortNewWindow(maxNameLength, false);
		}
		public string LinkShortNewWindow(int maxNameLength, bool includeDate)
		{
			return Utilities.Link(Url(), Cambro.Misc.Utility.Snip(Name, maxNameLength) + (includeDate ? " on " + Utilities.DateToString(this.DateTime) : ""),
									" target=\"_blank\"" + (Name.Length > maxNameLength ? " onmouseover=\"stt('" + HttpUtility.UrlEncodeUnicode(Name).Replace("'", "\\'") + "');\" onmouseout=\"htm();\"" : ""));
		}

		#region DoorlistIconLinkHtml
		public string DoorlistIconLinkHtml
		{
			get
			{
				if (this.TicketsSoldTotal > 0 && this.TicketRuns != null)
					return Utilities.LinkNewWindow(DoorlistUrl, Utilities.IconHtml(Utilities.Icon.Printer));
				else
					return "";//Utilities.IconHtml(Utilities.Icon.Cross);
			}
		}
		#endregion
		#region DoorlistUrl
		public string DoorlistUrl
		{
			get
			{
				return "/popup/doorlist/eventk-" + this.K.ToString();
			}
		}
		#endregion
		#endregion

		#region PicMisc and PicPhoto
		#region PicMisc
		public Misc PicMisc
		{
			get
			{
				if (picMisc == null && PicMiscK > 0)
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
				if (picPhoto == null && PicPhotoK > 0)
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

		#region IBuyableCredits Methods + Properties
		/// <summary>
		/// Queries database to retrieve the latest BuyableLockDateTime. Only returns if there is a lock within the Vars.IBUYABLE_LOCK_SECONDS
		/// </summary>
		public bool IsLocked
		{
			get
			{
				if (K == 0)
					return false;

				Query iBuyableLockDateTimeQuery = new Query(new And(new Q(Event.Columns.K, this.K),
																	new Q(Event.Columns.BuyableLockDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddSeconds(-1 * Vars.IBUYABLE_LOCK_SECONDS))));
				iBuyableLockDateTimeQuery.Columns = new ColumnSet(Event.Columns.BuyableLockDateTime);

				EventSet lockedBuyableSet = new EventSet(iBuyableLockDateTimeQuery);
				if (lockedBuyableSet.Count > 0)
				{
					this.BuyableLockDateTime = lockedBuyableSet[0].BuyableLockDateTime;
					return true;
				}
				else
					return false;
			}
		}

		/// <summary>
		/// Checks the price entered against the calculated price.  This checks if the figures have been adjusted during the payment processing.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <param name="price">InvoiceItem.Price</param>
		/// <returns></returns>
		public bool VerifyPriceCredits(InvoiceItem.Types invoiceItemType, int priceCredits)
		{
			if (invoiceItemType.Equals(InvoiceItem.Types.EventDonate))
			{
				return priceCredits == Vars.EventHighlightPriceCredits(this);
			}
			else
				throw new Exception("Invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
		}

		/// <summary>
		/// Checks if the IBuyableCredits Bob is ready to be processed. This is used as a pre-purchasing check.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <param name="price">InvoiceItem.Price</param>
		/// <returns></returns>
		public bool IsReadyForProcessingCredits(InvoiceItem.Types invoiceItemType, int priceCredits)
		{
			if (invoiceItemType.Equals(InvoiceItem.Types.EventDonate))
			{
				if (!this.Donated)
				{
					if (VerifyPriceCredits(invoiceItemType, priceCredits))
						return true;
					else
						throw new DsiUserFriendlyException("price wrong!");
				}
			}
			else
				throw new Exception("Invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));

			return this.Donated;
		}

		/// <summary>
		/// Processes the IBuyableCredits Bob. For events, it verifies that the event donation IsReadyForProcessing. If yes, then it sets event donation status to Donated, and updates the event.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <param name="price">InvoiceItem.Price</param>
		/// <returns></returns>
		public bool ProcessCredits(InvoiceItem.Types invoiceItemType, int priceCredits)
		{
			if (IsReadyForProcessingCredits(invoiceItemType, priceCredits))
			{
				this.Donated = true;
				this.UpdateHasHighlight(false);
				this.Update();
			}

			return IsProcessed(invoiceItemType);
		}

		/// <summary>
		/// Unprocesses the IBuyable Bob. For events, it sets the event donation off, and updates the event.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <returns></returns>
		public bool Unprocess(InvoiceItem.Types invoiceItemType)
		{
			if (invoiceItemType.Equals(InvoiceItem.Types.EventDonate))
			{
				if (this.Donated)
				{
					this.Donated = false;
					this.UpdateHasHighlight(false);
					this.Update();
				}
			}
			else
				throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));

			return !IsProcessed(invoiceItemType);
		}

		/// <summary>
		/// Verifies if the IBuyableCredits Bob has already been processed successfully.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <returns></returns>
		public bool IsProcessed(InvoiceItem.Types invoiceItemType)
		{
			if (invoiceItemType.Equals(InvoiceItem.Types.EventDonate))
				return this.Donated;
			else
				throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
		}

		/// <summary>
		/// Sets the IBuyableCredits Bob field BuyableLockDateTime to DateTime.Now and updates the Bob
		/// </summary>
		public void Lock()
		{
			this.BuyableLockDateTime = DateTime.Now;
			this.Update();
		}

		/// <summary>
		/// Sets the IBuyableCredits Bob field BuyableLockDateTime to DateTime.MinValue and updates the Bob
		/// </summary>
		public void Unlock()
		{
			this.BuyableLockDateTime = DateTime.MinValue;
			this.Update();
		}

		#region ToCampaignCredits()
		public List<CampaignCredit> ToCampaignCredits(Usr usr, int promoterK, bool saveToDatabase)
		{
			List<CampaignCredit> campaignCredits = new List<CampaignCredit>();
			CampaignCredit eventHighlightCampaignCredit = new CampaignCredit()
			{
				Description = "Event highlight " + this.K.ToString(),
				BuyableObjectK = this.K,
				BuyableObjectType = Model.Entities.ObjectType.Event,
				Credits = -Vars.EventHighlightPriceCredits(this),
				ActionDateTime = Time.Now,
				PromoterK = promoterK,
				InvoiceItemType = InvoiceItem.Types.EventDonate,
				Enabled = false,
				FixedDiscount = this.FixedDiscount,
				IsPriceFixed = this.IsPriceFixed
			};
			eventHighlightCampaignCredit.SetUsrAndActionUsr(usr);
			if (saveToDatabase)
				eventHighlightCampaignCredit.Update();
			campaignCredits.Add(eventHighlightCampaignCredit);

			return campaignCredits;
		}
		#endregion
		#region FixDiscountAndUpdate
		public void FixDiscountAndUpdate(double? discount)
		{
			if (discount != null)
			{
				this.FixedDiscount = Math.Round(discount.Value, 4);
				//this.PriceCreditsStored = this.CampaignCredits;
				this.IsPriceFixed = true;
			}
			else
			{
				this.FixedDiscount = 0;
				//this.PriceCreditsStored = 0;
				this.IsPriceFixed = false;
			}
			this.Update();
		}
		#endregion
		#endregion

		#region ListItem
		public ListItem ToListItem(bool includeDate)
		{
			return this.ToListItem(30000, includeDate);
		}
		public ListItem ToListItem(int maxEventNameLength, bool includeDate)
		{
			return new ListItem(Cambro.Misc.Utility.Snip(Name, maxEventNameLength) + (includeDate ? " on " + Utilities.DateToString(this.DateTime) : ""),
								this.K.ToString());
		}
		#endregion

		#region Tickets
		public DateTime DefaultEndOfTicketRunDateTime
		{
			get
			{
				DateTime endTicketRunDefaultDateTime = Utilities.GetStartOfDay(this.DateTime);
				if (this.StartTime.Equals(Event.StartTimes.Evening))
					endTicketRunDefaultDateTime = endTicketRunDefaultDateTime.AddHours(17);	// 5PM the day off
				else if (this.StartTime.Equals(Event.StartTimes.Morning))
					endTicketRunDefaultDateTime = endTicketRunDefaultDateTime.AddHours(-8);	// 5PM the day before
				// for Daytime events, leave at start of day

				return endTicketRunDefaultDateTime;
			}
		}

		public DateTime LatestEndOfTicketRunDateTime
		{
			get
			{
				DateTime latestEndOfTicketRunDateTime = Utilities.GetStartOfDay(this.DateTime);
				if (this.StartTime.Equals(Event.StartTimes.Evening))
					latestEndOfTicketRunDateTime = latestEndOfTicketRunDateTime.AddHours(21);	// 9PM the day off
				else if (this.StartTime.Equals(Event.StartTimes.Morning))
					latestEndOfTicketRunDateTime = latestEndOfTicketRunDateTime.AddHours(4);	// 4AM the day off
				else // for Daytime events
					latestEndOfTicketRunDateTime = latestEndOfTicketRunDateTime.AddHours(9);	// 9AM the day off

				return latestEndOfTicketRunDateTime;
			}
		}

		public static EventSet GetEventsForTicketsNeedingFeedback(Usr usr)
		{
			Query eventsForTicketsNeedingFeedbackQuery = new Query(new And(new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Today.AddDays(Vars.DAYS_TO_SHOW_TICKET_FEEDBACK_ALERT)),
																		   new Q(Ticket.Columns.Enabled, true),
																		   new Q(Ticket.Columns.BuyerUsrK, usr.K),
																		   new Q(Ticket.Columns.Feedback, Ticket.FeedbackEnum.None),
																		   new Q(Ticket.Columns.Cancelled, false)));

			eventsForTicketsNeedingFeedbackQuery.OrderBy = new OrderBy(new OrderBy(Event.Columns.DateTime), new OrderBy(Event.Columns.StartTime));
			eventsForTicketsNeedingFeedbackQuery.Distinct = true;
			eventsForTicketsNeedingFeedbackQuery.DistinctColumn = Event.Columns.K;
			eventsForTicketsNeedingFeedbackQuery.TableElement = new Join(Event.Columns.K, Ticket.Columns.EventK);
			eventsForTicketsNeedingFeedbackQuery.Columns = new ColumnSet(Event.Columns.Name,
																		 Event.Columns.VenueK,
																		 Event.Columns.K,
																		 Event.Columns.DateTime,
																		 Event.Columns.UrlFragment,
																		 Event.Columns.VenueK);

			return new EventSet(eventsForTicketsNeedingFeedbackQuery);

		}

		public static EventSet GetUpcomingEventsWithTickets(Promoter promoter)
		{
			return new EventSet(GetEventsWithTicketsQuery(promoter, Time.Today, new DateTime(2099, 1, 1)));
		}
		public static EventSet GetUpcomingEventsWithTickets(Brand brand)
		{
			return GetEventsWithTickets(brand, Time.Today, new DateTime(2099, 1, 1));
		}
		public static EventSet GetEventsWithTickets(Brand brand, DateTime from, DateTime to)
		{
			Query upcomingEventsWithTicketsQuery = GetEventsWithTicketsQuery(brand.Promoter, from, to);
			upcomingEventsWithTicketsQuery.QueryCondition = new And(upcomingEventsWithTicketsQuery.QueryCondition,
																	new Q(EventBrand.Columns.BrandK, brand.K));
			upcomingEventsWithTicketsQuery.TableElement = new Join(upcomingEventsWithTicketsQuery.TableElement, EventBrand.Columns.EventK, Event.Columns.K);

			return new EventSet(upcomingEventsWithTicketsQuery);
		}
		public static EventSet GetEvents(Brand brand, DateTime from, DateTime to)
		{
			Query eventsQuery = GetEventsQuery(brand, from, to);
			return new EventSet(eventsQuery);
		}
		public static EventSet GetEvents(Venue venue, DateTime from, DateTime to)
		{
			Query eventsQuery = GetEventsQuery(venue, from, to);
			return new EventSet(eventsQuery);
		}

		public static Query GetEventsWithTicketsQuery(Promoter promoter, DateTime from, DateTime to)
		{
			Query upcomingEventsWithTicketsQuery = new Query(new And(new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, from),
																	 new Q(Event.Columns.DateTime, QueryOperator.LessThan, to),
																	 new Q(TicketPromoterEvent.Columns.PromoterK, promoter.K)));
			upcomingEventsWithTicketsQuery.OrderBy = new OrderBy(new OrderBy(Event.Columns.DateTime), new OrderBy(Event.Columns.StartTime));
			upcomingEventsWithTicketsQuery.Distinct = true;
			upcomingEventsWithTicketsQuery.DistinctColumn = Event.Columns.K;
			upcomingEventsWithTicketsQuery.TableElement = new Join(Event.Columns.K, TicketPromoterEvent.Columns.EventK);
			upcomingEventsWithTicketsQuery.Columns = new ColumnSet(Event.Columns.Name,
																   Event.Columns.VenueK,
																   Event.Columns.K,
																   Event.Columns.DateTime,
																   Event.Columns.UrlFragment,
																   Event.Columns.VenueK);
			return upcomingEventsWithTicketsQuery;
		}
		#endregion

		public static EventSet GetUpcomingEvents(Brand brand)
		{
			Query upcomingEventsQuery = Event.GetUpcomingEventsQuery();
			upcomingEventsQuery.QueryCondition = new And(upcomingEventsQuery.QueryCondition,
														 new Q(EventBrand.Columns.BrandK, brand.K));
			upcomingEventsQuery.TableElement = Event.EventBrandJoin;

			return new EventSet(upcomingEventsQuery);
		}

		public static EventSet GetUpcomingEvents(Promoter promoter)
		{
			Query upcomingEventsQuery = Event.GetUpcomingEventsQuery();
			upcomingEventsQuery.QueryCondition = new And(upcomingEventsQuery.QueryCondition,
														 new Q(Promoter.Columns.K, promoter.K));
			upcomingEventsQuery.TableElement = Event.PromoterJoinWithVenue;

			return new EventSet(upcomingEventsQuery);
		}

		public static EventSet GetUpcomingEvents(Venue venue)
		{
			Query upcomingEventsQuery = Event.GetUpcomingEventsQuery();
			upcomingEventsQuery.QueryCondition = new And(upcomingEventsQuery.QueryCondition,
														 new Q(Event.Columns.VenueK, venue.K));
			return new EventSet(upcomingEventsQuery);
		}

		private static Query GetUpcomingEventsQuery()
		{
			return GetEventsQuery(Time.Today, new DateTime(2099, 1, 1));
		}

		private static Query GetPreviousEventsQuery()
		{
			return GetEventsQuery(new DateTime(1900, 1, 1), Time.Today);
		}

		private static Query GetEventsQuery(Brand brand, DateTime fromDate, DateTime toDate)
		{
			Query query = GetEventsQuery(fromDate, toDate);
			query.TableElement = Event.EventBrandJoin;
			query.QueryCondition = new And(query.QueryCondition,
										   new Q(EventBrand.Columns.BrandK, brand.K));

			return query;
		}

		private static Query GetEventsQuery(Venue venue, DateTime fromDate, DateTime toDate)
		{
			Query query = GetEventsQuery(fromDate, toDate);

			query.QueryCondition = new And(query.QueryCondition,
										   new Q(Event.Columns.VenueK, venue.K));

			return query;
		}

		private static Query GetEventsQuery(DateTime fromDate, DateTime toDate)
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, fromDate),
									   new Q(Event.Columns.DateTime, QueryOperator.LessThan, toDate));
			q.NoLock = true;
			q.Distinct = true;
			q.DistinctColumn = Event.Columns.K;
			q.OrderBy = Event.FutureEventOrder;
			q.Columns = new ColumnSet(
				Event.LinkColumns,
				Event.Columns.Name,
				Event.Columns.DateTime,
				Event.Columns.HasGuestlist,
				Event.Columns.GuestlistLimit,
				Event.Columns.GuestlistCount,
				Event.Columns.HasHilight,
				Event.Columns.Pic,
				Event.Columns.VenueK,
				Event.Columns.SpotterRequest);

			return q;
		}


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
				return new Q(Thread.Columns.EventK, K);
			}
		}
		bool IDiscussable.ShowPrivateThreads { get { return true; } }
		IDiscussable IDiscussable.UsedDiscussable { get { return this; } }
		bool IDiscussable.OnlyShowThreads { get { return false; } }


		#region GetEventsForDisplay
		public class EventsForDisplay
		{
			public int BrandK { get; set; }
			public int GroupK { get; set; }
			public int CountryK { get; set; }
			public bool BuddyDisplay { get; set; }
			public bool Tickets { get; set; }
			public bool HotTickets { get; set; }
			public bool FreeGuestlist { get; set; }
			public int VenueK { get; set; }
			public int PlaceK { get; set; }
			public int Day { get; set; }
			public bool Personalise { get; set; }
			public bool IgnoreMusicType { get; set; }
			public int MusicTypeK { get; set; }
			public int AttendedUsrK { get; set; }

			#region FilterBy...
			public bool FilterByBrand
			{
				get { return BrandK > 0; }
			}
			public bool FilterByGroup
			{
				get { return GroupK > 0; }
			}
			public bool FilterByVenue
			{
				get { return VenueK > 0; }
			}
			public bool FilterByPlace
			{
				get { return PlaceK > 0; }
			}
			public bool FilterByCountry
			{
				get { return CountryK > 0; }
			}
			public bool FilterByUsrAttended
			{
				get { return AttendedUsrK > 0; }
			}
			#endregion
			#region FilterBob
			public IBob FilterBob
			{
				get
				{
					if (filterBob == null)
					{
						if (FilterByBrand)
							filterBob = new Brand(BrandK);
						else if (FilterByGroup)
							filterBob = new Group(GroupK);
						else if (FilterByVenue)
							filterBob = new Venue(VenueK);
						else if (FilterByPlace)
							filterBob = new Place(PlaceK);
						else if (FilterByCountry)
							filterBob = new Country(CountryK);
						else if (FilterByUsrAttended)
							filterBob = new Usr(AttendedUsrK);
					}
					return filterBob;
				}
				set
				{
					filterBob = value;
				}
			}
			private IBob filterBob;
			#endregion
			#region MyCalendar
			bool MyCalendar
			{
				get { return (Personalise && !BuddyDisplay); }
			}
			#endregion
			#region Q Filter
			Q Filter
			{
				get
				{
					return new And(
							FreeGuestlistFilter,
							TicketsFilter,
							BrandFilter,
							GroupFilter,
							CountryFilter,
							BuddyFilter,
							PersonalFilter,
							VenueFilter,
							PlaceFilter,
							MusicFilter,
							AttendedUsrFilter
						);
				}
			}
			#endregion
			#region FilterQs
			Q AttendedUsrFilter
			{
				get
				{
					if (FilterByUsrAttended)
					{
						return new Q(UsrEventAttended.Columns.UsrK, AttendedUsrK);
					}
					else
						return new Q(true);
				}
			}
			Q BuddyFilter
			{
				get
				{
					if (BuddyDisplay)
					{
						return //new Or(
							new And(new Q(Buddy.Columns.FullBuddy, true), new Q(Buddy.Columns.UsrK, Usr.Current.K));
						//	new Q(UsrEventAttended.Columns.UsrK, Usr.Current.K));
					}
					else
						return new Q(true);
				}
			}
			Q TicketsFilter
			{
				get
				{
					if (Tickets || HotTickets)
						return new Q(Event.Columns.IsTicketsAvailable, true);
					else
						return new Q(true);
				}
			}
			Q FreeGuestlistFilter
			{
				get
				{
					if (FreeGuestlist)
						return new Q(Event.Columns.SpotterRequest, true);
					else
						return new Q(true);
				}
			}
			Q BrandFilter
			{
				get
				{
					if (FilterByBrand)
						return new Q(EventBrand.Columns.BrandK, BrandK);
					else
						return new Q(true);
				}
			}
			Q GroupFilter
			{
				get
				{
					if (FilterByGroup)
						return new Q(GroupEvent.Columns.GroupK, GroupK);
					else
						return new Q(true);
				}
			}
			Q VenueFilter
			{
				get
				{
					if (FilterByVenue)
						return new Q(Event.Columns.VenueK, VenueK);
					else
						return new Q(true);
				}
			}
			Q PlaceFilter
			{
				get
				{
					if (FilterByPlace)
						return new Q(Venue.Columns.PlaceK, PlaceK);
					else
						return new Q(true);
				}
			}
			Q MusicFilter
			{
				get
				{
					if (!FilterByMusic)
						return new Q(true);
					else
					{
						ArrayList al = new ArrayList();
						MusicType mt = new MusicType(MusicTypeK > 0 ? MusicTypeK : (int)Prefs.Current["MusicPref"]);
						//al.Add(new Q(EventMusicType.Columns.MusicTypeK,1));
						al.Add(new Q(EventMusicType.Columns.MusicTypeK, mt.K));
						MusicTypeSet mts = new MusicTypeSet(new Query(new Q(MusicType.Columns.ParentK, mt.K)));
						foreach (MusicType mtChild in mts)
						{
							al.Add(new Q(EventMusicType.Columns.MusicTypeK, mtChild.K));
						}
						Q[] qArr = (Q[])al.ToArray(typeof(Q));
						return new Or(qArr);
					}
				}
			}
			Q CountryFilter
			{
				get
				{
					if (FilterByPlace || FilterByVenue || MyCalendar || BuddyDisplay || FilterByBrand || FilterByGroup)
						return new Q(true);
					else if (FilterByCountry)
						return new Q(Place.Columns.CountryK, CountryK);
					else
						return new Q(true);
				}
			}
			Q PersonalFilter
			{
				get
				{
					if (MyCalendar)
					{
						Q musicQ = new Or(MusicType.MusicTypeSetAsQueryList(Usr.Current.MusicTypesFavourite, EventMusicType.Columns.MusicTypeK).ToArray());

						ArrayList alP = new ArrayList();
						foreach (Place p in Usr.Current.PlacesVisitKOnly)
							alP.Add(new Q(Place.Columns.K, p.K));
						Q[] qP = (Q[])alP.ToArray(typeof(Q));
						Q placesQ = new Or(qP);

						return new And(musicQ, placesQ);
					}
					else
						return new Q(true);

				}
			}
			#endregion
			#region FilterWorldwide
			public bool FilterWorldwide
			{
				get
				{
					return !(Personalise || FilterByVenue || FilterByPlace || FilterByCountry || FilterByBrand || FilterByGroup || FilterByUsrAttended);
				}
			}
			#endregion
			#region FilterByMusic
			protected bool FilterByMusic
			{
				get
				{
					return MusicTypeK > 1 || (!Personalise && !IgnoreMusicType && Prefs.Current["MusicPref"].Exists && Prefs.Current["MusicPref"] != 1);
				}
			}
			#endregion
			#region GetEvents...
			public EventSet GetEventsBetweenDates(DateTime firstDay, DateTime lastDay)
			{
				Query queryAll = new Query();
				queryAll.QueryCondition = new And(
					Filter,
					new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, firstDay),
					new Q(Event.Columns.DateTime, QueryOperator.LessThanOrEqualTo, lastDay)
					);
				queryAll.Columns = ColumnSet;

				if (this.BuddyDisplay)
					queryAll.ExtraSelectElements.Add("CurrentUsrAttendedEvent", "CASE WHEN [Event_K_UsrEventAttended].[EventK] IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END");

				queryAll.OrderBy = OrderBy;
				queryAll.TableElement = OutputTableElement;// SelectTableElement;
				//queryAll.Distinct = true;
				//queryAll.DistinctColumn = Event.Columns.K;
				//queryAll.DataTableElement = OutputTableElement;
				return new EventSet(queryAll);
			}

			public EventSet GetTopHotTicketEvents()
			{
				Query queryAll = new Query();
				queryAll.QueryCondition = new And(Filter);
				queryAll.Distinct = true;
				queryAll.DistinctColumn = Event.Columns.K;
				queryAll.Columns = EventListColumnSet;
				queryAll.OrderBy = OrderBy;
				queryAll.TopRecords = 20;
				queryAll.TableElement = SelectTableElement;
				return new EventSet(queryAll);
			}

			public EventSet GetEventsForDay(DateTime day)
			{
				Query queryAll = new Query();
				queryAll.QueryCondition = new And(
					Filter,
					new Q(Event.Columns.DateTime, day)
					);
				queryAll.Distinct = true;
				queryAll.DistinctColumn = Event.Columns.K;
				queryAll.Columns = Event.EventsForDisplay.EventListColumnSet;
				queryAll.OrderBy = OrderBy;
				queryAll.TableElement = SelectTableElement;
				return new EventSet(queryAll);
			}

			public Event GetLatestPastEvent(DateTime firstDay)
			{
				Query morePastQuery = new Query();
				this.IgnoreMusicType = true;
				morePastQuery.QueryCondition = new And(
					Filter,
					new Q(Event.Columns.DateTime, QueryOperator.LessThan, firstDay)
					);
				morePastQuery.Columns = new ColumnSet(Event.Columns.DateTime);
				morePastQuery.TopRecords = 1;
				morePastQuery.OrderBy = new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Descending);
				morePastQuery.TableElement = SelectTableElement;
				EventSet morePastEs = new EventSet(morePastQuery);
				if (morePastEs.Count > 0)
					return morePastEs[0];
				else
					return null;
			}

			public Event GetNextFutureEvent(DateTime lastDay)
			{
				Query moreFutureQuery = new Query();
				moreFutureQuery.QueryCondition = new And(
					Filter,
					new Q(Event.Columns.DateTime, QueryOperator.GreaterThan, lastDay)
					);
				moreFutureQuery.Columns = new ColumnSet(Event.Columns.DateTime);
				moreFutureQuery.TopRecords = 1;
				moreFutureQuery.OrderBy = new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Ascending);
				moreFutureQuery.TableElement = SelectTableElement;
				EventSet moreFutureEs = new EventSet(moreFutureQuery);
				if (moreFutureEs.Count > 0)
					return moreFutureEs[0];
				else
					return null;
			}
			#endregion
			#region OrderBy
			OrderBy OrderBy
			{
				get
				{
					if (HotTickets)
					{
						return new OrderBy(
							new OrderBy(Event.Columns.TicketHeat, OrderBy.OrderDirection.Descending),
							new OrderBy(Event.Columns.UsrAttendCount, OrderBy.OrderDirection.Descending),
							new OrderBy(Event.Columns.Capacity, OrderBy.OrderDirection.Descending),
							new OrderBy(Event.Columns.K));
					}
					else if (Tickets)
					{
						List<OrderBy> obl = new List<OrderBy>(new OrderBy[] { 
										  new OrderBy(Event.Columns.DateTime,OrderBy.OrderDirection.Ascending), 
										  new OrderBy(Event.Columns.StartTime,OrderBy.OrderDirection.Ascending),
										  new OrderBy(Event.Columns.TicketHeat,OrderBy.OrderDirection.Descending),
										  new OrderBy(Event.Columns.UsrAttendCount,OrderBy.OrderDirection.Descending),
										  new OrderBy(Event.Columns.Capacity,OrderBy.OrderDirection.Descending),
										  new OrderBy(Event.Columns.K)
									  });

						//if (Day == 0)
						//    obl.Add(new OrderBy(MusicType.Columns.Order));


						return new OrderBy(obl.ToArray());
					}
					else if (BuddyDisplay)
					{
						return new OrderBy(
							new OrderBy[] {
										  new OrderBy(Event.Columns.DateTime,OrderBy.OrderDirection.Ascending), 
										  new OrderBy(Event.Columns.StartTime,OrderBy.OrderDirection.Ascending), 
										  new OrderBy(Event.Columns.HasHilight,OrderBy.OrderDirection.Descending), 
										  new OrderBy(Event.Columns.IsTicketsAvailable,OrderBy.OrderDirection.Descending), 
										  new OrderBy(Event.Columns.UsrAttendCount,OrderBy.OrderDirection.Descending),
										  new OrderBy(Event.Columns.Capacity,OrderBy.OrderDirection.Descending),
										  new OrderBy(Event.Columns.K),
										  new OrderBy(Buddy.Columns.BuddyUsrK)
									  }
							);
					}
					else
					{
						List<OrderBy> obl = new List<OrderBy>(new OrderBy[] { 
										  new OrderBy(Event.Columns.DateTime,OrderBy.OrderDirection.Ascending), 
										  new OrderBy(Event.Columns.StartTime,OrderBy.OrderDirection.Ascending),
										  new OrderBy(Event.Columns.HasHilight,OrderBy.OrderDirection.Descending),
										  new OrderBy(Event.Columns.IsTicketsAvailable,OrderBy.OrderDirection.Descending),
										  new OrderBy(Event.Columns.SpotterRequest,OrderBy.OrderDirection.Descending),
										  new OrderBy(Event.Columns.UsrAttendCount,OrderBy.OrderDirection.Descending),
										  new OrderBy(Event.Columns.Capacity,OrderBy.OrderDirection.Descending),
										  new OrderBy(Event.Columns.K)
									  });
						//if (Day == 0)
						//    obl.Add(new OrderBy(MusicType.Columns.Order));

						return new OrderBy(obl.ToArray());
					}
				}
			}
			#endregion
			#region ColumnSets
			ColumnSet ColumnSet
			{
				get
				{
					if (BuddyDisplay)
					{
						return new ColumnSet(
							NonBuddyColumnSet,
							BuddyColumnSet);
					}
					else
					{
						return NonBuddyColumnSet;
					}
				}
			}
			ColumnSet NonBuddyColumnSet
			{
				get
				{
					return new ColumnSet(
						EventListColumnSet,

						Place.Columns.RegionAbbreviation,
						Country.Columns.UrlName,
						Place.Columns.UrlName,
						Place.Columns.Name,
						Place.Columns.CountryK,
						Country.Columns.FriendlyName,
						Venue.Columns.UrlName,
						Venue.Columns.Name
						);
				}
			}
			ColumnSet BuddyColumnSet
			{
				get
				{
					return new ColumnSet(
						Usr.Columns.K,
						Usr.Columns.Pic,
						Usr.Columns.NickName
						);
				}
			}
			public static ColumnSet EventListColumnSet
			{
				get
				{
					return new ColumnSet(
						Event.LinkColumns,
						Event.Columns.SpotterRequest,
						Event.Columns.IsTicketsAvailable,
						Event.Columns.DateTime,
						Event.Columns.VenueK,
						Event.Columns.StartTime,
						Event.Columns.LivePhotos,
						Event.Columns.HasHilight,
						Event.Columns.Pic,
						Event.Columns.IsDescriptionText,
						Event.Columns.IsDescriptionCleanHtml,
						Event.Columns.ShortDetailsHtml,
						Event.Columns.MusicTypesString
					);
				}
			}
			#endregion
			#region TableElements
			TableElement SelectTableElement
			{
				get { return GetTableElement(false); }
			}
			TableElement OutputTableElement
			{
				get {return  GetTableElement(true); }			
			}
			TableElement GetTableElement(bool outputOnly)
			{
				if (BuddyDisplay)
				{
					TableElement t;
					if (outputOnly)
					{
						t = new Join(Event.PlaceAndMusicTypeLeftJoin, Country.Columns.K, Place.Columns.CountryK);
					}
					else
					{
						t = new TableElement(TablesEnum.Event);
					}

					t = new Join(t,
						new TableElement(TablesEnum.UsrEventAttended),
						QueryJoinType.Left,
						new Q(Event.Columns.K, UsrEventAttended.Columns.EventK, true));

					t = new Join(t,
						new TableElement(TablesEnum.Buddy),
						QueryJoinType.Inner,
						new And(new Q(Buddy.Columns.FullBuddy, true), new Q(Buddy.Columns.UsrK, Usr.Current.K), new Q(UsrEventAttended.Columns.UsrK, Buddy.Columns.BuddyUsrK, true))
					);

					t = new Join(t,
						new TableElement(TablesEnum.Usr),
						QueryJoinType.Left,
						new Q(Buddy.Columns.BuddyUsrK, Usr.Columns.K, true)
					);

					t = new Join(t,
						new TableElement(new Column(Event.Columns.K, UsrEventAttended.Columns.EventK)),
						QueryJoinType.Left,
						new And(
							new Q(new Column(Event.Columns.K, UsrEventAttended.Columns.EventK), Event.Columns.K, true),
							new Q(new Column(Event.Columns.K, UsrEventAttended.Columns.UsrK), Usr.Current.K)
							)
					);

					return t;// Event.EventCountryJoin(t);
				}
				else if (FilterByUsrAttended)
				{
					return new Join(
						Event.CountryAllJoin,
						UsrEventAttended.Columns.EventK,
						Event.Columns.K);

				}
				else if (FilterByBrand)
					return new Join(
						new Join(Event.PlaceAndMusicTypeLeftJoin, Country.Columns.K, Place.Columns.CountryK),
						new TableElement(TablesEnum.EventBrand),
						QueryJoinType.Inner,
						Event.Columns.K,
						EventBrand.Columns.EventK
						);
				else if (FilterByGroup)
					return new Join(
						new Join(Event.PlaceAndMusicTypeLeftJoin, Country.Columns.K, Place.Columns.CountryK),
						new TableElement(TablesEnum.GroupEvent),
						QueryJoinType.Inner,
						Event.Columns.K,
						GroupEvent.Columns.EventK
						);
				else
					return new Join(Event.PlaceAndMusicTypeLeftJoin, Country.Columns.K, Place.Columns.CountryK);
			}
			#endregion
		}
		#endregion

		public string UrlGalleryEdit
		{
			get { return "/pages/galleries/add/eventk-" + this.K; }
		}



		public string VerboseInfo
		{
			get
			{
				return this.Name + " @ " + this.Venue.Name + " (" + this.Venue.Place.Country.FriendlyName + "), " +
				       this.DateTime.ToString("dd/MM/yy");
			}
		}

		#region Hotel Links
		public bool ShowHotelLink 
		{
			get
			{
				return !(this.DontShowHotelLink ?? false) &&
					DateTime.Now.Date <= this.DateTime &&
					this.Venue.Place.CountryK == 224; // UK
			}
		}
		public string FindHotelLink(HotelLinkSources source)
		{
			return string.Format("/popup/findhotel?place={0}&date={1}&source={2}",
				this.Venue.Place.NamePlain, this.DateTime.ToString("yyyyMMdd"), (int)source);
		}
		#endregion
	}
	#endregion

	#region EventMusicType

	#endregion

	#region EventBrand

	#endregion

 
}
