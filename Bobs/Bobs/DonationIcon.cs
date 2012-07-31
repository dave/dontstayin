using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bobs
{
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct Bob class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.

	#region DonationIcon
	/// <summary>
	/// Donation icons
	/// </summary>
	[Serializable]
	public partial class DonationIcon
	{

		#region Simple members
		/// <summary>
		/// K
		/// </summary>
		public override int K
		{
			get { return (int)this[DonationIcon.Columns.K] as int? ?? 0; }
			set { this[DonationIcon.Columns.K] = value; }
		}
		/// <summary>
		/// Name displayed on donation pages etc.
		/// </summary>
		public override string IconName
		{
			get { return (string)this[DonationIcon.Columns.IconName]; }
			set { this[DonationIcon.Columns.IconName] = value; }
		}
		/// <summary>
		/// Text displayed by Icon on Rollovers, etc.
		/// </summary>
		public override string IconText
		{
			get { return (string)this[DonationIcon.Columns.IconText]; }
			set { this[DonationIcon.Columns.IconText] = value; }
		}
		/// <summary>
		/// image path
		/// </summary>
		public override string ImgUrl
		{
			get { return (string)this[DonationIcon.Columns.ImgUrl]; }
			set { this[DonationIcon.Columns.ImgUrl] = value; }
		}
		/// <summary>
		/// Chat thread to discuss how much one loves one's donation icon
		/// </summary>
		public override int ThreadK
		{
			get { return (int)this[DonationIcon.Columns.ThreadK]; }
			set { this[DonationIcon.Columns.ThreadK] = value; }
		}
		/// <summary>
		/// Date and time when this icon becomes the Active icon
		/// </summary>
		public override DateTime StartDateTime
		{
			get { return (DateTime)this[DonationIcon.Columns.StartDateTime]; }
			set { this[DonationIcon.Columns.StartDateTime] = value; }
		}
		/// <summary>
		/// Price if bought while icon is active
		/// </summary>
		public override decimal PriceWhenActive
		{
			get { return (decimal)this[DonationIcon.Columns.PriceWhenActive]; }
			set { this[DonationIcon.Columns.PriceWhenActive] = value; }
		}
		/// <summary>
		/// Price if bought once icon is retroactive
		/// </summary>
		public override decimal PriceWhenRetroactive
		{
			get { return (decimal)this[DonationIcon.Columns.PriceWhenRetroactive]; }
			set { this[DonationIcon.Columns.PriceWhenRetroactive] = value; }
		}
		/// <summary>
		/// Donate page - which control layout to use
		/// </summary>
		public override Control DonatePageControl
		{
			get { return (Control)this[DonationIcon.Columns.DonatePageControl]; }
			set { this[DonationIcon.Columns.DonatePageControl] = value; }
		}
		/// <summary>
		/// Donate page Header
		/// </summary>
		public override string DonatePageHeader
		{
			get { return (string)this[DonationIcon.Columns.DonatePageHeader]; }
			set { this[DonationIcon.Columns.DonatePageHeader] = value; }
		}
		/// <summary>
		/// Donate page center text
		/// </summary>
		public override string DonatePageCenterText
		{
			get { return (string)this[DonationIcon.Columns.DonatePageCenterText]; }
			set { this[DonationIcon.Columns.DonatePageCenterText] = value; }
		}
		/// <summary>
		/// Donate page line 1 text
		/// </summary>
		public override string DonatePageLine1Text
		{
			get { return (string)this[DonationIcon.Columns.DonatePageLine1Text]; }
			set { this[DonationIcon.Columns.DonatePageLine1Text] = value; }
		}
		/// <summary>
		/// Donate page line 2 text
		/// </summary>
		public override string DonatePageLine2Text
		{
			get { return (string)this[DonationIcon.Columns.DonatePageLine2Text]; }
			set { this[DonationIcon.Columns.DonatePageLine2Text] = value; }
		}
		/// <summary>
		/// Is this DonationIcon ready to go live from the StartDate?
		/// </summary>
		public override bool Enabled
		{
			get { return (bool)this[DonationIcon.Columns.Enabled]; }
			set { this[DonationIcon.Columns.Enabled] = value; }
		}
		/// <summary>
		/// Storage GUID for icon, if applicable
		/// </summary>
		public override Guid? ImgGuid
		{
			get { return (Guid?)this[DonationIcon.Columns.ImgGuid]; }
			set { this[DonationIcon.Columns.ImgGuid] = value; }
		}
		/// <summary>
		/// Image extension for icon, if applicable
		/// </summary>
		public override string ImgExtension
		{
			get { return (string)this[DonationIcon.Columns.ImgExtension]; }
			set { this[DonationIcon.Columns.ImgExtension] = value; }
		}
		/// <summary>
		/// Is this icon a vatable item?
		/// </summary>
		public override bool? Vatable
		{
			get { return (bool?)this[DonationIcon.Columns.Vatable]; }
			set { this[DonationIcon.Columns.Vatable] = value; }
		}
		/// <summary>
		/// Not used
		/// </summary>
		public override string Description
		{
			get { return (string)this[DonationIcon.Columns.Description]; }
			set { this[DonationIcon.Columns.Description] = value; }
		}
		/// <summary>
		/// Is this icon a charity donation?
		/// </summary>
		public override bool Charity
		{
			get { return (bool)this[DonationIcon.Columns.Charity]; }
			set { this[DonationIcon.Columns.Charity] = value; }
		}
		#endregion

		public string IconPath
		{
			get
			{
				if (!string.IsNullOrEmpty(this.ImgUrl))
					return this.ImgUrl.Replace(".gif", ".png");
				if (this.ImgGuid.HasValue && !string.IsNullOrEmpty(this.ImgExtension))
					return Storage.Path(this.ImgGuid.Value, this.ImgExtension).Replace(".gif", ".png");
				throw new Exception("No valid Icon Path information set");
			}
		}

		public decimal Price
		{
			get { return this.IsActive ? this.PriceWhenActive : this.PriceWhenRetroactive; }
		}

		public static DonationIcon GetActiveDonationIcon()
		{
			Query q = new Query(
				new And(
					new Q(DonationIcon.Columns.StartDateTime, QueryOperator.LessThan, Common.Time.Now),
					new Q(DonationIcon.Columns.Enabled, true)));

			q.OrderBy = new OrderBy(DonationIcon.Columns.StartDateTime, OrderBy.OrderDirection.Descending);
			var dis = new DonationIconSet(q);
			if (dis.Count == 0)
			{
				throw new BobNotFound("No currently active icons!");
			}
			return dis[0];
		}

		private bool? isActive;
		public bool IsActive
		{
			get
			{
				if (!isActive.HasValue)
				{
					isActive = GetActiveDonationIcon().K == this.K;
				}
				return isActive.Value;
			}
		}

		public static DonationIconSet GetIconsForUsr(int usrK)
		{
			var q = new Query
			{
				QueryCondition = new And(
						new Q(DonationIcon.Columns.Enabled, true),
						new Q(UsrDonationIcon.Columns.UsrK, usrK),
						new Q(UsrDonationIcon.Columns.Enabled, true)
						),
				TableElement = new Join(DonationIcon.Columns.K, UsrDonationIcon.Columns.DonationIconK),
				OrderBy = new OrderBy(DonationIcon.Columns.StartDateTime, OrderBy.OrderDirection.Descending),
				Distinct = true,
				DistinctColumn = DonationIcon.Columns.K
			};
			return new DonationIconSet(q);
		}

		public string IconHtml
		{
			get
			{
				return string.Format(
					"<a href='/pages/icons/k-{0}' class='CleanLinks'><img src='{1}' border='0' align='absmiddle' style='margin-right:3px;' width='26' height='21'>{2}</a>",
					this.K, this.IconPath, this.IconText);
			}
		}

		/// <summary>
		/// this is cached for five minutes
		/// </summary>
		/// <returns></returns>
		public static DonationIconSet GetAllDonationIcons()
		{
			var q = new Query
			{
				QueryCondition = new And(
					new Q(DonationIcon.Columns.StartDateTime, QueryOperator.LessThan, DateTime.Now),
					new Q(DonationIcon.Columns.Enabled, true)),
				OrderBy = new OrderBy(DonationIcon.Columns.StartDateTime, OrderBy.OrderDirection.Ascending),
				CacheDuration = new TimeSpan(0, 5, 0)
			};
			return new DonationIconSet(q);
		}
	}
	#endregion
}
