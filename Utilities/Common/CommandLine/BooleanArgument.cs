using System;
using System.Collections.Generic;
using System.Text;

namespace Common.CommandLine
{
    public class BooleanArgument : StringArgument 
    {
        //bool defaultValue;
        //bool initialised = false;
        public BooleanArgument(string[] prefixes, bool defaultValue) : base(prefixes, defaultValue.ToString())
        {
        }

        public new bool Value
        {
            get
            {
                return bool.Parse(base.Value);
            }
        }

        
    }
}
