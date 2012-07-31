using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Common.Clocks;
using Common;
using Caching;

namespace Bobs.BannerServer.Rules.TypesOfRule
{
	public class PlacesVisitedByIdentityRule
	{
		private List<int> placeKs;

		internal PlacesVisitedByIdentityRule()
		{
			placeKs = new List<int>();
		}
		internal PlacesVisitedByIdentityRule(Identity id)
		{
			placeKs = id.PlacesVisited;

		}
		public void Add(int k)
		{
			if (!this.placeKs.Contains(k))
			{
				this.placeKs.Add(k);

			}
		}
		internal void Add(PlacesVisitedByIdentityRule m)
		{
			foreach (int k in m.placeKs)
			{
				this.Add(k);
			}
		}

		public Bobs.Q Query
		{
			get
			{
				placeKs.Sort();
				return (new Bobs.Or(
							new Q(BannerPlace.Columns.PlaceK, placeKs.ToArray()),
							new Q(Bobs.Banner.Columns.IsPlaceTargetted, Bobs.QueryOperator.IsNull, null),
							new Q(Bobs.Banner.Columns.IsPlaceTargetted, false)));
			}
		}
	}
}

