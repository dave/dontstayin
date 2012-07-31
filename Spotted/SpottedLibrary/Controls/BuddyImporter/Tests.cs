using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Bobs;
using Octazen.AddressBook;

namespace SpottedLibrary.Controls.BuddyImporter
{
	[TestFixture]
	public class Tests : UnitTestUtilities.DatabaseRollbackTestClass
	{
		[Test]
		public void Test()
		{
			BuddyImporterService service = new BuddyImporterService();
			string email = "Bob@bob3.com";

			Usr u = new Usr();
			u.Update();

			Usr u2 = new Usr()
			{
				Email = email
			};
			u2.Update();

			Buddy b = new Buddy()
			{
				UsrK = u.K,
				BuddyUsrK = u2.K
			};
			b.Update();

			List<Contact> cList;
			List<Usr> uList;

			List<Contact> list = new List<Contact>()
			{
				new Contact("Bob", email)
			};

			int alreadyBuddies;
			service.SplitOutEmailContacts(list, u.K, out alreadyBuddies, out uList, out cList);

			Assert.AreEqual(1, uList.Count);
			Assert.IsTrue((bool)uList[0].ExtraSelectElements["BuddyRequested"]);
			Assert.AreEqual(0, cList.Count);
		}
	}
}
