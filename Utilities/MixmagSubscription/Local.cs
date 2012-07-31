using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections;
using Cambro.Misc;
using Spotted;
using localNamespace = Spotted;
using Bobs;
using Bobs.Main;
using System.Text.RegularExpressions;


namespace Local
{

	#region MixmagHttpHandlerFactory
	public class MixmagHttpHandlerFactory : IHttpHandlerFactory
	{
		public virtual IHttpHandler GetHandler(HttpContext context, String requestType, String url, String pathTranslated)
		{

			#region Redirect if incoming on a different domain
			if (!Vars.DevEnv)
			{
				string domain = context.Request.Url.Host.ToLower();

				if (!domain.Equals("www.mixmag-online.com"))
				{
					context.Response.Redirect(Vars.UrlScheme + "://www.mixmag-online.com" + context.Request.Url.PathAndQuery, true);
				}
			}
			#endregion

			DateTime? issueDate = null;
			int? pageNumber = null;
			int? coverId = null;
			//throw new Exception("jjj - " + context.Request.Path);
			try
			{
				if (context.Request.Path.Length > 0 && context.Request.Path.Contains("-"))
				{
					string pathLastPart = "";
					string pathFirstPart = context.Request.Path.Substring(1);
					if (pathFirstPart.Contains("/"))
					{
						pathLastPart = pathFirstPart.Substring(pathFirstPart.IndexOf("/") + 1);
						pathFirstPart = pathFirstPart.Substring(0, pathFirstPart.IndexOf("/"));
					}

					string monthName = pathFirstPart.Substring(0, pathFirstPart.IndexOf("-"));
					int monthNumber = MonthNumber(monthName.ToLower());
					string yearAndMaybeCoverId = pathFirstPart.Substring(pathFirstPart.IndexOf("-") + 1);
					int year;
					if (yearAndMaybeCoverId.IndexOf("-") > -1)
					{
						year = int.Parse(yearAndMaybeCoverId.Substring(0, yearAndMaybeCoverId.IndexOf("-")));
						coverId = int.Parse(yearAndMaybeCoverId.Substring(yearAndMaybeCoverId.IndexOf("-") + 1));
						if (coverId == 0) coverId = null;
					}
					else
					{
						year = int.Parse(yearAndMaybeCoverId);
					}
					issueDate = new DateTime(year, monthNumber, 1);

					try
					{
						if (pathLastPart.Contains("/"))
						{
							string[] arr = pathLastPart.Split('/');
							if (arr[0] == "page")
								pageNumber = int.Parse(arr[1]);
						}
					}
					catch { }
				}
			}
			catch
			{
			}

			if (issueDate.HasValue || context.Request.Path == "/")
			{
				MixmagSubscription._Default page = null;
				page = (MixmagSubscription._Default)PageParser.GetCompiledPageInstance("/default.aspx", context.Server.MapPath("/default.aspx"), context);
				page.IssueDate = issueDate;
				page.CoverId = coverId;
				page.PageNumber = pageNumber;
				return page;
			}
			else
			{
				return PageParser.GetCompiledPageInstance(url, pathTranslated, context);
			}
		}

		public virtual void ReleaseHandler(IHttpHandler handler) { }

		#region YearRegex
		static Regex YearRegex
		{
			get
			{
				return new Regex("^[12][90][90123][0-9]$");
			}
		}
		#endregion
		#region MonthRegex
		Regex MonthRegex
		{
			get
			{
				return new Regex("^jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec$");
			}
		}
		#endregion
		#region MonthNumber
		public int MonthNumber(string urlName)
		{
			switch (urlName)
			{
				case "jan": return 1;
				case "feb": return 2;
				case "mar": return 3;
				case "apr": return 4;
				case "may": return 5;
				case "jun": return 6;
				case "jul": return 7;
				case "aug": return 8;
				case "sep": return 9;
				case "oct": return 10;
				case "nov": return 11;
				case "dec": return 12;
				default: return 0;
			}
		}
		#endregion

	}

	#endregion

	

}
