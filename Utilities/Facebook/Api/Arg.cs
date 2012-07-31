using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Facebook.Api
{
    /// <summary>Represents an argument that is passed into an API method call.</summary>
    [FacebookObject("arg")]
    public class Arg : FacebookObjectBase
    {
        /// <summary>Intializes an instance of <see cref="Arg" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public Arg(XElement content) : base(content) { }

        /// <summary>Intializes an instance of <see cref="Arg" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public Arg(IDictionary<String, Object> dict) : base(dict) { }

        /// <summary>Intializes an instance of <see cref="Arg" />.</summary>
        public Arg() { }

        /// <summary>Gets or sets the key of the argument.</summary>
        public String Key
        {
            get { return this.GetString("key"); }
            set { this.InnerDictionary["key"] = value; }
        }

        /// <summary>Gets or sets the value of the argument.</summary>
        public String Value
        {
            get { return this.GetString("value"); }
            set { this.InnerDictionary["value"] = value; }
        }
    }
}
