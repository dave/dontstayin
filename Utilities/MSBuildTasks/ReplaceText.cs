using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
namespace MSBuildTasks
{
    public class ReplaceText : Task
    {
        [Required] public string FilePath { get; set; }
        [Required] public string OldText { get; set; }
        [Required] public string NewText { get; set; }

        public override bool Execute()
        {
            string s = System.IO.File.ReadAllText(FilePath);
            System.IO.File.WriteAllText(FilePath, s.Replace(OldText, NewText));
            return true;
        }
    }
}
