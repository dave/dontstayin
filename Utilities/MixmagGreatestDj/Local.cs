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
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;


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

				if (!domain.Equals("greatest.dj"))
				{
					context.Response.Redirect(Vars.UrlScheme + "://greatest.dj" + context.Request.Url.PathAndQuery, true);
				}
			}
			#endregion

			
			string path = context.Request.Path.ToLower();
			int? MixmagGreatestDjK = null;
			bool isFacebookPage = false;

			try
			{
				if (path.Length > 0)
				{
					if (path.StartsWith("/fb1/"))
					{
						isFacebookPage = true;
						path = path.Substring(4);
					}
				}
			}
			catch { }


			try
			{
				if (path.Length > 0)
				{
					string name = path.Substring(1);
					MixmagGreatestDjSet djs = new MixmagGreatestDjSet(new Query(new Q(MixmagGreatestDj.Columns.UrlName, name)));
					if (djs.Count > 0)
						MixmagGreatestDjK = djs[0].K;
				}
			}
			catch
			{
			}

			if (path.StartsWith("/go/"))
			{



				HttpCookie cookie = context.Request.Cookies["MixmagGreatestAuth"];

				if (cookie == null)
					cookie = new HttpCookie("MixmagGreatestAuth");

				cookie.Value = "1";
				cookie.Expires = DateTime.Now.AddHours(1);

				context.Response.SetCookie(cookie);

				context.Response.Redirect("/");

				return null;
					//MixmagGreatest.Default3 page = null;
					//page = (MixmagGreatest.Default3)PageParser.GetCompiledPageInstance("/default3.aspx", context.Server.MapPath("/default3.aspx"), context);
					//page.IsFacebook = isFacebookPage;
					//return page;
			}
			else if (MixmagGreatestDjK.HasValue)
			{
				MixmagGreatest.Vote page = null;
				page = (MixmagGreatest.Vote)PageParser.GetCompiledPageInstance("/vote.aspx", context.Server.MapPath("/vote.aspx"), context);
				page.MixmagGreatestDjK = MixmagGreatestDjK.Value;
				return page;
			}
			else if (path == "/")
			{
				if (Vars.DevEnv)
				{
					//context.Response.Redirect("http://www.facebook.com/pages/MixmagTest/135045886539757?v=app_137494456291657");
				}
				else
				{
					if (DateTime.Now < new DateTime(2010, 9, 16, 10, 0, 0))
					{
						context.Response.Redirect("http://greatest.dj/wait.html");
					}
				}


				
				MixmagGreatest.Default3 page = null;
				page = (MixmagGreatest.Default3)PageParser.GetCompiledPageInstance("/default3.aspx", context.Server.MapPath("/default3.aspx"), context);
				return page;


			//	MixmagGreatest.Default1 page = null;
			//	page = (MixmagGreatest.Default1)PageParser.GetCompiledPageInstance("/default1.aspx", context.Server.MapPath("/default1.aspx"), context);
			//	page.IsFacebook = isFacebookPage;
			//	return page;

				//MixmagGreatest.default2 page = null;
				//page = (MixmagGreatest.default2)PageParser.GetCompiledPageInstance("/default2.aspx", context.Server.MapPath("/default2.aspx"), context);
				
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

		


		#region Signing...
		public static bool ValidateSignedRequest(string signedRequest, out string decodedPayload)
		{
			decodedPayload = null;
			string applicationSecret = Facebook.FacebookCommon.Common(Facebook.Apps.GreatestDj).Secret;

			string[] signedRequestArray = signedRequest.Split('.');
			string expectedSignature = signedRequestArray[0];
			string payload = signedRequestArray[signedRequestArray.Length - 1];

			var Hmac = SignWithHmac(UTF8Encoding.UTF8.GetBytes(payload), UTF8Encoding.UTF8.GetBytes(applicationSecret));
			var HmacBase64 = ToUrlBase64String(Hmac);

			bool result = (HmacBase64 == expectedSignature);
			if (result)
			{
				decodedPayload = UTF8Encoding.UTF8.GetString(FromBase64ForUrlString(payload));
			}
			return result;
		}

		private static byte[] FromBase64ForUrlString(string base64ForUrlInput)
		{
			int padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));

			StringBuilder result = new StringBuilder(base64ForUrlInput, base64ForUrlInput.Length + padChars);
			result.Append(String.Empty.PadRight(padChars, '='));

			result.Replace('-', '+');
			result.Replace('_', '/');

			return Convert.FromBase64String(result.ToString());
		}

		private static string ToUrlBase64String(byte[] Input)
		{
			return Convert.ToBase64String(Input).Replace("=", String.Empty)
												.Replace('+', '-')
												.Replace('/', '_');
		}

		private static byte[] SignWithHmac(byte[] dataToSign, byte[] keyBody)
		{
			using (var hmacAlgorithm = new HMACSHA256(keyBody))
			{
				hmacAlgorithm.ComputeHash(dataToSign);
				return hmacAlgorithm.Hash;
			}
		}
		#endregion

	}

	#endregion



	

}
