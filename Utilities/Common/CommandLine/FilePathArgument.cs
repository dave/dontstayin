using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Common.CommandLine
{
    public class FilePathArgument : StringArgument
    {
        public FileInfo File
        {

            get
            {
                return new FileInfo(base.Value);
            }
        }
        public FilePathArgument(string[] prefixes, string defaultPath, string errorMessage)
            : base(prefixes, defaultPath)
        {
        }
    }
}
