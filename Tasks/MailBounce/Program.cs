using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Johnvey.GmailAgent;

namespace MailBounce
{
	class Program
	{
		static void Main(string[] args)
		{

			// init new adapter
			GmailAdapter gmail = new GmailAdapter();

			// create new session and assign username and password
			GmailSession myAccount = new GmailSession();
			myAccount.Username = "test@dontstayin.com";
			myAccount.Password = "foo";

			// login and retrieve mailbox info
			GmailAdapter.RequestResponseType loginResult = gmail.Refresh(myAccount);

			// display mailbox info
			if (loginResult == GmailAdapter.RequestResponseType.Success)
			{

				// show new inbox count
				Console.WriteLine("New Threads: " + myAccount.DefaultSearchCounts["Inbox"]);

				// if new threads exist, show the subject of the first one
				if (myAccount.UnreadThreads.Count > 0)
				{
					GmailThread newThread = (GmailThread)myAccount.UnreadThreads[0];
					Console.WriteLine("Latest thread subject: " + newThread.SubjectHtml);
				}
			}


			Console.WriteLine("Ended!!!");
			System.Environment.Exit(0);
		}

		static void UnverifyEmail(string Email, ref int count)
		{
			UsrSet us = new UsrSet(new Query(new Q(Usr.Columns.Email, Email)));
			if (us.Count > 0 && us[0].IsEmailVerified)
			{
				us[0].IsEmailVerified = false;
				us[0].Update();
				Console.WriteLine("  Unverified: " + Email);
				count++;
			}
			else
			{
				Console.WriteLine("  Skipped: " + Email);
			}
		}
		static void UnsubscribeEmail(string Email, ref int count)
		{
			UsrSet us = new UsrSet(new Query(new Q(Usr.Columns.Email, Email)));
			if (us.Count > 0 && !us[0].EmailHold)
			{
				us[0].EmailHold = true;
				us[0].Update();
				Console.WriteLine("  Unsubscribed: " + Email);
				count++;
			}
			else
			{
				Console.WriteLine("  Skipped: " + Email);
			}
		}
	}
}
