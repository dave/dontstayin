using System;
using System.Web;
using System.Collections;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Common;
using Bobs.BannerServer;
using Bobs.DataHolders;

namespace Bobs
{

	#region Mailer
	/// <summary>
	/// Minimum requirement - UsrRecipient, Subject, Body, RedirectUrl
	/// </summary>
	public class Mailer
	{
		public Mailer() { }
		public Mailer(Random r)
		{
			this.Rnd = r;
		}

		#region Rnd
		public Random Rnd
		{
			get
			{
				if (rnd == null)
					rnd = new Random();
				return rnd;
			}
			set
			{
				rnd = value;
			}
		}
		private Random rnd;
		#endregion

		public string Body { get { return body; } set { body = value; } }
		private string body;

		public string Subject { get { return subject; } set { subject = value; } }
		private string subject;

		#region To
		public string To
		{
			get
			{
				if (to.Length == 0)
				{
					if (UsrRecipient != null)
					{
						return UsrRecipient.Email;
					}
				}
				return to;
			}
			set { to = value; }
		}
		private string to = "";
		#endregion

		public string OverrideFromAddress { get { return overrideFromAddress; } set { overrideFromAddress = value; } }
		private string overrideFromAddress = "";

		public TemplateTypes TemplateType
		{
			get
			{
				if (templateTypeSet)
					return templateType;
				else
					return TemplateTypes.AnotherSiteUser;
			}
			set
			{
				templateType = value;
				templateTypeSet = true;
			}
		}
		private TemplateTypes templateType;
		private bool templateTypeSet = false;

		#region ShowQuickLink
		public bool ShowQuickLink
		{
			get
			{
				return showQuickLink;
			}
			set
			{
				showQuickLink = value;
			}
		}
		bool showQuickLink = true;
		#endregion

		public string OverrideLoginLink { get { return overrideLoginLink; } set { overrideLoginLink = value; } }
		private string overrideLoginLink = "";

		public string RedirectUrl { get { return redirectUrl; } set { redirectUrl = value; } }
		private string redirectUrl = "";

		#region UsrRecipient
		public Usr UsrRecipient
		{
			get
			{
				return usrRecipient;
			}
			set
			{
				usrRecipient = value;
			}
		}
		private Usr usrRecipient;
		#endregion

		public enum TemplateTypes
		{
			AnotherSiteUser,
			AdminNote,
			SpecialMail
		}

		#region FullBody with replacements
		string GetFullBody()
		{
			if (TemplateType.Equals(TemplateTypes.SpecialMail))
			{
				string body = "";

				Assembly ass = Assembly.GetExecutingAssembly();
				StreamReader sr = new StreamReader(ass.GetManifestResourceStream("Bobs.Emails.DsiEmailTemplateSpecial.htm"));
				string siteUserTemplate = sr.ReadToEnd();

				string logInUrl = "http://" + Vars.DomainName + "/";
				if (UsrRecipient != null)
				{
					logInUrl = UsrRecipient.LoginUrl;
				}
				if (RedirectUrl.Length > 0 && UsrRecipient != null)
				{
					logInUrl = UsrRecipient.LoginAndTransfer(RedirectUrl);
				}
				if (OverrideLoginLink.Length > 0)
				{
					logInUrl = OverrideLoginLink;
				}
				body = siteUserTemplate.Replace("[SUBJECT]", Subject);
				body = body.Replace("[BODY]", Body);
				body = body.Replace("[LOGIN]", logInUrl);

				body = AolCompliance(body);
				body = GenericReplacements(body);
				return body;
			}
			else if (TemplateType.Equals(TemplateTypes.AnotherSiteUser))
			{
				string body = "";

				Assembly ass = Assembly.GetExecutingAssembly();
				StreamReader sr = new StreamReader(ass.GetManifestResourceStream("Bobs.Emails.DsiEmailTemplateSiteUser.htm"));
				string siteUserTemplate = sr.ReadToEnd();
				
				RelevanceHolder rel = new RelevanceHolder();
				UsrRecipient.AddRelevant(rel);

				BannerServer.Rules.RequestRules rules = new Bobs.BannerServer.Rules.RequestRules();

				foreach (int musicTypeK in rel.RelevantMusic)
				{
					rules.MusicTypes.Add(musicTypeK);
				}
				foreach (int placeK in rel.RelevantPlaces)
				{
					rules.PlacesVisited.Add(placeK);
				}
				Bobs.BannerServer.Server server = new Bobs.BannerServer.Server();
				BannerDataHolder bdh = server.GetBanner(Banner.Positions.EmailBanner, false, new UsrIdentity(UsrRecipient), rules);
				//Banner b = null;
				string bannerHtml = "";
				if (bdh != null)
				{
					Banner b = new Banner(bdh.K);
					bannerHtml = @"<table cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td rowspan=""3""><img src=""[WEB-ROOT]gfx/1pix.gif"" width=""23"" height=""90""></td><td rowspan=""3""><a href=""[LOGIN]""><img src=""[WEB-ROOT]gfx/logo-200-90.jpg"" border=""0"" width=""200"" height=""90""></a></td><td rowspan=""3""><img src=""[WEB-ROOT]gfx/1pix.gif"" width=""23"" height=""90""></td><td><img src=""[WEB-ROOT]gfx/1pix.gif"" width=""331"" height=""20""></td><td rowspan=""3""><img src=""[WEB-ROOT]gfx/1pix.gif"" width=""23"" height=""90""></td></tr><tr><td><a href=""[LOGIN(" + b.LinkUrlLive + @")]""><img src=""" + b.Misc.Url() + @""" border=""0"" width=""331"" height=""51""></a></td></tr><tr><td><img src=""[WEB-ROOT]gfx/1pix.gif"" width=""331"" height=""19""></td></tr></table>";
					b.RegisterHit(new UsrIdentity(UsrRecipient));
				}
				else
				{
					bannerHtml = @"<table cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td><img src=""[WEB-ROOT]gfx/1pix.gif"" width=""23"" height=""90""></td><td><a href=""[LOGIN]""><img src=""[WEB-ROOT]gfx/logo-200-90.jpg"" border=""0"" width=""200"" height=""90""></a></td><td><img src=""[WEB-ROOT]gfx/1pix.gif"" width=""377"" height=""90""></td></tr></table>";
				}
			
				siteUserTemplate = siteUserTemplate.Replace("[BANNER]", bannerHtml);
			 
				
				
				
				#region AnotherSiteUser
				string salutation = "Hi, ";
				string logInUrl = "http://" + Vars.DomainName + "/";
				if (UsrRecipient != null)
				{
					if (UsrRecipient.NickName.Length > 0)
						salutation = "Dear " + HttpUtility.HtmlEncode(UsrRecipient.NickName) + ", ";
					logInUrl = UsrRecipient.LoginUrl;
				}
				if (RedirectUrl.Length > 0 && UsrRecipient != null)
				{
					logInUrl = UsrRecipient.LoginAndTransfer(RedirectUrl);
				}
				if (OverrideLoginLink.Length > 0)
				{
					logInUrl = OverrideLoginLink;
				}
				body = siteUserTemplate.Replace("[SUBJECT]", Subject);
				#region [QUICKLINK]
				if (ShowQuickLink)
				{
					body = body.Replace("[QUICKLINK]", @"[h1]
		Quick link
	[/h1]
	[div]
		<p align=""center"" style=""margin:8px 0px 4px 0px;""><a href=""[LOGIN]"" style=""font-size:18px;font-weight:bold;"">Click here for DontStayIn</a></p>
	[/div]");
				}
				else
					body = body.Replace("[QUICKLINK]", "");
				#endregion
				body = body.Replace("[BODY]", Body);
				body = body.Replace("[SALUTATION]", salutation);
				#endregion

				body = AolCompliance(body);
				body = body.Replace("[LOGIN]", logInUrl);
				body = GenericReplacements(body);
				return body;
			}
			else if (TemplateType.Equals(TemplateTypes.AdminNote))
			{
				Assembly ass = Assembly.GetExecutingAssembly();
				StreamReader sr = new StreamReader(ass.GetManifestResourceStream("Bobs.Emails.DsiEmailTemplateAdminNote.htm"));
				string adminNoteTemplate = sr.ReadToEnd();

				#region AdminNote
				string logInUrl = "http://" + Vars.DomainName + "/";
				if (Usr.Current != null)
				{
					logInUrl = Usr.Current.LoginAndTransfer(RedirectUrl);
				}
				if (OverrideLoginLink.Length > 0)
					logInUrl = OverrideLoginLink;

				Usr usrDave = new Usr(4);
				Usr usrJohn = new Usr(1);
				Usr usrTim = new Usr(2);

				string body = adminNoteTemplate.Replace("[SUBJECT]", Subject);

				body = body.Replace("[BODY]", Body);
				body = body.Replace("[LOGIN-URL-CURRENT]", logInUrl);
				body = body.Replace("[LOGIN-URL-DAVE]", usrDave.LoginAndTransfer(RedirectUrl));
				body = body.Replace("[LOGIN-URL-JOHN]", usrJohn.LoginAndTransfer(RedirectUrl));
				body = body.Replace("[LOGIN-URL-TIM]", usrTim.LoginAndTransfer(RedirectUrl));

				body = GenericReplacements(body);

				return body;
				#endregion
			}
			else
				return "error";
		}
		#endregion

		public string AolCompliance(string body)
		{
			string aol = "";
			//aol += @"<div style=""width:500px; border:solid 1px #000000; padding:8px 10px 10px 10px; background-color:FECA26; line-height:130%; font-family: Verdana, sans-serif; font-size:11px; "">";
			//aol += @"<div style=""padding:4px 0px 5px 0px;"">";
			aol += @"<div style=""padding:35px 0px 0px 0px;"">[div]<p align=""center"">";

			if (UsrRecipient.IsSkeleton)
				aol += "This is a one-time mailing - we won't send you regular emails if you don't visit the site again. ";

			aol += "You can stop DontStayIn sending you any further emails by <a href=\"[LOGIN(/popup/unsubscribe)]\">unsubscribing</a>. ";

			if (UsrRecipient.IsSkeleton)
			{
				if (UsrRecipient.AddedByGroupK > 0)
					aol += "Your email address was added to DontStayIn when someone invited you to a group. ";
				else if (UsrRecipient.AddedByUsrK > 0)
					aol += "Your email address was added to DontStayIn when someone invited you to a topic or sent you a photo. ";
				else
					aol += "Your email address was added to DontStayIn on our new user page. ";
			}

			aol += "You can read <a href=\"[LOGIN(/pages/legalinformationpolicy)]\">our privacy policy here</a>. ";

			//aol += @"</div>";
			aol += @"</p>[/div]";

			return body.Replace("[AOL]", aol);
		}

		public string GenericReplacements(string body)
		{
			//	AddStyle(ref body,"p","font-family: Verdana, sans-serif;font-size:10px;line-height:130%;");
			//	AddStyle(ref body,"a","font-family: Verdana, sans-serif;font-size:10px;");

			body = body.Replace("[h1]", @"<div style=""padding:35px 0px 0px 0px;""><span style=""font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-size: 18px; font-weight:normal; color:#ffffff; border-left:5px solid #feca26; background-color:#444444; padding:2px 6px 2px 6px;"">");
			body = body.Replace("[/h1]", "</span>");
			body = body.Replace("[div]", @"<div style=""padding: 8px 15px 8px 15px; background-color:#ffffff;"">");
			body = body.Replace("[/div]", "</div></div>");

			body = body.Replace("<p>", "<div style=\"padding:7px 0px 7px 0px; font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 11px; line-height:1.5;\">");
			body = body.Replace("<p ", "<div style=\"padding:7px 0px 7px 0px; font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 11px; line-height:1.5;\" ");
			body = body.Replace("</p>", "</div>");

			body = body.Replace("<ul>", "<ul style=\"padding:7px 0px 7px 0px; font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 11px; line-height:1.5;\">");
			body = body.Replace("<ul ", "<ul style=\"padding:7px 0px 7px 0px; font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 11px; line-height:1.5;\" ");

			body = body.Replace("<h1>", "<div style=\"color: #444444; padding:2px 4px 2px 0px; margin:10px 0px 10px 0px; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-size: 16px; font-weight:normal;\">");
			body = body.Replace("<h1 ", "<div style=\"color: #444444; padding:2px 4px 2px 0px; margin:10px 0px 10px 0px; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-size: 16px; font-weight:normal;\" ");
			body = body.Replace("</h1>", "</div>");

			body = body.Replace("<h2>", "<div style=\"color: #444444; padding:2px 4px 2px 0px; margin:10px 0px 10px 0px; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-size: 16px; font-weight:normal; \">");
			body = body.Replace("<h2 ", "<div style=\"color: #444444; padding:2px 4px 2px 0px; margin:10px 0px 10px 0px; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-size: 16px; font-weight:normal; \" ");
			body = body.Replace("</h2>", "</div>");

			body = body.Replace("[WEB-ROOT]", "http://" + Vars.DomainName + "/");

			Regex loginRegex = new Regex(@"\[LOGIN\(([^\)]*)\)\]");
			MatchEvaluator m = new MatchEvaluator(LoginReplacement);
			body = loginRegex.Replace(body, m);
			return body;
		}

		public void AddStyle(ref string body, string tag, string styles)
		{
			Regex r = new Regex(@"\<" + tag + "([^>]*)>");
			AddStyleEvaluatorClass c = new AddStyleEvaluatorClass(tag, styles);
			MatchEvaluator m = new MatchEvaluator(c.AddStyleEvaluator);
			body = r.Replace(body, m);
		}
		public class AddStyleEvaluatorClass
		{
			public AddStyleEvaluatorClass(string tagName, string extraStyles)
			{
				this.tagName = tagName;
				this.extraStyles = extraStyles;
			}
			string tagName, extraStyles;
			public string AddStyleEvaluator(Match m)
			{
				string inTag = m.Groups[1].ToString();
				if (inTag.IndexOf("style=\"") > -1)
					inTag = inTag.Replace("style=\"", "style=\"" + extraStyles);
				else
					inTag = " style=\"" + extraStyles + "\"" + inTag;
				return "<" + tagName + inTag + ">";
			}
		}
		public string LoginReplacement(Match m)
		{
			if (TemplateType.Equals(TemplateTypes.AnotherSiteUser) || TemplateType.Equals(TemplateTypes.SpecialMail))
			{
				if (UsrRecipient == null)
					return "http://" + Vars.DomainName + m.Groups[1].ToString();
				else
					return UsrRecipient.LoginAndTransfer(m.Groups[1].ToString());
			}
			else
				return "http://" + Vars.DomainName + m.Groups[1].ToString();
		}

		#region Attachments
		public List<System.Net.Mail.Attachment> Attachments
		{
			get
			{
				if (attachments == null)
					attachments = new List<System.Net.Mail.Attachment>();
				return attachments;
			}
			set
			{
				attachments = value;
			}
		}
		private List<System.Net.Mail.Attachment> attachments;
		#endregion

		#region Bulk
		/// <summary>
		/// Default = false;
		/// </summary>
		public bool Bulk
		{
			get
			{
				return bulk;
			}
			set
			{
				bulk = value;
			}
		}
		private bool bulk;
		#endregion
		#region Inbox
		/// <summary>
		/// Default = false;
		/// </summary>
		public bool Inbox
		{
			get
			{
				return inbox;
			}
			set
			{
				inbox = value;
			}
		}
		private bool inbox;
		#endregion
		#region SendEvenIfUnverifiedOrBroken
		/// <summary>
		/// Default = false;
		/// </summary>
		public bool SendEvenIfUnverifiedOrBroken
		{
			get
			{
				return sendEvenIfUnverifiedOrBroken;
			}
			set
			{
				sendEvenIfUnverifiedOrBroken = value;
			}
		}
		private bool sendEvenIfUnverifiedOrBroken;
		#endregion

		#region Async
		/// <summary>
		/// Default = true;
		/// </summary>
		public bool Async
		{
			get
			{
				return async;
			}
			set
			{
				async = value;
			}
		}
		private bool async = true;
		#endregion

		#region Send()
		public void Send()
		{
			try
			{

				//unsubscribed and banned users
				if (UsrRecipient != null && (UsrRecipient.EmailHold || UsrRecipient.Banned))
					return;

				//email verified, broken with override (for sending email verify emails)
				//we turn this off for the first 24 hours after sign-up
				if (UsrRecipient != null && (!UsrRecipient.IsEmailVerified || UsrRecipient.IsEmailBroken) && !this.SendEvenIfUnverifiedOrBroken && UsrRecipient.DateTimeSignUp < DateTime.Now.AddDays(-1))
					return;

				//send bulk emails to skeleton users ONLY in the first day after sign-up
				if (UsrRecipient != null && this.Bulk && UsrRecipient.IsSkeleton && UsrRecipient.DateTimeSignUp < DateTime.Now.AddDays(-1))
					return;

				//users that have turned off inbox emails
				if (UsrRecipient != null && this.Inbox && UsrRecipient.NoInboxEmails)
					return;

				System.Net.Mail.SmtpClient c = new System.Net.Mail.SmtpClient();
				c.Host = Common.Properties.GetDefaultSmtpServer();

				System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();


				if (Attachments.Count > 0)
				{
					foreach (System.Net.Mail.Attachment a in Attachments)
						m.Attachments.Add(a);
				}
				string fullBody = GetFullBody();
				m.Body = fullBody.Replace("\n", "\n\r");

				if (OverrideFromAddress.Length > 0)
					m.From = new System.Net.Mail.MailAddress(OverrideFromAddress);
				else
					m.From = new System.Net.Mail.MailAddress(Vars.AdminReplyAddress);

				m.IsBodyHtml = true;

				if (Vars.DevEnv)
				{
					m.Subject = Subject + " (to:" + To + ")";
					m.Subject += " (" + DateTime.Now.ToString() + ")";
					m.To.Add("davidbrophy1@hotmail.com");
					m.To.Add("photos@mgshots.com");
					m.To.Add("dev.mail@dontstayin.com");
					m.From = new System.Net.Mail.MailAddress("mail@dontstayin.com");
				}
				else if (Vars.IsBeta)
				{
					m.Subject = "BETA " + Subject;
					m.To.Add(To);
				}
				else
				{
					m.Subject = Subject;
					if (To.EndsWith("@gmail.com") || To.EndsWith("@dontstayin.com"))
						m.Subject += " (" + DateTime.Now.ToString() + ")";
					m.To.Add(To);
					
				}

				c.Send(m);

				if (UsrRecipient != null)
					UsrRecipient.RegisterMailSentForBounceTracking();

				if (!this.TemplateType.Equals(TemplateTypes.AdminNote))
				{
					Log.Increment(Log.Items.EmailsSent);
				}
			}
			catch (Exception ex)
			{
				// Handle and log exception
				//Utilities.AdminEmailAlert("Exception occurred in Mailer.Send()", "Exception occurred in Mailer.Send()", ex);
			}
		}
		#endregion
	}
	#endregion

}
