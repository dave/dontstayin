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

	#region Region 
	[Serializable] 
	public partial class Region : IPage
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Region.Columns.K] as int? ?? 0; }
			set { this[Region.Columns.K] = value; }
		}
		/// <summary>
		/// Link to the country table
		/// </summary>
		public override int CountryK
		{
			get { return (int)this[Region.Columns.CountryK]; }
			set { country = null; this[Region.Columns.CountryK] = value; }
		}
		/// <summary>
		/// SubCountry in the Places table
		/// </summary>
		public override int SubCountry
		{
			get { return (int)this[Region.Columns.SubCountry]; }
			set { this[Region.Columns.SubCountry] = value; }
		}
		/// <summary>
		/// Name
		/// </summary>
		public override string Name
		{
			get { return (string)this[Region.Columns.Name]; }
			set { this[Region.Columns.Name] = value; }
		}
		/// <summary>
		/// Abbreviation (usually US state code)
		/// </summary>
		public override string Abbreviation
		{
			get { return (string)this[Region.Columns.Abbreviation]; }
			set { this[Region.Columns.Abbreviation] = value; }
		}
		/// <summary>
		/// Population in thousands
		/// </summary>
		public override double Population
		{
			get { return (double)this[Region.Columns.Population]; }
			set { this[Region.Columns.Population] = value; }
		}
		/// <summary>
		/// Area in sq km
		/// </summary>
		public override double Area
		{
			get { return (double)this[Region.Columns.Area]; }
			set { this[Region.Columns.Area] = value; }
		}
		#endregion


		public static OrderBy RegionOrder
		{
			get
			{
				return new OrderBy(new OrderBy(Bobs.Place.Columns.CountryK,OrderBy.OrderDirection.Ascending),new OrderBy(Bobs.Place.Columns.SubCountry,OrderBy.OrderDirection.Ascending),new OrderBy(Bobs.Place.Columns.Population,OrderBy.OrderDirection.Descending));
			}
		}

		public Country Country
		{
			get
			{
				if (country==null && CountryK>0)
					country=new Country(CountryK);
				return country;
			}
			set
			{
				country=value;
			}
		}
		Country country;



		#region IPage Members

		public string Url(params string[] par)
		{
			if (this.Country.UseRegion)
			{
				return UrlInfo.MakeUrl(this.Country.UrlName + "/" + this.Abbreviation.ToLower(), null, par);
			}
			else
			{
				string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "K", this.CountryK.ToString(), "RegionK", this.K.ToString() }, par);
				return UrlInfo.PageUrl("Country", fullParams);
			}
		}
		
		#endregion






	}
	#endregion

}
