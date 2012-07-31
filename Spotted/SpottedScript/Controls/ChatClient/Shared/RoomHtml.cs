#if SCRIPT
using Spotted.System.Text;
using System;
#else
using System.Text;
using Bobs.StorageScriptCompatibility;
#endif
namespace SpottedScript.Controls.ChatClient.Shared
{
	public class RoomHtml
	{
		public RoomStub RoomStub;
		public string ClientID;
		public string LinkID;
		//public string ArrowID;
		public string PresenceID;
		public string CrossID;
		public string TotalID;
		public string StatsSeperatorID;
		public string UnreadID;
		bool LoggedIn;

		public RoomHtml(RoomStub roomStub, bool loggedIn)
		{
			RoomStub = roomStub;

			ClientID = RoomStub.parentClientID + "_Room_" + RoomStub.guid;
			LinkID = ClientID + "_Link";
			//ArrowID = ClientID + "_Arrow";
			PresenceID = ClientID + "_Presence";
			CrossID = ClientID + "_Cross";
			TotalID = ClientID + "_Total";
			StatsSeperatorID = ClientID + "_StatsSeperator";
			UnreadID = ClientID + "_Unread";
			LoggedIn = loggedIn;

		}

		public string ToHtml()
		{
			StringBuilder sb = new StringBuilder();

			AppendHtml(sb);

			return sb.ToString();
		}
		public void AppendHtml(StringBuilder sb)
		{
			sb.Append(@"<div");
			sb.AppendAttribute("id", ClientID);
			sb.AppendAttribute("class", RoomStub.selected ? "ChatClientRoomHolder ChatClientRoomSelected" : "ChatClientRoomHolder");
			sb.AppendAttribute("roomGuid", RoomStub.guid);
			sb.AppendAttribute("roomName", RoomStub.name);
			sb.AppendAttribute("roomUrl", RoomStub.url);
			sb.AppendAttribute("roomPinned", RoomStub.pinned.ToString().ToLowerCase());
			sb.AppendAttribute("roomIsStarredByDefault", RoomStub.isStarredByDefault.ToString().ToLowerCase());
			sb.AppendAttribute("roomStarred", RoomStub.starred.ToString().ToLowerCase());
			sb.AppendAttribute("roomStarrable", RoomStub.starrable.ToString().ToLowerCase());
			sb.AppendAttribute("roomPinable", RoomStub.pinable.ToString().ToLowerCase());
			sb.AppendAttribute("roomSelected", RoomStub.selected.ToString().ToLowerCase());
			sb.AppendAttribute("roomGuest", RoomStub.guest.ToString().ToLowerCase());
			sb.AppendAttribute("roomNewMessages", RoomStub.newMessages.ToString());
			sb.AppendAttribute("roomTotalMessages", RoomStub.totalMessages.ToString());
			sb.AppendAttribute("roomLatestItem", RoomStub.latestItem);
			sb.AppendAttribute("roomLatestItemSeen", RoomStub.latestItemSeen);
			sb.AppendAttribute("roomLatestItemOld", RoomStub.latestItemOld);
			sb.AppendAttribute("roomReadOnly", RoomStub.readOnly.ToString().ToLowerCase());
			sb.AppendAttribute("roomListOrder", RoomStub.listOrder.ToString());
			sb.AppendAttribute("roomIsPhotoChatRoom", RoomStub.isPhotoChatRoom.ToString().ToLowerCase());
			sb.AppendAttribute("roomIsPrivateChatRoom", RoomStub.isPrivateChatRoom.ToString().ToLowerCase());
			sb.AppendAttribute("roomIsNewPhotoAlertsRoom", RoomStub.isNewPhotoAlertsRoom.ToString().ToLowerCase());
			sb.AppendAttribute("roomPresence", ((int)RoomStub.presence).ToString());
			sb.AppendAttribute("roomIcon", RoomStub.icon);
			sb.AppendAttribute("roomTokenDateTimeTicks", RoomStub.tokenDateTimeTicks);
			sb.AppendAttribute("roomToken", RoomStub.token);
			sb.AppendAttribute("roomHasArchive", RoomStub.hasArchive.ToString().ToLowerCase());
			if (RoomStub.hiddenFromRoomList)
				sb.AppendAttribute("style", "display:none;");
			sb.AppendAttribute("roomHiddenFromRoomList", RoomStub.hiddenFromRoomList.ToString().ToLowerCase());
			sb.AppendAttribute("roomIsStreamRoom", RoomStub.isStreamRoom.ToString().ToLowerCase());
			sb.Append(@">");

			#region LinkHolder
			sb.Append(@"<div class=""ChatClientRoomLinkHolder"">");

			//sb.Append(@"<img src=""/gfx/pin-fader3.png"" width=""50"" height=""20"" class=""ChatClientRoomLinkFader"" />");

			#region RoomLink
			sb.Append(@"<span");
			sb.AppendAttribute("id", LinkID);
			sb.AppendAttribute("class", RoomStub.selected ? "ChatClientRoomLink ChatClientRoomLinkSelected" : (RoomStub.newMessages == 0 ? "ChatClientRoomLink ChatClientRoomLinkNoUnread" : "ChatClientRoomLink"));
			if (RoomStub.icon.Length > 0)
			{
				sb.AppendAttribute("onmouseover", "stma('" + RoomStub.icon + "');");
				sb.AppendAttribute("onmouseout", "htm();");
			}
			sb.Append(@">");
			sb.Append(RoomStub.name);
			sb.Append(@"</span>"); //RoomLink
			#endregion

			#region Presence
			if (RoomStub.isPrivateChatRoom && (RoomStub.presence == PresenceState.Online || RoomStub.presence == PresenceState.Chatting))
			{
				sb.Append(@"<img");
				sb.AppendAttribute("src", RoomStub.presence == PresenceState.Chatting ? "/gfx/chat-chatting.png" : "/gfx/chat-online.png");
				sb.AppendAttribute("width", RoomStub.presence == PresenceState.Chatting ? "13" : "9");
				sb.AppendAttribute("height", "11");
				sb.AppendAttribute("id", PresenceID);
				sb.AppendAttribute("onmouseover", RoomStub.presence == PresenceState.Online ? "sttd(3);" : "sttd(4);");
				sb.AppendAttribute("onmouseout", "htm();");
				sb.AppendAttribute("class", "ChatClientRoomPresence");
				sb.Append(@" />"); //Presence
			}
			#endregion

			#region RoomArrow - removed
			//sb.Append(@"<img src=""/gfx/chat-handle.png"" width=""11"" height=""11"" class=""ChatClientRoomArrow""");
			//sb.AppendAttribute("id", ArrowID);
			//sb.AppendAttribute("onmouseover", "sttd(5);");
			//sb.AppendAttribute("onmouseout", "htm();");
			//if (!(LoggedIn && RoomStub.selected && !RoomStub.guest && RoomStub.pinned && RoomStub.isPrivateChatRoom))
			//    sb.AppendAttribute("style", "visibility:hidden;");
			//sb.Append(@" />"); //RoomArrow
			#endregion

			sb.Append(@"</div>"); //LinkHolder
			#endregion

			#region CrossHolder
			sb.Append(@"<div class=""ChatClientRoomPinHolder"">");
			#region Cross IMG
			bool showCross = LoggedIn && RoomStub.pinable;
			sb.Append(@"<img");
			sb.AppendAttribute("id", CrossID);
			sb.AppendAttribute("src", showCross ? "/gfx/chat-cross.png" : "/gfx/1pix.gif");
			sb.Append(@" width=""11"" height=""11""");
			sb.AppendAttribute("class", showCross ? "ChatClientRoomCross" : "ChatClientRoomNoCross");
			if (showCross)
			{
				sb.AppendAttribute("onmouseover", "sttd(1);");
				sb.AppendAttribute("onmouseout", "htm();");
			}
			sb.Append(@" />"); //Cross
			#endregion
			sb.Append(@"</div>"); //PinHolder
			#endregion

			#region StatsHolder
			sb.Append(@"<div class=""ChatClientRoomStatsHolder"">");
			#region UnreadHolder
			sb.Append(@"<span class=""ChatClientRoomUnreadHolder""");
			sb.AppendAttribute("id", UnreadID);
			sb.Append(@">");
			sb.Append(RoomStub.newMessages == 0 ? "&nbsp;" : RoomStub.newMessages.ToString());
			sb.Append(@"</span>"); //UnreadHolder
			#endregion
			#region SeperatorHolder
			sb.Append(@"<span class=""ChatClientRoomSeperatorHolder""");
			sb.AppendAttribute("id", StatsSeperatorID);
			sb.Append(@">");
			sb.Append(RoomStub.newMessages > 0 ? "/" : "&nbsp;");
			sb.Append(@"</span>"); //SeperatorHolder
			#endregion
			#region TotalHolder
			sb.Append(@"<span class=""ChatClientRoomTotalHolder""");
			sb.AppendAttribute("id", TotalID);
			sb.Append(@">");
			sb.Append(RoomStub.totalMessages == 0 ? "&nbsp;" : RoomStub.totalMessages.ToString());
			sb.Append(@"</span>"); //TotalHolder
			#endregion
			sb.Append(@"</div>"); //StatsHolder
			#endregion

			sb.Append(@"</div>"); //RoomHolder
		}
		static bool IsFireFox2
		{
			get
			{
#if SCRIPT
				return Misc.BrowserIsFirefox && Misc.BrowserVersion >= 2.0 && Misc.BrowserVersion < 3.0;
#else
				return System.Web.HttpContext.Current.Request.Browser.Browser == "Firefox" && System.Web.HttpContext.Current.Request.Browser.MajorVersion == 2;
#endif
			}
		}
		static bool IsIE
		{
			get
			{
#if SCRIPT
				return Misc.BrowserIsIE;
#else
				return System.Web.HttpContext.Current.Request.Browser.Browser == "IE";
#endif
			}
		}
	}
}
