using System;

namespace Facebook.Api
{
    /// <summary>Defines a contract for an object that encapsulates all the objects required to execute an API request
    /// within a <see cref="Batch" />.</summary>
    internal interface IBatchItem
    {
        /// <summary>Gets the <see cref="FacebookRequest" /> object containing information about the request, such as the method and any parameters to be passed in.</summary>
        FacebookRequest Request { get; }

        /// <summary>Gets a reference to the <see cref="IFacebookResponse" /> object representing the unpopulated response of the request.</summary>
        IFacebookResponse Response { get; }
    }
}
