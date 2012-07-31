using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Threading;
using System.Security.Permissions;
using System.Runtime.Remoting.Lifetime;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Security.Cryptography;

namespace ChatLibrary 
{

    [Serializable]
    public class ChatServer : MarshalByRefObject, ChatServerInterface
	{
		#region ChatServerIp
		public static string ChatServerIp
		{
			get
			{
				if (System.Environment.MachineName == "jhgjg")
					return "84.45.14.10";
				else
					return "84.45.14.97";
			}
		}
		#endregion

		#region Rooms
		/// <summary>
		/// Contains the list of active rooms
		/// </summary>
		public Dictionary<Guid, ChatRoom> Rooms { get; set; }
		#endregion
		#region Users
		/// <summary>
		/// Contains the list of active users
		/// </summary>
		public Dictionary<int, User> Users { get; set; }
		#endregion

		public ChatServer(ChatServer.StatusDelegate status)
		{
			Status = status;
			Rooms = new Dictionary<Guid, ChatRoom>();
			Users = new Dictionary<int, User>();
			Active = true;

			#region Trash collector thread
			ThreadStart startCollecting = new ThreadStart(CollectTrash);
			Thread TrashCollector = new Thread(startCollecting);
			TrashCollector.Start();
			#endregion
		}

		#region Interface methods

		#region JoinRoom
		/// <summary>
		/// Joins a bunch of users to a bunch of rooms
		/// </summary>
		public void JoinRoom(Guid[] roomGuids, int[] usrKs)
		{
			Status("JoinRoom " + roomGuids.Length.ToString() + " rooms, " + usrKs.Length.ToString() + " users.", false);

			foreach (Guid roomGuid in roomGuids)
			{
				ChatRoom room = getRoom(roomGuid);

				for (int i = 0; i < usrKs.Length; i++)
					room.EnsureRegistered(getUser(usrKs[i]));
			}
		}
		/// <summary>
		/// Joins a single user to a single room
		/// </summary>
		public void JoinRoom(Guid roomGuid, int usrK)
		{
			JoinRoom(new Guid[] { roomGuid }, new int[] { usrK });
		}
		/// <summary>
		/// Joins a bunch of users to a single room
		/// </summary>
		public void JoinRoom(Guid roomGuid, int[] usrKs)
		{
			JoinRoom(new Guid[] { roomGuid }, usrKs);
		}
		/// <summary>
		/// Joins a single user to a bunch of rooms
		/// </summary>
		public void JoinRoom(Guid[] roomGuids, int usrK)
		{
			JoinRoom(roomGuids, new int[] { usrK });
		}
		#endregion

		#region ExitRoom
		/// <summary>
		/// Exits a room
		/// </summary>
		public void ExitRoom(Guid roomGuid, int usrK)
		{
			lock (Rooms)
			{
				if (!Rooms.ContainsKey(roomGuid))
					return;

				ChatRoom room = Rooms[roomGuid];

				room.UnRegister(getUser(usrK), true);
			}
		}
		#endregion

		#region SendTo
		/// <summary>
		/// Adds a bunch of users to a room, then posts an item to that room
		/// </summary>
		public void SendTo(Guid roomGuid, string item, int[] usrKs, Guid itemGuid)
		{
			Status("ChatServer.SendTo(" + roomGuid.ToString() + ",\"" + item + "\"," + usrKs.ToString(), false);
			try
			{
				ChatRoom room = getRoom(roomGuid);

				for (int i = 0; i < usrKs.Length; i++)
					room.EnsureRegistered(getUser(usrKs[i]));

				room.AddItem(item, itemGuid);

			}
			catch (Exception ex)
			{
				Status(ex.ToString(), true);
				Status(this.ToString(), true);
			}
		}
		#endregion

		#region ResetSessionID
		public void ResetSessionID(int usrK, int sessionID)
		{
			User user = getUser(usrK);

			user.MakeOnline();

			user.SessionID = sessionID;
		}
		#endregion

		#region GetLatest
		/// <summary>
		/// If the session matches, we get the latest items from the user cache, up to [lastReceivedItemGuid]. 
		/// If the session doesn't match and it's the first request, we reset the session id and get all the items from the user cache.
		/// </summary>
		public string GetLatest(int usrK, int sessionID, bool isFirstRequest, Guid lastReceivedItemGuid, ref Guid newestItemGuid)
		{
			string output = String.Empty;
			try
			{
				Status("Server.GetLatest", false);

				User user = getUser(usrK);

				user.MakeOnline();

				if (user.SessionID == 0 || user.SessionID == sessionID)
				{
					output = user.GetItems(lastReceivedItemGuid, ref newestItemGuid, null);
				}
				else
				{
					//session mismatch...
					if (isFirstRequest)
					{
						user.SessionID = sessionID;
						output = user.GetItems(ref newestItemGuid);
					}
					else
					{
						output = "wrong session";
					}
				}

				Status(output, false);

			}
			catch (Exception ex)
			{
				Status(ex.ToString(), true);
				Status(this.ToString(), true);
			}
			return output;
		}
		#endregion

		#region GetLatestGuest
		/// <summary>
		/// Gets the latest items from a particular room, up to [lastReceivedItemGuid].
		/// This is used when getting the updates for guest rooms, when we don't want to actually join the room.
		/// </summary>
		public string GetLatestGuest(int usrK, bool isFirstRequest, Guid lastReceivedItemGuid, ref Guid newestItemGuid, Guid roomGuid)
		{
			string output = String.Empty;
			try
			{
				Status("Server.GetLatestGuest", false);

				ChatRoom room = getRoom(roomGuid);

				output = room.GetItems(isFirstRequest, lastReceivedItemGuid, ref newestItemGuid, GlobalMaxItems);

				Status(output, false);

			}
			catch (Exception ex)
			{
				Status(ex.ToString(), true);
				Status(this.ToString(), true);
			}
			return output;
		}
		#endregion

		#region PinRoom
		/// <summary>
		/// This is fired when we pin a room that hasn't been pinned before in this session.
		/// We reset the session id and join the room. Then we get all the items from the pinned room. We append to this cache from the user, up to [lastReceivedItemGuid], excluding any items in the pinned room.
		/// </summary>
		public string PinRoom(int usrK, int sessionID, Guid lastReceivedItemGuid, ref Guid newestItemGuid, Guid pinnedRoomGuid)
		{
			string output = String.Empty;
			try
			{
				Status("Server.PinRoom", false);

				User user = getUser(usrK);

				user.MakeOnline();

				user.SessionID = sessionID;

				ChatRoom pinnedRoom = getRoom(pinnedRoomGuid);
				pinnedRoom.EnsureRegistered(user);

				output = user.GetItems(lastReceivedItemGuid, ref newestItemGuid, pinnedRoom);
				
				Status(output, false);

			}
			catch (Exception ex)
			{
				Status(ex.ToString(), true);
				Status(this.ToString(), true);
			}
			return output;
		}
		#endregion

		#region ClearRoom
		public void ClearRoom(Guid roomGuid)
		{
			try
			{
				Status("Server.ClearRoom", false);

				ChatRoom room = getRoom(roomGuid);

				room.ClearAllItems();

			}
			catch (Exception ex)
			{
				Status(ex.ToString(), true);
				Status(this.ToString(), true);
			}
		}
		#endregion


		#endregion

		#region getRoom
		/// <summary>
		/// Gets or creates a room
		/// </summary>
		ChatRoom getRoom(Guid roomGuid)
		{
			if (Rooms.ContainsKey(roomGuid))
			{
				return Rooms[roomGuid];
			}
			else
			{
				lock (Rooms)
				{
					if (!Rooms.ContainsKey(roomGuid))
					{
						Rooms.Add(roomGuid, new ChatRoom(Status, roomGuid, this));
					}
					return Rooms[roomGuid];
				}
			}
		}
		#endregion
		#region getUser
		/// <summary>
		/// Gets or creates a user
		/// </summary>
		User getUser(int usrK)
		{
			if (Users.ContainsKey(usrK))
			{
				return Users[usrK];
			}
			else
			{
				lock (Users)
				{
					if (!Users.ContainsKey(usrK))
					{
						Users.Add(usrK, new User(Status, usrK, this));
					}
					return Users[usrK];
				}
			}
		}
		#endregion

		#region ToString()
		/// <summary>
        /// Returns a text description of the current state.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("Rooms : " + this.Rooms.Count);
			sb.AppendLine("Users : " + this.Users.Count);

            return sb.ToString();
		}
		#endregion

		#region CollectTrash
		/// <summary>
		/// Deletes old users and rooms
		/// </summary>
		private void CollectTrash()
        {
            while (Active)
            {
                Thread.Sleep(30000);
                Status("Collecting garbage...", true);
				int roomsDeleted = 0;
				int roomsTotal = 0;
				int usersDeleted = 0;
				int usersTotal = 0;

				try
				{

					List<int> usersToDelete = new List<int>();
					List<Guid> roomsToDelete = new List<Guid>();
					//not sure what will happen if the real object is removed before the proxies...
					try
					{

						//remove old users
						int[] keys;
						lock (Users)
						{
							keys = new int[Users.Keys.Count];
							Users.Keys.CopyTo(keys, 0);
						}
						foreach (int k in keys)
						{
							try
							{
								User user = Users[k];
								if (user.IsTrash)
									usersToDelete.Add(user.UsrK);
							}
							catch { }
						}

						usersDeleted = usersToDelete.Count;
						usersTotal = Users.Count;
						lock (Users)
						{
							foreach (int usrK in usersToDelete)
								removeUser(usrK);
						}

						//remove old chatrooms
						Guid[] guids;
						lock (Rooms)
						{
							guids = new Guid[Rooms.Keys.Count];
							Rooms.Keys.CopyTo(guids, 0);
						}
						foreach (Guid g in guids)
						{
							try
							{
								ChatRoom room = Rooms[g];
								if (room.IsTrash)
									roomsToDelete.Add(room.RoomGuid);
							}
							catch { }
						}

						roomsDeleted = roomsToDelete.Count;
						roomsTotal = Rooms.Count;
						lock (Rooms)
						{
							foreach (Guid roomGuid in roomsToDelete)
								removeRoom(roomGuid);
						}



						//Call the .Net GarbageCollector
						System.GC.Collect();
						System.GC.WaitForPendingFinalizers();

					}
					catch (Exception ex)
					{
						Status("ERROR in CollectTrash()\n" + ex.ToString(), true);
						continue;
					}

					Status(string.Format("Finished collecting garbage - {0}/{1} rooms and {2}/{3} users deleted.", roomsDeleted.ToString(), roomsTotal.ToString(), usersDeleted.ToString(), usersTotal.ToString()), true);

				}
				catch
				{
					Status("exception collecting garbage 2", true);
				}
            }
        }
		public bool Active { get; set; }
        #endregion
		#region removeRoom
		void removeRoom(Guid roomGuid)
		{
			try
			{
				lock (Rooms)
				{
					if (Rooms.ContainsKey(roomGuid))
					{
						System.GC.ReRegisterForFinalize(Rooms[roomGuid]);
						Rooms.Remove(roomGuid);
					}
				}
			}
			catch
			{

			}
		}
		#endregion
		#region removeUser
		void removeUser(int userK)
		{
			try
			{
				lock (Users)
				{
					if (Users.ContainsKey(userK))
					{
						//unregister user from chatrooms
						User user = getUser(userK);

						user.RemoveFromAllRooms();
				
						//remove user from server
						System.GC.ReRegisterForFinalize(Users[userK]);
						Users.Remove(userK);
					}
				}
			}
			catch (Exception ex)
			{
				Status(ex.ToString(), true);
				Status(this.ToString(), true);
				throw ex;
			}

		}
		#endregion

		#region ChatServerPort
		public static int ChatServerPort
		{
			get
			{
				return 28451;
			}
		}
		#endregion

		#region Status
		/// <summary>
		/// Status delegate for logging
		/// </summary>
		public ChatServer.StatusDelegate Status { get; set; }
		#endregion
		#region StatusDelegate
		/// <summary>
		/// Delegate defining the logging function
		/// </summary>
		public delegate void StatusDelegate(string message, bool writeToFile);
		#endregion

		#region TweakGuid
		/// <summary>
		/// Tweaks a guid by overwriting the last four bytes with a unique integer. Should be faster than calling Guid.NewGuid().
		/// </summary>
		public Guid TweakGuid(Guid g)
		{
			int i = getIncrementingNumber();

			byte[] bytes = new byte[16];

			byte[] gBytes = g.ToByteArray();
			byte[] iBytes = BitConverter.GetBytes(i);

			bytes[0] = gBytes[0];
			bytes[1] = gBytes[1];
			bytes[2] = gBytes[2];
			bytes[3] = gBytes[3];
			bytes[4] = gBytes[4];
			bytes[5] = gBytes[5];
			bytes[6] = gBytes[6];
			bytes[7] = gBytes[7];
			bytes[8] = gBytes[8];
			bytes[9] = gBytes[9];
			bytes[10] = gBytes[10];
			bytes[11] = gBytes[11];

			bytes[12] = iBytes[0];
			bytes[13] = iBytes[1];
			bytes[14] = iBytes[2];
			bytes[15] = iBytes[3];

			return new Guid(bytes);
		}
		object incrementingNumberLocker = new object();
		int incrementingNumber = 0;
		int getIncrementingNumber()
		{
			lock (incrementingNumberLocker)
			{
				if (incrementingNumber > int.MaxValue - 5)
					incrementingNumber = 0;
				return ++incrementingNumber;
			}
		}
		#endregion

		#region GlobalMaxItems
		/// <summary>
		/// The maximum number of items to output - used in various places
		/// </summary>
		public static int GlobalMaxItems
		{
			get
			{
				return 20;
			}
		}
		#endregion


	}
}
