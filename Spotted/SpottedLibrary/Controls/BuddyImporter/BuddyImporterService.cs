using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Bobs;
using Bobs.BannerServer;
using Common.Clocks;
using Octazen.AddressBook;

namespace SpottedLibrary.Controls.BuddyImporter
{
	public class BuddyImporterService
	{
		public List<Contact> GetEmailContacts(string email, string password)
		{
			List<Contact> contacts = SimpleAddressBookImporter.FetchContacts(email, password);
			contacts.RemoveAll(c => c.Email == email); // if user has self in contacts list, remove
			return contacts;
		}

		public void SplitOutEmailContacts(List<Contact> contacts, int currentUsrK, out int numberAlreadyBuddiesOrBuddyRequests, out List<Usr> memberContacts, out List<Contact> nonMemberContacts)
		{
			numberAlreadyBuddiesOrBuddyRequests = 0;

			if (contacts.Count > 0)
			{
				Query q = new Query();
				//q.Columns = new ColumnSet(Usr.Columns.K, Usr.Columns.NickName, Usr.Columns.Email);
				q.TableElement = new Join(Usr.Columns.K, Buddy.Columns.BuddyUsrK, QueryJoinType.Left, new Q(Buddy.Columns.UsrK, currentUsrK));
				q.QueryCondition = new And(
					new Q(Usr.Columns.Email, contacts.ConvertAll(c => c.Email).ToArray()),
					new Q(Usr.Columns.IsSkeleton, false)
					);
				q.ExtraSelectElements.Add("BuddyRequested", "case when [Buddy].[BuddyUsrK] IS NOT NULL and coalesce([Buddy].[FullBuddy], 0) = 0 then cast(1 as bit) else cast(0 as bit) end");
				q.ExtraSelectElements.Add("IsAlreadyBuddy", "case when coalesce([Buddy].[FullBuddy], 0) = 1 then cast(1 as bit) else cast(0 as bit) end");
				q.OrderBy = new OrderBy("case when [Buddy].[FullBuddy] = 1 then 0 when [Buddy].[FullBuddy] = 0 then 1 else 2 end ASC, [Usr].[Email]");
				memberContacts = new UsrSet(q).ToList();

				List<int> alreadyBuddyOrRequestedBuddyKs = new List<int>(contacts.Count);
				foreach (Usr u in memberContacts)
				{
					if ((bool)u.ExtraSelectElements["IsAlreadyBuddy"] || (bool)u.ExtraSelectElements["BuddyRequested"])
					{
						alreadyBuddyOrRequestedBuddyKs.Add(u.K);
					}
					contacts.RemoveAll(c => c.Email == u.Email);
				}

				memberContacts.RemoveAll(u => u.K == currentUsrK);

				numberAlreadyBuddiesOrBuddyRequests = alreadyBuddyOrRequestedBuddyKs.Count;
				foreach (int k in alreadyBuddyOrRequestedBuddyKs)
				{
					memberContacts.RemoveAll(u => u.K == k);
				}
			}
			else
			{
				memberContacts = new List<Usr>();
			}

			nonMemberContacts = contacts;
		}

		public void AddAsBuddy(Usr currentUsr, List<int> buddyUsrKs)
		{
			foreach (int buddyUsrK in buddyUsrKs)
			{
				currentUsr.AddBuddy(new Usr(buddyUsrK), Usr.AddBuddySource.BuddyImporter, Buddy.BuddyFindingMethod.EmailAddress, null);
			}
		}

		public void InviteContacts(Usr currentUsr, List<Contact> contacts)
		{
			foreach (Contact c in contacts)
			{
				Usr u = Usr.GetOrCreateSkeletonUser(currentUsr, c.Email, "", null, "", true, true);
				currentUsr.AddBuddy(u, true, true, Usr.AddBuddySource.BuddyImporter, Buddy.BuddyFindingMethod.EmailAddress, c.Name);
			}
		}

	}
}
