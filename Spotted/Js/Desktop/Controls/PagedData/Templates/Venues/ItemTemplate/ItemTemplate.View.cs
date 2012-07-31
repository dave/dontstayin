using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.PagedData.Templates.Venues.ItemTemplate
{
	public partial class View
		 : Js.Controls.ClientSideRepeater.Template.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
	}
}
