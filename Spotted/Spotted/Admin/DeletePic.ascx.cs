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
using System.IO;

namespace Spotted.Admin
{
	public partial class DeletePic : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Usr.Current.IsAdmin)
				throw new Exception("");

			Usr u = new Usr(ContainerPage.Url["UsrK"]);
			u.DeleteProfilePic();
			Mailer sm = new Mailer();
			sm.UsrRecipient = u;
			sm.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
			sm.Body = "<p>Your profile photo has been deleted</p>";
			sm.Body += "<p>This could have been for one of several reasons:</p>";
			sm.Body += "<ul>";
			sm.Body += "<li>Profile pictures <b>MUST BE OF YOUR FACE</b>.</li>";
			sm.Body += "<li>Profile pictures must show your face clearly - make sure you <b>ZOOM IN</b>.</li>";
			sm.Body += "<li>The face must match your sex";
			if (u.IsMale || u.IsFemale)
				sm.Body += " (you currently have <b>" + (u.IsMale ? "MALE" : "FEMALE") + "</b> selected as your sex)";
			sm.Body += " - see the <a href=\"[LOGIN(/pages/mydetails)]\">My details</a> page to change your selected sex.</li>";
			sm.Body += "</ul>";
			sm.Body += "<p>You can create a new profile picture on the <a href=\"[LOGIN(/pages/mypicture)]\">Create my picture</a> page.</p>";
			sm.Subject = "Your DontStayIn profile photo has been deleted";
			sm.RedirectUrl = "/pages/mypicture";
			sm.Send();

			Response.Redirect(u.Url());
		}
	}
}
