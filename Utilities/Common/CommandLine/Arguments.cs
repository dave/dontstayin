using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Common.CommandLine
{
    internal class Arguments
    {
        static List<StringArgument> arguments = new List<StringArgument>();
        static internal Dictionary<string, string> values = new Dictionary<string, string>();
        static bool argumentsHaveNotBeenRead = true;
        static internal Dictionary<string, string> Values
        {
            get
            {
                if (argumentsHaveNotBeenRead)
                {
                    PopulateValues();
                    argumentsHaveNotBeenRead = false;
                }
                return values;

            }
        }
            
        
        internal static string ReadArgument(string[] prefixes)
        {

            foreach (string prefix in prefixes)
            {
                if (Values[prefix] != null)
                {
                    return Values[prefix];
                }
            }
            return null;
        }
        

        private static void PopulateValues()
        {
            foreach (string s in System.Environment.GetCommandLineArgs())
            {
                if (s.Substring(0, 1) != "/")
                {
                    continue;
                }
                try
                {
                    string prefix = s.Substring(1, s.IndexOf(':') - 1);
                    string value = s.Substring(s.IndexOf(':') + 1);
                    values[prefix] = value;
                }
                catch
                {
                    throw new ArgumentException("Argument provided was '" + s + ". 'Arguments must be in the format '/prefix:value'");
                }
            }
        }
        internal static void RegisterArgument(StringArgument arg)
        {
            arguments.Add(arg);
            RegisterPrefixes(arg.Prefixes);
        }
        static void RegisterPrefixes(string[] prefixes)
        {
            foreach (string prefix in prefixes)
            {
                if (values.ContainsKey(prefix))
                {
                    throw new ArgumentException(String.Format("Cannot register prefix {0} as it is in use by more than one argument!", prefix));
                }
                else
                {
                    values.Add(prefix, null);
                }
            }
        }
        
    }
}
