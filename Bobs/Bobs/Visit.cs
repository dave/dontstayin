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

	#region Visit
	/// <summary>
	/// Stores information about a visit - guid from the cookie etc.
	/// </summary>
	[Serializable] 
	public partial class Visit
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Visit.Columns.K] as int? ?? 0; }
			set { this[Visit.Columns.K] = value; }
		}
		/// <summary>
		/// Guid stored in the cookie
		/// </summary>
		public override Guid Guid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Visit.Columns.Guid]); }
			set { this[Visit.Columns.Guid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Link to one user - the user was logged in. If nobody was logged in, it's set to 0.
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Visit.Columns.UsrK]; }
			set { usr = null; this[Visit.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Number of DsiPage impressions
		/// </summary>
		public override int Pages
		{
			get { return (int)this[Visit.Columns.Pages]; }
			set { this[Visit.Columns.Pages] = value; }
		}
		/// <summary>
		/// Number of comments posted
		/// </summary>
		public override int Comments
		{
			get { return (int)this[Visit.Columns.Comments]; }
			set { this[Visit.Columns.Comments] = value; }
		}
		/// <summary>
		/// Number of live chat messages posted
		/// </summary>
		public override int ChatMessages
		{
			get { return (int)this[Visit.Columns.ChatMessages]; }
			set { this[Visit.Columns.ChatMessages] = value; }
		}
		/// <summary>
		/// Total number of hits, including live chat impressions
		/// </summary>
		public override int Hits
		{
			get { return (int)this[Visit.Columns.Hits]; }
			set { this[Visit.Columns.Hits] = value; }
		}
		/// <summary>
		/// Number of photos viewed (quick browser and photo pages)
		/// </summary>
		public override int Photos
		{
			get { return (int)this[Visit.Columns.Photos]; }
			set { this[Visit.Columns.Photos] = value; }
		}
		/// <summary>
		/// Total number of top-banner clicks
		/// </summary>
		public override int TopBannerClicks
		{
			get { return (int)this[Visit.Columns.TopBannerClicks]; }
			set { this[Visit.Columns.TopBannerClicks] = value; }
		}
		/// <summary>
		/// Total number of hotbox clicks
		/// </summary>
		public override int HotboxClicks
		{
			get { return (int)this[Visit.Columns.HotboxClicks]; }
			set { this[Visit.Columns.HotboxClicks] = value; }
		}
		/// <summary>
		/// Total number of photo-banner clicks
		/// </summary>
		public override int PhotoBannerClicks
		{
			get { return (int)this[Visit.Columns.PhotoBannerClicks]; }
			set { this[Visit.Columns.PhotoBannerClicks] = value; }
		}
		/// <summary>
		/// Date/time the first visit started
		/// </summary>
		public override DateTime DateTimeStart
		{
			get { return (DateTime)this[Visit.Columns.DateTimeStart]; }
			set { this[Visit.Columns.DateTimeStart] = value; }
		}
		/// <summary>
		/// Date/time the last hit was received (if next hit is within 15 mins, they are counted as the same visit)
		/// </summary>
		public override DateTime DateTimeLast
		{
			get { return (DateTime)this[Visit.Columns.DateTimeLast]; }
			set { this[Visit.Columns.DateTimeLast] = value; }
		}
		/// <summary>
		/// IP address of the first hit
		/// </summary>
		public override string IpAddress
		{
			get { return (string)this[Visit.Columns.IpAddress]; }
			set { this[Visit.Columns.IpAddress] = value; }
		}
		/// <summary>
		/// Is the guid newly issued? (if so, it's unique)
		/// </summary>
		public override bool IsNewGuid
		{
			get { return (bool)this[Visit.Columns.IsNewGuid]; }
			set { this[Visit.Columns.IsNewGuid] = value; }
		}
		/// <summary>
		/// Country looked up by IP address
		/// </summary>
		public override int CountryK
		{
			get { return (int)this[Visit.Columns.CountryK]; }
			set { this[Visit.Columns.CountryK] = value; }
		}
		/// <summary>
		/// Is this visit from an external source?
		/// </summary>
		public override bool IsFromExternal
		{
			get { return (bool)this[Visit.Columns.IsFromExternal]; }
			set { this[Visit.Columns.IsFromExternal] = value; }
		}
		/// <summary>
		/// External source tag
		/// </summary>
		public override string ExternalTag
		{
			get { return (string)this[Visit.Columns.ExternalTag]; }
			set { this[Visit.Columns.ExternalTag] = value; }
		}
		/// <summary>
		/// Domain from which this request originated
		/// </summary>
		public override int DomainK
		{
			get { return (int)this[Visit.Columns.DomainK]; }
			set { this[Visit.Columns.DomainK] = value; }
		}
		/// <summary>
		/// Is this visit from a Crawler bot
		/// </summary>
		public override bool IsCrawler
		{
			get { return (bool)this[Visit.Columns.IsCrawler]; }
			set { this[Visit.Columns.IsCrawler] = value; }
		}
		/// <summary>
		/// The browser's UserAgent string
		/// </summary>
		public override string UserAgent
		{
			get { return (string)this[Visit.Columns.UserAgent]; }
			set { this[Visit.Columns.UserAgent] = value; }
		}
		#endregion

		#region Current
		/// <summary>
		/// This is the current visit object. In rare circumstances (e.g. in DbChatServer.aspx) it might be null.
		/// This is designed to be READ ONLY - do NOT update it!
		/// </summary>
		public static Visit Current
		{
			get
			{
				if (HttpContext.Current.Items["CurrentVisit"] != null)
					return (Visit)HttpContext.Current.Items["CurrentVisit"];
				else
					return null;
			}
		}
		#endregion

		#region HasCurrent
		/// <summary>
		/// Use this bool to tell if we have a current visit object.
		/// </summary>
		public static bool HasCurrent
		{
			get
			{
				return HttpContext.Current != null && HttpContext.Current.Items["CurrentVisit"] != null;
			}
		}
		#endregion

		#region Country
		public Country Country
		{
			get
			{
				if (country == null)
					country = new Country(CountryK);
				return country;
			}
		}
		Country country;
		#endregion

		#region Increment
		public static void Increment(int VisitK, int Pages, int Photos)
		{
			if (Pages > 0 || Photos > 0)
			{
				Update u = new Update();
				u.Table = TablesEnum.Visit;
				u.Where = new Q(Visit.Columns.K, VisitK);

				u.Changes.Add(new Assign(Visit.Columns.DateTimeLast, DateTime.Now));

				if (Pages > 0)
					u.Changes.Add(new Assign.Addition(Visit.Columns.Pages, Pages));

				if (Photos > 0)
					u.Changes.Add(new Assign.Addition(Visit.Columns.Photos, Photos));

				try
				{
					u.Run();
				}
				catch
				{
					Log.Increment(Log.Items.VisitWriteFailures);
				}
			}
		}
		#endregion

		#region Links to Bobs

		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr==null && UsrK>0)
					usr = new Usr(UsrK, this, Visit.Columns.UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion
		
		#endregion

	}
	#endregion

}

