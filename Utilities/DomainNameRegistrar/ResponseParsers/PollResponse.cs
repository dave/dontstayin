using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DomainNameRegistrar.ResponseParsers
{
	class PollResponse : Response
	{
		internal string UniqueClientTransactionID { get; private set; }
		internal int Count { get; private set; }
		internal List<ItemResponse> Items { get; private set; }
		internal ResponseCode ResponseCode { get; private set; }
		internal string Msg { get; private set; }

		internal List<ItemResponse> ProcessedItems
		{
			get
			{
				List<ItemResponse> processedItems = new List<ItemResponse>();
				foreach (ItemResponse i in Items)
				{
					if (i.ItemResponseStatus == ItemResponse.Status.Processed)
					{
						processedItems.Add(i);
					}
				}
				return processedItems;
			}
		}

		public PollResponse(string xml) : base(xml) { }
		internal override void InstantiateFromXml(XmlDocument xmlDocument)
		{
			/* Expect xml in the form:

				<response clTRID="70ec7ab8-dbb9-4ffc-a6cb-aa68c8daecfb">
					<result code="1004">
						<msg>messages waiting</msg>
					</result>
					<msgQ count="2" date="12-10-2007 03:11:36" />
					<resdata>
						<REPORT><ITEM orderid="31478" roid="!roid!" riid="1" resourceid="domain:11210" status="2" timestamp="12/10/2007 3:11:36 AM"/><ITEM orderid="31478" roid="!roid!" riid="absded" resourceid="domain:29282" status="2" timestamp="12/10/2007 3:11:36 AM"/></REPORT>
					</resdata>
				</response>
			*/

			XmlNode responseNode = xmlDocument.DocumentElement;
			this.UniqueClientTransactionID = responseNode.Attributes["clTRID"].Value;

			foreach (XmlNode node in responseNode.ChildNodes)
			{
				switch (node.Name)
				{
					case "result":
						ResponseCode = (ResponseCode)int.Parse(node.Attributes["code"].Value);
						Msg = node["msg"].InnerText;
						break;

					case "msgQ":
						Count = int.Parse(node.Attributes["count"].Value);
						break;

					case "resdata":
						Items = new List<ItemResponse>();
						foreach (XmlNode itemNode in node["REPORT"].ChildNodes)
						{
							Items.Add(new ItemResponse(itemNode));
						}
						break;

					default: break;
				}
			}
		}
		internal override bool Validate()
		{
			return (ResponseCode.MessagesWaiting == this.ResponseCode &&
				"messages waiting" == Msg &&
				this.Count == this.Items.Count);
		}
	}

	class ItemResponse
	{
		/*
		   <ITEM orderid="31478" roid="!roid!" riid="1" resourceid="domain:11210" status="2" timestamp="12/10/2007 3:11:36 AM"/>
		 */

		internal enum Status
		{
			Delivered = 1,
			Processed = 2,
			Cancelled = 3,
			Renewed = 4,
			AutoRenewFailed = 5,
			AutoRenewOff = 6,
			AutoRenewOn = 7,
			TransferAway = 19,
			ResourceIdChange = 20,
			InvalidDetails = 96,
			InsufficientFunds = 97,
			NotProcessed = 98,
			InvalidProduct = 99,
			Error = 999
		}
		internal Status ItemResponseStatus { get; private set; }
		internal int OrderID { get; private set; }
		internal string ResourceID { get; private set; }
		internal string RIID { get; private set; }
		internal string ROID { get; private set; }

		public ItemResponse(XmlNode itemNode)
		{
			ItemResponseStatus = (Status)int.Parse(itemNode.Attributes["status"].Value);
			OrderID = int.Parse(itemNode.Attributes["orderid"].Value);
			RIID = itemNode.Attributes["riid"].Value;
			ROID = itemNode.Attributes["roid"].Value;
			if (Status.Processed == ItemResponseStatus)
			{
				ResourceID = itemNode.Attributes["resourceid"].Value;
			}
		}
	}
}
