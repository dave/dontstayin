using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Text;
using Bobs;
using Bobs.Jobs;

namespace Spotted.Support
{
	public partial class DbButtonServer : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			//Cambro.Misc.Utility.Beep(3000,100);
			Response.ContentType = "text/xml";
			try
			{
				HttpContext.Current.Items["PageCobK"] = 999;
				HttpContext.Current.Items["PageCustPage"] = "BUTTON refresh done";

				XmlDocument requestXml = new XmlDocument();
				requestXml.Load(Request.InputStream);
				XmlNode docNode = requestXml.DocumentElement;

				if (Usr.Current == null)
				{
					Response.Write("<doc l=\"1\"></doc>");
					return;
				}

				bool state = false;
				string pagePath = "";
				string functionName = "";
				string functionArgs = "";


				#region get vars from request
				try
				{
					string stateString = docNode.Attributes["s"].Value;
					if (stateString.Equals("1"))
						state = true;
				}
				catch { }
				try
				{
					functionName = docNode.Attributes["f"].Value;
				}
				catch { }
				try
				{
					functionArgs = docNode.Attributes["a"].Value;
				}
				catch { }
				try
				{
					pagePath = docNode.Attributes["p"].Value;
				}
				catch { }
				#endregion
				
				#region WatchTopic
				if (functionName.Equals("WatchTopic"))
				{
                    WatchTopic(state, functionArgs);
                    //Thread t = new Thread(int.Parse(functionArgs));
                    //bool changed = false;
                    //if (t.CheckPermissionRead(Usr.Current))
                    //{
                    //    ThreadUsr tu = t.GetThreadUsr(Usr.Current);
                    //    if (state)
                    //    {
                    //        if (!tu.IsWatching)
                    //        {
                    //            tu.ChangeStatus(ThreadUsr.StatusEnum.Archived, true);
                    //            changed = true;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        tu.ChangeStatus(ThreadUsr.StatusEnum.Ignore, true);
                    //        changed = true;
                    //    }
                    //    if (changed)
                    //        new System.Threading.Thread(new System.Threading.ThreadStart(t.UpdateTotalParticipants)).Start();
                    //    SendResponse(tu.IsWatching);
                    //    return;
                    //}
                    //else
                    //{
                    //    throw new Exception("You don't have permission to watch this thread!");
                    //}
				}
				#endregion
				#region FavouriteTopic
				else if (functionName.Equals("FavouriteTopic"))
				{
                    FavouriteTopic(state, functionArgs);
                    //Thread t = new Thread(int.Parse(functionArgs));
                    //if (t.CheckPermissionRead(Usr.Current))
                    //{
                    //    ThreadUsr tu = t.GetThreadUsr(Usr.Current);
                    //    if (state)
                    //    {
                    //        if (!tu.Favourite)
                    //        {
                    //            tu.Favourite = true;
                    //            tu.Update();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (tu.Favourite)
                    //        {
                    //            tu.Favourite = false;
                    //            tu.Update();
                    //        }
                    //    }
                    //    SendResponse(tu.Favourite);
                    //    return;
                    //}
                    //else
                    //{
                    //    throw new Exception("You don't have permission to watch this thread!");
                    //}
				}
				#endregion
				#region CommentAlert
				else if (functionName.Equals("CommentAlert"))
				{
					string[] args = functionArgs.Split(',');
					int objectK = int.Parse(args[0]);
					Model.Entities.ObjectType parentType = (Model.Entities.ObjectType)int.Parse(args[1]);

					if (state)
					{
						if (parentType.Equals(Model.Entities.ObjectType.Article) ||
							parentType.Equals(Model.Entities.ObjectType.Brand) ||
							parentType.Equals(Model.Entities.ObjectType.Photo) ||
							parentType.Equals(Model.Entities.ObjectType.Event) ||
							parentType.Equals(Model.Entities.ObjectType.Venue) ||
							parentType.Equals(Model.Entities.ObjectType.Place) ||
							parentType.Equals(Model.Entities.ObjectType.Group))
						{
							if (parentType.Equals(Model.Entities.ObjectType.Group))
							{
								Bobs.Group g = new Bobs.Group(objectK);
								GroupUsr gu = g.GetGroupUsr(Usr.Current);
								if (!Usr.Current.CanGroupRead(g, gu))
									throw new DsiUserFriendlyException("You don't have permission to view this group.");
							}
							else
							{
								var b = Bob.Get(parentType, objectK);
								if (b == null)
									throw new DsiUserFriendlyException("Can't find forum.");
							}
							CommentAlert.Enable(Usr.Current, objectK, parentType);
							SendResponse(true);
							return;
						}
						else
						{
							throw new Exception("Unsupported parent type");
						}
					}
					else
					{
						CommentAlert.Disable(Usr.Current, objectK, parentType);
						SendResponse(false);
						return;
					}
				}
				#endregion
				#region FavouriteGroup
				else if (functionName.Equals("FavouriteGroup"))
				{
					Bobs.Group g = new Bobs.Group(int.Parse(functionArgs));
					GroupUsr gu = g.GetGroupUsr(Usr.Current);
					if (gu == null || !gu.IsMember)
						throw new DsiUserFriendlyException("You're not a member of this group!");

					if (state)
					{
						if (!gu.Favourite)
						{
							gu.Favourite = true;
							gu.Update();
						}
					}
					else
					{
						if (gu.Favourite)
						{
							gu.Favourite = false;
							gu.Update();
						}
					}
					SendResponse(gu.Favourite);
					return;
				}
				#endregion
				#region InboxTopic
				if (functionName.Equals("InboxTopic"))
				{
                    InboxTopic(state, functionArgs);
                    //Thread t = new Thread(int.Parse(functionArgs));
                    //bool changed = false;
                    //if (t.CheckPermissionRead(Usr.Current))
                    //{
                    //    ThreadUsr tu = t.GetThreadUsr(Usr.Current);
                    //    if (state)
                    //    {
                    //        if (!tu.IsInbox)
                    //        {
                    //            tu.ChangeStatus(ThreadUsr.StatusEnum.UnArchived, true);
                    //            changed = true;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (tu.IsInbox)
                    //        {
                    //            tu.ChangeStatus(ThreadUsr.StatusEnum.Archived, true);
                    //            changed = true;
                    //        }
                    //    }
                    //    if (changed)
                    //        new System.Threading.Thread(new System.Threading.ThreadStart(t.UpdateTotalParticipants)).Start();
                    //    SendResponse(tu.IsInbox);
                    //    return;
                    //}
                    //else
                    //{
                    //    throw new Exception("You don't have permission to access this thread!");
                    //}
				}
				#endregion
				#region UsrPhotoMe
				if (functionName.Equals("UsrPhotoMe"))
				{
					Photo p = new Photo(int.Parse(functionArgs));
					if (p.Validate())
					{
						Usr.Current.PhotoMe(p, state, null);

						//	if (p.EventK>0 && p.Event!=null && state)
						//	{
						//		Usr.Current.AttendEvent(p.EventK, true, null);
						//	}

						if (state)
						{
							if (p.ThreadK > 0 && p.Thread != null)
								CommentAlert.Enable(Usr.Current, p.ThreadK ?? 0, Model.Entities.ObjectType.Thread);
							else
								CommentAlert.Enable(Usr.Current, p.K, Model.Entities.ObjectType.Photo);
						}

						if (state)
						{
							if (!Usr.Current.HasPic)
							{
								SendRedirect("/pages/mypicture");
								return;
							}
						}

						SendCode("UsrPhotoMeReturn(" + (state ? "1" : "0") + ",\"" + Cambro.Misc.Utility.JsStringEncode(p.UsrHtml) + "\");");
						//SendResponse(state);
						return;
					}
					else
					{
						throw new DsiUserFriendlyException("You don't have permission to add yourself to this photo!");
					}
				}
				#endregion
				#region UsrPhotoFavourite
				if (functionName.Equals("UsrPhotoFavourite"))
				{
					Photo p = new Photo(int.Parse(functionArgs));
					if (p.Validate())
					{
						try
						{
							UsrPhotoFavourite u = new UsrPhotoFavourite(Usr.Current.K, p.K);
							if (!state)
							{
								u.Delete();
								u.Update();
							}
						}
						catch
						{
							if (state)
							{
								UsrPhotoFavourite newU = new UsrPhotoFavourite();
								newU.UsrK = Usr.Current.K;
								newU.PhotoK = p.K;
								newU.Update();

								if (Usr.Current.FacebookConnected && Usr.Current.FacebookStoryFavourite)
								{
									FacebookPost.CreateFavouritePhoto(Usr.Current, p);
								}
							}
						}
						if (state)
						{
							if (p.ThreadK > 0 && p.Thread != null)
								CommentAlert.Enable(Usr.Current, p.ThreadK ?? 0, Model.Entities.ObjectType.Thread);
							else
								CommentAlert.Enable(Usr.Current, p.K, Model.Entities.ObjectType.Photo);
						}
						SendResponse(state);
					}
					else
					{
						throw new DsiUserFriendlyException("You don't have permission to add this photo to your favourites!");
					}
				}
				#endregion
				#region PhotoWatch
				if (functionName.Equals("PhotoWatch"))
				{
					Photo p = new Photo(int.Parse(functionArgs));
					if (p.Validate())
					{
						if (p.ThreadK.IsNullOrZero())
						{
							if (state)
								CommentAlert.Enable(Usr.Current, p.K, Model.Entities.ObjectType.Photo);
							else
								CommentAlert.Disable(Usr.Current, p.K, Model.Entities.ObjectType.Photo);
						}
						else
						{
							CommentAlert.Disable(Usr.Current, p.K, Model.Entities.ObjectType.Photo);
							if (state)
								CommentAlert.Enable(Usr.Current, p.ThreadK ?? 0, Model.Entities.ObjectType.Thread);
							else
								CommentAlert.Disable(Usr.Current, p.ThreadK ?? 0, Model.Entities.ObjectType.Thread);
						}
						SendResponse(state);
					}
					else
					{
						throw new DsiUserFriendlyException("You don't have permission to watch for comments on this photo!");
					}
				}
				#endregion
				#region SingleThreadBobWatch
				if (functionName.Equals("SingleThreadBobWatch"))
				{
					Model.Entities.ObjectType type = (Model.Entities.ObjectType)int.Parse(functionArgs.Substring(0, functionArgs.IndexOf(',')));
					int k = int.Parse(functionArgs.Substring(functionArgs.IndexOf(',')+1));
					var b = Bob.Get(type, k);
					ICanView CanViewBob = (ICanView)b;
					IHasPrimaryThread HasSingleThreadBob = (IHasPrimaryThread)b;

					if (CanViewBob.CanView(Usr.Current))
					{
						if (HasSingleThreadBob.ThreadK.IsNullOrZero())
						{
							if (state)
								CommentAlert.Enable(Usr.Current, k, type);
							else
								CommentAlert.Disable(Usr.Current, k, type);
						}
						else
						{
							CommentAlert.Disable(Usr.Current, k, type);
							if (state)
								CommentAlert.Enable(Usr.Current, HasSingleThreadBob.ThreadK ?? 0, Model.Entities.ObjectType.Thread);
							else
								CommentAlert.Disable(Usr.Current, HasSingleThreadBob.ThreadK ?? 0, Model.Entities.ObjectType.Thread);
						}
						SendResponse(state);
					}
					else
					{
						throw new DsiUserFriendlyException("You don't have permission to watch for comments on this object!");
					}
				}
				#endregion
				#region QuickUsrPhotoMe
				if (functionName.Equals("QuickUsrPhotoMe"))
				{
					Photo p = new Photo(int.Parse(functionArgs));
					if (p.Validate())
					{
						Usr.Current.PhotoMe(p, state, null);

						//						if (p.EventK>0 && p.Event!=null && state)
						//						{
						//							Usr.Current.AttendEvent(p.EventK, true, null);
						//						}

						if (state)
						{
							if (p.ThreadK > 0 && p.Thread != null)
								CommentAlert.Enable(Usr.Current, p.ThreadK ?? 0, Model.Entities.ObjectType.Thread);
							else
								CommentAlert.Enable(Usr.Current, p.K, Model.Entities.ObjectType.Photo);
						}

						if (state)
						{
							if (!Usr.Current.HasPic)
							{
								SendRedirect("/pages/mypicture");
								return;
							}
						}

						SendCode("QuickUsrPhotoMeReturn(" + (state ? "1" : "0") + ",\"" + Cambro.Misc.Utility.JsStringEncode(p.UsrHtml) + "\"," + p.K.ToString() + ");");
						//SendResponse(state);
						return;
					}
					else
					{
						throw new DsiUserFriendlyException("You don't have permission to add yourself to this photo!");
					}
				}
				#endregion
				#region Buddy
				if (functionName.Equals("Buddy"))
				{
					if (state)
					{
						//check for bot condition only when adding a buddy.
						if (Usr.CheckForSpamBot(true))
						{
							SendRedirect("/popup/captcha?url=" + Server.UrlEncode(pagePath));
							return;
						}
					}
					Buddy(state, getBuddyK(functionArgs), getUsrFoundByMethod(functionArgs));
					SendResponse(state);
				}
				#endregion
				#region BuddyChatInvite
				if (functionName.Equals("BuddyChatInvite"))
				{
					BuddyInvite(state, getBuddyK(functionArgs), getUsrFoundByMethod(functionArgs));
					SendResponse(state);
				}
				#endregion
				#region BuddyDeny
				if (functionName.Equals("BuddyDeny"))
				{
					BuddyDeny(state, int.Parse(functionArgs));
					SendResponse(state);
				}
				#endregion
				#region MultiBuddy
				if (functionName.Equals("MultiBuddy"))
				{
					foreach (string s in functionArgs.Split(','))
					{
						// currently the only MultiBuddy features are from other users requesting you
						Buddy(state, int.Parse(s), Bobs.Buddy.BuddyFindingMethod.Nickname);
					}
					SendResponse(state);
				}
				#endregion
				#region MultiBuddyChatInvite
				if (functionName.Equals("MultiBuddyChatInvite"))
				{
					foreach (string s in functionArgs.Split(','))
					{
						// currently the only MultiBuddy features are from other users requesting you
						BuddyInvite(state, int.Parse(s), Bobs.Buddy.BuddyFindingMethod.Nickname);
					}
					SendResponse(state);
				}
				#endregion
				#region MultiBuddyDeny
				if (functionName.Equals("MultiBuddyDeny"))
				{
					foreach (string s in functionArgs.Split(','))
						BuddyDeny(state, int.Parse(s));
					SendResponse(state);
				}
				#endregion
				#region News
				if (functionName.Equals("MakeNews"))
				{
					if (!Usr.Current.CanNewsModerator())
						throw new DsiUserFriendlyException("You can't moderate news!");

					string[] argAry = functionArgs.Split(',');
					Thread thread = new Thread(int.Parse(argAry[0]));
					int level = int.Parse(argAry[1]);
					if (level < 0)
						level = 0;
					if (level > 60)
						level = 60;

					if (level == 0)
					{
						SendCode("alert(\"First select a news level!\");", false);
					}
					else if (state)
					{
						thread.EnableNews(level);
						SendResponse(thread.IsNews && thread.NewsStatus.Equals(Thread.NewsStatusEnum.Done));
					}
					else
					{
						SendCode("alert(\"NOTHING UPDATED! - to change the news level, first disable, then re-enable!\");");
					}
				}
				if (functionName.Equals("DisableNews"))
				{
					if (!Usr.Current.CanNewsModerator())
						throw new DsiUserFriendlyException("You can't moderate news!");

					Thread thread = new Thread(int.Parse(functionArgs));

					if (state)
					{
						thread.DisableNews();
					}
					else
					{
					}
					SendResponse(!thread.IsNews && thread.NewsStatus.Equals(Thread.NewsStatusEnum.Done));

				}
				#endregion
				#region GroupNews
				if (functionName.Equals("MakeGroupNews"))
				{
					Thread thread = new Thread(int.Parse(functionArgs));

					if (thread.GroupK == 0)
						throw new DsiUserFriendlyException("This thread isn't group news!");

					GroupUsr gu = thread.Group.GetGroupUsr(Usr.Current);

					if (!Usr.Current.CanGroupNewsAdmin(gu))
						throw new DsiUserFriendlyException("You can't moderate this group news!");


					if (state)
					{
						thread.EnableNews(10);

					}
					else
					{
						SendCode("alert(\"Whoops - you've already enabled this news item. To disable it, click the cross button.\");");
					}
					SendResponse(thread.IsNews && thread.NewsStatus.Equals(Thread.NewsStatusEnum.Done));

				}
				if (functionName.Equals("DisableGroupNews"))
				{
					Thread thread = new Thread(int.Parse(functionArgs));

					if (thread.GroupK == 0)
						throw new Exception("This thread isn't group news!");

					GroupUsr gu = thread.Group.GetGroupUsr(Usr.Current);

					if (!Usr.Current.CanGroupNewsAdmin(gu))
						throw new Exception("You can't moderate this group news!");


					if (state)
					{
						thread.DisableNews();
					}
					else
					{
						SendCode("alert(\"Whoops - you've already disabled this news item. To enable it, click the tick button.\");");
					}
					SendResponse(!thread.IsNews && thread.NewsStatus.Equals(Thread.NewsStatusEnum.Done));

				}
				#endregion
			}
			catch (Exception ex)
			{
				SpottedException.TryToSaveExceptionAndChildExceptions(ex, HttpContext.Current, Usr.Current, Visit.HasCurrent ? Visit.Current : null,
					"", "DbButtonServer", "", 0, null);
				HttpContext.Current.Items["PageCobK"] = 999;
				HttpContext.Current.Items["PageCustPage"] = "BUTTON refresh exception";
				StringBuilder sb = new StringBuilder();
				sb.Append("<doc ex=\"");
				sb.Append(HttpUtility.HtmlEncode(ex.Message));
				sb.Append("\"></doc>");
				Response.Write(sb.ToString());
			}
		}

		#region FunctionArgs
		private int getBuddyK(string functionArgs)
		{
			string[] functionArgsSplit = functionArgs.Split(',');
			return int.Parse(functionArgsSplit[0]);
		}

		private Buddy.BuddyFindingMethod getUsrFoundByMethod(string functionArgs)
		{
			string[] functionArgsSplit = functionArgs.Split(',');
			if (functionArgsSplit.Length > 1)
				return (Buddy.BuddyFindingMethod)int.Parse(functionArgsSplit[1]);
			else
				return Bobs.Buddy.BuddyFindingMethod.Nickname;
		}
		#endregion

		#region Functions
		private string[] GetIds(string functionArgs)
        {
            if (functionArgs.Contains("_"))
                return functionArgs.Split('_');
            else
                return new string[] { functionArgs };            
        }
        #region InboxTopic
        public void InboxTopic(bool state, string functionArgs)
        {
            string[] ids = GetIds(functionArgs);

            bool response = false;

            foreach (string id in ids)
            {
                Thread t = new Thread(int.Parse(id));
                bool changed = false;
                if (t.CheckPermissionRead(Usr.Current))
                {
                    ThreadUsr tu = t.GetThreadUsr(Usr.Current);
                    if (state)
                    {
                        if (!tu.IsInbox)
                        {
                            tu.ChangeStatus(ThreadUsr.StatusEnum.UnArchived, true);
                            changed = true;
                        }
                    }
                    else
                    {
                        if (tu.IsInbox)
                        {
                            tu.ChangeStatus(ThreadUsr.StatusEnum.Archived, true);
                            changed = true;
                        }
                    }
					if (changed)
					{
						UpdateTotalParticipantsJob job = new UpdateTotalParticipantsJob(t);
						job.ExecuteAsynchronously();
					}
                    response = tu.IsInbox;
                }
                else
                {
					throw new DsiUserFriendlyException("You don't have permission to access this thread!");
                }
            }
            SendResponse(response);
            return;
        }
        #endregion
        #region WatchTopic
        public void WatchTopic(bool state, string functionArgs)
        {
            string[] ids = GetIds(functionArgs);

            bool response = false;

            foreach (string id in ids)
            {
                Thread t = new Thread(int.Parse(id));
                bool changed = false;
                if (t.CheckPermissionRead(Usr.Current))
                {
                    ThreadUsr tu = t.GetThreadUsr(Usr.Current);
                    if (state)
                    {
                        if (!tu.IsWatching)
                        {
                            tu.ChangeStatus(ThreadUsr.StatusEnum.Archived, true);
                            changed = true;
                        }
                    }
                    else
                    {
                        tu.ChangeStatus(ThreadUsr.StatusEnum.Ignore, true);
                        changed = true;
                    }
					if (changed)
					{
						UpdateTotalParticipantsJob job = new UpdateTotalParticipantsJob(t);
						job.ExecuteAsynchronously();
					}
                    response = tu.IsWatching;
                }
                else
                {
					throw new DsiUserFriendlyException("You don't have permission to watch this thread!");
                }
            }
            SendResponse(response);
            return;
        }
        #endregion
        #region FavouriteTopic
        public void FavouriteTopic(bool state, string functionArgs)
        {
            string[] ids = GetIds(functionArgs);

            bool response = false;

            foreach (string id in ids)
            {
                Thread t = new Thread(int.Parse(id));
                if (t.CheckPermissionRead(Usr.Current))
                {
                    ThreadUsr tu = t.GetThreadUsr(Usr.Current);
                    if (state)
                    {
                        if (!tu.Favourite)
                        {
                            tu.Favourite = true;
                            tu.Update();

							if (!t.Private && !t.GroupPrivate && !t.PrivateGroup && Usr.Current.FacebookConnected && Usr.Current.FacebookStoryFavouriteTopic)
							{
								FacebookPost.CreateFavouriteTopic(Usr.Current, t);
							}
                        }
                    }
                    else
                    {
                        if (tu.Favourite)
                        {
                            tu.Favourite = false;
                            tu.Update();
                        }
                    }
                    response = tu.Favourite;
                }
                else
                {
					throw new DsiUserFriendlyException("You don't have permission to watch this thread!");
                }
            }
            SendResponse(response);
            return;
        }
        #endregion
		#region Buddy
		private static void Buddy(bool state, int buddyK, Buddy.BuddyFindingMethod foundByMethod)
		{
			Usr u = new Usr(buddyK);

			if (state)
				Usr.Current.AddBuddy(u, Usr.AddBuddySource.BuddyButtonClick, foundByMethod, null);
			else
				Usr.Current.RemoveBuddy(u.K);
		}
		#endregion
		#region BuddyInvite
		private void BuddyInvite(bool state, int buddyK, Buddy.BuddyFindingMethod usrFoundByMethod)
		{
			Usr.Current.SetBuddyInvite(new Usr(buddyK), state, Usr.AddBuddySource.BuddyButtonClick, usrFoundByMethod, null);
		}
		#endregion
		#region BuddyDeny
		private void BuddyDeny(bool state, int buddyK)
		{
			Usr.Current.DenyBuddy(buddyK, state);
		}
		#endregion
		#endregion


		public void SendResponse(bool state)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<doc s=\"");
			sb.Append(state ? "1" : "0");
			sb.Append("\"></doc>");
			Response.Write(sb.ToString());
		}
		public void SendRedirect(string url)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<doc r=\"");
			sb.Append(Cambro.Misc.Utility.XmlAttributeEncode(url));
			sb.Append("\"></doc>");
			Response.Write(sb.ToString());
		}
		public void SendCode(string code)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<doc e=\"");
			sb.Append(Cambro.Misc.Utility.XmlAttributeEncode(code));
			sb.Append("\"></doc>");
			Response.Write(sb.ToString());
		}
		public void SendCode(string code, bool state)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<doc e=\"");
			sb.Append(Cambro.Misc.Utility.XmlAttributeEncode(code));
			sb.Append("\" s=\"");
			sb.Append(state ? "1" : "0");
			sb.Append("\"></doc>");
			Response.Write(sb.ToString());
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
