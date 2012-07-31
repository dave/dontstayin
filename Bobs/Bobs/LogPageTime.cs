using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct Bob class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.

	#region LogPageTime
	/// <summary>
	/// Log of each page load time and page reference
	/// </summary>
	[Serializable] 
	public partial class LogPageTime
	{
		#region Simple members
        /// <summary>
        /// Primary key
        /// </summary>
        public override int K
        {
            get { return this[LogPageTime.Columns.K] as int? ?? 0; }
            set { this[LogPageTime.Columns.K] = value; }
        }
		/// <summary>
		/// Start time of page load
		/// </summary>
		public override DateTime StartDateTime
		{
			get { return (DateTime)this[LogPageTime.Columns.StartDateTime]; }
			set { this[LogPageTime.Columns.StartDateTime] = value; }
		}
		/// <summary>
		/// End time of page load
		/// </summary>
		public override DateTime EndDateTime
		{
			get { return (DateTime)this[LogPageTime.Columns.EndDateTime]; }
			set { this[LogPageTime.Columns.EndDateTime] = value; }
		}
		/// <summary>
		/// Current page filter
		/// </summary>
		public override string CurrentFilter
		{
			get { return (string)this[LogPageTime.Columns.CurrentFilter]; }
			set { this[LogPageTime.Columns.CurrentFilter] = value; }
		}
		/// <summary>
		/// Path of the master container page
		/// </summary>
		public override string MasterPath
		{
			get { return (string)this[LogPageTime.Columns.MasterPath]; }
			set { this[LogPageTime.Columns.MasterPath] = value; }
		}
		/// <summary>
		/// Page path
		/// </summary>
		public override string PagePath
		{
			get { return (string)this[LogPageTime.Columns.PagePath]; }
			set { this[LogPageTime.Columns.PagePath] = value; }
		}
		/// <summary>
		/// K of object referenced in the current filter
		/// </summary>
		public override int ObjectFilterK
		{
			get { return (int)this[LogPageTime.Columns.ObjectFilterK]; }
			set { this[LogPageTime.Columns.ObjectFilterK] = value; }
		}
        /// <summary>
        /// Type of object referenced in the current filter
        /// </summary>
		public override Model.Entities.ObjectType ObjectFilterType
        {
            get { return (Model.Entities.ObjectType)this[LogPageTime.Columns.ObjectFilterType]; }
            set { this[LogPageTime.Columns.ObjectFilterType] = value; }
        }
        /// <summary>
        /// Web server name
        /// </summary>
		public override string MachineName
        {
            get { return (string)this[LogPageTime.Columns.MachineName]; }
            set { this[LogPageTime.Columns.MachineName] = value; }
        }
        /// <summary>
		/// <summary>
		/// K of current user
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[LogPageTime.Columns.UsrK]; }
			set { this[LogPageTime.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Total Database select queries used to generate page
		/// </summary>
		public override int Selects
		{
			get { return (int)this[LogPageTime.Columns.Selects]; }
			set { this[LogPageTime.Columns.Selects] = value; }
		}
		/// <summary>
		/// Total Database update queries used to generate page
		/// </summary>
		public override int Updates
		{
			get { return (int)this[LogPageTime.Columns.Updates]; }
			set { this[LogPageTime.Columns.Updates] = value; }
		}
		/// <summary>
		/// Total Database insert queries used to generate page
		/// </summary>
		public override int Inserts
		{
			get { return (int)this[LogPageTime.Columns.Inserts]; }
			set { this[LogPageTime.Columns.Inserts] = value; }
		}
		/// <summary>
		/// Total Database delete queries used to generate page
		/// </summary>
		public override int Deletes
		{
			get { return (int)this[LogPageTime.Columns.Deletes]; }
			set { this[LogPageTime.Columns.Deletes] = value; }
		}
        /// <summary>
        /// Is page request a GET or POST. GET = true, POST = false
        /// </summary>
		public override bool IsGet
        {
            get { return (bool)this[LogPageTime.Columns.IsGet]; }
            set { this[LogPageTime.Columns.IsGet] = value; }
        }
		/// <summary>
		/// The Current Url
		/// </summary>
		public override string Url
		{
			get { return (string)this[LogPageTime.Columns.Url]; }
			set { this[LogPageTime.Columns.Url] = value; }
		}
		/// <summary>
		/// Post data of Request, if Request exceeded certain duration
		/// </summary>
		public override string PostData
		{
			get { return (string)this[LogPageTime.Columns.PostData]; }
			set { this[LogPageTime.Columns.PostData] = value; }
		}
		/// <summary>
		/// DSI Browser Guid
		/// </summary>
		public override Guid DsiGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[LogPageTime.Columns.DsiGuid]); }
			set { this[LogPageTime.Columns.DsiGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// User's IP address
		/// </summary>
		public override string IpAddress
		{
			get { return (string)this[LogPageTime.Columns.IpAddress]; }
			set { this[LogPageTime.Columns.IpAddress] = value; }
		}
		/// <summary>
		/// Is the browser a crawler?
		/// </summary>
		public override bool? IsCrawler
		{
			get { return (bool?)this[LogPageTime.Columns.IsCrawler]; }
			set { this[LogPageTime.Columns.IsCrawler] = value; }
		}
		/// <summary>
		/// Is this an AJAX request?
		/// </summary>
		public override bool? IsAjaxRequest
		{
			get { return (bool?)this[LogPageTime.Columns.IsAjaxRequest]; }
			set { this[LogPageTime.Columns.IsAjaxRequest] = value; }
		}
		/// <summary>
		/// Has Page.Render fired?
		/// </summary>
		public override bool? IsRendered
		{
			get { return (bool?)this[LogPageTime.Columns.IsRendered]; }
			set { this[LogPageTime.Columns.IsRendered] = value; }
		}
		#endregion
	}
	#endregion
}
