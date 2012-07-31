using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Octazen.AddressBook;

namespace SpottedLibrary.Controls.BuddyImporter
{
	public interface IBuddyImporter
	{
		event EventHandler OnBegin;
		event EventHandler OnDone;
	}

	public interface IBuddyImporterView : IView, IRequiresValidation
	{
		List<Usr> MemberContacts { set; }
		List<int> SelectedMemberContacts { get; }
		List<Contact> NonMemberContacts { set; }
		List<Contact> SelectedNonMemberContacts { get; }
		int AlreadyBuddiesOrRequestedBuddies { set; }
		string EmailAddress { get; set; }
		string Password { get; }
		BuddyImporterController.Stages CurrentStage { set; }
		List<Contact> ContactsEmailed { set; }
		List<Usr> BuddiesRequested { set; }

		event EventHandler GetEmailContacts;
		event EventHandler AddBuddiesAndInviteContacts;
		void NotifyBegin();
		void NotifyDone();
	}
}
