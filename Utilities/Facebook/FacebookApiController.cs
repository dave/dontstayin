using System;
using System.Collections.Generic;
using Facebook.Api;

namespace Facebook
{
    /// <summary>Provides base functionality for classes that encapsulate specific areas of the Facebook API.</summary>
    public abstract class FacebookApiController
    {
        /// <summary>Initializes a new instance of <see cref="FacebookApiController" /> that is tied to the specified <paramref name="facebookContext" />.</summary>
        /// <param name="facebookContext">A reference to the <see cref="IFacebookContext" /> object that will process requests made to this controller.</param>
        public FacebookApiController(IFacebookContext facebookContext)
        {
            this.FacebookContext = facebookContext;
        }

        /// <summary>Gets a reference to the <see cref="IFacebookContext" /> object that will process requests made to this controller.</summary>
        protected internal IFacebookContext FacebookContext { get; private set; }

        /// <summary>Executes a <see cref="FacebookRequest" /> for <paramref name="methodName" /> with no arguments specified.</summary>
        /// <typeparam name="TValue">The expected return type of the method call.</typeparam>
        /// <param name="methodName">The name of the API method.</param>
        /// <param name="excludedArgs">An array of <see cref="String" /> values specifying arguments that should be left out of the API method call.</param>
        /// <returns>A reference to a <see cref="FacebookResponse{TValue}" /> object that will contain the value of the response.</returns>
        /// <remarks>
        /// If a <see cref="Batch" /> is currently active, the <see cref="FacebookResponse{TValue}" /> returned by this method will not contain
        /// a value until <see cref="Batch.Complete" /> is called.
        /// </remarks>
        protected FacebookResponse<TValue> ExecuteRequest<TValue>(String methodName, params String[] excludedArgs)
        {
            return this.FacebookContext.ExecuteRequest<TValue>(methodName, null, excludedArgs);
        }

        /// <summary>Executes a <see cref="FacebookRequest" /> for <paramref name="methodName" /> with the specified arguments.</summary>
        /// <typeparam name="TValue">The expected return type of the method call.</typeparam>
        /// <param name="methodName">The name of the API method.</param>
        /// <param name="args">An <see cref="IDictionary{String,Object} " /> of arguments that will be passed into the API call.</param>
        /// <param name="excludedArgs">An array of <see cref="String" /> values specifying arguments that should be left out of the API method call.</param>
        /// <returns>A reference to a <see cref="FacebookResponse{TValue}" /> object that will contain the value of the response.</returns>
        /// <remarks>
        /// If a <see cref="Batch" /> is currently active, the <see cref="FacebookResponse{TValue}" /> returned by this method will not contain
        /// a value until <see cref="Batch.Complete" /> is called.
        /// </remarks>
        protected FacebookResponse<TValue> ExecuteRequest<TValue>(String methodName, IDictionary<String, Object> args, params String[] excludedArgs)
        {
            return this.FacebookContext.ExecuteRequest<TValue>(methodName, args, excludedArgs);
        }
    }
}
