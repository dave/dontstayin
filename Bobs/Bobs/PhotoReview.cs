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

	#region PhotoReview
	/// <summary>
	/// Rating of a photo
	/// </summary>
	[Serializable] 
	public partial class PhotoReview 
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[PhotoReview.Columns.K] as int? ?? 0; }
			set { this[PhotoReview.Columns.K] = value; }
		}
		/// <summary>
		/// Link to one user - the user that submitted the review
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[PhotoReview.Columns.UsrK]; }
			set { usr = null; this[PhotoReview.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Link to one photo
		/// </summary>
		public override int PhotoK
		{
			get { return (int)this[PhotoReview.Columns.PhotoK]; }
			set { photo = null; this[PhotoReview.Columns.PhotoK] = value; }
		}
		/// <summary>
		/// Date/time the review was added
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[PhotoReview.Columns.DateTime]; }
			set { this[PhotoReview.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Rate the photo 0-9 on...
		/// </summary>
		public override int Rating
		{
			get { return (int)this[PhotoReview.Columns.Rating]; }
			set { this[PhotoReview.Columns.Rating] = value; }
		}
		/// <summary>
		/// 1=Cool, 2=Sexy, 3=Sexy (any more?)
		/// </summary>
		public override RatingTypes RatingType
		{
			get { return (RatingTypes)this[PhotoReview.Columns.RatingType]; }
			set { this[PhotoReview.Columns.RatingType] = (int)value; }
		}

		#endregion
		

		#region Links to Bobs

		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr==null)
					usr = new Usr(UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion
		#region Photo
		public Photo Photo
		{
			get
			{
				if (photo==null)
					photo = new Photo(PhotoK);
				return photo;
			}
		}
		Photo photo;
		#endregion
		
		#endregion

	}
	#endregion

}

