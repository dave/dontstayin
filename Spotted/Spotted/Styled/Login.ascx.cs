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
using Common;

namespace Spotted.Styled
{
	public partial class Login : StyledUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			throw new Exception("Styled pages disabled.");



			// Compensate for data insert error being off by a few milliseconds, AddSeconds(1)
			if (Usr.Current != null && Usr.Current.DateTimeLastAccess.AddSeconds(1) >= Visit.Current.DateTimeStart)
				Proceed();

			JavascriptLabel.Text = "";
			if (!this.IsPostBack)
				JavascriptLabel.Text = "<script>document.getElementById('LoginDiv').style.display = 'none';document.getElementById('RegisterDiv').style.display = 'none';</script>";

			AlreadySignedInP.Visible = Usr.Current != null;
			if (Usr.Current != null)
			{
				UserFirstNameLabel.Text = Usr.Current.FirstName;
			}

			this.RegistrationEmailRegularExpressionValidator.ValidationExpression = Cambro.Misc.RegEx.Email;
			
		}	

		protected void LoginButton_Click(object sender, EventArgs e)
		{
			if (LoginEmailTextBox.Text.Trim().Length < 5)
			{
				LoginCustomValidator.ErrorMessage = "Invalid email address";
				LoginCustomValidator.IsValid = false;
			}
			else if (LoginPasswordTextBox.Text.Trim().Length < 4)
			{
				LoginCustomValidator.ErrorMessage = "Email and password do not match our records";
				LoginCustomValidator.IsValid = false;
			}
			else if (Page.IsValid)
			{
				// Validate email and password
				UsrSet us = new UsrSet(new Query(new Or(new Q(Usr.Columns.NickName, LoginEmailTextBox.Text.Trim()),
														new Q(Usr.Columns.Email, LoginEmailTextBox.Text.Trim()))));
				if (us.Count == 1 && us[0].CheckPassword(LoginPasswordTextBox.Text.Trim()))
				{
				//	us[0].LogInAsThisUser(true);
					Proceed();
				}
				else
				{
					// invalidate
					LoginCustomValidator.ErrorMessage = "Email and password do not match our records";
					LoginCustomValidator.IsValid = false;
				}
			}
		}

		protected void RegistrationButton_Click(object sender, EventArgs e)
		{ 
			// Validate registration details
			List<string> errors = new List<string>();
			//Email below 100 chars
			if (RegistrationEmailTextBox.Text.Trim().Length > 100)
				errors.Add("Email should be 100 chars or less");

			//Email >= 1 chars
			if (RegistrationEmailTextBox.Text.Trim().Length < 5)
				errors.Add("Enter email address");

			//Name >= 1 chars
			if (Cambro.Web.Helpers.StripHtml(RegistrationFirstNameTextBox.Text).Trim().Length < 1)
				errors.Add("Enter first name");

			//Name >= 1 chars
			if (Cambro.Web.Helpers.StripHtml(RegistrationLastNameTextBox.Text).Trim().Length < 1)
				errors.Add("Enter last name");

			//Password <= 20 chars
			if (RegistrationPasswordTextBox.Text.Trim().Length > 20)
				errors.Add("Password should be 20 chars or less");

			//Password >= 4 chars
			if (RegistrationPasswordTextBox.Text.Trim().Length < 4)
				errors.Add("Password should be 4 chars or more");

			if(RegistrationPasswordTextBox.Text != RegistrationConfirmPasswordTextBox.Text)
				errors.Add("Passwords do not match");

			if (errors.Count > 0)
			{
				string errorOutput = errors[0];
				for (int i = 1; i < errors.Count; i++)
				{
					errorOutput += "<li>" + errors[i] + "</li>";
				}
				RegistrationCustomValidator.ErrorMessage = errorOutput;
				RegistrationCustomValidator.IsValid = false;
			}
			else if (Page.IsValid)
			{
				//Check email is not in the database already
				UsrSet us = new UsrSet(new Query(new Q(Usr.Columns.Email, RegistrationEmailTextBox.Text.Trim())));
				if (us.Count > 0)
				{
					if (us[0].CheckPassword(RegistrationPasswordTextBox.Text.Trim()))
					{
						// Login
			//			us[0].LogInAsThisUser(true);
						Proceed();
					}
					else if (us[0].IsSkeleton && !us[0].IsTicketsRegistered)
					{
						// Register user
						us[0].FirstName = Cambro.Web.Helpers.StripHtml(RegistrationFirstNameTextBox.Text).Trim();
						us[0].LastName = Cambro.Web.Helpers.StripHtml(RegistrationLastNameTextBox.Text).Trim();
						us[0].IsTicketsRegistered = true;
						us[0].Update();
		//				us[0].LogInAsThisUser(true);
						Proceed();
					}
					else
					{
						// That email has already been registered on DontStayIn with a different password
						RegistrationCustomValidator.ErrorMessage = "That email address has already been registered on DontStayIn, with a different password.";
						RegistrationCustomValidator.IsValid = false;
					}
				}
				else
				{

					//this whole page is disabled
					//Usr u = Usr.CreateNewUsr(RegistrationEmailTextBox.Text, RegistrationPasswordTextBox.Text.Trim(), RegistrationFirstNameTextBox.Text,
					//						RegistrationLastNameTextBox.Text, HttpContext.Current != null ? Utilities.TruncateIp(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]) : "");
					
					//u.IsTicketsRegistered = true;
					
					//u.Update();
					//u.SendWelcomeEmail(null, null, "");
					//#region Log the user in to the site
					//u.LogInAsThisUser(true);
					//#endregion

					Proceed();
				}				
			}
		}

		protected void ContinueButton_Click(object sender, EventArgs e)
		{ 
			// Proceed to payment page
	//		Usr.Current.LogInAsThisUser(true);
			Proceed();
		}

		private void Proceed()
		{
			if (Request.QueryString["url"] != null && Request.QueryString["url"] != "")
			{
				Response.Redirect(Request.QueryString["url"]);
			}
			else
			{
				Response.Redirect(StyledObject.UrlStyled());
			}
		}

		public void TermsVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = TermsCheckBox.Checked;
			JavascriptLabel.Text = "<script>document.getElementById('LoginDiv').style.display = 'none';document.getElementById('RegisterDiv').style.display = 'block';</script>";
		}
	}
}
