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
using Bobs;
using Spotted.Master;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Spotted.Controls
{
	public partial class MultiUsrDrop : EnhancedUserControl, IPostBackDataHandler
	{

		public MultiUsrDrop()
		{
			AnchorSkip = "";
			DropDownRows = 15;
			Width = 210;
			Height = 150;
			ThreadK = 0;
			restrictionGroupK = 0;
		}
		public string AnchorSkip { get; set; }
		public int DropDownRows { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		/// <summary>
		/// If this is set, the drop-down excludes all the buddies 
		/// that have a ThreadUsr for this thread
		/// </summary>
		public int ThreadK { get; set; }
		#region RestrictionGroupK
		/// <summary>
		/// If this is set, the drop-down excludes all the buddies 
		/// that have a GroupUsr for this group (that can't be invited)
		/// </summary>
		public int RestrictionGroupK
		{
			get
			{
				return restrictionGroupK;
			}
			set
			{
				restrictionGroupK = value;
			}
		}
		private int restrictionGroupK = 0;
		#endregion
		#region RestrictionGroup
		public Bobs.Group RestrictionGroup
		{
			get
			{
				if (restrictionGroup == null && RestrictionGroupK > 0)
					restrictionGroup = new Bobs.Group(RestrictionGroupK);

				return restrictionGroup;
			}
			set
			{
				restrictionGroup = value;
			}
		}
		private Bobs.Group restrictionGroup;
		#endregion
		#region RestrictionGroupUsr
		public GroupUsr RestrictionGroupUsr
		{
			get
			{
				if (!restrictionGroupUsrDone && RestrictionGroupK > 0)
				{
					restrictionGroupUsr = RestrictionGroup.GetGroupUsr(Usr.Current);
					restrictionGroupUsrDone = true;
				}
				return restrictionGroupUsr;
			}
			set
			{
				restrictionGroupUsr = value;
			}
		}
		private bool restrictionGroupUsrDone = false;
		private GroupUsr restrictionGroupUsr;
		#endregion
		#region RestrictionGroupQ
		public Q RestrictionGroupQ
		{
			get
			{
				if (!restrictionGroupQDone)
				{
					if (RestrictionGroupK > 0)
					{
						ArrayList al = new ArrayList();
						al.Add(new Q(GroupUsr.Columns.Status, QueryOperator.IsNull, null));
						if (!RestrictionGroup.Restriction.Equals(Bobs.Group.RestrictionEnum.Moderator) || Usr.Current.CanGroupMemberAdmin(RestrictionGroupUsr))
						{
							al.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Request));
							al.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Recommend));
						}
						if (Usr.Current.CanGroupMemberAdmin(RestrictionGroupUsr))
						{
							al.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.RequestRejected));
							al.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Barred));
							al.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.RecommendRejected));
						}
						restrictionGroupQ = new Or((Q[])al.ToArray(typeof(Q)));
					}
					else
					{
						restrictionGroupQ = new Q(true);
					}
					restrictionGroupQDone = true;
				}
				return restrictionGroupQ;
			}
			set
			{
				restrictionGroupQ = value;
			}
		}
		private bool restrictionGroupQDone = false;
		private Q restrictionGroupQ;
		#endregion

		public string ServerMethod, ServerType, ServerAssembly;
		public DropDownList Drop;
		public HtmlSelect SelectBox;
		public HtmlButton AddButton, RemoveButton;

		#region Page_PreRender(object o, System.EventArgs e)
		public void Page_PreRender(object o, System.EventArgs e)
		{
			if (Usr.Current != null)
			{
				ScriptManager.RegisterStartupScript(this, typeof(Page), this.UniqueID + "_StartUp", "MultiUsrDropInit('" + this.ClientID + "', '" + this.UniqueID + "');", true);

				if (Vars.DevEnv)
					ScriptManager.RegisterClientScriptInclude(this, typeof(Page), "MultiUsrDrop", "/misc/MultiUsrDrop.js?" + DateTime.Now.Ticks);
				else
					ScriptManager.RegisterClientScriptInclude(this, typeof(Page), "MultiUsrDrop", "/misc/MultiUsrDrop.js?a=9");
			}

			bool buddiesRemoved = Usr.Current != null && Buddies.Count != Usr.Current.BuddyCount && Buddies.Count > 0;

			if (Usr.Current != null && Buddies.Count == 0)
			{
				Drop.Enabled = false;
				if (Drop.Items.Count == 0)
					Drop.Items.Insert(0, new ListItem("(no buddies to select here)", "0"));
			}
			else
			{
				if (Drop.Items.Count == 0 || !Drop.Items[0].Value.Equals("0"))
					Drop.Items.Insert(0, new ListItem("Select a buddy, then click Add..." + (buddiesRemoved ? "*" : ""), "0"));
			}
		}
		#endregion
		#region Page_Load(object sender, System.EventArgs e)
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Usr.Current != null)
			{
				Page.RegisterRequiresPostBack(this);

				Drop.Style["width"] = Width.ToString() + "px";

				if (Vars.IE)
				{
					SelectBox.Style["position"] = "relative";
					SelectBox.Style["top"] = "-3px";
					SelectBox.Style["left"] = "-3px";
					SelectBox.Style["width"] = (Width + 3).ToString() + "px";
					SelectBox.Style["height"] = (Height + 6).ToString() + "px";
					ClipSpan.Style["width"] = (Width - 1).ToString() + "px";
					ClipSpan.Style["height"] = (Height - 4).ToString() + "px";
					ClipSpan.Style["margin-top"] = "-1px";
					ClipSpan.Style["overflow"] = "hidden";
					ClipSpan.Style["border"] = "solid 1px #999999";
				}
				else
				{
					SelectBox.Style["width"] = Width.ToString() + "px";
					SelectBox.Style["height"] = Height.ToString() + "px";
					SelectBox.Style["margin-top"] = "-1px";
				}
				if (!Vars.IE)
				{
					MoreButton.Style["margin-top"] = "-2px";
				}
				Drop.Attributes["onchange"] = "MultiUsrDropIndexChanged(event,'" + this.ClientID + "', '" + this.UniqueID + "')";
				Drop.Attributes["onkeypress"] = "return MultiUsrDropKeyPress(event, '" + this.ClientID + "', '" + this.UniqueID + "')";
				Drop.Attributes["onmouseup"] = "MultiUsrDropMouseUp(event, '" + this.ClientID + "', '" + this.UniqueID + "')";
				AddButton.Attributes["onclick"] = "MultiUsrDropAddItem('" + this.ClientID + "', '" + this.UniqueID + "');return false;";
				RemoveButton.Attributes["onclick"] = "MultiUsrDropRemoveItem('" + this.ClientID + "', '" + this.UniqueID + "');return false;";
				SelectBox.Attributes["onchange"] = "MultiUsrDropSelectChange('" + this.ClientID + "', '" + this.UniqueID + "')";
				SelectBox.Attributes["onkeyup"] = "return MultiUsrDropKeyUp(event, '" + this.ClientID + "', '" + this.UniqueID + "')";
				RemoveButton.Attributes["style"] = "margin-bottom:-1px;border:solid 1px #999999;margin-left:" + (Bobs.Vars.Netscape ? "-5" : "-1") + "px;font-weight:bold;width:61px;height:18px;";

				AddMoreAddBuddyCheckBox.Visible = Usr.Current != null && Usr.Current.IsAdmin;
				AddMoreTextBox.Style["Width"] = (Width - 10).ToString() + "px";

				BindDrop();
				this.DataBind();

				SetVisibility();
			}
		}
		#endregion
		#region SetVisibility()
		void SetVisibility()
		{
			int totalBuddies = 0;
			if (Usr.Current != null)
				totalBuddies = Usr.Current.BuddyCount;

			bool buddiesRemoved = Usr.Current != null && Buddies.Count != Usr.Current.BuddyCount && Buddies.Count > 0;

			MoreButton.Visible = Buddies.Count > 0;
			NoBuddiesThreadPanel.Visible = Buddies.Count == 0 && totalBuddies > 0;
			BuddiesRemovedPanel.Visible = buddiesRemoved && ThreadK > 0;
			BuddiesRemovedGroupPanel.Visible = buddiesRemoved && RestrictionGroupK > 0;
			if (buddiesRemoved)
			{
				int removed = Usr.Current.BuddyCount - Buddies.Count;
				BuddiesRemovedLabel.Text = "We have removed " + removed.ToString("#,##0") + " of your buddies";
				BuddiesRemovedGroupLabel.Text = "We have removed " + removed.ToString("#,##0") + " of your buddies";
			}
			NoBuddiesPanel.Visible = Buddies.Count == 0 && totalBuddies == 0;
			BuddiesTable.Visible = Buddies.Count > 0 || Items.Count > 0;
			AddAllPanel.Visible = ShowMore && Buddies.Count > 0;
			AddMorePanel.Visible = ShowMore || Buddies.Count == 0;
		}
		#endregion


		public void Reset()
		{
			Items.Clear();
			ItemsPlain.Clear();
			Buddies = null;
			AddMoreTextBox.Text = "";
			BindDrop();
			this.DataBind();
			SetVisibility();
		}

		#region Buddies
		public UsrSet Buddies
		{
			get
			{
				if (buddies == null)
				{
					Query q = new Query();
					q.OrderBy = new OrderBy(Usr.Columns.NickName);
					q.Columns = new ColumnSet(Usr.Columns.K, Usr.Columns.NickName, Usr.Columns.Pic, Usr.Columns.FacebookUID);
					if (ThreadK > 0)
					{
						q.TableElement = new Join(
							Usr.BuddyJoin,
							new TableElement(TablesEnum.ThreadUsr),
							QueryJoinType.Left,
							new And(
								new Q(Usr.Columns.K, ThreadUsr.Columns.UsrK, true),
								new Q(ThreadUsr.Columns.ThreadK, ThreadK)
							)
						);
						q.QueryCondition = new And(
							Usr.Current.BuddiesFullQ,
							new Q(ThreadUsr.Columns.UsrK, QueryOperator.IsNull, null));
					}
					else if (RestrictionGroupK > 0)
					{
						q.TableElement = new Join(
							Usr.BuddyJoin,
							new TableElement(TablesEnum.GroupUsr),
							QueryJoinType.Left,
							new And(
								new Q(Usr.Columns.K, GroupUsr.Columns.UsrK, true),
								new Q(GroupUsr.Columns.GroupK, RestrictionGroupK)
							)
						);

						q.QueryCondition = new And(
							Usr.Current.BuddiesFullQ,
							RestrictionGroupQ
						);
					}
					else
					{
						q.TableElement = Usr.BuddyJoin;
						q.QueryCondition = Usr.Current.BuddiesFullQ;
					}
					buddies = new UsrSet(q);
				}
				return buddies;
			}
			set
			{
				buddies = value;
			}
		}
		private UsrSet buddies;
		#endregion
		#region BindDrop()
		public void BindDrop()
		{
			Drop.DataSource = Buddies;
			Drop.DataTextField = "NickName";
			Drop.DataValueField = "MultiUsrDropValue";
		}
		#endregion

		#region ShowMore
		bool ShowMore
		{
			get
			{
				if (this.ViewState["More"] == null)
					return false;
				else
					return (bool)this.ViewState["More"];
			}
			set
			{
				this.ViewState["More"] = value;
			}
		}
		#endregion
		#region More_Click
		protected void More_Click(object sender, System.EventArgs e)
		{
			if (ShowMore)
			{
				MoreButton.InnerText = "More...";
				ShowMore = false;
			}
			else
			{
				MoreButton.InnerText = "Hide more...";
				BindAddAll();
				ShowMore = true;
			}
			SetVisibility();
			if (AnchorSkip.Length > 0)
				((Spotted.Master.DsiPage)Page).AnchorSkip(AnchorSkip);
		}
		#endregion

		#region AddAll
		#region AddAllNow_Click
		protected void AddAllNow_Click(object sender, System.EventArgs e)
		{

			Query q = new Query();
			Join j = new Join(
				Usr.BuddyUsrJoin,
				new TableElement(TablesEnum.UsrMusicTypeFavourite),
				QueryJoinType.Left,
				Usr.Columns.K,
				UsrMusicTypeFavourite.Columns.UsrK);
			q.TableElement = new Join(
				j,
				new TableElement(TablesEnum.UsrPlaceVisit),
				QueryJoinType.Left,
				Usr.Columns.K,
				UsrPlaceVisit.Columns.UsrK);

			if (ThreadK > 0)
			{
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.ThreadUsr),
					QueryJoinType.Left,
					new And(
						new Q(Usr.Columns.K, ThreadUsr.Columns.UsrK, true),
						new Q(ThreadUsr.Columns.ThreadK, ThreadK)
					)
				);

			}
			else if (RestrictionGroupK > 0)
			{
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.GroupUsr),
					QueryJoinType.Left,
					new And(
						new Q(Usr.Columns.K, GroupUsr.Columns.UsrK, true),
						new Q(GroupUsr.Columns.GroupK, RestrictionGroupK)
					)
				);
			}

			Q placeQ = new Q(true);
			if (!AddAllPlaceDrop.SelectedValue.Equals("0"))
			{
				placeQ = new Or(
					new Q(Usr.Columns.HomePlaceK, int.Parse(AddAllPlaceDrop.SelectedValue)),
					new Q(UsrPlaceVisit.Columns.PlaceK, int.Parse(AddAllPlaceDrop.SelectedValue)));
			}
			int musicTypeK = int.Parse(AddAllMusicDrop.SelectedValue);
			Q musicQ = new Q(true);
			if (musicTypeK > 1)
			{
				ArrayList musicQs = new ArrayList();
				MusicType mt = new MusicType(musicTypeK);
				musicQs.Add(new Q(Usr.Columns.FavouriteMusicTypeK, 1));
				musicQs.Add(new Q(UsrMusicTypeFavourite.Columns.MusicTypeK, 1));
				musicQs.Add(new Q(Usr.Columns.FavouriteMusicTypeK, musicTypeK));
				musicQs.Add(new Q(UsrMusicTypeFavourite.Columns.MusicTypeK, musicTypeK));
				foreach (MusicType mtChild in mt.Children)
				{
					musicQs.Add(new Q(Usr.Columns.FavouriteMusicTypeK, mtChild.K));
					musicQs.Add(new Q(UsrMusicTypeFavourite.Columns.MusicTypeK, mtChild.K));
				}
				musicQ = new Or((Q[])musicQs.ToArray(typeof(Q)));
			}

			Q restrictionQ = new Q(true);
			if (ThreadK > 0)
			{
				restrictionQ = new Q(ThreadUsr.Columns.UsrK, QueryOperator.IsNull, null);
			}
			else if (RestrictionGroupK > 0)
			{
				restrictionQ = RestrictionGroupQ;
			}

			q.QueryCondition = new And(
				new Q(Buddy.Columns.BuddyUsrK, Usr.Current.K),
				new Q(Buddy.Columns.FullBuddy, true),
				new Q(Buddy.Columns.CanBuddyInvite, true),
				restrictionQ,
				musicQ,
				placeQ);
			q.Columns = new ColumnSet(Usr.Columns.NickName, Usr.Columns.K, Usr.Columns.Pic, Usr.Columns.FacebookUID);
			q.Distinct = true;
			q.OrderBy = new OrderBy(Usr.Columns.NickName);
			q.DistinctColumn = Usr.Columns.K;
			UsrSet us = new UsrSet(q);
			int duplicate = 0;
			int done = 0;
			foreach (Usr u in us)
			{
				bool yes = this.AddUsr(u);
				if (yes)
					done++;
				else
					duplicate++;
			}
			string selectedDuplicate = "";
			if (duplicate > 0)
				selectedDuplicate = us.Count.ToString("#,##0") + " " + (us.Count == 1 ? "buddy" : "buddies") + " selected\n" + duplicate.ToString("#,##0") + " of them " + (duplicate == 1 ? "was" : "were") + " already in the list\n";

			AlertMessageAll(selectedDuplicate + done.ToString("#,##0") + " " + (done == 1 ? "buddy" : "buddies") + " added");

			if (done > 0)
			{
				MoreButton.InnerText = "More...";
				ShowMore = false;
				SetVisibility();
			}

			if (AnchorSkip.Length > 0)
				((Spotted.Master.DsiPage)Page).AnchorSkip(AnchorSkip);

		}
		#endregion

		protected PlaceHolder AddAllMessagePlaceHolder, AddMoreMessagePlaceHolder;
		public void AlertMessageAll(string message)
		{
			AddAllMessagePlaceHolder.Controls.Add(new LiteralControl("<p style=\"color:blue;\">" + message.Replace("\n", "<br>") + "</p>"));
		}
		public void AlertMessageEmail(string message)
		{
			AddMoreMessagePlaceHolder.Controls.Add(new LiteralControl("<p style=\"color:blue;\">" + message.Replace("\n", "<br>") + "</p>"));
		}

		protected string GetMusicTypeName(int K)
		{
			if (K == 1)
				return "any music";
			else if (K == 4)
				return "house music";
			else if (K == 10)
				return "hard-dance";
			else if (K == 15)
				return "alternative-dance music";
			else if (K == 20)
				return "techno";
			else if (K == 24)
				return "drum-and-bass";
			else if (K == 28)
				return "urban";
			else if (K == 35)
				return "chillout";
			else if (K == 36)
				return "alternative";
			else if (K == 42)
				return "commercial music";
			else if (K == 46)
				return "retro music";
			else
				return "";
		}
		void BindAddAll()
		{
			if (Buddies.Count > 0)
			{
				Query qMusic = new Query();
				qMusic.Columns = new ColumnSet(MusicType.Columns.K, MusicType.Columns.Name, MusicType.Columns.ParentK);
				if (AddAllShowAllItemsCheck.Checked || ((DsiPage)Page).RelevantMusic.Count == 0)
				{
					qMusic.QueryCondition = new Q(true);
				}
				else
				{
					ArrayList alMusicQ = new ArrayList();
					foreach (int mK in ((DsiPage)Page).RelevantMusic)
						alMusicQ.Add(new Q(MusicType.Columns.K, mK));
					qMusic.QueryCondition = new Or((Q[])alMusicQ.ToArray(typeof(Q)));
				}
				qMusic.OrderBy = new OrderBy(MusicType.Columns.Order);
				MusicTypeSet mts = new MusicTypeSet(qMusic);

				ArrayList musicTypesDone = new ArrayList();
				AddAllMusicDrop.Items.Clear();
				musicTypesDone.Add(1);
				AddAllMusicDrop.Items.Add(new ListItem(GetMusicTypeName(1), "1"));

				foreach (MusicType mt in mts)
				{
					if (!musicTypesDone.Contains(mt.ParentK) && mt.ParentK != 0)
					{
						musicTypesDone.Add(mt.ParentK);
						AddAllMusicDrop.Items.Add(new ListItem(GetMusicTypeName(mt.ParentK), mt.ParentK.ToString()));
					}
					if (!musicTypesDone.Contains(mt.K))
					{
						musicTypesDone.Add(mt.K);
						AddAllMusicDrop.Items.Add(new ListItem((mt.ParentK == 1 ? "" : " - ") + mt.Name.ToLower(), mt.K.ToString()));
					}
				}

				Query qPlace = new Query();
				qPlace.Columns = new ColumnSet(Place.Columns.Name, Place.Columns.K);
				if (AddAllShowAllItemsCheck.Checked || ((DsiPage)Page).RelevantPlaces.Count == 0)
				{
					qPlace.QueryCondition = Country.PlaceFilterQ;
				}
				else
				{
					ArrayList alPlaceQ = new ArrayList();
					foreach (int pK in ((DsiPage)Page).RelevantPlaces)
						alPlaceQ.Add(new Q(Place.Columns.K, pK));
					qPlace.QueryCondition = new Or((Q[])alPlaceQ.ToArray(typeof(Q)));
				}
				qPlace.OrderBy = new OrderBy(Place.Columns.Name);
				PlaceSet ps = new PlaceSet(qPlace);

				AddAllPlaceDrop.Items.Clear();
				AddAllPlaceDrop.Items.Add(new ListItem("any town", "0"));
				foreach (Place p in ps)
				{
					AddAllPlaceDrop.Items.Add(new ListItem(p.Name, p.K.ToString()));
				}
			}
		}
		protected void AddAllShowAllItemsCheck_Change(object sender, System.EventArgs e)
		{
			BindAddAll();
			if (AnchorSkip.Length > 0)
				((DsiPage)Page).AnchorSkip(AnchorSkip);
		}

		#endregion

		#region AddMore
		#region AddBuddies
		bool AddBuddies
		{
			get
			{
				if (Usr.Current == null)
					return true;
				if (Usr.Current.IsAdmin)
					return AddMoreAddBuddyCheckBox.Checked;
				else
					return true;
			}
		}
		#endregion
		#region AddMore_Click
		public void AddMore_Click(Object o, System.EventArgs e)
		{

			AddMoreProcessReturn d = AddMoreProcess();
			int duplicate = d.Duplicate;
			int total = d.Total;
			int done = d.Done;

			string selectedDuplicate = "";
			if (duplicate > 0)
				selectedDuplicate = total.ToString("#,##0") + " " + (total == 1 ? "buddy" : "buddies") + " selected\n" + duplicate.ToString("#,##0") + " of them " + (duplicate == 1 ? "was" : "were") + " already in the list\n";

			AlertMessageEmail(selectedDuplicate + done.ToString("#,##0") + " " + (done == 1 ? "buddy" : "buddies") + " added");
			//AddMoreResultsP.InnerHtml=selectedDuplicate+done.ToString("#,##0")+" "+(done==1?"buddy":"buddies")+" added";
			//AddMoreResultsP.Visible=true;

			AddMoreTextBox.Text = "";

			if (total > 0)
			{
				MoreButton.InnerText = "More...";
				ShowMore = false;
				SetVisibility();
			}

			if (AnchorSkip.Length > 0)
				((DsiPage)Page).AnchorSkip(AnchorSkip);
		}
		#endregion
		public AddMoreProcessReturn AddMoreProcess()
		{
			return AddEmails(AddMoreTextBox.Text);
		}
		public AddMoreProcessReturn AddEmails(string emailList)
		{
			ArrayList matchedEmails = new ArrayList();
			string[] arr = Regex.Replace(emailList, @"[\s,]+", @" ").Split(' ');
			foreach (string c in arr)
			{
				if (Regex.Match(c, Cambro.Misc.RegEx.Email).Success)
				{
					if (!matchedEmails.Contains(c.ToLower().Trim()))
						matchedEmails.Add(c.ToLower().Trim());
				}
			}
			int total = 0;
			int duplicate = 0;
			int done = 0;
			foreach (string c in matchedEmails)
			{
				Usr u = Usr.GetOrCreateSkeletonUser(Usr.Current, c, "", null, "");

				if (AddBuddies)
					Usr.Current.AddBuddy(u, Usr.AddBuddySource.InvitePanelEmails, Buddy.BuddyFindingMethod.EmailAddress, null);

				bool yes = this.AddUsr(u);
				if (yes)
					done++;
				else
					duplicate++;

				total++;
			}
			AddMoreProcessReturn r = new AddMoreProcessReturn();
			r.Total = total;
			r.Duplicate = duplicate;
			r.Done = done;
			return r;
		}
		public class AddMoreProcessReturn
		{
			public int Total = 0;
			public int Duplicate = 0;
			public int Done = 0;
		}
		#endregion

		#region Items
		public ListItemCollection Items
		{
			get
			{
				if (items == null)
					items = new ListItemCollection();
				return items;
			}
			set
			{
				items = value;
			}
		}
		private ListItemCollection items;
		public ListItemCollection ItemsPlain
		{
			get
			{
				if (itemsPlain == null)
					itemsPlain = new ListItemCollection();
				return itemsPlain;
			}
			set
			{
				itemsPlain = value;
			}
		}
		private ListItemCollection itemsPlain;
		#endregion
		#region AddUsr(Usr u)
		public bool AddUsr(Usr u)
		{
			if (Items.FindByValue(u.K.ToString()) == null)
			{
				if (u.NickName.Length == 0)
				{
					Items.Add(new ListItem(u.Email, u.K.ToString()));
					ItemsPlain.Add(new ListItem(u.Email, u.K + "$" + u.Pic.ToString().ToLower()));
				}
				else
				{
					Items.Add(new ListItem(u.NickName, u.K.ToString()));
					ItemsPlain.Add(new ListItem(u.NickName, u.K + "$" + u.Pic.ToString().ToLower()));
				}
				return true;
			}
			else
				return false;
		}
		#endregion
		#region Usrs
		/// <summary>
		/// This is a list of nickanames
		/// </summary>
		public List<string> UsrNicknames
		{
			get
			{
				if (DoneUsrs == false)
					GenUsrs();
				return usrNicknames;
			}
		}
		/// <summary>
		/// This is a list of emails
		/// </summary>
		public List<string> UsrEmails
		{
			get
			{
				if (DoneUsrs == false)
					GenUsrs();
				return usrEmails;
			}
		}
		void GenUsrs()
		{
			usrNicknames = new List<string>();
			usrEmails = new List<string>();

			if (Items.Count == 0)
				return;

			ArrayList kAry = new ArrayList();
			foreach (ListItem li in Items)
			{
				if (!kAry.Contains(int.Parse(li.Value)))
				{
					if (li.Text.Length > 0)
					{
						if (li.Text.IndexOf("@") == -1)
							usrNicknames.Add(li.Text);
						else
							usrEmails.Add(li.Text);

						kAry.Add(int.Parse(li.Value));
					}
				}
			}
			DoneUsrs = true;
		}
		List<string> usrNicknames;
		List<string> usrEmails;
		bool DoneUsrs = false;
		#endregion

		#region LoadPostData
		public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
		{
			string texts = Texts.Value;
			string values = Values.Value;
			string[] textsAry = texts.Split('&');
			string[] valuesAry = values.Split('&');
			for (int i = 0; i < textsAry.Length; i++)
			{
				if (valuesAry[i].Length > 0)
				{
					ItemsPlain.Add(
						new ListItem(
						HttpUtility.UrlDecode(textsAry[i]),
						HttpUtility.UrlDecode(valuesAry[i])
						)
						);
					Items.Add(
						new ListItem(
						HttpUtility.UrlDecode(textsAry[i]),
						HttpUtility.UrlDecode(valuesAry[i]).Split('$')[0]
						)
						);
				}
			}
			return false;
		}
		#endregion
		#region IPostBackDataHandler Members

		public void RaisePostDataChangedEvent()
		{
			// TODO:  Add ucDbComboMulti.RaisePostDataChangedEvent implementation
		}

		#endregion
		#region Render(HtmlTextWriter writer)
		protected override void Render(HtmlTextWriter writer)
		{
			// TODO:  Add ucDbComboMulti.Render implementation
			base.Render(writer);
		}
		#endregion
		#region LoadViewState(object savedState)
		protected override void LoadViewState(object savedState)
		{
			Texts.Value = (string)this.ViewState["Texts"];
			Values.Value = (string)this.ViewState["Values"];
			base.LoadViewState(savedState);
		}
		#endregion
		#region SaveViewState()
		protected override object SaveViewState()
		{
			string texts = "";
			string values = "";
			SelectBox.Items.Clear();
			foreach (ListItem li in ItemsPlain)
			{
				SelectBox.Items.Add(new ListItem(li.Text, li.Value));
				texts += (texts.Length > 0 ? "&" : "") + HttpUtility.UrlEncodeUnicode(li.Text).Replace("+", "%20");
				values += (values.Length > 0 ? "&" : "") + HttpUtility.UrlEncodeUnicode(li.Value).Replace("+", "%20");

			}
			Texts.Value = texts;
			Values.Value = values;

			this.ViewState["Texts"] = Texts.Value;
			this.ViewState["Values"] = Values.Value;
			return base.SaveViewState();
		}
		#endregion

	}
}
