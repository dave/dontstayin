using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Facebook
{
	public enum Apps
	{
		Dsi = 1,
		MixmagOnline = 2,
		MixmagVote = 3,
		MixmagGreatest = 4
	}

	public class FacebookCommon
	{

		//private static readonly FacebookApplicationElement _common = FacebookSection.Current.Applications.GetCurrent();
		private static readonly Dictionary<string, FacebookApplicationElement> _common = FacebookSection.Current.Applications.GetAll();

		private FacebookCommon() { }

		public static FacebookApplicationElement Common(Facebook.Apps app)
		{
			return _common[app.ToString()];
		}

		public static JObject DecodeFacebookRequest(string signed_request, Facebook.Apps app)
		{
			string secret = Facebook.FacebookCommon.Common(app).Secret;
			var facebookData = new Dictionary<string, string>();

			var requestArray = signed_request.Split('.');

			var sig = Base64_Url_Decode(requestArray[0]);

			var dataString = Base64_Url_Decode(requestArray[1]);

			var data = JObject.Parse(dataString);

			var algo = data["algorithm"].ToString().Replace("\"", "");

			if (algo != "HMAC-SHA256")
			{
				return null;
			}

			var hmacsha256 = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(secret));
			hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(requestArray[1]));

			var expected_sig = Encoding.UTF8.GetString(hmacsha256.Hash);

			if (sig != expected_sig)
			{
				return null;
			}

			return data;
		}

		public static string Base64_Url_Decode(string input)
		{
			var fixedString = string.Empty;
			var fixedDashString = input.Replace('-', '+');
			var fixedUnderscoreString = fixedDashString.Replace('_', '/');
			if (fixedUnderscoreString.Length % 4 != 0)
			{
				fixedString = String.Format("{0}", fixedUnderscoreString);
				int paddingCount = fixedString.Length % 4;
				while (paddingCount % 4 != 0)
				{
					fixedString += '=';
					paddingCount++;
				}
			}
			else
			{
				fixedString = fixedUnderscoreString;
			}
			var inputBytes = Convert.FromBase64String(fixedString);
			return Encoding.UTF8.GetString(inputBytes);
		}

	}

}
