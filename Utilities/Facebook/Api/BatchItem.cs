using System;

namespace Facebook.Api
{
    /// <summary>Encapsulates all the objects required to execute an API request within a <see cref="Batch" />.</summary>
    /// <typeparam name="TReturnType">The return type of the API method.</typeparam>
    [Serializable]
    public struct BatchItem<TReturnType> : IBatchItem
    {
        /// <summary>Intializes an instance of <see cref="BatchItem{TReturnType}" /> with the specified objects.</summary>
        /// <param name="request">A <see cref="FacebookRequest" /> object containing information about the request, such as the method and any parameters to be passed in.</param>
        /// <param name="response">An <see cref="IFacebookResponse" /> object representing the unpopulated response of the request.</param>
        internal BatchItem(FacebookRequest request, IFacebookResponse response)
            : this()
        {
            this.Request = request;
            this.Response = (FacebookResponse<TReturnType>)response;      
        }

        /// <summary>Gets or sets the <see cref="FacebookRequest" /> object containing information about the request, such as the method and any parameters to be passed in.</summary>
        public FacebookRequest Request { get; set; }

        /// <summary>Gets or sets a reference to the <see cref="FacebookResponse{TReturnType}" /> object representing the unpopulated response of the request.</summary>
        public FacebookResponse<TReturnType> Response { get; set; }

        /// <summary>Gets or sets a reference to the <see cref="IFacebookResponse" /> object representing the unpopulated response of the request.</summary>
        IFacebookResponse IBatchItem.Response { get { return this.Response; } }
    }
}
