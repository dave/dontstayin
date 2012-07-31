using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Facebook.Api
{
    /// <summary>Defines a contract required to instantiate an API wrapper type and access its properties and acts as an anchor for the
    /// extension methods found in the <see cref="FacebookObjectExtensions" /> class.</summary>
    /// <remarks>
    /// <para>By using the extension methods defined in <see cref="FacebookObjectExtensions" />, <see cref="IFacebookObject" /> implementations
    /// allow the XML responses from Facebook API methods to be easily and economically deserialized.</para>
    /// <para>The properties of a given <see cref="IFacebookObject" /> are queried on demand, so the first time the get accessor of a property is
    /// accessed, an XPath query will be executed, and the result is stored in the <see cref="InnerDictionary" /> object for subsequent access.</para>
    /// <para>There are methods in <see cref="FacebookObjectExtensions" /> that support loading child <see cref="IFacebookObject" /> implementations,
    /// so a theoretically infinite hierarchy of parent/child objects is supported.</para>
    /// <para><see cref="Facebook.Api.Controllers.FqlController.Query{TValue}" /> requires that the type parameter implement <see cref="IFacebookObject" />,
    /// which allows developers to create their own custom <see cref="IFacebookObject" /> implementions that can be returned for FQL queries.</para>
    /// <seealso cref="FacebookObjectBase" />
    /// <seealso cref="FacebookObjectExtensions" />
    /// <seealso cref="Facebook.Api.Controllers.FqlController.Query{TValue}" />
    /// </remarks>
    public interface IFacebookObject
    {
        /// <summary>Gets a reference to a dictionary of properties and values for the object.</summary>
        IDictionary<String, Object> InnerDictionary { get; }

        /// <summary>Gets a reference the <see cref="XDocument" /> data source specified for this instance.</summary>
        XDocument XmlContent { get; }

        /// <summary>Initializes the <see cref="FacebookObjectBase" /> instance using the specified <see cref="XElement" /> object as the data source.</summary>
        /// <param name="dataSource">A <see cref="XElement" /> object containing XML that will be queried by the object's properties.</param>
        void Init(XElement dataSource);
    }
}
