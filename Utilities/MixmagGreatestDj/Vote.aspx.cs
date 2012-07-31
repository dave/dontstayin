using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Web.Services;
using Facebook.Api;

namespace MixmagGreatest
{
	public partial class Vote : System.Web.UI.Page
	{

		#region Dj accessors
		public int MixmagGreatestDjK = 0;
		public MixmagGreatestDj NextDj
		{
			get
			{
				initDjs();
				return nextDj;
			}
		}
		public MixmagGreatestDj PrevDj
		{
			get
			{
				initDjs();
				return prevDj;
			}
		}
		public MixmagGreatestDj CurrentMixmagGreatestDj
		{
			get
			{
				initDjs();
				return currentMixmagGreatestDj;
			}
			set
			{
				MixmagGreatestDjK = value.K;
				currentMixmagGreatestDj = null;
			}
		}
		private MixmagGreatestDj currentMixmagGreatestDj;
		private MixmagGreatestDj nextDj;
		private MixmagGreatestDj prevDj;
		private void initDjs()
		{
			if (currentMixmagGreatestDj == null && MixmagGreatestDjK > 0)
			{
				currentMixmagGreatestDj = new MixmagGreatestDj(MixmagGreatestDjK);

				{
					MixmagGreatestDjSet djs1 = new MixmagGreatestDjSet(new Query(new Q(MixmagGreatestDj.Columns.K, QueryOperator.GreaterThan, MixmagGreatestDjK), new OrderBy(MixmagGreatestDj.Columns.K), 1));
					if (djs1.Count > 0)
						nextDj = djs1[0];
					else
					{
						MixmagGreatestDjSet djs2 = new MixmagGreatestDjSet(new Query(new Q(true), new OrderBy(MixmagGreatestDj.Columns.K), 1));
						nextDj = djs2[0];
					}
				}

				{
					MixmagGreatestDjSet djs1 = new MixmagGreatestDjSet(new Query(new Q(MixmagGreatestDj.Columns.K, QueryOperator.LessThan, MixmagGreatestDjK), new OrderBy(MixmagGreatestDj.Columns.K, OrderBy.OrderDirection.Descending), 1));
					if (djs1.Count > 0)
						prevDj = djs1[0];
					else
					{
						MixmagGreatestDjSet djs2 = new MixmagGreatestDjSet(new Query(new Q(true), new OrderBy(MixmagGreatestDj.Columns.K, OrderBy.OrderDirection.Descending), 1));
						prevDj = djs2[0];
					}
				}

			}
			

		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{

			//if (Request.Cookies["MixmagGreatestAuth"] != null && Request.Cookies["MixmagGreatestAuth"].Value != null && Request.Cookies["MixmagGreatestAuth"].Value.Length > 0)//has cookie
			//{
			//    HttpCookie cookie = Request.Cookies["MixmagGreatestAuth"];

			//    if (cookie.Value != "1")
			//    {
			//        redirectNow();
			//    }
			//}
			//else
			//{
			//    redirectNow();
			//}


			VideoPanel.Visible = CurrentMixmagGreatestDj.YoutubeId != null && CurrentMixmagGreatestDj.YoutubeId.Length > 0;

			//if (Request.QueryString["auth"] != null && Request.QueryString["auth"].Length > 0)
			//{

			//    string auth = Request.QueryString["auth"];

			//    try
			//    {
			//        CheckAuth(auth);
			//    }
			//    catch
			//    {
			//        redirectNow();
			//    }

			//    HttpCookie cookie = Request.Cookies["MixmagGreatestAuth"];

			//    if (cookie == null)
			//        cookie = new HttpCookie("MixmagGreatestAuth");

			//    cookie.Value = Request.QueryString["auth"];
			//    cookie.Expires = DateTime.Now.AddHours(1);

			//    Response.SetCookie(cookie);

			//    Response.Redirect("/" + CurrentMixmagGreatestDj.UrlName);
			//}
			//else if (Request.Cookies["MixmagGreatestAuth"] != null && Request.Cookies["MixmagGreatestAuth"].Value != null && Request.Cookies["MixmagGreatestAuth"].Value.Length > 0)//has cookie
			//{
			//    HttpCookie cookie = Request.Cookies["MixmagGreatestAuth"];

			//    try
			//    {
			//        CheckAuth(cookie.Value);
			//    }
			//    catch
			//    {
			//        redirectNow();
			//    }
			//}
			//else
			//{
			//    redirectNow();
			//}

		}

		void redirectNow()
		{
			if (Vars.DevEnv)
				Response.Redirect("http://www.facebook.com/pages/MixmagTest/135045886539757?v=app_137494456291657");
			else
				Response.Redirect("http://www.facebook.com/MixmagMagazine?v=app_4949752878");
		}

		public void CheckAuth(string auth)
		{
			if (auth == null || auth.Length == 0)
				throw new Exception("Invalid authorisation - no auth");

			string payload = "";
			bool valid = ValidateSignedRequest(auth, out payload);

			if (!valid)
				throw new Exception("Invalid authorisation - hash");

			JObject o = JObject.Parse(payload);

			long uid = long.Parse((string)o["user_id"]);
			long correctUsrId = Vars.DevEnv ? long.Parse("135045886539757") : long.Parse("12120996025");
			long correctUsrId1 = Vars.DevEnv ? long.Parse("12120996025") : long.Parse("135045886539757");

			if (uid != correctUsrId && uid != correctUsrId1)
				throw new Exception("Invalid authorisation - uid");
		}


		//[WebMethod]
		//public static bool VoteNow(string uidString, string sessionKey, string secret, string expires, string baseDomain, int mixmagGreatestDjK, string mixmagGreatestDjUrlName)
		//{
		//    long uid = long.Parse(uidString);

		//    FacebookHttpContext.Init(HttpContext.Current, uid.ToString(), sessionKey, secret, expires, baseDomain);

		//    MixmagGreatestDj dj = new MixmagGreatestDj(mixmagGreatestDjK);

		//    if (dj.UrlName != mixmagGreatestDjUrlName)
		//        throw new Exception("Error voting - try again.");

		//    long usr;
		//    bool emailPermission = false;
		//    bool publishPermission = false;
		//    string emailFromFacebook = "";
		//    using (var batch = Batch.Start(FacebookHttpContext.Current))
		//    {
		//        var usrR = FacebookHttpContext.Current.Users.GetLoggedInUser();
		//        var emailPermissionR = FacebookHttpContext.Current.Users.HasAppPermission("email");
		//        var publishPermissionR = FacebookHttpContext.Current.Users.HasAppPermission("publish_stream");

		//        batch.Complete();

		//        usr = usrR.Value;
		//        emailPermission = emailPermissionR.Value;
		//        publishPermission = publishPermissionR.Value;

		//        if (emailPermission)
		//        {
		//            emailFromFacebook = FacebookHttpContext.Current.Fql.Query<User>("SELECT email FROM user WHERE uid=" + usr.ToString()).Value.Email;
		//        }
		//    }

		//    if (usr != uid)
		//        throw new Exception("Error voting - try logging out from Facebook and trying again.");

		//    try
		//    {
		//        MixmagGreatestVote v = new MixmagGreatestVote(uid);
		//        MixmagGreatestDj votedDj = new MixmagGreatestDj(v.MixmagGreatestDjK);
		//        throw new Exception("Sorry, you can't vote twice. You already voted for " + votedDj.Name);
		//    }
		//    catch (BobNotFound)
		//    {
				
		//        MixmagGreatestVote v = new MixmagGreatestVote();
		//        v.FacebookUid = uid;
		//        v.MixmagGreatestDjK = mixmagGreatestDjK;
		//        v.DateTime = DateTime.Now;
		//        v.WallPostPermission = publishPermission;
		//        v.EmailPermission = emailPermission;
		//        v.DidWallPost = publishPermission;
		//        v.FacebookEmail = emailPermission ? emailFromFacebook : "";
		//        v.Update();

		//        if (publishPermission)
		//        {
		//            FacebookHttpContext.Current.Status.Set(dj.Name + " is my greatest DJ of all time. Click to vote for yours in the Mixmag poll: http://greatest.dj/");
		//        }

		//    }

		//    return true;
		//}

		//[WebMethod]
		//public static bool DisconnectNow(string uid, string sessionKey, string secret, string expires, string baseDomain)
		//{
		//    FacebookHttpContext.Init(HttpContext.Current, uid, sessionKey, secret, expires, baseDomain);

		//    Facebook.Desktop.FacebookDesktopContext.Current.Auth.RevokeAuthorization(long.Parse(uid));

		//    return true;
		//}

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


}
