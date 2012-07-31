using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using Cambro;
using Cambro.Web;
using Cambro.Misc;

using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;

using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;

namespace Bobs
{

	#region OutgoingSms
	[Serializable]
	public partial class OutgoingSms
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return (int)this[OutgoingSms.Columns.K]; }
			set { this[OutgoingSms.Columns.K] = value; }
		}
		/// <summary>
		/// The datetime that the message was sent
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[OutgoingSms.Columns.DateTime]; }
			set { this[OutgoingSms.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Type - the type of response
		/// </summary>
		public override Types Type
		{
			get { return (Types)this[OutgoingSms.Columns.Type]; }
			set { this[OutgoingSms.Columns.Type] = value; }
		}
		/// <summary>
		/// If theis sms was triggered by an incoming sms, this is the link to the IncomingSms table.
		/// </summary>
		public override int? IncomingSmsK
		{
			get { return (int?)this[OutgoingSms.Columns.IncomingSmsK]; }
			set { incomingSms = null; this[OutgoingSms.Columns.IncomingSmsK] = value; }
		}
		/// <summary>
		/// This is the full string that was posted to iTagg to send the message
		/// </summary>
		public override string PostString
		{
			get { return (string)this[OutgoingSms.Columns.PostString]; }
			set { this[OutgoingSms.Columns.PostString] = value; }
		}
		/// <summary>
		/// The message
		/// </summary>
		public override string Message
		{
			get { return (string)this[OutgoingSms.Columns.Message]; }
			set { this[OutgoingSms.Columns.Message] = value; }
		}
		/// <summary>
		/// Did the sms send OK? 0=OK, !0=Error
		/// </summary>
		public override int ErrorCode
		{
			get { return (int)this[OutgoingSms.Columns.ErrorCode]; }
			set { this[OutgoingSms.Columns.ErrorCode] = value; }
		}
		/// <summary>
		/// Error string returned
		/// </summary>
		public override string ErrorText
		{
			get { return (string)this[OutgoingSms.Columns.ErrorText]; }
			set { this[OutgoingSms.Columns.ErrorText] = value; }
		}
		/// <summary>
		/// Submission reference returned by the sms server
		/// </summary>
		public override string SubmissionReference
		{
			get { return (string)this[OutgoingSms.Columns.SubmissionReference]; }
			set { this[OutgoingSms.Columns.SubmissionReference] = value; }
		}
		/// <summary>
		/// How is this outgoing sms charged?
		/// </summary>
		public override ChargeTypes ChargeType
		{
			get { return (ChargeTypes)this[OutgoingSms.Columns.ChargeType]; }
			set { this[OutgoingSms.Columns.ChargeType] = value; }
		}
		/// <summary>
		/// Has the text been sent to the sms server properly?
		/// </summary>
		public override bool Sent
		{
			get { return (bool)this[OutgoingSms.Columns.Sent]; }
			set { this[OutgoingSms.Columns.Sent] = value; }
		}
		/// <summary>
		/// The mobile that the message is being sent to
		/// </summary>
		public override int MobileK
		{
			get { return (int)this[OutgoingSms.Columns.MobileK]; }
			set { mobile = null; this[OutgoingSms.Columns.MobileK] = value; }
		}
		/// <summary>
		/// Incoming type - Tonight or Pllay
		/// </summary>
		public override Model.Entities.IncomingSms.ServiceTypes ServiceType
		{
			get { return (Model.Entities.IncomingSms.ServiceTypes)this[OutgoingSms.Columns.ServiceType]; }
			set { this[OutgoingSms.Columns.ServiceType] = value; }
		}
		/// <summary>
		/// The message was successfully delivered (and charged if it is a premium rate sms)
		/// </summary>
		public override bool Delivered
		{
			get { return (bool)this[OutgoingSms.Columns.Delivered]; }
			set { this[OutgoingSms.Columns.Delivered] = value; }
		}
		#endregion

		public static ChargeTypes GetCharge(Types type)
		{
			switch (type)
			{
				case Types.FrontPagePhoto: return ChargeTypes.Premium150p;
				
				default:
					throw new Exception("can't find type");
			}
		}

		public void ReceivedDeliveryNote()
		{
			if (!this.Delivered)
			{
				this.Delivered = true;
				this.Update();

				if (Type == Types.FrontPagePhoto)
				{
					string[] arr = IncomingSms.Message.ToLower().Split(' ');
					Photo p = new Photo(int.Parse(arr[1]));

					string caption = "";
					if (arr.Length > 2)
					{
						for (int i = 2; i < arr.Length; i++)
							caption += (i > 2 ? " " : "") + arr[i];

						caption = Cambro.Web.Helpers.Strip(caption, true, true, true, true);

						if (caption.Length > 200)
							caption = caption.Substring(0, 195) + "...";
					}

					p.SetAsPhotoOfWeek(true, caption, false, true);
				}
			}

		}

		#region Send()
		public void Send()
		{
			string tarrif = "";
			bool premiumRate = false;
			string username = "d.brophy";
			string password = "foo";
			
			if (ChargeType.Equals(ChargeTypes.FreeBudget))
			{
				premiumRate = false;
			}
			else if (ChargeType.Equals(ChargeTypes.FreeAdvanced))
			{
				premiumRate = false;
			}
			else if (ChargeType.Equals(ChargeTypes.FreePremier))
			{
				premiumRate = false;
			}
			else if (ChargeType.Equals(ChargeTypes.FreePremierPorted))
			{
				premiumRate = false;
			}
			else if (ChargeType.Equals(ChargeTypes.FreePremierPlus))
			{
				premiumRate = false;
			}
			else if (ChargeType.Equals(ChargeTypes.Premium012p))
			{
				premiumRate = true;
				tarrif = "12";
			}
			else if (ChargeType.Equals(ChargeTypes.Premium025p))
			{
				premiumRate = true;
				tarrif = "25";
			}
			else if (ChargeType.Equals(ChargeTypes.Premium035p))
			{
				premiumRate = true;
				tarrif = "35";
			}
			else if (ChargeType.Equals(ChargeTypes.Premium050p))
			{
				premiumRate = true;
				tarrif = "50";
			}
			else if (ChargeType.Equals(ChargeTypes.Premium100p))
			{
				premiumRate = true;
				tarrif = "100";
			}
			else if (ChargeType.Equals(ChargeTypes.Premium150p))
			{
				premiumRate = true;
				tarrif = "150";
			}
			else if (ChargeType.Equals(ChargeTypes.Premium300p))
			{
				premiumRate = true;
				tarrif = "300";
			}
			else if (ChargeType.Equals(ChargeTypes.Premium500p))
			{
				premiumRate = true;
				tarrif = "500";
			}


			string url = "";

			if (premiumRate)
			{
				url = String.Format("https://secure.itagg.com/smsg/sms_prem.mes?usr=d.brophy&pwd=foo&tariff={0}&to={1};{2}&txt={3}&incoming_message_id={4}",
					tarrif,
					Mobile.Number,
					Mobile.Network.ToString(),
					HttpUtility.UrlEncode(Message, System.Text.Encoding.ASCII),
					IncomingSms.MessageID
				);
				//url = "https://secure.itagg.com/smsg/sms_prem.mes" +
				//    "?usr=" + username +
				//    "&pwd=" + password +
				//    "&to=" + Mobile.Number + ";" + Mobile.Network.ToString() +
				//    "&tarrif=" + tarrif +
				//    "&txt=" + HttpUtility.UrlEncode(Message, System.Text.Encoding.ASCII) +
				//    (IncomingSmsK > 0 ? "&incoming_message_id=" + IncomingSms.MessageID : "") +
				//    "&type=text";
			}
			else
			{
			}


			//string url = "http://www.itagg.com/smsg/sms" + (premiumRate ? "_prem" : "") + ".mes?usr=" + username + "&pwd=" + password +
			//    "&from=" + from +
			//    "&route=" + route +
			//    (premiumRate ? "" : "&type=text") +
			//    "&to=" + Mobile.Number + (premiumRate ? (";" + Mobile.Network.ToString()) : "") +
			//    "&txt=" + HttpUtility.UrlEncode(Message, System.Text.Encoding.ASCII);

			//if (Type.Equals(Types.BusinessCard))
			//{
			//    url = "http://itagg.com/smsg/sms.mes?usr=" + username + "&pwd=" + password +
			//        "&from=83248" +
			//        "&to=" + Mobile.Number +
			//        "&type=binary&route=8&txt=:06:05:04:23:F4:00-:00:42:45:47:49:4E:3A:56:43:41:52:44:0D:0A:56:45:52:53:49:4F:4E:3A:32:2E:31:0D:0A:4E:3A:54:4F:4E:49:47:48:54:0D:0A:54:45:4C:3B:50:52:45:46:3A:38:33:32:34:38:0D:0A:45:4E:44:3A:56:43:41:52:44:0D:0A";
			//}

			PostString = url;
			try
			{
				string resp = readHtmlPage(url);

				//if (Vars.DevEnv)
				//{
				//    Mailer sm = new Mailer();
				//    sm.Body = "<p>" + Message + "</p>";
				//    sm.Subject = "SMS - " + Message;
				//    sm.TemplateType = Mailer.TemplateTypes.AdminNote;
				//    sm.To = "d.brophy@dontstayin.com";
				//    sm.Send();
				//    if (HttpContext.Current != null)
				//    {
				//        HttpContext.Current.Response.Write("ChargeType: " + ChargeType + "<br>");
				//        HttpContext.Current.Response.Write("Type: " + Type + "<br>");
				//        HttpContext.Current.Response.Write("Message:<br><b>" + HttpUtility.HtmlEncode(Message).Replace("\n", "<br>") + "</b><br>");
				//        HttpContext.Current.Response.Write(HttpUtility.HtmlEncode(url) + "<br>&nbsp;<br>");
				//    }
				//}
				//else
				
				string[] respArr = resp.Split('\n')[1].Split('|');
				this.ErrorCode = int.Parse(respArr[0]);
				this.ErrorText = respArr[1];
				this.SubmissionReference = respArr[2].ToLower().Substring(0,32);
				this.Sent = true;
				this.Delivered = false;
			}
			catch
			{
				this.Sent = false;
				this.Delivered = false;
			}

			if (this.Sent)
			{
				this.Mobile.TotalOutgoing++;
				this.Mobile.Update();
			}

			this.Update();
		}
		#endregion

		protected string readHtmlPage(string url)
		{
			string result = "";
			WebRequest objRequest = WebRequest.Create(url);
			objRequest.ContentType = "application/x-www-form-urlencoded";
			WebResponse objResponse = objRequest.GetResponse();
			StreamReader sr = new StreamReader(objResponse.GetResponseStream());
			result = sr.ReadToEnd();
			sr.Close();
			return result;
		}



		#region Mobile
		public Mobile Mobile
		{
			get
			{
				if (mobile == null && MobileK > 0)
					mobile = new Mobile(MobileK);
				return mobile;
			}
			set
			{
				mobile = value;
			}
		}
		private Mobile mobile;
		#endregion

		#region IncomingSms
		public IncomingSms IncomingSms
		{
			get
			{
				if (incomingSms == null && IncomingSmsK.HasValue && IncomingSmsK.Value > 0)
					incomingSms = new IncomingSms(IncomingSmsK.Value);
				return incomingSms;
			}
			set
			{
				incomingSms = value;
			}
		}
		private IncomingSms incomingSms;
		#endregion

	}
	#endregion

}
