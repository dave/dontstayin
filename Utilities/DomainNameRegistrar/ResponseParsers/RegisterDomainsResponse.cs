using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DomainNameRegistrar.ResponseParsers
{
	class RegisterDomainsResponse : Response
	{
		internal int ShopperID { get; private set; }
		internal string UniqueClientTransactionID { get; private set; }
		internal string OrderID { get; private set; }
		internal string svTRID { get; private set; }
		internal ResponseCode ResponseCode { get; private set; }
		internal string Msg { get; private set; }

		internal RegisterDomainsResponse(string xml) : base(xml) { }

		internal override void InstantiateFromXml(XmlDocument xmlDocument)
		{
			/* Expect response in the form:
			 * 
				 <response [user="{shopper.userid}"] svTRID="order.{orderid}" clTRID="<uniqueClientTransactionID>">
					<result code="1000">
						<msg>processed 2 items</msg>
					</result>
					<resdata>
						<orderid>{orderid}</orderid>
					</resdata>
				 </response>
			 */

			XmlNode responseNode = xmlDocument.DocumentElement;

			foreach (XmlAttribute att in responseNode.Attributes)
			{
				switch (att.Name)
				{
					case "user":
						int shopperID;
						if (int.TryParse(att.Value, out shopperID)) this.ShopperID = shopperID;
						break;
					case "svTRID":
						this.svTRID = att.Value; break;
					case "clTRID":
						this.UniqueClientTransactionID = att.Value; break;
					default: break;
				}
			}

			foreach (XmlNode node in responseNode.ChildNodes)
			{
				switch (node.Name)
				{
					case "result":
						ResponseCode = (ResponseCode)int.Parse(node.Attributes["code"].Value);
						XmlNode msgNode = node["msg"];
						this.Msg = msgNode.InnerText;
						break;

					case "resdata":
						XmlNode orderidNode = node["orderid"];
						this.OrderID = orderidNode.InnerText;
						break;

					default: break;
				}
			}
		}

		internal override bool Validate()
		{
			return (ResponseCode.Success == this.ResponseCode);
		}
	}
}
