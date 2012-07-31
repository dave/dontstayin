using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Blank
{
	public partial class BannerEditLocation : BlankUserControl
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			if (!ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
			{
				//ScriptManager.RegisterClientScriptInclude(Page, typeof(Page), "jquery", Common.Properties.JQueryPath);
				ScriptManager.RegisterClientScriptInclude(Page, typeof(Page), "jquery", "/misc/jquery-2008-05-09/jquery-latest.js");
#if DEBUG
				ScriptManager.GetCurrent(this.Page).Scripts.Add(new ScriptReference(@"/misc/SpottedScript/spottedscript.debug.js"));

#else
				ScriptManager.GetCurrent(this.Page).Scripts.Add(new ScriptReference(@"/misc/SpottedScript/spottedscript.js"));
#endif
			}
		}

		public string PlaceKs
		{
			get
			{
 
				return null; 
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string strPlaceKs = Request.Params["placek"];
				if (strPlaceKs.Length > 0)
				{
					
					List<string> placeKs = new List<string>(strPlaceKs.Split(','));
					Query q = new Query();
					q.QueryCondition = new Or(placeKs.ConvertAll(placeK => new Q(Place.Columns.K, int.Parse(placeK))).ToArray());

					PlaceSet ps = new PlaceSet(q);
					this.uiPlacesChooser.SelectedPlaces = ps;
				}
			}
		}

  

		 

		 
	}
}
