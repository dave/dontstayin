using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bobs;
using SpottedScript.Controls.ChatClient.Shared;
using System.Collections;


namespace RandomChatSpammer
{
	class Program
	{
		
		static void Main(string[] args)
		{
			Console.WriteLine("Delay (ms)?");
			int delay = 500;
			try
			{
				delay = int.Parse(Console.ReadLine());
			}
			catch { }

			Console.WriteLine("Spam type? c = chat, p = photos, o = comments, u = users");
			string type = "c";
			try
			{
				type = Console.ReadLine();
			}
			catch { }


			while (true)
				SendAll(delay, type);
		}


		static void SendAll(int delay, string type)
		{
			Random r = new Random();
			Query q = new Query();
			q.TopRecords = 10;
			if (type == "c")
			{
				q.QueryCondition = new Q(ChatMessage.Columns.K, QueryOperator.GreaterThan, r.Next(15000000) + 5000);
				ChatMessageSet bs = new ChatMessageSet(q);
				if (bs.Count == 0)
				{
					for (int i = 1; i < 100; i++)
					{
						SendNow(new ChatMessage() { 
							DateTime = new DateTime(2008, 12, 1), 
							UsrK = 1,
							K = 2456543,
							Text = "This is a message"}, r);
						System.Threading.Thread.Sleep(r.Next(delay / 10, delay));
					}
				}
				else
				{
					for (int i = 0; i < bs.Count - 1; i++)
					{
						SendNow(bs[i], r);

						System.Threading.Thread.Sleep(r.Next(delay / 10, delay));
						//Console.ReadLine();
					}
				}
			}
			else if (type == "p")
			{
				q.QueryCondition = new Q(Photo.Columns.K, QueryOperator.GreaterThan, r.Next(8000000) + 5000);
				PhotoSet bs = new PhotoSet(q);

				for (int i = 0; i < bs.Count - 1; i++)
				{
					SendNow(bs[i], r);

					System.Threading.Thread.Sleep(r.Next(delay / 10, delay));
					//Console.ReadLine();
				}
			}
			else if (type == "o")
			{
				q.QueryCondition = new Q(Comment.Columns.K, QueryOperator.GreaterThan, r.Next(8000000) + 5000);
				CommentSet bs = new CommentSet(q);

				for (int i = 0; i < bs.Count - 1; i++)
				{
					try
					{
						bs[i].SendLiveChatMessagesForTesting();
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
					}

					System.Threading.Thread.Sleep(r.Next(delay / 10, delay));
					//Console.ReadLine();
				}
			}
			else if (type == "u")
			{
				q.QueryCondition = new And(new Q(Usr.Columns.K, QueryOperator.GreaterThan, r.Next(2000000) + 100), new Q(Usr.Columns.CommentCount, QueryOperator.GreaterThan, 100));
				UsrSet bs = new UsrSet(q);

				for (int i = 0; i < bs.Count - 1; i++)
				{
					try
					{
						Guid g = Guid.Empty;

						ChatLibrary.ChatServerInterface cs1 = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
						cs1.JoinRoom(new Chat.RoomSpec(RoomType.Normal).Guid, bs[i].K);

						ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
						string chatItems = cs.GetLatest(bs[i].K, 23423, true, Guid.Empty, ref g);
						Console.WriteLine(chatItems);
					}
					catch(Exception ex)
					{
						Console.WriteLine(ex.ToString());
					}

					System.Threading.Thread.Sleep(r.Next(delay / 10, delay));
					//Console.ReadLine();
				}
			}

		}
		public static void SendNow(Photo p, Random r)
		{
			try
			{
				p.SendPhotoChatAlerts();
				//Guid roomGuidAll = new Chat.RoomSpec(RoomType.NewPhotosAll, ObjectType.None, 0, ObjectType.None, 0).Guid;
				//Guid roomGuidSpotters = new Chat.RoomSpec(RoomType.NewPhotosSpotters, ObjectType.None, 0, ObjectType.None, 0).Guid;
				//Guid roomGuidPro = new Chat.RoomSpec(RoomType.NewPhotosProSpotters, ObjectType.None, 0, ObjectType.None, 0).Guid;

				//PhotoStub ps = new PhotoStub(
				//        Guid.NewGuid().Pack(),
				//        ItemType.PhotoAlert,
				//        DateTime.Now.Ticks.ToString(),
				//        roomGuidSpotters.Pack(),
				//        p.WebWidth,
				//        p.WebHeight,
				//        p.Url(),
				//        p.Web.ToString(),
				//        p.Icon.ToString(),
				//        false);

				//Chat.SendJsonChatItem(ps);
				//ps.guid = Guid.NewGuid().Pack();
				//ps.roomGuid = roomGuidAll.Pack();
				//Chat.SendJsonChatItem(ps);

				//if (p.Usr.IsProSpotter)
				//{
				//    PhotoStub ps1 = new PhotoStub(
				//        Guid.NewGuid().Pack(),
				//        ItemType.PhotoAlert,
				//        DateTime.Now.Ticks.ToString(),
				//        roomGuidPro.Pack(),
				//        p.WebWidth,
				//        p.WebHeight,
				//        p.Url(),
				//        p.Web.ToString(),
				//        p.Icon.ToString(),
				//        false);

				//    Chat.SendJsonChatItem(ps1);
				//    ps1.guid = Guid.NewGuid().Pack();
				//    ps1.roomGuid = roomGuidAll.Pack();
				//    Chat.SendJsonChatItem(ps1);
				//}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
		public static void SendNow(ChatMessage c, Random r)
		{
			try
			{
				Query q = new Query();
				q.QueryCondition = new Q(RoomPin.Columns.Pinned, true);
				RoomPinSet rps = new RoomPinSet(q);

				Guid roomGuid;
				int rand = r.Next(10);
				if (rand == 1)
					roomGuid = new Chat.RoomSpec(RoomType.RandomChat, Model.Entities.ObjectType.None, 0, Model.Entities.ObjectType.None, 0).Guid;
				else if (rand == 2)
					roomGuid = new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.None, 0, Model.Entities.ObjectType.None, 0).Guid;
				else
					roomGuid = rps[r.Next(rps.Count)].RoomGuid;


				string chatRoomName = roomGuid.ToString();
				try
				{
					Chat.RoomSpec c1 = Chat.RoomSpec.FromGuid(roomGuid);
					if (c1.GetName().Length > 0)
						chatRoomName = c1.GetName();
					else
						chatRoomName = c1.RoomType.ToString();

					if (c1.RoomType != RoomType.Normal &&
						c1.RoomType != RoomType.PrivateChat)
						return;

				}
				catch { }


				MessageStub m = new MessageStub(
					Guid.NewGuid().Pack(),
					ItemType.PublicChatMessage,
					DateTime.Now.Ticks.ToString(),
					roomGuid.Pack(),
					c.FromUsr.NickName,
					c.FromUsr.StmuParams,
					c.FromUsr.K,
					c.FromUsr.HasPic ? c.FromUsr.Pic.ToString() : "0",
					c.FromUsr.HasChatPic ? c.FromUsr.ChatPic.Value.ToString() : "0",
					c.Text + " (" + chatRoomName + ")",
					"");

				Chat.SendJsonChatItem(m, c.FromUsr.K);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.ToString());
				throw ex;
			}
		}
	}
}
