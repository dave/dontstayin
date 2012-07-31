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
	public partial class Vote : MixmagVoteUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			int mixmagEntryK = 0;
			MixmagEntry entry = null;
			MixmagComp comp = null;
			string imageUrl = "";

			try
			{
				mixmagEntryK = ContainerPage.Url[0].ValueInt;
				if (mixmagEntryK > 0)
					entry = new MixmagEntry(mixmagEntryK);
			}
			catch
			{
				throw new Exception("Can't find that entry");
			}

			if (entry != null)
			{
				comp = MixmagComp.GetByK(entry.MixmagCompK);
				imageUrl = entry.ImageUrl;
			}
			else if (Request.QueryString["k"].IsNumeric())
			{
				comp = MixmagComp.GetByK(int.Parse(Request.QueryString["k"]));
				imageUrl = Request.QueryString["url"];
				if (!imageUrl.StartsWith("http://www.mixmag.net/") && !imageUrl.StartsWith("http://www.mixmagfashion.com/"))
				{
					throw new Exception("Invalid photo");
				}
			}
			else
			{
				throw new Exception("Can't find that competition");
			}

			VoteClosedPanel.Visible = comp.EndDate < DateTime.Now;
			if (comp.EndDate < DateTime.Now)
			{
				Vote1Panel.Visible = false;
				VoteConfirmPanel.Visible = false;
				VoteLikePanel.Visible = false;
				Vote2Panel.Visible = false;
			}

			string name = entry == null || entry.FirstName.Length == 0 ? "this photo" : entry.FirstName;

			EntryK.Value = entry == null ? "0" : entry.K.ToString();
			CompK.Value = comp.K.ToString();
			ImageUrl.Value = imageUrl;
			PageIdToLike.Value = comp.PageIdToLike.ToString();

			GlobalHeaderPlaceholder.Controls.Add(new LiteralControl(comp.GlobalHeader));

			if (Request.QueryString["k"].IsNumeric())
			{
				HeaderPlaceholder.Controls.Add(new LiteralControl(comp.VoteHeaderMicrosite.Replace("%1", name)));
				FooterPlaceholder.Controls.Add(new LiteralControl(comp.VoteFooterMicrosite.Replace("%1", name)));
			}
			else
			{
				HeaderPlaceholder.Controls.Add(new LiteralControl(comp.VoteHeaderFacebook.Replace("%1", name)));
				FooterPlaceholder.Controls.Add(new LiteralControl(comp.VoteFooterFacebook.Replace("%1", name)));
			}
			Vote1TopPlaceholder.Controls.Add(new LiteralControl(comp.Vote1Top.Replace("%1", name)));
			Vote1MiddlePlaceholder.Controls.Add(new LiteralControl(comp.Vote1Middle.Replace("%1", name)));
			Vote1BottomPlaceholder.Controls.Add(new LiteralControl(comp.Vote1Bottom.Replace("%1", name)));
			Vote1Img.Src = imageUrl;

			Vote1VoteButton.InnerText = comp.Vote1ButtonText.Replace("%1", name);

			VoteLikePlaceholder.Controls.Add(new LiteralControl(comp.VoteLike.Replace("%1", name)));

			Vote2Placeholder.Controls.Add(new LiteralControl(comp.Vote2.Replace("%1", name)));
			
		}

		

		#region VoteNow
		[Client]
		public static Hashtable VoteNow(int entryK, int compK, string imageUrl)
		{
			var facebook = new FacebookGraphAPI(Facebook.Apps.MixmagVote);
			JObject user = facebook.GetObject("me", null);

			string email = user.Value<string>("email");
			string firstName = user.Value<string>("first_name");
			string lastName = user.Value<string>("last_name");

			MixmagComp c = null;
			MixmagEntry e = null;

			if (entryK > 0)
			{
				try
				{
					e = new MixmagEntry(entryK);
				}
				catch
				{
				}
			}
			
			Hashtable ret = new Hashtable();

			if (e != null)
			{
				//got an entry
				c = MixmagComp.GetByK(e.MixmagCompK);
			}
			else if (imageUrl.Length > 0 && compK > 0)
			{
				c = MixmagComp.GetByK(compK);
				MixmagEntrySet mes = new MixmagEntrySet(new Query(new And(new Q(MixmagEntry.Columns.MixmagCompK, compK), new Q(MixmagEntry.Columns.ImageUrl, imageUrl))));
				if (mes.Count > 0)
				{
					e = mes[0];
				}
				else
				{
					//create a new skeleton entry for this image
					e = new MixmagEntry();
					e.DateTime = DateTime.Now;
					e.Email = "";
					e.FacebookUid = 0;
					e.FirstName = "";
					e.LastName = "";
					e.ImageUrl = imageUrl;
					e.MixmagCompK = c.K;
					e.Update();
				}
			}
			else
			{
				ret["Done"] = false;
				ret["Message"] = "Can't find this entry.";
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

			if (e.K > 0)
			{
				Query q = new Query(
					new And(
						new Q(Bobs.MixmagVote.Columns.MixmagEntryK, e.K),
						new Q(Bobs.MixmagVote.Columns.FacebookUID, facebook.Uid)
					)
				);
				q.ReturnCountOnly = true;
				MixmagVoteSet mes = new MixmagVoteSet(q);
				if (mes.Count > 0)
				{
					ret["Done"] = false;
					ret["Message"] = "You have already voted for this photo.";
					return ret;
				}
			}

			Bobs.MixmagVote v = new Bobs.MixmagVote();
			v.DateTime = DateTime.Now;
			v.FacebookUID = facebook.Uid;
			v.MixmagEntryK = e.K;
			v.Update();


			if (e.FacebookUid.HasValue && e.FacebookUid.Value > 0 && c.FacebookVoteMessage.Length > 0)
			{
				try
				{
					Dictionary<string, object> par = new Dictionary<string, object>();
					par["picture"] = e.ImageUrl;
					par["link"] = "http://mixmag-vote.com/" + e.K;
					par["name"] = c.FacebookPostName;
					par["caption"] = c.FacebookPostCaption;
					par["description"] = c.FacebookPostDescription;
					facebook.PutWallPost(c.FacebookVoteMessage, par);
				}
				catch { }
			}


			ret["Done"] = true;
			ret["MixmagVoteK"] = v.K;

			return ret;
		}
		#endregion
	



		#region SaveQuestion
		[Client]
		public static Hashtable SaveQuestion(int entryK, int compK, string imageUrl, string questionString)
		{
			var facebook = new FacebookGraphAPI(Facebook.Apps.MixmagVote);
			JObject user = facebook.GetObject("me", null);

			string email = user.Value<string>("email");
			string firstName = user.Value<string>("first_name");
			string lastName = user.Value<string>("last_name");

			MixmagComp c = null;
			MixmagEntry e = null;

			if (entryK > 0)
			{
				try
				{
					e = new MixmagEntry(entryK);
				}
				catch
				{
				}
			}

			
			Hashtable ret = new Hashtable();

			if (e != null)
			{
				//got an entry
				c = MixmagComp.GetByK(e.MixmagCompK);
			}
			else if (imageUrl.Length > 0 && compK > 0)
			{
				c = MixmagComp.GetByK(compK);
				MixmagEntrySet mes = new MixmagEntrySet(new Query(new And(new Q(MixmagEntry.Columns.MixmagCompK, compK), new Q(MixmagEntry.Columns.ImageUrl, imageUrl))));
				if (mes.Count > 0)
				{
					e = mes[0];
				}
				else
				{
					ret["Done"] = false;
					ret["Message"] = "Can't find this entry.";
					return ret;
				}
			}
			else
			{
				ret["Done"] = false;
				ret["Message"] = "Can't find this entry.";
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

			if (e.K == 0)
			{
				ret["Done"] = false;
				ret["Message"] = "Vote not found.";
				return ret;

			}


			Query q = new Query(
				new And(
					new Q(Bobs.MixmagVote.Columns.MixmagEntryK, e.K),
					new Q(Bobs.MixmagVote.Columns.FacebookUID, facebook.Uid)
				)
			);
			MixmagVoteSet mvs = new MixmagVoteSet(q);
			if (mvs.Count == 0)
			{
				ret["Done"] = false;
				ret["Message"] = "Vote not found.";
				return ret;
				
			}

			Bobs.MixmagVote v = mvs[0];
			v.TextField1 = questionString;
			v.Update();

			ret["Done"] = true;
			ret["MixmagVoteK"] = v.K;
			return ret;

			
		}
		#endregion

	}
}
