using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Text;
using Bobs;
using Common;
using System.Collections;
using Facebook;

namespace Spotted.MixmagGreatest
{
	public class SessionObject
	{
		public string access_token;
		public string base_domain;
		public string expires;
		public string secret;
		public string session_key;
		public string sig;
		public string uid;
	}
	public partial class Home : MixmagGreatestUserControl
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			PageIdToLike.Value = Facebook.FacebookCommon.Common(Facebook.Apps.MixmagGreatest).PageId.ToString();
			ContainerPage.BodyTag.Style["overflow"] = "hidden";
			//"{\"uid\":\"513584417\",\"session_key\":\"2.AQDx6MRd8nUqn5oY.3600.1316102400.1-513584417\",\"secret\":\"7VmpjjS4YkwyVQ8telmflQ__\",\"expires\":1316102400,\"base_domain\":\"dontstayin.com\",\"
			//"access_token=AAADFav5hjiYBAJ1ZB6qdhs5phaqdurpzqpQrH5miD61cs9OwxEpReOu9pBZBMaqZAyf8o6wSjDScWHoZCbvBMRukEM88P7XcYNZAsSPzGDwZDZD\",\"sig\":\"c92f793930c372f512fd1da019b5bd7a\"}"
			//"access_token=AAADFav5hjiYBAJ1ZB6qdhs5phaqdurpzqpQrH5miD61cs9OwxEpReOu9pBZBMaqZAyf8o6wSjDScWHoZCbvBMRukEM88P7XcYNZAsSPzGDwZDZD&base_domain=dontstayin.com&expires=1316102400&secret=7VmpjjS4YkwyVQ8telmflQ__&session_key=2.AQDx6MRd8nUqn5oY.3600.1316102400.1-513584417&sig=c92f793930c372f512fd1da019b5bd7a&uid=513584417"


		//	bool gotSession = HttpContext.Current.Request["session"] != null && HttpContext.Current.Request["session"].Length > 0;

		//	DoneRefresh.Value = gotSession ? "1" : "0";

			SafariKludge.Value = (Request.UserAgent.ToLower().Contains("safari") || Request.UserAgent.ToLower().Contains("msie") || Vars.DevEnv).ToString().ToLower();


			string signedRequest = HttpContext.Current.Request["signed_request"];
			SignedRequest.Value = signedRequest;
			

			

			if (signedRequest == null)
			{
				string appData = "&app_data=" + Url.MixmagGreatestDjK.ToString() + "-" + (Request.QueryString["s"] == null ? "0" : "1") + "-" + (Request.QueryString["fb"] == null ? "0" : "1");
				if (Vars.DevEnv)
					Response.Redirect("http://www.facebook.com/pages/MixmagTest2/247863881900953?sk=app_217063325011494" + appData);
				else
					Response.Redirect("http://www.facebook.com/MixmagMagazine?sk=app_220006991381259" + appData);
			}
			else
			{
				JObject req = Facebook.FacebookCommon.DecodeFacebookRequest(signedRequest, Apps.MixmagGreatest);
				#region sample signedRequest
				/*
				{
				  "algorithm": "HMAC-SHA256",
				  "expires": 1312902000,
				  "issued_at": 1312897172,
				  "app_data": "????",
				  "oauth_token": "217063325011494|2.AQBQXYMXowmqEDC4.3600.1312902000.0-513584417|H4gnYUMiuQm9FmttzwxZRjxhdtU",
				  "page": {
					"id": "247863881900953",
					"liked": false,
					"admin": true
				  },
				  "user": {
					"country": "gb",
					"locale": "en_US",
					"age": {
					  "min": 21
					}
				  },
				  "user_id": "513584417"
				}
				*/
				#endregion
				bool liked = ((JObject)req["page"])["liked"].Value<bool>();
				#region appData -> Dj

				string appData = "";

				try
				{
					appData = req["app_data"].Value<string>();
				}
				catch { }

				MixmagGreatestDj Dj = null;
				try
				{
					Dj = new MixmagGreatestDj(int.Parse(appData.Split('-')[0]));
				}
				catch { }

				bool skip = false;
				try
				{
				    skip = appData.Split('-')[1] == "1";
				}
				catch { }

				bool facebook = false;
				try
				{
					facebook = appData.Split('-')[2] == "1";
				}
				catch { }

				FacebookSource.Value = facebook ? "1" : "0";
				#endregion

				LikedPage.Value = liked.ToString();
				MixmagGreatestDjK.Value = Dj != null ? Dj.K.ToString() : "0";
				
				//bool notRunning = (DateTime.Now > new DateTime(2011, 12, 24) || Vars.DevEnv) && !skip;
				bool notRunning = (DateTime.Now > new DateTime(2011, 12, 24)) && !skip;

				if (Dj == null || notRunning)
				{
					NominationsLikeButtonHolder.Style["display"] = liked ? "none" : "";
					NominationsHolder.Style["display"] = liked ? "" : "none";
				}

				NotRunningPanel.Style["display"] = notRunning ? "" : "none";
				RunningPanel.Style["display"] = !notRunning ? "" : "none";
			//	if (notRunning)
			//	{
			//		NominationsPanel.Visible = false;
			//		VotePanel.Visible = false;
			//	}
				NominationsPanel.Style["display"] = Dj == null || notRunning ? "" : "none";

				VotePanel.Style["display"] = Dj != null && !notRunning ? "" : "none"; 

				VoteLikeHolder.Style["display"] = liked ? "none" : "";
				VoteButtonHolder.Style["display"] = liked ? "" : "none";
				//VoteFollowHolder.Style["display"] = liked ? "" : "none";

				if (Dj != null && !notRunning)
				{
					VoteName.InnerHtml = Dj.Name;
					VoteImg.Src = Dj.Image200Url;
					//VoteImgDiv.Style["background"] = "white url(" + Dj.Image200Url + ") repeat-x cefter top";
					//VoteFollowPrompt.InnerHtml = liked ? "Step 1 - click the Follow button:" : "Step 2 - click the Follow button:";
					string s = liked ? "Step 1 - click the Vote button:" : "Step 2 - click the Vote button:";
					VoteButtonPrompt.InnerHtml = s;
					//VoteButtonPrompt.InnerHtml = "foobar";

					VoteTweetButton.Attributes["data-url"] = "http://mixmag-greatest.com/";
					if (Dj.IsHidden)
					{
						VoteTweetButton.Attributes["data-text"] = "I voted in Mixmag's #GreatestDanceAct of all time poll. Who's yours? Vote at ";
					}
					else
					{
						VoteTweetButton.Attributes["data-text"] = "I voted " + Dj.ShortName + (Dj.TwitterName.Length > 0 ? " (@" + Dj.TwitterName + ")" : "") + " in Mixmag's #GreatestDanceAct of all time poll. Who's yours? Vote at ";
					}
					//I voted Altern-8 in Mixmag's #GreatestDanceAct of all time poll. Who's yours? Vote at http://mixmag-greatest.com/
					VoteDescriptionP.InnerHtml = Dj.Description;
					VoteVideo1.Attributes["src"] = "http://www.youtube.com/embed/" + Dj.YoutubeId;
					VoteVideo2.Attributes["src"] = "http://www.youtube.com/embed/" + Dj.YoutubeId2;
					VoteQuickSearchPh.Controls.Add(new LiteralControl(BuildQuickSearch(Dj.K, skip)));
					FacebookComments.Attributes["data-href"] = "mixmag-greatest.com/fb/" + Dj.UrlName;
					FacebookComments2.Attributes["data-href"] = "mixmag-greatest.com/fb/" + Dj.UrlName;

					EndPh.Controls.Add(new LiteralControl(Common.Settings.MixmagGreatestPollThanksPageHtml
						.Replace("{tweet}", @"<a href=""http://twitter.com/share"" target=""_blank"" class=""twitter-share-button"" data-count=""none"" data-url=""http://mixmag-greatest.com/"" data-text=""I voted " + Dj.ShortName + (Dj.TwitterName.Length > 0 ? " (@" + Dj.TwitterName + ")" : "") + @" in Mixmag's #GreatestDanceAct of all time poll. Who's yours? Vote at "">Tweet</a>")
						.Replace("{follow}", @"<a href=""http://twitter.com/mixmag"" target=""_blank"" class=""twitter-follow-button"" data-show-count=""false"">Follow @mixmag</a>")
					));
				}
				else
				{
					BuildDjs(skip, facebook);
					QuickSearchPh.Controls.Add(new LiteralControl(BuildQuickSearch(0, skip)));
				}
			}
		}


		public string url
		{
			get
			{
				return Vars.DevEnv ? "dev0.dontstayin.com" : "mixmag-greatest.com";
			}
		}
		public bool newWindow
		{
			get
			{
				return false;
			}
		}
		public string auth
		{
			get
			{
				return "";
				//return "?auth=" + Request.Form["signed_request"];
			}
		}

		public string BuildQuickSearch(int selectedK, bool skip)
		{
			StringBuilder s = new StringBuilder();


			//MixmagGreatestDjSet publicDjs = null;
			//{
			//    Query q1 = new Query();
			//    q1.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
			//    q1.QueryCondition = new And(new Q(MixmagGreatestDj.Columns.IsPublicNominated, true), new Q(MixmagGreatestDj.Columns.TotalVotes, QueryOperator.GreaterThan, 10));
			//    publicDjs = new MixmagGreatestDjSet(q1);
			//}
			MixmagGreatestDj current = null;
			if (selectedK > 0)
			{
				current = new MixmagGreatestDj(selectedK);
			}

			string skipString = "";
			if (skip)
			{
				skipString = "?s=1";
			}

			MixmagGreatestDjSet mixmagDjs = null;
			{
				Query q1 = new Query();
				q1.OrderBy = new OrderBy(MixmagGreatestDj.Columns.Name, OrderBy.OrderDirection.Ascending);
				q1.QueryCondition = 
					new And(
						new Or(
							new Q(MixmagGreatestDj.Columns.IsPublicNominated, false), 
							new Q(MixmagGreatestDj.Columns.IsPublicNominated, QueryOperator.IsNull, null), 
							new Q(MixmagGreatestDj.Columns.TotalVotes, QueryOperator.GreaterThan, 10)
						),
						new Q(MixmagGreatestDj.Columns.StealthMode, false)
					);
				mixmagDjs = new MixmagGreatestDjSet(q1);
			}

			s.Append(@"<select id=""qs1"" onchange=""top.location = $('#qs1').val();"">");
			if (selectedK == 0 || (current != null && current.IsHidden))
				s.Append(@"<option value=""/"">A-Z listing...</option>");
			foreach (MixmagGreatestDj dj in mixmagDjs)
				s.Append(@"<option value=""/" + dj.UrlName + skipString + @"""" + (dj.K == selectedK ? " selected" : "") + @">" + dj.Name + @"</option>");
			s.Append(@"</select>");

			return s.ToString();

		}

		public void BuildDjs(bool skip, bool facebook)
		{
			{
				Query q1 = new Query();
				q1.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
				q1.QueryCondition = new And(
					new Or(
						new Q(MixmagGreatestDj.Columns.IsPublicNominated, false), 
						new Q(MixmagGreatestDj.Columns.IsPublicNominated, QueryOperator.IsNull, null)
					),
					new Q(MixmagGreatestDj.Columns.StealthMode, false)
				);
				MixmagGreatestDjSet djs = new MixmagGreatestDjSet(q1);
				DjsPh.Controls.Add(new LiteralControl(getDjHtml(djs, skip, facebook)));
			}
			//HtmlRenderer h = new HtmlRenderer();
			//h.RenderFlashTagsRaw = true;

			{
				Query q1 = new Query();
				q1.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
				q1.QueryCondition = new And(
					new Q(MixmagGreatestDj.Columns.IsPublicNominated, true), 
					new Q(MixmagGreatestDj.Columns.TotalVotes, QueryOperator.GreaterThan, 10),
					new Q(MixmagGreatestDj.Columns.StealthMode, false)
				);
				MixmagGreatestDjSet djs = new MixmagGreatestDjSet(q1);
				if (djs.Count == 0)
					PublicDjsHolder.Visible = false;
				else
					PublicDjsPh.Controls.Add(new LiteralControl(getDjHtml(djs, skip, facebook)));
			}


		}

		string getDjHtml(MixmagGreatestDjSet djs, bool skip, bool facebook)
		{
			StringBuilder sbDjs = new StringBuilder();
			int count = 0;
			foreach (MixmagGreatestDj dj in djs)
			{
				count++;

				string urlFull = "http://" + url + "/" + dj.UrlName + (skip || facebook ? "?" : "") + (skip ? "s=1" : "") + (skip && facebook ? "&" : "") + (facebook ? "fb=1" : "");
				string target = newWindow ? " target=\"_blank\"" : "";

				sbDjs.Append(@"
				<div class=""BoxOuter" + (count % 3 == 0 ? "End" : "") + @""">
					<a href=""" + urlFull + @"""" + target + @" class=""BoxInnerLink NoStyle"" target=""_top"">
						<img src=""" + dj.Image160Url + @""" width=""160"" height=""160"" border=""0"" class=""rounded-corners"" />
					</a>
					<a href=""" + urlFull + @"""" + target + @" class=""NoStyle"" target=""_top"">
						<div class=""BoxInnerHeader""><span class=""BoxInnerHeaderWord"">" + dj.Name.ToUpper().Replace(" ", @"</span><span class=""BoxInnerHeaderWord""> ") + @" </span></div>
					</a>
				</div>
				");

				if (count % 3 == 0)
					sbDjs.Append(@"<div style=""clear: both;""></div>");

				//sbDjs.Append(dj.Name);
				//sbDjs.Append("<BR />");
				//sbDjs.Append(dj.ShortDescription);
				//sbDjs.Append("<BR />");

			}
			return sbDjs.ToString();
		}
		#region SetCookie
		[Client]
		public static Hashtable SetCookie(string signedRequest)
		{
			//JObject j = JObject.Parse(json);
			//string cookie = "access_token=" + HttpUtility.UrlEncode(j["access_token"].Value<string>());
			//cookie += "&base_domain=" + HttpUtility.UrlEncode(j["base_domain"].Value<string>());
			//cookie += "&expires=" + HttpUtility.UrlEncode(j["expires"].Value<string>());
			//cookie += "&secret=" + HttpUtility.UrlEncode(j["secret"].Value<string>());
			//cookie += "&session_key=" + HttpUtility.UrlEncode(j["session_key"].Value<string>());
			//cookie += "&sig=" + HttpUtility.UrlEncode(j["sig"].Value<string>());
			//cookie += "&uid=" + HttpUtility.UrlEncode(j["uid"].Value<string>());
			//Cambro.Web.Helpers.SetCookie("fbs_" + Facebook.FacebookCommon.Common(Apps.MixmagGreatest).ApiKey, "\"" + cookie + "\"", true);

			Cambro.Web.Helpers.SetCookie("fbsr_" + Facebook.FacebookCommon.Common(Apps.MixmagGreatest).AppId, signedRequest, true);

			Hashtable ret = new Hashtable();
			ret["Done"] = true;
			return ret;
		}
		#endregion
		#region VoteNow
		[Client]
		public static Hashtable VoteNow(int mixmagGreatestDjK, string facebookSource, bool facebookMessage)
		{
			try
			{
				var facebook = new FacebookGraphAPI(Facebook.Apps.MixmagGreatest);
				JObject user = facebook.GetObject("me", null);

				//string email = user.Value<string>("email");
				string firstName = user.Value<string>("first_name");
				string lastName = user.Value<string>("last_name");


				Hashtable ret = new Hashtable();

				if (DateTime.Now > new DateTime(2011, 12, 24))
				{
					ret["Done"] = false;
					ret["Message"] = "Sorry, voting closed at midnight on 23rd December 2011.";
					return ret;
				}


				
				MixmagGreatestDj dj = new MixmagGreatestDj(mixmagGreatestDjK);



				MixmagGreatestVote mgv = new MixmagGreatestVote();
				mgv.DateTime = DateTime.Now;
				mgv.DidWallPost = false;
				mgv.EmailPermission = true;
				//mgv.FacebookEmail = email;
				mgv.FacebookUid = facebook.Uid;
				mgv.MixmagGreatestDjK = dj.K;
				mgv.WallPostPermission = true;
				mgv.FacebookSource = facebookSource == "1";
				try
				{
					if (!Vars.DevEnv && DateTime.Now > new DateTime(2011, 09, 15, 11, 0, 0))
						mgv.Update();

				}
				catch (Exception ex)
				{
					MixmagGreatestVoteSet voteSet = new MixmagGreatestVoteSet(new Query(new Q(MixmagGreatestVote.Columns.FacebookUid, facebook.Uid)));
					if (voteSet.Count > 0)
					{
						MixmagGreatestDj votedFor = new MixmagGreatestDj(voteSet[0].MixmagGreatestDjK);
						ret["Done"] = false;
						ret["Message"] = "You already voted for " + votedFor.Name + ". Sorry - you can only vote once.";
						return ret;
					}
					else
					{
						ret["Done"] = false;
						ret["Message"] = "There's a problem voting: " + ex.ToString();
						return ret;
					}
				}

				try
				{

					Update u = new Update();
					u.Table = TablesEnum.MixmagGreatestDj;
					u.Changes.Add(new Assign.Increment(MixmagGreatestDj.Columns.TotalVotes));
					u.Where = new Q(MixmagGreatestDj.Columns.K, dj.K);
					u.Run();
				}
				catch { }

				//FacebookHttpContext.Current.Status.Set(dj.Name + " is my greatest DJ of all time. Click to vote for yours in the Mixmag poll: http://greatest.dj/");

				if (facebookMessage)
				{
					try
					{
						//send facebook message
						//http://developers.facebook.com/docs/reference/api/post
						Dictionary<string, object> par = new Dictionary<string, object>();
						par["picture"] = dj.Image90Url;
						par["link"] = "http://mixmag-greatest.com/?fb=1";
						par["name"] = "Mixmag Greatest Dance Act";
						if (dj.IsHidden)
						{
							par["description"] = "I voted for my greatest dance act of all time. Vote for yours now in the Mixmag poll...";
						}
						else
						{
							par["caption"] = dj.Name;
							par["description"] = "I voted " + dj.ShortName + " the greatest dance act of all time. Vote for yours now in the Mixmag poll...";
						}
						facebook.PutWallPost("", par);

						mgv.DidWallPost = true;
						mgv.Update();
					}
					catch { }
				}

				ret["Done"] = true;



				return ret;
			}
			catch (Exception ex)
			{
				Hashtable ret = new Hashtable();
				ret["Done"] = false;
				ret["Message"] = ex.ToString();
				return ret;
			}
		}
		#endregion

		
	}
}
