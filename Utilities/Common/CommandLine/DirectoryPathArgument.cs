using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Common.CommandLine
{
    public class DirectoryPathArgument : StringArgument
    {
        DirectoryInfo directory;
        bool initialised = false;
        public DirectoryInfo Directory
        {
            get
            {
                if (!initialised)
                {
                    try
                    {
                        directory = new DirectoryInfo(base.Value);
                    }
                    catch
                    {
                        directory = null;
                    }
                    initialised = true;
                }
                return directory;
            }
        }
        public DirectoryPathArgument(string[] prefixes, string defaultPath)
            : base(prefixes, defaultPath)
        {
        }
    }
}
