using System;
using System.Data;
using System.Data.SqlClient;

namespace Bobs
{
	/// <summary>
	/// Links a user to many events (my favorite events)
	/// </summary>
	[Serializable] 
	public partial class GroupPhoto
	{
		readonly static string fetchSql = "SELECT TOP 1 * FROM [" + Tables.GetTableName(TablesEnum.GroupPhoto) + "] WITH (NOLOCK) WHERE [" + Tables.GetColumnName(GroupPhoto.Columns.GroupK) + "] = @GroupK AND [" + Tables.GetColumnName(GroupPhoto.Columns.PhotoK) + "] = @PhotoK";
		public GroupPhoto(int groupK, int photoK)
			: this()
		{

			using (SqlConnection conn = new SqlConnection(Common.Properties.ConnectionString)){
				using (SqlCommand cmd = new SqlCommand(fetchSql, conn))
				{
						
					cmd.Parameters.AddWithValue("@GroupK", groupK);
					cmd.Parameters.AddWithValue("@PhotoK", photoK);
					using (var adapter = new SqlDataAdapter(cmd))
					{
						conn.Open();
						DataTable dt = new DataTable();
						adapter.Fill(dt);
						if (dt.Rows.Count == 0)
						{
							throw new BobNotFound();
						}
						else
						{
							this.Initialise(dt.Rows[0]);
						}
					}
				}
			}
		}
		public GroupPhoto(IBob Parent, object Column)
			: this()
		{
			this.Bob.GetBobFromParentSimple(Parent, Column, TablesEnum.GroupPhoto);
		}

		#region simple members
		public override int K
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
		/// <summary>
		/// Link to Group table
		/// </summary>
		public override int GroupK
		{
			get { return (int)this[GroupPhoto.Columns.GroupK]; }
			set { this.group = null; this[GroupPhoto.Columns.GroupK] = value; }
		}
		/// <summary>
		/// Link to the Photo table
		/// </summary>
		public override int PhotoK
		{
			get { return (int)this[GroupPhoto.Columns.PhotoK]; }
			set { this.photo = null; this[GroupPhoto.Columns.PhotoK] = value; }
		}
		/// <summary>
		/// Caption for the group homepage
		/// </summary>
		public override string Caption
		{
			get { return (string)this[GroupPhoto.Columns.Caption]; }
			set { this[GroupPhoto.Columns.Caption] = value; }
		}
		/// <summary>
		/// When was the photo added
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[GroupPhoto.Columns.DateTime]; }
			set { this[GroupPhoto.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Who added/modified the photo
		/// </summary>
		public override int AddedByUsrK
		{
			get { return (int)this[GroupPhoto.Columns.AddedByUsrK]; }
			set { this.addedByUsr = null; this[GroupPhoto.Columns.AddedByUsrK] = value; }
		}
		/// <summary>
		/// Do we show this on the group front page?
		/// </summary>
		public override bool ShowOnFrontPage
		{
			get { return (bool)this[GroupPhoto.Columns.ShowOnFrontPage]; }
			set { this[GroupPhoto.Columns.ShowOnFrontPage] = value; }
		}
		#endregion

		#region Links to Bobs
		#region Group
		public Group Group
		{
			get
			{
				if (this.group==null)
					this.group = new Group(this.GroupK, this, GroupPhoto.Columns.GroupK);
				return this.group;
			}
		}
		Group group;
		#endregion
		#region Photo
		public Photo Photo
		{
			get
			{
				if (this.photo==null)
					this.photo = new Photo(this.PhotoK, this, GroupPhoto.Columns.PhotoK);
				return this.photo;
			}
		}
		Photo photo;
		#endregion
		#region AddedByUsr
		public Usr AddedByUsr
		{
			get
			{
				if (this.addedByUsr==null)
					this.addedByUsr = new Usr(this.AddedByUsrK, this, GroupPhoto.Columns.AddedByUsrK);
				return this.addedByUsr;
			}
		}
		Usr addedByUsr;
		#endregion
		#endregion
		
	}
}
