using System;
using System.Collections.Generic;
using Facebook.Api;

namespace Facebook
{
    /// <summary>Defines a contract that the API client implements to provide programmatic access to the Facebook API.</summary>
    public interface IFacebookContext
    {
        /// <summary>Gets the API key that identifies the Facebook application.</summary>
        String ApiKey { get; }

        /// <summary>Gets the application secret that corresponds to the <see cref="ApiKey" /> used to authenticate the application.</summary>
        String Secret { get; }

        /// <summary>Gets the version of the API targeted by the context instance.</summary>
        String Version { get; }

        /// <summary>Gets a value representing whether an authentication session has been established between the user and the appliation.</summary>
        Boolean HasSession { get; }

        /// <summary>Gets a reference to a <see cref="ISessionInfo" /> object containing data about the current session.</summary>
        ISessionInfo Session { get; }

        /// <summary>Initializes the <see cref="IFacebookContext" /> implementation's <see cref="Session" /> property with values
        /// from the specified <see cref="ISessionInfo" /> object.</summary>
        /// <param name="sessionInfo"></param>
        void InitSession(ISessionInfo sessionInfo);

        /// <summary>Gets the <see cref="ApplicationType" /> of the context instance.</summary>
        ApplicationType ApplicationType { get; }

        /// <summary>Executes a <see cref="FacebookRequest" /> for <paramref name="methodName" /> with the specified arguments.</summary>
        /// <typeparam name="TValue">The expected return type of the method call.</typeparam>
        /// <param name="methodName">The name of the API method.</param>
        /// <param name="args">An <see cref="IDictionary{String,Object} " /> of arguments that will be passed into the API call.</param>
        /// <param name="excludedArgs">An array of <see cref="String" /> values specifying arguments that should be left out of the API method call.</param>
        /// <returns>A reference to a <see cref="FacebookResponse{TValue}" /> object that will contain the value of the response.</returns>
        /// <remarks>
        /// If a <see cref="Batch" /> is currently active, the <see cref="FacebookResponse{TValue}" /> returned by this method will not contain
        /// a value until <see cref="Api.Batch.Complete" /> is called.
        /// </remarks>
        FacebookResponse<TValue> ExecuteRequest<TValue>(String methodName, IDictionary<String, Object> args, params String[] excludedArgs);

        /// <summary>Gets the current batch, if any, for the implementation of <see cref="IFacebookContext" />.</summary>
        Batch Batch { get; set; }
    }
}
