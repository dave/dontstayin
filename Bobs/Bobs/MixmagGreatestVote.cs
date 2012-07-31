using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	#region MixmagGreatestVote
	/// <summary>
	/// Vote for the mixmag greatest DJ competition
	/// </summary>
	[Serializable]
	public partial class MixmagGreatestVote
	{

		#region Simple members
		/// <summary>
		/// Facebook unique id
		/// </summary>
		public override long FacebookUid
		{
			get { return (long)this[MixmagGreatestVote.Columns.FacebookUid]; }
			set { this[MixmagGreatestVote.Columns.FacebookUid] = value; }
		}
		/// <summary>
		/// DJ that they voted for
		/// </summary>
		public override int MixmagGreatestDjK
		{
			get { return (int)this[MixmagGreatestVote.Columns.MixmagGreatestDjK]; }
			set { this[MixmagGreatestVote.Columns.MixmagGreatestDjK] = value; }
		}
		/// <summary>
		/// Date time they voted
		/// </summary>
		public override DateTime? DateTime
		{
			get { return (DateTime?)this[MixmagGreatestVote.Columns.DateTime]; }
			set { this[MixmagGreatestVote.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Did we post to their facebook wall?
		/// </summary>
		public override bool? DidWallPost
		{
			get { return (bool?)this[MixmagGreatestVote.Columns.DidWallPost]; }
			set { this[MixmagGreatestVote.Columns.DidWallPost] = value; }
		}
		/// <summary>
		/// Their email address from Facebook
		/// </summary>
		public override string FacebookEmail
		{
			get { return (string)this[MixmagGreatestVote.Columns.FacebookEmail]; }
			set { this[MixmagGreatestVote.Columns.FacebookEmail] = value; }
		}
		/// <summary>
		/// Do we have wall post permission?
		/// </summary>
		public override bool? WallPostPermission
		{
			get { return (bool?)this[MixmagGreatestVote.Columns.WallPostPermission]; }
			set { this[MixmagGreatestVote.Columns.WallPostPermission] = value; }
		}
		/// <summary>
		/// Do we have email send permission?
		/// </summary>
		public override bool? EmailPermission
		{
			get { return (bool?)this[MixmagGreatestVote.Columns.EmailPermission]; }
			set { this[MixmagGreatestVote.Columns.EmailPermission] = value; }
		}
		/// <summary>
		/// Did this vote come from a facebook wall post?
		/// </summary>
		public override bool? FacebookSource
		{
			get { return (bool?)this[MixmagGreatestVote.Columns.FacebookSource]; }
			set { this[MixmagGreatestVote.Columns.FacebookSource] = value; }
		}
		#endregion

	}
	#endregion
}
