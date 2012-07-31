using System;
using System.Collections.Generic;
using System.Text;

namespace ChatLibrary
{
	public interface ChatServerInterface
	{
		void JoinRoom(Guid roomGuid, int usrK);
		void JoinRoom(Guid roomGuid, int[] usrKs);
		void JoinRoom(Guid[] roomGuids, int usrK);
		void JoinRoom(Guid[] roomGuids, int[] usrKs);

		void ExitRoom(Guid roomGuid, int usrK);

		string GetLatest(int usrK, int sessionID, bool isFirstRequest, Guid lastReceivedItemGuid, ref Guid newestItemGuid);
		string GetLatestGuest(int usrK, bool isFirstRequest, Guid lastReceivedItemGuid, ref Guid newestItemGuid, Guid roomGuid);

		string PinRoom(int usrK, int sessionID, Guid lastReceivedItemGuid, ref Guid newestItemGuid, Guid pinnedRoomGuid);

		void ResetSessionID(int usrK, int sessionID);

		void SendTo(Guid roomGuid, string item, int[] usrKs, Guid itemGuid);

		void ClearRoom(Guid roomGuid);
	}

	public static class GuidExtentions
	{
		public static string Pack(this Guid g)
		{
			if (g == Guid.Empty)
				return "";

			return Convert.ToBase64String(g.ToByteArray()).Replace("x", "x1").Replace("+", "x2").Replace("/", "x3").Replace("==", string.Empty);
		}
		/// <summary>
		/// To use: Guid myGuid = Guid.Empty.UnPack(myPackedString); - have to do this because gay extention methods can't be static.
		/// </summary>
		/// <param name="g"></param>
		/// <param name="s"></param>
		/// <returns></returns>
		public static Guid UnPack(this Guid g, string s)
		{
			if (s.Length == 0)
				return Guid.Empty;

			return new Guid(Convert.FromBase64String(s.Replace("x3", "/").Replace("x2", "+").Replace("x1", "x") + "=="));
		}
	}
}
