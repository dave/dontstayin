using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Facebook.Api
{
    /// <summary>Represents Facebook API-specific errors that occur during the execution of API calls.</summary>
    [FacebookObject("error_response")]
    public class FacebookApiException : Exception, IFacebookObject
    {
        /// <summary>Initializes a new instance of <see cref="FacebookApiException" /> using the specified xml <paramref name="content" /> to
        /// represent the data of the exception.</summary>
        /// <param name="content">An xml document or snippet that contains information about the exception.</param>
        public FacebookApiException(XElement content)
        {
            this.Init(content);
        }

        /// <summary>Initializes a new instance of <see cref="FacebookApiException" />.</summary>
        public FacebookApiException()
        {
            this.InnerDictionary = new Dictionary<String, Object>();
        }

        /// <summary>Initializes the <see cref="FacebookApiException" /> with the specified <paramref name="content"/>.</summary>
        /// <param name="content">An xml document or snippet that contains information about the exception.</param>
        public void Init(XElement content)
        {
            this.XmlContent = new XDocument(content);
            this.InnerDictionary = new Dictionary<String, Object>();
        }

        /// <summary>Returns a value representing whether the specified <paramref name="contenxt" /> contains information about an API exception.</summary>
        /// <param name="content">An xml document or snippet that may contain information about the exception.</param>
        /// <returns><c>true</c> if the content is API exception data; otherwise, <c>false</c>.</returns>
        public static Boolean IsApiException(XElement content)
        {
            return content.Name.LocalName == "error_response";
        }

        /// <summary>Gets a reference to the xml content used to initalize this exception.</summary>
        public XDocument XmlContent { get; private set; }

        /// <summary>Gets a reference to the dictionary used to store the properties and values of the <see cref="IFacebookObject" /> implementation.</summary>
        public IDictionary<String, Object> InnerDictionary { get; private set; }

        /// <summary>Gets or sets the <see cref="ErrorCode" /> returned by the API.</summary>
        public ErrorCode ErrorCode
        {
            get { return this.GetValueType<ErrorCode>("error_code"); }
            set { this.InnerDictionary["error_code"] = value; }
        }

        /// <summary>Gets or sets the error message returned by the API.</summary>
        public String ErrorMsg
        {
            get { return this.GetString("error_msg"); }
            set { this.InnerDictionary["error_msg"] = value; }
        }

        /// <summary>Gets or sets the list of arguments returned by the API that were originally passed into the method.</summary>
        public List<Arg> RequestArgs
        {
            get { return this.GetFacebookObjectCollection<Arg>("request_args"); }
            set { this.InnerDictionary["request_args"] = value; }
        }

        /// <summary>Overrides <see cref="Exception.Message" /> to return the message returned by the API.</summary>
        public override String Message { get { return this.ErrorMsg; } }

        /// <summary>Overrides <see cref="Exception.Data" /> to return the request arguments returned by the API in dictionary form.</summary>
        public override IDictionary Data
        {
            get { return this.RequestArgs.ToDictionary(arg => arg.Key, arg => arg.Value); }
        }
    }
}
