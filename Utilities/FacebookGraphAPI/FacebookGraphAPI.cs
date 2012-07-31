/// Copyright 2010 Hernan Amiune (hernan.amiune.com)
/// Licensed under MIT license:
/// http://www.opensource.org/licenses/mit-license.php
/// 
/// Requires Newtonsoft.Json.Linq.JObject
/// http://james.newtonking.com/projects/json-net.aspx
/// Based on the Official Python client library for the Facebook Platform
/// http://github.com/facebook/python-sdk/
/// 
/// C# client library for the Facebook Platform
/// 
/// This client library is designed to support the Graph API and the official
/// Facebook JavaScript SDK, which is the canonical way to implement
/// Facebook authentication. Read more about the Graph API at
/// http://developers.facebook.com/docs/api. You can download the Facebook
/// JavaScript SDK at http://github.com/facebook/connect-js/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.IO;

namespace Facebook
{

    /// <summary>
    /// A client for the Facebook Graph API.
    /// 
    /// See http://developers.facebook.com/docs/api for complete documentation
    /// for the API.
    /// 
    /// The Graph API is made up of the objects in Facebook (e.g., people, pages,
    /// events, photos) and the connections between them (e.g., friends,
    /// photo tags, and event RSVPs). This client provides access to those
    /// primitive types in a generic way. For example, given an OAuth access
    /// token, this will fetch the profile of the active user and the list
    /// of the user's friends:
    /// 
    ///    var facebook = new FacebookGraphAPI(args["access_token"]);
    ///    var user = facebook.GetObject("me", null);
    ///    var friends = facebook.GetConnections("me", "friends", null);
    /// 
    /// You can see a list of all of the objects and connections supported
    /// by the API at http://developers.facebook.com/docs/reference/api/.
    /// 
    /// You can obtain an access token via OAuth or by using the Facebook
    /// JavaScript SDK. See http://developers.facebook.com/docs/authentication/
    /// for details.
    /// 
    /// If you are using the JavaScript SDK, you can use the
    /// get_user_from_cookie() method below to get the OAuth access token
    /// for the active user from the cookie saved by the SDK.
    /// </summary>
    public class FacebookGraphAPI
    {

		public bool LoggedIn = false;
		public long Uid = 0;
        public string AccessToken = null;

		public static FacebookGraphAPI GetPageApi(Facebook.Apps app)
		{
			//string accessToken = Post("https://graph.facebook.com/oauth/access_token", "type", "client_cred", "client_id", FacebookCommon.Common.AppId.ToString(), "client_secret", FacebookCommon.Common.Secret);

			return new FacebookGraphAPI(app, FacebookCommon.Common(app).PageId, FacebookCommon.Common(app).PageToken);
		}
		static string Post(params string[] form)
		{
			string s = "";
			for (int i = 0; i < form.Length - 1; i += 2)
				s += (s.Length == 0 ? "" : "&") + HttpUtility.UrlEncode(form[i]) + "=" + HttpUtility.UrlEncode(form[i + 1]);

			HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("https://graph.facebook.com/oauth/access_token");
			myRequest.Method = "POST";
			myRequest.ContentLength = s.Length;
			myRequest.ContentType = "application/x-www-form-urlencoded";
			myRequest.CookieContainer = new CookieContainer();
			StreamWriter swRequestWriter = new StreamWriter(myRequest.GetRequestStream());
			swRequestWriter.Write(s);
			swRequestWriter.Close();
			WebResponse myResponse = myRequest.GetResponse();
			StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
			string result = sr.ReadToEnd();
			sr.Close();
			myResponse.Close();
			return result;
		}

		public FacebookGraphAPI(Facebook.Apps app, long uid, string accessToken)
		{

			Dictionary<string, string> args = null;

			try
			{
				args = FacebookGraphAPI.GetUserFromCookie(app, HttpContext.Current.Request.Cookies);
			}
			catch { }

			if (args != null && args["uid"] != null && args["uid"].Length > 0 && args["uid"] != "0" && long.Parse(args["uid"]) == uid)
			{
				LoggedIn = true;
				Uid = long.Parse(args["uid"]);
				AccessToken = args["access_token"];
			}
			else
			{
				LoggedIn = false;
				Uid = uid;
				AccessToken = accessToken;
			}
		}
		public FacebookGraphAPI(Facebook.Apps app)
		{
			Dictionary<string, string> args = null;

			try
			{
				args = FacebookGraphAPI.GetUserFromCookie(app, HttpContext.Current.Request.Cookies);
			}
			catch { }

			if (args != null && args["uid"] != null && args["uid"].Length > 0 && args["uid"] != "0")
			{
				LoggedIn = true;
				Uid = long.Parse(args["uid"]);
				AccessToken = args["access_token"];
			}
		}

		//public FacebookGraphAPI(string accessToken)
		//{
		//    this.accessToken = accessToken;
		//}

        /// <summary>
        /// Fetchs the given object from the graph.
        /// </summary>
        /// <param name="id">Id of the object to fetch</param>
        /// <param name="args">List of arguments</param>
        /// <returns>The required object</returns>
        public JObject GetObject(string id, Dictionary<string, string> args)
        {
            return Request(id, args, null);
        }

        /// <summary>
        /// Fetchs all of the given object from the graph.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="ids">Ids of the objects to return</param>
        /// <returns>
        /// A map from ID to object. If any of the IDs are invalid, an exception is raised
        /// </returns>
        public JObject GetObjects(Dictionary<string, string> args, params string[] ids)
        {
            string joinedIds = "";
            for (int i = 0; i < ids.Length; i++) if (i == 0) joinedIds += ids[i]; else joinedIds += "," + ids[i];
            if (args == null) args = new Dictionary<string, string>();
            args["ids"] = joinedIds;

            return Request("", args, null);
        }

        /// <summary>
        /// Fetchs the connections for given object.
        /// </summary>
        /// <param name="id">Id of the object to fetch</param>
        /// <param name="connectionName">Name of the connection</param>
        /// <param name="args">List of arguments</param>
        /// <returns>A JObject containing the required connections</returns>
        public JObject GetConnections(string id, string connectionName, Dictionary<string, string> args)
        {
            return Request(id + "/" + connectionName, args, null);
        }

        /// <summary>
        /// Writes the given object to the graph, connected to the given parent.
        /// 
        /// For example,
        /// 
        ///     var data = new Dictionary<string, string>();
        ///     data.Add("message", "Hello, world");
        ///     facebook.PutObject("me", "feed", data);
        /// 
        /// writes "Hello, world" to the active user's wall.
        /// 
        /// See http://developers.facebook.com/docs/api#publishing for all of
        /// the supported writeable objects.
        /// 
        /// Most write operations require extended permissions. For example,
        /// publishing wall posts requires the "publish_stream" permission. See
        /// http://developers.facebook.com/docs/authentication/ for details about
        /// extended permissions.
        /// </summary>
        /// <param name="parentObject">The parent object</param>
        /// <param name="connectionName">The connection name</param>
        /// <param name="data">Post data</param>
        /// <returns>A JObject with the result of the operation</returns>
        public JObject PutObject(string parentObject, string connectionName, Dictionary<string, object> data)
        {
            if (this.AccessToken == null) throw new FacebookGraphAPIException("Authentication", "Access Token Required");
            return Request(parentObject + "/" + connectionName, null, data);
        }


        /// <summary>
        /// Writes a wall post to current user wall.
        /// 
        /// We default to writing to the authenticated user's wall if no
        /// profile_id is specified.
        /// 
        /// attachment adds a structured attachment to the status message being
        /// posted to the Wall. It should be a dictionary of the form:
        /// 
        ///     {"name": "Link name"
        ///      "link": "http://www.example.com/",
        ///      "caption": "{*actor*} posted a new review",
        ///      "description": "This is a longer description of the attachment",
        ///      "picture": "http://www.example.com/thumbnail.jpg"}
        /// </summary>
        /// <param name="message">The message to put on the wall</param>
        /// <param name="attachment">Optional attachment for the message</param>
        /// <returns>A JObject with the result of the operation</returns>
        public JObject PutWallPost(string message, Dictionary<string, object> attachment)
        {
			if (attachment == null) attachment = new Dictionary<string, object>();
            attachment.Add("message", message);
            return PutObject("me", "feed", attachment);
        }


        /// <summary>
        /// Writes a wall post to the given profile's wall.
        /// 
        /// We default to writing to the authenticated user's wall if no
        /// profile_id is specified.
        /// 
        /// attachment adds a structured attachment to the status message being
        /// posted to the Wall. It should be a dictionary of the form:
        /// 
        ///     {"name": "Link name"
        ///      "link": "http://www.example.com/",
        ///      "caption": "{*actor*} posted a new review",
        ///      "description": "This is a longer description of the attachment",
        ///      "picture": "http://www.example.com/thumbnail.jpg"}
        /// </summary>
        /// <param name="message">The message to put on the wall</param>
        /// <param name="attachment">Optional attachment for the message</param>
        /// <param name="profileId">The profile where the message is goint to be put</param>
        /// <returns>A JObject with the result of the operation</returns>
		public JObject PutWallPost(string message, Dictionary<string, object> attachment, string profileId)
        {
			if (attachment == null) attachment = new Dictionary<string, object>();
            attachment.Add("message", message);
            return PutObject(profileId, "feed", attachment);
        }

        /// <summary>
        /// Writes the given comment on the given post.
        /// </summary>
        /// <param name="objectId">Id of the object</param>
        /// <param name="message">Message</param>
        /// <returns>A JObject with the result of the operation</returns>
        public JObject PutComment(string objectId, string message)
        {
			var args = new Dictionary<string, object>();
            args.Add("message", message);
            return PutObject(objectId, "comments", args);
        }

        /// <summary>
        /// Likes the given post.
        /// </summary>
        /// <param name="objectId">Id of the object to be like</param>
        /// <returns>A JObject with the result of the operation</returns>
        public JObject PutLike(string objectId)
        {
            return PutObject(objectId, "likes", null);
        }

        /// <summary>
        /// Deletes the object with the given ID from the graph.
        /// </summary>
        /// <param name="id">Id of the object to delete</param>
        /// <returns>A JObject with the result of the operation</returns>
        public JObject DeleteObject(string id)
        {
			var postArgs = new Dictionary<string, object>();
            postArgs.Add("method", "delete");
            return Request(id, null, postArgs);
        }

		#region removed
		//public JObject PublishPhoto(string parentObject, string connectionName, byte[] photo, string message)
		//{
		//    if (this.AccessToken == null)
		//        throw new FacebookGraphAPIException("Authentication", "Access Token Required");

		//    string path = parentObject + "/" + connectionName;

		//    Dictionary<string, string> args = new Dictionary<string, string>();

		//    if (this.AccessToken != null)
		//    {
		//        args["access_token"] = this.AccessToken;
		//    }

		//    string postUrl = "https://graph.facebook.com/" + path + "?" + EncodeDictionary(args);


		//    //Set up basic variables for constructing the multipart/form-data data
		//    string newline = "\r\n";
		//    string boundary = DateTime.Now.Ticks.ToString("x");
		//    string data = "";

		//    //Construct data
		//    data += "--" + boundary + newline;
		//    data += "Content-Disposition: form-data; name=\"message\"" + newline + newline;
		//    data += message + newline;

		//    data += "--" + boundary + newline;
		//    data += "Content-Disposition: form-data; filename=\"test.jpg\"" + newline;
		//    data += "Content-Type: image/jpeg" + newline + newline;

		//    string ending = newline + "--" + boundary + "--" + newline;

		//    //Convert data to byte[] array
		//    MemoryStream finaldatastream = new MemoryStream();
		//    byte[] databytes = Encoding.UTF8.GetBytes(data);
		//    byte[] endingbytes = Encoding.UTF8.GetBytes(ending);
		//    finaldatastream.Write(databytes, 0, databytes.Length);
		//    finaldatastream.Write(photo, 0, photo.Length);
		//    finaldatastream.Write(endingbytes, 0, endingbytes.Length);
		//    byte[] finaldatabytes = finaldatastream.ToArray();
		//    finaldatastream.Dispose();

		//    //Make the request

		//    WebRequest request = HttpWebRequest.Create(postUrl);
		//    request.ContentType = "multipart/form-data; boundary=" + boundary;
		//    request.ContentLength = finaldatabytes.Length;
		//    request.Method = "POST";
		//    using (Stream RStream = request.GetRequestStream())
		//    {
		//        RStream.Write(finaldatabytes, 0, finaldatabytes.Length);
		//    }
		//    WebResponse WR = request.GetResponse();
		//    string reply = "";
		//    using (StreamReader sr = new StreamReader(WR.GetResponseStream()))
		//    {
		//        reply = sr.ReadToEnd();
		//        sr.Close();
		//    }
			

		//    JObject jo = JObject.Parse(reply);
		//    if (jo["error"] != null)
		//        throw new FacebookGraphAPIException(jo["error"]["type"].ToString(),
		//            jo["error"]["message"].ToString());

		//    return jo;
		//}
		#endregion




		/// <summary>
        /// Fetches the given path in the Graph API.
        /// 
        /// Translates args to a valid query string. If post_args is given,
        /// sends a POST request to the given path with the given arguments.
        /// </summary>
        /// <param name="path">The path where the request is to be send</param>
        /// <param name="args">The Query arguments</param>
        /// <param name="postArgs">The POST arguments</param>
        /// <returns>A JObject of the facebook response</returns>
		private JObject Request(string path, Dictionary<string, string> args, Dictionary<string, object> postArgs)
        {
            if (args == null) 
				args = new Dictionary<string, string>();
            
			if (this.AccessToken != null)
                args["access_token"] = this.AccessToken;
            


			string reply = "";
			string postUrl = "https://graph.facebook.com/" + path + "?" + EncodeDictionary(args);
			if (postArgs != null)
			{
				bool hasByteArray = false;
				foreach (object ob in postArgs.Values)
				{
					if (ob is byte[])
					{
						hasByteArray = true;
						break;
					}
				}
				if (hasByteArray)
				{
					using (HttpWebResponse webResponse = WebHelpers.MultipartFormDataPost(postUrl, "dontstayin", postArgs))
					{
						StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
						reply = responseReader.ReadToEnd();
					}
				}
				else
				{
					using (WebClient wc = new WebClient())
					{
						wc.Encoding = System.Text.Encoding.UTF8;
						reply = wc.UploadString(postUrl, "POST", EncodeDictionary(postArgs));
					}
				}
			}
			else
			{
				using (WebClient wc = new WebClient())
				{
					wc.Encoding = System.Text.Encoding.UTF8;
					reply = wc.DownloadString(postUrl);
				}
			}

			//string postData = null;
			//if (postArgs != null) postData = EncodeDictionary(postArgs);

			//string reply = "";
			//using (WebClient wc = new WebClient())
			//{
			//    wc.Encoding = System.Text.Encoding.UTF8;
			//    if (postArgs == null) reply = wc.DownloadString("https://graph.facebook.com/" + path + "?" + EncodeDictionary(args));
			//    else reply = wc.UploadString("https://graph.facebook.com/" + path + "?" + EncodeDictionary(args), "POST", postData);
			//}

            JObject jo = JObject.Parse(reply);
            if (jo["error"] != null)
                throw new FacebookGraphAPIException(jo["error"]["type"].ToString(),
                    jo["error"]["message"].ToString());

            return jo;
        }


		//public JObject SendMultipart(string postUrl, string connectionName, byte[] photo, string message)
		//{
		//    if (this.AccessToken == null)
		//        throw new FacebookGraphAPIException("Authentication", "Access Token Required");

		//    string path = parentObject + "/" + connectionName;

		//    Dictionary<string, string> args = new Dictionary<string, string>();



		//    //Set up basic variables for constructing the multipart/form-data data
		//    string newline = "\r\n";
		//    string boundary = DateTime.Now.Ticks.ToString("x");
		//    string data = "";

		//    //Construct data
		//    data += "--" + boundary + newline;
		//    data += "Content-Disposition: form-data; name=\"message\"" + newline + newline;
		//    data += message + newline;

		//    data += "--" + boundary + newline;
		//    data += "Content-Disposition: form-data; filename=\"test.jpg\"" + newline;
		//    data += "Content-Type: image/jpeg" + newline + newline;

		//    string ending = newline + "--" + boundary + "--" + newline;

		//    //Convert data to byte[] array
		//    MemoryStream finaldatastream = new MemoryStream();
		//    byte[] databytes = Encoding.UTF8.GetBytes(data);
		//    byte[] endingbytes = Encoding.UTF8.GetBytes(ending);
		//    finaldatastream.Write(databytes, 0, databytes.Length);
		//    finaldatastream.Write(photo, 0, photo.Length);
		//    finaldatastream.Write(endingbytes, 0, endingbytes.Length);
		//    byte[] finaldatabytes = finaldatastream.ToArray();
		//    finaldatastream.Dispose();

		//    //Make the request

		//    WebRequest request = HttpWebRequest.Create(postUrl);
		//    request.ContentType = "multipart/form-data; boundary=" + boundary;
		//    request.ContentLength = finaldatabytes.Length;
		//    request.Method = "POST";
		//    using (Stream RStream = request.GetRequestStream())
		//    {
		//        RStream.Write(finaldatabytes, 0, finaldatabytes.Length);
		//    }
		//    WebResponse WR = request.GetResponse();
		//    string reply = "";
		//    using (StreamReader sr = new StreamReader(WR.GetResponseStream()))
		//    {
		//        reply = sr.ReadToEnd();
		//        sr.Close();
		//    }


		//    JObject jo = JObject.Parse(reply);
		//    if (jo["error"] != null)
		//        throw new FacebookGraphAPIException(jo["error"]["type"].ToString(),
		//            jo["error"]["message"].ToString());

		//    return jo;
		//}


        /// <summary>
        /// Encodes a dictionary keys to send them via HTTP request
        /// </summary>
        /// <param name="dict">Dictionary to be encoded</param>
        /// <returns>Encoded dictionary keys</returns>
        private string EncodeDictionary(Dictionary<string, object> dict)
        {
            string ret = "";
            if (dict != null)
            {
                foreach (var item in dict)
                    ret += HttpUtility.UrlEncode(item.Key) + "=" + HttpUtility.UrlEncode(item.Value.ToString()) + "&";
                ret = ret.TrimEnd('&');
            }
            return ret;
        }
		private string EncodeDictionary(Dictionary<string, string> dict)
		{
			string ret = "";
			if (dict != null)
			{
				foreach (var item in dict)
					ret += HttpUtility.UrlEncode(item.Key) + "=" + HttpUtility.UrlEncode(item.Value) + "&";
				ret = ret.TrimEnd('&');
			}
			return ret;
		}


        /// <summary>
        /// Parses the cookie set by the official Facebook JavaScript SDK.
        /// 
        /// cookies should be a dictionary-like object mapping cookie names to
        /// cookie values.
        /// 
        /// If the user is logged in via Facebook, we return a dictionary with the
        /// keys "uid" and "access_token". The former is the user's Facebook ID,
        /// and the latter can be used to make authenticated requests to the Graph API.
        /// If the user is not logged in, we return None.
        /// 
        /// Download the official Facebook JavaScript SDK at
        /// http://github.com/facebook/connect-js/. Read more about Facebook
        /// authentication at http://developers.facebook.com/docs/authentication/.
        /// </summary>
        /// <param name="cookies">HttpCookieCollection</param>
        /// <param name="appId">Facebook Application Id</param>
        /// <param name="appSecret">Facebook Application Secret</param>
        /// <returns>Dictionary with the keys "uid" and "access_token"</returns>
		internal static Dictionary<string, string> GetUserFromCookie(Facebook.Apps app, HttpCookieCollection cookies)
		{
			try
			{
				string signedRequest = cookies["fbsr_" + FacebookCommon.Common(app).AppId].Value;

				JObject args = Facebook.FacebookCommon.DecodeFacebookRequest(signedRequest, app);


				string code = args["code"].Value<string>();

				//https://graph.facebook.com/oauth/access_token?
				//client_id=YOUR_APP_ID&redirect_uri=YOUR_URL&
				//client_secret=YOUR_APP_SECRET&code=THE_CODE_FROM_ABOVE

				string[] response = Post("client_id", FacebookCommon.Common(app).AppId.ToString(), "client_secret", FacebookCommon.Common(app).Secret, "redirect_uri", "", "code", code).Split('&');
				string accessToken = HttpUtility.UrlDecode(response[0].Split('=')[1]);
				//string expires = HttpUtility.UrlDecode(response[1].Split('=')[1]);
				

				Dictionary<string, string> ret = new Dictionary<string, string>();
				ret.Add("uid", args["user_id"].Value<string>());
				ret.Add("access_token", accessToken);

				return ret;
			}
			catch
			{
				return null;
			}

			//string appId = FacebookCommon.Common(app).ApiKey;
			//string appSecret = FacebookCommon.Common(app).Secret;
			//var args = new Dictionary<string, string>();
			//string[] fbsig = HttpUtility.UrlDecode(cookies["fbs_" + appId].Value.Trim('"')).Split('&');
			//foreach (var s in fbsig)
			//{
			//    string[] tmp = s.Split('=');
			//    args.Add(tmp[0], tmp[1]);
			//}
			//string payload = "";
			//foreach (var item in args) if (item.Key != "sig") payload += item.Key + "=" + item.Value;

			//string sig = Md5Hash(payload + appSecret);
			//int expires = int.Parse(args["expires"]);
			//int epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

			//if (sig == args["sig"] && (expires == 0 || epoch < expires)) return args;
			//return null;
		}

        /// <summary>
        /// Gets the MD5 Hash string for the given input
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Hashed string</returns>
        private static string Md5Hash(string input)
        {
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++) sb.Append(data[i].ToString("x2"));
            return sb.ToString();
        }
    }

    /// <summary>
    /// FacebookGraphAPIException
    /// </summary>
    public class FacebookGraphAPIException : Exception
    {
        public string Type { get; set; }
        public string Message { get; set; }

        public FacebookGraphAPIException(string type, string message)
        {
            Type = type;
            Message = message;
        }
    }

	public static class WebHelpers
	{
		public static Encoding encoding = Encoding.UTF8;

		/// <summary>
		/// Post the data as a multipart form
		/// postParameters with a value of type byte[] will be passed in the form as a file, and value of type string will be
		/// passed as a name/value pair.
		/// </summary>
		public static HttpWebResponse MultipartFormDataPost(string postUrl, string userAgent, Dictionary<string, object> postParameters)
		{
			string formDataBoundary = "-----------------------------28947758029299";
			string contentType = "multipart/form-data; boundary=" + formDataBoundary;

			byte[] formData = WebHelpers.GetMultipartFormData(postParameters, formDataBoundary);

			return WebHelpers.PostForm(postUrl, userAgent, contentType, formData);
		}

		/// <summary>
		/// Post a form
		/// </summary>
		private static HttpWebResponse PostForm(string postUrl, string userAgent, string contentType, byte[] formData)
		{
			HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

			if (request == null)
			{
				throw new NullReferenceException("request is not a http request");
			}

			// Add these, as we're doing a POST
			request.Method = "POST";
			request.ContentType = contentType;
			request.UserAgent = userAgent;
			request.CookieContainer = new CookieContainer();

			// We need to count how many bytes we're sending. 
			request.ContentLength = formData.Length;

			using (Stream requestStream = request.GetRequestStream())
			{
				// Push it out there
				requestStream.Write(formData, 0, formData.Length);
				requestStream.Close();
			}

			return request.GetResponse() as HttpWebResponse;
		}

		/// <summary>
		/// Turn the key and value pairs into a multipart form.
		/// See http://www.ietf.org/rfc/rfc2388.txt for issues about file uploads
		/// </summary>
		private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
		{
			Stream formDataStream = new System.IO.MemoryStream();

			foreach (var param in postParameters)
			{
				if (!(param.Value is byte[]))
				{
					string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n", boundary, param.Key, param.Value);
					formDataStream.Write(encoding.GetBytes(postData), 0, postData.Length);
				}
			}

			foreach (var param in postParameters)
			{
				if (param.Value is byte[])
				{
					byte[] fileData = param.Value as byte[];

					// Add just the first part of this param, since we will write the file data directly to the Stream
					string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: application/octet-stream\r\n\r\n", boundary, param.Key, param.Key);
					//string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: image/jpg\r\n\r\n", boundary, param.Key, param.Key);
					formDataStream.Write(encoding.GetBytes(header), 0, header.Length);

					// Write the file data directly to the Stream, rather than serializing it to a string.  This 
					formDataStream.Write(fileData, 0, fileData.Length);
				}
			}

			// Add the end of the request
			string footer = "\r\n--" + boundary + "--\r\n";
			formDataStream.Write(encoding.GetBytes(footer), 0, footer.Length);

			// Dump the Stream into a byte[]
			formDataStream.Position = 0;
			byte[] formData = new byte[formDataStream.Length];
			formDataStream.Read(formData, 0, formData.Length);
			formDataStream.Close();

			return formData;
		}
	}


}
