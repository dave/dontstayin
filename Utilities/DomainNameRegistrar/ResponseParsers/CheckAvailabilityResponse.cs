using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DomainNameRegistrar.ResponseParsers
{
	class CheckAvailabilityResponse : Response
	{
		internal List<Availability> Availabilities { get; private set; }
		internal List<string> DomainNames { get; private set; }
		internal ResponseCode ResponseCode { get; private set; }

		internal CheckAvailabilityResponse(string xml) : base(xml) { }

		#region This code works for the responses from the OTE system, but not PROD! see method below
		//internal override void InstantiateFromXml(XmlDocument xmlDocument)
		//{
		//    /* Expect testing environment response in the form:
		//     * 
		//        <response>
		//            <result code=\"1000\"/>
		//            <resdata>
		//                <check>
		//                    <domain name=\"example.us\" avail=\"1\"/>
		//                    <domain name=\"example.biz\" avail=\"1\"/>
		//                </check>
		//            </resdata>
		//        </response>
		//     */

		//    XmlNode responseNode = xmlDocument.DocumentElement;

		//    XmlNode resultNode = responseNode["result"];
		//    this.ResponseCode = (ResponseCode)int.Parse(resultNode.Attributes["code"].Value);

		//    DomainNames = new List<string>();
		//    Availabilities = new List<Availability>();
		//    if (responseNode["resdata"] != null && responseNode["resdata"]["check"] != null)
		//    {
		//        foreach (XmlNode domainNode in responseNode["resdata"]["check"].ChildNodes)
		//        {
		//            DomainNames.Add(domainNode.Attributes["name"].Value);
		//            Availabilities.Add(GetAvailability(int.Parse(domainNode.Attributes["avail"].Value)));
		//        }
		//    }
		//}
		#endregion

		internal override void InstantiateFromXml(XmlDocument xmlDocument)
		{
			/* Expect production response in the form:
			 * 
				<check>
					<domain name=\"example.us\" avail=\"1\"/>
					<domain name=\"example.biz\" avail=\"1\"/>
				</check>
			 */

			XmlNode checkNode = xmlDocument.DocumentElement;

			DomainNames = new List<string>();
			Availabilities = new List<Availability>();
			foreach (XmlNode domainNode in checkNode.ChildNodes)
			{
				DomainNames.Add(domainNode.Attributes["name"].Value);
				Availabilities.Add(GetAvailability(int.Parse(domainNode.Attributes["avail"].Value)));
			}

			this.ResponseCode = ResponseCode.Success;
		}
		internal override bool Validate()
		{
			return (ResponseCode.Success == this.ResponseCode);
		}

		internal Availability?[] AvailabilitiesForDomains(string[] domainNamesInOrder)
		{
			Availability?[] avails = new Availability?[domainNamesInOrder.Length];
			for (int i = 0; i < avails.Length; i++)
			{
				string domainName = domainNamesInOrder[i];
				int index = DomainNames.FindIndex(d => d == domainName);
				if (index >= 0)
				{
					avails[i] = Availabilities[index];
				}
				else
				{
					avails[i] = null;
				}
			}
			return avails;
		}
	}
}
