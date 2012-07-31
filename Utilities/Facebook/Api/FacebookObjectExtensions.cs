using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;

namespace Facebook.Api
{
    /// <summary>Provides extension methods for implementations of <see cref="IFacebookObject" />.</summary>
    public static class FacebookObjectExtensions
    {
        /// <summary>Gets the value of the element specified by <paramref name="xpath" />.</summary>
        /// <typeparam name="T">The type of value to return.</typeparam>
        /// <param name="obj">A reference to the current <see cref="IFacebookObject" /> instance.</param>
        /// <param name="xpath">A relative <c>XPath</c> query to the desired element.</param>
        /// <returns>A <typeparamref name="T" /> value containing the value of the specified element.</returns>
        public static T GetValueType<T>(this IFacebookObject obj, String xpath)
            where T : struct
        {
            if (obj.InnerDictionary.ContainsKey(xpath)) return (T)obj.InnerDictionary[xpath];
            else if (obj.XmlContent == null) return default(T);
            else
            {
                var element = obj.XmlContent.XPathSelectElement("//" + xpath);
                T result = default(T);
                if (element != null)
                {
                    result = ConvertStruct<T>(element.Value);
                }
                obj.InnerDictionary.Add(xpath, result);
                return result;
            }
        }

        /// <summary>Gets the value of the element specified by <paramref name="xpath" />.</summary>
        /// <param name="obj">A reference to the current <see cref="IFacebookObject" /> instance.</param>
        /// <param name="xpath">A relative <c>XPath</c> query to the desired element.</param>
        /// <returns>A <see cref="String" /> value containing the value of the specified element.</returns>
        public static String GetString(this IFacebookObject obj, String xpath)
        {
            if (obj.InnerDictionary.ContainsKey(xpath)) return (String)obj.InnerDictionary[xpath];
            else if (obj.XmlContent == null) return null;
            else
            {
                var element = obj.XmlContent.XPathSelectElement("//" + xpath);
                String result = null;
                if (element != null) result = element.Value;
                obj.InnerDictionary.Add(xpath, result);
                return result;
            }
        }

        /// <summary>Gets the value of the element specified by <paramref name="xpath" />.</summary>
        /// <typeparam name="T">The type of value to return.</typeparam>
        /// <param name="obj">A reference to the current <see cref="IFacebookObject" /> instance.</param>
        /// <param name="xpath">A relative <c>XPath</c> query to the desired element.</param>
        /// <returns>A <typeparamref name="T" /> object representation of the value of the specified element and its children.</returns>
        public static T GetFacebookObject<T>(this IFacebookObject obj, String xpath)
            where T : FacebookObjectBase, new()
        {
            if (obj.InnerDictionary.ContainsKey(xpath)) return (T)obj.InnerDictionary[xpath];
            else if (obj.XmlContent == null) return default(T);
            else
            {
                var element = obj.XmlContent.XPathSelectElement("//" + xpath);
                T result = null;
                if (element != null)
                {
                    result = new T();
                    result.Init(element);
                }
                obj.InnerDictionary.Add(xpath, result);
                return result;
            }
        }

        /// <summary>Gets a collection of <typeparamref name="T"/> objects that are the children of the element specified by <paramref name="xpath" />.</summary>
        /// <typeparam name="T">The type of value to return.</typeparam>
        /// <param name="obj">A reference to the current <see cref="IFacebookObject" /> instance.</param>
        /// <param name="xpath">A relative <c>XPath</c> query to the desired element.</param>
        /// <returns>A list of <typeparamref name="T" /> objects representing the children of the specified element.</returns>
        public static FacebookList<T> GetFacebookObjectCollection<T>(this IFacebookObject obj, String xpath)
            where T : FacebookObjectBase, new()
        {
            if (obj.InnerDictionary.ContainsKey(xpath)) return (FacebookList<T>)obj.InnerDictionary[xpath];
            else if (obj.XmlContent == null) return null;
            else
            {
                var element = obj.XmlContent.XPathSelectElement("//" + xpath);
                var attr = (FacebookObjectAttribute)typeof(T).GetCustomAttributes(typeof(FacebookObjectAttribute), true)[0];
                FacebookList<T> result = null;
                if (element != null) result = new FacebookList<T>(element, attr.XPath);
                obj.InnerDictionary.Add(xpath, result);
                return result;
            }
        }

        /// <summary>Gets a collection of <typeparamref name="T"/> values that are the children of the element specified by <paramref name="xpath" />.</summary>
        /// <typeparam name="T">The type of value to return.</typeparam>
        /// <param name="obj">A reference to the current <see cref="IFacebookObject" /> instance.</param>
        /// <param name="xpath">A relative <c>XPath</c> query to the desired element.</param>
        /// <param name="itemXPath">A relative <c>XPath</c> query to the child items within the element specified by <paramref name="xpath" />.</param>
        /// <returns>A list of <typeparamref name="T" /> values representing the children of the specified element.</returns>
        public static List<T> GetValueTypeCollection<T>(this IFacebookObject obj, String xpath, String itemXPath)
            where T : struct
        {
            if (obj.InnerDictionary.ContainsKey(xpath)) return (List<T>)obj.InnerDictionary[xpath];
            else if (obj.XmlContent == null) return null;
            else
            {
                var elements = obj.XmlContent.XPathSelectElements("//" + itemXPath);
                var values = elements.Select(element => ConvertStruct<T>(element.Value));
                List<T> result = null;
                if (elements != null) result = new List<T>(values);
                obj.InnerDictionary.Add(xpath, result);
                return result;
            }
        }

        /// <summary>Gets a collection of <see cref="String" /> values that are the children of the element specified by <paramref name="xpath" />.</summary>
        /// <param name="obj">A reference to the current <see cref="IFacebookObject" /> instance.</param>
        /// <param name="xpath">A relative <c>XPath</c> query to the desired element.</param>
        /// <param name="itemXPath">A relative <c>XPath</c> query to the child items within the element specified by <paramref name="xpath" />.</param>
        /// <returns>A list of <see cref="String" /> values representing the children of the specified element.</returns>
        public static List<String> GetStringCollection(this IFacebookObject obj, String xpath, String itemXPath)
        {
            if (obj.InnerDictionary.ContainsKey(xpath)) return (List<String>)obj.InnerDictionary[xpath];
            else if (obj.XmlContent == null) return null;
            else
            {
                var elements = obj.XmlContent.XPathSelectElements("//" + itemXPath);
                List<String> result = null;
                if (elements != null)
                {
                    var values = elements.Select(element => element.Value);
                    result = new List<String>(values);
                }
                obj.InnerDictionary.Add(xpath, result);
                return result;
            }
        }

        private static T ConvertStruct<T>(String input)
        {
            if (String.IsNullOrEmpty(input)) return default(T);
            else
            {
                var type = typeof(T);
                if (type == typeof(DateTime))
                {
                    UnixDateTime unixTime;
                    UnixDateTime.TryParse(input, out unixTime);
                    return (T)(Object)unixTime.ToDateTime();
                }
                else if (type == typeof(Boolean))
                {
                    Byte value;
                    if (Byte.TryParse(input, out value) && (value == 0 || value == 1)) return (T)(Object)Convert.ToBoolean(value);
                    else return default(T);
                }
                else if (type.IsEnum)
                {
                    try{ return (T)Enum.Parse(type, input); }
                    catch { return default(T); }
                }
                else return (T)Convert.ChangeType(input, typeof(T));
            }
        }
    }
}
