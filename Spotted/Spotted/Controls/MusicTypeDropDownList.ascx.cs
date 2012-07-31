using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;

namespace Spotted.Controls
{
	/// <summary>
	/// notes: this control is referenced by Name in DsiPage to get the newly selected MusicType
	/// into Prefs early enough to use in relevance holders for banners, etc.
	/// might consider loading values from database(cache) each time, but since values rarely change,
	/// it's not such a worry at the moment.
	/// </summary>
	public partial class MusicTypeDropDownList : System.Web.UI.UserControl
	{
		public bool AutoPostback = true;
		public string OnChangeAttribute
		{
			get
			{
				if (AutoPostback)
					return " onchange=\"__doPostBack('<%= Name %>','');\"";
				else
					return "";
			}
		}
		
		public static readonly string Name = "MusicTypeDropDownList";
		public static bool Clicked(HttpRequest request)
		{
			return request.Params.Get("__EVENTTARGET") == Name;
		}
		public string MusicDropDown(int MusicTypeK)
		{
			if (Prefs.Current["MusicPref"].IsNull)
				return (1 == MusicTypeK) ? "selected" : "";
			else
			{
				return Prefs.Current["MusicPref"] == MusicTypeK ? "selected" : "";
			}
		}
		public string SelectTagID
		{
			get
			{
				return this.ClientID + "_Select";
			}
		}
	}
}
