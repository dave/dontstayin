using System;
namespace Bobs
{

	#region TagPhotoHistory
	/// <summary>
	/// History of actions on a tag photo
	/// </summary>
	[Serializable]
	public partial class TagPhotoHistory 
	{

		#region Simple members
		/// <summary>
		/// Primary key
		/// </summary>
		public override int K
		{
			get { return this[TagPhotoHistory.Columns.K] as int? ?? 0; }
			set { this[TagPhotoHistory.Columns.K] = value; }
		}
		/// <summary>
		/// The tagPhoto that was edited
		/// </summary>
		public override int TagPhotoK
		{
			get { return (int)this[TagPhotoHistory.Columns.TagPhotoK]; }
			set { this.tagPhoto = null; this[TagPhotoHistory.Columns.TagPhotoK] = value; }
		}
		/// <summary>
		/// What the person did
		/// </summary>
		public override TagPhotoHistoryAction Action
		{
			get { return (TagPhotoHistoryAction)this[TagPhotoHistory.Columns.Action]; }
			set { this[TagPhotoHistory.Columns.Action] = (int) value; }
		}
		/// <summary>
		/// The usr that did it
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[TagPhotoHistory.Columns.UsrK]; }
			set { usr = null; this[TagPhotoHistory.Columns.UsrK] = value; }
		}
		/// <summary>
		/// When they did it
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[TagPhotoHistory.Columns.DateTime]; }
			set { this[TagPhotoHistory.Columns.DateTime] = value; }
		}
		#endregion


		TagPhoto tagPhoto;
		public TagPhoto TagPhoto
		{
			get
			{
				if (tagPhoto == null) tagPhoto = new TagPhoto(this.TagPhotoK);
				return tagPhoto;
			}
		}
		Usr usr;
		public Usr Usr
		{
			get
			{
				if (usr == null) usr = new Usr(UsrK);
				return usr;
			}
		}
	}
	#endregion
}
