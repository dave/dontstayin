using System;
using System.Collections.Generic;
using System.Text;

using DomainNameRegistrar.com.wildwestdomains.api;
using DomainNameRegistrar.ResponseParsers;

namespace DomainNameRegistrar
{
	public class DotComDomain
	{
		public string WwdResourceID { get; private set; }

		WAPI wapi;
		readonly Credential credentials;
		readonly Shopper shopper;
		readonly ContactInfo registrant;
		readonly NS[] nsArray = new NS[] { new NS() { name = "ns1.cambro.net" }, new NS() { name = "ns2.cambro.net" } };
		readonly int registrationPeriodInYears = 1;
		readonly int productIdForOneYearDotCom = (int)ProductID.OneYearDomainNewRegistrationDotCOM;
		readonly string secondLevelDomain;
		readonly string topLevelDomain;
		string DomainName { get { return secondLevelDomain + "." + topLevelDomain; } }

		/// <summary>
		/// Prepare a new .com domain for registration
		/// </summary>
		/// <param name="secondLevelDomain">e.g. "dontstayin" of "www.dontstayin.com"</param>
		public DotComDomain(string secondLevelDomain)
		{
			this.secondLevelDomain = secondLevelDomain.ToLower();

			if (!IsSecondLevelDomainNameValid())
			{
				throw new Exception("Invalid second-level domain: " + this.secondLevelDomain);
			}

			this.topLevelDomain = "com";

			shopper = new Shopper()
			{
				user = "18178663",
				pwd	= "Blind78bat"
				//user = "createNew",
				//pwd = "Blind78bat",
				//firstname = "David",
				//lastname = "Brophy",
				//email = "dave@dontstayin.com",
				//phone = "+44.2078355599"
			};
			registrant = new ContactInfo()
			{
				fname = "David",
				lname = "Brophy",
				org = "Development Hell Limited",
				sa1 = "90-92 Pentonville Road",
				sa2 = "London",
				city = "London",
				sp = "London",
				pc = "N1 8HS",
				cc = "United Kingdom",
				email = "dave@dontstayin.com",
				phone = "+44.2078355599",
			};

			credentials = new Credential()
			{
				Account = "dontstayin",
				Password = "Blind78bat" // TODO: read from database
			};

			wapi = new WAPI();
		}

		private bool IsSecondLevelDomainNameValid()
		{
			return IsSecondLevelDomainNameValid(this.secondLevelDomain);
		}
		public static bool IsSecondLevelDomainNameValid(string secondLevelDomainName)
		{
			return System.Text.RegularExpressions.Regex.IsMatch(secondLevelDomainName, "^([a-z0-9-])+$");
		}

		/// <summary>
		/// Registers the domain name and sets WwdResourceID, useful for renewing purchased items later.
		/// </summary>
		public void Register()
		{
			if (Common.Properties.IsDevelopmentEnvironment)
			{
				WwdResourceID = "test domain";
				return;
			}


			Availability preAvailability = CheckAvailability();
			if (preAvailability != Availability.AvailableForRegistration)
			{
				throw new Exception(preAvailability.ToString());
			}

			DomainRegistration domainReg = new DomainRegistration()
			{
				nsArray = this.nsArray,
				registrant = this.registrant,
				order = new OrderItem() { productid = this.productIdForOneYearDotCom },
				tld = this.topLevelDomain,
				sld = this.secondLevelDomain,
				period = this.registrationPeriodInYears,
				autorenewflag = 1
			};

			string transactionID = Guid.NewGuid().ToString();


			RegisterDomainsResponse regDomainResp = new RegisterDomainsResponse(
				wapi.OrderDomains(transactionID, this.credentials, this.shopper, new DomainRegistration[] { domainReg }, null, null));


			//int sleep = 100;
			//DateTime timeout = DateTime.Now.AddMilliseconds(10000);

			//Availability postAvailability = CheckAvailability();
			//while (postAvailability != Availability.NotAvailableForRegistration && DateTime.Now < timeout)
			//{
			//    System.Threading.Thread.Sleep(sleep);
			//    postAvailability = CheckAvailability();
			//}

			//if (postAvailability != Availability.NotAvailableForRegistration)
			//{
			//    throw new Exception("Domain still available after registration");
			//}

			//PollResponse pollResp = new PollResponse(
			//    wapi.Poll(transactionID, this.credentials, null));


			//if (pollResp.ProcessedItems.Count != 1) throw new Exception("transactionID = " + transactionID + ". Poll didn't return expected results: " + pollResp.Xml);
			//WwdResourceID = pollResp.ProcessedItems[0].ResourceID;
		}

		Availability CheckAvailability()
		{
			CheckAvailabilityResponse checkAvailResp = new CheckAvailabilityResponse(
				wapi.CheckAvailability(Guid.NewGuid().ToString(), this.credentials, new string[] { this.DomainName }, null, null));
			return checkAvailResp.Availabilities[0];
		}


		public bool IsAvailable()
		{
			return CheckAvailability() == Availability.AvailableForRegistration;
		}
	}

	#region ProductIDs
	enum ProductID
	{
		OneYearDomainNewRegistrationDotCOM = 350001,
		TwoYearDomainNewRegistrationDotCOM = 350002,
		ThreeYearDomainNewRegistrationDotCOM = 350003,
		FourYearDomainNewRegistrationDotCOM = 350004,
		FiveYearDomainNewRegistrationDotCOM = 350005,
		SixYearDomainNewRegistrationDotCOM = 350006,
		SevenYearDomainNewRegistrationDotCOM = 350007,
		EightYearDomainNewRegistrationDotCOM = 350008,
		NineYearDomainNewRegistrationDotCOM = 350009,
		TenYearDomainNewRegistrationDotCOM = 350010,
		TransferDotCOM = 350011,
		OneYearDomainRenewalDotCOM = 350012,
		TwoYearDomainRenewalDotCOM = 350013,
		ThreeYearDomainRenewalDotCOM = 350014,
		FourYearDomainRenewalDotCOM = 350015,
		FiveYearDomainRenewalDotCOM = 350016,
		SixYearDomainRenewalDotCOM = 350017,
		SevenYearDomainRenewalDotCOM = 350018,
		EightYearDomainRenewalDotCOM = 350019,
		YearDomainRenewalDotCOM = 350020,
		TenYearDomainRenewalDotCOM = 350021,
		DomainNameForwarding = 350022,
		DomainNameForwardingRenewal = 350023,
		DomainMasking = 350024,
		DomainMaskingRenewal = 350025,
		OneYearDomainNewRegistrationDotNET = 350030,
		TwoYearDomainNewRegistrationDotNET = 350031,
		ThreeYearDomainNewRegistrationDotNET = 350032,
		FourYearDomainNewRegistrationDotNET = 350033,
		FiveYearDomainNewRegistrationDotNET = 350034,
		SixYearDomainNewRegistrationDotNET = 350035,
		SevenYearDomainNewRegistrationDotNET = 350036,
		EightYearDomainNewRegistrationDotNET = 350037,
		NineYearDomainNewRegistrationDotNET = 350038,
		TenYearDomainNewRegistrationDotNET = 350039,
		TransferDotNET = 350040,
		OneYearDomainRenewalDotNET = 350041,
		TwoYearDomainRenewalDotNET = 350042,
		ThreeYearDomainRenewalDotNET = 350043,
		FourYearDomainRenewalDotNET = 350044,
		FiveYearDomainRenewalDotNET = 350045,
		SixYearDomainRenewalDotNET = 350046,
		SevenYearDomainRenewalDotNET = 350047,
		EightYearDomainRenewalDotNET = 350048,
		NineYearDomainRenewalDotNET = 350049,
		TenYearDomainRenewalDotNET = 350050,
		OneYearDomainNewRegistrationDotINFO = 350051,
		TwoYearDomainNewRegistrationDotINFO = 350052,
		ThreeYearDomainNewRegistrationDotINFO = 350053,
		FourYearDomainNewRegistrationDotINFO = 350054,
		FiveYearDomainNewRegistrationDotINFO = 350055,
		SixYearDomainNewRegistrationDotINFO = 350056,
		SevenYearDomainNewRegistrationDotINFO = 350057,
		EightYearDomainNewRegistrationDotINFO = 350058,
		NineYearDomainNewRegistrationDotINFO = 350059,
		TenYearDomainNewRegistrationDotINFO = 350060,
		TransferDotINFO = 350061,
		OneYearDomainRenewalDotINFO = 350062,
		TwoYearDomainRenewalDotINFO = 350063,
		ThreeYearDomainRenewalDotINFO = 350064,
		FourYearDomainRenewalDotINFO = 350065,
		FiveYearDomainRenewalDotINFO = 350066,
		SixYearDomainRenewalDotINFO = 350067,
		SevenYearDomainRenewalDotINFO = 350068,
		EightYearDomainRenewalDotINFO = 350069,
		NineYearDomainRenewalDotINFO = 350070,
		TenYearDomainRenewalDotINFO = 350071,
		OneYearDomainNewRegistrationDotBIZ = 350076,
		TwoYearDomainNewRegistrationDotBIZ = 350077,
		ThreeYearDomainNewRegistrationDotBIZ = 350078,
		FourYearDomainNewRegistrationDotBIZ = 350079,
		FiveYearDomainNewRegistrationDotBIZ = 350080,
		SixYearDomainNewRegistrationDotBIZ = 350081,
		SevenYearDomainNewRegistrationDotBIZ = 350082,
		EightYearDomainNewRegistrationDotBIZ = 350083,
		NineYearDomainNewRegistrationDotBIZ = 350084,
		TenYearDomainNewRegistrationDotBIZ = 350085,
		TransferDotBIZ = 350086,
		OneYearDomainRenewalDotBIZ = 350087,
		TwoYearDomainRenewalDotBIZ = 350088,
		ThreeYearDomainRenewalDotBIZ = 350089,
		FourYearDomainRenewalDotBIZ = 350090,
		FiveYearDomainRenewalDotBIZ = 350091,
		SixYearDomainRenewalDotBIZ = 350092,
		SevenYearDomainRenewalDotBIZ = 350093,
		EightYearDomainRenewalDotBIZ = 350094,
		NineYearDomainRenewalDotBIZ = 350095,
		TenYearDomainRenewalDotBIZ = 350096,
		OneYearDomainNewRegistrationDotWS = 350101,
		TwoYearDomainNewRegistrationDotWS = 350102,
		ThreeYearDomainNewRegistrationDotWS = 350103,
		FourYearDomainNewRegistrationDotWS = 350104,
		FiveYearDomainNewRegistrationDotWS = 350105,
		SixYearDomainNewRegistrationDotWS = 350106,
		SevenYearDomainNewRegistrationDotWS = 350107,
		EightYearDomainNewRegistrationDotWS = 350108,
		NineYearDomainNewRegistrationDotWS = 350109,
		TenYearDomainNewRegistrationDotWS = 350110,
		OneYearDomainRenewalDotWS = 350112,
		TwoYearDomainRenewalDotWS = 350113,
		ThreeYearDomainRenewalDotWS = 350114,
		FourYearDomainRenewalDotWS = 350115,
		FiveYearDomainRenewalDotWS = 350116,
		SixYearDomainRenewalDotWS = 350117,
		SevenYearDomainRenewalDotWS = 350118,
		EightYearDomainRenewalDotWS = 350119,
		NineYearDomainRenewalDotWS = 350120,
		TenYearDomainRenewalDotWS = 350121,
		OneYearDomainNewRegistrationDotUS = 350126,
		TwoYearDomainNewRegistrationDotUS = 350127,
		ThreeYearDomainNewRegistrationDotUS = 350128,
		FourYearDomainNewRegistrationDotUS = 350129,
		FiveYearDomainNewRegistrationDotUS = 350130,
		SixYearDomainNewRegistrationDotUS = 350131,
		SevenYearDomainNewRegistrationDotUS = 350132,
		EightYearDomainNewRegistrationDotUS = 350133,
		NineYearDomainNewRegistrationDotUS = 350134,
		TenYearDomainNewRegistrationDotUS = 350135,
		TransferDotUS = 350136,
		OneYearDomainRenewalDotUS = 350137,
		TwoYearDomainRenewalDotUS = 350138,
		ThreeYearDomainRenewalDotUS = 350139,
		FourYearDomainRenewalDotUS = 350140,
		FiveYearDomainRenewalDotUS = 350141,
		SixYearDomainRenewalDotUS = 350142,
		SevenYearDomainRenewalDotUS = 350143,
		EightYearDomainRenewalDotUS = 350144,
		NineYearDomainRenewalDotUS = 350145,
		TenYearDomainRenewalDotUS = 350146,
		OneYearDomainNewRegistrationDotORG = 350150,
		TwoYearDomainNewRegistrationDotORG = 350151,
		ThreeYearDomainNewRegistrationDotORG = 350152,
		FourYearDomainNewRegistrationDotORG = 350153,
		FiveYearDomainNewRegistrationDotORG = 350154,
		SixYearDomainNewRegistrationDotORG = 350155,
		SevenYearDomainNewRegistrationDotORG = 350156,
		EightYearDomainNewRegistrationDotORG = 350157,
		NineYearDomainNewRegistrationDotORG = 350158,
		TenYearDomainNewRegistrationDotORG = 350159,
		TransferDotORG = 350160,
		OneYearDomainRenewalDotORG = 350161,
		TwoYearDomainRenewalDotORG = 350162,
		ThreeYearDomainRenewalDotORG = 350163,
		FourYearDomainRenewalDotORG = 350164,
		FiveYearDomainRenewalDotORG = 350165,
		SixYearDomainRenewalDotORG = 350166,
		SevenYearDomainRenewalDotORG = 350167,
		EightYearDomainRenewalDotORG = 350168,
		NineYearDomainRenewalDotORG = 350169,
		TenYearDomainRenewalDotORG = 350170,
		TrafficBlazer = 371401,
		TrafficBlazerRenewal = 371411,
		DomainForSaleParkedPage = 371701,
		OnePageWebsite = 371702,
		DomainForSaleParkedPageRenewal = 371711,
		OnePageWebsiteRenewal = 371712,
		StealthRay = 371900,
		cSite = 372201,
		EnterpriseLevelDNS = 375001,
		EnterpriseLevelDNSRenewal = 385001,
		DomainAlertOnePack = 379002,
		DomainAlertBackorder = 379003,
		DomainAlertPowerListSubscription = 379004,
		DomainAlertOnePackRenewal = 389002,
		DomainAlertBackorderingRenewal = 389003,
		DomainAlertPowerlistSubscriptionRenewal = 389004,
		PrivateBackorderingRenewal = 389005,
		PrivateRegistrationServicesAPI = 377001,
		PrivateBackordering = 379005,
		PrivateRegistrationServicesRenewalAPI = 387001
	}

	#endregion

}
