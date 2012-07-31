using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.LinkCloud
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public DivElement Panel1 {get {if (_Panel1 == null) {_Panel1 = (DivElement)Document.GetElementById(clientId + "_Panel1");}; return _Panel1;}} private DivElement _Panel1;
		public jQueryObject Panel1J {get {if (_Panel1J == null) {_Panel1J = jQuery.Select("#" + clientId + "_Panel1");}; return _Panel1J;}} private jQueryObject _Panel1J;
	}
}
