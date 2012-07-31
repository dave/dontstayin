using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace Bobs
{
	#region Phone
	/// <summary>
	/// Phone handset settings helper
	/// </summary>
	[Serializable] 
	public partial class Phone
	{

		#region Simple members
		/// <summary>
		/// Key
		/// </summary>
		public override int K
		{
			get { return this[Phone.Columns.K] as int? ?? 0; }
			set { this[Phone.Columns.K] = value; }
		}
		/// <summary>
		/// Link to usr
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Phone.Columns.UsrK]; }
			set { this[Phone.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Phone extention - e.g. 200 or 201 etc.
		/// </summary>
		public override int Extention
		{
			get { return (int)this[Phone.Columns.Extention]; }
			set { this[Phone.Columns.Extention] = value; }
		}
		/// <summary>
		/// Mac address of the phone handset
		/// </summary>
		public override string Mac
		{
			get { return (string)this[Phone.Columns.Mac]; }
			set { this[Phone.Columns.Mac] = value; }
		}
		/// <summary>
		/// IP address of the phone handset
		/// </summary>
		public override string IpAddress
		{
			get { return (string)this[Phone.Columns.IpAddress]; }
			set { this[Phone.Columns.IpAddress] = value; }
		}
		/// <summary>
		/// The IP of the phone on the local network
		/// </summary>
		public override string LocalIpAddress
		{
			get { return (string)this[Phone.Columns.LocalIpAddress]; }
			set { this[Phone.Columns.LocalIpAddress] = value; }
		}
		/// <summary>
		/// The gateway on the local network
		/// </summary>
		public override string LocalGateway
		{
			get { return (string)this[Phone.Columns.LocalGateway]; }
			set { this[Phone.Columns.LocalGateway] = value; }
		}
		/// <summary>
		/// The dns server on the local network
		/// </summary>
		public override string LocalDns
		{
			get { return (string)this[Phone.Columns.LocalDns]; }
			set { this[Phone.Columns.LocalDns] = value; }
		}
		/// <summary>
		/// The Nat redirect port
		/// </summary>
		public override string NatPort
		{
			get { return (string)this[Phone.Columns.NatPort]; }
			set { this[Phone.Columns.NatPort] = value; }
		}
		/// <summary>
		/// Test column - not used
		/// </summary>
		public override string TestColumn
		{
			get { return (string)this[Phone.Columns.TestColumn]; }
			set { this[Phone.Columns.TestColumn] = value; }
		}
		/// <summary>
		/// Test column 1 - not used
		/// </summary>
		public override string TestColumn1
		{
		    get { return (string)this[Phone.Columns.TestColumn1]; }
		    set { this[Phone.Columns.TestColumn1] = value; }
		}
		/// <summary>
		/// Test column 2 - not used
		/// </summary>
		public override string TestColumn2
		{
			get { return (string)this[Phone.Columns.TestColumn2]; }
			set { this[Phone.Columns.TestColumn2] = value; }
		}
		#endregion

		public void MakeCall(string number)
		{

			com.dontstayin.hoth.Phone p = new Bobs.com.dontstayin.hoth.Phone();
			p.MakeCall(this.LocalIpAddress, number);

			//try
			//{
			//    if (this.IpAddress.Length > 0 && !(this.IpAddress.StartsWith("192.168.113.") || this.IpAddress.StartsWith("192.168.16.")))
			//    {
			//        //make call directly
			//        WebClient client = new WebClient();
			//        client.Credentials = new NetworkCredential("admin", "foo");
			//        client.Proxy.Credentials = new NetworkCredential("admin", "foo");
			//        try
			//        {
			//            string port = NatPort == null || NatPort.Length == 0 ? "" : (":" + NatPort);
			//            Stream data = client.OpenRead("http://" + this.IpAddress + port + "/index.htm?number=" + number);
			//            data.Close();
			//        }
			//        catch { }
			//    }
			//    else
			//    {
			//        com.dontstayin.hoth.Phone p = new Bobs.com.dontstayin.hoth.Phone();
			//        p.MakeCall(this.DefaultIp, number);
			//    }
			//}
			//catch
			//{
			//    com.dontstayin.hoth.Phone p = new Bobs.com.dontstayin.hoth.Phone();
			//    p.MakeCall(this.DefaultIp, number);
			//}
		}

        public string PhoneNumber
        {
            get
            {
                return "0207 0990 " + this.Extention.ToString();
            }
        }

		public static Phone GetFromMac(string Mac)
		{
			PhoneSet ps = new PhoneSet(new Query(new Q(Phone.Columns.Mac, Mac)));
			if (ps.Count != 1)
				return null;

			return ps[0];
		}

		public static Phone GetFromExtention(string Extention)
		{
			PhoneSet ps = new PhoneSet(new Query(new Q(Phone.Columns.Extention, Extention)));
			if (ps.Count != 1)
				return null;

			return ps[0];
		}

		public static Phone GetFromUsrK(int UsrK)
		{
			PhoneSet ps = new PhoneSet(new Query(new Q(Phone.Columns.UsrK, UsrK)));
			if (ps.Count != 1)
				return null;

			return ps[0];
		}

		#region DefaultIp
		public string DefaultIp
		{
		    get
		    {
		        return "192.168.113." + Extention.ToString();
		    }
		}
		#endregion

		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null && UsrK > 0)
					usr = new Usr(UsrK);
				return usr;
			}
			set
			{
				usr = value;
			}
		}
		Usr usr;
		#endregion

	}
	#endregion
}
