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
using System.Collections.Generic;

namespace Spotted.Controls
{
	public partial class BuddyControl : System.Web.UI.UserControl
	{
		public BuddyControl()
		{
			Query = new Query();
			ShowDenyOption = false;
		}
		public bool ShowDenyOption { get; set; }
		public bool ShowSelectCheckboxes { get; set; }
		public bool ShowSelectAllCheckbox { get; set; }
		public Buddy.BuddyFindingMethod FindMethod { get; set; }
		public bool NoRecordsToDisplay { get { return BuddyUsrList.Count == 0; } }
		public int ResultsCount { get { return BuddyUsrList.Count; } }
		public string NameStyle { get; set; }
		#region ImageSize
		public enum ImageSizes
		{
			Small,
			Normal
		}
		public ImageSizes ImageSize { get; set; }
		#endregion

		#region Buddy List
		public Query Query { get; set; }
		private List<Usr> buddyUsrList { get; set; }
		private List<Usr> BuddyUsrList
		{
			get
			{
				if (buddyUsrList == null)
				{
					if (SetUpQuery != null) SetUpQuery(this, EventArgs.Empty);

					// users want to look themselves up!
					//Query.QueryCondition = new And(Query.QueryCondition,
					//    new Q(Usr.Columns.K, QueryOperator.NotEqualTo, Usr.Current.K));

					if (Query.TableElement != null)
					{
						Query.TableElement = new Join(
								Query.TableElement,
								new TableElement(TablesEnum.Buddy),
								QueryJoinType.Left,
								new And(new Q(Usr.Columns.K, Buddy.Columns.BuddyUsrK), new Q(Buddy.Columns.UsrK, Usr.Current.K)));
					}
					else
					{
						Query.TableElement = new Join(
							Usr.Columns.K, Buddy.Columns.BuddyUsrK, QueryJoinType.Left, new Q(Buddy.Columns.UsrK, Usr.Current.K));
					}

					if (Query.OrderBy == null)
						Query.OrderBy = new OrderBy(Buddy.Columns.CanBuddyInvite, OrderBy.OrderDirection.Descending);

					buddyUsrList = new UsrSet(Query).ToList();
				}
				return buddyUsrList;
			}
		}
		#endregion

		public void uiBuddy_ChangePage(object o, GridViewPageEventArgs e)
		{
			PageIndex = e.NewPageIndex;
			this.DataBind();
		}
		public int PageIndex { set { uiBuddy.PageIndex = value; } }

		public event EventHandler SetUpQuery;
		public override void DataBind()
		{
			if (BuddyUsrList.Count > 0)
			{
				this.uiBuddy.DataSource = BuddyUsrList;
				this.uiBuddy.DataBind();
				this.uiBuddy.Visible = true;
			}
			else
			{
				this.uiBuddy.Visible = false;
			}
			this.uiNoRecords.Visible = this.NoRecordsToDisplay;
		}

		protected void uiBuddy_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if (this.ShowSelectCheckboxes)
				{
					AddCheckboxToSelectAllList(e.Row);
				}

				SetUpBuddyButtons(e.Row);
			}
		}

		#region SelectAll checkbox
		private List<string> MemberContactCheckBoxClientIDs = new List<string>();
		private void AddCheckboxToSelectAllList(GridViewRow row)
		{
			this.MemberContactCheckBoxClientIDs.Add("\"" + ((CheckBox)row.FindControl("uiCheckBox")).ClientID + "\"");
		}
		protected string MemberContactCheckBoxClientIDsAsString
		{
			get { return string.Join(",", MemberContactCheckBoxClientIDs.ToArray()); }
		}
		#endregion
		#region SetUpBuddyButtons
		private void SetUpBuddyButtons(GridViewRow row)
		{
			Usr usr = (Usr)row.DataItem;
			// you can't buddy yourself!
			if (usr.K == Usr.Current.K) return;

			Literal l = (Literal)row.FindControl("uiDbButtonScripts");
			l.Text = string.Format(@"
<p><script>
DbButton(
	""/gfx/icon-star-26-up.png"",
	""/gfx/icon-star-26-dn.png"",
	""{0} has been added to your buddy list"",
	""{0} is not on your buddy list"",
	""Remove from my buddy list"",
	""Add to my buddy list"",
	"""",
	""cursor:pointer;margin-right:3px;"",
	""absmiddle"",
	26,21,
	""Buddy"",
	""{1},{5}"",
	""{3}"",
	""{6}BuddyButton{2}"",
	""{6}BuddyButtonReturn{2}"",
	"""",
	"""");
function {6}BuddyButtonReturn{2}(id,oldState,newState)
{{
	DbButtonSetState(""{6}BuddyInviteButton{2}"",newState);
	" + (ShowDenyOption ? @"DbButtonSetState(""{6}BuddyDenyButton{2}"",false);" : "") + @"
}}</script></p>
<p><script>DbButton(
	""/gfx/icon-inbox-up.png"",
	""/gfx/icon-inbox-dn.png"",
	""{0} can invite you to chat topics"",
	""You have stopped {0} inviting you to chat topics"",
	""Stop inviting me in bulk to chat topics"",
	""Allow to invite me in bulk to chat topics"",
	"""",
	""cursor:pointer;margin-right:3px;"",
	""absmiddle"",
	26,21,
	""BuddyChatInvite"",
	""{1},{5}"",
	""{4}"",
	""{6}BuddyInviteButton{2}"",
	""{6}BuddyInviteButtonReturn{2}"",
	"""",
	"""");
function {6}BuddyInviteButtonReturn{2}(id,oldState,newState)
{{
	if (newState)
		DbButtonSetState(""{6}BuddyButton{2}"",true);
	" + (ShowDenyOption ? @"DbButtonSetState(""{6}BuddyDenyButton{2}"",false);" : "") + @"
}}</script></p>",
		usr.DisplayName(FindMethod),
		usr.K.ToString(),
		row.RowIndex.ToString(),
		usr.ExtraSelectElements["CanBuddyInvite"].ToString() != "" ? "1" : "0",
		usr.ExtraSelectElements["CanBuddyInvite"].ToString() == "True" ? "1" : "0",
		(int)FindMethod,
		this.ClientID.Replace("_", ""));

			if (ShowDenyOption)
			{
				l.Text += string.Format(@"
<p><script>DbButton(
	""/gfx/icon-cross-up.png"",
	""/gfx/icon-cross-dn.png"",
	"""",
	"""",
	""Leave buddy request in this list for later"",
	""Deny and remove buddy request from this list"",
	"""",
	""cursor:pointer;margin-right:3px;"",
	""absmiddle"",
	26,21,
	""BuddyDeny"",
	""{1}"",
	""{0}"",
	""{3}BuddyDenyButton{2}"",
	""{3}BuddyDenyButtonReturn{2}"",
	"""",
	"""");
function {3}BuddyDenyButtonReturn{2}(id,oldState,newState)
{{
	if (newState)
	{{
		DbButtonSetState(""{3}BuddyButton{2}"",false);
		DbButtonSetState(""{3}BuddyInviteButton{2}"",false);
	}}
}}</script></p>",
				usr.ExtraSelectElements["Denied"].ToString() == "True" ? "1" : "0",
				usr.K.ToString(),
				row.RowIndex.ToString(),
				this.ClientID.Replace("_", ""));
			}
		}
		#endregion

		protected void Page_PreRender(object o, EventArgs e)
		{
			this.uiToggleSelectAll.Visible = this.ShowSelectCheckboxes && this.ShowSelectAllCheckbox;
			this.uiBuddy.Columns[0].Visible = this.ShowSelectCheckboxes;
		}

		#region Selected Buddies
		public List<int> SelectedBuddyKs
		{
			get
			{
				if (this.ShowSelectCheckboxes == false)
				{
					throw new ArgumentException("ShowSelectCheckboxes", "must be true to use SelectedBuddyKs");
				}
				if (this.selectedBuddyKs == null)
				{
					selectedBuddyKs = new List<int>();
					foreach (GridViewRow gvr in uiBuddy.Rows)
					{
						if (((CheckBox)gvr.FindControl("uiCheckbox")).Checked)
						{
							selectedBuddyKs.Add((int)this.uiBuddy.DataKeys[gvr.RowIndex].Value);
						}
					}
				}
				return this.selectedBuddyKs;
			}
		}
		private List<int> selectedBuddyKs;
		#endregion
	}
}
