using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;

namespace Js.DsiUserControl
{
	public partial class View
			: Js.GenericUserControl.View
	{
		public string clientId;
		public View(string clientId)
			: base(clientId)
		{
			this.clientId = clientId;
		}
		public Element GenericContainerPage { get { return (Element)Document.GetElementById(clientId + "_GenericContainerPage"); } }//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
