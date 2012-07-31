using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Bobs
{
	#region MixmagEntry
	/// <summary>
	/// Each entry into the Mixmag Vote competition has a record in here
	/// </summary>
	[Serializable]
	public partial class MixmagEntry
	{

		#region Simple members
		/// <summary>
		/// Primary key
		/// </summary>
		public override int K
		{
			get { return (int)this[MixmagEntry.Columns.K] as int? ?? 0; }
			set { this[MixmagEntry.Columns.K] = value; }
		}
		/// <summary>
		/// Comp K
		/// </summary>
		public override int MixmagCompK
		{
			get { return (int)this[MixmagEntry.Columns.MixmagCompK]; }
			set { this[MixmagEntry.Columns.MixmagCompK] = value; }
		}
		/// <summary>
		/// Facebook UID (can by null if votes come before the entry)
		/// </summary>
		public override long? FacebookUid
		{
			get { return (long?)this[MixmagEntry.Columns.FacebookUid]; }
			set { this[MixmagEntry.Columns.FacebookUid] = value; }
		}
		/// <summary>
		/// Date / time of the entry
		/// </summary>
		public override DateTime? DateTime
		{
			get { return (DateTime?)this[MixmagEntry.Columns.DateTime]; }
			set { this[MixmagEntry.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Url of the image entered
		/// </summary>
		public override string ImageUrl
		{
			get { return (string)this[MixmagEntry.Columns.ImageUrl]; }
			set { this[MixmagEntry.Columns.ImageUrl] = value; }
		}
		/// <summary>
		/// Email
		/// </summary>
		public override string Email
		{
			get { return (string)this[MixmagEntry.Columns.Email]; }
			set { this[MixmagEntry.Columns.Email] = value; }
		}
		/// <summary>
		/// First name
		/// </summary>
		public override string FirstName
		{
			get { return (string)this[MixmagEntry.Columns.FirstName]; }
			set { this[MixmagEntry.Columns.FirstName] = value; }
		}
		/// <summary>
		/// Last name
		/// </summary>
		public override string LastName
		{
			get { return (string)this[MixmagEntry.Columns.LastName]; }
			set { this[MixmagEntry.Columns.LastName] = value; }
		}
		/// <summary>
		/// Send daily vote update emails?
		/// </summary>
		public override bool SendDailyEmails
		{
			get { return (bool)this[MixmagEntry.Columns.SendDailyEmails]; }
			set { this[MixmagEntry.Columns.SendDailyEmails] = value; }
		}

		
		#endregion


		public static void SendUpdateEmails()
		{
			//send updates

			MixmagComp c1 = MixmagComp.GetByK(2);
			if (c1.EndDate < System.DateTime.Now)
				return;

			List<string> emails = new List<string>();
			Query qEmails = new Query();
			qEmails.QueryCondition = new And(
				new Q(MixmagEntry.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-28)),
				new Q(MixmagEntry.Columns.SendDailyEmails, true));
			MixmagEntrySet mesEmails = new MixmagEntrySet(qEmails);
			foreach (MixmagEntry meEmail in mesEmails)
			{
				if (emails.Contains(meEmail.Email))
					continue;

				emails.Add(meEmail.Email);

				Query qComps = new Query();
				qComps.QueryCondition = new And(
					new Q(MixmagEntry.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-28)),
					new Q(MixmagEntry.Columns.SendDailyEmails, true),
					new Q(MixmagEntry.Columns.Email, meEmail.Email));
				qComps.OrderBy = new OrderBy(MixmagEntry.Columns.K);
				MixmagEntrySet mesComps = new MixmagEntrySet(qComps);
				//string s = "";
				if (mesComps.Count > 0)
				{
					List<int> comps = new List<int>();

					foreach (MixmagEntry meComp in mesComps)
					{
						if (comps.Contains(meComp.MixmagCompK))
							continue;

						comps.Add(meComp.MixmagCompK);

						MixmagComp comp = MixmagComp.GetByK(meComp.MixmagCompK);
						Query qEntries = new Query();
						qEntries.QueryCondition = new And(
							new Q(MixmagEntry.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-28)),
							new Q(MixmagEntry.Columns.SendDailyEmails, true),
							new Q(MixmagEntry.Columns.Email, meComp.Email),
							new Q(MixmagEntry.Columns.MixmagCompK, meComp.MixmagCompK));
						qEntries.OrderBy = new OrderBy(MixmagEntry.Columns.K);
						MixmagEntrySet mesEntries = new MixmagEntrySet(qEntries);
						if (mesEntries.Count > 0)
						{
							string s = comp.DailyEmailHtmlComp;

							int entryNumber = 1;
							foreach (MixmagEntry meEntry in mesEntries)
							{
								if (mesEntries.Count > 1)
									s += comp.DailyEmailHtmlEntryMultiple.Replace("%1", entryNumber.ToString());

								entryNumber++;

								Query qVoteCount = new Query();
								qVoteCount.QueryCondition = new Q(MixmagVote.Columns.MixmagEntryK, meComp.K);
								qVoteCount.ReturnCountOnly = true;
								MixmagVoteSet mvsVoteCount = new MixmagVoteSet(qVoteCount);

								int daysToGo = (int)(meEntry.DateTime.Value.AddDays(30) - System.DateTime.Now).TotalDays;
								if (daysToGo < 1)
									daysToGo = 1;

								
								s += comp.DailyEmailHtmlEntry
									.Replace("%1", meEntry.ImageUrl)
									.Replace("%2", mvsVoteCount.Count.ToString("#,##0"))
									.Replace("%3", mvsVoteCount.Count == 1 ? "" : "s")
									.Replace("%4", daysToGo.ToString("#,##0"))
									.Replace("%5", daysToGo == 1 ? "" : "s")
									.Replace("%6", "http://mixmag-vote.com/repost?entry=" + meEntry.K.ToString())
									.Replace("%7", "http://mixmag-vote.com/" + meEntry.K.ToString());
							}
							s += @"
<p>
	To stop these daily emails, <a href=""http://mixmag-vote.com/stop?email=" + HttpUtility.UrlEncode(meEmail.Email) + @""">click here</a>.
</p>
";



							System.Net.Mail.SmtpClient c = new System.Net.Mail.SmtpClient();
							c.Host = Common.Properties.GetDefaultSmtpServer();
							System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
							string fullBody = s;
							m.Body = fullBody.Replace("\n", "\n\r");
							m.From = new System.Net.Mail.MailAddress("mail@mixmag-vote.com");
							m.IsBodyHtml = true;
							m.Subject = comp.DailyEmailHtmlCompSubject;

							if (Vars.DevEnv)
							{
								m.To.Add("dev.mail@dontstayin.com");
							}
							else
							{
								m.To.Add(meEmail.Email);
							}

							c.Send(m);







						}
					}
					
					
				}
			}


		}

	}
	#endregion
}
