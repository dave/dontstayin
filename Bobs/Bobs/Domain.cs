using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Bobs
{
	#region Domain
	/// <summary>
	/// Domains that redirect  ot DSI
	/// </summary>
	[Serializable] 
	public partial class Domain
	{

		#region Simple members
		/// <summary>
		/// Unique Id
		/// </summary>
		public override int K
		{
			get { return this[Domain.Columns.K] as int? ?? 0; }
			set { this[Domain.Columns.K] = value; }
		}
		/// <summary>
		/// Domain name
		/// </summary>
		public override string DomainName
		{
			get { return (string)this[Domain.Columns.DomainName]; }
			set { this[Domain.Columns.DomainName] = value; }
		}
		/// <summary>
		/// Link to the promoter table
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[Domain.Columns.PromoterK]; }
			set { this[Domain.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// Redirect to object page - type
		/// </summary>
		public override Model.Entities.ObjectType RedirectObjectType
		{
			get { return (Model.Entities.ObjectType)this[Domain.Columns.RedirectObjectType]; }
			set { this[Domain.Columns.RedirectObjectType] = value; }
		}
		/// <summary>
		/// Redirect to object - K
		/// </summary>
		public override int RedirectObjectK
		{
			get { return (int)this[Domain.Columns.RedirectObjectK]; }
			set { this[Domain.Columns.RedirectObjectK] = value; }
		}
		/// <summary>
		/// Redirect to custom URL
		/// </summary>
		public override string RedirectUrl
		{
			get { return (string)this[Domain.Columns.RedirectUrl]; }
			set { this[Domain.Columns.RedirectUrl] = value; }
		}
		/// <summary>
		/// Site Application to invoke, in combination with RedirectObjectK and RedirectObjectType where relevant
		/// </summary>
		public override string RedirectApp
		{
			get { return (string)this[Domain.Columns.RedirectApp]; }
			set { this[Domain.Columns.RedirectApp] = value; }
		}
		/// <summary>
		/// The resource ID of this domain when registered with Wild West Domains, useful for automated domain renewal
		/// </summary>
		public override string WwdResourceID
		{
			get { return (string)this[Domain.Columns.WwdResourceID]; }
			set { this[Domain.Columns.WwdResourceID] = value; }
		}
		/// <summary>
		/// Has this domain been registered in the Primary zone? (Extra)
		/// </summary>
		public override bool RegisteredPrimary
		{
			get { return (bool)this[Domain.Columns.RegisteredPrimary]; }
			set { this[Domain.Columns.RegisteredPrimary] = value; }
		}
		/// <summary>
		/// Has this domain been registered in the Secondary zone? (Mace)
		/// </summary>
		public override bool RegisteredSecondary
		{
			get { return (bool)this[Domain.Columns.RegisteredSecondary]; }
			set { this[Domain.Columns.RegisteredSecondary] = value; }
		}
		#endregion

		public void IncrementStats()
		{
			DateTime today = DateTime.Today;
			Query q = new Query();
			q.QueryCondition=new And(
				new Q(DomainStats.Columns.DomainK, this.K),
				new Q(DomainStats.Columns.Date, today)
			);
			q.ReturnCountOnly = true;
			DomainStatsSet dts = new DomainStatsSet(q);
			if (dts.Count > 0)
			{
				Update u = new Update();
				u.Changes.Add(new Assign.Increment(DomainStats.Columns.Hits));
				u.Table = TablesEnum.DomainStats;
				u.Where = new And(new Q(DomainStats.Columns.DomainK, this.K), new Q(DomainStats.Columns.Date, today));
				u.Run();
				//Cambro.Misc.Db.Qu("UPDATE DomainStats SET Hits=Hits+1 WHERE K=" + this.K.ToString());
			}
			else
			{
				try
				{
					DomainStats ds = new DomainStats();
					ds.Date = DateTime.Now.Date;
					ds.DomainK = this.K;
					ds.Hits = 1;
					ds.Update();
				}
				catch { }
			}
		}

		public string RedirectUrlComplete
		{
			get
			{
				try
				{
					if (this.RedirectObjectK > 0)
					{
						IObjectPage p = (IObjectPage)Bob.Get(this.RedirectObjectType, this.RedirectObjectK);
						return "http://www.dontstayin.com" + p.UrlApp(this.RedirectApp);
					}
					else if (this.RedirectUrl.StartsWith("http://"))
					{
						return this.RedirectUrl;
					}
					else
					{
						return "http://www.dontstayin.com" + this.RedirectUrl;
					}
				}
				catch 
				{ 
					return "";
				}
			}
		}

		public void Redirect()
		{
			Redirect(HttpContext.Current.Request.Url.PathAndQuery);
		}
		public void Redirect(string pathAndQuery)
		{
			HttpContext.Current.Response.Redirect("http://www.dontstayin.com/popup/redirect?domainK=" + this.K +
				"&redirectUrl=" + this.RedirectUrlComplete + pathAndQuery, true);
		}

		public static int CurrentK
		{
			get { return (Visit.HasCurrent) ? Visit.Current.DomainK : 0; }
		}

		public bool IsAvailable
		{
			get
			{
				DomainNameRegistrar.DotComDomain domain = new DomainNameRegistrar.DotComDomain(
					DomainNameRegistrar.Helpers.GetSecondLevelDomain(this.DomainName));
				return domain.IsAvailable();
			}
		}

		public void Register()
		{
			if (this.WwdResourceID.Length > 0)
			{
				throw new Exception("This domain is already registered!");
			}
			if (DomainNameRegistrar.Helpers.GetTopLevelDomain(this.DomainName).ToLower() != "com")
			{
				throw new NotImplementedException("Can only register .com domains");
			}

			DomainNameRegistrar.DotComDomain domain = new DomainNameRegistrar.DotComDomain(
				DomainNameRegistrar.Helpers.GetSecondLevelDomain(this.DomainName));
			domain.Register();
			this.WwdResourceID = domain.WwdResourceID;
			this.Update();
		}

	}
	#endregion

	#region DomainStats

	#endregion
}
