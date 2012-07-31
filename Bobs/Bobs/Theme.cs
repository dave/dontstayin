using System;
using System.Collections.Generic;
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
using System.Xml;

namespace Bobs
{

	#region Theme
	/// <summary>
	/// e.g. Promoter / Event Theme
	/// </summary>
	[Serializable] 
	public partial class Theme 
	{

		#region simple members

	

		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Theme.Columns.K] as int? ?? 0; }
			set { this[Theme.Columns.K] = value; }
		}
		/// <summary>
		/// Name for url's
		/// </summary>
		public override string UrlName
		{
			get { return (string)this[Theme.Columns.UrlName]; }
			set { this[Theme.Columns.UrlName] = value; }
		}
		/// <summary>
		/// Proper name
		/// </summary>
		public override string Name
		{
			get { return (string)this[Theme.Columns.Name]; }
			set { this[Theme.Columns.Name] = value; }
		}
		/// <summary>
		/// Short description
		/// </summary>
		public override string Description
		{
			get { return (string)this[Theme.Columns.Description]; }
			set { this[Theme.Columns.Description] = value; }
		}
		/// <summary>
		/// Examples of topics in this theme... dollar seperated, all lower case.
		/// </summary>
		public override string Examples
		{
			get { return (string)this[Theme.Columns.Examples]; }
			set { this[Theme.Columns.Examples] = value; }
		}
		/// <summary>
		/// Order in lists
		/// </summary>
		public override double Order
		{
			get { return (double)this[Theme.Columns.Order]; }
			set { this[Theme.Columns.Order] = value; }
		}
		#endregion


		public string ExamplesHtml1
		{
			get
			{
				return "";
			}
		}
		public string ExamplesHtml
		{
			get
			{
				string s = "";
				string[] examplesArr = Examples.Split('$');
				for(int i=0;i<examplesArr.Length;i++)
				{
					s+=(i==0?"":", ")+examplesArr[i];
				}
				return s;
			}
		}

		public string RadioButtonText
		{
			get
			{
				return "<span style=\"font-size:14px;font-weight:bold;\">&nbsp;"+Name+"</span><p style=\"margin-left:26px;margin-top:0px;margin-bottom:10px;\">e.g. "+ExamplesHtml+"</p>";
			}
		}

	}

	 

	#endregion

}
