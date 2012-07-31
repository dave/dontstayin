using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Facebook.Api;

namespace Facebook
{
    /// <summary>Provides access to data from the response from a Facebook API method call.</summary>
    /// <typeparam name="TValue">The type of the value that should be returned from the response.</typeparam>
    [Serializable]
    public class FacebookResponse<TValue> : IFacebookResponse
    {
        /// <summary>Initializes an instance of <see cref="FacebookResponse{TValue}" />.</summary>
        internal FacebookResponse() { }

        /// <summary>Initializes an instance of <see cref="FacebookResponse{TValue}" /> that represents an exceptional response.</summary>
        /// <param name="ex">The <see cref="Exception" /> thrown by the method call.</param>
        internal FacebookResponse(Exception ex)
        {
            this.HasResult = true;
            this.ResponseException = ex;
        }

        /// <summary>Initializes an instance of <see cref="FacebookResponse{TValue}" /> representing specific content returned from the API.</summary>
        /// <param name="content">The XML content returned from the API.</param>
        internal FacebookResponse(String content)
        {
            this.Value = this.Init(content);
        }

        /// <summary>Gets a reference to an exception thrown by the response.</summary>
        public Exception ResponseException { get; private set; }

        /// <summary>Gets a value representing whether the current response is exceptional.</summary>
        public Boolean IsError { get { return this.ResponseException != null; } }

        /// <summary>Gets the value of the response.</summary>
        public TValue Value { get; internal set; }

        /// <summary>Gets a value representing whether the response's result has been processed.</summary>
        /// <remarks>
        /// When working with <see cref="FacebookResponse{TValue}" /> objects within a <see cref="Batch" />, this property will be <c>false</c>
        /// until <see cref="Batch.Complete" /> is called. Otherwise, it will be <c>true</c> as soon as the reference is returned.
        /// </remarks>
        public Boolean HasResult { get; private set; }

        /// <summary>Populates the value of the response.</summary>
        /// <param name="content">The XML content returned from the API.</param>
        internal TValue Init(String content)
        {
            this.HasResult = true;
            var responseContent = XDocument.Parse(content.Replace("xmlns=\"http://api.facebook.com/1.0/\"", String.Empty));
            try
            {
                if (FacebookApiException.IsApiException(responseContent.Root)) throw new FacebookApiException(responseContent.Root);
                else
                {
                    Type type = typeof(TValue);
                                        
                    if (type.IsValueType || type == typeof(String)) // check for simple types
                    {
                        var result = responseContent.Root.Value;
                        if (type == typeof(String)) return (TValue)(Object)result; // this is real easy for strings, but it needs to be double-boxed
                        else
                        {
                            Object value = this.GetConvertible(result, type);
                            return (TValue)Convert.ChangeType(value, type);
                        }
                    }
                    else if (type.IsArray) // check for arrays - only arrays of strings and value types are allowed
                    {
                        Type elementType = type.GetElementType();
                        Boolean elementTypeIsString = elementType == typeof(String);

                        var elements = responseContent.Root.Elements().ToArray();

                        Array array = (Array)ObjectFactory.Create(type, new Type[] { typeof(Int32) }, new Object[] { elements.Count() });
                        
                        if (!elementTypeIsString && !elementType.IsValueType) throw new InvalidOperationException("Arrays can only be used for simple types. Use FacebookList<T> instead.");
                        
                        for(var index = 0; index < array.Length;index++)
                        {
                            var element = elements[index];
                            array.SetValue(elementTypeIsString ? element.Value : Convert.ChangeType(this.GetConvertible(element.Value, elementType), elementType), index);
                        }

                        return (TValue)(Object)array;
                    }
                    else if (type == typeof(XElement))
                    {
                        return (TValue)(Object)responseContent.Root;
                    }
                    else // all other types
                    {
                        var typeInterfaces = type.GetInterfaces();
                        if (typeInterfaces.Contains(typeof(IFacebookObject)))
                        {
                            var result = ObjectFactory.Create(type,
                                new Type[] { typeof(XElement) },
                                new Object[] { responseContent.Root });

                            return (TValue)result;
                        }
                        else if (typeInterfaces.Contains(typeof(IEnumerable)))
                        {
                            Type listItemType = null;
                            if (type.IsGenericType)
                            {
                                var typeParams = type.GetGenericArguments();
                                listItemType = typeParams[0];
                                if (typeParams.Length == 1 && (listItemType.IsIntegral() || listItemType == typeof(String)))
                                {
                                    var listType = Type.GetType(String.Format("System.Collections.Generic.List`1[[{0}]]", listItemType.FullName));
                                    if (type == listType)
                                    {
                                        var list = (IList)ObjectFactory.Create(type, null, null);
                                        foreach (var element in responseContent.Root.Elements())
                                        {
                                            list.Add(Convert.ChangeType(element.Value, listItemType));
                                        }
                                        return (TValue)list;
                                    }
                                }
                            }

                            if (!typeInterfaces.Any(i => i.Name == "IFacebookList")) throw new InvalidOperationException("Collections must be a subclass of FacebookList<T>.");

                            listItemType = type.GetGenericArguments()[0];
                            var objAttr = (FacebookObjectAttribute)listItemType.GetCustomAttributes(typeof(FacebookObjectAttribute), true)[0];

                            var result = ObjectFactory.Create(type,
                                new Type[] { typeof(XElement), typeof(String) },
                                new Object[] { responseContent.Root, objAttr.XPath });

                            return (TValue)result;
                        }
                        else throw new ArgumentException(String.Format("{0} must be a String, Value type, or a subclass FacebookList<T> or an implementation of IFacebookObject.", type.FullName), "TValue");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ResponseException = ex;
                throw ex;
            }
        }

        void IFacebookResponse.Init(String content)
        {
            this.Value = this.Init(content);
        }

        private Object GetConvertible(String input, Type type)
        {
            Object value = null;
            if (type.IsIntegral()) value = Int64.Parse(input); // integers need to be specifically parsed before changing their type
            else value = input;

            return value;
        }

        /// <summary>Provides an implicit conversion from <see cref="FacebookResponse{TValue}" /> to <typeparamref name="TValue" /> by returning the value in <see cref="FacebookResponse{TValue}.Value" />.</summary>
        /// <param name="response">A <see cref="FacebookResponse{TValue}" /> object.</param>
        /// <returns>The value in <see cref="FacebookResponse{TValue}.Value" />.</returns>
        public static implicit operator TValue (FacebookResponse<TValue> response)
        {
            return response.Value;
        }
    }
}
