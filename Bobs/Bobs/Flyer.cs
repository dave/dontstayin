using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct Bob class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.

	#region Flyer
	/// <summary>
	/// eFlyers sent out by Promoters
	/// </summary>
	[Serializable]
	public partial class Flyer
	{

		#region Simple members
		/// <summary>
		/// K of the Flyer
		/// </summary>
		public override int K
		{
			get { return this[Flyer.Columns.K] as int? ?? 0; }
			set { this[Flyer.Columns.K] = value; }
		}
		/// <summary>
		/// Promoter
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[Flyer.Columns.PromoterK]; }
			set { this[Flyer.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// Name to identify this flyer run
		/// </summary>
		public override string Name
		{
			get { return (string)this[Flyer.Columns.Name]; }
			set { this[Flyer.Columns.Name] = value; }
		}
		/// <summary>
		/// Subject for the email
		/// </summary>
		public override string Subject
		{
			get { return (string)this[Flyer.Columns.Subject]; }
			set { this[Flyer.Columns.Subject] = value; }
		}
		/// <summary>
		/// Background colour of the email to make it blend with flyer. Use hex, e.g. #D2D2D2, but without the #
		/// </summary>
		public override string BackgroundColor
		{
			get { return (string)this[Flyer.Columns.BackgroundColor]; }
			set { this[Flyer.Columns.BackgroundColor] = value; }
		}
		/// <summary>
		/// Flyer image K entry in Misc table
		/// </summary>
		public override int MiscK
		{
			get { return (int)this[Flyer.Columns.MiscK]; }
			set { this[Flyer.Columns.MiscK] = value; }
		}
		/// <summary>
		/// DateTime when to send the eFlyer
		/// </summary>
		public override DateTime SendDateTime
		{
			get { return (DateTime)this[Flyer.Columns.SendDateTime]; }
			set { this[Flyer.Columns.SendDateTime] = value; }
		}
		/// <summary>
		/// Url to redirect to when clicking on the Flyer image
		/// </summary>
		public override string LinkTargetUrl
		{
			get { return (string)this[Flyer.Columns.LinkTargetUrl]; }
			set { this[Flyer.Columns.LinkTargetUrl] = value; }
		}
		/// <summary>
		/// Comma-delimited list of PlaceKs to which this Flyer is targetted
		/// </summary>
		public override string PlaceKs
		{
			get { return (string)this[Flyer.Columns.PlaceKs]; }
			set { this[Flyer.Columns.PlaceKs] = value; }
		}
		/// <summary>
		/// Comma-delimited list of MusicTypeKs to which this Flyer is targetted
		/// </summary>
		public override string MusicTypeKs
		{
			get { return (string)this[Flyer.Columns.MusicTypeKs]; }
			set { this[Flyer.Columns.MusicTypeKs] = value; }
		}
		/// <summary>
		/// Total eFlyers we have sent
		/// </summary>
		public override int Sends
		{
			get { return (int)this[Flyer.Columns.Sends]; }
			set { this[Flyer.Columns.Sends] = value; }
		}
		/// <summary>
		/// Total times image has been viewed (downloaded) (or displayed in popup if the user clicks "I can't se
		/// </summary>
		public override int Views
		{
			get { return (int)this[Flyer.Columns.Views]; }
			set { this[Flyer.Columns.Views] = value; }
		}
		/// <summary>
		/// Total clicks on the email image (or the popup)
		/// </summary>
		public override int Clicks
		{
			get { return (int)this[Flyer.Columns.Clicks]; }
			set { this[Flyer.Columns.Clicks] = value; }
		}
		/// <summary>
		/// Total people who unsubscribed because of this flyer
		/// </summary>
		public override int Unsubscribes
		{
			get { return (int)this[Flyer.Columns.Unsubscribes]; }
			set { this[Flyer.Columns.Unsubscribes] = value; }
		}
		/// <summary>
		/// Optional display name in the From field on the sent email
		/// </summary>
		public override string MailFromDisplayName
		{
			get { return (string)this[Flyer.Columns.MailFromDisplayName]; }
			set { this[Flyer.Columns.MailFromDisplayName] = value; }
		}
		/// <summary>
		/// Is eFlyer only to be sent to Usrs with IsPromoter true?
		/// </summary>
		public override bool PromotersOnly
		{
			get { return (bool)this[Flyer.Columns.PromotersOnly]; }
			set { this[Flyer.Columns.PromotersOnly] = value; }
		}
		/// <summary>
		/// Is this eFlyer confirmed by admin and queued up to send?
		/// </summary>
		public override bool IsReadyToSend
		{
			get { return (bool)this[Flyer.Columns.IsReadyToSend]; }
			set { this[Flyer.Columns.IsReadyToSend] = value; }
		}
		/// <summary>
		/// Is this eFlyer currently in the process of sending?
		/// </summary>
		public override bool IsSending
		{
			get { return (bool)this[Flyer.Columns.IsSending]; }
			set { this[Flyer.Columns.IsSending] = value; }
		}
		/// <summary>
		/// If the eFlyer has been stopped mid-sending, the last usrK reached
		/// </summary>
		public override int PausedAtUsrK
		{
			get { return (int)this[Flyer.Columns.PausedAtUsrK]; }
			set { this[Flyer.Columns.PausedAtUsrK] = value; }
		}
		/// <summary>
		/// Has this eFlyer successfully finished sending?
		/// </summary>
		public override bool HasFinishedSending
		{
			get { return (bool)this[Flyer.Columns.HasFinishedSending]; }
			set { this[Flyer.Columns.HasFinishedSending] = value; }
		}
		/// <summary>
		/// Comma-separated list of EventKs, Usrs Attended of which to target this Flyer to
		/// </summary>
		public override string EventKs
		{
			get { return (string)this[Flyer.Columns.EventKs]; }
			set { this[Flyer.Columns.EventKs] = value; }
		}
		/// <summary>
		/// When was the last activity?
		/// </summary>
		public override DateTime? DateTimeLastMessageSent
		{
			get { return (DateTime?)this[Flyer.Columns.DateTimeLastMessageSent]; }
			set { this[Flyer.Columns.DateTimeLastMessageSent] = value; }
		}
		/// <summary>
		/// Is this an HTML eFlyer?
		/// </summary>
		public override bool IsHtml
		{
			get { return (bool)this[Flyer.Columns.IsHtml]; }
			set { this[Flyer.Columns.IsHtml] = value; }
		}
		/// <summary>
		/// Html to send for HTML eFlyers
		/// </summary>
		public override string Html
		{
			get { return (string)this[Flyer.Columns.Html]; }
			set { this[Flyer.Columns.Html] = value; }
		}
		/// <summary>
		/// Text to send as the alternative to HTML
		/// </summary>
		public override string TextAlternative
		{
			get { return (string)this[Flyer.Columns.TextAlternative]; }
			set { this[Flyer.Columns.TextAlternative] = value; }
		}
		/// <summary>
		/// How many emasils were skipped due to broken emails?
		/// </summary>
		public override int Broken
		{
			get { return (int)this[Flyer.Columns.Broken]; }
			set { this[Flyer.Columns.Broken] = value; }
		}
		/// <summary>
		/// How many emasils were skipped due to exceptions?
		/// </summary>
		public override int Exceptions
		{
			get { return (int)this[Flyer.Columns.Exceptions]; }
			set { this[Flyer.Columns.Exceptions] = value; }
		}
		/// <summary>
		/// How many retries due to mail server out of space?
		/// </summary>
		public override int MailServerRetries
		{
			get { return (int)this[Flyer.Columns.MailServerRetries]; }
			set { this[Flyer.Columns.MailServerRetries] = value; }
		}
		/// <summary>
		/// When was the last mail server retry?
		/// </summary>
		public override DateTime? MailServerLastRetry
		{
			get { return (DateTime?)this[Flyer.Columns.MailServerLastRetry]; }
			set { this[Flyer.Columns.MailServerLastRetry] = value; }
		}
		#endregion


		public List<Event> Events
		{
			get
			{
				if (!string.IsNullOrEmpty(this.EventKs))
				{
					return this.EventKs.CommaSeparatedValuesToIntList().ConvertAll(k => new Event(k));
				}
				return null;
			}
		}

		private string[] validationErrors;
		public string[] ValidationErrors
		{
			get
			{
				if (validationErrors == null)
				{
					List<string> errors = new List<string>();
					if (PromoterK == 0) errors.Add("No promoter selected");
					if (Subject.Trim() == "") errors.Add("No subject");
					if (!LinkTargetUrl.StartsWith("http://")) errors.Add("Link must begin with http://");
					if (MiscK == 0 && !IsHtml) errors.Add("No image uploaded");
					if (this.Events != null)
					{
						//foreach (Event e in this.Events)
						//{
						//    if (e.Brands.FindFirstIndex(b => b.PromoterK == this.PromoterK) == null)
						//        errors.Add(string.Format("EventK {0} ({1}) doesn't belong to this promoter", e.K, e.Name));
						//}
						if (this.PlaceKs.Length > 0) errors.Add("Cannot target both event and places");
						if (this.MusicTypeKs.Length > 0) errors.Add("Cannot target both event and music types");
					}
					validationErrors = errors.ToArray();
				}
				return validationErrors;
			}
		}
		public bool CanSend
		{
			get { return !this.HasFinishedSending && ValidationErrors.Length == 0; }
		}

		public bool IsEditable
		{
			get { return !this.HasFinishedSending && !this.IsReadyToSend; }
		}


		#region Misc
		public Misc Misc
		{
			get
			{
				if (misc == null && MiscK > 0)
					misc = new Misc(MiscK);
				return misc;
			}
		}
		private Misc misc;
		#endregion

		#region Promoter
		public Promoter Promoter
		{
			get
			{
				if (promoter == null && PromoterK > 0)
					promoter = new Promoter(PromoterK);
				return promoter;
			}
		}
		private Promoter promoter;
		#endregion


		#region Select Usrs queries
		#region CountUsrs
		public int CountUsrs()
		{
			return CountUsrs(this.EventKs.CommaSeparatedValuesToIntList(), this.PlaceKs.CommaSeparatedValuesToIntList(), this.MusicTypeKs.CommaSeparatedValuesToIntList(), this.PromotersOnly);
		}
		public static int CountUsrs(string eventKs, string placeKs, string musicTypeKs, bool promotersOnly)
		{
			return CountUsrs(eventKs.CommaSeparatedValuesToIntList(), placeKs.CommaSeparatedValuesToIntList(), musicTypeKs.CommaSeparatedValuesToIntList(), promotersOnly);
		}
		public static int CountUsrs(List<int> placeKs, List<int> musicTypeKs, bool promotersOnly)
		{
			return CountUsrs(null, placeKs, musicTypeKs, promotersOnly);
		}
		public static int CountUsrs(List<int> eventKs, List<int> placeKs, List<int> musicTypeKs, bool promotersOnly)
		{
			Query q = new Query();

			q.OverideSql = SelectUsrsSql(eventKs, placeKs, musicTypeKs, promotersOnly, 0, 0, true);

			UsrSet us = new UsrSet(q);
			return (us.Count == 0) ? 0 : (int)us[0].ExtraSelectElements["Count"];
		}
		#endregion
		#region GetUsrs
		public UsrSet GetUsrs()
		{
			return GetUsrs(this.EventKs.CommaSeparatedValuesToIntList(), this.PlaceKs.CommaSeparatedValuesToIntList(), this.MusicTypeKs.CommaSeparatedValuesToIntList(), this.PromotersOnly);
		}
		public UsrSet GetUsrs(int minimumUsrK, int maximumUsrK)
		{
			return GetUsrs(this.EventKs.CommaSeparatedValuesToIntList(), this.PlaceKs.CommaSeparatedValuesToIntList(), this.MusicTypeKs.CommaSeparatedValuesToIntList(), this.PromotersOnly, minimumUsrK, maximumUsrK);
		}
		public static UsrSet GetUsrs(List<int> eventKs, List<int> placeKs, List<int> musicTypeKs, bool promotersOnly)
		{
			return GetUsrs(eventKs, placeKs, musicTypeKs, promotersOnly, 0, 0);
		}
		public static UsrSet GetUsrs(List<int> eventKs, List<int> placeKs, List<int> musicTypeKs, bool promotersOnly, int minimumUsrK, int maximumUsrK)
		{
			Query q = new Query();
			q.OverideSql = SelectUsrsSql(eventKs, placeKs, musicTypeKs, promotersOnly, minimumUsrK, maximumUsrK, false);
			return new UsrSet(q);
		}
		#endregion

		public static string SelectUsrsSql(List<int> eventKs, List<int> placeKs, List<int> musicTypeKs, bool promotersOnly, int minimumUsrK, int maximumUsrK, bool returnCountOnly)
		{
			bool checkPlaces = CheckPlaceKs(placeKs);
			bool checkMusicTypes = CheckMusicTypeKs(musicTypeKs);
			string select = (returnCountOnly) ? "COUNT(DISTINCT [Usr].[K]) as Count" : "DISTINCT [Usr].[K], [Usr].[Email], [Usr].[LoginString], [Usr].[IsEmailVerified], [Usr].[IsEmailBroken], [Usr].[BouncePeriodDateTime], [Usr].[TotalEmailsSentInPeriod], [Usr].[MatchedHardBounceInPeriod], [Usr].[UnmatchedHardBounceInPeriod], [Usr].[SoftBounceInPeriod] ";
			string usrConditions = "[Usr].[SendFlyers] = 1  AND  [Usr].[IsSkeleton] = 0  AND  [Usr].[Banned] = 0  AND [Usr].[EmailHold] = 0 ";
			usrConditions += (minimumUsrK > 0) ? " AND [Usr].[K] >= " + minimumUsrK : "";
			usrConditions += (maximumUsrK > 0) ? " AND [Usr].[K] <= " + maximumUsrK : "";
			string orderBy = (returnCountOnly) ? "" : " ORDER BY [Usr].[K]";

			if (eventKs != null && eventKs.Count > 0)
			{
				return string.Format(@"
SELECT {0}
FROM     [Usr] WITH (NOLOCK)
INNER JOIN [UsrEventAttended] (NOLOCK) ON [Usr].[K] = [UsrEventAttended].[UsrK]
WHERE  ( {1} AND [UsrEventAttended].[EventK] IN ({3}) ) {2}",
				select,
				usrConditions,
				orderBy,
				string.Join(",", eventKs.ConvertAll(i => i.ToString()).ToArray()));
			}

			usrConditions += (promotersOnly) ? " AND [Usr].[IsPromoter] = 1 " : "";

			if (!checkPlaces && !checkMusicTypes)
			{
				return string.Format(@"
SELECT {0}
FROM     [Usr] WITH (NOLOCK)
WHERE  ( {1} ) {2}", 
				select,
				usrConditions,
				orderBy);
			}
			else if (checkPlaces && !checkMusicTypes)
			{
				return string.Format(@"
SELECT {0}
FROM     [Usr] WITH (NOLOCK) LEFT JOIN [UsrPlaceVisit] WITH (NOLOCK) ON ([Usr].[K] = [UsrPlaceVisit].[UsrK] )  
LEFT JOIN [UsrEventAttended] WITH (NOLOCK) ON ([Usr].[K] = [UsrEventAttended].[UsrK] )  
LEFT JOIN [Event] WITH (NOLOCK) ON ([UsrEventAttended].[EventK] = [Event].[K] )  
LEFT JOIN [Venue] WITH (NOLOCK) ON ([Event].[VenueK] = [Venue].[K] )  
WHERE  (  ( {1} )  
AND  (  ( [HomePlaceK] IN ({2}) )  OR  ( [UsrPlaceVisit].[PlaceK] IN ({2}) )  OR  ( [Venue].[PlaceK] IN ({2}) )  )  ) {3}", 
				select,
				usrConditions,
				string.Join(",", Array.ConvertAll(placeKs.ToArray(), i => i.ToString())),
				orderBy);
			}
			else if (!checkPlaces && checkMusicTypes)
			{
				return string.Format(@"
SELECT {0}
FROM     [Usr] WITH (NOLOCK) LEFT JOIN [UsrMusicTypeFavourite] WITH (NOLOCK) ON ([Usr].[K] = [UsrMusicTypeFavourite].[UsrK] ) 
WHERE  (  ( {1} )  
AND  ( [UsrMusicTypeFavourite].[MusicTypeK] IN ({2}) )  ) {3}", 
				select,
				usrConditions,
				string.Join(",", Array.ConvertAll(MusicType.GetAllApplicableMusicTypeKs(musicTypeKs).ToArray(), i => i.ToString())),
				orderBy);
			}
			else //both
			{
				return string.Format(@"
SELECT {0}
FROM     [Usr] WITH (NOLOCK) LEFT JOIN [UsrPlaceVisit] WITH (NOLOCK) ON ([Usr].[K] = [UsrPlaceVisit].[UsrK] )  
LEFT JOIN [UsrEventAttended] WITH (NOLOCK) ON ([Usr].[K] = [UsrEventAttended].[UsrK] )  
LEFT JOIN [Event] WITH (NOLOCK) ON ([UsrEventAttended].[EventK] = [Event].[K] )  
LEFT JOIN [Venue] WITH (NOLOCK) ON ([Event].[VenueK] = [Venue].[K] )  
LEFT JOIN [UsrMusicTypeFavourite] WITH (NOLOCK) ON ([Usr].[K] = [UsrMusicTypeFavourite].[UsrK] ) 
WHERE  (  ( {1} )  
AND  (  ( [HomePlaceK] IN ({2}) )  OR  ( [UsrPlaceVisit].[PlaceK] IN ({2}) )  OR  ( [Venue].[PlaceK] IN ({2}) )  ) 
AND  ( [UsrMusicTypeFavourite].[MusicTypeK] IN ({3}) )  ) {4}",
				select,
				usrConditions,
				string.Join(",", Array.ConvertAll(placeKs.ToArray(), i => i.ToString())),
				string.Join(",", Array.ConvertAll(MusicType.GetTheseAndChildMusicTypeKs(musicTypeKs).ToArray(), i => i.ToString())),
				orderBy);
			}
		}

		private static bool CheckPlaceKs(List<int> placeKs)
		{
			return placeKs.Count > 0;
		}
		private static bool CheckMusicTypeKs(List<int> musicTypeKs)
		{
			return musicTypeKs.Count > 0 && !musicTypeKs.Contains(1);
		}
		#endregion

		#region Links
		public string PopupUrl
		{
			get
			{
				return "http://" + Vars.DomainName + "/popup/flyer/k-" + this.K;
			}
		}
		public string ImgSrcTrackingViews
		{
			get
			{
				if (this.MiscK == 0)
				{
					return "http://" + Vars.DomainName + "/images/flyer/" + this.K + ".gif";
				}
				else
				{
					return "http://" + Vars.DomainName + "/images/flyer/" + this.K + this.Misc.ExtentionWithDot;
				}
			}
		}
		public string LinkTargetUrlTrackingClicks
		{
			get
			{
				return "http://" + Vars.DomainName + "/popup/flyerclick/k-" + this.K;
			}
		}
		#endregion

		#region Send!
		/// <summary>
		/// Careful!! this is the live, send emails, count sends method!
		/// </summary>
		public void SendMail()
		{
			SendMail(this.GetUsrs(), true, this.PausedAtUsrK);
		}
		public void SendMail(UsrSet us, bool liveSend, int startUsrK)
		{
			Console.WriteLine("Flyer.SendMail v3 - with IsEmailBroken flag...");

			if (liveSend && this.IsReadyToSend && !this.IsSending)
			{
				if (this.Sends == 0)
					this.SendDateTime = DateTime.Now;
				this.IsSending = true;
				this.Update();
			}

			string body = "";
			if (!IsHtml)
			{
				body = string.Format(@"
<div style=""padding-top:10px;padding-bottom:10px;{0}""><center>
<a href=""{1}"">Click here if you can't see the e-flyer below</a><br>
<a href=""{2}""><img src=""{3}"" border=""0"" width=""{4}"" height=""{5}"" vspace=""10""></a><br>",
					(this.BackgroundColor == "") ? "" : @"background-color:#" + this.BackgroundColor + ";",
					this.PopupUrl,
					this.LinkTargetUrlTrackingClicks,
					this.ImgSrcTrackingViews,
					this.Misc.Width,
					this.Misc.Height);
			}
			//string unsubscribeLink = "/popup/unsubscribeeflyers/usrk-"  "/flyerk-" + this.K;

			Random r = new Random();
			const int intervalToUpdateSends = 100;
			//int startIndex = usrList.FindIndex(u => u.K >= startUsrK);
			int skipped = 0;

			for (int i = 0; i < us.Count; i++)
			{
				try
				{
					Usr u = us[i];
					
					//string unsubscribeLinkNoDomain = "http://" + Vars.DomainName + "/popup/unsubscribeeflyers/usrk-" + u.K.ToString() + "/loginstring-" + u.LoginString.ToLower() + "/flyerk-" + this.K;
					//string unsubscribeLinkWithDomain = "http://" + Vars.DomainName + "/popup/unsubscribeeflyers/usrk-" + u.K.ToString() + "/loginstring-" + u.LoginString.ToLower() + "/flyerk-" + this.K;
					string unsubscribeLinkWithDomain = u.LoginAndTransfer("/popup/unsubscribeeflyers/flyerk-" + this.K);

					if (u.K > startUsrK)
					{
						
						if (liveSend && i % intervalToUpdateSends == 0)
						{
							//this.Update();
							
							// check that we're still good to send, not been paused 
							// this'll be cached until anything changes
							if (new Flyer(this.K).IsReadyToSend == false)
							{
								this.IsSending = false;
								this.Update();
								return;
							}
						}
						
						System.Net.Mail.SmtpClient c = new System.Net.Mail.SmtpClient();
						c.Host = Common.Properties.GetDefaultSmtpServer(r);

						System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
						
						m.IsBodyHtml = true;

						if (!IsHtml)
						{
							m.Body = body + @"
<a href=""" + unsubscribeLinkWithDomain + @""">Click here to unsubscribe from DontStayIn e-flyers</a>
</center></div>
";
						}
						else
						{

							if (this.TextAlternative.Length > 0)
							{
								System.Net.Mail.AlternateView alt = System.Net.Mail.AlternateView.CreateAlternateViewFromString(this.TextAlternative + 
								@"

Click here to unsubscribe from DontStayIn e-flyers:
" + unsubscribeLinkWithDomain,
								null, "text/plain");
								m.AlternateViews.Add(alt);

								System.Net.Mail.AlternateView html = System.Net.Mail.AlternateView.CreateAlternateViewFromString(
									@"<p style=""margin:5px;padding:5px;background-color:#ffffff;""><center><a href=""" + unsubscribeLinkWithDomain + @""">Click here to unsubscribe from DontStayIn e-flyers</a><img src=""" + this.ImgSrcTrackingViews + @""" width=""1"" height=""1"" border=""0"" /></center></p>" +
									this.Html,
									null,
									"text/html");
								m.AlternateViews.Add(html);
							}
							else
							{
								m.Body = @"<p style=""margin:5px;padding:5px;background-color:#ffffff;""><center><a href=""" + unsubscribeLinkWithDomain + @""">Click here to unsubscribe from DontStayIn e-flyers</a><img src=""" + this.ImgSrcTrackingViews + @""" width=""1"" height=""1"" border=""0"" /></center></p>" +
									this.Html;
							}
							
						}


						m.Subject = this.Subject;
						string To = u.Email;

						m.From = (this.MailFromDisplayName.Trim().Length > 0) ?
							new System.Net.Mail.MailAddress(Vars.AdminReplyAddress, this.MailFromDisplayName.Trim()) :
							new System.Net.Mail.MailAddress(Vars.AdminReplyAddress);
						
						//System.Threading.Thread.Sleep(50);

						if (!u.IsEmailBroken)
						{
							if (Vars.DevEnv)
							{
								m.Subject += " (to:" + To + ") (" + DateTime.Now.ToString() + ")";
								m.To.Add("dev.mail@dontstayin.com");
								if (u.K == 8)
									m.To.Add(To);
							}
							else
							{
								m.To.Add(To);
							}

							bool done = false;
							do
							{
								try
								{
									c.Send(m);
									done = true;

									u.RegisterMailSentForBounceTracking();
								}
								catch (Exception ex)
								{
								    if (ex.Message.Contains("Insufficient system storage"))
								    {
								        Update up = new Update();
								        up.Changes.Add(new Assign.Increment(Flyer.Columns.MailServerRetries));
								        up.Changes.Add(new Assign(Flyer.Columns.MailServerLastRetry, DateTime.Now));
								        up.Changes.Add(new Assign(Flyer.Columns.DateTimeLastMessageSent, DateTime.Now));
								        up.Table = TablesEnum.Flyer;
								        up.Where = new Q(Flyer.Columns.K, this.K);
								        up.Run();
								        Console.Error.WriteLine("UsrK: {0}, eFlyerK: {1}, retrying after 5 sec.", us[i].K, this.K);

								        System.Threading.Thread.Sleep(5000);
								    }
								    else
								        throw ex;
								}
							}
							while (!done);

							if (liveSend)
							{
								Update up = new Update();
								up.Changes.Add(new Assign.Increment(Flyer.Columns.Sends));
								up.Changes.Add(new Assign(Flyer.Columns.PausedAtUsrK, u.K));
								up.Changes.Add(new Assign(Flyer.Columns.DateTimeLastMessageSent, DateTime.Now));
								up.Table = TablesEnum.Flyer;
								up.Where = new Q(Flyer.Columns.K, this.K);
								up.Run();
							}
						}
						else
						{
							if (liveSend)
							{
								Update up = new Update();
								up.Changes.Add(new Assign.Increment(Flyer.Columns.Broken));
								up.Changes.Add(new Assign(Flyer.Columns.DateTimeLastMessageSent, DateTime.Now));
								up.Table = TablesEnum.Flyer;
								up.Where = new Q(Flyer.Columns.K, this.K);
								up.Run();
							}
						}
						
					}

					if (i % 10 == 0)
						Console.Out.WriteLine("Done " + i + "/" + us.Count);
					
					us.Kill(i);


				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("UsrK: {0}, eFlyerK: {1}, Exception: {2}", us[i].K, this.K, ex.Message);

					if (liveSend)
					{
						Update up = new Update();
						up.Changes.Add(new Assign.Increment(Flyer.Columns.Exceptions));
						up.Changes.Add(new Assign(Flyer.Columns.DateTimeLastMessageSent, DateTime.Now));
						up.Table = TablesEnum.Flyer;
						up.Where = new Q(Flyer.Columns.K, this.K);
						up.Run();
					}
				}
				
			}

			if (liveSend)
			{
				//this.Sends += (us.Count - skipped) % intervalToUpdateSends;
				this.IsSending = false;
				this.HasFinishedSending = true;
				this.Update();
			}
		}
		#endregion

		#region Log helpers
		public void LogSend()
		{
			StoredProcedures.Bobs.Flyer.LogSend.Execute(this.K);
			//SendsCacheCounter.Increment();
		}
		public void LogView()
		{
			StoredProcedures.Bobs.Flyer.LogView.Execute(this.K);
			//ViewsCacheCounter.Increment();
		}
		public void LogClick()
		{
			StoredProcedures.Bobs.Flyer.LogClick.Execute(this.K);
			//ClicksCacheCounter.Increment();
		}
		public void LogUnsubscribe()
		{
			StoredProcedures.Bobs.Flyer.LogUnsubscribe.Execute(this.K);
			//UnsubscribesCacheCounter.Increment();
		}
		#endregion

		#region Cache Counters
		//public Cache.Counter SendsCacheCounter
		//{
		//    get { return new Cache.Counter("Flyer.SendsCacheCounter." + this.K); }
		//}
		//public Cache.Counter ViewsCacheCounter
		//{
		//    get { return new Cache.Counter("Flyer.ViewsCacheCounter." + this.K); }
		//}
		//public Cache.Counter ClicksCacheCounter
		//{
		//    get { return new Cache.Counter("Flyer.ClicksCacheCounter." + this.K); }
		//}
		//public Cache.Counter UnsubscribesCacheCounter
		//{
		//    get { return new Cache.Counter("Flyer.UnsubscribesCacheCounter." + this.K); }
		//}
		#endregion

	}
	#endregion

}
