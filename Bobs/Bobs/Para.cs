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

	#region Para
	[Serializable] 
	public partial class Para : IPic, IDeleteAll, IBobType
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Para.Columns.K] as int? ?? 0; }
			set { this[Para.Columns.K] = value; }
		}
		/// <summary>
		/// Link to the article
		/// </summary>
		public override int ArticleK
		{
			get { return (int)this[Para.Columns.ArticleK]; }
			set { article = null; this[Para.Columns.ArticleK] = value; }
		}
		/// <summary>
		/// What page is the paragraph on? (Starts at 1)
		/// </summary>
		public override int Page
		{
			get { return (int)this[Para.Columns.Page]; }
			set { this[Para.Columns.Page] = value; }
		}
		/// <summary>
		/// Relative order of the paragraphs on the page 0 is at the top
		/// </summary>
		public override double Order
		{
			get { return (double)this[Para.Columns.Order]; }
			set { this[Para.Columns.Order] = value; }
		}
		/// <summary>
		/// Paragraph type - 1=Title, 2=Paragraph, 3=Photo
		/// </summary>
		public override TypeEnum Type
		{
			get { return (TypeEnum)this[Para.Columns.Type]; }
			set { this[Para.Columns.Type] = value; }
		}
		/// <summary>
		/// If the paragraph is a photo, or has a photo linked to it
		/// </summary>
		public override int PhotoK
		{
			get { return (int)this[Para.Columns.PhotoK]; }
			set { photo = null; this[Para.Columns.PhotoK] = value; }
		}
		/// <summary>
		/// The text of the paragraph
		/// </summary>
		public override string Text
		{
			get { return (string)this[Para.Columns.Text]; }
			set { this[Para.Columns.Text] = value; }
		}
		/// <summary>
		/// The thread attached to this paragraph.
		/// </summary>
		public override int ThreadK
		{
			get { return (int)this[Para.Columns.ThreadK]; }
			set { thread = null; this[Para.Columns.ThreadK] = value; }
		}
		/// <summary>
		/// Photo align type - 1=Left, 2=Right, 3=Center
		/// </summary>
		public override PhotoAlignEnum PhotoAlign
		{
			get { return (PhotoAlignEnum)this[Para.Columns.PhotoAlign]; }
			set { this[Para.Columns.PhotoAlign] = value; }
		}
		/// <summary>
		/// Cropped image 100*100
		/// </summary>
		public override Guid Pic
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Para.Columns.Pic]); }
			set { this[Para.Columns.Pic] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// PhotoType - photo type - 0=None, 1=Icon, 2=Thumb, 3=Web, 4=Custom (file referenced by Pic)
		/// </summary>
		public override PhotoTypes PhotoType
		{
			get { return (PhotoTypes)this[Para.Columns.PhotoType]; }
			set { this[Para.Columns.PhotoType] = value; }
		}
		/// <summary>
		/// Caption for the photo - 100 chars or less.
		/// </summary>
		public override string Caption
		{
			get { return (string)this[Para.Columns.Caption]; }
			set { this[Para.Columns.Caption] = value; }
		}
		/// <summary>
		/// Width of the image in pixels if PhotoType=Custom.
		/// </summary>
		public override int PicWidth
		{
			get
			{
				if (HasPicPrivate)
				{
					if (PhotoType.Equals(PhotoTypes.Icon))
						return 100;
					else if (PhotoType.Equals(PhotoTypes.Thumb))
						return Photo.ThumbWidth;
					else if (PhotoType.Equals(PhotoTypes.Web))
						return Photo.WebWidth;
					else
						return (int)this[Para.Columns.PicWidth];
				}
				else
					return (int)this[Para.Columns.PicWidth];
			}
			set { this[Para.Columns.PicWidth] = value; }
		}
		/// <summary>
		/// Height of the image in pixels if PhotoType=Custom.
		/// </summary>
		public override int PicHeight
		{
			get
			{
				if (HasPicPrivate)
				{
					if (PhotoType.Equals(PhotoTypes.Icon))
						return 100;
					else if (PhotoType.Equals(PhotoTypes.Thumb))
						return Photo.ThumbHeight;
					else if (PhotoType.Equals(PhotoTypes.Web))
						return Photo.WebHeight;
					else
						return (int)this[Para.Columns.PicHeight];
				}
				else
					return (int)this[Para.Columns.PicHeight];
			}
			set { this[Para.Columns.PicHeight] = value; }
		}
		/// <summary>
		/// State var used to reconstruct cropper when re-cropping
		/// </summary>
		public override string PicState
		{
			get { return (string)this[Para.Columns.PicState]; }
			set { this[Para.Columns.PicState] = value; }
		}
		/// <summary>
		/// The Photo that was used to create the Pic.
		/// </summary>
		public override int PicPhotoK
		{
			get { return (int)this[Para.Columns.PicPhotoK]; }
			set { picPhoto = null; this[Para.Columns.PicPhotoK] = value; }
		}
		/// <summary>
		/// The Misc that was used to create the Pic.
		/// </summary>
		public override int PicMiscK
		{
			get { return (int)this[Para.Columns.PicMiscK]; }
			set { picMisc = null; this[Para.Columns.PicMiscK] = value; }
		}
		#endregion


		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;

			if (this.ThreadK>0)
				this.Thread.DeleteAll(transaction);

			Guid oldPic = this.HasPic ? this.Pic : Guid.Empty;
			int oldPicMiscK = this.PicMisc != null ? this.PicMiscK : 0;

			this.Delete(transaction);

			if (oldPic != Guid.Empty)
				Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");

			if (oldPicMiscK > 0)
			{
				Misc m = new Misc(oldPicMiscK);
				m.DeleteAll(transaction);
			}

		}

		#region Links to Bobs
		#region Article
		public Article Article
		{
			get
			{
				if (article==null)
					article = new Article(ArticleK);
				return article;
			}
			set
			{
				article = value;
			}
		}
		private Article article;
		#endregion
		#region Photo
		public Photo Photo
		{
			get
			{
				if (photo==null && PhotoK>0)
					photo = new Photo(PhotoK);
				return photo;
			}
			set
			{
				photo = value;
			}
		}
		private Photo photo;
		#endregion
		#region Thread
		public Thread Thread
		{
			get
			{
				if (thread==null && ThreadK>0)
					thread = new Thread(ThreadK);
				return thread;
			}
			set
			{
				thread = value;
			}
		}
		private Thread thread;
		#endregion
		#endregion

		
		#region IPic Members

		public bool HasPic
		{
			get
			{
				return this.PhotoK > 0 && this.Photo != null && this.Photo.Status.Equals(Photo.StatusEnum.Enabled);
			}
		}

		public bool HasPicPrivate
		{
			get
			{
				return this.PhotoK > 0 && this.Photo!=null;
			}
		}

		public string PicPath
		{
			get
			{
				if (HasPicPrivate)
				{
					if (PhotoType.Equals(PhotoTypes.Icon))
						return Photo.IconPath;
					if (PhotoType.Equals(PhotoTypes.Thumb))
						return Photo.ThumbPath;
					if (PhotoType.Equals(PhotoTypes.Web))
						return Photo.WebPath;
					if (PhotoType.Equals(PhotoTypes.Custom))
						return Storage.Path(Pic);
				}
				return "";
			}
		}

		#endregion

		#region PicMisc and PicPhoto
		#region PicMisc
		public Misc PicMisc
		{
			get
			{
				if (picMisc==null && PicMiscK>0)
					picMisc = new Misc(PicMiscK);
				return picMisc;
			}
			set
			{
				picMisc = value;
			}
		}
		private Misc picMisc;
		#endregion
		#region PicPhoto
		public Photo PicPhoto
		{
			get
			{
				if (picPhoto==null && PicPhotoK>0)
					picPhoto = new Photo(PicPhotoK);
				return picPhoto;
			}
			set
			{
				picPhoto = value;
			}
		}
		private Photo picPhoto;
		#endregion
		#endregion


		#region IBobType Members

		public string TypeName
		{
			get { return "Para"; }
		}

		#endregion

		#region IHasObjectType Members

		public Model.Entities.ObjectType ObjectType
		{
			get { return Model.Entities.ObjectType.Para; }
		}

		#endregion
	}
	#endregion

}
