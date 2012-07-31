using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	#region BannerFolder
	/// <summary>
	/// A BannerFolder object used for grouping banners
	/// </summary>
	[Serializable]
	public partial class BannerFolder
	{

		#region Simple members
		/// <summary>
		/// The auto incrementing primary key
		/// </summary>
		public override int K
		{
			get { return this[BannerFolder.Columns.K] as int? ?? 0; }
			set { this[BannerFolder.Columns.K] = value; }
		}
		/// <summary>
		/// The name of the BannerFolder
		/// </summary>
		public override string Name
		{
			get { return (string)this[BannerFolder.Columns.Name]; }
			set { this[BannerFolder.Columns.Name] = value; }
		}
		/// <summary>
		/// The primary key of the promoter which owns the BannerFolder
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[BannerFolder.Columns.PromoterK]; }
			set { this[BannerFolder.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// When the BannerFolder object was first created
		/// </summary>
		public override DateTime DateTimeCreated
		{
			get { return (DateTime)this[BannerFolder.Columns.DateTimeCreated]; }
			set { this[BannerFolder.Columns.DateTimeCreated] = value; }
		}

		/// <summary>
		/// The EventK the folder corresponds to, if any
		/// </summary>
		public override Int32 EventK
		{
			get { return (Int32)this[BannerFolder.Columns.EventK]; }
			set { this[BannerFolder.Columns.EventK] = value; }
		}
		/// <summary>
		/// Duplicate guid used to prevent duplicates while adding
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[BannerFolder.Columns.DuplicateGuid]); }
			set { this[BannerFolder.Columns.DuplicateGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}

		#endregion

		BannerSet banners = null;
		public BannerSet Banners
		{
			get{
				if (banners == null)
				{
					Query q = new Query(new Q(Banner.Columns.BannerFolderK, this.K));
					q.OrderBy = new OrderBy(Banner.Columns.K, OrderBy.OrderDirection.Descending);
					banners = new BannerSet(q);
				}
				return banners;
			}
		}

		public long TotalClicks
		{
			get
			{
				long sum = 0;
				foreach (Banner b in Banners)
				{
					sum += b.TotalClicks;
				}
				return sum;
			}

		}
		public long TotalHits
		{
			get
			{
				long sum = 0;
				foreach (Banner b in Banners)
				{
					sum += b.TotalHits;
				}
				return sum;
			}
		}
		public double ClickRate
		{
			get
			{
				if (TotalClicks == 0) return 0.0d;
				return (double)TotalClicks / (double)TotalHits;
			}
		}

	}
	#endregion
}
