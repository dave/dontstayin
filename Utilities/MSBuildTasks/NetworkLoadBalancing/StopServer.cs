using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
namespace MSBuildTasks.NetworkLoadBalancing
{
    public class StopServer : NlbTask
    {
        protected override string MessageIndicatingSuccess
        {
            get { return "reported: cluster mode stopped"; }
        }

        protected override string NlbAction
        {
            get { return "stop"; }
        }
    }
}
