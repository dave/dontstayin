using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bobs
{
	#region BacardiEmail
	/// <summary>
	/// Emails gathered for Bacardi
	/// </summary>
	[Serializable]
	public partial class BacardiEmail
	{

		#region Simple members
		/// <summary>
		/// Primary K
		/// </summary>
		public override int K
		{
			get { return this[BacardiEmail.Columns.K] as int? ?? 0; }
			set { this[BacardiEmail.Columns.K] = value; }
		}
		/// <summary>
		/// Email
		/// </summary>
		public override string Email
		{
			get { return (string)this[BacardiEmail.Columns.Email]; }
			set { this[BacardiEmail.Columns.Email] = value; }
		}
		#endregion

	}
	#endregion
}
