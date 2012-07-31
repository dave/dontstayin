using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bobs
{
	#region MixmagVote
	/// <summary>
	/// Votes in the Mixmag Vote system
	/// </summary>
	[Serializable]
	public partial class MixmagVote
	{

		#region Simple members
		/// <summary>
		/// Primary key
		/// </summary>
		public override int K
		{
			get { return (int)this[MixmagVote.Columns.K] as int? ?? 0; }
			set { this[MixmagVote.Columns.K] = value; }
		}
		/// <summary>
		/// Facebook user ID
		/// </summary>
		public override long FacebookUID
		{
			get { return (long)this[MixmagVote.Columns.FacebookUID]; }
			set { this[MixmagVote.Columns.FacebookUID] = value; }
		}
		/// <summary>
		/// Entry K
		/// </summary>
		public override int? MixmagEntryK
		{
			get { return (int?)this[MixmagVote.Columns.MixmagEntryK]; }
			set { this[MixmagVote.Columns.MixmagEntryK] = value; }
		}
		/// <summary>
		/// When the vote was cast
		/// </summary>
		public override DateTime? DateTime
		{
			get { return (DateTime?)this[MixmagVote.Columns.DateTime]; }
			set { this[MixmagVote.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Custom text field
		/// </summary>
		public override string TextField1
		{
			get { return (string)this[MixmagVote.Columns.TextField1]; }
			set { this[MixmagVote.Columns.TextField1] = value; }
		}
		#endregion

	}
	#endregion
}
