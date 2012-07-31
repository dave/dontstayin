using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;

namespace Js.GenericUserControl
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Element GenericContainerPage { get { return (Element)Document.GetElementById(clientId + "_GenericContainerPage"); } }//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
