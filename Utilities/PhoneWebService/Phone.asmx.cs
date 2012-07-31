using System;
using System.Web;
using System.Net;
using System.IO;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace PhoneWebService
{
	/// <summary>
	/// Summary description for Service1
	/// </summary>
	[WebService(Namespace = "http://hoth.dontstayin.com/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class Phone : System.Web.Services.WebService
	{
		[WebMethod]
		public void MakeCall(string PhoneIpAddress, string Number)
		{
			WebClient client = new WebClient();
			client.Credentials = new NetworkCredential("admin", "foo");
			client.Proxy.Credentials = new NetworkCredential("admin", "foo");
			try
			{
				Stream data = client.OpenRead("http://" + PhoneIpAddress + "/index.htm?number=" + Number);
				data.Close();
			}
			catch { }




		}
	}
}
