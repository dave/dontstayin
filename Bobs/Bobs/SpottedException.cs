using System;
using System.Collections.Generic;
using System.Text;
using System.Web;


namespace Bobs
{
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct Bob class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.

	#region SpottedException
	/// <summary>
	/// Log of Exceptions thrown from the Spotted website
	/// </summary>
	[Serializable]
	public partial class SpottedException
	{

		#region Simple members
		/// <summary>
		/// K of the Exception
		/// </summary>
		public override int K
		{
			get { return this[SpottedException.Columns.K] as int? ?? 0; }
			set { this[SpottedException.Columns.K] = value; }
		}
		/// <summary>
		/// K of the parent Exception, when this is an InnerException
		/// </summary>
		public override int ParentK
		{
			get { return (int)this[SpottedException.Columns.ParentK]; }
			set { this[SpottedException.Columns.ParentK] = value; }
		}
		/// <summary>
		/// Time of logging Exception
		/// </summary>
		public override DateTime ExceptionDateTime
		{
			get { return (DateTime)this[SpottedException.Columns.ExceptionDateTime]; }
			set { this[SpottedException.Columns.ExceptionDateTime] = value; }
		}
		/// <summary>
		/// The type of Exception
		/// </summary>
		public override string ExceptionType
		{
			get { return (string)this[SpottedException.Columns.ExceptionType]; }
			set { this[SpottedException.Columns.ExceptionType] = value; }
		}
		/// <summary>
		/// Exception message
		/// </summary>
		public override string Message
		{
			get { return (string)this[SpottedException.Columns.Message]; }
			set { this[SpottedException.Columns.Message] = value; }
		}
		/// <summary>
		/// Exception source
		/// </summary>
		public override string Source
		{
			get { return (string)this[SpottedException.Columns.Source]; }
			set { this[SpottedException.Columns.Source] = value; }
		}
		/// <summary>
		/// Exception stack trace
		/// </summary>
		public override string StackTrace
		{
			get { return (string)this[SpottedException.Columns.StackTrace]; }
			set { this[SpottedException.Columns.StackTrace] = value; }
		}
		/// <summary>
		/// The Url which caused the Exception
		/// </summary>
		public override string Url
		{
			get { return (string)this[SpottedException.Columns.Url]; }
			set { this[SpottedException.Columns.Url] = value; }
		}
		/// <summary>
		/// Path of the master container page
		/// </summary>
		public override string MasterPath
		{
			get { return (string)this[SpottedException.Columns.MasterPath]; }
			set { this[SpottedException.Columns.MasterPath] = value; }
		}
		/// <summary>
		/// Page path
		/// </summary>
		public override string PagePath
		{
			get { return (string)this[SpottedException.Columns.PagePath]; }
			set { this[SpottedException.Columns.PagePath] = value; }
		}
		/// <summary>
		/// Current page filter
		/// </summary>
		public override string CurrentFilter
		{
			get { return (string)this[SpottedException.Columns.CurrentFilter]; }
			set { this[SpottedException.Columns.CurrentFilter] = value; }
		}
		/// <summary>
		/// K of object referenced in current filter
		/// </summary>
		public override int ObjectFilterK
		{
			get { return (int)this[SpottedException.Columns.ObjectFilterK]; }
			set { this[SpottedException.Columns.ObjectFilterK] = value; }
		}
		/// <summary>
		/// Type of object referenced in current filter
		/// </summary>
		public override Model.Entities.ObjectType ObjectFilterType
		{
			get { return (Model.Entities.ObjectType)this[SpottedException.Columns.ObjectFilterType]; }
			set { this[SpottedException.Columns.ObjectFilterType] = value; }
		}
		/// <summary>
		/// Machine name of the server on which this Exception was thrown
		/// </summary>
		public override string MachineName
		{
			get { return (string)this[SpottedException.Columns.MachineName]; }
			set { this[SpottedException.Columns.MachineName] = value; }
		}
		/// <summary>
		/// K of current Usr
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[SpottedException.Columns.UsrK]; }
			set { this[SpottedException.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Current browser guid
		/// </summary>
		public override Guid DsiGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[SpottedException.Columns.DsiGuid]); }
			set { this[SpottedException.Columns.DsiGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Contents of browser cookies
		/// </summary>
		public override string Cookies
		{
			get { return (string)this[SpottedException.Columns.Cookies]; }
			set { this[SpottedException.Columns.Cookies] = value; }
		}
		/// <summary>
		/// Post data of Request
		/// </summary>
		public override string PostData
		{
			get { return (string)this[SpottedException.Columns.PostData]; }
			set { this[SpottedException.Columns.PostData] = value; }
		}
		/// <summary>
		/// User's IP address
		/// </summary>
		public override string IpAddress
		{
			get { return (string)this[SpottedException.Columns.IpAddress]; }
			set { this[SpottedException.Columns.IpAddress] = value; }
		}
		/// <summary>
		/// Now according to current Common.Time context
		/// </summary>
		public override DateTime CommonTimeNow
		{
			get { return (DateTime)this[SpottedException.Columns.CommonTimeNow]; }
			set { this[SpottedException.Columns.CommonTimeNow] = value; }
		}
		#endregion



		public bool ShowMessageToUsrs
		{
			get
			{
				return ExceptionType == typeof(Bobs.DsiUserFriendlyException).ToString() ||
					ExceptionType == typeof(Bobs.BobNotFound).ToString();
			}
		}

		public static bool ExceptionShouldBeLogged(Exception exception)
		{
			return !(exception is MalformedUrlException);
		}


		public static SpottedException TryToSaveExceptionAndChildExceptions(Exception exception)
		{
			return TryToSaveExceptionAndChildExceptions(exception, null, null, null, "", "", "", 0, null);
		}

		public static SpottedException TryToSaveExceptionAndChildExceptions(Exception exception, HttpContext currentHttpContext, Usr currentUsr, Visit currentVisit, string currentFilter, string masterPath, string pagePath, int objectFilterK, Model.Entities.ObjectType? objectFilterType)
		{
			// attempt to save detail to database
			string url = "", ipAddress = "", cookieXml = "", postDataXml = "";

			if (currentHttpContext != null)
			{
				url = currentHttpContext.Request.Url.ToString();
				ipAddress = currentHttpContext.Request.UserHostAddress;
				cookieXml = Utilities.GetCookieDataAsXml(currentHttpContext.Request.Cookies);
				postDataXml = Utilities.GetPostDataAsXml(currentHttpContext.Request.Form);
			}

			int? usrK = null;
			if (currentUsr != null)
			{
				usrK = currentUsr.K;
			}
			Guid? browserGuid = null;
			if (currentVisit != null)
			{
				browserGuid = currentVisit.Guid;
			}

			return TryToSaveExceptionAndChildExceptions(exception, url, currentFilter, masterPath, pagePath, objectFilterK, objectFilterType, cookieXml, postDataXml, usrK, browserGuid, ipAddress);
		}

		public static SpottedException TryToSaveExceptionAndChildExceptions(
			Exception exception,
			string url, string currentFilter, string masterPath, string pagePath, int objectFilterK, Model.Entities.ObjectType? objectFilterType,
			string cookies, string postData, int? usrK, Guid? browserGuid, string ipAddress)
		{
			// discard outer wrapped HttpUnhandledException, if one exists
			while (exception != null && exception is System.Web.HttpUnhandledException)
			{
				exception = exception.InnerException;
			}

			SpottedException spottedEx = TryToSaveException(exception, url, currentFilter, masterPath, pagePath, objectFilterK, objectFilterType, cookies, postData, usrK, browserGuid, ipAddress, null);

			if (spottedEx.K > 0)
			{
				int parentK = spottedEx.K;
				// save any Inner Exceptions
				for (exception = exception.InnerException; exception != null; exception = exception.InnerException)
				{
					SpottedException childException = TryToSaveException(exception, url, currentFilter, masterPath, pagePath, objectFilterK, objectFilterType, cookies, postData, usrK, browserGuid, ipAddress, parentK);
					if (childException != null)
					{
						parentK = childException.K;
					}
				}
			}

			return spottedEx;
		}
		private static SpottedException TryToSaveException(
			Exception exception,
			string url, string currentFilter, string masterPath, string pagePath, int objectFilterK, Model.Entities.ObjectType? objectFilterType,
			string cookies, string postData, int? usrK, Guid? browserGuid, string ipAddress,
			int? parentK)
		{
			Bobs.SpottedException spottedEx = new SpottedException();
			if (parentK.HasValue) spottedEx.ParentK = parentK.Value;
			spottedEx.ExceptionDateTime = DateTime.Now;
			spottedEx.CommonTimeNow = Common.Time.Now;
			spottedEx.ExceptionType = exception.GetType().ToString();
			spottedEx.Message = exception.Message.Truncate(4000);
			spottedEx.Source = exception.Source.Truncate(50);
			spottedEx.StackTrace = exception.StackTrace.Truncate(4000);
			spottedEx.MachineName = Common.Properties.MachineName;
			spottedEx.Url = url.Truncate(150);

			spottedEx.CurrentFilter = currentFilter.Truncate(150);
			spottedEx.MasterPath = masterPath.Truncate(50);
			spottedEx.PagePath = pagePath.Truncate(50);
			spottedEx.ObjectFilterK = objectFilterK;
			if (objectFilterType.HasValue) spottedEx.ObjectFilterType = objectFilterType.Value;

			spottedEx.Cookies = cookies;
			spottedEx.PostData = postData;

			if (usrK.HasValue) spottedEx.UsrK = usrK.Value;
			if (browserGuid.HasValue) spottedEx.DsiGuid = browserGuid.Value;
			spottedEx.IpAddress = ipAddress;

			if (SpottedException.ExceptionShouldBeLogged(exception))
			{
				try
				{
					spottedEx.Update();
				}
				catch
				{
					// ignore. it may be database access issues which are causing the Exceptions!
				}
			}
			return spottedEx;
		}

	}
	#endregion

}
