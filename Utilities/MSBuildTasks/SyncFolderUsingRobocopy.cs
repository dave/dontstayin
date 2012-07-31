using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
namespace MSBuildTasks
{
   
    public class SyncFolderUsingRobocopy : Microsoft.Build.Utilities.Task
    {
        const string PathToRobocopy = @"C:\Windows\Robocopy.exe";
        /// <summary>
        /// Source directory 
        /// </summary>
        /// <remarks>
        /// You can use drive:\path or \\server\share\path
        /// </remarks>
        [Required]
        public string SourceFolder { get; set; }


        /// <summary>
        /// Destination directory 
        /// </summary>
        /// <remarks>
        /// You can use drive:\path or \\server\share\path
        /// </remarks>
        [Required]
        public string DestinationFolder { get; set; }


        public string ExtraArgs { get; set; }

        
        static string GetMessage(int errorLevel){
            StringBuilder sb = new StringBuilder();
            if ((errorLevel & 16) == 16) sb.Append("Serious error. Robocopy did not copy any files. This is either a usage error or an error due to insufficient access privileges on the source or destination directories.");
            if ((errorLevel &  8) ==  8) sb.Append("Some files or directories could not be copied (copy errors occurred and the retry limit was exceeded). Check these errors further.");
            if ((errorLevel &  4) ==  4) sb.Append("Some Mismatched files or directories were detected. Examine the output log. Housekeeping is probably necessary.");
            if ((errorLevel &  2) ==  2) sb.Append("Extra files or directories were detected. Examine the output log. Some housekeeping may be needed.");
            if ((errorLevel &  1) ==  1) sb.Append("One or more files were copied successfully (that is, new files have arrived).");
            if (errorLevel == 0) sb.Append("No errors occurred, and no copying was done. The source and destination directory trees are completely synchronized.");
            return sb.ToString();
        }
        
        


        public override bool Execute(){
            System.Diagnostics.Process proc = new System.Diagnostics.Process();


            if (ExtraArgs == null)
            {
                ExtraArgs = "";
            }
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(PathToRobocopy, String.Format("{0} {1} /MIR /r:10 /w:3 /NP {2}", SourceFolder, DestinationFolder, ExtraArgs));
            //startInfo.RedirectStandardError = true;
            //startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            proc.StartInfo = startInfo;
            proc.Start();
            proc.WaitForExit();
            
            if (proc.ExitCode >= 8)
            {
                this.Log.LogError(GetMessage(proc.ExitCode));
                return false;
            }
            else
            {
                   this.Log.LogMessage(GetMessage(proc.ExitCode));
                return true;
            }
           

     
        }
    }
}
