using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using System.Text;
using Spotted.com.royalmail.epro.www1;
using Spotted.com.royalmail.epro.www;

namespace Spotted.Blank
{
	public partial class MailingReport : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotAdmin("Only admin!");
		}

		#region GetReport
		protected void GetReport(object sender, EventArgs eventArgs)
		{

			#region Get summary report
			EProExportService export = new EProExportService();
			Spotted.com.royalmail.epro.www1.Authentication authExport = new Spotted.com.royalmail.epro.www1.Authentication();
			authExport.Version = "1.0.0.1";
			
			Bobs.Global glo = new Bobs.Global(Bobs.Global.Records.RoyalMailEproPassword);

			authExport.AccessCode = 10702;
			authExport.Username = "dave@dontstayin.com";
			authExport.Password = glo.ValueString;
			
			export.AuthenticationValue = authExport;
			Export[] exportList = export.GetExportList();
			if (true)
			{
//				Response.Write("<pre>");
//				foreach (Export e in exportList)
//				{
//					Response.Write(e.Reference + " " + e.Name + " (" + e.Description + ")\n");
//				}
//				Response.Write("</pre>");
			}
			
			GenerateExportInput exportRequest = new GenerateExportInput();
			//exportRequest.ExportReference = new Guid("3a33ebb0-6a78-4bec-b703-9c73531ad46a");
			exportRequest.ExportReference = new Guid("3a33ebb0-6a78-4bec-b703-9c73531ad46a");
			exportRequest.OutputType = ExportOutputTypes.html;


			MultiSelectParameter parameter1 = new MultiSelectParameter();
			parameter1.Name = "CustomerIdList";
		//	if (Vars.DevEnv)
		//		parameter1.Values = new string[] { "3325" }; //test account
		//	else
				parameter1.Values = new string[] { "22527" }; //dsi account

			MultiSelectParameter parameter2 = new MultiSelectParameter();
			parameter2.Name = "ServiceIdList";
			parameter2.Values = new string[] { "7111", "7107", "200", "3005" };

			DateTimeParameter parameter3 = new DateTimeParameter();
			parameter3.Name = "StartDate";
			parameter3.Value = DateTime.Today.AddDays(0);

			DateTimeParameter parameter4 = new DateTimeParameter();
			parameter4.Name = "EndDate";
			parameter4.Value = DateTime.Today.AddDays(1);

			BooleanParameter parameter5 = new BooleanParameter();
			parameter5.Name = "PosterDetails";
			parameter5.Value = true;

			BooleanParameter parameter6 = new BooleanParameter();
			parameter6.Name = "DocketDetails";
			parameter6.Value = true;

			SingleSelectParameter parameter7 = new SingleSelectParameter();
			parameter7.Name = "ApprovalState";
			parameter7.Value = "3"; //1=Approved, 2=Unapproved, 3=Both

			BooleanParameter parameter8 = new BooleanParameter();
			parameter8.Name = "RPSiteComments";
			parameter8.Value = false;

			BooleanParameter parameter9 = new BooleanParameter();
			parameter9.Name = "PosterComments";
			parameter9.Value = false;

			exportRequest.Parameters = new ExportParameter[] { parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9 };
			GenerateExportOutput report;
			try
			{
				report = export.GenerateExport(exportRequest);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			#endregion

			if (report.ExtraParametersRequired!=null && report.ExtraParametersRequired.Length > 0)
			{
				foreach (RequiredParameter rp in report.ExtraParametersRequired)
				{
					Response.Write("<b>" + rp.Name + " (" + rp.ParameterType + ")</b> ");
					if (rp.PossibleValues != null)
					{
						foreach (PossibleValue pv in rp.PossibleValues)
							Response.Write("<br>" + pv.Value + " (" + pv.Description + ")");
					}
				}
				return;
			}


			#region Output report to page
			Response.Clear();
		//	Response.Write("<div style=\"page-break-after:always;\">");
			Response.Write(report.ExportData.InnerText);
		//	Response.Write("</div>");
			Response.End();
			#endregion

		}
		#endregion

		#region GetEpro()
		public EProImportService GetEpro()
		{
			EProImportService mail = new EProImportService();
			Spotted.com.royalmail.epro.www.Authentication auth = new Spotted.com.royalmail.epro.www.Authentication();
			auth.Version = "1.0.0.1";
			if (Vars.DevEnv)
			{
				auth.AccessCode = 47;
				auth.Username = "manager";
				auth.Password = "Demonstrate1";
			}
			else
			{
				Bobs.Global glo = new Bobs.Global(Bobs.Global.Records.RoyalMailEproPassword);

				auth.AccessCode = 10702;
				auth.Username = "dave@dontstayin.com";
				auth.Password = glo.ValueString;
			}
			mail.AuthenticationValue = auth;
			return mail;
		}
		#endregion

		#region UpdatePass
		protected void UpdatePass(object sender, EventArgs eventArgs)
		{
			Bobs.Global glo = new Bobs.Global(Bobs.Global.Records.RoyalMailEproPassword);
			glo.ValueString = Pass.Text;
			glo.Update();
			Pass.Text="";
		}
		#endregion

	}
}
