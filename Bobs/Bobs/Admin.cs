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

	#region Admin
	/// <summary>
	/// Specifies admin privilages for users
	/// </summary>
	[Serializable] 
	public partial class Admin
	{
		
		#region Simple members
		/// <summary>
		/// Primary key
		/// </summary>
		public override int K
		{
			get { return this[Admin.Columns.K] as int? ?? 0; }
			set { this[Admin.Columns.K] = value; }
		}
		/// <summary>
		/// Link to the user table
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Admin.Columns.UsrK]; }
			set { usr = null; this[Admin.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Object type - 1=Country, 2=Place
		/// </summary>
		public override AdminObjectType ObjectType
		{
			get { return (AdminObjectType)this[Admin.Columns.ObjectType]; }
			set { objectBob = null; place = null; country = null; this[Admin.Columns.ObjectType] = value; }
		}
		/// <summary>
		/// Key in object table
		/// </summary>
		public override int ObjectK
		{
			get { return (int)this[Admin.Columns.ObjectK]; }
			set { objectBob = null; place = null; country = null; this[Admin.Columns.ObjectK] = value; }
		}
		#endregion

		#region ObjectType
		#endregion

		#region Links to Bobs
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null)
				{
					usr = new Usr(UsrK);
				}
				return usr;
			}
		}
		private Usr usr;
		#endregion
		#region ObjectBob
		public IBob ObjectBob
		{
			get
			{
				if (objectBob == null)
				{
					if (ObjectType.Equals(AdminObjectType.Country))
						objectBob = new Country(ObjectK);
					else if (ObjectType.Equals(AdminObjectType.Place))
						objectBob = new Place(ObjectK);
				}
				return objectBob;
			}
		}
		IBob objectBob;
		#endregion
		#region Place
		public Place Place
		{
			get
			{
				if (place == null)
				{
					if (ObjectType.Equals(AdminObjectType.Place))
						place = new Place(ObjectK);
				}
				return place;
			}
		}
		Place place;
		#endregion
		#region Country
		public Country Country
		{
			get
			{
				if (country == null)
				{
					if (ObjectType.Equals(AdminObjectType.Country))
						country = new Country(ObjectK);
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
