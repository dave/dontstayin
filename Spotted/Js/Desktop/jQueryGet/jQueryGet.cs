

namespace Js.jQueryGetAPI
{
    using jQueryApi;
    using System.Runtime.CompilerServices;

	[IgnoreNamespace]
	[Imported]
	[ScriptName("$")]
	public static class jQueryGet
    {
		/// <param name="url">The URL of the page to load.</param>
		/// <param name="data">Key/value pairs that will be sent to the server.</param>
		/// <param name="callback">A function to be executed whenever the data is loaded successfully.</param>
		/// <param name="type">Type of data to be returned to callback function: "xml", "html", "script", "json", "jsonp", or "text".</param>
		public static jQueryXmlHttpRequest Get(string url, object data, object callback, object type, object args)
		{
			return null;
		}
    }
	public delegate void ActionGet(string data, string textStatus, jQueryXmlHttpRequest req, string args);
}
