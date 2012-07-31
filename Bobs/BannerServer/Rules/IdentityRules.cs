using System.Collections.Generic;
using Bobs.BannerServer.Rules.TypesOfRule;
using Bobs;
using Caching;

namespace Bobs.BannerServer.Rules
{
	/// <summary>
	/// This class contains the metadata that we know about a particular identity, e.g. a users age,sex,location etc.
	/// </summary>
	internal class IdentityRules : RequestRules
	{
		internal IdentityRules(Identity id)
		{
			this.Add(new IdentityPropertyRules(id));

			try
			{
				this.PlacesVisited.Add(new PlacesVisitedByIdentityRule(id));
				this.MusicTypes.Add(new MusicTypesFavouredByIdentityRule(id));
			}
			catch (Bobs.BobNotFound) { }
		}
	}
}
