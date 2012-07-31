using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Chilkat;
using Bobs;
using System.Text.RegularExpressions;

namespace BounceMailDeleter
{
	class Program
	{
		static void Main(string[] args)
		{
			#region strings
			string[] strings = new string[] {
				"mailbox unavailable",
				"unable to validate recipient",
				"inactive user",
				"invalid address",
				"invalid recipient",
				"no such user",
				"address rejected",
				"user is unknown at this domain",
				"user not known at",
				"unrouteable address",
				"unknown user",
				"the email account that you tried to reach does not exist",
				"the recipient's e-mail address was not found",
				"no mail box available for this user",
				"user unknown",
				"the email address you used is incorrect",
				"recipient rejected",
				"recipient unknown",
				"invalid mailbox",
				"domain name not found",
				"host not found",
				"mailbox does not exist",
				"user not known",
				"no such person at this address",
				"unknown or illegal alias",
				"this user doesn't have a",
				"this account has been disabled or discontinued",
				"no such mailbox",
				"user account is unavailable",
				"bad destination",
				"user is unknown",
				"address was not found",
				"bad_recipient",
				"is not a known user",
				"is not known here",
				"not listed in domino directory",
				"no mailbox here by that name",
				"this email address does not exist",
				"no such recipient",
				"cannot verify recipient",
				"does not exist",
				"unknown email address",
				"invalid recipient",
				"unknown recipient",
				"unknown local part in recipient",
				"address not recognised",
				"not listed in public name & address book",
				"recipient not recognized",
				"user invalid",
				"no such address",
				"address is unknown",
				"invalid email address",
				"mailbox not available",
				"this server doesn't handle mail for that user",
				"user not found",
				"user disabled",
				"mailbox disabled",
				"account is disabled",
				"account has been disabled",
				"this address no longer accepts mail",
				"resolver.adr.recipnotfound",
				"this email address is not known",
				"the email account that you tried to reach is disabled",
				"Recipient doesn't want your mail",
				"Mailbox is inactive",
				"Relaying denied",
				"Proper authentication required",
				"Administrative prohibition",
				"accepting mail from specific email addresses",
				"Unknown local-part",
				"sorry, that domain isn't in my list"
			};
			#endregion

			if (args.Length == 0 || args[0] == "1" || args[0] == "2" || args[0] == "3")
			{

				if (args.Length == 0 || args[0] == "1")
				{
					try
					{
						//if (!Vars.DevEnv)
						ScanGmail(strings, Types.DontStayIn);
					}
					catch { }
				}

				if (args.Length == 0 || args[0] == "2")
				{
					try
					{
						if (!Vars.DevEnv)
							ScanGmail(strings, Types.Mixmag);
					}
					catch { }
				}

				if (args.Length == 0 || args[0] == "3")
				{
					try
					{
						if (!Vars.DevEnv)
							ScanGmail(strings, Types.SpamTrap);
					}
					catch { }
				}
				//if (Vars.DevEnv)
					//ScanGmail(strings, Types.SpamTrap);
			}
			else
			{
				ScanDir(strings, args);
			}
		}
		public enum Types { Mixmag, DontStayIn, SpamTrap }
		public static void ScanGmail(string[] strings, Types type)
		{
			Console.WriteLine("VER 3");
			Bounce b = new Bounce();
			b.UnlockComponent("DONTSTAYINBOUNCE_OS2MBg0e7GpB");

			MailMan mailman = new MailMan();
			mailman.UnlockComponent("DONTSTAYINMAILQ_5CHEZpZc7Gp9");
			//mailman.ConnectTimeout = 5;
			mailman.MailPort = 995;
			mailman.PopSsl = true;
			mailman.MailHost = "pop.gmail.com";
			if (type == Types.DontStayIn)
			{
				Console.WriteLine("default@dontstayin.com");
				mailman.PopUsername = "default@dontstayin.com";
				mailman.PopPassword = "foo";
			}
			else if (type == Types.Mixmag)
			{
				Console.WriteLine("no-reply@dontstayin.com");
				mailman.PopUsername = "no-reply@dontstayin.com";
				mailman.PopPassword = "foo";
			}
			else if (type == Types.SpamTrap)
			{
				Console.WriteLine("spamtrap@dontstayin.com");
				mailman.PopUsername = "spamtrap@dontstayin.com";
				mailman.PopPassword = "foo";
			}
			// Copy the mail into a bundle object.  The mail still remains
			// on the POP3 server.  Call TransferMail to copy and remove
			// mail from the POP3 server.
			Chilkat.EmailBundle bundle;

			Console.WriteLine("Getting bundle...");

			List<string> emailAddresses = new List<string>();
			int ignored = 0;
			int ignoredHard = 0;
			int hardBounces = 0;
			int exceptions = 0;
			int total = 0;

			// Loop over each email and save attachments.
			// Also add the email subject to a list box.

			do
			{

				emailAddresses = new List<string>();
				ignored = 0;
				ignoredHard = 0;
				hardBounces = 0;
				exceptions = 0;
				total = 0;

				bundle = mailman.CopyMail();
				Console.WriteLine("Got {0} messages", bundle.MessageCount.ToString());

				total = bundle.MessageCount;
				int i;
				for (i = 0; i < bundle.MessageCount; i++)
				{
					Chilkat.Email email = bundle.GetEmail(i);

					try
					{
						Console.Write(".");
						if (type == Types.SpamTrap)
						{
							#region spam trap
							if (email.Subject == "complaint about message from 84.45.14.32")
							{
								bool isEflyer = false;
								bool isUpdateEmail = false;
								string fullBody = email.Body;
								string recipient = "";
								string subject = "";

								//Console.WriteLine(fullBody.Length > 4096 ? fullBody.Substring(0, 4096) : fullBody);
								//Console.ReadLine();

								if (fullBody.IndexOf("From: mail@dontstayin.com\r", StringComparison.OrdinalIgnoreCase) == -1)
								{
									isEflyer = true;
								}

								if (fullBody.IndexOf("Subject: DontStayIn this week", StringComparison.OrdinalIgnoreCase) > -1 ||
									fullBody.IndexOf("Subject: Don't Stay In this week", StringComparison.OrdinalIgnoreCase) > -1)
								{
									isUpdateEmail = true;
								}

								{
									string firstLine = fullBody.Substring(0, fullBody.IndexOf("\r"));
									recipient = firstLine.Substring(firstLine.IndexOf(" ") + 1);
								}

								{
									string subjectAndOn = fullBody.Substring(fullBody.IndexOf("\r\nSubject: ", StringComparison.OrdinalIgnoreCase) + 11);
									subject = subjectAndOn.Substring(0, subjectAndOn.IndexOf("\r"));
								}

								Console.WriteLine("Email: {0}, Eflyer: {1}, UpdateEmail: {2}, Subject: {3}", recipient, isEflyer, isUpdateEmail, subject.Length > 30 ? subject.Substring(0,30) : subject);

								UsrSet us = new UsrSet(new Query(new Q(Usr.Columns.Email, recipient)));
								if (us.Count > 0)
								{
									foreach (Usr u in us)
									{
										if (isEflyer)
										{
											u.SendFlyers = false;
										}
										else if (isUpdateEmail)
										{
											u.SendSpottedEmails = false;
										}
										else
										{
											u.EmailHold = true;
										}
										u.Update();
									}
								}

							}
							else
								Console.WriteLine("(not complaint email)");
							#endregion
						}
						else
						{
							b.ExamineEmail(email);
							dealWithBounce(strings, b, "", ref hardBounces, ref ignoredHard, ref ignored);
						}
						
					}
					catch (Exception ex)
					{
						Console.WriteLine("EXCEPTION");
						//Console.WriteLine(ex.Message);
						//throw ex;
						exceptions++;
					}
					//Console.WriteLine("");
					
					//if (i < 10 || i % 10 == 0)
					//	Console.WriteLine("Processed: " + i.ToString("#,##0") + " / " + total.ToString("#,##0") + ", bounces: " + hardBounces.ToString("#,##0") + ", ignored: " + ignored.ToString("#,##0") + ", ignoredHard: " + ignoredHard.ToString("#,##0") + ", errors: " + exceptions.ToString("#,##0"));


				}
			}
			while (total >= 250);
			
		}

		static void dealWithBounce(string[] strings, Bounce b, string fileName, ref int hardBounces, ref int ignoredHard, ref int ignored)
		{
			if (isUnsubscribe(b.BounceType))
			{
				Console.Write(b.BounceAddress + " ");
				SetEmailBroken(b.BounceAddress);
			}
			else if (isPossiblyHardBounce(b.BounceType))
			{
				Console.Write(b.BounceAddress + " ");

				if (isBroken(strings, b.BounceData))
				{
					Console.WriteLine("Matched HARD BOUNCE");
					incrementAndDealWith(b.BounceAddress, 1);
					hardBounces++;
				}
				else
				{
					Console.WriteLine("Unmatched HARD BOUNCE");
					incrementAndDealWith(b.BounceAddress, 2);
					ignoredHard++;
				}

				if (fileName.Length > 0)
					File.Delete(fileName);
			}
			else if (isSoftBounce(b.BounceType))
			{
				Console.WriteLine("SOFT BOUNCE");
				incrementAndDealWith(b.BounceAddress, 3);
			}
			else
			{
				//Console.WriteLine("NOT HARD BOUNCE");
				//Log1("nothardbounce", b.BounceAddress, b.BounceType, b.BounceData);
				ignored++;
			}
		}


		static void incrementAndDealWith(string email, int type)
		{
			Usr u = null;
			try
			{
				u = new UsrSet(new Query(new Q(Usr.Columns.Email, email)))[0];
			}
			catch { }

			if (u == null)
				return;

			DateTime thisPeriod = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
			DateTime? periodFromUsr = u.BouncePeriodDateTime;

			if (!periodFromUsr.HasValue || thisPeriod != periodFromUsr.Value)
			{
				u.BouncePeriodDateTime = thisPeriod;
				u.TotalEmailsSentInPeriod = 0;
				u.MatchedHardBounceInPeriod = 0;
				u.UnmatchedHardBounceInPeriod = 0;
				u.SoftBounceInPeriod = 0;
			}

			if (type == 1) // hard bounce with match
				u.MatchedHardBounceInPeriod++;
			else if (type == 2) // hard bounce without match
				u.UnmatchedHardBounceInPeriod++;
			else if (type == 3) //soft bounce
				u.SoftBounceInPeriod++;

			u.Update();

			int total = !u.TotalEmailsSentInPeriod.HasValue || u.TotalEmailsSentInPeriod.Value == 0 ? 1 : u.TotalEmailsSentInPeriod.Value;
			int matchedHardBounce = u.MatchedHardBounceInPeriod.HasValue ? u.MatchedHardBounceInPeriod.Value : 0;
			int unmatchedHardBounce = u.UnmatchedHardBounceInPeriod.HasValue ? u.UnmatchedHardBounceInPeriod.Value : 0;
			int softBounce = u.SoftBounceInPeriod.HasValue ? u.SoftBounceInPeriod.Value : 0;

			if (matchedHardBounce > 2 && (matchedHardBounce + 0.0) / (total + 0.0) > 0.02)
				SetEmailBroken(email);

			if (unmatchedHardBounce > 5 && (unmatchedHardBounce + 0.0) / (total + 0.0) > 0.05)
				SetEmailBroken(email);

			if (softBounce > 10 && (softBounce + 0.0) / (total + 0.0) > 0.10)
				SetEmailBroken(email);


		}


		static bool isBroken(string[] brokenStrings, string text)
		{
			text = text.ToLower();
			foreach (string s in brokenStrings)
			{
				if (text.Contains(s))
				{
					return true;
				}
			}

			if (text.Contains("status: 5.5.0") && text.Contains("is not valid here"))
				return true;

			if (text.Contains("status: 5.4.0") && text.Contains("reporting-mta: dns;mail.dontstayin.com"))
				return true;

	//		if (text.Contains("status: 4.4.7") && text.Contains("reporting-mta: dns;mail.dontstayin.com"))
	//			return true;

			return false;
		}



		private static void SetEmailBroken(string emailAddress)
		{

			if (emailAddress.Length == 0)
				return;
			

			{
				int changes = 0;
				Update u = new Update();
				u.Table = TablesEnum.Usr;
				u.Changes.Add(new Assign(Usr.Columns.IsEmailBroken, true));
				u.Where = new And(new Q(Usr.Columns.Email, emailAddress), new Or(new Q(Usr.Columns.IsEmailBroken, false), new Q(Usr.Columns.IsEmailBroken, QueryOperator.IsNull, null)));
				changes = u.Run();
				Log.Increment(Model.Entities.Log.Items.EmailBouncesDisabled, changes);
			}

			{
				int changes = 0;
				Update u = new Update();
				u.Table = TablesEnum.MixmagSubscription;
				u.Changes.Add(new Assign(MixmagSubscription.Columns.IsEmailBroken, true));
				u.Changes.Add(new Assign(MixmagSubscription.Columns.EmailBrokenDateTime, DateTime.Now));
				u.Where = new And(new Q(MixmagSubscription.Columns.Email, emailAddress), new Or(new Q(MixmagSubscription.Columns.IsEmailBroken, false), new Q(MixmagSubscription.Columns.IsEmailBroken, QueryOperator.IsNull, null)));
				changes = u.Run();
				Log.Increment(Model.Entities.Log.Items.MixmagBouncesDisabled, changes);
			}
			

		}

		public static void Log1(string file, string emailAddress, int bounceType, string bounceMessage)
		{
			try
			{
				using (StreamWriter sw = File.AppendText("c:\\" + file + "-a-" + DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString("00") + "-" + DateTime.Today.Day.ToString("00") + ".txt"))
				{
					sw.WriteLine("");
					sw.WriteLine("================================================");
					sw.WriteLine(emailAddress + " (bounce type = " + bounceType.ToString() + ")");
					sw.WriteLine("------------------------------------------------");
					if (bounceMessage.Length > 1024)
						sw.WriteLine(bounceMessage.Substring(0, 1024));
					else
						sw.WriteLine(bounceMessage);
					sw.WriteLine("================================================");
					sw.WriteLine("");

					sw.Flush();
					sw.Close();
					sw.Dispose();
				}
			}
			catch { }
		}



		public static void ScanDir(string[] strings, string[] args)
		{
			if (args.Length == 0)
			{
				//args = new string[] { @"C:\EMLs" };
				throw new Exception("first argumant should be directory to scan...");
			}
			Console.WriteLine("Getting files...");
			string[] fileEntries = Directory.GetFiles(args[0]);
			Console.WriteLine("Got " + fileEntries.Length.ToString("#,##0") + " files...");
			int left = 0;
			int exceptions = 0;
			int i = 0;
			int ignored = 0;
			int ignoredHard = 0;
			int hardBounces = 0;

			Bounce b = new Bounce();
			b.UnlockComponent("DONTSTAYINBOUNCE_OS2MBg0e7GpB");

			foreach (string fileName in fileEntries)
			{
				try
				{
					b.ExamineEml(fileName);

					dealWithBounce(strings, b, fileName, ref hardBounces, ref ignoredHard, ref ignored);
				}
				catch (Exception ex)
				{
					exceptions++;
				}
				
				i++;
				if (i % 100 == 0)
					Console.WriteLine("Processed: " + i.ToString("#,##0") + " / " + fileEntries.Length.ToString("#,##0") + ", hardBounces: " + hardBounces.ToString("#,##0") + ", ignoredHard: " + ignoredHard.ToString("#,##0") + ", ignored: " + ignored.ToString("#,##0") + ", errors: " + exceptions.ToString("#,##0"));

			}
		}

		



		#region Bounce Type
		/**
		 * http://www.chilkatsoft.com/refDoc/csBounceRef.html
		 * 
		 * 1. Hard Bounce. The email could not be delivered and BounceAddress contains the failed email address. 
		 * 2. Soft Bounce. A temporary condition exists causing the email delivery to fail. The BounceAddress property contains the failed email address. 
		 * 3. General Bounced Mail, cannot determine if it is hard or soft, and the email address is not available. 
		 * 4. General Bounced Mail, cannot determine if it is hard or soft, but an email address is available. 
		 * 5. Mail Block. A bounce occured because the sender was blocked. 
		 * 6. Auto-Reply/Out-of-Office email. 
		 * 7. Transient message, such as "Delivery Status / No Action Required". 
		 * 8. Subscribe request. 
		 * 9. Unsubscribe request. 
		 * 10. Virus email notification. 
		 * 11. Suspected Bounce, but no other information is available 
		 * 12. Challenge/Response - Auto-reply message sent by SPAM software where only verified email addresses are accepted. 
		 * 13. Address Change Notification Messages. 
		 */
		private static List<int> hardBounceValues = new List<int>() { 1 };
		private static List<int> softBounceValues = new List<int>() { 2, 3, 4, 5, 11 };
		private static List<int> unsubscribeValues = new List<int>() { 9 };
		private static bool isPossiblyHardBounce(int bounceType)
		{
			return hardBounceValues.Contains(bounceType);
		}
		private static bool isSoftBounce(int bounceType)
		{
			return softBounceValues.Contains(bounceType);
		}
		private static bool isUnsubscribe(int bounceType)
		{
			return unsubscribeValues.Contains(bounceType);
		}
		#endregion
	}
}
