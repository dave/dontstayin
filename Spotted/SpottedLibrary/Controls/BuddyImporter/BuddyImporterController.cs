using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Octazen.AddressBook;

namespace SpottedLibrary.Controls.BuddyImporter
{
	public class BuddyImporterController
	{
		public enum Stages
		{
			RequestEmailAddressAndPassword,
			ErrorBadCredentials,
			ErrorUnknownEmailProvider,
			SelectContactsToBuddyOrInvite,
			Done
		}

		IBuddyImporterView view;
		BuddyImporterService service;

		public BuddyImporterController(IBuddyImporterView view, BuddyImporterService service)
		{
			this.view = view;
			this.view.Load += new EventHandler(view_Load);
			this.view.GetEmailContacts += new EventHandler(view_GetEmailContacts);
			this.view.AddBuddiesAndInviteContacts += new EventHandler(view_AddBuddiesAndInviteContacts);
			this.service = service;
		}

		#region Load -> RequestEmailAddressAndPassword
		void view_Load(object sender, EventArgs e)
		{
			if (!view.IsPostBack)
			{
				if (Usr.Current != null)
					view.EmailAddress = Usr.Current.Email;
				view.CurrentStage = Stages.RequestEmailAddressAndPassword;
				//view.DataBind();
			}
		}
		#endregion

		#region RequestEmailAddressAndPassword -> SelectContactsToBuddyOrInvite
		void view_GetEmailContacts(object sender, EventArgs e)
		{
			if (!view.IsValid) return;

			int alreadyBuddies;
			List<Usr> memberContacts = new List<Usr>();
			List<Contact> nonMemberContacts = new List<Contact>();
			List<Contact> contacts = null;
			try
			{
				contacts = service.GetEmailContacts(view.EmailAddress, view.Password);
			}
            catch (UnsupportedAddressBookException)
            {
				view.CurrentStage = Stages.ErrorUnknownEmailProvider;
            }
            catch (AddressBookAuthenticationException)
            {
				view.CurrentStage = Stages.ErrorBadCredentials;
            }

			if (contacts != null)
			{
				service.SplitOutEmailContacts(contacts, Usr.Current.K, out alreadyBuddies, out memberContacts, out nonMemberContacts);
				view.MemberContacts = memberContacts;
				view.NonMemberContacts = nonMemberContacts;
				view.AlreadyBuddiesOrRequestedBuddies = alreadyBuddies;

				view.CurrentStage = Stages.SelectContactsToBuddyOrInvite;
			}

			view.DataBind();
			view.NotifyBegin();
		}
		#endregion

		#region SelectContactsToBuddyOrInvite -> Done
		void view_AddBuddiesAndInviteContacts(object sender, EventArgs e)
		{
			service.AddAsBuddy(Usr.Current, view.SelectedMemberContacts);
			view.BuddiesRequested = new UsrSet(new Query(new Q(Usr.Columns.K, view.SelectedMemberContacts.ToArray()))).ToList();

			service.InviteContacts(Usr.Current, view.SelectedNonMemberContacts);
			view.ContactsEmailed = view.SelectedNonMemberContacts;

			view.CurrentStage = Stages.Done;
			view.NotifyDone();
		}
		#endregion

	}
}
