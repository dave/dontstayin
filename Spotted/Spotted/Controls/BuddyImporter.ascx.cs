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
using SpottedLibrary.Controls.BuddyImporter;
using Bobs;

namespace Spotted.Controls
{
	public partial class BuddyImporter : EnhancedUserControl, IBuddyImporterView, IBuddyImporter
	{
		#region create Controller
		BuddyImporterController controller;
		public BuddyImporter()
		{
			controller = new BuddyImporterController(this, new BuddyImporterService());
		}
		#endregion

		#region IView Members
		public bool IsValid
		{
			get { return Page.IsValid; }
		}
		#endregion

		#region Page_Init
		protected void Page_Init(object o, EventArgs e)
		{
			if (!IsPostBack && Prefs.Current["UsedBuddyImporter"].IsNull) Prefs.Current["UsedBuddyImporter"] = 1;
		}
		#endregion

		#region Page_Load
		protected void Page_Load(object o, EventArgs e)
		{
			if (!IsPostBack)
			{
				uiEmailProviderDropDown.DataSource = this.SupportedEmailProviders;
				uiEmailProviderDropDown.DataBind();
				uiEmailProviderDropDown.Items.Insert(0, "");
			}
			this.uiToggleSelectAllMemberContactsCheckBox.Attributes["onclick"] = "ToggleSelectAllMemberContacts()";
			this.uiToggleSelectAllNonMemberContactsCheckBox.Attributes["onclick"] = "ToggleSelectAllNonMemberContacts()";
			Cambro.Web.Helpers.TieButton(uiPasswordText, uiGetEmailContactsButton);
		}
		#endregion
		#region CheckBox ClientID lists for javascript
		private List<string> MemberContactCheckBoxClientIDs = new List<string>();
		protected void uiSelectMemberContactsGridView_RowDataBound(object o, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
				this.MemberContactCheckBoxClientIDs.Add("\"" + ((CheckBox)e.Row.FindControl("uiCheckBox")).ClientID + "\"");
		}
		protected string MemberContactCheckBoxClientIDsAsString
		{
			get { return string.Join(",", MemberContactCheckBoxClientIDs.ToArray()); }
		}

		private List<string> NonMemberContactCheckBoxClientIDs = new List<string>();
		protected void uiSelectNonMemberContactsGridView_RowDataBound(object o, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
				this.NonMemberContactCheckBoxClientIDs.Add("\"" + ((CheckBox)e.Row.FindControl("uiCheckBox")).ClientID + "\"");
		}
		protected string NonMemberContactCheckBoxClientIDsAsString
		{
			get { return string.Join(",", NonMemberContactCheckBoxClientIDs.ToArray()); }
		}
		#endregion
		#region events
		public event EventHandler GetEmailContacts;
		public event EventHandler AddBuddiesAndInviteContacts;
		#endregion
		#region Button click event wire-ups
		protected void GetEmailContacts_Click(object sender, EventArgs e)
		{
			if (this.GetEmailContacts != null) { this.GetEmailContacts(new object(), EventArgs.Empty); }
		}

		protected void InviteSelectedContacts_Click(object sender, EventArgs e)
		{
			if (this.AddBuddiesAndInviteContacts != null) { this.AddBuddiesAndInviteContacts(new object(), EventArgs.Empty); }
		}
		#endregion

		#region Current Stage management
		public BuddyImporterController.Stages CurrentStage
		{
			set
			{
				uiEmailCredentialsPanel.Visible =
					(value == BuddyImporterController.Stages.RequestEmailAddressAndPassword ||
					value == BuddyImporterController.Stages.ErrorBadCredentials ||
					value == BuddyImporterController.Stages.ErrorUnknownEmailProvider);
				uiErrorBadCredentialsLabel.Visible = value == BuddyImporterController.Stages.ErrorBadCredentials;
				uiErrorUnknownEmailProvider.Visible = value == BuddyImporterController.Stages.ErrorUnknownEmailProvider;
				uiSelectContactsPanel.Visible = value == BuddyImporterController.Stages.SelectContactsToBuddyOrInvite;
				uiSuccess.Visible = value == BuddyImporterController.Stages.Done;
			}
		}
		#endregion
		#region Request Email credentials controls
		#region SupportedEmailProviders
		List<string> SupportedEmailProviders = new List<string>()
		{
			"hotmail.com",
			"hotmail.co.uk",
			"msn.com",
			"yahoo.com",
			"yahoo.co.uk",
			"aol.com",
			"aol.co.uk",
			"gmail.com",
			"googlemail.com",
			"",
			"123mail.org",
			"150mail.com",
			"150ml.com",
			"16mail.com",
			"2die4.com",
			"2-mail.com",
			"4email.net",
			"50mail.com",
			"accountant.com",
			"activist.com",
			"adexec.com",
			"africamail.com",
			"aim.com",
			"airpost.net",
			"alabama.usa.com",
			"alaska.usa.com",
			"allergist.com",
			"allmail.net",
			"alumni.com",
			"alumnidirector.com",
			"americamail.com",
			"amorous.com",
			"angelic.com",
			"aol.co.uk",
			"aol.com",
			"aol.com.br",
			"aol.de",
			"aol.fr",
			"aol.in",
			"aol.nl",
			"aol.se",
			"archaeologist.com",
			"arcticmail.com",
			"arizona.usa.com",
			"arkansas.usa.com",
			"aroma.com",
			"artlover.com",
			"asia.com",
			"asia-mail.com",
			"atheist.com",
			"australiamail.com",
			"bartender.net",
			"been-there.com",
			"berlin.com",
			"bestmail.us",
			"bigger.com",
			"bikerider.com",
			"birdlover.com",
			"brazilmail.com",
			"brew-master.com",
			"california.usa.com",
			"californiamail.com",
			"caramail.com",
			"caramail.fr",
			"caress.com",
			"catlover.com",
			"cheerful.com",
			"chef.net",
			"chemist.com",
			"chinamail.com",
			"clerk.com",
			"cliffhanger.com",
			"cluemail.com",
			"collector.org",
			"colorado.usa.com",
			"columnist.com",
			"comfortable.com",
			"comic.com",
			"connecticut.usa.com",
			"consultant.com",
			"contractor.net",
			"counsellor.com",
			"count.com",
			"couple.com",
			"cutey.com",
			"cyberdude.com",
			"cybergal.com",
			"cyber-wizard.com",
			"dallasmail.com",
			"delaware.usa.com",
			"delhimail.com",
			"deliveryman.com",
			"diplomats.com",
			"disciples.com",
			"disposable.com",
			"doctor.com",
			"doglover.com",
			"doubt.com",
			"dr.com",
			"dublin.com",
			"dutchmail.com",
			"earthling.net",
			"elitemail.org",
			"elvisfan.com",
			"email.com",
			"email.de",
			"emailcorner.net",
			"emailengine.net",
			"emailengine.org",
			"emailgroups.net",
			"emailplus.org",
			"emailuser.net",
			"eml.cc",
			"engineer.com",
			"englandmail.com",
			"europe.com",
			"europemail.com",
			"execs.com",
			"fan.com",
			"fan.net",
			"fastem.com",
			"fast-email.com",
			"fastemail.us",
			"fastemailer.com",
			"fastest.cc",
			"fastimap.com",
			"fastmail.cn",
			"fastmail.co.uk",
			"fastmail.com.au",
			"fastmail.es",
			"fastmail.fm",
			"fastmail.in",
			"fastmail.jp",
			"fastmail.net",
			"fast-mail.org",
			"fastmail.to",
			"fastmail.us",
			"fastmailbox.net",
			"fastmessaging.com",
			"fea.st",
			"feelings.com",
			"financier.com",
			"fireman.net",
			"florida.usa.com",
			"f-m.fm",
			"fmail.co.uk",
			"fmailbox.com",
			"fmgirl.com",
			"fmguy.com",
			"footballer.com",
			"ftml.net",
			"gardener.com",
			"geologist.com",
			"georgia.usa.com",
			"germanymail.com",
			"gmail",
			"gmail.com",
			"gmx.at",
			"gmx.ch",
			"gmx.de",
			"gmx.eu",
			"gmx.net",
			"googlemail.com",
			"graduate.org",
			"graphic-designer.com",
			"hailmail.net",
			"hairdresser.net",
			"hawaii.usa.com",
			"hilarious.com",
			"h-mail.us",
			"hockeymail.com",
			"homosexual.net",
			"hotmail.co.jp",
			"hotmail.co.th",
			"hotmail.co.uk",
			"hotmail.com",
			"hotmail.com.ar",
			"hotmail.com.tr",
			"hotmail.de",
			"hotmail.fr",
			"hotmail.it",
			"hot-shot.com",
			"hour.com",
			"howling.com",
			"humanoid.net",
			"icqmail.com",
			"idaho.usa.com",
			"illinois.usa.com",
			"imap.cc",
			"imap-mail.com",
			"imapmail.org",
			"iname.com",
			"indiamail.com",
			"indiana.usa.com",
			"indiatimes.com",
			"innocent.com",
			"inorbit.com",
			"inoutbox.com",
			"instruction.com",
			"instructor.net",
			"insurer.com",
			"internet-e-mail.com",
			"internetemails.net",
			"internet-mail.org",
			"internetmailing.net",
			"iowa.usa.com",
			"irelandmail.com",
			"israelmail.com",
			"italymail.com",
			"japan.com",
			"jetemail.net",
			"journalist.com",
			"justemail.net",
			"kansas.usa.com",
			"kentucky.usa.com",
			"koreamail.com",
			"lawyer.com",
			"legislator.com",
			"letterboxes.org",
			"linkedin",
			"lobbyist.com",
			"london.com",
			"louisiana.usa.com",
			"loveable.com",
			"lycos.at",
			"lycos.be",
			"lycos.ch",
			"lycos.co.uk",
			"lycos.com",
			"lycos.de",
			"lycos.es",
			"lycos.fr",
			"lycos.it",
			"lycos.nl",
			"mac.com",
			"mad.scientist.com",
			"madonnafan.com",
			"madrid.com",
			"mail.com",
			"mail.org",
			"mailandftp.com",
			"mailas.com",
			"mailbolt.com",
			"mailc.net",
			"mailcan.com",
			"mail-central.com",
			"mailforce.net",
			"mailftp.com",
			"mailhaven.com",
			"mailingaddress.org",
			"mailite.com",
			"mailmight.com",
			"mailnew.com",
			"mail-page.com",
			"mailsent.net",
			"mailservice.ms",
			"mailup.net",
			"mailworks.org",
			"maine.usa.com",
			"maryland.usa.com",
			"massachusetts.usa.com",
			"mexicomail.com",
			"michigan.usa.com",
			"mindless.com",
			"minister.com",
			"minnesota.usa.com",
			"mississippi.usa.com",
			"missouri.usa.com",
			"ml1.net",
			"mm.st",
			"mobsters.com",
			"monarchy.com",
			"montana.usa.com",
			"moscowmail.com",
			"msn.com",
			"munich.com",
			"musician.org",
			"muslim.com",
			"myfastmail.com",
			"mymacmail.com",
			"mynet.com",
			"myself.com",
			"nastything.com",
			"nebraska.usa.com",
			"netscape.net",
			"nevada.usa.com",
			"newhampshire.usa.com",
			"newjersey.usa.com",
			"newmexico.usa.com",
			"newyork.usa.com",
			"nightly.com",
			"nonpartisan.com",
			"northcarolina.usa.com",
			"northdakota.usa.com",
			"nospammail.net",
			"null.net",
			"nycmail.com",
			"oath.com",
			"octazen",
			"ohio.usa.com",
			"oklahoma.usa.com",
			"optician.com",
			"oregon.usa.com",
			"orthodontist.net",
			"orthodox.com",
			"ownmail.net",
			"pacific-ocean.com",
			"pacificwest.com",
			"paris.com",
			"pediatrician.com",
			"pennsylvania.usa.com",
			"petlover.com",
			"petml.com",
			"photographer.net",
			"physicist.net",
			"playful.com",
			"poetic.com",
			"polandmail.com",
			"politician.com",
			"popstar.com",
			"post.com",
			"postinbox.com",
			"postpro.net",
			"presidency.com",
			"priest.com",
			"programmer.net",
			"proinbox.com",
			"promessage.com",
			"protestant.com",
			"publicist.com",
			"radiologist.net",
			"realemail.net",
			"reallyfast.biz",
			"reallyfast.info",
			"realtyagent.com",
			"reborn.com",
			"rediffmail.com",
			"reggaefan.com",
			"registerednurses.com",
			"religious.com",
			"repairman.com",
			"representative.com",
			"rescueteam.com",
			"revenue.com",
			"rhodeisland.usa.com",
			"rocketship.com",
			"rockfan.com",
			"rome.com",
			"royal.net",
			"rushpost.com",
			"russiamail.com",
			"safrica.com",
			"saintly.com",
			"salesperson.net",
			"samerica.com",
			"sanfranmail.com",
			"scientist.com",
			"scotlandmail.com",
			"secretary.net",
			"seductive.com",
			"sent.as",
			"sent.at",
			"sent.com",
			"singapore.com",
			"sister.com",
			"sizzling.com",
			"snakebite.com",
			"socialworker.net",
			"sociologist.com",
			"songwriter.net",
			"soon.com",
			"southcarolina.usa.com",
			"southdakota.usa.com",
			"spainmail.com",
			"speedpost.net",
			"speedymail.org",
			"ssl-mail.com",
			"surgical.net",
			"swedenmail.com",
			"swift-mail.com",
			"swissmail.com",
			"teachers.org",
			"techie.com",
			"technologist.com",
			"tempting.com",
			"tennessee.usa.com",
			"texas.usa.com",
			"the-fastest.net",
			"thegame.com",
			"theinternetemail.com",
			"theplate.com",
			"the-quickest.com",
			"therapist.net",
			"toke.com",
			"tokyo.com",
			"toothfairy.com",
			"torontomail.com",
			"tough.com",
			"tvstar.com",
			"umpire.com",
			"usa.com",
			"utah.usa.com",
			"vermont.usa.com",
			"veryfast.biz",
			"veryspeedy.net",
			"virginia.usa.com",
			"wallet.com",
			"warpmail.net",
			"washington.usa.com",
			"web.de",
			"webname.com",
			"weirdness.com",
			"westvirginia.usa.com",
			"who.net",
			"whoever.com",
			"winning.com",
			"wisconsin.usa.com",
			"witty.com",
			"worker.com",
			"writeme.com",
			"wyoming.usa.com",
			"xsmail.com",
			"yahoo.ca",
			"yahoo.co.in",
			"yahoo.co.jp",
			"yahoo.co.kr",
			"yahoo.co.mx",
			"yahoo.co.ru",
			"yahoo.co.th",
			"yahoo.co.tw",
			"yahoo.co.uk",
			"yahoo.com",
			"yahoo.com.ar",
			"yahoo.com.au",
			"yahoo.com.au",
			"yahoo.com.br",
			"yahoo.com.cn",
			"yahoo.com.es",
			"yahoo.com.hk",
			"yahoo.com.kr",
			"yahoo.com.my",
			"yahoo.com.no",
			"yahoo.com.ph",
			"yahoo.com.ru",
			"yahoo.com.se",
			"yahoo.com.sg",
			"yahoo.com.tw",
			"yahoo.de",
			"yahoo.dk",
			"yahoo.es",
			"yahoo.fr",
			"yahoo.gr",
			"yahoo.ie",
			"yahoo.it",
			"yahoo.kr",
			"yahoo.ru",
			"yahoo.se",
			"yahoo.tw",
			"yepmail.net",
			"your-mail.com",
			"yours.com"
		};
		#endregion
		public string EmailAddress
		{
			get { return uiEmailText.Text + "@" + uiEmailProviderDropDown.SelectedValue; }
			set
			{
				string provider = value.Substring(value.IndexOf("@") + 1);
				if (SupportedEmailProviders.Contains(provider))
				{
					uiEmailProviderDropDown.SelectedValue = provider;
					uiEmailText.Text = value.Substring(0, value.IndexOf("@"));
				}
			}
		}

		public string Password
		{
			get { return uiPasswordText.Text; }
		}
		#endregion
		#region Select Contacts controls
		public int AlreadyBuddiesOrRequestedBuddies
		{
			set
			{
				switch (value)
				{
					case 0: this.uiAlreadyBuddiesLabel.Text = ""; break;
					case 1: this.uiAlreadyBuddiesLabel.Text = "You are already buddies, or have a pending buddy request, with <b>one</b> person from your address book."; break;
					default: this.uiAlreadyBuddiesLabel.Text = "You are already buddies, or have a pending buddy request, with <b>" + value + "</b> people from your address book."; break;
				}
			}
		}

		public List<Octazen.AddressBook.Contact> NonMemberContacts
		{
			set
			{
				if (value.Count < 1) { this.uiNonMembersLabel.Text = ""; }
				else { this.uiNonMembersLabel.Text = "The following people are not yet members of DontStayIn. Send them a Welcome email?"; }

				if (value.Count < 2) { this.uiToggleSelectAllNonMemberContactsCheckBox.Visible = false; }
				SetHeightOfDiv(uiSelectNonMemberContactsDiv, 300, 12, value.Count);
				this.uiSelectNonMemberContactsGridView.DataSource = value;
			}
		}
		public List<Octazen.AddressBook.Contact> SelectedNonMemberContacts
		{
			get
			{
				List<Octazen.AddressBook.Contact> selectedContacts = new List<Octazen.AddressBook.Contact>();

				foreach (GridViewRow gvr in uiSelectNonMemberContactsGridView.Rows)
				{
					CheckBox cb = (CheckBox)gvr.FindControl("uiCheckBox");
					if (cb.Checked)
					{
						string name = ((Label)gvr.FindControl("uiName")).Text;
						string emailAddress = ((Label)gvr.FindControl("uiEmailAddress")).Text;
						selectedContacts.Add(new Octazen.AddressBook.Contact(name, emailAddress));
					}
				}

				return selectedContacts;
			}
		}

		public List<Usr> MemberContacts
		{
			set
			{
				if (value.Count < 1) { this.uiNonBuddyMembersLabel.Text = ""; }
				else if (value.Count == 1) { this.uiNonBuddyMembersLabel.Text = "Of your contacts, only <b>" + value[0].NickName + "</b> is already signed up but not yet a buddy. Send a buddy request?"; }
				else { this.uiNonBuddyMembersLabel.Text = "<b>" + value.Count + "</b> people are already Members. Send them a buddy request?"; }

				if (value.Count < 2) { this.uiToggleSelectAllMemberContactsCheckBox.Visible = false; }
				SetHeightOfDiv(this.uiSelectMemberContactsDiv, 300, 12, value.Count);
				this.uiSelectMemberContactsGridView.DataSource = value;
			}
		}
		public List<int> SelectedMemberContacts
		{
			get
			{
				List<int> selectedContacts = new List<int>();

				foreach (GridViewRow gvr in uiSelectMemberContactsGridView.Rows)
				{
					CheckBox cb = (CheckBox)gvr.FindControl("uiCheckBox");
					if (cb.Enabled && cb.Checked)
					{
						int k = (int)uiSelectMemberContactsGridView.DataKeys[gvr.RowIndex].Value;
						selectedContacts.Add(k);
					}
				}

				return selectedContacts;
			}
		}

		private void SetHeightOfDiv(HtmlGenericControl divControl, int maxHeight, int itemHeight, int itemCount)
		{
			if (itemCount * itemHeight > maxHeight) divControl.Attributes["style"] = "border-width:thin; overflow:auto; width: auto; height:" + maxHeight + ";";
			else divControl.Attributes["style"] = "";
		}
		#endregion
		#region Done
		public List<Usr> BuddiesRequested
		{
			set
			{
				if (value.Count > 0)
				{
					uiNoContactsAddedLabel.Visible = false;

					uiBuddiesRequestedList.Text = "<p>A buddy request has been sent to:</p><ul>";
					foreach (Usr u in value)
					{
						uiBuddiesRequestedList.Text += "<li>" + 
							(u.AllowLinkToProfile() ? u.FullName + " (" + u.NickName + ")" : u.Email) +
							"</li>";
					}
					uiBuddiesRequestedList.Text += "</ul>";
				}
			}
		}

		public List<Octazen.AddressBook.Contact> ContactsEmailed
		{
			set
			{
				if (value.Count > 0)
				{
					uiNoContactsAddedLabel.Visible = false;

					uiEmailsSentList.Text = "<p>The following people have been sent a DontStayIn Welcome email:</p><ul>";
					foreach (Octazen.AddressBook.Contact c in value)
					{
						uiEmailsSentList.Text += "<li>" + c.Name + " &lt;" + c.Email + "&gt;</li>";
					}
					uiEmailsSentList.Text += "</ul><p>When they sign up they will have a pending Buddy request from you.</p>";
				}
			}
		}
		#endregion

		#region IBuddyImporter Members

		public event EventHandler OnBegin;
		public event EventHandler OnDone;

		#endregion

		#region IBuddyImporterView Members

		public void NotifyBegin()
		{
			if (OnBegin != null) { OnBegin(this, new EventArgs()); }
		}
		public void NotifyDone()
		{
			if (OnDone != null) { OnDone(this, new EventArgs()); }
		}

		#endregion
	}
}
