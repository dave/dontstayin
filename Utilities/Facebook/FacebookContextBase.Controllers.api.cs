using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Facebook.Api;
using Facebook.Api.Controllers;

namespace Facebook
{
    public partial class FacebookContextBase<TCache, TSession> : IFacebookContext, ISerializable
        where TCache : ICacheWrapper, new()
        where TSession : class, ISessionInfo
    {
        private FqlController _fql;

        /// <summary>Gets a reference to the <see cref="FqlController" /> that allows <c>FQL</c> queries to be executed.</summary>
        public virtual FqlController Fql { get { return this._fql; } }

        private void InitNonGeneratedControllers()
        {
            this._fql = new FqlController(this);
        }
    }
}
