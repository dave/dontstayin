using System;
using System.DHTML;
using JQ;
using Sys.UI;
using Sys;
using ScriptSharpLibrary;


namespace JQ
{
	
	[GlobalMethods]
	[Imported]
	public static class JQueryAPI
	{

		public static JQueryObject JQuery(DOMElement e)
		{
			return null;
		}

		public static JQueryObject JQuery(string selector)
		{
			return null;
		}

		public static JQueryObject JQuery(string selector, object ob)
		{
			return null;
		}

		internal static JQueryObject JQuery(object doc)
		{
			return null;
		}
		internal static JQueryObject JQuery()
		{
			return null;
		}
		//public static JSONObject JSON;
		
	}


	//[Imported]
	//public sealed class JSONObject
	//{

	//    private JSONObject()
	//    {
	//    }

	//    public object parse(string json)
	//    {
	//        return null;
	//    }

	//    public string stringify(object obj)
	//    {
	//        return "";
	//    }


	//}
	

	[Imported]
	public sealed class JQueryObject
	{

		/// <param name="url">The URL of the page to load.</param>
		/// <param name="data">Key/value pairs that will be sent to the server.</param>
		/// <param name="callback">A function to be executed whenever the data is loaded successfully.</param>
		/// <param name="type">Type of data to be returned to callback function: "xml", "html", "script", "json", "jsonp", or "text".</param>
		public void get(string url, object data, object callback, object type, object args) { }

		public void dialog(object args) { }

		private JQueryObject()
		{
		}

		public Offset Offset()
		{
			return null;
		}

		internal int Width()
		{
			return -1;
		}

		internal int Height()
		{
			return -1;
		}

		public object Sortable(object param1, object param2)
		{
			return null;
		}

		public JQueryObject Draggable()
		{
			return null;
		}

		public JQueryObject Resizable(object options)
		{
			return null;
		}

		public Array Item;
		public void ready(Action action)
		{

		}
		public void tabs()
		{

		}

		public void bind(string eventName, BindFunctionDelegate del)
		{

		}

		public JQueryObject show(object effect, object options, object speed, object callback) { return null; }

		public JQueryObject hide(object effect, object options, object speed, object callback) { return null; }
		public JQueryObject hide() { return null; }

		public JQueryObject animate(object options, object duration, object easing, object callback) { return null; }

		public JQueryObject effect(object effect, object options, object speed, object callback) { return null; }




		internal void slideDown(Action callback)
		{
		}

		public void click(ObjectDel a)
		{
		}

		/// <param name="url">Page to get options from (must be valid JSON)</param>
		/// <param name="par">Any parameters to send with the request</param>
		/// <param name="select">Select the added options?, default true</param>
		/// <param name="fn">call this function with the select object as param after completion</param>
		/// <param name="args">(optional) array of arguments to pass onto the function</param>
		public void ajaxAddOption(object url, object par, object select, object fn, object args) { }
		public void ajaxAddOption1(object url, object par, object select, object fn, object args) { }

		/// <param name="regex">spec of which items to remove</param>
		public void removeOption(object regex) { }

		public void removeAll() { }

		/// <param name="items">dictionary containing the items</param>
		/// <param name="select">Select the added options?, default true</param>
		public void addOption(object items, object select) { }

		

	}
	public delegate bool ObjectDel(object o);
	public delegate void BindFunctionDelegate(object ev, TabsSelectEventArgs tab, TabsSelectEventArgs tab2);

	public class TabsSelectEventArgs
	{
		/// <summary>
		/// internal widget instance
		/// </summary>
		public object instance;

		/// <summary>
		/// options used to intialize this widget
		/// </summary>
		public object options;
		/// <summary>
		/// anchor element of the currently shown tab
		/// </summary>
		public DOMElement tab;
		/// <summary>
		/// element
		/// </summary>
		public DOMElement panel;
	}

	[Imported]
	public sealed class Offset
	{
		public int Left;
		public int Top;
	}

}

//[Imported]
//public sealed class JQuery
//{
//    [IntrinsicProperty]
//    internal static JQueryStatic jQuery { get { return null; } }
//}
[Imported]
public sealed class jQuery
{
	/// <param name="url">The URL of the page to load.</param>
	/// <param name="data">Key/value pairs that will be sent to the server.</param>
	/// <param name="callback">A function to be executed whenever the data is loaded successfully.</param>
	/// <param name="type">Type of data to be returned to callback function: "xml", "html", "script", "json", "jsonp", or "text".</param>
	public static void get(string url, object data, object callback, object type, object args) { }
}
