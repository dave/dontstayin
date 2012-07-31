using System;
using System.Collections.Generic;
using System.Text;

namespace Common.CommandLine
{
    public class StringArgument
    {
        public string[] Prefixes { get; protected set; }
        bool initialised = false;
        string value;
        string defaultValue;
        public StringArgument(string[] prefixes, string defaultValue)
        {
            this.defaultValue = defaultValue;
            this.Prefixes = prefixes;
            Arguments.RegisterArgument(this);
        }
        
        protected string Value
        {
            get
            {
                if (!initialised)
                {
                    value = Arguments.ReadArgument(Prefixes);
                    if (value == null)
                    {
                        value = defaultValue;
                    }
                    initialised = true;
                }
                return value;
            }
        }
    }
}
