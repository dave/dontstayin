using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
namespace MSBuildTasks.NetworkLoadBalancing
{
    public abstract class NlbTask : Task
    {
        [Required]
        public string NlbClusterAddress { get; set; }
        [Required]
        public string ServerNumber { get; set; }
        [Required]
        public string NlbPassword { get; set; }

        protected abstract string MessageIndicatingSuccess{get;}
        protected abstract string NlbAction { get; }

        public override bool Execute()
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(@"c:\windows\system32\nlb.exe", String.Format(@"{3} {0}:{1} /passw {2}", NlbClusterAddress, ServerNumber, NlbPassword, NlbAction));
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            proc.StartInfo = startInfo;
            proc.Start();
            proc.WaitForExit();
            string output = proc.StandardOutput.ReadToEnd();

            if (output.Contains(MessageIndicatingSuccess))
            {
                this.Log.LogMessage(output);
                return true;
            }
            else
            {
                this.Log.LogError(output);
                return false;
            }
        }
    }
}
