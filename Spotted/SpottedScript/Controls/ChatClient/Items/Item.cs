using System.XML;
using System;
using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;

namespace SpottedScript.Controls.ChatClient.Items
{

	public interface IHasPostingUsr
	{
		int PostingUsrK { get; }
	}

	public class Item
	{
		protected Controller Parent;
		public string Guid;
		public ItemType Type;
		string DateTime;
		public string RoomGuid;
		/// <summary>
		/// This tells us which server request this item was downloaded in (for batch ui updates)
		/// </summary>
		public int ServerRequestIndex;
		public bool FromGuestRefresh;
		int Instance;

		public Item(ItemStub itemStub, Controller parent)
		{
			Parent = parent;
			Guid = itemStub.guid;
			Type = itemStub.type;
			DateTime = itemStub.dateTime;
			RoomGuid = itemStub.roomGuid;
			FromGuestRefresh = false;
		}

		protected int GetAgeInMinutes()
		{
			return (int)Math.Floor(Parent.GetMessageAge(DateTime) / 60000);
		}

		public static Item Create(ItemStub itemStub, Controller parent, int serverRequestIndex, bool fromGuestRefresh, int instance)
		{
			ItemType type = itemStub.type;
			Item item;
			
			if (type == ItemType.LoginAlert)
				item = new Login((AlertStub)itemStub, parent, serverRequestIndex);
			else if (type == ItemType.LogoutAlert)
				item = new Logout((AlertStub)itemStub, parent, serverRequestIndex);
			else if (type == ItemType.LaughAlert)
				item = new Laugh((LaughStub)itemStub, parent, serverRequestIndex);
			else if (type == ItemType.PrivateChatMessage)
				item = new Private((PrivateStub)itemStub, parent, serverRequestIndex);
			else if (type == ItemType.CommentChatMessage)
				item = new CommentMessage((CommentMessageStub)itemStub, parent, serverRequestIndex);
			else if (type == ItemType.PublicChatMessage)
				item = new Message((MessageStub)itemStub, parent, serverRequestIndex);
			else if (type == ItemType.PhotoAlert)
				item = new Photo((PhotoStub)itemStub, parent, serverRequestIndex);
			else if (type == ItemType.TopPhoto)
				item = new TopPhoto((TopPhotoStub)itemStub, parent);
			else
				item = new Item(itemStub, parent);

			item.Instance = instance;
			item.ServerRequestIndex = serverRequestIndex;
			item.FromGuestRefresh = fromGuestRefresh;

			return item;
		}
		

	}
}
