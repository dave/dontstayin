using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Facebook
{
    /// <summary>Makes a request to a Facebook API method.</summary>
    [Serializable]
    public class FacebookRequest
    {
        /// <summary>The URL of the Facebook API REST application.</summary>
        internal const String FACEBOOK_REST_URL = "https://api.facebook.com/restserver.php";

        /// <summary>Intializes an instance of <see cref="FacebookRequest" /> for the specified <paramref name="methodName"/>.</summary>
        /// <param name="facebookContext">A reference to the <see cref="IFacebookContext" /> object the request is being made for.</param>
        /// <param name="methodName">The name of the API method.</param>
        /// <param name="args">A <see cref="IDictionary{String,Object}" /> object containing arguments to be passed with the method call.</param>
        /// <param name="excludedArgs">An array of <see cref="String" /> values specifying arguments that should be left out of the API method call.</param>
        public FacebookRequest(IFacebookContext facebookContext, String methodName, IDictionary<String, Object> args, params String[] excludedArgs)
        {
            this.Context = facebookContext;
            this.MethodName = methodName;
            this.Args = args;
            this.ExcludedArgs = excludedArgs;
        }

        /// <summary>Gets a reference to the <see cref="IFacebookContext" /> the request if being made for.</summary>
        public IFacebookContext Context { get; private set; }

        /// <summary>Gets the name of the method being called.</summary>
        public String MethodName { get; internal set; }

        /// <summary>Gets a dictionary of arguments that will be inluded in the request.</summary>
        public IDictionary<String, Object> Args { get; internal set; }

        /// <summary>Gets or sets the array of <see cref="String" /> values specifying arguments that should be left out of the API method call.</summary>
        public String[] ExcludedArgs { get; set; }

        /// <summary>Executes the request.</summary>
        /// <typeparam name="TValue">The expected return type of the method call.</typeparam>
        /// <param name="timeout">The amount of time, in milliseconds, the request will be active before timing out.</param>
        /// <returns>A reference to a <see cref="FacebookResponse{TValue}" /> object that will contain the value of the response.</returns>
        internal FacebookResponse<TValue> Execute<TValue>(Int32 timeout)
        {
            try
            {
                var request = this.CreateWebRequest(timeout);
                var response = (HttpWebResponse)request.GetResponse();

                return FacebookRequest.ProcessResponse<TValue>(response);
            }
            catch (Exception ex) { return new FacebookResponse<TValue>(ex); }
        }

        /// <summary>Creates an <see cref="HttpWebRequest" /> object for the request.</summary>
        /// <param name="timeout">The amount of time, in milliseconds, the request will be active before timing out.</param>
        /// <returns>An <see cref="HttpWebRequest" /> object for the request.</returns>
        internal HttpWebRequest CreateWebRequest(Int32 timeout)
        {
            var content = this.GenerateRequestContent();
            var request = (HttpWebRequest)WebRequest.Create(FacebookRequest.FACEBOOK_REST_URL);
            request.Timeout = timeout;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = content.Length;

            using (var stream = request.GetRequestStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(content.ToString());
                    writer.Close();
                }
            }

            return request;
        }

        /// <summary>Generates a <see cref="String" /> containing the content stream for the request.</summary>
        /// <returns>A <see cref="String" /> containing the content stream for the request.</returns>
        internal String GenerateRequestContent()
        {
            var args = new Dictionary<String, Object>(this.Args ?? new Dictionary<String, Object>());
            foreach (var excluded in this.ExcludedArgs) args.Remove(excluded);

            args.Add("method", this.MethodName);
            args.Add("api_key", this.Context.ApiKey);
            args.Add("v", this.Context.Version);

            if (this.Context.HasSession && !this.ExcludedArgs.Contains("session_key"))
            {
                //args.Add("ss", "1");
                args.Add("session_key", this.Context.Session.SessionKey);
                args.Add("call_id", DateTime.Now.Ticks);
            }
            else
            {
                args.Add("ss", "0");
                args.Add("session_key", String.Empty);
                args.Add("call_id", String.Empty);
            }

            var sig = FacebookRequest.ComputeSignature(this.Context.Secret, args);
            args.Add("sig", sig);


            var content = new StringBuilder();

            foreach (var arg in args)
            {
                Object value = null;
                IEnumerable col = null;
                if (arg.Value.TryMakeEnumerable(out col)) value = col.ToDelimitedString(',');
                else value = arg.Value;

                content.AppendFormat("{0}{1}={2}", content.Length > 0 ? "&" : String.Empty, arg.Key, HttpUtility.UrlEncode(value.ToString()));
            }

            return content.ToString();
        }

        /// <summary>Computes the signature of a request passing the specified <paramref name="arguments"/>.</summary>
        /// <param name="secret">The application secret.</param>
        /// <param name="arguments">The list of arguments being passed into the method request.</param>
        /// <returns>A <see cref="String" /> containing a request signature generated according to the documentation provided by Facebook in the article <a href="http://wiki.developers.facebook.com/index.php/How_Facebook_Authenticates_Your_Application">How Facebook Authenticates Your Application</a>.</returns>
        internal static String ComputeSignature(String secret, IDictionary<String, Object> arguments)
        {
            var sortedArgs = arguments.OrderBy(arg => arg.Key);

            var seedBuilder = new StringBuilder();
            foreach (var arg in sortedArgs)
            {
                Object value = null;
                IEnumerable col = null;
                if (arg.Value.TryMakeEnumerable(out col)) value = col.ToDelimitedString(',');
                else value = arg.Value;

                seedBuilder.AppendFormat("{0}={1}", arg.Key, value);
            }
            seedBuilder.Append(secret);

            var seed = seedBuilder.ToString();
            var seedBytes = Encoding.ASCII.GetBytes(seed);
            var sig = MD5.Create().ComputeHashString(seed);

            return sig.ToLower();
        }

        /// <summary>Processes the <see cref="HttpWebResponse" /> object generated by the request and returns a <see cref="FacebookResponse{TValue}" /> object containing the result.</summary>
        /// <typeparam name="TValue">The expected return type of the method call.</typeparam>
        /// <param name="response">A <see cref="HttpWebResponse" /> object.</param>
        /// <returns>A <see cref="FacebookResponse{TValue}" /> object containing the result of the API method call.</returns>
        internal static FacebookResponse<TValue> ProcessResponse<TValue>(HttpWebResponse response)
        {
            String responseContent = null;
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    responseContent = reader.ReadToEnd();
                    reader.Close();
                }
            }

            return new FacebookResponse<TValue>(responseContent);
        }
    }
}
