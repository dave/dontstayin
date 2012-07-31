using System;

namespace Facebook
{
    /// <summary>Provides type-agnostic access to <see cref="FacebookResponse{TValue}" />.</summary>
    public interface IFacebookResponse
    {
        /// <summary>Populates the value of the response.</summary>
        /// <param name="content">The XML content returned from the API.</param>
        void Init(String content);

        /// <summary>Gets a reference to an exception thrown by the response.</summary>
        Exception ResponseException { get; }

        /// <summary>Gets a value representing whether the current response is exceptional.</summary>
        Boolean IsError { get; }
    }
}
