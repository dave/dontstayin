using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Transactions;
namespace Bobs {
	[TestFixture]
	public class UsrTest : UnitTestUtilities.DatabaseRollbackTestClass{
	

	

		[Test]
		public void Usr_UserCanBeFetchedFromDatabase(){
			Usr usr = new Usr(1);
			Assert.AreEqual("user1@domain.com", usr.Email);
		}
		[Test]
        [ExpectedException(typeof(Bobs.BobNotFound))]
		public void Usr_InvalidKSupplied_BobNotFoundThrown() {
			Usr usr = new Usr(-1);			
		}

		[Test]
		[ExpectedException(typeof(Bobs.BobNotFound))]
		public void Delete_RemoveUsr1ThenLoadUsr1_BobNotFoundThrown()
		{
			SetupCacheForTesting();

			Usr usr = new Usr();
			usr.Update();
			int k = usr.K;
			usr.Delete();

			usr = new Usr(k);
		}
		[Test, Ignore("This test doesnt work consistently so is being taken out")]
		public void BobsCachingIsWorking()
		{
			SetupCacheForTesting();
			
			UnitTestUtilities.Sql.SqlTrace trace = new UnitTestUtilities.Sql.SqlTrace(Common.Properties.ConnectionString);
			trace.Start();
			Usr usr = new Usr();
			usr.Update();
			trace.Stop();
			Assert.AreNotEqual(0, trace.TotalReads, "First read was zero");
			trace.ClearData();
			trace.Start();
			Usr selectedUsr = new Usr(usr.K);
			trace.Stop();
			Assert.AreNotEqual(0, trace.TotalReads, "Second read was zero");

			trace.ClearData();
			trace.Start();
			Usr cachedUsr = new Usr(usr.K);
			trace.Stop();
			Assert.AreEqual(0, trace.TotalReads, "Third read was not zero");
		}

		private static void SetupCacheForTesting()
		{
			Caching.Instances.Main.FlushAll();
		}
		[Test]
		public void NickNameCachingIsWorking()
		{
			SetupCacheForTesting();
			Usr usr1 = new Usr(1);
			UnitTestUtilities.Sql.SqlTrace trace = new UnitTestUtilities.Sql.SqlTrace(Common.Properties.ConnectionString);
			trace.Start();
			Usr usrGotWithoutCachedNickname = Usr.GetFromNickName(usr1.NickName);
			trace.Stop();
			Assert.AreNotEqual(0, trace.TotalReads);
			trace.ClearData();
			trace.Start();
			Usr usrGotWithCachedNickname = Usr.GetFromNickName(usr1.NickName);
			trace.Stop();
			Assert.AreEqual(0, trace.TotalReads);

		}
	}
}
