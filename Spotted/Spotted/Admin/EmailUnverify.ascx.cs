using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using System.Text.RegularExpressions;

namespace Spotted.Admin
{
	public partial class EmailUnverify : AdminUserControl
	{
		
		public void Process_Click(object o, System.EventArgs e)
		{
			Cambro.Web.Helpers.WriteAlertHeader();

			ArrayList matchedEmails = new ArrayList();
			string[] arr = Regex.Replace(EmailsTextBox.Text, @"[\s,]+", @" ").Split(' ');
			foreach (string c in arr)
			{
				if (Regex.Match(c, Cambro.Misc.RegEx.Email).Success)
				{
					if (!matchedEmails.Contains(c.ToLower().Trim()))
						matchedEmails.Add(c.ToLower().Trim());
				}
			}
			Random r = new Random();
			int done = 0;
			int skipped = 0;
			foreach (string c in matchedEmails)
			{
				UsrSet us = new UsrSet(new Query(new Q(Usr.Columns.Email, c.ToLower())));
				if (us.Count == 1)
				{
					if (us[0].IsEmailVerified)
					{
						us[0].IsEmailVerified = false;
						us[0].LoginString = Cambro.Misc.Utility.GenRandomText(6, r);
						us[0].Update();
						Cambro.Web.Helpers.WriteAlert("Done " + c + " - " + us[0].NickName, 1);
						done++;
					}
				}
				else
				{
					skipped++;
					Cambro.Web.Helpers.WriteAlert("Can't find " + c, 2);
				}
			}
			Cambro.Web.Helpers.WriteAlert("Done " + done, 3);
			Cambro.Web.Helpers.WriteAlert("Skipped " + skipped, 4);


		}
	}
}
