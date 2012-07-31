using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using UnitTestUtilities.Web;
namespace MSBuildTasks
{
    public class MakeWebRequest : Task
    {
        [Required] public string Url { get; set; }
        [Required] public int TimeoutInSeconds { get; set; }
        [Required] public string TextThatMustBeInResponse { get; set; }
        [Required] public int MaxNumberOfAttempts { get; set; }
        public override bool Execute()
        {
            int numberOfAttemptsMade = 0;
            while (numberOfAttemptsMade < MaxNumberOfAttempts)
            {
                try
                {
                    WebRequest request = new WebRequest(Url, TimeoutInSeconds * 1000);

                    if (request.Response.Contains(TextThatMustBeInResponse))
                    {
                        if (this.BuildEngine != null) Log.LogMessage("Attempt " + (numberOfAttemptsMade + 1) + ": Success - text '{0}' found at {1}", TextThatMustBeInResponse, Url);
                        return true;
                    }
                    else
                    {
                        if (this.BuildEngine != null) throw new Exception(String.Format("Text '{0}' not found at {1}", TextThatMustBeInResponse, Url));
                    }
                }
                catch (Exception ex)
                {

                    Log.LogError("Attempt " + (numberOfAttemptsMade + 1) + ": Error occured accessing '{0}':\n{1}", this.Url, ex.ToString());
                    this.Log.LogErrorFromException(ex);
                }
                numberOfAttemptsMade++;
            }
            return false;
            
        }
    }
}
