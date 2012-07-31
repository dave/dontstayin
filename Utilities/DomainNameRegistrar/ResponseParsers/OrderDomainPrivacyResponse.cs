using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DomainNameRegistrar.ResponseParsers
{
	class OrderDomainPrivacyResponse : Response
	{
		internal int UserID { get; private set; }
		internal int DbpUserID { get; private set; }
		internal string OrderID { get; private set; }
		internal string UniqueClientTransactionID { get; private set; }
		internal ResponseCode ResponseCode { get; private set; }
		internal string Msg { get; private set; }
		internal List<int> OrderIDs { get; private set; }

		internal OrderDomainPrivacyResponse(string xml) : base(xml) { }
		internal override void InstantiateFromXml(XmlDocument xmlDocument)
		{
			/* Expected format:
			 
			<response user="142888" dbpuser="142889" svTRID="order.31591" clTRID="309edd90-8be9-4c20-acad-d14725d356a6">
				<result code="1000">
					<msg>processed 1 item</msg>
				</result>
				<resdata>
					<orderid>31591</orderid>
				</resdata>
			</response>
			 */

			XmlNode responseNode = xmlDocument.DocumentElement;
			this.UserID = int.Parse(responseNode.Attributes["user"].Value);
			this.DbpUserID = int.Parse(responseNode.Attributes["dbpuser"].Value);
			this.OrderID = responseNode.Attributes["svTRID"].Value;
			this.UniqueClientTransactionID = responseNode.Attributes["clTRID"].Value;

			XmlNode resultElement = responseNode["result"];
			this.ResponseCode = (ResponseCode)int.Parse(resultElement.Attributes["code"].Value);
			this.Msg = resultElement["msg"].InnerText;

			XmlNode resdataNode = responseNode["resdata"];
			OrderIDs = new List<int>();
			foreach (XmlNode orderidNode in resdataNode.ChildNodes)
			{
				OrderIDs.Add(int.Parse(orderidNode.InnerText));
			}
		}

		internal override bool Validate()
		{
			return (ResponseCode.Success == this.ResponseCode &&
				"processed " + this.OrderIDs.Count + " item" == this.Msg);
		}
	}
}
