using System;

namespace Facebook.Api
{
    /// <summary>Specifies the relative XPath identifier required to query the decorated object.</summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FacebookObjectAttribute : Attribute
    {
        /// <summary>Initializes an instance of the <see cref="FacebookObjectAttribute" /> class with the specified <paramref name="xpath" />.</summary>
        /// <param name="xpath"></param>
        public FacebookObjectAttribute(String xpath)
        {
            this.XPath = xpath;
        }

        /// <summary>Gets or sets the relative XPath identifier required to query the decorated object.</summary>
        public String XPath { get; set; }
    }
}
