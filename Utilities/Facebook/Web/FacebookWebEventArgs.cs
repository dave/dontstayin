using System;

namespace Facebook.Web
{
    /// <summary><see cref="FacebookWebEventArgs" /> is the base class for classes containing data about a <see cref="FacebookWebEventHandler" /> event.</summary>
    public class FacebookWebEventArgs : EventArgs
    {
        /// <summary>Initializes an instance of the <see cref="FacebookWebEventArgs" /> class with the specified <paramref name="request" />,
        /// <paramref name="session" /> and <paramref name="context" /> objects.</summary>
        /// <param name="request">A reference to the current <see cref="FacebookHttpRequest" /> object at the time of the event.</param>
        /// <param name="session">A reference to the current <see cref="FacebookHttpSession" /> object at the time of the event.</param>
        /// <param name="context">A reference to the current <see cref="FacebookHttpContext" /> object at the time of the event.</param>
        public FacebookWebEventArgs(FacebookHttpRequest request, FacebookHttpSession session, FacebookHttpContext context)
        {
            this.Request = request;
            this.Session = session;
            this.Context = context;
        }

        /// <summary>Gets a reference to the current <see cref="FacebookHttpRequest" /> object at the time of the event.</summary>
        public FacebookHttpRequest Request { get; private set; }

        /// <summary>Gets a reference to the current <see cref="FacebookHttpSession" /> object at the time of the event.</summary>
        public FacebookHttpSession Session { get; private set; }

        /// <summary>Gets a reference to the current <see cref="FacebookHttpContext" /> object at the time of the event.</summary>
        public FacebookHttpContext Context { get; private set; }
    }
}
