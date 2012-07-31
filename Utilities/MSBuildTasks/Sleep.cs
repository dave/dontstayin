using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Threading;

namespace MSBuildTasks
{
    public class Sleep : Task
    {
        public int NumberOfSeconds { get; set; }

        public override bool Execute()
        {
            Thread.Sleep(NumberOfSeconds * 1000);
            return true;
        }
    }
}
