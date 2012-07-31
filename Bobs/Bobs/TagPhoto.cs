using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	#region TagPhoto
	/// <summary>
	/// a table that links tags to photos
	/// </summary>
	[Serializable]
	public partial class TagPhoto
	{
		#region Simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[TagPhoto.Columns.K] as int? ?? 0; }
			set { this[TagPhoto.Columns.K] = value; }
		}
		/// <summary>
		/// the k of the tag
		/// </summary>
		public override int TagK
		{
			get { return (int)this[TagPhoto.Columns.TagK]; }
			set { this.tag = null; this[TagPhoto.Columns.TagK] = value; }
		}
		/// <summary>
		/// the k of the photo that is tagged
		/// </summary>
		public override int PhotoK
		{
			get { return (int)this[TagPhoto.Columns.PhotoK]; }
			set { this.photo = null; this[TagPhoto.Columns.PhotoK] = value; }
		}
		
		/// <summary>
		/// Is disabled
		/// </summary>
		public override bool Disabled
		{
			get { return (bool)this[TagPhoto.Columns.Disabled]; }
			set { this[TagPhoto.Columns.Disabled] = value; }
		}
		
		#endregion


		internal static TagPhoto Get(int tagK, int photoK)
		{
			Query q = new Query(new And(new Q(TagPhoto.Columns.TagK, tagK), new Q(TagPhoto.Columns.PhotoK, photoK)));
			TagPhotoSet set = new TagPhotoSet(q);
			switch (set.Count)
			{
				case 0: return null;
				case 1: return set[0];
				default: throw new Exception("Should not happen as should be unique");
			}
		}

		#region Tag
		private Tag tag;
		public Tag Tag
		{
			get
			{
				if (tag == null)
				{
					tag = new Tag(this.TagK);
				}
				return tag;
			}
		}
		#endregion

	 

 

		#region Photo
		private Photo photo;
		public Photo Photo
		{
			get
			{
				if (photo == null)
				{
					photo = new Photo(this.PhotoK);
				}
				return photo;
			}
		}
		#endregion

		public TagPhotoHistory MostRecentTagPhotoHistory
		{
			get
			{
				var children = ChildTagPhotoHistorys(new KeyValuePair<object, OrderBy.OrderDirection>(Bobs.TagPhotoHistory.Columns.DateTime, OrderBy.OrderDirection.Descending));
				if (children.Count > 0)
				{
					return children[0];
				}
				else
				{
					return null;
				}
			}
		}
 

		public static TagPhoto GetTagPhoto(int tagK, int photoK)
		{
			Query q = new Query(new And(new Q(TagPhoto.Columns.PhotoK, photoK), new Q(TagPhoto.Columns.TagK, tagK)));
			TagPhotoSet set = new TagPhotoSet(q);
			if (set.Count == 0)
			{
				return null;
			}
			else
			{
				return set[0];
			}

		}

		public void SetDisabledAndUpdate(bool disabled)
		{
			Transaction transaction = new Transaction();
			try
			{
				this.Disabled = disabled;
				this.Update(transaction);
				TagPhotoHistory historyItem = new TagPhotoHistory()
				{
					Action = disabled ? TagPhotoHistory.TagPhotoHistoryAction.Disabled : TagPhotoHistory.TagPhotoHistoryAction.Enabled,
					DateTime = DateTime.Now,
					TagPhotoK = this.K,
					UsrK = Usr.Current.K
				};
				historyItem.Update(transaction);
				transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.Rollback();
			}
		}

		public static void Disable(int tagK, int photoK)
		{
			TagPhoto tp = TagPhoto.Get(tagK, photoK);
			tp.SetDisabledAndUpdate(true);
		}
	}
	#endregion

}
