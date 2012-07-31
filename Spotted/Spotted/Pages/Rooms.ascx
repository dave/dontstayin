<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Rooms.ascx.cs" Inherits="Spotted.Pages.Rooms" %>
<%@ Import Namespace="SpottedScript.Controls.ChatClient.Shared" %>
<dsi:h1 runat="server" ID="MessagesHeader">Chat rooms</dsi:h1>
<div class="ContentBorder">
	<p>
		As you browse the site, we suggest chat rooms for you to chat in. They appear as un-pinned rooms at the bottom of the chat box. If you're interested in one, pin it to your chat box by clicking the pin button.
	</p>
	<p>
		On this page you'll find special rooms that you can pin to your chat box.
	</p>
	
	<h2>Popular chat rooms</h2>
	<asp:PlaceHolder runat="server" ID="PopularRooms"></asp:PlaceHolder>
	
	<h2>
		General chat
	</h2>
	<p>
		This is the main chat room for general chat:
	</p>
	<p class="CleanLinks">
		<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal)) %>
	</p>
	
	<h2>
		Inbox updates
	</h2>
	<p>
		Each time a topic gets added to your inbox, we'll send you an alert to this room:
	</p>
	<p class="CleanLinks">
		<%= ChatRoomLink(new Chat.RoomSpec(RoomType.InboxUpdates, Model.Entities.ObjectType.Usr, Usr.Current.K)) %>
	</p>
	
	<h2>
		Buddy alerts
	</h2>
	<p>
		Keep up to date on what your buddies are doing with this room
	</p>
	<p class="CleanLinks">
		<%= ChatRoomLink(new Chat.RoomSpec(RoomType.BuddyAlerts)) %>
	</p>
	
	
	<h2>
		Photos
	</h2>
	<p>
		Each time a photo / video gets uploaded to the site, we will alert you with a chat item. We've got several rooms so you can get alerted to exactly what you want:
	</p>
	<p class="CleanLinks">
		<%= ChatRoomLink(new Chat.RoomSpec(RoomType.NewPhotosAll)) %><br />
		<small>
			All photos / videos
		</small>
	</p>
	<p class="CleanLinks">
		<%= ChatRoomLink(new Chat.RoomSpec(RoomType.NewPhotosProSpotters)) %><br />
		<small>
			Just photos / videos uploaded by pro spotters
		</small>
	</p>
	<p class="CleanLinks">
		<%= ChatRoomLink(new Chat.RoomSpec(RoomType.NewPhotosSpotters)) %><br />
		<small>
			Just photos / videos uploaded by spotters
		</small>
	</p>
	<p class="CleanLinks">
		<%= ChatRoomLink(new Chat.RoomSpec(RoomType.NewPhotosBuddies))%><br />
		<small>
			Just photos / videos uploaded by your buddies
		</small>
	</p>
	<p class="CleanLinks">
		<%= ChatRoomLink(new Chat.RoomSpec(RoomType.NewVideosAll)) %><br />
		<small>
			Just videos
		</small>
	</p>
	
	
	<h2>
		Private chat alerts
	</h2>
	<p>
		When someone sends you a private chat message and you don't have their private chat room pinned, we send an alert to one of these rooms:
	</p>
	<p class="CleanLinks">
		<%= ChatRoomLink(new Chat.RoomSpec(RoomType.PrivateChatRequestsBuddies)) %><br />
		<small>
			Private chat messages from your buddies
		</small>
	</p>
	<p class="CleanLinks">
		<%= ChatRoomLink(new Chat.RoomSpec(RoomType.PrivateChatRequestsStrangers)) %><br />
		<small>
			Private chat messages from strangers
		</small>
	</p>
	
	<h2>
		Random comments
	</h2>
	<p>
		Each time a comment is posted (accross the whole site), we send an alert here:
	</p>
	<p class="CleanLinks">
		<%= ChatRoomLink(new Chat.RoomSpec(RoomType.RandomChat)) %>
	</p>
	
	<h2>
		Laughs
	</h2>
	<p>
		Each time someone clicks the "this made me laugh" link, we post an alert in here:
	</p>
	<p class="CleanLinks">
		<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Laughs)) %>
	</p>
	
	<h2>
		Popups
	</h2>
	<p>
		When a new message arrives in a starred room, we show a popup. The animations on these popups can cause some somputers to slow down. You can turn them off here:
	</p>
	<p>
		<asp:RadioButton runat="server" ID="PopupAminationsOn" Text="Animation on" AutoPostBack="true" OnCheckedChanged="PopupAminationsChange" GroupName="PopupAminations"/><br />
		<asp:RadioButton runat="server" ID="PopupAminationsOff" Text="Animation off" AutoPostBack="true" OnCheckedChanged="PopupAminationsChange" GroupName="PopupAminations" />
	</p>

	<asp:Panel runat="server" ID="Testing">
		<h2>
			System messages
		</h2>
		<p>
			Only really for nerds - this room contains system messages and alerts from the matrix!
		</p>
		<p class="CleanLinks">
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.SystemMessages, Model.Entities.ObjectType.Usr, Usr.Current.K))%>
		</p>
		<p class="CleanLinks">
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Orphans))%>
		</p>
		
		<h2>Testing</h2>
		<p class="CleanLinks">
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Place, 1))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Place, 2))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Place, 3))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Place, 4))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Place, 5))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Place, 6))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Place, 7))%><br />
		</p>
		<p class="CleanLinks">
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Group, 3494))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Group, 12843))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Group, 3495))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Group, 3245))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Group, 3229))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Group, 5256))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Group, 4537))%><br />
		</p>
		<p class="CleanLinks">
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Usr, 1))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Usr, 2))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Usr, 4))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Usr, 5))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Usr, 6))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Usr, 7))%><br />
			<%= ChatRoomLink(new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Usr, 8))%><br />
		</p>
	</asp:Panel>
	
</div>

