using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
// note: OTE (Test) API!
using DomainNameRegistrar.com.wildwestdomains.ote.api;
using NUnit.Framework;
using DomainNameRegistrar;
using DomainNameRegistrar.ResponseParsers;

namespace DomainNameRegistrar.Test
{
	[TestFixture]
	public class OteCertification
	{
		[Test]
		public void Test()
		{
			OteCertificationRegistrar r = new OteCertificationRegistrar("example.us", "example.biz");
			r.DoCertificationTest();
		}
		[Test]
		public void TestDomainNameValidation()
		{
			Assert.IsTrue(DotComDomain.IsSecondLevelDomainNameValid("x"));
			Assert.IsTrue(DotComDomain.IsSecondLevelDomainNameValid("53"));
			Assert.IsTrue(DotComDomain.IsSecondLevelDomainNameValid("2girls1cup"));
			Assert.IsTrue(DotComDomain.IsSecondLevelDomainNameValid("therapist-finder"));
			Assert.IsFalse(DotComDomain.IsSecondLevelDomainNameValid(""));
			Assert.IsFalse(DotComDomain.IsSecondLevelDomainNameValid("can'tdoapostrophes"));
			Assert.IsFalse(DotComDomain.IsSecondLevelDomainNameValid("dots.notallowed"));
			Assert.IsFalse(DotComDomain.IsSecondLevelDomainNameValid("."));
			Assert.IsFalse(DotComDomain.IsSecondLevelDomainNameValid("/"));
			Assert.IsFalse(DotComDomain.IsSecondLevelDomainNameValid("DONTSHOUT"));
		}
	}

	/// <summary>
	/// this class runs through the tests as prescribed by WWD's quick start API,
	/// to achieve certification and ability to use live web service.
	/// </summary>
	class OteCertificationRegistrar
	{
		int userID { get; set; }
		int dbpuserID { get; set; }

		WAPI wapi { get; set; }
		Credential cred { get; set; }
		string[] domainNames { get; set; }
		readonly string orderDomainTransactionID;
		readonly string orderPrivacyTransactionID;
		List<string> otherTransactionIDs;
		readonly int RegistrationPeriodInYears = 2;
		string OrderID { get; set; }
		PollResponse pollResponse { get; set; }

		public OteCertificationRegistrar(params string[] domainNames)
		{
			wapi = new WAPI();
			cred = new Credential();
			cred.Account = "dontstayin";
			cred.Password = "Blind78bat"; // TODO read this from database

			this.domainNames = domainNames;
			for (int i = 0; i < domainNames.Length; i++)
			{
				this.domainNames[i] = this.domainNames[i].ToLower();
			}

			orderDomainTransactionID = Guid.NewGuid().ToString();
			orderPrivacyTransactionID = Guid.NewGuid().ToString();
			otherTransactionIDs = new List<string>();
		}

		public void DoCertificationTest()
		{
			try
			{
				Availability?[] availabilities0 = CheckAvailability();
				for (int i = 0; i < availabilities0.Length; i++)
				{
					if (!availabilities0[i].HasValue || availabilities0[i].Value != Availability.AvailableForRegistration)
					{
						throw new NotImplementedException(); // flag domains[i] as not good
					}
				}

				Register();
				PurchasePrivacy();


				Availability?[] availabilities1 = CheckAvailability();
				for (int i = 0; i < availabilities1.Length; i++)
				{
					if (!availabilities1[i].HasValue || availabilities1[i].Value != Availability.NotAvailableForRegistration)
					{
						throw new NotImplementedException(); // flag domains[i] as not good
					}
				}

				QueryInformation();
				Renew();
				Transfer();
			}
			finally
			{
				Reset();
			}
		}

		#region Methods
		//#region Describe
		//public void Describe()
		//{
		//    string describe = wapi.Describe(uniqueClientTransactionID, cred);
		//}
		//#endregion
		#region Reset
		private void Reset()
		{
			Reset(this.orderDomainTransactionID);
			Reset(this.orderPrivacyTransactionID);
			foreach (string transactionID in otherTransactionIDs)
			{
				Reset(transactionID);
			}
		}
		private void Reset(string transactionID)
		{
			string resetCommandXml = "<wapi clTRID='" + transactionID + "' account='" + this.cred.Account + "' pwd='" + this.cred.Password + "'><manage><script cmd='reset' /></manage></wapi>";
			string returnXml = wapi.ProcessRequest(resetCommandXml);
			if ("scripting status reset" != returnXml) { throw new InvalidResponseException(returnXml); }
		}
		#endregion
		#region CheckAvailability
		private Availability?[] CheckAvailability()
		{
			string transactionID = Guid.NewGuid().ToString();
			string returnXml = wapi.CheckAvailability(transactionID, cred, domainNames, null, null);
			CheckAvailabilityResponse c = new CheckAvailabilityResponse(returnXml);
			otherTransactionIDs.Add(transactionID);
			//if (c.ResponseCode != ResponseCode.Success) throw new FailedResponseException(c.ResponseCode, returnXml);
			return c.AvailabilitiesForDomains(this.domainNames);
		}
		#endregion
		#region Register
		private void Register()
		{
			ContactInfo registrant = new ContactInfo()
			{
				fname = "Artemus",
				lname = "Gordon",
				email = "agordon@wildwestdomains.com",
				phone = "+1.8885551212",
				sa1 = "2 N. Main St.",
				city = "Valdosta",
				sp = "Georgia",
				pc = "17123",
				cc = "United States",
			};

			DomainRegistration[] drArray = new DomainRegistration[this.domainNames.Length];
			for (int i = 0; i < this.domainNames.Length; i++)
			{
				string topLevelDomain = Helpers.GetTopLevelDomain(this.domainNames[i]);
				drArray[i] = new DomainRegistration()
				{
					nsArray = new NS[] { new NS() { name = "ns1.example.com" }, new NS() { name = "ns2.example.com" } },
					registrant = registrant,
					order = new OrderItem() { productid = (int)GetProductID(topLevelDomain, RegistrationPeriodInYears, false), riid = i.ToString() },
					tld = topLevelDomain,
					sld = Helpers.GetSecondLevelDomain(this.domainNames[i]),
					period = RegistrationPeriodInYears
				};

				if (topLevelDomain == "us")
				{
					drArray[i].nexus = new Nexus()
					{
						category = "citizen of US",
						use = "personal",
						country = "us"
					};
				}
			}

			Shopper shopper = new Shopper();
			if (this.userID > 0)
			{
				shopper.user = userID.ToString();
			}
			else
			{
				shopper.user = "createNew";
				shopper.pwd = "abcde";
				shopper.email = "agordon@wildwestdomains.com";
				shopper.firstname = "Artemus";
				shopper.lastname = "Gordon";
				shopper.phone = "+1.8885551212"; // country code . number
			}

			string returnXml = wapi.OrderDomains(orderDomainTransactionID, cred, shopper, drArray, null, "domain");
			RegisterDomainsResponse r = new RegisterDomainsResponse(returnXml);
			if (this.orderDomainTransactionID != r.UniqueClientTransactionID) throw new InvalidResponseException(returnXml);
			this.OrderID = r.OrderID;
			this.userID = r.ShopperID;
		}
		#endregion
		#region PurchasePrivacy
		private void PurchasePrivacy()
		{
			string pollResponseXml = wapi.Poll(this.orderDomainTransactionID, cred, null);
			pollResponse = new PollResponse(pollResponseXml);
			if (this.orderDomainTransactionID != pollResponse.UniqueClientTransactionID) throw new InvalidResponseException(pollResponseXml);

			Shopper s = new Shopper();
			s.user = this.userID.ToString();
			s.dbpuser = "createNew";
			s.dbppwd = "defgh";
			s.dbpemail = "info@example.us";

			DomainByProxy dbp = new DomainByProxy()
			{
				sld = "example",
				tld = "us",
				resourceid = pollResponse.Items.Find(i => i.ROID == "domain" && i.RIID == "0").ResourceID,
				order = new OrderItem()
				{
					duration = 1,
					productid = (int)ProductID.PrivateRegistrationServicesAPI
				}
			};
			string response = wapi.OrderDomainPrivacy(this.orderPrivacyTransactionID, cred, s, new DomainByProxy[] { dbp }, null);
			OrderDomainPrivacyResponse r = new OrderDomainPrivacyResponse(response);
			if (this.orderPrivacyTransactionID != r.UniqueClientTransactionID) throw new InvalidResponseException(response);
			dbpuserID = r.DbpUserID;
		}
		#endregion
		#region QueryInformation
		private void QueryInformation()
		{
			string trid = Guid.NewGuid().ToString();
			string responseXml =
				wapi.Info(trid, cred, pollResponse.Items.Find(i => i.ROID == "domain" && i.RIID == "1").ResourceID, "standard", null, null);
			// lazy, no need to read this response at the mo
			otherTransactionIDs.Add(trid);
		}
		#endregion
		#region Renew
		private void Renew()
		{
			DomainRenewal[] drArray = new DomainRenewal[2];
			for (int i = 0; i < domainNames.Length; i++)
			{
				string tld = Helpers.GetTopLevelDomain(domainNames[i]);
				drArray[i] = new DomainRenewal()
				{
					resourceid = pollResponse.Items.Find(item => item.ROID == "domain" && item.RIID == i.ToString()).ResourceID,
					tld = tld,
					sld = Helpers.GetSecondLevelDomain(domainNames[i]),
					period = 1,
					order = new OrderItem()
					{
						productid = (int)GetProductID(tld, 1, true),
						duration = 1,
						quantity = 1,
						riid = i.ToString()
					}
				};
			}

			PollResponse p = new PollResponse(
				wapi.Poll(this.orderPrivacyTransactionID, cred, null));

			Shopper shopper = new Shopper() { user = userID.ToString(), dbpuser = dbpuserID.ToString(), dbppwd = "defgh" };

			ResourceRenewal rr = new ResourceRenewal()
			{
				resourceid = p.Items[0].ResourceID,
				order = new OrderItem()
				{
					productid = (int)ProductID.PrivateRegistrationServicesRenewalAPI,
					duration = 1,
					quantity = 1
				}
			};

			string trid = Guid.NewGuid().ToString();
			string resp1 =
				wapi.OrderPrivateDomainRenewals(trid, cred, shopper, drArray, new ResourceRenewal[] { rr }, null);

			otherTransactionIDs.Add(trid);
		}
		#endregion
		#region Transfer
		private void Transfer()
		{
			Shopper newShopper = new Shopper()
			{
				user = "createNew",
				pwd = "ghijk",
				firstname = "Joe",
				lastname = "Smith",
				email = "joe@smith.us",
				phone = "+1.7775551212"
			};
			ContactInfo newRegistrant = new ContactInfo()
			{
				fname = "Joe",
				lname = "Smith",
				email = "joe@smith.us",
				sa1 = "1 S. Main St.",
				city = "Oakland",
				sp = "California",
				pc = "97123",
				cc = "United States",
				phone = "+1.7775551212"
			};

			DomainTransfer[] dtArray = new DomainTransfer[1];
			dtArray[0] = new DomainTransfer()
			{
				sld = "example",
				tld = "com",
				order = new OrderItem()
				{
					productid = (int)ProductID.TransferDotCOM
				}
			};

			string trid = Guid.NewGuid().ToString();
			string responseXml =
				wapi.OrderDomainTransfers(trid, cred, newShopper, dtArray, null);
			otherTransactionIDs.Add(trid);
		}
		#endregion
		#endregion

		private static ProductID GetProductID(string topLevelDomain, int numberOfYears, bool isRenewal)
		{
			switch (topLevelDomain)
			{
				case "biz": switch (numberOfYears)
					{
						case 1: return isRenewal ? ProductID.OneYearDomainRenewalDotBIZ : ProductID.OneYearDomainNewRegistrationDotBIZ;
						case 2: return isRenewal ? ProductID.TwoYearDomainRenewalDotBIZ : ProductID.TwoYearDomainNewRegistrationDotBIZ;
						default: throw new NotImplementedException();
					}
				case "us": switch (numberOfYears)
					{
						case 1: return isRenewal ? ProductID.OneYearDomainRenewalDotUS : ProductID.OneYearDomainNewRegistrationDotUS;
						case 2: return isRenewal ? ProductID.TwoYearDomainRenewalDotUS : ProductID.TwoYearDomainNewRegistrationDotUS;
						default: throw new NotImplementedException();
					}
				default: throw new NotImplementedException();
			}
		}

	}
}
