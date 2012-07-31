using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using Cambro;
using Cambro.Web;
using Cambro.Misc;

using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;

using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;

namespace Bobs
{

	#region IncomingSms
	[Serializable] 
	public partial class IncomingSms
	{

	 

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public int K
		{
			get { return (int)this[IncomingSms.Columns.K]; }
			set { this[IncomingSms.Columns.K] = value; }
		}
		/// <summary>
		/// The mobile number that this sms came from.
		/// </summary>
		public int MobileK
		{
			get { return (int)this[IncomingSms.Columns.MobileK]; }
			set { mobile = null; this[IncomingSms.Columns.MobileK] = value; }
		}
		/// <summary>
		/// The text of the text message (should start "tonight")
		/// </summary>
		public string Message
		{
			get { return (string)this[IncomingSms.Columns.Message]; }
			set { this[IncomingSms.Columns.Message] = value; }
		}
		/// <summary>
		/// DateTime that the message arrived
		/// </summary>
		public DateTime DateTime
		{
			get { return (DateTime)this[IncomingSms.Columns.DateTime]; }
			set { this[IncomingSms.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Incoming type - Tonight or Pllay
		/// </summary>
		public ServiceTypes ServiceType
		{
			get { return (ServiceTypes)this[IncomingSms.Columns.ServiceType]; }
			set { this[IncomingSms.Columns.ServiceType] = value; }
		}
		/// <summary>
		/// The xml data coming from the gateway
		/// </summary>
		public string PostData
		{
			get { return (string)this[IncomingSms.Columns.PostData]; }
			set { this[IncomingSms.Columns.PostData] = value; }
		}
		/// <summary>
		/// The message_id coming from the gateway
		/// </summary>
		public string MessageID
		{
			get { return (string)this[IncomingSms.Columns.MessageID]; }
			set { this[IncomingSms.Columns.MessageID] = value; }
		}
		#endregion

		
		public void Process()
		{
			string[] arr = this.Message.ToLower().Split(' ');

			if (arr[0].StartsWith("hello"))
			{
				Photo p = new Photo(int.Parse(arr[1]));

				if (p.BlockedFromPhotoOfWeekUser)
					return;

				if (p.Status != Photo.StatusEnum.Enabled)
					return;

				//send the outgoing premium sms and await the response...

				this.ServiceType = ServiceTypes.FrontPagePhoto;
				this.Update();

				if (Common.Settings.EnablePremiumRate == Common.Settings.EnablePremiumRateOption.On)
				{

					OutgoingSms sms = new OutgoingSms();

					sms.DateTime = DateTime.Now;
					sms.Type = OutgoingSms.Types.FrontPagePhoto;
					sms.IncomingSmsK = this.K;
					sms.Message = "Your photo / video will now appear on the front page!";
					sms.ChargeType = Vars.DevEnv ? OutgoingSms.ChargeTypes.Premium012p : OutgoingSms.ChargeTypes.Premium150p;
					sms.MobileK = this.MobileK;
					sms.ServiceType = ServiceTypes.FrontPagePhoto;
					sms.Update();
					sms.Send();
				}
				else
				{
					//enable photo immediatly

					string caption = "";
					if (arr.Length > 2)
					{
						for (int i = 2; i < arr.Length; i++)
							caption += (i > 2 ? " " : "") + arr[i];

						caption = Cambro.Web.Helpers.Strip(caption, true, true, true, true);

						if (caption.Length > 200)
							caption = caption.Substring(0, 195) + "...";
					}

					p.SetAsPhotoOfWeek(true, caption, false, true);
				}

			}
			
			#region removed
			//if (arr[0].StartsWith("tonight"))
			//{
			//    #region process TONIGHT texts
			//    Mobile m = new Mobile(number, network, ServiceTypes.Tonight);
			//    this.MobileK = m.K;
			//    this.ServiceType = ServiceTypes.Tonight;
			//    this.Mobile.TotalIncoming++;
			//    this.Mobile.Update();
			//    this.Update();

			//    for (int i = 1; i < arr.Length; i++)
			//    {
			//        if (arr[i].Equals("event") || arr[i].Equals("venue"))
			//        {
			//            for (int j = 1; j < arr.Length; j++)
			//            {
			//                int eventK = 0;
			//                Event ev = null;
			//                Venue v = null;
			//                try
			//                {
			//                    eventK = int.Parse(arr[j]);
			//                    ev = new Event(eventK);
			//                    v = ev.Venue;
			//                }
			//                catch { }
			//                if (arr[i].Equals("event") && ev != null)
			//                {
			//                    ProcessTonightEventDetail(ev);
			//                    return;
			//                }
			//                else if (arr[i].Equals("venue") && ev != null && v != null)
			//                {
			//                    ProcessTonightVenueDetail(v, ev);
			//                    return;
			//                }
			//            }
			//        }
			//        else if (arr[i].Equals("help"))
			//        {
			//            ProcessTonightInfo();
			//            return;
			//        }
			//        else if (arr[i].Equals("end"))
			//        {
			//            ProcessTonightEnd();
			//            return;
			//        }
			//    }

			//    try
			//    {
			//        ArrayList placeQueryCondition = new ArrayList();
			//        ArrayList musicQueryCondition = new ArrayList();
			//        for (int i = 1; i < arr.Length; i++)
			//        {
			//            placeQueryCondition.Add(new Q(Place.Columns.Name, arr[i]));
			//            musicQueryCondition.Add(new Q(MusicType.Columns.SmsCode, arr[i]));
			//        }
			//        Q placeOr = new Or((Q[])placeQueryCondition.ToArray(typeof(Q)));
			//        Q musicOr = new Or((Q[])musicQueryCondition.ToArray(typeof(Q)));

			//        Query placeQ = new Query();
			//        placeQ.QueryCondition = new And(
			//            placeOr,
			//            new Q(Place.Columns.Enabled, true),
			//            new Q(Place.Columns.CountryK, 224)
			//            );
			//        PlaceSet ps = new PlaceSet(placeQ);

			//        Query musicQ = new Query();
			//        musicQ.QueryCondition = musicOr;
			//        MusicTypeSet mts = new MusicTypeSet(musicQ);

			//        if (mts.Count > 0 || ps.Count > 0)
			//        {
			//            ProcessTonightListings(mts, ps);
			//            return;
			//        }
			//    }
			//    catch
			//    {
			//    }

			//    ProcessTonightError();
			//    return;
			//    #endregion
			//}
			//			else if (arr[0].StartsWith("pllay"))
			//			{
			//				#region process PLLAY texts
			//				Mobile m = new Mobile(number, network, ServiceTypes.TextGuest, 1);
			//				this.MobileK = m.K;
			//				this.ServiceType=ServiceTypes.TextGuest;
			//				this.Mobile.TotalIncoming++;
			//				this.Mobile.Update();
			//				this.Update();
			//
			//				if (arr.Length>1 && arr[1].Equals("end"))
			//				{
			//					ProcessGuestEnd();
			//					return;
			//				}
			//				else
			//				{
			//					GuestPromotionSet gps = new GuestPromotionSet(new Q(GuestPromotion.Columns.Shortcut,"pllay"));
			//					GuestPromotion gp = gps[0];
			//					ProcessGuest(gp);
			//					return;
			//				}
			//				#endregion
			//			}
			//			else if (arr[0].StartsWith("guest"))
			//			{
			//				#region process GUEST texts
			//				int GuestPromotionK = 0;
			//				if (arr.Length>1)
			//				{
			//					bool numeric = true;
			//					if (arr[1].Equals("end"))
			//					{
			//						try
			//						{
			//							GuestClient c = new GuestClient(int.Parse(arr[2]));
			//							Mobile m = new Mobile(number, network, ServiceTypes.TextGuest, c.K);
			//							this.MobileK = m.K;
			//							this.ServiceType=ServiceTypes.TextGuest;
			//							this.Mobile.TotalIncoming++;
			//							this.Mobile.Update();
			//							this.Update();
			//							ProcessGuestEnd();
			//							return;
			//						}
			//						catch
			//						{
			//							return;
			//						}
			//					}
			//					else
			//					{
			//						Regex r = new Regex("[^0123456789]");
			//						try
			//						{
			//							string numbers = r.Replace(arr[1],"");
			//							GuestPromotionK = int.Parse(numbers);
			//						}
			//						catch
			//						{
			//							numeric = false;
			//						}
			//					}
			//					if (!numeric)
			//					{
			//						GuestPromotionSet gps = new GuestPromotionSet(new Q(GuestPromotion.Columns.Shortcut,arr[1].ToLower()));
			//						if (gps.Count>0)
			//							GuestPromotionK = gps[0].K;
			//					}
			//				}
			//				if (GuestPromotionK==0)
			//				{
			//					Mobile m = new Mobile(number, network, ServiceTypes.TextGuest, 0);
			//					this.MobileK = m.K;
			//					this.ServiceType=ServiceTypes.TextGuest;
			//					this.Mobile.TotalIncoming++;
			//					this.Mobile.Update();
			//					this.Update();
			//
			//					OutgoingSms.CreateOutgoingSmsSet(
			//						"Sorry, you have not entered an event code. This should be a number - it tells us which guestlist to put you on. Send GUEST followed by the code to 83248.",
			//						OutgoingSms.Types.GuestError, 
			//						this.Mobile, 
			//						this,
			//						ServiceTypes.TextGuest);
			//					return;
			//				}
			//				GuestPromotion gp = null;
			//				try
			//				{
			//					gp = new GuestPromotion(GuestPromotionK);
			//				}
			//				catch
			//				{
			//					Mobile m = new Mobile(number, network, ServiceTypes.TextGuest, 0);
			//					this.MobileK = m.K;
			//					this.ServiceType=ServiceTypes.TextGuest;
			//					this.Mobile.TotalIncoming++;
			//					this.Mobile.Update();
			//					this.Update();
			//
			//					OutgoingSms.CreateOutgoingSmsSet(
			//						"Sorry, you have entered an invalid event code. This should be a number - it tells us which guestlist to put you on. Send GUEST followed by the code to 83248.",
			//						OutgoingSms.Types.GuestError, 
			//						this.Mobile, 
			//						this,
			//						ServiceTypes.TextGuest);
			//					return;
			//				}
			//
			//				Mobile m1 = new Mobile(number, network, ServiceTypes.TextGuest, gp.Event.GuestClientK);
			//				this.MobileK = m1.K;
			//				this.ServiceType=ServiceTypes.TextGuest;
			//				this.Mobile.TotalIncoming++;
			//				this.Mobile.Update();
			//				this.Update();
			//
			//				ProcessGuest(gp);
			//				#endregion
			//			}
			#endregion
		}
		#region removed
		//		#region ProcessGuest
		//		protected void ProcessGuest(GuestPromotion gp)
		//		{
		//			if (gp.Event.Date<DateTime.Today)
		//			{
		//				OutgoingSms.CreateOutgoingSmsSet(
		//					gp.Event.AfterEventMessage,
		//					OutgoingSms.Types.GuestError, 
		//					this.Mobile, 
		//					this,
		//					ServiceTypes.TextGuest);
		//				return;
		//			}
		//			else if (gp.Event.CodeSent)
		//			{
		//				OutgoingSms.CreateOutgoingSmsSet(
		//					gp.Event.ListClosedMessage,
		//					OutgoingSms.Types.GuestError, 
		//					this.Mobile, 
		//					this,
		//					ServiceTypes.TextGuest);
		//				return;
		//			}
		//			string result = gp.CreateGuest(this.Mobile);
		//			if (result.StartsWith("d"))
		//			{
		//				OutgoingSms.CreateOutgoingSmsSet(
		//					gp.Event.ConfirmationMessage.Replace("XXXXX",result.Substring(1)),
		//					OutgoingSms.Types.GuestConfirmation, 
		//					this.Mobile, 
		//					this,
		//					ServiceTypes.TextGuest);
		//				return;
		//			}
		//			else if (result.StartsWith("e"))
		//			{
		//				OutgoingSms.CreateOutgoingSmsSet(
		//					"Sorry, there has been an error. Send GUEST followed by the event code to 83248.",
		//					OutgoingSms.Types.GuestError, 
		//					this.Mobile, 
		//					this,
		//					ServiceTypes.TextGuest);
		//				return;
		//			}
		//			else if (result.StartsWith("c"))
		//			{
		//				OutgoingSms.CreateOutgoingSmsSet(
		//					gp.Event.ConfirmationMessage.Replace("XXXXX",result.Substring(1)),
		//					OutgoingSms.Types.GuestConfirmation,
		//					this.Mobile, 
		//					this,
		//					ServiceTypes.TextGuest);
		//				return;
		//			}
		//		}
		//		#endregion
		//		#region ProcessGuestEnd
		//		protected void ProcessGuestEnd()
		//		{
		//			OutgoingSms.CreateOutgoingSmsSet("You will not receive any more "+this.Mobile.Client.Name+" updates.",OutgoingSms.Types.GuestMisc, this.Mobile, this);
		//			this.Mobile.SendUpdates=false;
		//			this.Mobile.Update();
		//		}
		//		#endregion
		
		//#region ProcessTonightInfo
		//protected void ProcessTonightInfo()
		//{
		//    OutgoingSms.CreateOutgoingSmsSet(OutgoingSms.Types.HelpListings, this.Mobile, this);
		//    OutgoingSms.CreateOutgoingSmsSet(OutgoingSms.Types.HelpMusicTypes, this.Mobile, this);
		//    OutgoingSms.CreateOutgoingSmsSet(OutgoingSms.Types.HelpContinuation, this.Mobile, this);
		//    OutgoingSms.CreateOutgoingSmsSet(OutgoingSms.Types.HelpEvents, this.Mobile, this);
		//    this.Mobile.SentIntro = true;
		//    this.Mobile.Update();
		//}
		//#endregion
		//#region ProcessTonightEnd
		//protected void ProcessTonightEnd()
		//{
		//    OutgoingSms.CreateOutgoingSmsSet("You will not receive any more TONIGHT updates.", OutgoingSms.Types.TonightEnd, this.Mobile, this);
		//    this.Mobile.SendUpdates = false;
		//    this.Mobile.Update();
		//}
		//#endregion
		//#region ProcessTonightError
		//protected void ProcessTonightError()
		//{
		//    OutgoingSms.CreateOutgoingSmsSet(OutgoingSms.Types.Error, this.Mobile, this);
		//}
		//#endregion
		//#region ProcessTonightEventDetail
		//protected void ProcessTonightEventDetail(Event ev)
		//{
		//    this.Mobile.EventDetailRequests++;
		//    this.Mobile.Update();
		//    string txt = "";

		//    txt += ev.Name + "\n";
		//    if (ev.DateTime != DateTime.Today)
		//        txt += ev.FriendlyDate(true) + "\n";
		//    txt += ev.Venue.Name;
		//    if (ev.Venue.Postcode.Length > 0)
		//        txt += " (" + ev.Venue.Postcode.ToUpper() + ")";
		//    txt += "\n";

		//    bool doneOne = false;
		//    foreach (MusicType mt in ev.MusicTypes)
		//    {
		//        txt += (doneOne ? ", " : "") + mt.SmsName;
		//        doneOne = true;
		//    }
		//    if (doneOne)
		//        txt += "\n";

		//    txt += ev.Capacity + " capacity\n\n";

		//    if (Cambro.Web.Helpers.StripHtml(ev.LongDetailsHtml).Length < 300)
		//        txt += Cambro.Web.Helpers.StripHtml(ev.LongDetailsHtml);
		//    else
		//        txt += Cambro.Web.Helpers.StripHtml(ev.ShortDetailsHtml);


		//    OutgoingSms.CreateOutgoingSmsSet(txt, OutgoingSms.Types.EventDetail, this.Mobile, this);


		//}
		//#endregion
		//#region ProcessTonightVenueDetail
		//protected void ProcessTonightVenueDetail(Venue v, Event e)
		//{
		//    this.Mobile.VenueDetailRequests++;
		//    this.Mobile.Update();
		//    string txt = "";

		//    txt += v.Name;
		//    if (v.Postcode.Length > 0)
		//        txt += " (" + v.Postcode.ToUpper() + ")";
		//    txt += "\n";

		//    txt += v.Capacity + " capacity\n\n";

		//    txt += Cambro.Web.Helpers.StripHtml(v.DetailsHtml);

		//    Query q = new Query();
		//    q.QueryCondition = new And(
		//        Event.FutureEventsQueryCondition,
		//        new Q(Event.Columns.K, QueryOperator.NotEqualTo, e.K),
		//        new Q(Event.Columns.VenueK, v.K)
		//        );
		//    q.OrderBy = Event.FutureEventOrder;
		//    q.TopRecords = 5;
		//    EventSet es = new EventSet(q);
		//    if (es.Count > 0)
		//    {
		//        txt += "\n\nMore events:\n";
		//        foreach (Event moreEvent in es)
		//        {
		//            txt += "* " + moreEvent.Name + " " + moreEvent.FriendlyDate(false) + " (code " + moreEvent.K.ToString() + ")\n";
		//        }
		//    }

		//    OutgoingSms.CreateOutgoingSmsSet(txt, OutgoingSms.Types.VenueDetail, this.Mobile, this);

		//}
		//#endregion
		//#region ProcessTonightListings
		//protected void ProcessTonightListings(MusicTypeSet mts, PlaceSet ps)
		//{

		//    this.Mobile.ListingsRequests++;
		//    this.Mobile.Update();

		//    ArrayList musicTypes = new ArrayList();
		//    ArrayList musicTypeQs = new ArrayList();
		//    ArrayList places = new ArrayList();
		//    ArrayList placeQs = new ArrayList();

		//    Q musicTypeFinalOr = new Q(true);
		//    if (musicTypeQs.Count > 0)
		//        musicTypeFinalOr = new Or((Q[])musicTypeQs.ToArray(typeof(Q)));

		//    Q placeFinalOr = new Q(true);
		//    if (placeQs.Count > 0)
		//        placeFinalOr = new Or((Q[])placeQs.ToArray(typeof(Q)));

		//    Query eventQ = new Query();
		//    if (musicTypes.Count > 0)
		//        eventQ.TableElement = Event.PlaceAndMusicTypeJoin;
		//    else
		//        eventQ.TableElement = Event.PlaceAndMusicTypeLeftJoin;
		//    eventQ.OrderBy = Event.FutureEventOrder;

		//    DateTime date = DateTime.Today;
		//    if (DateTime.Now.Hour < 4)
		//        date = DateTime.Today.AddDays(-1);

		//    eventQ.QueryCondition = new And(
		//        new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, date),
		//        new Q(Event.Columns.DateTime, QueryOperator.LessThan, date.AddDays(1)),
		//        musicTypeFinalOr,
		//        placeFinalOr);
		//    EventSet es = new EventSet(eventQ);

		//    ArrayList doneEvents = new ArrayList();
		//    string txt = "";
		//    foreach (Event e in es)
		//    {
		//        if (!doneEvents.Contains(e.K))
		//        {
		//            doneEvents.Add(e.K);
		//            txt += "* " + e.Name + " @ " + e.Venue.Name;
		//            if (e.MusicTypes.Count > 0)
		//            {
		//                txt += " - ";
		//                bool doneOne = false;
		//                ArrayList doneMusic = new ArrayList();
		//                foreach (MusicType mt in e.MusicTypes)
		//                {
		//                    if ((mt.ParentK == 1 && !doneMusic.Contains(mt.K)) || (mt.K == 1 && !doneMusic.Contains(1)) || ((mt.ParentK != 1 && mt.ParentK != 0) && !doneMusic.Contains(mt.ParentK)))
		//                    {
		//                        if (mt.ParentK == 1 || mt.ParentK == 0)
		//                        {
		//                            txt += (doneOne ? ", " : "") + mt.SmsCode;
		//                            doneOne = true;
		//                            doneMusic.Add(mt.K);
		//                        }
		//                        else
		//                        {
		//                            txt += (doneOne ? ", " : "") + mt.Parent.SmsCode;
		//                            doneOne = true;
		//                            doneMusic.Add(mt.Parent.K);
		//                        }
		//                    }
		//                }
		//                //txt += "";
		//            }
		//            txt += " (code " + e.K.ToString() + ")";
		//            txt += "\n";
		//        }
		//    }

		//    if (doneEvents.Count > 0)
		//        OutgoingSms.CreateOutgoingSmsSet(txt, OutgoingSms.Types.EventsList, this.Mobile, this);
		//    else
		//    {
		//        string music = " for ";
		//        if (mts.Count > 0)
		//        {
		//            ArrayList musics = new ArrayList();
		//            foreach (MusicType mt in mts)
		//            {
		//                if (mt.ParentK == 1)
		//                    musics.Add(mt);
		//            }
		//            bool doneOneMt = false;

		//            for (int i = 0; i < musics.Count; i++)
		//            {
		//                MusicType mt = (MusicType)musics[i];
		//                music += (doneOneMt ? (i == (musics.Count - 1) ? " or " : ", ") : "") + mt.GenericName.ToLower();
		//                doneOneMt = true;
		//            }
		//        }
		//        else
		//            music = "";

		//        string placesStr = "";
		//        if (ps.Count > 0)
		//        {
		//            bool doneOneP = false;
		//            for (int i = 0; i < ps.Count; i++)
		//            {
		//                Place p = ps[i];
		//                placesStr += (doneOneP ? (i == (ps.Count - 1) ? " or " : ", ") : "") + p.Name;
		//                doneOneP = true;
		//            }
		//        }
		//        else
		//            placesStr = "nationwide";

		//        string txtNoEvents = "Sorry, no " + placesStr + " listings" + music + " tonight. You have only been charged 25p this time. Try again later.";
		//        OutgoingSms.CreateOutgoingSmsSet(txtNoEvents, OutgoingSms.Types.EventsListNone, this.Mobile, this);
		//    }
		//}
		//#endregion
#endregion
		#region Mobile
		public Mobile Mobile
		{
			get
			{
				if (mobile == null && MobileK > 0)
					mobile = new Mobile(MobileK);
				return mobile;
			}
			set
			{
				mobile = value;
			}
		}
		private Mobile mobile;
		#endregion

	 

	}
	#endregion

}
