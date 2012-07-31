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

namespace Spotted.Admin
{
	public partial class MultiDelete : AdminUserControl
	{
		protected Button DeleteButton;
		protected DropDownList ObjectTypeDropDown;
		protected TextBox ObjectKTextBox;
		protected Label DoneLabel;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			DeleteButton.Attributes["onclick"] = "return confirm('Are you sure?');";
			if (!Page.IsPostBack)
			{
				if (Request.QueryString["ObjectType"] != null)
					ObjectTypeDropDown.SelectedValue = Request.QueryString["ObjectType"];
				if (Request.QueryString["ObjectK"] != null)
					ObjectKTextBox.Text = Request.QueryString["ObjectK"];
			}

		}
		public void DeleteNow(object o, System.EventArgs e)
		{
			if (ObjectTypeDropDown.SelectedValue.Equals("Photo"))
			{
				Photo c = new Photo(int.Parse(ObjectKTextBox.Text));
				SendEmail(c);
				Delete.DeleteAll(c);
			}
			else if (ObjectTypeDropDown.SelectedValue.Equals("Gallery"))
			{
				Gallery c = new Gallery(int.Parse(ObjectKTextBox.Text));
				SendEmail(c);
				Delete.DeleteAll(c);
			}
			else if (ObjectTypeDropDown.SelectedValue.Equals("Venue"))
			{
				Venue c = new Venue(int.Parse(ObjectKTextBox.Text));
				SendEmail(c);
				Delete.DeleteAll(c);
			}
			else if (ObjectTypeDropDown.SelectedValue.Equals("Event"))
			{
				Event c = new Event(int.Parse(ObjectKTextBox.Text));
				SendEmail(c);
				Delete.DeleteAll(c);
			}
			else if (ObjectTypeDropDown.SelectedValue.Equals("Comment"))
			{
				Comment c = new Comment(int.Parse(ObjectKTextBox.Text));
				SendEmail(c);
				Delete.DeleteAll(c);
			}
			else if (ObjectTypeDropDown.SelectedValue.Equals("Thread"))
			{
				Thread c = new Thread(int.Parse(ObjectKTextBox.Text));
				SendEmail(c);
				Delete.DeleteAll(c);
			}
			else if (ObjectTypeDropDown.SelectedValue.Equals("Usr"))
			{
				Usr c = new Usr(int.Parse(ObjectKTextBox.Text));
				SendEmail(c);
				Delete.DeleteAll(c);
			}
			else if (ObjectTypeDropDown.SelectedValue.Equals("Article"))
			{
				Article c = new Article(int.Parse(ObjectKTextBox.Text));
				SendEmail(c);
				Delete.DeleteAll(c);
			}
			DoneLabel.Visible = true;
		}
		public void SendEmail(object o)
		{
			//try
			//{
			//    Mailer admin = new Mailer();
			//    admin.TemplateType = Mailer.TemplateTypes.AdminNote;
			//    admin.Subject = "Multi delete - by " + Usr.Current.NickName;
			//    admin.Body = "<p>" + ((IPage)o).Url() + "</p>";
			//    admin.To = "dave@dontstayin.com";
			//    admin.Send();
			//}
			//catch { }
		}
	}
}
