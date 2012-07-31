using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Common.Clocks;
using Common;

namespace Bobs.BannerServer.Rules.TypesOfRule
{
	public class MusicTypesFavouredByIdentityRule
	{
		private List<int> musicTypeKs;

		internal MusicTypesFavouredByIdentityRule()
		{
			musicTypeKs = new List<int>();
		}
		internal MusicTypesFavouredByIdentityRule(Usr u) : this(new UsrIdentity(u)) { }
		internal MusicTypesFavouredByIdentityRule(Identity identity)
		{
			musicTypeKs = new List<int>();
			List<int> favouriteMusicTypes = identity.FavouriteMusicTypes;

			foreach (int k in favouriteMusicTypes)
			{
				Add(k);
			}

		}

		public void Add(int k)
		{
			if (!this.musicTypeKs.Contains(k))
			{
				this.musicTypeKs.Add(k);
				MusicType.GetChildMusicTypeKs(k).ForEach(mtk => Add(mtk));
			}
		}
		internal void Add(MusicTypesFavouredByIdentityRule m)
		{
			foreach (int musicTypeK in m.musicTypeKs)
			{
				this.Add(musicTypeK);
			}
		}

		public Bobs.Q Query
		{
			get
			{
				musicTypeKs.Sort();
				return new Bobs.Or(
					new Q(BannerMusicType.Columns.MusicTypeK, musicTypeKs.ToArray()),		//musictypek in typekeys
					new Bobs.Or(new Q(Bobs.Banner.Columns.IsMusicTargetted, Bobs.QueryOperator.IsNull, null), //musictype k is null
					new Q(Bobs.Banner.Columns.IsMusicTargetted, false))); //banner <> musictargetted
			}
		}
	}
}

