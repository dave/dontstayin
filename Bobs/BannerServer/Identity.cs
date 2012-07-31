using System;
using Bobs.BannerServer.Rules;
using Bobs;
using Caching;
using System.Web;
using System.Collections.Generic;

namespace Bobs.BannerServer
{
	public abstract class Identity
	{
		internal Usr Usr { get; set; }
		internal Guid Guid { get; set; }

		public static Identity Current
		{
			get
			{
				if (HttpContext.Current == null)
				{
					return new BrowserGuidIdentity(new Guid("6d227e76-82e0-11dc-8314-0800200c9a66"));
				}
				else if (Usr.Current != null)
				{
					return new UsrIdentity(Usr.Current);
				}
				else
				{
					try
					{
						return new BrowserGuidIdentity(new Guid(HttpContext.Current.Request.Cookies["DsiGuid"].Value));
					}
					catch
					{
						return new BrowserGuidIdentity(new Guid("6d227e76-82e0-11dc-8314-0800200c9a66"));
					}
				}
			}
		}

		Demographics demographics;
		internal Demographics Demographics
		{
			get
			{
				if (demographics == null)
				{
					try
					{
						demographics = new Demographics(this.Guid);
					}
					catch (BobNotFound) { }
				}
				return demographics;
			}
		}

		internal IdentityRules IdentityRules
		{
			get { return new IdentityRules(this); }
		}
 
		abstract public List<int> FavouriteMusicTypes { get; }
		abstract public void AddFavouriteMusicType(int musicTypeK);
		abstract public List<int> PlacesVisited { get; }
		abstract public void AddPlaceVisited(int placeK);


	}
}

