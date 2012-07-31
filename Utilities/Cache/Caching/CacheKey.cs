using System;
using System.Collections.Generic;
using System.Text;
using Common;
using System.Diagnostics;

namespace Caching
{
	public class CacheKey
	{
		
		CacheKeyPrefix prefix;
		string[] keyParts;
		
		public CacheKey(CacheKeyPrefix prefix, params string[] keyParts)
		{
			this.keyParts = keyParts;
			this.prefix = prefix;
		}




		
		public override string ToString()
		{
			string[] allKeyParts = new string[keyParts.Length + 1];
			allKeyParts[0] = "Prefix" + ((int)prefix).ToString();
			keyParts.CopyTo(allKeyParts, 1);
			return String.Join("|", allKeyParts);
		}

		
		static public implicit operator string(CacheKey ck)
		{
			return ck.ToString();
		}
	}
	
	public enum CacheKeyPrefix
	{
		PlacesVisitedForIdentityRuleByGuid = 1,
		FavouredMusicTypeForIdentityRuleByGuid = 2,
		JobQueued = 3,
		JobExecuted = 4,
		JobException = 5,
		BobCacheItem = 6,
		PhotoUploaderTries = 7,
		PhotoUploaderSuccesses = 8,
		PhotoUploaderFailures = 9,
		UpdateThreadUsrJobStatus = 10,
		SpamQueryResults = 11,
		GetTagKFromTagTable = 12,
		TagsForAnITaggable = 13,
		TagVersion = 14,
		SpamBotDefeater = 18,
		TagCloudData = 19,
		ParentChildren = 24,
		TableRow = 26,
		PhotosForTagQuery = 27,
		CachedQuerySet = 28,
		BobChildren = 29,
		BobChildrenField = 30,
		TagCloudVersion = 31,
		ChatClientRoomState = 32,
		GetVerifiedUsersWithOption = 33,
		RecentVideos = 34,
		ChatClientStateDirty = 35,
		LatestTopPhoto = 36,
		ChatClientRoomArchive = 37,
		GetPlaces = 38,
		GetVenues = 39,

	}
}

