using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Reflection;

namespace Facebook.Api
{
    /// <summary>Represents a strongly typed list of <see cref="FacebookObjectBase" /> objects.</summary>
    /// <typeparam name="T"></typeparam>
    public class FacebookList<T> : List<T>, IFacebookList
        where T : FacebookObjectBase, new()
    {
        /// <summary>Initializes an instance of <see cref="FacebookList{T}" /> with the specified content.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing the list serialized into XML.</param>
        /// <param name="itemXPath">The XPath identifier of a single <typeparamref name="T"/> object within the list.</param>
        public FacebookList(XElement content, String itemXPath)
            : base(FacebookList<T>.GetObjects(content, itemXPath)) { }

        /// <summary>Deserializes a list of <typeparamref name="T" /> objects from the specified XML content.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing the list serialized into XML.</param>
        /// <param name="itemXPath">The XPath identifier of a single <typeparamref name="T"/> object within the list.</param>
        /// <returns>An <see cref="IEnumerable{T}" /> containing the objects queried from the specified content.</returns>
        private static IEnumerable<T> GetObjects(XElement content, String itemXPath)
        {
            return
                from element in content.XPathSelectElements("//" + itemXPath)
                select FacebookList<T>.GetFacebookObject(element);
        }

        /// <summary>Deserializes a single instance of <typeparamref name="T" /> from the specified xml content.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing the an XML serialized representation of a <typeparamref name="T" /> object.</param>
        /// <returns></returns>
        private static T GetFacebookObject(XElement content)
        {
            var obj = new T();
            obj.Init(content);
            return obj;
        }
    }
}
