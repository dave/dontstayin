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
using System.IO;

namespace Spotted.Admin
{
	public partial class FlyerEdit : AdminUserControl
	{
		// annoyingly, generator often fails on these controls
		//protected global::JsWebControls.HtmlAutoComplete uiPromotersAutoComplete;
		//protected global::JsWebControls.HtmlAutoComplete uiEvent;
		//protected global::JsWebControls.HtmlAutoComplete uiBrandEvent;

		int FlyerK
		{
			get { return (uiFlyerKLabel.Text == "<new>") ? 0 : int.Parse(uiFlyerKLabel.Text); }
			set { uiFlyerKLabel.Text = (value > 0) ? value.ToString() : "<new>"; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadFlyer(ContainerPage.Url["k"].ValueInt);
				uiPlaceTargettingButton.Attributes["onclick"] = "openPopupFocusSize('/popup/bannereditlocation?placek=' + escape(document.getElementById('" + uiPlaceTargettingHidden.ClientID + "').value), 500, 600);return false;";
				uiMusicTargettingButton.Attributes["onclick"] = "openPopupFocusSize('/popup/bannereditmusic?ShowMajorMusicTypesOnly=true&musictypek=' + escape(document.getElementById('" + uiMusicTargettingHidden.ClientID + "').value), 500, 600);return false;";
			}
			SetTargettingTexts();
		}

		private void LoadFlyer(int flyerK)
		{
			if (flyerK > 0) 
				LoadFlyer(new Flyer(flyerK));
			else
				LoadFlyer(new Flyer());
		}
		protected CheckBox uiIsHtml;
		protected TextBox uiHtml, uiTextAlternative;
		private void LoadFlyer(Flyer f)
		{
			FlyerK = f.K;
			uiNameTextBox.Text = f.Name;
			uiSubjectTextBox.Text = f.Subject;
			uiFromDisplayNameTextBox.Text = f.MailFromDisplayName;
			uiBackgroundColor.Text = f.BackgroundColor;
			uiUrlTextBox.Text = f.LinkTargetUrl;
			uiSendTime.Time = f.SendDateTime;
			uiSendDate.Date = f.SendDateTime;
			uiPromotersAutoComplete.Value = f.PromoterK.ToString();
			uiPromotersAutoComplete.Text = (f.PromoterK > 0) ? f.Promoter.Name : "";

			uiPlaceTargettingHidden.Value = f.PlaceKs;
			uiMusicTargettingHidden.Value = f.MusicTypeKs;
			uiPromotersOnly.Checked = f.PromotersOnly;
			uiIsHtml.Checked = f.IsHtml;
			uiHtml.Text = f.Html;
			uiTextAlternative.Text = f.TextAlternative;


			if (!string.IsNullOrEmpty(f.EventKs))
			{
				uiEvents.Text = string.Join("<br />",
					f.EventKs.CommaSeparatedValuesToIntList().ConvertAll(k => new Event(k)).ConvertAll(e => e.VerboseInfo).ToArray());
				uiEventKs.Value = f.EventKs;
			}
			SetTargettingTexts();

			if (f.MiscK > 0)
			{
				uiPreviewFile.NavigateUrl = f.Misc.Url();
				uiPreviewFile.Target = "_blank";
				uiPreviewFile.Visible = true;
				
			}
			else
			{
				uiPreviewFile.Visible = false;
			}

			if (f.MiscK > 0 || f.IsHtml)
			{
				uiTestButton.Visible = true;
				uiTestEmail.Visible = true;
				uiTestEmail.Text = Usr.Current.Email;
			}
			else
			{
				uiTestButton.Visible = false;
				uiTestEmail.Visible = false;
			}

			if (!f.IsEditable)
			{
				uiSaveButton.Enabled = false;
			}
		}

		private void SetTargettingTexts()
		{
			int places = (uiPlaceTargettingHidden.Value == "") ? 0 : uiPlaceTargettingHidden.Value.Split(',').Length;
			uiPlaceTargettingTextBox.Text = (places == 0 ? "all" : places.ToString()) + " town" + (places == 1 ? "" : "s");

			int musicTypes = (uiMusicTargettingHidden.Value == "" || uiMusicTargettingHidden.Value == "1") ? 0 : uiMusicTargettingHidden.Value.Split(',').Length;
			uiMusicTargettingTextBox.Text = (musicTypes == 0 ? "all" : musicTypes.ToString()) + " music type" + (musicTypes == 1 ? "" : "s");
		}

		protected void CalculateUsrBase(object sender, EventArgs e)
		{
			uiUsrBaseCountLabel.Text = Flyer.CountUsrs(uiEventKs.Value, uiPlaceTargettingHidden.Value, uiMusicTargettingHidden.Value, uiPromotersOnly.Checked).ToString("N0") + " potential users";
		}
		#region Save

		protected void Save(object sender, EventArgs e)
		{
			if (!Page.IsValid)
			{
				return;
			}

			try
			{
				Misc m = SaveMisc();

				Flyer f;
				if (FlyerK > 0)
				{
					f = new Flyer(FlyerK);
				}
				else
				{
					f = new Flyer();
				}

				f.Name = uiNameTextBox.Text;
				if (m.K > 0) f.MiscK = m.K;
				f.LinkTargetUrl = uiUrlTextBox.Text.Trim();
				f.Subject = uiSubjectTextBox.Text;
				f.MailFromDisplayName = uiFromDisplayNameTextBox.Text.Trim();
				f.BackgroundColor = uiBackgroundColor.Text;
				f.PromoterK = int.Parse(uiPromotersAutoComplete.Value);
				f.SendDateTime = uiSendDate.Date.AddHours(uiSendTime.Time.Hour).AddMinutes(uiSendTime.Minute);
				f.PlaceKs = uiPlaceTargettingHidden.Value;
				f.MusicTypeKs = uiMusicTargettingHidden.Value;
				f.PromotersOnly = uiPromotersOnly.Checked;
				f.IsHtml = uiIsHtml.Checked;
				f.Html = uiHtml.Text;
				f.TextAlternative = uiTextAlternative.Text;
				f.EventKs = uiEventKs.Value;
				f.Update();

				uiSavedLabel.Text = "Saved as Flyer #" + f.K;
				uiSavedLabel.ForeColor = System.Drawing.Color.Black;

				LoadFlyer(f);
			}
			catch (Exception ex)
			{
				this.uiSavedLabel.Text = "ERROR: " + ex.Message;
				this.uiSavedLabel.ForeColor = System.Drawing.Color.Red;
			}
		}

		private Misc SaveMisc()
		{
			Misc m = new Misc();

			if (uiInputFile.PostedFile.FileName.Length == 0)
				return m;

			m.Guid = Guid.NewGuid();

			SetAndCheckExtension(m);

			m.UsrK = Usr.Current.K;
			m.PromoterK = int.Parse(uiPromotersAutoComplete.Value);
			m.DateTime = DateTime.Now;
			m.Size = uiInputFile.PostedFile.ContentLength;
			m.Name = uiInputFile.PostedFile.FileName.Substring(uiInputFile.PostedFile.FileName.LastIndexOf("\\") + 1);

			byte[] bytes = new byte[uiInputFile.PostedFile.InputStream.Length];
			uiInputFile.PostedFile.InputStream.Read(bytes, 0, (int)uiInputFile.PostedFile.InputStream.Length);

			SetAndCheckWidthAndHeight(m, bytes);

			m.Update();

			try
			{
				Storage.AddToStore(bytes, Storage.Stores.Pix, m.Guid, m.Extention, m, "");
			}
			catch (Exception ex)
			{
				m.Delete();
				throw ex;
			}

			return m;

		}

		private void SetAndCheckExtension(Misc m)
		{
			if (uiInputFile.PostedFile.FileName.IndexOf(".") == -1)
				m.Extention = "";
			else
				m.Extention = uiInputFile.PostedFile.FileName.Substring(uiInputFile.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();

			if (m.Extention.Equals("jpeg") || m.Extention.Equals("jpe"))
				m.Extention = "jpg";

			if (!(m.Extention.Equals("jpg") || m.Extention.Equals("gif")))
				throw new DsiUserFriendlyException("You can only upload gif or jpg files with this page.");
		}

		private static void SetAndCheckWidthAndHeight(Misc m, byte[] bytes)
		{
			using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(bytes)))
			{
				m.Width = image.Width;
				m.Height = image.Height;
			}
		}
		#endregion


		protected void AddEventToEvents(object o, EventArgs e)
		{
			if (!string.IsNullOrEmpty(uiEvent.Value))
			{
				this.AddEvent(this.uiEvent.Text, int.Parse(this.uiEvent.Value));
				uiEvent.Text = "";
				uiEvent.Value = "";
			}
		}

		protected void AddBrandEventsToEvents(object o, EventArgs e)
		{
			if (!string.IsNullOrEmpty(uiBrand.Value))
			{
				Brand b = new Brand(int.Parse(uiBrand.Value));
				foreach (var ev in b.Events)
				{
					AddEvent(ev.VerboseInfo, ev.K);
				}
			}
		}

		private void AddEvent(string eventText, int eventK)
		{
			if (string.IsNullOrEmpty(this.uiEvents.Text))
			{
				this.uiEvents.Text = eventText;
				this.uiEventKs.Value = eventK.ToString();
			}
			else
			{
				this.uiEvents.Text += "<br />" + eventText;
				this.uiEventKs.Value += "," + eventK.ToString();
			}
		}


		#region Test
		protected void Test(object sender, EventArgs e)
		{
			if (uiTestEmail.Text == Usr.Current.Email)
			{
				Query q = new Query();
				q.QueryCondition = new Q(Usr.Columns.K, Usr.Current.K);
				UsrSet currentUsrSet = new UsrSet(q);
				new Flyer(FlyerK).SendMail(currentUsrSet, false, 0);
			}
			else
			{
				Query q = new Query();
				q.QueryCondition = new Q(Usr.Columns.K, 8);
				UsrSet dummyUsrSet = new UsrSet(q);
				dummyUsrSet[0].Email = uiTestEmail.Text;
				dummyUsrSet[0].LoginString = "";

				new Flyer(FlyerK).SendMail(dummyUsrSet, false, 0);
			}
			uiTestEmailSuccess.Text = "Sent! to " + uiTestEmail.Text;
		}
		#endregion
	}
}
