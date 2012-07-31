using System;
using System.DHTML;
using Sys.UI;
using Sys;
using ScriptSharpLibrary;


namespace JsonAPI
{
	[IgnoreNamespace]
	[Imported]
	public class JSON
	{
		[PreserveCase]
		public static string stringify(object o) { return null; }
	}
}

namespace FacebookAPI
{
	[IgnoreNamespace]
	[Imported]
	public class FB
	{

		[PreserveCase]
		public static void api(params object[] parameters) { }

		[PreserveCase]
		public static void login(Response response, Dictionary parameters) { }

		[PreserveCase]
		public static void logout(Response response) { }

		[PreserveCase]
		public static void getLoginStatus(Response response) { }

		[PreserveCase]
		public static Event Event;

		[PreserveCase]
		public static XFBML XFBML;

		[PreserveCase]
		public static Canvas Canvas;

		//[PreserveCase]
		//public static Connect Connect;

		//[PreserveCase]
		//public static Facebook Facebook;

	}


	[Imported]
	public class Event
	{
		public void subscribe(string eventName, Response action) { }
		public void unsubscribe(string eventName, Response action) { }
	}

	[Imported]
	public class Canvas
	{
		public void scrollTo(int pos1, int pos2) { }
	}

	[Imported]
	public class XFBML
	{
		public void parse(DOMElement element) { }
		public Host Host;
	}

	[Imported]
	public class Host
	{
		public void refresh() { }
		public void parseDomElement() {}
		public void parseDomTree() {}
	}
	//[Imported]
	//public class Connect
	//{
	//    public void requireSession(Action action) {}
	//}

	//[Imported]
	//public class Facebook
	//{
	//    public apiClient apiClient; 
	//}

	//[Imported]
	//public class apiClient
	//{
	//    public SessionInfo get_session() { return null; }
	//}

	//[Imported]
	//public class SessionInfo
	//{
	//    public string uid;
	//    public string session_key;
	//    public string secret;
	//    public string expires;
	//    public string base_domain;
	//}
	

}
