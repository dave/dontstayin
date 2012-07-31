using System;
using Facebook.Json;
using System.Xml.Linq;
using System.Xml;

namespace Facebook
{
    /// <summary>A wrapper for <see cref="FacebookResponse{TOriginalValue}" /> that allows the responses of Facebook API method calls to be returned
    /// as JSON.</summary>
    /// <typeparam name="TOriginalValue">The type of the value that should be returned from the inner <see cref="FacebookResponse{TOriginalValue}" />.</typeparam>
    public class FacebookJsonResponse<TOriginalValue> : IFacebookResponse
    {
        internal FacebookJsonResponse() { }

        /// <summary>Initializes a <see cref="FacebookJsonResponse{TOriginalValue}" /> instance using the specified <see cref="FacebookResponse{TOriginalValue}" />.</summary>
        /// <param name="response">A <see cref="FacebookResponse{TOriginalValue}" /> object.</param>
        public FacebookJsonResponse(FacebookResponse<TOriginalValue> response)
        {
            this.InnerResponse = response;

            if (this.InnerResponse.HasResult && !this.InnerResponse.IsError)
            {
                var type = typeof(TOriginalValue);
                if (type.IsSubclassOf(typeof(XNode)))
                {
                    using (var reader = ((XNode)(Object)this.InnerResponse.Value).CreateReader())
                    {
                        if(reader.Read()) this.Init(reader.ReadOuterXml());
                    }
                }
                else if (type.IsSubclassOf(typeof(XmlNode)))
                {
                    this.Init(((XmlNode)(Object)this.InnerResponse.Value).OuterXml);
                }
                else this.Value = JsonConvert.SerializeObject(this.InnerResponse.Value);
            }
        }

        private FacebookResponse<TOriginalValue> InnerResponse { get; set; }

        #region [ IFacebookResponse Members ]

        /// <summary>Populates the value of the response.</summary>
        /// <param name="content">The XML content returned from the API.</param>
        public void Init(String content)
        {
            this.InnerResponse.Init(content);
            if (!this.InnerResponse.IsError)
            {
                var doc = new XmlDocument();
                doc.LoadXml(content);
                this.Value = JsonConvert.SerializeXmlNode(doc);
            }
        }

        /// <summary>Gets a reference to an exception thrown by the response.</summary>
        public Exception ResponseException
        {
            get { return this.InnerResponse.ResponseException; }
        }

        /// <summary>Gets a value representing whether the current response is exceptional.</summary>
        public Boolean IsError
        {
            get { return this.InnerResponse.IsError; }
        }

        #endregion

        /// <summary>Gets the value of the underlying <see cref="FacebookResponse{TOriginalValue}" /> object.</summary>
        public String Value { get; private set; }
    }
}
