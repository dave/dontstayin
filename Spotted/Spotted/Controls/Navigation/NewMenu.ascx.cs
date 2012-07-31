using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using System.Web.Security;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Spotted.Controls.Navigation
{
	public partial class NewMenu : System.Web.UI.UserControl
	{
		public Spotted.Controls.Navigation.Admin Admin;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Usr.Current != null)
			{
				#region Home country
				{
					if (Country.Current != null)
					{
						HomeCountryLink.HRef = Country.Current.Url();
						HomeCountryLink.InnerHtml = Country.Current.FriendlyName;
						HomeCountryLabel.Text = Country.Current.FriendlyName;
					}
				}
				#endregion

				#region Favourite groups
				{
					Query q = new Query();
					q.TableElement = new Join(
						new TableElement(TablesEnum.Group),
						new TableElement(TablesEnum.GroupUsr),
						QueryJoinType.Inner,
						new And(
							new Q(Group.Columns.K, GroupUsr.Columns.GroupK, true),
							new Q(GroupUsr.Columns.Favourite, true),
							new Q(GroupUsr.Columns.UsrK, Usr.Current.K),
							new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)
						)
					);
					q.Columns = new ColumnSet(Group.Columns.UrlName, Group.Columns.Name);
					q.OrderBy = new OrderBy(Group.Columns.Name);
					GroupSet gs = new GroupSet(q);
					if (gs.Count > 0)
					{
						StringBuilder sb = new StringBuilder();
						foreach (Group g in gs)
						{
							sb.Append(string.Format("{0}<a href=\"{1}\">{2}</a>", sb.Length == 0 ? "" : ", ", g.Url(), g.Name));
						}
						FavouriteGroupsPlaceholder.Controls.Add(new LiteralControl(sb.ToString()));
						FavouriteGroupsHolder.Visible = true;
						NoFavouriteGroupsHolder.Visible = false;
					}
					else
					{
						FavouriteGroupsHolder.Visible = false;
						NoFavouriteGroupsHolder.Visible = true;
					}
				}
				#endregion

				#region Places I visit
				{
					Query q = new Query();
					q.TableElement = new Join(
						new TableElement(TablesEnum.Place),
						new TableElement(TablesEnum.UsrPlaceVisit),
						QueryJoinType.Inner,
						new And(
							new Q(Place.Columns.K, UsrPlaceVisit.Columns.PlaceK, true),
							new Q(UsrPlaceVisit.Columns.UsrK, Usr.Current.K)
						)
					);
					q.Columns = new ColumnSet(Place.Columns.UrlName, Place.Columns.Name, Place.Columns.RegionAbbreviation, Place.Columns.UrlFragment);
					q.OrderBy = new OrderBy(Place.Columns.Name);
					PlaceSet ps = new PlaceSet(q);
					if (ps.Count > 0)
					{
						StringBuilder sb = new StringBuilder();
						foreach (Place p in ps)
						{
							sb.Append(string.Format("{0}<a href=\"{1}\">{2}</a>", sb.Length == 0 ? "" : ", ", p.Url(), p.NamePlain));
						}
						PlacesPlaceholder.Controls.Add(new LiteralControl(sb.ToString()));
					}
					else
					{
						PlacesHolder.Visible = false;
					}
				}
				#endregion

				#region Calendars
				{
					MyCalendarLink.HRef = "/pages/mycalendar#Day" + DateTime.Now.ToString("yyyyMMdd");
					BuddyCalendarLink.HRef = "/pages/mycalendar/type-buddy#Day" + DateTime.Now.ToString("yyyyMMdd");
				}
				#endregion

				#region My comments
				{
					MyCommentsLink.HRef = Usr.Current.UrlApp("chat");
				}
				#endregion

				#region My tickets
				{
					MyTicketsLink.HRef = Usr.Current.UrlApp("mytickets");
				}
				#endregion

				#region Online buddies
				{
					Query q = new Query();
					q.TableElement = Usr.BuddyJoin;
					q.QueryCondition = new And(
						new Q(Buddy.Columns.UsrK, Usr.Current.K),
						Usr.LoggedIn30MinIncludeNonEmailVerifiedQ);
					q.OrderBy = new OrderBy(Usr.Columns.NickName);
					UsrSet us = new UsrSet(q);
					StringBuilder sb = new StringBuilder();
					foreach (Usr u in us)
					{
						sb.Append(sb.Length == 0 ? "" : ", ");
						u.LinkAppend(sb, false);
					}
					OnlineBuddiesPlaceholder.Controls.Add(new LiteralControl(sb.ToString()));
				}
				{
					Query q = new Query();
					q.QueryCondition = Usr.LoggedIn30MinIncludeNonEmailVerifiedQ;
					q.CacheDuration = TimeSpan.FromMinutes(15);
					q.ReturnCountOnly = true;
					UsrSet us = new UsrSet(q);
					NumberOnlineLabel.Text = us.Count.ToString("#,##0");
				}
				#endregion

				#region Favourite photos
				//{
				//    Query q = new Query();
				//    q.TableElement = Photo.UsrFavouritesJoin;
				//    q.OrderBy = new OrderBy(UsrPhotoFavourite.Columns.DateTimeAdded, OrderBy.OrderDirection.Descending);
				//    q.TopRecords = 7;
				//    q.QueryCondition = new Q(UsrPhotoFavourite.Columns.UsrK, Usr.Current.K);
				//    PhotoSet ps = new PhotoSet(q);
				//    StringBuilder sb = new StringBuilder();
				//    foreach (Photo p in ps)
				//    {
				//        sb.Append(string.Format("<a href=\"{0}\"><img src=\"{1}\" border=\"0\" width=\"50\" height=\"50\" class=\"BorderBlack All\" /></a> ", p.Url(), p.IconPath));
				//    }
				//    FavouritePhotosPlaceholder.Controls.Add(new LiteralControl(sb.ToString()));

				//    FavouritePhotosLink.HRef = Usr.Current.UrlApp("favouritephotos");
				//}
				#endregion
			}
			
		}
		protected HtmlAnchor LogInLink, SignUpLink, LogOutLink;
		protected void Page_PreRender(object sender, EventArgs e)
		{
			MenuLink.Visible = Usr.Current != null;
			BigMenu.Visible = Usr.Current != null;
			QuickLinksLinkHolder.Visible = Usr.Current != null;

			LogInLink.Visible = Usr.Current == null;
			SignUpLink.Visible = Usr.Current == null;
			LogOutLink.Visible = Usr.Current != null;

			//TopBarLogInLinkHolder.Visible = Usr.Current == null;
			//TopBarSignUpLinkHolder.Visible = Usr.Current == null;
			//TopBarLogOutLinkHolder.Visible = Usr.Current != null;
			//TopBarMyAccountLinkHolder.Visible = Usr.Current != null;
			TobBarProfileLinkHolder.Visible = Usr.Current != null;
			if (Usr.Current != null)
			{
				TobBarProfileLink.HRef = Usr.Current.Url();
				TobBarProfileLink.InnerHtml = Usr.Current.NickName;
				Usr.Current.MakeRollover(TobBarProfileLink);
			}
			else
			{
				TopBarInboxLink.InnerHtml = "Chat";
				TopBarInboxLink.HRef = "/chat";
			}

			if (Vars.FacebookCanvasMode)
			{
				OuterDiv.Style["width"] = "755px";
				BigMenu.Style["width"] = "715px";
				BigMenuDiv1.Style["width"] = "350px";
				BigMenuDiv2.Style["width"] = "350px";
				QuickLinksLinkHolder.Style["left"] = "290px";
			}
		}
		#region LogOutClick
		public void LogOutClick(object o, System.EventArgs e)
		{
			if (Usr.Current != null)
			{
				Usr.SignOut();
			}
			else 
				Response.Redirect("/");
		}
		#endregion
	}
}
