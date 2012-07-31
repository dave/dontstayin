using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using System.Collections;
using System.Diagnostics;

namespace Wednesday
{
	class Wednesday
	{
		static void Main(string[] args)
		{
			if (args.Length > 0 && args[0].Equals("r")) { System.Environment.Exit(0); } // cache reporting code removed - doubt anything calls this with "r" (reset) arg but just in case, terminate.

			int totalProcesses = 1;
			int processNumber = 1;
			int processIndex = 0;
			try
			{
				processNumber = int.Parse(args[0]);
				processIndex = processNumber - 1;
				totalProcesses = int.Parse(args[1]);
			}
			catch { }

			#region previousTuesday
			DateTime previousTuesday = DateTime.Today;
			if (DateTime.Today.DayOfWeek == DayOfWeek.Wednesday)
				previousTuesday = DateTime.Today.AddDays(-1);
			else if (DateTime.Today.DayOfWeek == DayOfWeek.Thursday)
				previousTuesday = DateTime.Today.AddDays(-2);
			else if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
				previousTuesday = DateTime.Today.AddDays(-3);
			else if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday)
				previousTuesday = DateTime.Today.AddDays(-4);
			else if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
				previousTuesday = DateTime.Today.AddDays(-5);
			else if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
				previousTuesday = DateTime.Today.AddDays(-6);
			#endregion

			bool NyeSpecial = previousTuesday.Day == 18 && previousTuesday.Month == 12 && previousTuesday.Year == 2007;

			Console.WriteLine("Starting process " + processNumber + " / " + totalProcesses + "... Email batch ID = " + previousTuesday.Date.ToShortDateString());
			
			int sentCount = 0;
			int noMatchCount = 0;
			int failCount = 0;
			UsrSet us = null;

			#region Get users
			
			Console.WriteLine("Selecting users...");
			if (Vars.DevEnv)
			{
				Query q = new Query();
				q.QueryCondition = new And(
					//new Q(Usr.Columns.K, 4),
					new Q(Usr.Columns.SendSpottedEmails, true),
					Usr.IsEmailVerifiedQ,
					Usr.IsNotSkeletonQ);
				q.OrderBy = new OrderBy(Usr.Columns.K);
				q.TopRecords = 5;

				us = new UsrSet(q);
			}
			else
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Q(Usr.Columns.SendSpottedEmails, true),
					Usr.IsEmailVerifiedQ,
					Usr.IsNotSkeletonQ,
					new StringQueryCondition(" ([Usr].[K] % " + totalProcesses + " = " + processIndex + ") "),
					new Q(Usr.Columns.DateTimeLastUpdateEmail, QueryOperator.NotEqualTo, previousTuesday)
				);
				q.OrderBy = new OrderBy(Usr.Columns.K);
				us = new UsrSet(q);
			}
			Console.WriteLine("Got {0} users...", us.Count.ToString("#,##0"));
			#endregion

			DateTime dtStart = DateTime.Now;

			#region Top bit...

			// Set date range to be Mon - Fri
			bool extraBody = previousTuesday.Equals(new DateTime(2009, 10, 20));
			int extraFlyerK = 1442;
			string extraSubject = "Don't Stay In this week PLUS 10% off all Halloween costumes for DSI users at Escapade!!!";
			string extraBodyHtml = "";

			//Another section below without paragraph aboive
			extraBodyHtml += "<p>Halloween is coming! This week we're offering a 10% discount on your Halloween fancy dress at <a href=\"[LOGIN(/popup/flyerclick/k-" + extraFlyerK + ")]\">escapade.co.uk</a> online store, order now to beat the postal strike! <a href=\"[LOGIN(/popup/flyerclick/k-" + extraFlyerK + ")]\">Click here for details</a>.</p>\n";
			//extraBodyHtml += "[/div]\n<div style=\"padding:0px 0px 13px 0px;\"><center><a href=\"[LOGIN(/popup/flyerclick/k-" + extraFlyerK + ")]\"><img src=\"http://www.dontstayin.com/images/flyer/" + extraFlyerK + ".gif\" width=\"500\" height=\"300\" border=\"0\" style=\"border:0px solid #000000;\"></a></center></div>\n[h1]Events this week[/h1]\n[div]";
			extraBodyHtml += "[/div]\n[h1]Spotlight[/h1]\n<center><a href=\"[LOGIN(/popup/flyerclick/k-" + extraFlyerK + ")]\"><img src=\"http://www.dontstayin.com/images/flyer/" + extraFlyerK + ".gif\" width=\"630\" height=\"401\" border=\"0\"></a></center></div>\n[h1]Events this week[/h1]\n[div]";
			//extraBodyHtml += "[/div]\n[h1]Find the animals[/h1]\n<center><a href=\"[LOGIN(/popup/flyerclick/k-" + extraFlyerK + ")]\"><img src=\"http://www.dontstayin.com/images/flyer/" + extraFlyerK + ".gif\" width=\"498\" height=\"175\" border=\"0\" style=\"border:1px solid #000000;\"></a></center></div>\n[h1]Megaspot[/h1]\n<center><a href=\"[LOGIN(/popup/flyerclick/k-299)]\"><img src=\"http://www.dontstayin.com/images/flyer/299.gif\" width=\"498\" height=\"175\" border=\"0\" style=\"border:1px solid #000000;\"></a></center></div>\n[h1]Events this week[/h1]\n[div]";


			//Big centered title
			//extraBodyHtml += "<p><center><b style=\"font-size:14px;\">Exclusive Interview with the duo behind this summers South West Four - Danny from Turnmills & Damian from Heat.</b></center></p>\n";

			//Single image link:
			//extraBodyHtml += "<p><center><a href=\"[LOGIN(/article-236)]\"><img src=\"http://pix.dontstayin.com/113d2579-4f00-434c-bbaa-54974e392c74.jpg\" width=\"100\" width=\"100\" border=\"0\" style=\"margin-bottom:5px;\"><br>Hed Kandi @ KoKo</a></center></p>\n";
			//extraBodyHtml += "<p><center><a href=\"[LOGIN(/popup/bannerclick/bannerk-3751)]\"><img src=\"http://misc.dontstayin.com/6e1f3049-20c9-4fc5-8f8d-28c3e498c313.jpg\" width=\"100\" width=\"100\" border=\"0\" style=\"margin-bottom:5px;border:1px solid #000000;\"><br><b>Read the article!</b></a></center></p>\n";

			//Image link on right, paragraph on left:
			//extraBodyHtml += "<p><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tr><td valign=\"top\">Another weekend, another broadcast!  Red Velvet launched at SEone with a playboy party, and we were there to check out exactly what went on. With a free bar, some wicked dancers and a great selection of tunes, Red Velvet kicked off their first party with a bang!</td><td valign=\"top\"><center><a href=\"[LOGIN(/article-171)]\"><img src=\"http://pix.dontstayin.com/c4619c2f-595c-4f42-b60e-e7e400b932c6.jpg\" width=\"100\" width=\"100\" border=\"0\" style=\"margin-bottom:5px;\"><br>Red Velvet @ SEOne</a></center></td></tr></table></p>\n";

			//Two image links
			//extraBodyHtml += "<p><table width=\"100%\"><tr><td width=\"50%\"><center><a href=\"[LOGIN(/article-147)]\"><img src=\"[WEB-ROOT]gfx/video-frantic.gif\" width=\"100\" width=\"100\" border=\"0\" style=\"margin-bottom:5px;\"><br>Frantic : MORE @ SeOne</a></center></td><td width=\"50%\"><center><a href=\"[LOGIN(/article-148)]\"><img src=\"[WEB-ROOT]gfx/video-ndsm.gif\" width=\"100\" width=\"100\" border=\"0\" style=\"margin-bottom:5px;\"><br>NastyDirtySexMusic @ Ministry of Sound</a></center></td></tr></table></p>\n";
			//extraBodyHtml += "<p><table width=\"100%\"><tr><td width=\"50%\" valign=\"center\"><center><a href=\"[LOGIN(/popup/bannerclick/bannerk-3752)]\"><img src=\"http://misc.dontstayin.com/c3/ac/c3ac94ce-9935-430b-b9e4-a68c983d0f62.jpg\" width=\"100\" width=\"100\" border=\"0\" style=\"margin-bottom:5px;border:1px solid #000000;\"><br><b>Watch the video</b></a></center></td><td width=\"50%\" valign=\"center\"><center><a href=\"[LOGIN(/popup/bannerclick/bannerk-3751)]\"><img src=\"http://misc.dontstayin.com/f5/9a/f59a5dea-e597-43a5-948d-4bcde5f71a74.jpg\" width=\"100\" height=\"100\" border=\"0\" style=\"margin-bottom:5px;border:1px solid #000000;\"/><br><b>Buy tickets</b></a></center></td></tr></table></p>\n";

			//Single paragraph:
			//extraBodyHtml += "<p>What followed was a night of glitz, glamour, disco balls, fantastic music and one of the best crowds in London. Click the icon below to watch the video:</p>\n";
			//extraBodyHtml += "<p>Also, a few weeks ago we did a broadcast from Frantic presents MORE @ SEone. We showed you some snippits of interviews with Frantic boss Will Paterson, and also hard dance superstar DJ Lisa Pin-up. Click the icons below to watch the interviews in all their glory!</p>\n";


			//Another section below:
			//extraBodyHtml += "[/div][h1]Win a Sony Ericcson K800i camera phone[/h1][div]";
			//extraBodyHtml += "<p>Dont forget we're giving away a Brand new Sony Ericcson K800i camera phone every Friday. Click below to win!</p>\n";
			//extraBodyHtml += "[/div]\n<p style=\"margin:5px 5px 15px 5px;\"><center><a href=\"[LOGIN(/popup/bannerclick/bannerk-4100)]\"><img src=\"http://pix.dontstayin.com/ba/79/ba7991cc-9d12-4bd0-92f1-6bac37530935.jpg\" width=\"100\" height=\"100\" border=\"0\"></a><br><a href=\"[LOGIN(/popup/bannerclick/bannerk-4100)]\">Click here to win!</a></p></center>\n[h1]Events this week[/h1]\n[div]";
			//OR
			//extraBodyHtml+= "[/div]\n[h1]Events this week[/h1]\n[div]";



			//Another section below:
			//extraBodyHtml += "[/div][h1]Win a Sony Ericcson K800i camera phone[/h1][div]";
			//extraBodyHtml += "<p>Dont forget we're giving away a Brand new Sony Ericcson K800i camera phone every Friday. Click below to win!</p>\n";
			//extraBodyHtml += "[/div]\n<p style=\"margin:5px 5px 15px 5px;\"><center><a href=\"[LOGIN(/popup/bannerclick/bannerk-4100)]\"><img src=\"http://pix.dontstayin.com/ba/79/ba7991cc-9d12-4bd0-92f1-6bac37530935.jpg\" width=\"100\" height=\"100\" border=\"0\"></a><br><a href=\"[LOGIN(/popup/bannerclick/bannerk-4100)]\">Click here to win!</a></p></center>\n[h1]Events this week[/h1]\n[div]";
			//OR
			//extraBodyHtml+= "[/div]\n[h1]Events this week[/h1]\n[div]";

			//Another section below without paragraph aboive
			//extraBodyHtml += "<p>Dont forget we're giving away a Brand new Sony Ericcson K800i camera phone every Friday. Click below to win!</p>\n";
			//extraBodyHtml += "[/div]\n<p style=\"margin:5px 5px 15px 5px;\"><center><a href=\"[LOGIN(/popup/bannerclick/bannerk-4100)]\"><img src=\"http://pix.dontstayin.com/ba/79/ba7991cc-9d12-4bd0-92f1-6bac37530935.jpg\" width=\"100\" height=\"100\" border=\"0\"></a><br><a href=\"[LOGIN(/popup/bannerclick/bannerk-4100)]\">Click here to win!</a></p></center>\n[h1]Events this week[/h1]\n[div]";
			//OR
			//extraBodyHtml+= "[/div]\n[h1]Events this week[/h1]\n[div]";



			//Image link on right, paragraph on left:
			//extraBodyHtml += "<p><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tr><td valign=\"top\">SW4 just got BIGGER - Paul Van Dyke album Launch & Official London afterparty announced!</td><td valign=\"top\"><center><a href=\"[LOGIN(/popup/bannerclick/bannerk-7711)]\"><img src=\"http://se.dontstayin.com/ed/07/ed071351-ffcc-4434-b786-68d3253c8e45.jpg\" width=\"100\" width=\"100\" border=\"0\" style=\"margin-bottom:5px;\"><br>Click to read the article</a></center></td></tr></table></p>\n";
			//extraBodyHtml += "<p><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tr><td valign=\"top\">DontStayIn join the lineup @ SW4 London - GET YOUR TICKETS ON DSI TODAY!!</td><td valign=\"top\"><center><a href=\"[LOGIN(/popup/bannerclick/bannerk-7712)]\"><img src=\"http://sd.dontstayin.com/d7/8f/d78fb611-3f01-4d43-8174-04c9cc098a98.jpg\" width=\"100\" width=\"100\" border=\"0\" style=\"margin-bottom:5px;\"><br>Click to read the article</a></center></td></tr></table></p>\n";
			//extraBodyHtml += "<p><table width=\"100%\"><tr><td width=\"50%\" valign=\"center\"><div style=\"font-family: Verdana, sans-serif;font-size:11px;padding:0px 2px 0px 2px;line-height:130%;\"><center><b>SW4 just got BIGGER</b> - Paul Van Dyke album Launch & Official London afterparty announced!</center></div></td><td width=\"50%\" valign=\"center\"><div style=\"font-family: Verdana, sans-serif;font-size:11px;padding:0px 2px 0px 2px;line-height:130%;\"><center><b>DontStayIn join the lineup @ SW4 London</b> - get your tickets on DSI today!!</center></div></td></tr></table></p>\n";
			//extraBodyHtml += "<p><table width=\"100%\"><tr><td width=\"50%\" valign=\"center\"><div style=\"font-family: Verdana, sans-serif;font-size:11px;padding:0px 2px 0px 2px;line-height:130%;\"><center><a href=\"[LOGIN(/popup/bannerclick/bannerk-7711)]\"><img src=\"http://se.dontstayin.com/ed/07/ed071351-ffcc-4434-b786-68d3253c8e45.jpg\" width=\"100\" width=\"100\" border=\"0\" style=\"margin-bottom:5px;border:1px solid #000000;\"><br>Click to read the article</a></center></div></td><td width=\"50%\" valign=\"center\"><div style=\"font-family: Verdana, sans-serif;font-size:11px;padding:0px 2px 0px 2px;line-height:130%;\"><center><a href=\"[LOGIN(/popup/bannerclick/bannerk-7712)]\"><img src=\"http://sd.dontstayin.com/d7/8f/d78fb611-3f01-4d43-8174-04c9cc098a98.jpg\" width=\"100\" width=\"100\" border=\"0\" style=\"margin-bottom:5px;border:1px solid #000000;\"><br>Click to read the article</a></center></div></td></tr></table></p>\n";
			//extraBodyHtml += "<p><table width=\"100%\"><tr><td width=\"50%\" valign=\"center\"><div style=\"font-family: Verdana, sans-serif;font-size:11px;padding:0px 2px 0px 2px;line-height:130%;\"><center><a href=\"[LOGIN(/popup/bannerclick/bannerk-7714)]\"><img src=\"http://www.dontstayin.com/gfx/icon-ticketsblue-yellow.gif\" border=\"0\" align=\"absmiddle\" style=\"margin-right:3px;\" width=\"26\" height=\"21\">Buy tickets for SW4 Cardiff</a></center></div></td><td width=\"50%\" valign=\"center\"><div style=\"font-family: Verdana, sans-serif;font-size:11px;padding:0px 2px 0px 2px;line-height:130%;\"><center><a href=\"[LOGIN(/popup/bannerclick/bannerk-7713)]\"><img src=\"http://www.dontstayin.com/gfx/icon-ticketsblue-yellow.gif\" border=\"0\" align=\"absmiddle\" style=\"margin-right:3px;\" width=\"26\" height=\"21\">Buy tickets for SW4 London</a></center></div></td></tr></table></p>\n";
			//extraBodyHtml += "<p><center> - </center></p>\n";


			//big image on the right
//            extraBodyHtml += @"
//<a href=""[LOGIN(/members/honey-sugah-d)]""><img src=""http://sf.dontstayin.com/f6/93/f6932062-d98c-44e0-8694-2b995335bb9a.jpg"" width=""100"" height=""300"" border=""0"" align=""right""></a>
//<p><b><font size=""4"">Born again raver</font></b></p>
//<p>The winner of this month's WIN A YEAR'S FREE CLUBBING compeition is <a href=""[LOGIN(/members/honey-sugah-d)]"">Honey-Sugah-D</a>, otherwise known as Lizi, or that girl up there on the podium dancing with a big smile on her face.</p>
//<p>Lizi is part of Sugah Dee, the popular dance troupe who perform at hard dance events in Peterborough, London and elsewhere.</p>
//<p>The lucky lady from just outside Cambridge was 'shocked' upon learning of her good fortune and says the free entrance will certainly help with her clubbing budget.</p>
//<p>She got back into clubbing three years ago and has left behind her old life shopping at B&Q in the weekends.</p>
//<p>""The most exciting thing I used to do was buy a strimmer.""</p>
//<p>You too can drastically improve your quality of life by winning a year's free clubbing. Just <a href=""[LOGIN(/2007/sep/tickets)]"">buy a ticket to any event on DontStayIn</a> this month and go into the draw.</p>";


			

			//extraBodyHtml += "<p>Dont forget to get your tickets via DontStayIn before the end of the month to stand the chance of winning free clubbing for a year!!!</p>\n";
			//extraBodyHtml += "<p>The winner will be picked at midday October 1st (next Monday) from everyone who bought a ticket in September.</p>\n";
			//extraBodyHtml += "<p><center><b style=\"font-size:14px;\">Recommended tickets: <a href=\"[LOGIN(/pages/ticketscalendar/M-9/Y-2007#Day20070925)]\">September</a>, <a href=\"[LOGIN(/pages/ticketscalendar/M-10/Y-2007)]\">October</a></b></center></p>\n";
			//extraBodyHtml += "<p><center><b style=\"font-size:14px;\">All tickets worldwide: <a href=\"[LOGIN(/2007/sep/tickets#Day20070925)]\">September</a>, <a href=\"[LOGIN(/2007/oct/tickets)]\">October</a></b></center></p>\n";
			//extraBodyHtml += "<p><center><b><a href=\"[LOGIN(/groups/dontstayin-website/chat/k-2148273)]\" style=\"font-size:14px;\">Find out more</a></b></center></p>\n";


			//9333

			//Another section below without paragraph aboive
			//extraBodyHtml += "<p>This Bank Holiday Sunday 24th May, Lock N Load Events presents another house marathon in the shape of Connected at London�s KOKO featuring two of Europe�s most exciting breakthrough DJ/Producers of the past few years - Style Of Eye and Bart B More, alongside some of the capital�s hottest up-and-coming talent. Don�t miss London�s hottest event this coming Bank Holiday weekend (no work Monday!). <a href=\"[LOGIN(/popup/flyerclick/k-" + extraFlyerK + ")]\">click here</a>.</p>\n";
			//extraBodyHtml += "[/div]\n<div style=\"padding:0px 0px 13px 0px;\"><center><a href=\"[LOGIN(/popup/flyerclick/k-" + extraFlyerK + ")]\"><img src=\"http://www.dontstayin.com/images/flyer/" + extraFlyerK + ".gif\" width=\"500\" height=\"300\" border=\"0\" style=\"border:0px solid #000000;\"></a></center></div>\n[h1]Events this week[/h1]\n[div]";
			//extraBodyHtml += "[/div]\n[h1]Spotlight[/h1]\n<center><a href=\"[LOGIN(/popup/flyerclick/k-" + extraFlyerK + ")]\"><img src=\"http://www.dontstayin.com/images/flyer/" + extraFlyerK + ".gif\" width=\"498\" height=\"242\" border=\"0\" style=\"border:1px solid #000000;\"></a></center></div>\n[h1]Events this week[/h1]\n[div]";
			//extraBodyHtml += "[/div]\n[h1]Find the animals[/h1]\n<center><a href=\"[LOGIN(/popup/flyerclick/k-" + extraFlyerK + ")]\"><img src=\"http://www.dontstayin.com/images/flyer/" + extraFlyerK + ".gif\" width=\"498\" height=\"175\" border=\"0\" style=\"border:1px solid #000000;\"></a></center></div>\n[h1]Megaspot[/h1]\n<center><a href=\"[LOGIN(/popup/flyerclick/k-299)]\"><img src=\"http://www.dontstayin.com/images/flyer/299.gif\" width=\"498\" height=\"175\" border=\"0\" style=\"border:1px solid #000000;\"></a></center></div>\n[h1]Events this week[/h1]\n[div]";



			//extraBodyHtml += "[/div]\n[h1]Events this week[/h1]\n[div]";
			#endregion

			MatchMaker mm = new MatchMaker();
			if (NyeSpecial)
			{
				mm.NyeSpecial = true;
				mm.StartDate = new DateTime(2007,12,31);
				mm.EndDate = new DateTime(2008, 1, 1);
			}
			else
			{
				mm.StartDate = previousTuesday.AddDays(1);
				mm.EndDate = mm.StartDate.AddDays(7);
			}
			StringBuilder eventsBody = null;

			

			try
			{
				MatchMaker.Return mmReturn = new MatchMaker.Return();
				for (int indK=0; indK<us.Count; indK++)
				{
					Usr u = us[indK];
					try
					{
						int totalEvents = 0;
						if (u.FavouriteMusicTypeK == 0 && u.MusicTypesFavouriteCount == 0 && u.HomePlaceK == 0 && u.PlacesVisitCount == 0)
						{
							totalEvents = 0;
						}
						else
						{
							eventsBody = new StringBuilder();
							mm.Init(u);
							mmReturn = mm.BuildString(eventsBody);
							totalEvents = mmReturn.Events;
						}

						if (totalEvents>0)
						{

							#region Build body
							StringBuilder s = new StringBuilder();
							if (extraBody)
								s.Append(extraBodyHtml);

							if (NyeSpecial)
								s.Append("<p>We've got some NYE events listed on the site that you might be interested in coming to. Check them out below, and if you're coming along, make sure you support DSI and buy your tickets via the event page!</p>");
							else
								s.Append("<p>We've got some events coming up in the next few days that you might be interested in coming to. Check them out below, and if you're coming along, make sure you click <b>I'll be there</b> on the event page.</p>");
							
							s.Append("<p>We're sending you events that match on ");
							if (mm.AllMusicTypes.Count>0)
							{
								s.Append("music you like (");
								bool first=true;
								foreach(MusicType mt in mm.AllMusicTypes.Values)
								{
									s.Append(first?"":", ");
									s.Append(mt.Name);
									first=false;
								}
								s.Append(")");
							}
							
							if (mm.SelectedPlaces.Count>0)
							{
								if (mm.AllMusicTypes.Count > 0)
									s.Append(", and ");
									
								s.Append("places you visit (");
								bool first=true;
								foreach(Place p in mm.SelectedPlaces.Values)
								{
									s.Append(first?"":", ");
									s.Append(p.NamePlainRegion);
									first=false;
								}
								s.Append(")");
							}
							s.Append(". <a href=\"[LOGIN(/pages/updateoptions)]\">Click here</a> to change these selections.</p>");
							s.Append("<p>If you would prefer not to receive these emails, un-tick \"Send me the weekly email\" on your <a href=\"[LOGIN(/pages/myprivacy)]\">My privacy</a> page.</p>");
							if (mmReturn.HasFreeGuestlist || mmReturn.HasTickets)
							{
								s.Append("<p>What do the icons below mean?</p>");

								if (mmReturn.HasTickets)
									s.Append("<p><img src=\"[WEB-ROOT]gfx/icon-tickets-small.png\" width=\"20\" height=\"16\" border=\"0\" align=\"absmiddle\" style=\"margin-right:3px;\">Tickets available!</p>");

								if (mmReturn.HasFreeGuestlist)
									s.Append("<p><img src=\"[WEB-ROOT]gfx/icon-freeguestlist-small.png\" width=\"20\" height=\"16\" border=\"0\" align=\"absmiddle\" style=\"margin-right:3px;\">Exclusive Free Guestlist offers for our members!</p>");
							}
							s.Append("[/div]");
							s.Append(eventsBody);
							s.Remove(s.Length - 6, 6);//get rid of trailing [/div]
							#endregion

							Mailer sm = new Mailer();
							sm.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
							sm.UsrRecipient=u;
							sm.To=u.Email;
							if (extraBody)
								sm.Subject = extraSubject;
							else if (NyeSpecial)
								sm.Subject = "DontStayIn this NEW YEARS EVE!!!";
							else
								sm.Subject = "DontStayIn this week!";
							sm.Body=s.ToString();
							sm.RedirectUrl=UrlInfo.PageUrl("MyCalendar","D",DateTime.Today.Day.ToString(),"M",DateTime.Today.Month.ToString(),"Y",DateTime.Today.Year.ToString())+"#Day"+DateTime.Today.Year.ToString()+DateTime.Today.Month.ToString("00")+DateTime.Today.Day.ToString("00");
							sm.Bulk = true;
							//if (!Vars.DevEnv)
							sm.Send();

							if (extraBody)
							{
								try
								{
									StoredProcedures.Bobs.Flyer.LogSend.Execute(extraFlyerK);
									//Bobs.BannerStat.Log(9333, Banner.Positions.Hotbox, DateTime.Today, 1, 0, 0);
								}
								catch { }
							}

							sentCount++;
							
							if (sentCount % 1000 == 0)
							{
								//TimeSpan timeTaken = (DateTime.Now - dtStart);
								//string summary = "<p>Start: " + dtStart.ToLongTimeString() + "</p>";
								//summary += "<p>Got to " + sentCount.ToString("#,##0") + ": " + DateTime.Now.ToLongTimeString() + "</p>";
								//summary += "<p>Total time: " + timeTaken.TotalHours.ToString("0.##") + " hr</p>";
								//summary += "<p>Event matches - sent email: " + sentCount.ToString() + "</p>";
								//summary += "<p>No matches - email not sent: " + noMatchCount.ToString() + "</p>";
								//summary += "<p>Failed (exception): " + failCount.ToString() + "</p>";

								//Mailer smAdmin = new Mailer();
								//smAdmin.TemplateType = Mailer.TemplateTypes.AdminNote;

								//smAdmin.Body += "<h1>Summary</h1>";
								//smAdmin.Body += summary;

								//smAdmin.Subject = "Update email (process " + processNumber + " / " + totalProcesses + ") " + sentCount.ToString("#,##0");
								//smAdmin.To = "d.brophy@dontstayin.com";
								//smAdmin.Send();
							}

						}
						else
						{
							noMatchCount++;
						}

						Console.WriteLine("Sent: " + sentCount.ToString() + ", UsrK: " + u.K + ", Events = " + totalEvents.ToString());

						u.DateTimeLastUpdateEmail = previousTuesday;
						u.Update();



					}
					catch (Exception ex)
					{
						failCount++;
						Console.WriteLine(sentCount.ToString()+" "+u.Email + " EX " + ex.ToString());
					}
					finally
					{
						us.Kill(indK);
						GC.Collect();
					}
				}
			}
			finally
			{
			//	if (!Vars.DevEnv)
			//	{
					//TimeSpan timeTaken = (DateTime.Now - dtStart);
					//string summary = "<p>Start: " + dtStart.ToLongTimeString() + "</p>";
					//summary += "<p>End: " + DateTime.Now.ToLongTimeString() + "</p>";
					//summary += "<p>Total time: " + timeTaken.TotalHours.ToString("0.##") + " hr</p>";
					//summary += "<p>Event matches - sent email: " + sentCount.ToString() + "</p>";
					//summary += "<p>No matches - email not sent: " + noMatchCount.ToString() + "</p>";
					//summary += "<p>Failed (exception): " + failCount.ToString() + "</p>";

					//Mailer smAdmin = new Mailer();
					//smAdmin.TemplateType = Mailer.TemplateTypes.AdminNote;

					//smAdmin.Body += "<h1>Summary</h1>";
					//smAdmin.Body += summary;

					//smAdmin.Subject = "Update email finished (process " + processNumber + " / " + totalProcesses + ")";
					//smAdmin.To = "d.brophy@dontstayin.com";
					//smAdmin.Send();
			//	}

			}
			Console.WriteLine("Ended!!!");
			System.Environment.Exit(0);
		}
		
		
	}
}
