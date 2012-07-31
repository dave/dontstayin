using ThoughtWorks.CruiseControl.Core;
using ThoughtWorks.CruiseControl.Core.Sourcecontrol;
using ThoughtWorks.CruiseControl.UnitTests.Core;

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ccnet.vsts.plugin.unittests
{

	/// <summary>
	///This is a test class for ThoughtWorks.CruiseControl.Core.Sourcecontrol.Vsts and is intended
	///to contain all ThoughtWorks.CruiseControl.Core.Sourcecontrol.Vsts Unit Tests
	///</summary>
	[TestClass()]
	public class VstsTest
	{

		private const string SERVER = "http://chandrila:8080";
		private const string PROJECT = "$/Development";
        private const string USERNAME = "TfsService";
        private const string PASSWORD = "better374vault";
        private const string DOMAIN = "DsiHome";

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		/// <summary>
		///Initialize() is called once during test execution before
		///test methods in this test class are executed.
		///</summary>
		[TestInitialize()]
		public void Initialize()
		{
			//  TODO: Add test initialization code
		}

		/// <summary>
		///Cleanup() is called once during test execution after
		///test methods in this class have executed unless
		///this test class' Initialize() method throws an exception.
		///</summary>
		[TestCleanup()]
		public void Cleanup()
		{
			//  TODO: Add test cleanup code
		}


		/// <summary>
		///   Check that no modifcations are returned when they shouldn't be.
		/// </summary>
		[TestMethod()]
		public void GetModificationsTest()
		{
			Vsts vsts = new Vsts();
			vsts.ProjectPath = PROJECT;
			vsts.Server = SERVER;
            vsts.Username = USERNAME;
            vsts.Domain = DOMAIN;
            vsts.Password = PASSWORD;

			IIntegrationResult from = IntegrationResultMother.CreateSuccessful(DateTime.Parse("2005-01-01T00:00:00"));
			IIntegrationResult to = IntegrationResultMother.CreateSuccessful(DateTime.Today.AddDays(1));

			Modification[] actual = vsts.GetModifications(from, to);

			Assert.AreEqual(26, actual.Length, "Didn't get expected number of modifcations");
		}

		/// <summary>
		///   Check that no modifcations are returned when they shouldn't be.
		/// </summary>
		[TestMethod()]
		public void GetNoModificationsTest()
		{
			Vsts vsts = new Vsts();
			vsts.ProjectPath = PROJECT;
			vsts.Server = SERVER;
            vsts.Username = USERNAME;
            vsts.Domain = DOMAIN;
            vsts.Password = PASSWORD;

			IIntegrationResult from = IntegrationResultMother.CreateSuccessful(DateTime.Parse("2005-10-01T00:00:00"));
            IIntegrationResult to = IntegrationResultMother.CreateSuccessful(DateTime.Parse("2005-10-01T00:00:00"));

			Modification[] actual = vsts.GetModifications(from, to);

			Assert.AreEqual(0, actual.Length, "Didn't get expected number of modifcations");
		}

		/// <summary>
		///   If we ask for something that does not exist then just return 0 modifcations
		/// </summary>
		[TestMethod()]
		public void GetModificationsForSomethingThatDoesNotExistTest()
		{
			Vsts vsts = new Vsts();
			vsts.ProjectPath = "$/FreeLunch";
			vsts.Server = SERVER;
            vsts.Username = USERNAME;
            vsts.Domain = DOMAIN;
            vsts.Password = PASSWORD;

			IIntegrationResult from = IntegrationResultMother.CreateSuccessful(DateTime.Parse("2005-03-10T00:00:00"));
            IIntegrationResult to = IntegrationResultMother.CreateSuccessful(DateTime.Parse("2005-10-01T00:00:00"));

			Modification[] actual = vsts.GetModifications(from, to);

			Assert.AreEqual(0, actual.Length);
		}

		/// <summary>
		///   An error is thrown if the passed server does not exist.
		/// </summary>
		[TestMethod()]
        [ExpectedException(typeof(Microsoft.TeamFoundation.TeamFoundationInvalidServerNameException), "The server name NoneExistentServer is not valid..")]
		public void GetModificationsForServerThatDoesNotExistTest()
		{
			Vsts vsts = new Vsts();
			vsts.ProjectPath = PROJECT;
			vsts.Server = "NoneExistentServer";

			IIntegrationResult from = IntegrationResultMother.CreateSuccessful(DateTime.Parse("2005-03-10T00:00:00"));
            IIntegrationResult to = IntegrationResultMother.CreateSuccessful(DateTime.Parse("2005-10-01T00:00:00"));

			Modification[] actual = vsts.GetModifications(from, to);

			Assert.Fail("Exception Expected");
		}



		/// <summary>
		///   If we ask for something that does not exist then just return 0 modifcations
		/// </summary>
		[TestMethod()]
		public void LabelTest()
		{
			Vsts vsts = new Vsts();
			vsts.ProjectPath = PROJECT;
			vsts.Server = SERVER;
            vsts.Username = USERNAME;
            vsts.Domain = DOMAIN;
            vsts.Password = PASSWORD;

            vsts.ApplyLabel = true;

			IIntegrationResult result = IntegrationResultMother.CreateSuccessful(DateTime.Now);
			result.Label = "UnitTestRun_" + DateTime.Now.Ticks.ToString();

			vsts.LabelSourceControl(result);

			Assert.IsInstanceOfType(vsts, typeof(Vsts));
		}

		/// <summary>
		///A test case for GetSource (IIntegrationResult)
		///</summary>
		[TestMethod()]
		public void GetSourceTest()
		{
			Vsts vsts = new Vsts();
			vsts.ProjectPath = PROJECT;
			vsts.Server = SERVER;
            vsts.Username = USERNAME;
            vsts.Domain = DOMAIN;
            vsts.Password = PASSWORD;

			vsts.WorkingDirectory = "c:\\source\\temp";
			vsts.AutoGetSource = true;
			vsts.CleanCopy = true;

			IIntegrationResult result = IntegrationResultMother.CreateSuccessful(DateTime.Now);
			result.Label = "Test Build";

			vsts.GetSource(result);

			Assert.IsTrue(File.Exists("c:\\source\\temp\\VSTSPlugins.sln"));

		}

	}


}
