using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Spotted.Controls.EventDisplay
{
	public abstract class EventDisplay : System.Web.UI.UserControl
	{
		public Bobs.Event CurrentEvent { get; set; }
		public bool LinkToEventGallery { get; set; }
		public string Url { get { return (LinkToEventGallery) ? CurrentEvent.UrlGalleryEdit : CurrentEvent.Url(); } }
	}
}
