using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using Common;
using System.Collections;
using Facebook;
using Newtonsoft.Json.Linq;

namespace Spotted.MixmagVote
{
	public partial class Repost : MixmagVoteUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int entryK = int.Parse(Request.QueryString["entry"]);
			MixmagEntry me = new MixmagEntry(entryK);

			EntryK.Value = me.K.ToString();

			MixmagComp comp = MixmagComp.GetByK(me.MixmagCompK);

			GlobalHeaderPlaceholder.Controls.Add(new LiteralControl(comp.GlobalHeader));

			HeaderPlaceholder.Controls.Add(new LiteralControl(comp.RepostHeader));
			FooterPlaceholder.Controls.Add(new LiteralControl(comp.RepostFooter));

			Repost1Img.Src = me.ImageUrl;
			Repost1TopPlaceholder.Controls.Add(new LiteralControl(comp.Repost1Top));
			Repost1MiddlePlaceholder.Controls.Add(new LiteralControl(comp.Repost1Middle));
			Repost1BottomPlaceholder.Controls.Add(new LiteralControl(comp.Repost1Bottom));
			Repost1Button.InnerHtml = comp.Repost1ButtonText;
			Repost2Placeholder.Controls.Add(new LiteralControl(comp.Repost2));

			Repost1FacebookMessageTextbox.Text = comp.FacebookEntryMessageDefault;


		}

		#region VoteNow
		[Client]
		public static Hashtable RepostNow(int entryK, string message)
		{
			var facebook = new FacebookGraphAPI(Facebook.Apps.MixmagVote);
			JObject user = facebook.GetObject("me", null);

			MixmagEntry me = new MixmagEntry(entryK);
			MixmagComp c = MixmagComp.GetByK(me.MixmagCompK);

			Hashtable ret = new Hashtable();

			try
			{
				Dictionary<string, object> par = new Dictionary<string, object>();
				par["picture"] = me.ImageUrl;
				par["link"] = "http://mixmag-vote.com/" + me.K;
				par["name"] = c.FacebookPostName;
				par["caption"] = c.FacebookPostCaption;
				par["description"] = c.FacebookPostDescription;
				facebook.PutWallPost(message, par);
			}
			catch
			{
				ret["Done"] = false;
				return ret;
			}

			ret["Done"] = true;
			return ret;
		}
		#endregion

	}
}
