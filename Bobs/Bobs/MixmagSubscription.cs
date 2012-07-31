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

using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;

namespace Bobs
{

	#region MixmagSubscription
	/// <summary>
	/// Subscribers to the Mixmag-by-email service
	/// </summary>
	[Serializable]
	public partial class MixmagSubscription
	{

		#region Simple members
		/// <summary>
		/// Primary key
		/// </summary>
		public override int K
		{
			get { return (int)this[MixmagSubscription.Columns.K] as int? ?? 0; }
			set { this[MixmagSubscription.Columns.K] = value; }
		}
		/// <summary>
		/// Facebook user ID
		/// </summary>
		public override long FacebookUID
		{
			get { return (long)this[MixmagSubscription.Columns.FacebookUID]; }
			set { this[MixmagSubscription.Columns.FacebookUID] = value; }
		}
		/// <summary>
		/// Facebook email extended permission
		/// </summary>
		public override bool? FacebookPermissionEmail
		{
			get { return (bool?)this[MixmagSubscription.Columns.FacebookPermissionEmail]; }
			set { this[MixmagSubscription.Columns.FacebookPermissionEmail] = value; }
		}
		/// <summary>
		/// Facebook publish_stream extended permission
		/// </summary>
		public override bool? FacebookPermissionPublish
		{
			get { return (bool?)this[MixmagSubscription.Columns.FacebookPermissionPublish]; }
			set { this[MixmagSubscription.Columns.FacebookPermissionPublish] = value; }
		}
		/// <summary>
		/// Date / time the subscription was created
		/// </summary>
		public override DateTime? DateTimeCreated
		{
			get { return (DateTime?)this[MixmagSubscription.Columns.DateTimeCreated]; }
			set { this[MixmagSubscription.Columns.DateTimeCreated] = value; }
		}
		/// <summary>
		/// Do we send mixmag by email?
		/// </summary>
		public override bool? SendMixmag
		{
			get { return (bool?)this[MixmagSubscription.Columns.SendMixmag]; }
			set { this[MixmagSubscription.Columns.SendMixmag] = value; }
		}
		/// <summary>
		/// Do we publish to their wall when they read?
		/// </summary>
		public override bool? PublishStoryOnRead
		{
			get { return (bool?)this[MixmagSubscription.Columns.PublishStoryOnRead]; }
			set { this[MixmagSubscription.Columns.PublishStoryOnRead] = value; }
		}
		/// <summary>
		/// Total number of issues we have sent this user
		/// </summary>
		public override int? TotalSent
		{
			get { return (int?)this[MixmagSubscription.Columns.TotalSent]; }
			set { this[MixmagSubscription.Columns.TotalSent] = value; }
		}
		/// <summary>
		/// Total number of issues this user has opened
		/// </summary>
		public override int? TotalRead
		{
			get { return (int?)this[MixmagSubscription.Columns.TotalRead]; }
			set { this[MixmagSubscription.Columns.TotalRead] = value; }
		}
		/// <summary>
		/// First name
		/// </summary>
		public override string FirstName
		{
			get { return (string)this[MixmagSubscription.Columns.FirstName]; }
			set { this[MixmagSubscription.Columns.FirstName] = value; }
		}
		/// <summary>
		/// Last name
		/// </summary>
		public override string LastName
		{
			get { return (string)this[MixmagSubscription.Columns.LastName]; }
			set { this[MixmagSubscription.Columns.LastName] = value; }
		}
		/// <summary>
		/// First line of the postal address.
		/// </summary>
		public override string AddressFirstLine
		{
			get { return (string)this[MixmagSubscription.Columns.AddressFirstLine]; }
			set { this[MixmagSubscription.Columns.AddressFirstLine] = value; }
		}
		/// <summary>
		/// Postal code.
		/// </summary>
		public override string AddressPostCode
		{
			get { return (string)this[MixmagSubscription.Columns.AddressPostCode]; }
			set { this[MixmagSubscription.Columns.AddressPostCode] = value; }
		}
		/// <summary>
		/// Country
		/// </summary>
		public override int? AddressCountryK
		{
			get { return (int?)this[MixmagSubscription.Columns.AddressCountryK]; }
			set { this[MixmagSubscription.Columns.AddressCountryK] = value; }
		}
		/// <summary>
		/// Has the name and address been completed
		/// </summary>
		public override bool? IsAddressComplete
		{
			get { return (bool?)this[MixmagSubscription.Columns.IsAddressComplete]; }
			set { this[MixmagSubscription.Columns.IsAddressComplete] = value; }
		}
		/// <summary>
		/// Is the email verified
		/// </summary>
		public override bool? IsEmailVerified
		{
			get { return (bool?)this[MixmagSubscription.Columns.IsEmailVerified]; }
			set { this[MixmagSubscription.Columns.IsEmailVerified] = value; }
		}
		/// <summary>
		/// Email address
		/// </summary>
		public override string Email
		{
			get { return (string)this[MixmagSubscription.Columns.Email]; }
			set { this[MixmagSubscription.Columns.Email] = value; }
		}
		/// <summary>
		/// Has the email been provided
		/// </summary>
		public override bool? IsEmailComplete
		{
			get { return (bool?)this[MixmagSubscription.Columns.IsEmailComplete]; }
			set { this[MixmagSubscription.Columns.IsEmailComplete] = value; }
		}
		/// <summary>
		/// Email verification secret
		/// </summary>
		public override string EmailVerificationSecret
		{
			get { return (string)this[MixmagSubscription.Columns.EmailVerificationSecret]; }
			set { this[MixmagSubscription.Columns.EmailVerificationSecret] = value; }
		}
		/// <summary>
		/// Have we received a bounce message for this email?
		/// </summary>
		public override bool? IsEmailBroken
		{
			get { return (bool?)this[MixmagSubscription.Columns.IsEmailBroken]; }
			set { this[MixmagSubscription.Columns.IsEmailBroken] = value; }
		}
		/// <summary>
		/// When did we receive the bounce?
		/// </summary>
		public override DateTime? EmailBrokenDateTime
		{
			get { return (DateTime?)this[MixmagSubscription.Columns.EmailBrokenDateTime]; }
			set { this[MixmagSubscription.Columns.EmailBrokenDateTime] = value; }
		}
		/// <summary>
		/// Is this email address from Facebook?
		/// </summary>
		public override bool? IsEmailFromFacebook
		{
			get { return (bool?)this[MixmagSubscription.Columns.IsEmailFromFacebook]; }
			set { this[MixmagSubscription.Columns.IsEmailFromFacebook] = value; }
		}
		#endregion

	}
	#endregion

	#region MixmagIssue
	/// <summary>
	/// Each Mixmag issue has a record in here
	/// </summary>
	[Serializable]
	public partial class MixmagIssue
	{

		#region Simple members
		/// <summary>
		/// Primary key
		/// </summary>
		public override int K
		{
			get { return (int)this[MixmagIssue.Columns.K] as int? ?? 0; }
			set { this[MixmagIssue.Columns.K] = value; }
		}
		/// <summary>
		/// URL of the Ceros content
		/// </summary>
		public override string CerosUrl
		{
			get { return (string)this[MixmagIssue.Columns.CerosUrl]; }
			set { this[MixmagIssue.Columns.CerosUrl] = value; }
		}
		/// <summary>
		/// If this is not set, the email won't send and it won't be available as a back-issue
		/// </summary>
		public override bool? Ready
		{
			get { return (bool?)this[MixmagIssue.Columns.Ready]; }
			set { this[MixmagIssue.Columns.Ready] = value; }
		}
		/// <summary>
		/// When we're queued to send the email, and make available as a back-issue
		/// </summary>
		public override DateTime? DateTimeSend
		{
			get { return (DateTime?)this[MixmagIssue.Columns.DateTimeSend]; }
			set { this[MixmagIssue.Columns.DateTimeSend] = value; }
		}
		/// <summary>
		/// The date on the cover (usually the month after when it's sent)
		/// </summary>
		public override DateTime? IssueCoverDate
		{
			get { return (DateTime?)this[MixmagIssue.Columns.IssueCoverDate]; }
			set { this[MixmagIssue.Columns.IssueCoverDate] = value; }
		}
		/// <summary>
		/// Total sends
		/// </summary>
		public override int? TotalSent
		{
			get { return (int?)this[MixmagIssue.Columns.TotalSent]; }
			set { this[MixmagIssue.Columns.TotalSent] = value; }
		}
		/// <summary>
		/// Total reads
		/// </summary>
		public override int? TotalRead
		{
			get { return (int?)this[MixmagIssue.Columns.TotalRead]; }
			set { this[MixmagIssue.Columns.TotalRead] = value; }
		}
		/// <summary>
		/// Is the issue currently sending?
		/// </summary>
		public override bool? SendingNow
		{
			get { return (bool?)this[MixmagIssue.Columns.SendingNow]; }
			set { this[MixmagIssue.Columns.SendingNow] = value; }
		}
		/// <summary>
		/// Has the issue finished sending?
		/// </summary>
		public override bool? SendingFinished
		{
			get { return (bool?)this[MixmagIssue.Columns.SendingFinished]; }
			set { this[MixmagIssue.Columns.SendingFinished] = value; }
		}
		/// <summary>
		/// Date / time of the last send
		/// </summary>
		public override DateTime? LastSendDateTime
		{
			get { return (DateTime?)this[MixmagIssue.Columns.LastSendDateTime]; }
			set { this[MixmagIssue.Columns.LastSendDateTime] = value; }
		}
		/// <summary>
		/// Short text description of the contents
		/// </summary>
		public override string Summary
		{
			get { return (string)this[MixmagIssue.Columns.Summary]; }
			set { this[MixmagIssue.Columns.Summary] = value; }
		}
		/// <summary>
		/// Url of an image of the cover. MUST be 194px x 254px.
		/// </summary>
		public override string CoverImageUrl
		{
			get { return (string)this[MixmagIssue.Columns.CoverImageUrl]; }
			set { this[MixmagIssue.Columns.CoverImageUrl] = value; }
		}
		/// <summary>
		/// Data used to show the contents.
		/// </summary>
		public override string ContentsData
		{
			get { return (string)this[MixmagIssue.Columns.ContentsData]; }
			set { this[MixmagIssue.Columns.ContentsData] = value; }
		}
		/// <summary>
		/// Note shown under the link on the listings page
		/// </summary>
		public override string IssueNote
		{
			get { return (string)this[MixmagIssue.Columns.IssueNote]; }
			set { this[MixmagIssue.Columns.IssueNote] = value; }
		}
		/// <summary>
		/// If there are multiple covers, this is the ID
		/// </summary>
		public override int? IssueCoverId
		{
			get { return (int?)this[MixmagIssue.Columns.IssueCoverId]; }
			set { this[MixmagIssue.Columns.IssueCoverId] = value; }
		}
		#endregion

		public static string DavesFunction()
		{
			return "testing!";
		}

		public string Url(bool includeDomain, int? page)
		{
			return UrlStatic(includeDomain, IssueCoverDate.Value, page, IssueCoverId);
			//return (includeDomain ? "http://www.mixmag-online.com" : "") + "/" + this.IssueCoverDate.Value.ToString("MMM-yyyy").ToLower() + (this.IssueCoverId > 0 ? ("-" + this.IssueCoverId.ToString()) : "") + (page.HasValue ? ("/page/" + page.ToString()) : "");
		}
		public static string UrlStatic(bool includeDomain, DateTime date, int? page, int? coverId)
		{
			return (includeDomain ? "http://www.mixmag-online.com" : "") + "/" + date.ToString("MMM-yyyy").ToLower() + (coverId.HasValue && coverId.Value > 0 ? ("-" + coverId.Value.ToString()) : "") + (page.HasValue ? ("/page/" + page.ToString()) : "");
		}

		public string GetHtml(int? defaultPage, bool includeFullUrl)
		{
			string s = "";
			s += @"
<div style=""height:270px; clear:both;"">

<div style=""width:200px; height:260px; float:left;"">
<a href=""" + Url(includeFullUrl, defaultPage) + @""" target=""_blank""><img src=""" + this.CoverImageUrl + @""" width=""194"" height=""254"" border=""0"" /></a>
</div>

<div style=""width:400px; height:254px; float:left; padding-left:5px;"">
	<h2 style=""margin-top:0px;margin-bottom:5px;"">	
		<a href=""" + Url(includeFullUrl, defaultPage) + @""" target=""_blank"">" + this.IssueCoverDate.Value.ToString("MMMM yyyy") + @"</a>
	</h2>
	" + (IssueNote != null && IssueNote.Length > 0 ? (@"<p style=""font-size:10px;margin-top:2px;margin-bottom:2px;"">" + IssueNote.ToString() + "</p>") : "") + @"
	<p style=""margin-top:5px;margin-bottom:5px;"">
		In this issue:
	</p>";
			foreach (Bobs.MixmagIssue.ContentsItem item in this.Contents)
			{
				s += @"
	<p style=""margin-top:5px;margin-bottom:5px;"">
		<a href=""" + Url(includeFullUrl, item.PageNumber) + @""" target=""_blank"">" + item.Tagline + @" - page " + item.PageNumber.ToString() + @"</a>
	</p>";
			}
			s += @"
				
</div>
</div>";
			return s;
		}

		public static MixmagIssue GetIssueFromDate(DateTime d, int? coverId)
		{
			Query q = new Query();
			if (coverId.HasValue && coverId.Value > 0)
				q.QueryCondition = new And(new Q(MixmagIssue.Columns.IssueCoverDate, d), new Q(MixmagIssue.Columns.IssueCoverId, coverId.Value));
			else
				q.QueryCondition = new Q(MixmagIssue.Columns.IssueCoverDate, d);
			MixmagIssueSet ms = new MixmagIssueSet(q);
			if (ms.Count > 0)
				return ms[0];
			else
				return null;
		}


		public ContentsItem[] Contents
		{
			get
			{
				System.Collections.Generic.List<ContentsItem> list = new System.Collections.Generic.List<ContentsItem>();
				foreach (string data in ContentsData.Split('^'))
				{
					string[] parts = data.Split('\\');
					ContentsItem c = new ContentsItem();
					c.Tagline = parts[0];
					c.StatusMessage = parts[1];
					c.PageNumber = int.Parse(parts[2]);
					list.Add(c);
				}
				return list.ToArray();
			}
		}




		public class ContentsItem
		{
			public string Tagline;
			public string StatusMessage;
			public int PageNumber;
		}

	}
	#endregion

	#region MixmagRead
	/// <summary>
	/// Each read of an issue
	/// </summary>
	[Serializable]
	public partial class MixmagRead
	{

		#region Simple members
		/// <summary>
		/// Subscriber key
		/// </summary>
		public override int MixmagSubscriberK
		{
			get { return (int)this[MixmagRead.Columns.MixmagSubscriberK]; }
			set { this[MixmagRead.Columns.MixmagSubscriberK] = value; }
		}
		/// <summary>
		/// Issue key
		/// </summary>
		public override int MixmagIssueK
		{
			get { return (int)this[MixmagRead.Columns.MixmagIssueK]; }
			set { this[MixmagRead.Columns.MixmagIssueK] = value; }
		}
		/// <summary>
		/// Date time of the read
		/// </summary>
		public override DateTime? DateTimeRead
		{
			get { return (DateTime?)this[MixmagRead.Columns.DateTimeRead]; }
			set { this[MixmagRead.Columns.DateTimeRead] = value; }
		}
		/// <summary>
		/// Was a story published to Facebook?
		/// </summary>
		public override bool? StoryPublished
		{
			get { return (bool?)this[MixmagRead.Columns.StoryPublished]; }
			set { this[MixmagRead.Columns.StoryPublished] = value; }
		}
		/// <summary>
		/// Date/time the issue was last read.
		/// </summary>
		public override DateTime? DateTimeLastRead
		{
			get { return (DateTime?)this[MixmagRead.Columns.DateTimeLastRead]; }
			set { this[MixmagRead.Columns.DateTimeLastRead] = value; }
		}
		/// <summary>
		/// Total number of reads.
		/// </summary>
		public override int? TotalReads
		{
			get { return (int?)this[MixmagRead.Columns.TotalReads]; }
			set { this[MixmagRead.Columns.TotalReads] = value; }
		}
		/// <summary>
		/// Date/time that the last stream story was published.
		/// </summary>
		public override DateTime? DateTimeLastStoryPublished
		{
			get { return (DateTime?)this[MixmagRead.Columns.DateTimeLastStoryPublished]; }
			set { this[MixmagRead.Columns.DateTimeLastStoryPublished] = value; }
		}
		#endregion

	}
	#endregion

}
