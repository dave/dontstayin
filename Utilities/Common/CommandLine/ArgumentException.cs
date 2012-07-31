using System;
using System.Collections.Generic;
using System.Text;

namespace Common.CommandLine
{
    public class ArgumentException : Exception 
    {
        public ArgumentException(string message)
            : base(message)
        {

        }
    }
}
