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
	[ClientScript]
	public partial class MusicDropDown : EnhancedUserControl
	{

		public MusicDropDown()
		{
		}

		public void Page_Load(object o, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Prefs.Current["MusicPref"].IsNull)
				{
					DropDown.SelectedValue = "1";
				}
				else
				{
					DropDown.SelectedValue = Prefs.Current["MusicPref"].ToString();
				}
			}
		}
	}
}
