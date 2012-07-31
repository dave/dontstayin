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

namespace Bobs
{

	#region IpCountry
	/// <summary>
	/// Links to many places that the user may like to visit.
	/// </summary>
	[Serializable] 
	public partial class IpCountry 
	{

		#region simple members
		/// <summary>
		/// Ip from
		/// </summary>
		public override Int64 IpFrom
		{
			get { return (Int64)this[IpCountry.Columns.IpFrom]; }
			set { this[IpCountry.Columns.IpFrom] = value; }
		}
		/// <summary>
		/// Ip to
		/// </summary>
		public override Int64 IpTo
		{
			get { return (Int64)this[IpCountry.Columns.IpTo]; }
			set { this[IpCountry.Columns.IpTo] = value; }
		}
		/// <summary>
		/// Country code 2 letter
		/// </summary>
		public override string Code2Letter
		{
			get { return (string)this[IpCountry.Columns.Code2Letter]; }
			set { this[IpCountry.Columns.Code2Letter] = value; }
		}
		/// <summary>
		/// Country code 3 letter
		/// </summary>
		public override string Code3Letter
		{
			get { return (string)this[IpCountry.Columns.Code3Letter]; }
			set { this[IpCountry.Columns.Code3Letter] = value; }
		}
		/// <summary>
		/// Country name
		/// </summary>
		public override string Name
		{
			get { return (string)this[IpCountry.Columns.Name]; }
			set { this[IpCountry.Columns.Name] = value; }
		}
		/// <summary>
		/// Link to the country database
		/// </summary>
		public override int CountryK
		{
			get { return (int)this[IpCountry.Columns.CountryK]; }
			set { this[IpCountry.Columns.CountryK] = value; }
		}
		#endregion

		#region ClientCountry
		public static Country ClientCountry
		{
			get
			{
				return new Country(ClientCountryK());
			}
		}
		#endregion

		public static int ClientCountryK()
		{
			int defaultCountryK = 224;
			try
			{
				if (HttpContext.Current != null)
				{
					string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
					IpCountry ipc = Lookup(ip);
					if (ipc == null)
						return defaultCountryK;
					else
					{
						if (ipc.Country == null)
							return defaultCountryK;
						else
						{
							if (ipc.Country.Enabled)
								return ipc.Country.K;
							else
								return defaultCountryK;

						}
					}
				}
				else
					return defaultCountryK;
			}
			catch
			{
				return defaultCountryK;
			}
		}

		public static IpCountry Lookup(string ip)
		{
			try
			{
				string[] arr = ip.Split('.');
				Int64 ipInt = (Int64.Parse(arr[0]) * 16777216) + (Int64.Parse(arr[1]) * 65536) + (Int64.Parse(arr[2]) * 256) + Int64.Parse(arr[3]);
				Query q = new Query();
				q.NoLock = true;
				q.QueryCondition = new And(
						new Q(IpCountry.Columns.IpFrom, QueryOperator.LessThanOrEqualTo, ipInt),
						new Q(IpCountry.Columns.IpTo, QueryOperator.GreaterThanOrEqualTo, ipInt)
					);
				q.TopRecords = 1;
				IpCountrySet ipcs = new IpCountrySet(q);
				if (ipcs.Count == 1)
					return ipcs[0];
				else
					return null;
			}
			catch
			{
				return null;
			}
		}

		#region Links to Bobs
		#region Country
		public Country Country
		{
			get
			{
				if (country==null)
				{
					CountrySet cs = new CountrySet(new Query(new Q(Country.Columns.Code2Letter,Code2Letter)));
					if (cs.Count==1)
						country = cs[0];
					else
					{
						CountrySet cs1 = new CountrySet(new Query(new Q(Country.Columns.Code3Letter,Code3Letter)));
						if (cs1.Count==1)
							country = cs1[0];
					}
				}
				return country;
			}
		}
		Country country;
		#endregion
		#endregion

	}
	#endregion

}
