using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.TeamFoundation.Client;
//using System.Collections;

namespace SpottedLibrary.Admin.ReportABug
{
	class ReportABugService
	{
		
		internal void AddNewWorkItem(string serverName, string projectName, string type, string title, string desc, params KeyValuePair<string, string>[] otherFields)
		{
			
			////TeamFoundationServer tfs = TeamFoundationServerFactory.GetServer(serverName);
			////WorkItemStore store = (WorkItemStore)tfs.GetService(typeof(WorkItemStore));
			////Project project = store.Projects[projectName];
			////WorkItemType wit = project.WorkItemTypes[type];
			////WorkItem wi = new WorkItem(wit);
			
			////wi.Title = title;
			////StringBuilder sb = new StringBuilder();
			////sb.AppendLine(desc);
			////foreach (var pair in otherFields)
			////{
			////    sb.AppendLine(pair.Key + " : " + pair.Value);
			////}
			////wi.Description = sb.ToString();
			////FieldValidityCheck(wi);
			////wi.Save();

		}

		/// <summary>
		/// Make sure all fields are valid before saving
		/// </summary>
		//private static void FieldValidityCheck(WorkItem wi)
		//{
		//    ArrayList invalidFields = wi.Validate();

		//    if (invalidFields.Count != 0)
		//    {
		//        StringBuilder sb = new StringBuilder();
		//        sb.AppendLine("Invalid fields in workitem");
		//        foreach (Field f in invalidFields)
		//        {
		//            sb.AppendFormat("Invalid Field '{0}': {1}\r\n", f.Name, f.Status.ToString());
		//            sb.AppendFormat("Current Value: '{0}'\r\n", f.Value);
		//        }
		//        throw new Exception(sb.ToString());
		//    }
		//} 

	}


}
