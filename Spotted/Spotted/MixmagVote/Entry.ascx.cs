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
	public partial class Entry : MixmagVoteUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
			int compK = int.Parse(Request.QueryString["K"]);
			MixmagComp comp = MixmagComp.GetByK(compK);

			EntryClosedPanel.Visible = comp.EndDate < DateTime.Now;
			if (comp.EndDate < DateTime.Now)
			{
				Entry1Panel.Visible = false;
				EntryConfirmPanel.Visible = false;
				Entry2Panel.Visible = false;
			}
				

			CompK.Value = comp.K.ToString();
			ImageUrl.Value = Request.QueryString["Url"];
			PageIdToLike.Value = comp.PageIdToLike.ToString();

			GlobalHeaderPlaceholder.Controls.Add(new LiteralControl(comp.GlobalHeader));

			HeaderPlaceholder.Controls.Add(new LiteralControl(comp.EntryHeader));
			FooterPlaceholder.Controls.Add(new LiteralControl(comp.EntryFooter));

			Entry1TopPlaceholder.Controls.Add(new LiteralControl(comp.Entry1Top));
			Entry1MiddlePlaceholder.Controls.Add(new LiteralControl(comp.Entry1Middle));
			Entry1BottomPlaceholder.Controls.Add(new LiteralControl(comp.Entry1Bottom));
			Entry1Img.Src = Request.QueryString["Url"];
			Entry1FacebookMessageTextbox.Text = comp.FacebookEntryMessageDefault;
			Entry1Button.InnerText = comp.Entry1ButtonText;
			Entry1DailyEmailCheckboxPara.Visible = comp.DailyEmailEnabled;
			Entry1DailyEmailCheckbox.Text = comp.Entry1DailyEmailTickBoxText;

			Entry2TopPlaceholder.Controls.Add(new LiteralControl(comp.Entry2Top));
			Entry2MiddlePlaceholder.Controls.Add(new LiteralControl(comp.Entry2Middle));
			Entry2LikeButtonPlaceholder.Controls.Add(new LiteralControl(comp.Entry2LikeButton));
			Entry2BottomPlaceholder.Controls.Add(new LiteralControl(comp.Entry2Bottom));
			Entry2Img.Src = Request.QueryString["Url"];
			


		}

		#region EnterComp
		[Client]
		public static Hashtable EnterComp(int compK, string imageUrl, string facebookMessage, bool sendEmails)
		{
			

			MixmagEntry me = null;
			var facebook = new FacebookGraphAPI(Facebook.Apps.MixmagVote);
			JObject user = facebook.GetObject("me", null);

			string email = user.Value<string>("email");
			string firstName = user.Value<string>("first_name");
			string lastName = user.Value<string>("last_name");

			MixmagComp c = MixmagComp.GetByK(compK);

			Hashtable ret = new Hashtable();

			if (!imageUrl.StartsWith("http://www.mixmag.net/") && !imageUrl.StartsWith("http://www.mixmagfashion.com/") && !Vars.DevEnv)
			{
				ret["Done"] = false;
				ret["Message"] = "This photo is invalid.";
				return ret;
			}

			if (!c.Enabled)
			{
				ret["Done"] = false;
				ret["Message"] = "This competition isn't enabled.";
				return ret;
			}

			if (DateTime.Now > c.EndDate)
			{
				ret["Done"] = false;
				ret["Message"] = "This competition is now closed.";
				return ret;
			}

			if (DateTime.Now < c.StartDate)
			{
				ret["Done"] = false;
				ret["Message"] = "This competition hasn't started yet.";
				return ret;
			}

			{
				Query q = new Query(
					new And(
						new Q(MixmagEntry.Columns.MixmagCompK, c.K),
						new Q(MixmagEntry.Columns.ImageUrl, imageUrl)
					)
				);
				MixmagEntrySet mes = new MixmagEntrySet(q);
				if (mes.Count > 0)
				{
					if (!mes[0].FacebookUid.HasValue || mes[0].FacebookUid.Value == 0)
					{
						me = mes[0];
					}
					else
					{
						ret["Done"] = false;
						ret["Message"] = "This photo has already been entered into the competition.";
						return ret;
					}
				}
			}

			{
				Query q = new Query(
					new And(
						new Q(MixmagEntry.Columns.MixmagCompK, c.K),
						new Q(MixmagEntry.Columns.FacebookUid, facebook.Uid)
					)
				);
				q.ReturnCountOnly = true;
				MixmagEntrySet mes = new MixmagEntrySet(q);
				if (mes.Count > 4)
				{
					ret["Done"] = false;
					ret["Message"] = "You have too many entries to the competition. The maximum entries per competition is five.";
					return ret;
				}
			}

			if (me == null)
				me = new MixmagEntry();
			me.DateTime = DateTime.Now;
			me.FacebookUid = facebook.Uid;
			me.ImageUrl = imageUrl;
			me.MixmagCompK = compK;
			me.SendDailyEmails = sendEmails;

			me.Email = email;
			me.FirstName = firstName;
			me.LastName = lastName;
			me.Update();

			try
			{
				Bobs.MixmagVote v = new Bobs.MixmagVote();
				v.DateTime = DateTime.Now;
				v.FacebookUID = facebook.Uid;
				v.MixmagEntryK = me.K;
				v.Update();
			}
			catch { }

			try
			{
				Dictionary<string, object> par = new Dictionary<string, object>();
				par["picture"] = me.ImageUrl;
				par["link"] = "http://mixmag-vote.com/" + me.K;
				par["name"] = c.FacebookPostName;
				par["caption"] = c.FacebookPostCaption;
				par["description"] = c.FacebookPostDescription;
				facebook.PutWallPost(facebookMessage, par);
			}
			catch { }

			ret["Done"] = true;
			ret["MixmagEntryK"] = me.K;

			return ret;
		}
		#endregion
	}

	
}
