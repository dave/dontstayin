using System;
using System.Net;

namespace Facebook
{
    /// <summary>Represents async state for asynchronous API requests.</summary>
    internal class FacebookAsyncState
    {
        /// <summary>Initializes a new instance of the <see cref="FacebookAsyncState" /> class.</summary>
        /// <param name="facebookRequest">A <see cref="FacebookRequest" /> object.</param>
        /// <param name="httpRequest">An <see cref="HttpWebRequest" /> object.</param>
        internal FacebookAsyncState(FacebookRequest facebookRequest, HttpWebRequest httpRequest)
        {
            this.HttpRequest = httpRequest;
            this.Request = facebookRequest;
        }

        /// <summary>Gets the <see cref="HttpWebRequest" /> object associated with the async request.</summary>
        internal HttpWebRequest HttpRequest { get; private set; }

        /// <summary>Gets the <see cref="FacebookRequest" /> object associated with the async request.</summary>
        public FacebookRequest Request { get; private set; }
    }
}
