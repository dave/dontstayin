using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Facebook.Web.UI;
using Facebook.Web;
using Facebook.Api;
using Bobs;
using System.Text;
using System.Text.RegularExpressions;

namespace MixmagSubscription
{
	public partial class _Default : FacebookPage
	{
		public DateTime? IssueDate = null;
		public int? PageNumber = null;
		public int? CoverId = null;

		protected void Page_Load(object sender, EventArgs e)
		{

			Query qAll = new Query();
			qAll.Columns = new ColumnSet(Country.Columns.FriendlyName, Country.Columns.K);
			qAll.OrderBy = new OrderBy(Country.Columns.FriendlyName);
			qAll.QueryCondition = new Q(Country.Columns.Enabled, true);
			CountrySet csAll = new CountrySet(qAll);

			CountryDropDownList.DataSource = csAll;
			CountryDropDownList.DataTextField = "FriendlyName";
			CountryDropDownList.DataValueField = "K";
			CountryDropDownList.DataBind();

			CountryDropDownList.SelectedValue = IpCountry.ClientCountryK().ToString();


			if (Request.QueryString["k"] != null && Request.QueryString["code"] != null)
			{
				Bobs.MixmagSubscription ms = new Bobs.MixmagSubscription(int.Parse(Request.QueryString["k"]));
				if (ms.EmailVerificationSecret.ToLower() == Request.QueryString["code"].ToLower())
				{
					ms.IsEmailVerified = true;
					ms.Update();
				}
			}

			if (Request.QueryString["k"] != null && Request.QueryString["email"] != null)
			{
				Bobs.MixmagSubscription ms = new Bobs.MixmagSubscription(int.Parse(Request.QueryString["k"]));
				if (ms.Email.ToLower() == Request.QueryString["email"].ToLower())
				{
					Query q = new Query();
					q.QueryCondition = new Q(Bobs.MixmagSubscription.Columns.Email, ms.Email);
					MixmagSubscriptionSet mss = new MixmagSubscriptionSet(q);
					foreach (Bobs.MixmagSubscription ms1 in mss)
					{
						ms1.SendMixmag = false;
						ms1.Update();
					}
				}
			}

			if (Request.QueryString["k"] != null && Request.QueryString["uid"] != null)
			{
				Bobs.MixmagSubscription ms = new Bobs.MixmagSubscription(int.Parse(Request.QueryString["k"]));
				if (ms.FacebookUID == int.Parse(Request.QueryString["uid"]))
				{
					ms.SendMixmag = false;
					ms.Update();
				}
			}

			if (IssueDate.HasValue)
			{
				string code = IssueDate.Value.Year.ToString("0000");
				code += IssueDate.Value.Month.ToString("00");
				code += CoverId.HasValue ? CoverId.Value.ToString("0") : "0";
				if (PageNumber.HasValue && PageNumber.Value > 0)
				{
					code += PageNumber.Value.ToString();
				}
				RequestCode.Value = code;
			}


			int selectedIssue = 0;
			DateTime? selectedIssueDate = null;
			if (IssueDate.HasValue)
			{
				DateTime month = IssueDate.Value;
				Query q = new Query();
				if (CoverId.HasValue)
				{
					q.QueryCondition = new And(
						new Q(MixmagIssue.Columns.IssueCoverDate, month),
						new Q(MixmagIssue.Columns.IssueCoverId, CoverId.Value)
					);
				}
				else
				{
					q.QueryCondition = new Q(MixmagIssue.Columns.IssueCoverDate, month);
				}
				q.OrderBy = new OrderBy(MixmagIssue.Columns.IssueCoverId);
				MixmagIssueSet mis = new MixmagIssueSet(q);
				if (mis.Count > 0)
				{
					MixmagIssue issue = mis[0];

					LoginButtonIntroTextP.InnerText = "You must connect with Facebook to read Mixmag:";

					FacebookHttpContext context = null;
					try
					{
						context = FacebookHttpContext.Current;
					}
					catch { }

					if (context != null)
					{
						var usrR = FacebookHttpContext.Current.Users.GetLoggedInUser();
						if (usrR != null && usrR.Value > 0)
						{
							Query q2 = new Query();
							q2.QueryCondition = new Q(Bobs.MixmagSubscription.Columns.FacebookUID, usrR.Value);
							MixmagSubscriptionSet mss = new MixmagSubscriptionSet(q2);
							if (mss.Count > 0)
							{
								Bobs.MixmagSubscription subscriber = mss[0];

								MixmagRead mr = null;
								try
								{
									mr = new MixmagRead(subscriber.K, issue.K);
								}
								catch (BobNotFound)
								{
									mr = new MixmagRead();
									mr.MixmagSubscriberK = subscriber.K;
									mr.MixmagIssueK = issue.K;
									mr.DateTimeRead = DateTime.Now;
								}
								if (subscriber.PublishStoryOnRead.HasValue && subscriber.PublishStoryOnRead.Value)
								{
									//Have we sent a message to this subscribers facebook wall on the last 12 hours? If so, don't send this one.
									Query q3 = new Query();
									q3.QueryCondition = new And(
										new Q(MixmagRead.Columns.MixmagSubscriberK, subscriber.K),
										new Q(MixmagRead.Columns.DateTimeLastStoryPublished, QueryOperator.GreaterThan, DateTime.Now.AddHours(-48)),
										new Q(MixmagRead.Columns.StoryPublished, true)
									);
									MixmagReadSet mrs = new MixmagReadSet(q3);
									if (mrs.Count == 0)
									{
										//send to wall...
										bool found = false;
										if (PageNumber.HasValue)
										{
											foreach (MixmagIssue.ContentsItem item in issue.Contents)
											{
												if (item.PageNumber == PageNumber.Value)
												{
													FacebookHttpContext.Current.Status.Set(item.StatusMessage + " - " + issue.Url(true, PageNumber));
													found = true;
												}
											}
										}
										if (!found)
											FacebookHttpContext.Current.Status.Set(issue.Contents[0].StatusMessage + " - " + issue.Url(true, null));
									

										mr.StoryPublished = true;
										mr.DateTimeLastStoryPublished = DateTime.Now;
										
									}
								}
								
								if (mr.TotalReads.HasValue)
									mr.TotalReads++;
								else
									mr.TotalReads = 1;

								mr.DateTimeLastRead = DateTime.Now;
								mr.Update();

								if (issue.TotalRead.HasValue)
									issue.TotalRead++;
								else
									issue.TotalRead = 1;

								issue.Update();
								//Response.Redirect(issue.CerosUrl);
								Response.Write(@"<FRAMESET cols=""100%""><FRAMESET rows=""100%""><FRAME src=""" + issue.CerosUrl + (PageNumber.HasValue ? ("/page/" + PageNumber.ToString()) : "") + @"""></FRAMESET></FRAMESET>");
								Response.End();

							}
						}
					}

					SelectedIssueHolder.Visible = true;
					SelectedIssuePh.Controls.Clear();
					SelectedIssuePh.Controls.Add(new LiteralControl(issue.GetHtml(PageNumber, false)));
					selectedIssue = issue.K;
				}
			}

			if (selectedIssue == 0)
			{
				//Get latest issue
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(MixmagIssue.Columns.DateTimeSend, QueryOperator.LessThan, DateTime.Now),
					new Q(MixmagIssue.Columns.Ready, true));
				q1.OrderBy = new OrderBy(MixmagIssue.Columns.IssueCoverDate, OrderBy.OrderDirection.Descending);
				q1.TopRecords = 1;
				MixmagIssueSet latestDateTimeSet = new MixmagIssueSet(q1);

				if (latestDateTimeSet.Count > 0)
				{
					selectedIssueDate = latestDateTimeSet[0].IssueCoverDate;

					Query q2 = new Query();
					q2.QueryCondition = new And(
						new Q(MixmagIssue.Columns.DateTimeSend, QueryOperator.LessThan, DateTime.Now),
						new Q(MixmagIssue.Columns.Ready, true),
						new Q(MixmagIssue.Columns.IssueCoverDate, latestDateTimeSet[0].IssueCoverDate.Value));
					q2.OrderBy = new OrderBy(MixmagIssue.Columns.IssueCoverId);

					MixmagIssueSet latestIssueSet = new MixmagIssueSet(q2);
					if (latestIssueSet.Count > 0)
					{
						SelectedIssueHeader.InnerHtml = "Current issue" + (latestIssueSet.Count > 1 ? "s" : "");
						SelectedIssuePh.Controls.Clear();
						foreach (MixmagIssue issue in latestIssueSet)
							SelectedIssuePh.Controls.Add(new LiteralControl(issue.GetHtml(null, false)));
						SelectedIssueHolder.Visible = true;
						
					}
				}
			}



			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Q(MixmagIssue.Columns.DateTimeSend, QueryOperator.LessThan, DateTime.Now),
					new Q(MixmagIssue.Columns.Ready, true),
					selectedIssue > 0 ? new Q(MixmagIssue.Columns.K, QueryOperator.NotEqualTo, selectedIssue) : new Q(true),
					selectedIssueDate.HasValue ? new Q(MixmagIssue.Columns.IssueCoverDate, QueryOperator.NotEqualTo, selectedIssueDate) : new Q(true));
				q.OrderBy = new OrderBy(
					new OrderBy(MixmagIssue.Columns.IssueCoverDate, OrderBy.OrderDirection.Descending),
					new OrderBy(MixmagIssue.Columns.IssueCoverId));
				q.TopRecords = 24;
				MixmagIssueSet backIssues = new MixmagIssueSet(q);
				if (backIssues.Count > 0)
				{
					StringBuilder sb = new StringBuilder();
					foreach (MixmagIssue mi in backIssues)
					{
						sb.Append(mi.GetHtml(null, false));
					}
					BackIssuesPh.Controls.Clear();
					BackIssuesPh.Controls.Add(new LiteralControl(sb.ToString()));
					BackIssuesHolder.Visible = true;
				}
			}



		}


		[WebMethod]
		public static string SaveAddress(string uid, string sessionKey, string secret, string expires, string baseDomain, string requestCode, string email, string firstName, string lastName, string addressFirstLine, string postalCode, string country)
		{
			FacebookHttpContext.Init(HttpContext.Current, uid, sessionKey, secret, expires, baseDomain);
			long usr;
			bool emailPermission;
			bool publishPermission;
			using (var batch = Batch.Start(FacebookHttpContext.Current))
			{
				var usrR = FacebookHttpContext.Current.Users.GetLoggedInUser();
				var emailPermissionR = FacebookHttpContext.Current.Users.HasAppPermission("email");
				var publishPermissionR = FacebookHttpContext.Current.Users.HasAppPermission("publish_stream");

				batch.Complete();

				usr = usrR.Value;
				emailPermission = emailPermissionR.Value;
				publishPermission = publishPermissionR.Value;
			}

			Query q = new Query();
			q.QueryCondition = new Q(Bobs.MixmagSubscription.Columns.FacebookUID, usr);
			MixmagSubscriptionSet mss = new MixmagSubscriptionSet(q);

			if (mss.Count == 0)
			{
				return "XXX";
			}
			else
			{
				Bobs.MixmagSubscription s = mss[0];

				s.FirstName = firstName;
				s.LastName = lastName;
				s.AddressFirstLine = addressFirstLine;
				s.AddressPostCode = postalCode;

				Country c = new Country(int.Parse(country));
				s.AddressCountryK = c.K;

				s.IsAddressComplete = true;

				email = email.ToLower();

				if (email.Length > 0)
				{
					if (IsEmail(email))
					{
						if (s.Email != email)
						{
							s.EmailVerificationSecret = Cambro.Misc.Utility.GenRandomChars(5).ToLower();
							s.Email = email;
							s.IsEmailBroken = false;
							s.EmailBrokenDateTime = null;
							s.IsEmailComplete = true;
							s.IsEmailVerified = false;
							s.IsEmailFromFacebook = false;
							SendVerificationEmail(s, requestCode);
						}
					}
					else
					{
						throw new Exception("Bad email address!");
					}
				}

				s.Update();

				return returnString(s, emailPermission, publishPermission);
			}
		}

		public static string returnString(Bobs.MixmagSubscription s, bool facebookEmailPermission, bool facebookPublishPermission)
		{
			string email = s.Email == null ? "" : s.Email;
			if (email.EndsWith("@proxymail.facebook.com"))
				email = "xxxx@proxymail.facebook.com";

			return (facebookEmailPermission && s.SendMixmag.Value ? "1" : "0") +
					(facebookPublishPermission && s.PublishStoryOnRead.Value ? "1" : "0") +
					(s.IsAddressComplete.HasValue && s.IsAddressComplete.Value ? "1" : "0") +
					(s.IsEmailComplete.HasValue && s.IsEmailComplete.Value ? "1" : "0") +
					(s.IsEmailVerified.HasValue && s.IsEmailVerified.Value ? "1" : "0") +
					(s.IsEmailBroken.HasValue && s.IsEmailBroken.Value ? "1" : "0") +
					(s.IsEmailFromFacebook.HasValue && s.IsEmailFromFacebook.Value ? "1" : "0") +
					email;
		}

		[WebMethod]
		public static string SaveEmail(string uid, string sessionKey, string secret, string expires, string baseDomain, string requestCode, string email)
		{
			FacebookHttpContext.Init(HttpContext.Current, uid, sessionKey, secret, expires, baseDomain);
			long usr;
			bool emailPermission;
			bool publishPermission;
			using (var batch = Batch.Start(FacebookHttpContext.Current))
			{
				var usrR = FacebookHttpContext.Current.Users.GetLoggedInUser();
				var emailPermissionR = FacebookHttpContext.Current.Users.HasAppPermission("email");
				var publishPermissionR = FacebookHttpContext.Current.Users.HasAppPermission("publish_stream");

				batch.Complete();

				usr = usrR.Value;
				emailPermission = emailPermissionR.Value;
				publishPermission = publishPermissionR.Value;
			}

			Query q = new Query();
			q.QueryCondition = new Q(Bobs.MixmagSubscription.Columns.FacebookUID, usr);
			MixmagSubscriptionSet mss = new MixmagSubscriptionSet(q);

			if (mss.Count == 0)
			{
				return "XXX";
			}
			else
			{
				Bobs.MixmagSubscription s = mss[0];

				email = email.ToLower();

				if (email.Length > 0 && !email.EndsWith("@proxymail.facebook.com"))
				{
					if (IsEmail(email))
					{
						if (s.Email != email)
						{
							s.EmailVerificationSecret = Cambro.Misc.Utility.GenRandomChars(5).ToLower();
							s.Email = email;
							s.IsEmailBroken = false;
							s.EmailBrokenDateTime = null;
							s.IsEmailComplete = true;
							s.IsEmailVerified = false;
							s.IsEmailFromFacebook = false;
							SendVerificationEmail(s, requestCode);
						}
					}
					else
					{
						throw new Exception("Bad email address!");
					}
				}

				s.Update();

				return returnString(s, emailPermission, publishPermission);
			}
		}

		[WebMethod]
		public static string SendLink(string uid, string sessionKey, string secret, string expires, string baseDomain, string requestCode)
		{
			FacebookHttpContext.Init(HttpContext.Current, uid, sessionKey, secret, expires, baseDomain);
			long usr;
			bool emailPermission;
			bool publishPermission;
			using (var batch = Batch.Start(FacebookHttpContext.Current))
			{
				var usrR = FacebookHttpContext.Current.Users.GetLoggedInUser();
				var emailPermissionR = FacebookHttpContext.Current.Users.HasAppPermission("email");
				var publishPermissionR = FacebookHttpContext.Current.Users.HasAppPermission("publish_stream");

				batch.Complete();

				usr = usrR.Value;
				emailPermission = emailPermissionR.Value;
				publishPermission = publishPermissionR.Value;
			}

			Query q = new Query();
			q.QueryCondition = new Q(Bobs.MixmagSubscription.Columns.FacebookUID, usr);
			MixmagSubscriptionSet mss = new MixmagSubscriptionSet(q);

			if (mss.Count == 0)
			{
				return "XXX";
			}
			else
			{
				Bobs.MixmagSubscription s = mss[0];

				if (!(s.IsEmailComplete.HasValue && s.IsEmailComplete.Value))
					throw new Exception("Email has not been entered.");

				if (s.IsEmailVerified.HasValue && s.IsEmailVerified.Value && !(s.IsEmailBroken.HasValue && s.IsEmailBroken.Value))
					throw new Exception("Email has already been verified.");

				if (s.IsEmailFromFacebook.HasValue && s.IsEmailFromFacebook.Value && !(s.IsEmailBroken.HasValue && s.IsEmailBroken.Value))
					throw new Exception("Email is from Facebook - this doesn't need verifying.");

				if (s.IsEmailBroken.HasValue && s.IsEmailBroken.Value)
				{
					s.IsEmailBroken = false;
					s.EmailBrokenDateTime = null;

					if (s.IsEmailVerified.HasValue && s.IsEmailVerified.Value)
					{
						s.EmailVerificationSecret = Cambro.Misc.Utility.GenRandomChars(5).ToLower();
						s.IsEmailVerified = false;
					}

					s.Update();
				}

				SendVerificationEmail(s, requestCode);

				return returnString(s, emailPermission, publishPermission);
			}
		}

		public static void SendVerificationEmail(Bobs.MixmagSubscription s, string requestCode)
		{
			System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
			client.Host = Common.Properties.GetDefaultSmtpServer();
			System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();

			

			DateTime? issueDate = null;
			int? coverId = null;
			int? pageNumber = null;
			string linkUrl = "";
			if (requestCode.Length > 0)
			{
				try
				{
					int year = int.Parse(requestCode.Substring(0, 4));
					int month = int.Parse(requestCode.Substring(4, 2));
					coverId = int.Parse(requestCode.Substring(6, 1));
					if (coverId == 0)
						coverId = null;

					issueDate = new DateTime(year, month, 1);
					if (requestCode.Length > 7)
						pageNumber = int.Parse(requestCode.Substring(7));

					linkUrl = MixmagIssue.UrlStatic(true, issueDate.Value, pageNumber, coverId);

				}
				catch { }
			}
			else
			{
				linkUrl = "http://www.mixmag-online.com/";
			}


			string str2 = s.K.ToString();
			string str3 = s.EmailVerificationSecret.ToLower();

			string body = @"Mixmag Online

To verify your email address, click the link below:

" + linkUrl + @"?k=" + str2 + @"&code=" + str3 + @"

";

			
			string subject = "Mixmag Online email verification";

			m.Subject = subject;
			m.Body = body;
			m.From = new System.Net.Mail.MailAddress("no-reply@mixmag-online.com", "Mixmag");
			m.ReplyTo = new System.Net.Mail.MailAddress("no-reply@mixmag-online.com", "Mixmag");
			if (Vars.DevEnv)
				m.To.Add("dev.mail@dontstayin.com");
			else
				m.To.Add(s.Email);

			m.IsBodyHtml = false;

			

			client.Send(m);
		}

		public static bool IsEmail(string Email)
		{
			string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
				@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
				@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
			Regex re = new Regex(strRegex);
			if (re.IsMatch(Email))
				return (true);
			else
				return (false);
		}

		[WebMethod]
		public static string RevokeEmailPermission(string uid, string sessionKey, string secret, string expires, string baseDomain)
		{
			FacebookHttpContext.Init(HttpContext.Current, uid, sessionKey, secret, expires, baseDomain);

			Facebook.FacebookResponse<bool> resp;
			using (var batch = Batch.Start(FacebookHttpContext.Current))
			{
				resp = FacebookHttpContext.Current.Auth.RevokeExtendedPermission(long.Parse(uid), "email");
				resp = FacebookHttpContext.Current.Auth.RevokeExtendedPermission(long.Parse(uid), "contact_email");
				batch.Complete();
			}
			return resp.Value.ToString();

			
		}

		[WebMethod]
		public static string QueryDetails(string uid, string sessionKey, string secret, string expires, string baseDomain)
		{
			//throw new Exception("uid: " + uid + ", sessionKey: " + sessionKey);
			FacebookHttpContext.Init(HttpContext.Current, uid, sessionKey, secret, expires, baseDomain);
			long usr;
			bool emailPermission;
			bool publishPermission;
			using (var batch = Batch.Start(FacebookHttpContext.Current))
			{
				var usrR = FacebookHttpContext.Current.Users.GetLoggedInUser();
				var emailPermissionR = FacebookHttpContext.Current.Users.HasAppPermission("email");
				var publishPermissionR = FacebookHttpContext.Current.Users.HasAppPermission("publish_stream");

				batch.Complete();

				usr = usrR.Value;
				emailPermission = emailPermissionR.Value;
				publishPermission = publishPermissionR.Value;
			}

			Query q = new Query();
			q.QueryCondition = new Q(Bobs.MixmagSubscription.Columns.FacebookUID, usr);
			MixmagSubscriptionSet mss = new MixmagSubscriptionSet(q);

			if (mss.Count == 0)
			{
				return "XXX";
			}
			else
			{
				return returnString(mss[0], emailPermission, publishPermission);
			}

		}

		public static void updateEmailFromFacebook(Bobs.MixmagSubscription ms, string emailFromFacebook)
		{
			ms.Email = emailFromFacebook;
			ms.IsEmailFromFacebook = true;
			ms.IsEmailBroken = false;
			ms.IsEmailComplete = true;
			ms.IsEmailVerified = true;
		}

		[WebMethod]
		public static string SaveDetailsFromPermissions(string uid, string sessionKey, string secret, string expires, string baseDomain, bool saveEmail, bool savePublish, bool revertEmailToFacebook)
		{
			return SaveDetailsGeneric(uid, sessionKey, secret, expires, baseDomain, "SaveDetailsFromPermissions", false, false, saveEmail, savePublish, revertEmailToFacebook);
		}
		[WebMethod]
		public static string SaveDetails(string uid, string sessionKey, string secret, string expires, string baseDomain, bool sendMixmag, bool publishStoryOnRead, bool revertEmailToFacebook)
		{
			return SaveDetailsGeneric(uid, sessionKey, secret, expires, baseDomain, "SaveDetails", sendMixmag, publishStoryOnRead, false, false, revertEmailToFacebook);
		}
		public static string SaveDetailsGeneric(string uid, string sessionKey, string secret, string expires, string baseDomain, string type, bool sendMixmag, bool publishStoryOnRead, bool saveEmail, bool savePublish, bool revertEmailToFacebook)
		{
			FacebookHttpContext.Init(HttpContext.Current, uid, sessionKey, secret, expires, baseDomain);

			//bool sendMixmagFinal, publishStoryOnReadFinal;

			long usr;
			bool emailPermission;
			bool publishPermission;
			string emailFromFacebook = "";
			//bool isAddressComplete = false;
			//bool isEmailComplete = false;
			//bool isEmailVerified = false;
			//bool isEmailBroken = false;
			//string email = "";
			using (var batch = Batch.Start(FacebookHttpContext.Current))
			{
				var usrR = FacebookHttpContext.Current.Users.GetLoggedInUser();
				var emailPermissionR = FacebookHttpContext.Current.Users.HasAppPermission("email");
				var publishPermissionR = FacebookHttpContext.Current.Users.HasAppPermission("publish_stream");

				batch.Complete();

				usr = usrR.Value;
				emailPermission = emailPermissionR.Value;
				publishPermission = publishPermissionR.Value;

				if (emailPermission)
				{
					//get email from Facebook query...
					emailFromFacebook = FacebookHttpContext.Current.Fql.Query<User>("SELECT email FROM user WHERE uid=" + usr.ToString()).Value.Email;
				}
			}
			Query q = new Query();
			q.QueryCondition = new Q(Bobs.MixmagSubscription.Columns.FacebookUID, usr);
			MixmagSubscriptionSet mss = new MixmagSubscriptionSet(q);
			if (mss.Count > 0)
			{
				mss[0].FacebookPermissionEmail = emailPermission;
				mss[0].FacebookPermissionPublish = publishPermission;
				if (type == "SaveDetails")
				{
					mss[0].SendMixmag = sendMixmag;
					mss[0].PublishStoryOnRead = publishStoryOnRead;

					if ((mss[0].Email == null || mss[0].Email.Length == 0 || revertEmailToFacebook) && emailFromFacebook.Length > 0)
						updateEmailFromFacebook(mss[0], emailFromFacebook);
				}
				else if (type == "SaveDetailsFromPermissions")
				{
					if (saveEmail)
					{
						mss[0].SendMixmag = emailPermission;
						if (emailPermission && (mss[0].Email == null || mss[0].Email.Length == 0 || revertEmailToFacebook) && emailFromFacebook.Length > 0)
							updateEmailFromFacebook(mss[0], emailFromFacebook);
					}

					if (savePublish)
						mss[0].PublishStoryOnRead = publishPermission;
				}

				
				//sendMixmagFinal = mss[0].FacebookPermissionEmail.Value && mss[0].SendMixmag.Value;
				//publishStoryOnReadFinal = mss[0].FacebookPermissionPublish.Value && mss[0].PublishStoryOnRead.Value;
				//isAddressComplete = mss[0].IsAddressComplete.HasValue && mss[0].IsAddressComplete.Value;
				//isEmailComplete = mss[0].IsEmailComplete.HasValue && mss[0].IsEmailComplete.Value;
				//isEmailVerified = mss[0].IsEmailVerified.HasValue && mss[0].IsEmailVerified.Value;
				//isEmailBroken = mss[0].IsEmailBroken.HasValue && mss[0].IsEmailBroken.Value;
				//email = mss[0].Email;

				mss[0].Update();

				return returnString(mss[0], mss[0].FacebookPermissionEmail.Value, mss[0].FacebookPermissionPublish.Value);
			}
			else
			{
				Bobs.MixmagSubscription ms = new Bobs.MixmagSubscription();
				
				ms.FacebookUID = usr;
				ms.FacebookPermissionEmail = emailPermission;
				ms.FacebookPermissionPublish = publishPermission;
				ms.DateTimeCreated = DateTime.Now;

				if (type == "SaveDetails")
				{
					ms.SendMixmag = sendMixmag;
					ms.PublishStoryOnRead = publishStoryOnRead;
					if (emailFromFacebook.Length > 0)
						updateEmailFromFacebook(ms, emailFromFacebook);
				}
				else if (type == "SaveDetailsFromPermissions")
				{
					if (saveEmail)
					{
						ms.SendMixmag = emailPermission;
						if (emailPermission && emailFromFacebook.Length > 0)
							updateEmailFromFacebook(ms, emailFromFacebook);
					}

					if (savePublish)
						ms.PublishStoryOnRead = publishPermission;
				}

				ms.TotalSent = 0;
				ms.TotalRead = 0;

				//sendMixmagFinal = ms.FacebookPermissionEmail.Value && ms.SendMixmag.Value;
				//publishStoryOnReadFinal = ms.FacebookPermissionPublish.Value && ms.PublishStoryOnRead.Value;
				//isAddressComplete = ms.IsAddressComplete.HasValue && ms.IsAddressComplete.Value;
				//isEmailComplete = ms.IsEmailComplete.HasValue && ms.IsEmailComplete.Value;
				//isEmailVerified = ms.IsEmailVerified.HasValue && ms.IsEmailVerified.Value;
				//isEmailBroken = ms.IsEmailBroken.HasValue && ms.IsEmailBroken.Value;
				//email = ms.Email;

				ms.Update();

				return returnString(ms, ms.FacebookPermissionEmail.Value, ms.FacebookPermissionPublish.Value);
			}

			//return (sendMixmagFinal ? "1" : "0") + 
			//    (publishStoryOnReadFinal ? "1" : "0") +
			//    (isAddressComplete ? "1" : "0") +
			//    (isEmailComplete ? "1" : "0") +
			//    (isEmailVerified ? "1" : "0") +
			//    (isEmailBroken ? "1" : "0") +
			//    email;

			//return string.Format("{3}: {0}, {1}, {2}",
			//    usr.ToString(),
			//    (sendMixmag && emailPermission).ToString(),
			//    (publishStoryOnRead && publishPermission).ToString(),
			//    type
			//);
		
			
			//return "";
			//FacebookHttpContext.Current.Notifications.SendEmail(new string[] { uid }, "Test subject", "Test text", "");
			
		}
	}
}
