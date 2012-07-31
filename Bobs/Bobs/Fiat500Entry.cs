using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bobs
{
	#region Fiat500Entry
	/// <summary>
	/// Entries for the Fiat 500 competition
	/// </summary>
	[Serializable]
	public partial class Fiat500Entry
	{

		#region Simple members
		/// <summary>
		/// Primary key
		/// </summary>
		public override int K
		{
			get { return this[Fiat500Entry.Columns.K] as int? ?? 0; }
			set { this[Fiat500Entry.Columns.K] = value; }
		}
		/// <summary>
		/// K of Usr who filled out the form
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Fiat500Entry.Columns.UsrK]; }
			set { this[Fiat500Entry.Columns.UsrK] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override DateTime Submitted
		{
			get { return (DateTime)this[Fiat500Entry.Columns.Submitted]; }
			set { this[Fiat500Entry.Columns.Submitted] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override string FirstName
		{
			get { return (string)this[Fiat500Entry.Columns.FirstName]; }
			set { this[Fiat500Entry.Columns.FirstName] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override string LastName
		{
			get { return (string)this[Fiat500Entry.Columns.LastName]; }
			set { this[Fiat500Entry.Columns.LastName] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override string MobileNumber
		{
			get { return (string)this[Fiat500Entry.Columns.MobileNumber]; }
			set { this[Fiat500Entry.Columns.MobileNumber] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override string EmailAddress
		{
			get { return (string)this[Fiat500Entry.Columns.EmailAddress]; }
			set { this[Fiat500Entry.Columns.EmailAddress] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override string HouseNumberAndStreetName
		{
			get { return (string)this[Fiat500Entry.Columns.HouseNumberAndStreetName]; }
			set { this[Fiat500Entry.Columns.HouseNumberAndStreetName] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override string Town
		{
			get { return (string)this[Fiat500Entry.Columns.Town]; }
			set { this[Fiat500Entry.Columns.Town] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override string City
		{
			get { return (string)this[Fiat500Entry.Columns.City]; }
			set { this[Fiat500Entry.Columns.City] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override string County
		{
			get { return (string)this[Fiat500Entry.Columns.County]; }
			set { this[Fiat500Entry.Columns.County] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override string PostCode
		{
			get { return (string)this[Fiat500Entry.Columns.PostCode]; }
			set { this[Fiat500Entry.Columns.PostCode] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override bool AcceptConditions
		{
			get { return (bool)this[Fiat500Entry.Columns.AcceptConditions]; }
			set { this[Fiat500Entry.Columns.AcceptConditions] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override int NumberOfKids
		{
			get { return (int)this[Fiat500Entry.Columns.NumberOfKids]; }
			set { this[Fiat500Entry.Columns.NumberOfKids] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override bool NotifyByEmail
		{
			get { return (bool)this[Fiat500Entry.Columns.NotifyByEmail]; }
			set { this[Fiat500Entry.Columns.NotifyByEmail] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override bool NotifyByPost
		{
			get { return (bool)this[Fiat500Entry.Columns.NotifyByPost]; }
			set { this[Fiat500Entry.Columns.NotifyByPost] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override bool NotifyByPhone
		{
			get { return (bool)this[Fiat500Entry.Columns.NotifyByPhone]; }
			set { this[Fiat500Entry.Columns.NotifyByPhone] = value; }
		}
		/// <summary>
		/// server
		/// </summary>
		public override bool NotifyBySms
		{
			get { return (bool)this[Fiat500Entry.Columns.NotifyBySms]; }
			set { this[Fiat500Entry.Columns.NotifyBySms] = value; }
		}
		#endregion
	}
	#endregion
}
