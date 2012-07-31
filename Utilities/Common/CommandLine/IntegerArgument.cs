using System;
using System.Collections.Generic;
using System.Text;

namespace Common.CommandLine
{
    public class IntegerArgument : StringArgument 
    {
        public IntegerArgument(string[] prefixes, int defaultValue)
            : base(prefixes, defaultValue.ToString())
        {
        }

        public new int Value
        {
            get
            {
                return int.Parse(base.Value);
            }
        }
    }
}
