using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;


namespace ChatLibrary
{
	[Serializable]
	public class ChatRoom : System.MarshalByRefObject
	{

		public ChatRoom(ChatServer.StatusDelegate status, Guid roomGuid, ChatServer server)
		{
			Server = server;
			Status = status;
			RoomGuid = roomGuid;
			
			lastItemPosted = DateTime.Now;
			lastUserExited = DateTime.Now;

			if (System.Environment.MachineName == "SOLO")
				timeoutMins = 1;
			else
				timeoutMins = 15;

			cacheCapacity = 30;

			users = new Dictionary<int, User>();
			itemCache = new List<ChatItemHolder>();
			responseBuilder = new StringBuilder();

		}

		#region Status
		/// <summary>
		/// Status delegate for logging
		/// </summary>
		public ChatServer.StatusDelegate Status { get; set; }
		#endregion

		#region users
		/// <summary>
		/// Users in this room
		/// </summary>
		Dictionary<int, User> users { get; set; }
		#endregion

		#region itemCache
		/// <summary>
		/// Sorted list of items, sorted by ticks? Is this needed? Why not index by guid... we shouldn't need to re-order, and all items should arrive in the correct order...
		/// </summary>
		List<ChatItemHolder> itemCache { get; set; }
		#endregion

		#region responseBuilder
		/// <summary>
		/// Stringbuilder used for building responses
		/// </summary>
		StringBuilder responseBuilder;
		#endregion

		#region cacheCapacity
		/// <summary>
		/// The capacity of the cache - each time we add a new item, we trim it down. Perhaps we could only trim sometimes, like we do with the user cache?
		/// </summary>
		int cacheCapacity { get; set; }
		#endregion
		#region timeoutMins
		/// <summary>
		/// This time in minutes that this room stays alive after the last post was made
		/// </summary>
		int timeoutMins { get; set; }
		#endregion

		#region IsTrash
		/// <summary>
		/// Is this room empty and expired?
		/// </summary>
		public bool IsTrash
		{
			get
			{
				return users.Count == 0 && DateTime.Now - lastItemPosted > TimeSpan.FromMinutes(timeoutMins);
				//DateTime.Now - lastUserExited > TimeSpan.FromMinutes(timeoutMins) && 
			}
		}
		#endregion
		#region lastItemPosted
		/// <summary>
		/// The ticks of the last post - used by the trash collector to tell when the room has expired
		/// </summary>
		DateTime lastItemPosted;
		#endregion
		#region lastUserExited
		/// <summary>
		/// The ticks of the last post - used by the trash collector to tell when the room has expired
		/// </summary>
		DateTime lastUserExited;
		#endregion

		#region EnsureRegistered
		/// <summary>
		/// Ensures that a user is registered in this room, and adds the latest posts from this room to their cache.
		/// </summary>
		public void EnsureRegistered(User user)
		{
			if (!users.ContainsKey(user.UsrK))
			{
				lock (users)
				{
					if (users.ContainsKey(user.UsrK))
						return;

					users.Add(user.UsrK, user);

					user.AddRoom(this);

					UpdateUserWithLatestPosts(user);
					
				}
			}
		}
		#endregion
		#region UnRegister
		/// <summary>
		/// Unregisters a user from this room, and disables all items from this room in their cache.
		/// </summary>
		public void UnRegister(User user, bool disableItemsAndRemoveRoomFromUser)
		{
			if (users.ContainsKey(user.UsrK))
			{
				lock (users)
				{
					if (!users.ContainsKey(user.UsrK))
						return;

					users.Remove(user.UsrK);

					if (disableItemsAndRemoveRoomFromUser)
					{
						user.RemoveRoom(this);
						user.DisableAllItems(this.RoomGuid);
					}

					lastUserExited = DateTime.Now;
				}
			}
		}
		#endregion

		#region ClearAllItems()
		public void ClearAllItems()
		{
			lock (itemCache)
			{
				while (itemCache.Count > 0)
					itemCache.RemoveAt(0);
			}

			lock (users)
			{
				foreach (User u in users.Values)
					u.DisableAllItems(this.RoomGuid);
			}
		}
		#endregion

		#region Server
		/// <summary>
		/// Link to the parent server
		/// </summary>
		public ChatServer Server { get; set; }
		#endregion

		#region RoomGuid
		/// <summary>
		/// The unique guid for this room
		/// </summary>
		public Guid RoomGuid { get; set; }
		#endregion

		#region UpdateUserWithLatestPosts
		/// <summary>
		/// Adds [GlobalMaxItems] items from this room to a user
		/// </summary>
		public void UpdateUserWithLatestPosts(User user)
		{
			if (!user.Online)
				return;

			lock (itemCache)
			{
				if (itemCache.Count > 0)
				{
					int i = 0;
					if (itemCache.Count > ChatServer.GlobalMaxItems)
						i = itemCache.Count - ChatServer.GlobalMaxItems;


					for (; i < itemCache.Count; i++)
					{
						user.Update(itemCache[i].Item, itemCache[i].ItemGuid, itemCache[i].RoomGuid, itemCache[i].DateTime);
					}
				}
			}
		}
		#endregion

		#region GetItems
		/// <summary>
		/// Gets the latest items in this room, up to [latestItemGuid].
		/// </summary>
		public string GetItems(bool isFirstRequest, Guid latestItemGuid, ref Guid newestItemGuid, int maxItemsToReturn)
		{
			string output = string.Empty;

			lock (responseBuilder)
			{
				responseBuilder.Length = 0;

				AppendItems(isFirstRequest, latestItemGuid, ref newestItemGuid, maxItemsToReturn, responseBuilder);

				output = responseBuilder.ToString();

				responseBuilder.Length = 0;
			}

			return output;
		}
		#endregion

		#region AppendItems
		/// <summary>
		/// Appends the latest items in this room to a stringbuilder, up to [latestItemGuid].
		/// </summary>
		public void AppendItems(bool isFirstRequest, Guid latestItemGuid, ref Guid newestItemGuid, int maxItemsToReturn, StringBuilder sb)
		{
			Status("Room.AppendItems", false);

			lock (itemCache)
			{
				if (itemCache.Count > 0)
				{
					//set the latest message id to pass back by ref
					newestItemGuid = itemCache[itemCache.Count - 1].UniqueItemGuid;

					int i = 0;

					if (isFirstRequest || latestItemGuid == Guid.Empty)
					{
						if (itemCache.Count > maxItemsToReturn)
							i = itemCache.Count - maxItemsToReturn;
					}
					else
					{
						i = itemCache.Count - 1;

						//find the value of i to start building from
						while (i >= 0 && itemCache[i].UniqueItemGuid != latestItemGuid && i > itemCache.Count - maxItemsToReturn)
						{
							i--;
						}

						//increment i because we already have the message at position i
						i++;
					}

					//build the response from the remaining items
					for (; i < itemCache.Count; i++)
					{
						if (sb.Length > 0)
							sb.Append(",");

						sb.Append(itemCache[i].Item);
					}
				}
			}
		}
		#endregion

		#region AddItem
		/// <summary>
		/// Add an item to the room
		/// </summary>
		/// <param name="xml"></param>
		public void AddItem(string item, Guid itemGuid)
		{
			Status("ChatRoom.AddItem", false);

			//record the time of this post
			lastItemPosted = DateTime.Now;

			long ticks = DateTime.Now.Ticks;

			ChatItemHolder thisItem = new ChatItemHolder(item, itemGuid, itemGuid, this.RoomGuid, DateTime.Now);

			//if we already have a post recorded at this time (unlikely), add one to make it unique - I think we can change this so it indexes by guid, rather than this ticks bullshit...
			lock (itemCache)
			{
				//add the item to the cache
				itemCache.Add(thisItem);
				
				//cleanOutIfNeeded();
				while (itemCache.Count > cacheCapacity)
					itemCache.RemoveAt(0);

			}

			//update users about the change
			lock (users)
			{
				foreach (User registeredUser in users.Values)
				{
					if (registeredUser.Online)
						registeredUser.Update(thisItem);
				}
			}
		}
		#endregion

		#region cleanOutIfNeeded (removed)
		/// <summary>
		/// Truncates the cache to [cacheCapacity] items. Only fires if more than 50 items have been added since the last clean out.
		/// </summary>
		//void cleanOutIfNeeded()
		//{
		//    //This has been removed because I think it's the reason that the memory leaked...

		//    //itemCache should already be locked...
		//    itemsAddedSinceLastCleanOut++;

		//    if (itemsAddedSinceLastCleanOut > 50)
		//    {
		//        Status("Cleaning out now...", false);

		//        //while we have more than cacheCapacity items, get rid of the first one(s)
		//        while (itemCache.Count > cacheCapacity)
		//            itemCache.RemoveAt(0);

		//        itemsAddedSinceLastCleanOut = 0;
		//    }
		//}
		#endregion
		#region itemsAddedSinceLastCleanOut
		/// <summary>
		/// Number of items added to the cache since the last clean out. Clean-out trims the cache to [cacheCapacity] items.
		/// </summary>
		int itemsAddedSinceLastCleanOut;
		#endregion

		#region ToString()
		/// <summary>
		/// String reprasentation of the room
		/// </summary>
		public override string ToString()
		{
			StringBuilder roomDetails = new StringBuilder();

			roomDetails.AppendFormat("\nRoomGuid : {0}", RoomGuid);
			roomDetails.AppendFormat("\nItems    : {0}", itemCache.Count.ToString());

			return roomDetails.ToString();
		}
		#endregion

	}
}
