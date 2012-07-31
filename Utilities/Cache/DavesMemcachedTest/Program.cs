using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
//using Memcached.ClientLibrary;

namespace DavesMemcachedTest
{
	class Program
	{
		//static MemcachedClient mc;

		static void Main(string[] args)
		{


			RunTests();


			//if (args.Length > 0)
			
			//else
			//{
			//    StateStub s = new StateStub();
			//    s.guest = false;
			//    s.guid = "AQEFAAAAAAUAAAAAvVaVmQ";
			//    s.latestItem = "AQEFAAAAAAUAAAAAvVaVmQ";
			//    s.latestItemOld = "AQEFAAAAAAUAAAAAvVaVmQ";
			//    s.latestItemSeen = "AQEFAAAAAAUAAAAAvVaVmQ";
			//    s.listOrder = 4;
			//    s.newMessages = 54;
			////	s.roomToken = "AQEFAAAAAAUAAAAAvVaVmQ";
			////	s.roomTokenDateTimeTicks = "3732189237231987";
			//    s.selected = true;
			//    s.totalMessages = 384;
			//    string longString = @"<div id=""ChatClient_RoomList"" class=""ChatClientRoomList ClearAfter""><div id=""ChatClient_Room_AQEFAAAAAAUAAAAAvVaVmQ"" class=""ChatClientRoomHolder"" roomGuid=""AQEFAAAAAAUAAAAAvVaVmQ"" roomName=""General chat"" roomUrl="""" roomPinned=""true"" roomPinable=""false"" roomSelected=""false"" roomGuest=""false"" roomNewMessages=""0"" roomTotalMessages=""0"" roomLatestItem="""" roomLatestItemSeen="""" roomLatestItemOld="""" roomReadOnly=""false"" roomListOrder=""-1"" roomIsPhotoRoom=""false"" roomIcon="""" roomTokenDateTimeTicks="""" roomToken=""""><div class=""ChatClientRoomLinkHolder""><img src=""/gfx/pin5.gif"" width=""5"" height=""7"" class=""ChatClientRoomHandle"" id=""ChatClient_Room_AQEFAAAAAAUAAAAAvVaVmQ_Handle"" onmouseover=""stt('Drag to move this chat rooms');"" onmouseout=""htm();"" style=""display:none;"" /><img src=""/gfx/pin-arrow1.gif"" width=""5"" height=""7"" class=""ChatClientRoomArrow"" id=""ChatClient_Room_AQEFAAAAAAUAAAAAvVaVmQ_Arrow"" style=""visibility:hidden;"" /><a href="""" id=""ChatClient_Room_AQEFAAAAAAUAAAAAvVaVmQ_Link"" class=""ChatClientRoomLink ChatClientRoomLinkNoUnread"">General chat</a></div><div class=""ChatClientRoomPinHolder""><img id=""ChatClient_Room_AQEFAAAAAAUAAAAAvVaVmQ_Pin"" src=""/gfx/1pix.gif"" width=""9"" height=""8"" class=""ChatClientRoomNoPin"" /></div><div class=""ChatClientRoomStatsHolder""><span class=""ChatClientRoomUnreadHolder"" id=""ChatClient_Room_AQEFAAAAAAUAAAAAvVaVmQ_Unread"">&nbsp;</span><span class=""ChatClientRoomSeperatorHolder"" id=""ChatClient_Room_AQEFAAAAAAUAAAAAvVaVmQ_StatsSeperator"">&nbsp;</span><span class=""ChatClientRoomTotalHolder"" id=""ChatClient_Room_AQEFAAAAAAUAAAAAvVaVmQ_Total"">&nbsp;</span></div></div><div id=""ChatClient_Room_AQIMBAAAAAUAAAAAaAZx2fQ"" class=""ChatClientRoomHolder"" roomGuid=""AQIMBAAAAAUAAAAAaAZx2fQ"" roomName=""Inbox updates"" roomUrl="""" roomPinned=""true"" roomPinable=""false"" roomSelected=""false"" roomGuest=""false"" roomNewMessages=""0"" roomTotalMessages=""0"" roomLatestItem="""" roomLatestItemSeen="""" roomLatestItemOld="""" roomReadOnly=""true"" roomListOrder=""-1"" roomIsPhotoRoom=""false"" roomIcon="""" roomTokenDateTimeTicks="""" roomToken=""""><div class=""ChatClientRoomLinkHolder""><img src=""/gfx/pin5.gif"" width=""5"" height=""7"" class=""ChatClientRoomHandle"" id=""ChatClient_Room_AQIMBAAAAAUAAAAAaAZx2fQ_Handle"" onmouseover=""stt('Drag to move this chat rooms');"" onmouseout=""htm();"" style=""display:none;"" /><img src=""/gfx/pin-arrow1.gif"" width=""5"" height=""7"" class=""ChatClientRoomArrow"" id=""ChatClient_Room_AQIMBAAAAAUAAAAAaAZx2fQ_Arrow"" style=""visibility:hidden;"" /><a href="""" id=""ChatClient_Room_AQIMBAAAAAUAAAAAaAZx2fQ_Link"" class=""ChatClientRoomLink ChatClientRoomLinkNoUnread"">Inbox updates</a></div><div class=""ChatClientRoomPinHolder""><img id=""ChatClient_Room_AQIMBAAAAAUAAAAAaAZx2fQ_Pin"" src=""/gfx/1pix.gif"" width=""9"" height=""8"" class=""ChatClientRoomNoPin"" /></div><div class=""ChatClientRoomStatsHolder""><span class=""ChatClientRoomUnreadHolder"" id=""ChatClient_Room_AQIMBAAAAAUAAAAAaAZx2fQ_Unread"">&nbsp;</span><span class=""ChatClientRoomSeperatorHolder"" id=""ChatClient_Room_AQIMBAAAAAUAAAAAaAZx2fQ_StatsSeperator"">&nbsp;</span><span class=""ChatClientRoomTotalHolder"" id=""ChatClient_Room_AQIMBAAAAAUAAAAAaAZx2fQ_Total"">&nbsp;</span></div></div><div id=""ChatClient_Room_AQkFAAAAAAUAAAAAU4kx2Dg"" class=""ChatClientRoomHolder"" roomGuid=""AQkFAAAAAAUAAAAAU4kx2Dg"" roomName=""Private chat (buddies)"" roomUrl="""" roomPinned=""true"" roomPinable=""false"" roomSelected=""false"" roomGuest=""false"" roomNewMessages=""0"" roomTotalMessages=""0"" roomLatestItem="""" roomLatestItemSeen="""" roomLatestItemOld="""" roomReadOnly=""true"" roomListOrder=""-1"" roomIsPhotoRoom=""false"" roomIcon="""" roomTokenDateTimeTicks="""" roomToken=""""><div class=""ChatClientRoomLinkHolder""><img src=""/gfx/pin5.gif"" width=""5"" height=""7"" class=""ChatClientRoomHandle"" id=""ChatClient_Room_AQkFAAAAAAUAAAAAU4kx2Dg_Handle"" onmouseover=""stt('Drag to move this chat rooms');"" onmouseout=""htm();"" style=""display:none;"" /><img src=""/gfx/pin-arrow1.gif"" width=""5"" height=""7"" class=""ChatClientRoomArrow"" id=""ChatClient_Room_AQkFAAAAAAUAAAAAU4kx2Dg_Arrow"" style=""visibility:hidden;"" /><a href="""" id=""ChatClient_Room_AQkFAAAAAAUAAAAAU4kx2Dg_Link"" class";
			//    Caching.Instances.Main.Set("Prefix32|UsrK|4|RoomGuid|000501010000050000000000bd569599|Version|8", serializeStateStub(s));
			//}
		}
		static string serializeStateStub(StateStub state)
		{
			string XmlizedString = null;
			MemoryStream memoryStream = new MemoryStream();
			XmlSerializer xs = new XmlSerializer(typeof(StateStub));
			XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
			xs.Serialize(xmlTextWriter, state);
			memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

			UTF8Encoding encoding = new UTF8Encoding();
			XmlizedString = encoding.GetString(memoryStream.ToArray());

			return XmlizedString;
		}
		static StateStub deSerializeStateStub(string xml)
		{
			XmlSerializer xs = new XmlSerializer(typeof(StateStub));

			UTF8Encoding encoding = new UTF8Encoding();
			Byte[] byteArray = encoding.GetBytes(xml);

			MemoryStream memoryStream = new MemoryStream(byteArray);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
			return (StateStub)xs.Deserialize(memoryStream);

		}

		public static void RunTests()
		{

			//SockIOPool sockIOPool = SockIOPool.GetInstance("default");
			//sockIOPool.SetServers(new string[] { "192.168.113.27:11211", "192.168.113.27:11212" });

			//// BinarySearches occur - assume sorted list
			//sockIOPool.Servers.Sort();
			//// don't use this feature - undesirable for our needs
			//sockIOPool.Failover = false;
			////sockIOPool.HashingAlgorithm = HashingAlgorithm.NewCompatibleHash;
			//sockIOPool.Initialize();

			//mc = new MemcachedClient();

			//while (true)
			//{
			//    try
			//    {
			//        object o = Caching.Instances.Main.Get("Prefix32|UsrK|4|RoomGuid|000501010000050000000000bd569599|Version|8");
			//        if (o == null)
			//            Console.Write("X");
			//        else
			//            Console.Write(".");

			//        System.Threading.Thread.Sleep(50);
			//    }
			//    catch (Exception ex)
			//    {
			//        Console.WriteLine();
			//        Console.WriteLine(ex.ToString());
			//        System.Threading.Thread.Sleep(5000);
			//    }
				

				
			//}







			int length = 1000;
			System.Threading.Thread[] writers = new System.Threading.Thread[length];
			System.Threading.Thread[] readers = new System.Threading.Thread[length];
			for (int i = 0; i < length; i++)
			{
				Guid g = Guid.NewGuid();
				writers[i] = new System.Threading.Thread(() => StandardMemcachedWriter(g));
				writers[i].Start();

				readers[i] = new System.Threading.Thread(() => StandardMemcachedReader(g));
				readers[i].Start();

				Console.Write("["+i.ToString()+"]");

				System.Threading.Thread.Sleep(5000 / length);
			}
		}




		public static void StandardMemcachedWriter(Guid key)
		{
			//Testing bob cache
			Random r = new Random();
			//Guid key = Guid.NewGuid();

			while (true)
			{
				try
				{
					//System.Threading.Thread.Sleep(50);

					Guid data = Guid.NewGuid();

					Caching.Instances.Main.Store(key.ToString("N"), data);

					System.Threading.Thread.Sleep(r.Next(500, 1500));
					//Caching.Instances.Main.Delete(key);

					
				}
				catch (Exception exception) { Console.Write("!"); } //exception.Message); }
			}
		}

		public static void StandardMemcachedReader(Guid key)
		{
			//Testing bob cache
			Random r = new Random();
			//Guid key = Guid.NewGuid();

			while (true)
			{
				try
				{
					//System.Threading.Thread.Sleep(50);

					//Guid data = Guid.NewGuid();

					//Caching.Instances.Main.Store(key, data);

					System.Threading.Thread.Sleep(r.Next(5000, 12000));
					//Caching.Instances.Main.Delete(key);

					object ObFromCache = Caching.Instances.Main.Get(key.ToString("N"));

					if (ObFromCache == null)
					{
						Console.Write("X");
					}
					else if (ObFromCache is Guid)
					{
						Console.Write(".");
					}
					else
						Console.Write("Q");
				}
				catch (Exception exception) { Console.Write("x"); } //exception.Message); }
			}
		}





		//public static void MemCacheTest()
		//{
		//    //Testing bob cache

		//    int i = 1;

		//    while (true)
		//    {
		//        string key = "test";// +i.ToString();

		//        Guid g = Guid.NewGuid();
		//        key = g.ToString();

		//        int maxTries = 10;

		//        int addTries = 0;
		//        while (!mc.Add(key, 1) && addTries < maxTries)
		//        {
		//            addTries++;
		//            if (addTries == maxTries)
		//            {
		//                throw new Exception("Add failed");
		//            }
		//        }

		//        int deleteTries = 0;
		//        while (!mc.Delete(key, null, 2) && deleteTries < maxTries)
		//        {
		//            deleteTries++;
		//            if (deleteTries == maxTries)
		//            {
		//                throw new Exception("Delete failed");
		//            }
		//        }

		//        //System.Threading.Thread.Sleep(100);


		//        //System.Threading.Thread.Sleep(100);

		//        //object testOb = Cache.Static.Cache.Get(g.ToString());

		//        //if (testOb == null)
		//        //{
		//        //    Console.Write("!");
		//        //}

		//        //System.Threading.Thread.Sleep(100);

		//        //System.Threading.Thread.Sleep(100);

		//        object ObFromCache = mc.Get(key);

		//        string output = "";
		//        if (addTries > 0)
		//        {
		//            output += "A"+addTries;
		//        }
		//        if (deleteTries > 0)
		//        {
		//            output += "D"+deleteTries;
		//        }
		//        if (ObFromCache != null)
		//            throw new Exception("obj exists");
		//        else
		//            Console.Write(output + ".");

		//        i++;
		//    }
		//}

	}
}
