using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.MusicDropDown
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public SelectElement DropDown {get {if (_DropDown == null) {_DropDown = (SelectElement)Document.GetElementById(clientId + "_DropDown");}; return _DropDown;}} private SelectElement _DropDown;
		public jQueryObject DropDownJ {get {if (_DropDownJ == null) {_DropDownJ = jQuery.Select("#" + clientId + "_DropDown");}; return _DropDownJ;}} private jQueryObject _DropDownJ;
	}
}
