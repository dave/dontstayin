using ScriptSharpLibrary;
using Sys;
using Sys.Net;
using System;
using System.DHTML;
using Sys.UI;

#if MOBILE
namespace MobileScript
#else
namespace SpottedScript
#endif
{
	public class Misc
	{
		public static string GetPicUrlFromGuid(string guid)
		{
			string s = (string)Script.Eval("StoragePath('" + guid + "');");
			return s;
			//return "http://s" + guid.Substr(0, 1) + ".dontstayin.com/" + guid.Substr(0, 2) + "/" + guid.Substr(2, 2) + "/" + guid + ".jpg";
		}

		public static void Redirect(string url)
		{
			// cannot set location through ScriptSharp?
			Script.Eval("window.location = '" + url + "'");
		}
		public static void AddHoverText(DOMElement el, string hoverText)
		{
			DomEvent.AddHandler(el, "mouseover", delegate { Stt(hoverText);});
			DomEvent.AddHandler(el, "mouseout", delegate { Htm();});
		}

	 

		public static void RedirectToAnchor(string anchorName)
		{
			Window.Location.Hash = anchorName;
		}

		public static void ShowWaitingCursor()
		{
			Script.Eval("ShowWaitingCursor();");
		}
		public static void HideWaitingCursor()
		{
			Script.Eval("HideWaitingCursor();");
		}

		internal static void Stt(string p)
		{
			Script.Eval("stt('" + p + "');");
		}

		internal static void Htm()
		{
			Script.Eval("htm();");
		}

		public static bool BrowserIsFirefox
		{
			get
			{
				return Browser.Agent == Browser.Firefox;
			}
		}
		public static bool BrowserIsIE
		{
			get
			{
				return Browser.Agent == Browser.InternetExplorer;
			}
		}
		public static float BrowserVersion
		{
			get
			{
				return Browser.Version;
			}
		}

		public static Action CombineAction(Action runFirst, Action runSecond)
		{
			Action composite = new Action(delegate()
			                              	{
			                              		if (runFirst != null) runFirst();
												if (runSecond != null) runSecond();
												
			                              	}
				);
			return composite;
		}
		public static EventHandler CombineEventHandler(EventHandler runFirst, EventHandler runSecond)
		{
			EventHandler composite = new EventHandler(delegate(object sender, EventArgs e)
			{
				if (runFirst != null) runFirst(sender, e);
				if (runSecond != null) runSecond(sender, e);

			}
				);
			return composite;
		}

		internal static void Debug()
		{
			Script.Literal("debugger;");
		}
	}

	public class IntEventArgs : EventArgs
	{
		public int value;
		public IntEventArgs(int value)
			: base()
		{
			this.value = value;
		}
	}

}
