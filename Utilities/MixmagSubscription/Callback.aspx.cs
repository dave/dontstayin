using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace MixmagSubscription
{
	public partial class Callback : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string fb_sig_time = Request.Form["fb_sig_time"];
			long fb_sig_user = long.Parse(Request.Form["fb_sig_user"]);
			string fb_sig_api_key = Request.Form["fb_sig_api_key"];
			string fb_sig_linked_account_ids = Request.Form["fb_sig_linked_account_ids"];
			string fb_sig = Request.Form["fb_sig"];

			if (Request.QueryString["type"] == "authorize")
			{
				int fb_sig_authorize = int.Parse(Request.Form["fb_sig_authorize"]);
				string fb_sig_profile_update_time = Request.Form["fb_sig_profile_update_time"];
				string fb_sig_session_key = Request.Form["fb_sig_session_key"];
				string fb_sig_expires = Request.Form["fb_sig_expires"];
			}
			else if (Request.QueryString["type"] == "remove")
			{
				int fb_sig_uninstall = int.Parse(Request.Form["fb_sig_uninstall"]);
				string fb_sig_app_id = Request.Form["fb_sig_app_id"];
				int fb_sig_added = int.Parse(Request.Form["fb_sig_added"]);

				Query q2 = new Query();
				q2.QueryCondition = new Q(Bobs.MixmagSubscription.Columns.FacebookUID, fb_sig_user);
				MixmagSubscriptionSet mss = new MixmagSubscriptionSet(q2);
				if (mss.Count > 0)
				{
					Bobs.MixmagSubscription subscriber = mss[0];
					subscriber.FacebookPermissionEmail = false;
					subscriber.FacebookPermissionPublish = false;
					subscriber.SendMixmag = false;
					subscriber.PublishStoryOnRead = false;
					subscriber.Update();
				}


			}
		}
	}
}
