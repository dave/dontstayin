//mappings.Add("Spotted.Controls.MapControl", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using Js.Library;

namespace Js.Controls.MapBrowser.Map
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Element uiMapControl {get {return (Element) Document.GetElementById(clientId + "_uiMapControl");}}//mappings.Add("Spotted.Controls.MapControl", ElementGetter("Element"));
	}
}
